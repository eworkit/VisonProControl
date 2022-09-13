using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Utilities.UI.ExMethod
{
   public static   class CtrlExMethod
   {
       [DllImport("user32.dll")]
       public static extern bool ReleaseCapture();
       [DllImport("user32.dll")]
       public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
       public const int WM_SYSCOMMAND = 0x0112;
       public const int SC_MOVE = 0xF010;
       public const int HTCAPTION = 0x0002;
       public static void SafeInvoke(this Control control, Action handler)
       {
           if (control == null)
               handler();
           try
           {
               if (control.InvokeRequired)
               {
                   control.Invoke(handler);
                   return;
               }
           }
           catch (Exception ex){ }
           handler();
       }
       public static void BeginSafeInvoke(this Control control, Action handler)
       {
           if (control == null)
               handler();
           try
           {
               if (control.InvokeRequired)
               {
                   control.BeginInvoke(handler);
                   return;
               }
           }
           catch { }
           handler();

       }
        public static bool IsRowSelected(this DataGridView gv)
        {
            if (gv.SelectedRows.Count == 0)
                return false;
            if (gv.SelectedRows[0].Index == gv.NewRowIndex)
                return false;
            return true;
        }

        public static int SelectIndex(this DataGridView gv)
        {
            if (gv.SelectedRows.Count > 0)
                return gv.SelectedRows[0].Index;
            return -1;
        }
        public static int SetRowSelect(this DataGridView gv, int X, int Y)
        {
            gv.ClearSelection();
            var h = gv.HitTest(X, Y);
            int row = h.RowIndex;
            if (row < 0)
            {
                gv.ClearSelection();
                return -1;
            }
            SetRowSelect(gv, row);
            return row;
        }
        public static void SetRowSelect(this DataGridView gv, int row)
        {
            if (gv.Rows.Count == 0)
                return;
            gv.ClearSelection();           
            if (row < 0)
                row = 0;
            else if (row > gv.Rows.Count - 1)
                row = gv.Rows.Count - 1;
            gv.Rows[row].Selected = true;
            try
            {
                foreach (DataGridViewColumn col in gv.Columns)
                    if (col.Visible)
                    {
                        gv.CurrentCell = gv.Rows[row].Cells[col.Name];
                        break;
                    }
            }
            catch { }
          //  gv.CurrentCell.Selected = true;
        }
        public static int GetRowIndexAt(this DataGridView dgv, Point pt)
        {
            if (dgv.FirstDisplayedScrollingRowIndex < 0)
            {
                return -1;  // no rows.   
            }
            if (dgv.ColumnHeadersVisible == true && pt.Y <= dgv.ColumnHeadersHeight)
            {
                return -2;//above the first displayed row
            }
            int index = dgv.FirstDisplayedScrollingRowIndex;
            int displayedCount = dgv.DisplayedRowCount(true);
            for (int k = 1; k <= displayedCount; )  // 因为行不能ReOrder，故只需要搜索显示的行   
            {
                if (dgv.Rows[index].Visible == true)
                {
                    Rectangle rect = dgv.GetRowDisplayRectangle(index, true);  // 取该区域的显示部分区域   
                    //if (rect.Top <= pt.Y && pt.Y < rect.Bottom)
                    if (rect.Contains(pt))
                    {
                        return index;
                    }
                    k++;  // 只计数显示的行;   
                }
                index++;
            }
            return -1;

            /*DataGridView.HitTestInfo hti = dgv.HitTest(pt.X, pt.Y);
            return hti.RowIndex;*/
        }
        public static TreeNode FindParent(this TreeNode n, Predicate<TreeNode> predicate)
        {
            var p = n;
            while (p != null)
            {
                if (predicate(p))
                    return p;
                p = p.Parent;
            }
            return null;
        }
        public static void CheckAllChildNodes(this TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the
                    // CheckAllChildNodes method recursively.
                    CheckAllChildNodes(node, nodeChecked);
                }
            }
        }
        public const int WM_USER = 0x0400;
        public const int EM_GETPARAFORMAT = WM_USER + 61;
        public const int EM_SETPARAFORMAT = WM_USER + 71;
        public const long MAX_TAB_STOPS = 32;
        public const uint PFM_LINESPACING = 0x00000100;
        [System.Runtime.InteropServices.StructLayout(LayoutKind.Sequential)]
        private struct PARAFORMAT2
        {
            public int cbSize;
            public uint dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [System.Runtime.InteropServices.MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }


        [System.Runtime.InteropServices.DllImport("user32", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(System.Runtime.InteropServices.HandleRef hWnd, int msg, int wParam, ref PARAFORMAT2 lParam);

        public static void SetMove(this Form f, Control c)
        {
            c.MouseDown += (s, e) =>
            {
                ReleaseCapture();
                SendMessage(f.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            };
        }  /// <summary>
        /// 设置行距
        /// </summary>
        /// <param name="ctl">控件</param>
        /// <param name="dyLineSpacing">间距</param>
        public static void SetLineSpace(this Control ctl, int dyLineSpacing)
        {
            PARAFORMAT2 fmt = new PARAFORMAT2();
            fmt.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(fmt);
            fmt.bLineSpacingRule = 4;// bLineSpacingRule;
            fmt.dyLineSpacing = dyLineSpacing;
            fmt.dwMask = PFM_LINESPACING;
            try
            {
                SendMessage(new HandleRef(ctl, ctl.Handle), EM_SETPARAFORMAT, 0, ref fmt);
            }
            catch
            {

            }
        }
        public static void SetMove(this Control c)
        {
            SetMove(c, c);
        }

        public static void SetMove(this  Control Target, params object[] drags)
        {
            bool leftFlag = false;
            Point mouseOff = Target.Location;
            MouseEventHandler Down = (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    mouseOff = new Point(-e.X, -e.Y);
                    leftFlag = true;
                }
            };
            MouseEventHandler Move = (s, e) =>
            {
                if (leftFlag)
                {
                    Target.Cursor = Cursors.Arrow;
                    Point mouseSet = Control.MousePosition;
                    mouseSet.Offset(mouseOff.X, mouseOff.Y);
                    //  MoveControl.Location = mouseSet;
                    new System.Threading.Thread(() => Target.SafeInvoke(() => Target.Location = Target.Parent == null ? Target.PointToClient(mouseSet) : Target.Parent.PointToClient(mouseSet))).Start();
                }
            };
            MouseEventHandler Up = (s, e) =>
            {
                if (leftFlag)
                    leftFlag = false;
            };
            foreach (var d in drags)
            {
                if (d is Control)
                {
                    var c = d as Control;
                    c.MouseDown += Down;
                    c.MouseMove += Move;
                    c.MouseUp += Up;
                }
                else if (d is ToolStripItem)
                {
                    var c = d as ToolStripItem;
                    c.MouseDown += Down;
                    c.MouseMove += Move;
                    c.MouseUp += Up;
                }
            }
        }

    }
}
