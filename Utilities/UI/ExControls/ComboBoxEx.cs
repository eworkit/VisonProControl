using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using Utilities.ExMethod;
 
namespace Utilities.UI
{
    public class ComboBoxEx : ComboBox
    {
        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        static extern IntPtr GetWindowDC(IntPtr hWnd);//返回hWnd参数所指定的窗口的设备环境。  

        [System.Runtime.InteropServices.DllImport("user32.dll ")]
        static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC); //函数释放设备上下文环境（DC）  
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);
        int GW_CHILD = 5;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        public const int EM_SETREADONLY = 0xcf;
        bool readOnly;
        public bool ReadOnly
        {
            get { return readOnly; }
            set
            {
                readOnly = value;
              if(value)
              {
                 // DropDownStyle = ComboBoxStyle.DropDownList;
                 
              }
            }
        }
        public ComboBoxEx()
        {
            ReadOnly = false;
            IntPtr editHandle = GetWindow(Handle, GW_CHILD);
           var i=  SendMessage(editHandle, EM_SETREADONLY, 1, 0);
           EnabledChanged += ComboBoxEx_EnabledChanged;
          
             DrawItem+=ComboBoxEx_DrawItem;
            this.KeyDown += ComboBoxEx_KeyDown;
        }

        private void ComboBoxEx_KeyDown(object sender, KeyEventArgs e)
        {
            if (readOnly)
                e.Handled = true;
           
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (ReadOnly)
            base.OnPaint(e);
            else
            {
                if(Items.Count==0)
                    e.Graphics.DrawString("", this.Font, new SolidBrush(ForeColor), new Point(1, 1));
                else
                e.Graphics.DrawString(Items[0].ToString(), this.Font, new SolidBrush(ForeColor), new Point(1, 1));
            }
        }

        void ComboBoxEx_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox cmb = ((ComboBox)(sender));
            if (ReadOnly)

            { e.DrawBackground(); }
            if (e.Index < 0)
            {

                e.Graphics.DrawString("", e.Font, new SolidBrush(e.ForeColor), new Point(e.Bounds.Left, e.Bounds.Top));
            }
            else
            {


                e.Graphics.DrawString(cmb.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), new Point(e.Bounds.Left, e.Bounds.Top));
            }
        }

        void ComboBoxEx_EnabledChanged(object sender, EventArgs e)
        {
            IntPtr editHandle = GetWindow(Handle, GW_CHILD);
            var i = SendMessage(editHandle, EM_SETREADONLY, -1, 0); 
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (ReadOnly)
            {
               // if (e.KeyChar  !=(char)Keys.Back &&(int)e.KeyChar < 32)  // 控制键
                if(e.KeyChar==(char)Keys.Tab)
                {
                    return;
                }
                e.Handled = true;
                return;
            }
        }
        protected override void WndProc(ref Message m)
        {
            //if (ReadOnly && (m.Msg == 0xa1 || m.Msg == 0x201 || m.Msg == 0x007B|| m.Msg == 0x301 || m.Msg == 0x302 || m.Msg == 0x203 || m.Msg == 0x204 || m.Msg == 0x205))
            //{
            //    return;
            //}
            base.WndProc(ref m);
            return;
            //WM_PAINT = 0xf; 要求一个窗口重画自己,即Paint事件时  
            //WM_CTLCOLOREDIT = 0x133;当一个编辑型控件将要被绘制时发送此消息给它的父窗口；  
            //通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色  
            //windows消息值表,可参考:http://hi.baidu.com/dooy/blog/item/0e770a24f70e3b2cd407421b.html  
            if (readOnly&&( m.Msg == 0xf /*|| m.Msg == 0x133*/))
            {
                if(Enabled!=false)
                     Enabled = false;
                IntPtr hDC = GetWindowDC(m.HWnd);
                if (hDC.ToInt32() == 0) //如果取设备上下文失败则返回  
                {
                    return;
                }
               
                //建立Graphics对像  
                Graphics g = Graphics.FromHdc(hDC);
                using (Pen pen = new Pen(System.Windows.Forms.VisualStyles.VisualStyleInformation.TextControlBorder))
                   {
                       g.DrawRectangle(pen, new Rectangle(0, 0, Width, Height));
                       g.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, 1, Width-2, Height-2));
                       g.DrawString(Items[0].ToString(), Font, new SolidBrush( ForeColor), new Point(3, 4));
                   }
                //画边框的   
                //ControlPaint.DrawBorder(g, new Rectangle(0, 0, Width, Height), Color.Silver, ButtonBorderStyle.Solid);
                //画坚线  
            //    ControlPaint.DrawBorder(g, new Rectangle(Width - Height, 0, Height, Height), Color.Red, ButtonBorderStyle.Solid);
                //g.DrawLine(new Pen(Brushes.Blue, 2), new PointF(this.Width - this.Height, 0), new PointF(this.Width - this.Height, this.Height));  
                //释放DC    
                ReleaseDC(m.HWnd, hDC);
            }  
            
            //if (m.Msg != 0x007B && m.Msg != 0x0301 && m.Msg != 0x0302 && m.Msg != 0x0204 && m.Msg != 0x0205)
            //{
            //    base.WndProc(ref m);
            //}
        }

      
    }
    public class ComboBoxMultiSelect : ComboBoxEx
    {
        Form fLst = new Form();        
        ListBox lst = new ListBox();
        public string LabInfo { get { return lab.Text; } set { lab.Text = value; tt.SetToolTip(lab, value); } }
        Label lab = new Label();
        ToolTip tt = new ToolTip();
        bool showLab = true;
        public string SeperatorText { get; set; }
        public bool ShowLab
        {
            get { return showLab; }
            set
            {
                showLab = value;
                SelectionMode = lst.SelectionMode;
            }
        }

        private readonly int MOUSEEVENTF_LEFTDOWN = 0x2;
        private readonly int MOUSEEVENTF_LEFTUP = 0x4;
        private readonly int MOUSEEVENTF_RIGHTDOWN = 0x008;
        private readonly int MOUSEEVENTF_RIGHTUP = 0x0010;

        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SetCursorPos")]  
        private static extern int SetCursorPos(int x, int y);
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public SelectionMode SelectionMode
        {
            get { return lst.SelectionMode; }
            set
            {
                lst.SelectionMode = value;
                if(value== SelectionMode.One || !showLab)
                {
                    lab.Visible = false;
                    lab.Height = 0;
                }
                else if(showLab)
                {
                    lab.Visible = true;
                    lab.Height = 12;
                } 
            }
        }
        public ComboBoxMultiSelect()
        {
            SeperatorText = ",";
            lst.SelectionMode = SelectionMode.MultiExtended;
            this.DrawMode = DrawMode.OwnerDrawFixed;//只有设置这个属性为OwnerDrawFixed才可能让重画起作用
            lst.KeyUp += new KeyEventHandler(lst_KeyUp);
            lst.MouseUp += new MouseEventHandler(lst_MouseUp);
            lst.MouseDoubleClick += lst_MouseDoubleClick;
            lst.SelectedIndexChanged += lst_SelectedIndexChanged;
            lst.MouseMove += lst_MouseMove;
            lst.KeyDown += new KeyEventHandler(lst_KeyDown);
            fLst.FormBorderStyle = FormBorderStyle.None;
            fLst.AutoSize = false;
            fLst.TopMost = true;
            fLst.ShowInTaskbar = false;
            fLst.Height = lst.Height;

            fLst.AutoScroll = true;
            fLst.Controls.Add(lst);
          
           // lab.Top = f + 3;
            lab.BackColor = Color.Transparent;
            lab.ForeColor = Color.Gray;
            lab.Dock = DockStyle.Bottom;
            lab.TextAlign = ContentAlignment.MiddleLeft;
            if (LabInfo.IsEmpty())
                lab.Text = "Press the \"CTRL\" key to multi-select";
            else lab.Text = LabInfo;
          
            tt.ShowAlways = true;
            tt.Active = true;
            tt.SetToolTip(lab, lab.Text);
            fLst.Controls.Add(lab);
            lst.Dock = DockStyle.Fill;
            lst.VisibleChanged += new EventHandler(lst_VisibleChanged);
            fLst.Location = new Point(this.Left, this.Top + this.ItemHeight + 6);
            lst.LostFocus += new EventHandler(fLst_LostFocus);
            fLst.LostFocus+=fLst_LostFocus;
            fLst.VisibleChanged += (s, e) => { if (fLst.Visible) lab.Focus(); };
            lab.AutoSize = false;
            lab.Height = 12;
            if (!ShowLab)
                lab.Height = 0;
            OnDropDown(null);
            fLst.Hide();
            this.LostFocus += ComboBoxEx_LostFocus;
        }

        int lastMouseItem = -1;
        void lst_MouseMove(object sender, MouseEventArgs e)
        {
            int n=lst.IndexFromPoint(e.Location);
            if(n<0)
                return;
            if (lastMouseItem == n)
                return;
            lastMouseItem = n;
            tt.SetToolTip(lst, lst.Items[n].ToStr());
        }

        void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Text = "";
                for (int i = 0; i < lst.SelectedItems.Count; i++)
                {
                    if (i == 0)
                        this.Text = lst.SelectedItems[i].ToString();
                    else
                    {
                        this.Text = this.Text + SeperatorText + lst.SelectedItems[i].ToString();
                    }
                }
            }
            catch
            {
                this.Text = "";
            }
            bool isControlPressed = (Control.ModifierKeys == Keys.Control);
            bool isShiftPressed = (Control.ModifierKeys == Keys.Shift);
            if (isControlPressed || isShiftPressed)
                lst.Show();
            else
                lst.Hide();
           // tt.SetToolTip(this, Text);
           
        }


        void ComboBoxEx_LostFocus(object sender, EventArgs e)
        {
             
        }

        void lst_VisibleChanged(object sender, EventArgs e)
        {
            fLst.Visible = lst.Visible;
        }

        void fLst_LostFocus(object sender, EventArgs e)
        {
            if (lst.Focused || fLst.Focused || lab.Focused)
                return;
            fLst.Hide();
        }
        #region Property
        [Description("选定项的值"), Category("Data")]
        public ListBox.SelectedObjectCollection SelectedItems
        {
            get
            {
                return lst.SelectedItems;
            }
        }

        #endregion

        #region override

    
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            bool Pressed = (e.Control && ((e.KeyData & Keys.A) == Keys.A));
            if (Pressed)
            {
                for (int i = 0; i < lst.Items.Count; i++)
                    lst.SetSelected(i, true);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
         //  
            this.DropDownHeight =1;
            //if (bDropTag)
            //{
            //    fLst.Hide();
            //    bDropTag = false;
            //}else
            //bDropTag = true;
           // fLst.Select();
           // fLst.Focus();
         //   this.DroppedDown = false;
            
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (!fLst.Visible)
                return;

            if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor > 0)
            {
                int X, Y;
                var mp = MousePosition;
                var pos = lab.RectangleToScreen(lab.ClientRectangle);
                X = pos.X; Y = pos.Y;
                SetCursorPos(X + 1, Y + 1);
                mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
                //  mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                SetCursorPos(mp.X, mp.Y);
                //   this.DroppedDown = false;
                //   lst.Focus();            
                //fLst.Select();
                //fLst.Focus();
                //fLst.BringToFront(); 
            }
        }

        bool bDropTag=false;
        protected override void OnDropDown(EventArgs e)
        {
            this.DropDownHeight = 1;
            //  if (bDropTag) return;
            // this.DroppedDown = false;
            lst.Items.Clear();
        
            lst.MaximumSize = new Size(800, 800);

            lst.ItemHeight = this.ItemHeight;
            lst.BorderStyle = BorderStyle.FixedSingle;
            //  lst.Size = new Size(this.DropDownWidth, this.ItemHeight * (this.MaxDropDownItems - 1) - (int)this.ItemHeight / 2);
            fLst.Width = this.DropDownWidth;
            // lst.Location = new Point(this.Left, this.Top + this.ItemHeight + 6);
            lst.BeginUpdate();
            var selectedItemArr = this.Text.Split(SeperatorText .ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
            for (int i = 0; i < this.Items.Count; i++)
            {
                int n = lst.Items.Add(this.Items[i]);
                if (selectedItemArr.Contains(Items[i].ToStr()))
                {
                    lst.SetSelected(n, true);
                    selectedItemArr.Remove(Items[i].ToStr());
                }
            }
            //             foreach (var s in selectedItemArr)
            //             {
            //                 int n = lst.Items.Add(s);
            //                 lst.SetSelected(n, true);
            //             }
            lst.Height = this.ItemHeight * (Math.Min(lst.Items.Count, this.MaxDropDownItems));
            var rect = this.RectangleToScreen(this.ClientRectangle);
            fLst.Location = new Point(rect.Left, rect.Bottom );
         
            fLst.Height = lst.Height + lab.Height;
            lst.Show();
            lst.EndUpdate();
            fLst.Show();
            //   this.DroppedDown = true;

            //  this.Parent.Controls.Add(lst);
            fLst.Focus();
        }

        protected override void OnDropDownClosed(EventArgs e)
        {
            if (lst.Focused ||fLst.Focused || lab.Focused)
                return;
            fLst.Hide();
          //  fLst.Hide();
        }
        #endregion
        private void lst_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        private void lst_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            fLst.Hide();
        }
        private void lst_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Down:
                    if (lst.SelectedItems.Count != 0)
                    {
                        this.Text = lst.SelectedItem.ToString();
                    }
                    else
                        this.Text = this.Items[0].ToString();
                    break;
                case Keys.Up:
                    if (lst.SelectedItems.Count != 0)
                    {
                        this.Text = lst.SelectedItem.ToString();
                    }
                    else
                        this.Text = this.Items[0].ToString();
                    break;
            }
        }
    }
}