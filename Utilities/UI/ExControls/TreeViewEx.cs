using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Utilities.UI
{/// <summary>
    /// Represents a node with hidden checkbox
    /// </summary>
    public class HiddenCheckBoxTreeNode : TreeNode
    {
        public HiddenCheckBoxTreeNode() { }
        public HiddenCheckBoxTreeNode(string text) : base(text) { }
        public HiddenCheckBoxTreeNode(string text, TreeNode[] children) : base(text, children) { }
        public HiddenCheckBoxTreeNode(string text, int imageIndex, int selectedImageIndex) : base(text, imageIndex, selectedImageIndex) { }
        public HiddenCheckBoxTreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : base(text, imageIndex, selectedImageIndex, children) { }
        protected HiddenCheckBoxTreeNode(SerializationInfo serializationInfo, StreamingContext context) : base(serializationInfo, context) { }
    }

    public class MixedCheckBoxesTreeView : TreeView
    {
        /// <summary>
        /// Specifies the attributes of a node
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TV_ITEM
        {
            public int Mask;
            public IntPtr ItemHandle;
            public int State;
            public int StateMask;
            public IntPtr TextPtr;
            public int TextMax;
            public int Image;
            public int SelectedImage;
            public int Children;
            public IntPtr LParam;
        }

        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;

        public const int TVM_SETITEMA = 0x110d;
        public const int TVM_SETITEM = 0x110d;
        public const int TVM_SETITEMW = 0x113f;

        public const int TVM_GETITEM = 0x110C;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref TV_ITEM lParam);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg != 0x203)
            {
                base.WndProc(ref m);
            }

            // trap TVM_SETITEM message
            if (m.Msg == TVM_SETITEM || m.Msg == TVM_SETITEMA || m.Msg == TVM_SETITEMW)
                // check if CheckBoxes are turned on
            {
                if (CheckBoxes)
                {
                    // get information about the node
                    TV_ITEM tv_item = (TV_ITEM)m.GetLParam(typeof(TV_ITEM));
                    HideCheckBox(tv_item);
                }
            }
            
        }

        protected void HideCheckBox(TV_ITEM tv_item)
        {
            if (tv_item.ItemHandle != IntPtr.Zero)
            {
                // get TreeNode-object, that corresponds to TV_ITEM-object
                TreeNode currentTN = TreeNode.FromHandle(this, tv_item.ItemHandle);

                HiddenCheckBoxTreeNode hiddenCheckBoxTreeNode = currentTN as HiddenCheckBoxTreeNode;
                // check if it's HiddenCheckBoxTreeNode and
                // if its checkbox already has been hidden

                if (hiddenCheckBoxTreeNode != null)
                {
                    HandleRef treeHandleRef = new HandleRef(this, Handle);

                    // check if checkbox already has been hidden
                    TV_ITEM currentTvItem = new TV_ITEM();
                    currentTvItem.ItemHandle = tv_item.ItemHandle;
                    currentTvItem.StateMask = TVIS_STATEIMAGEMASK;
                    currentTvItem.State = 0;

                    IntPtr res = SendMessage(treeHandleRef, TVM_GETITEM, 0, ref currentTvItem);
                    bool needToHide = res.ToInt32() > 0 && currentTvItem.State != 0;

                    if (needToHide)
                    {
                        // specify attributes to update
                        TV_ITEM updatedTvItem = new TV_ITEM();
                        updatedTvItem.ItemHandle = tv_item.ItemHandle;
                        updatedTvItem.Mask = TVIF_STATE;
                        updatedTvItem.StateMask = TVIS_STATEIMAGEMASK;
                        updatedTvItem.State = 0;

                        // send TVM_SETITEM message
                        SendMessage(treeHandleRef, TVM_SETITEM, 0, ref updatedTvItem);
                    }
                }
            }
        }

        protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
        {
            base.OnBeforeCheck(e);

            // prevent checking/unchecking of HiddenCheckBoxTreeNode,
            // otherwise, we will have to repeat checkbox hiding
            if (e.Node is HiddenCheckBoxTreeNode)
                e.Cancel = true;
        }
    }
    public class TreeLE : TreeView
    {

        #region Component Designer generated code

        private System.ComponentModel.Container components = null;

        private void InitializeComponent()
        {
            this.HideSelection = false;
            this.LabelEdit = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion
        /// <summary>
        /// Specifies the attributes of a node
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct TV_ITEM
        {
            public int Mask;
            public IntPtr ItemHandle;
            public int State;
            public int StateMask;
            public IntPtr TextPtr;
            public int TextMax;
            public int Image;
            public int SelectedImage;
            public int Children;
            public IntPtr LParam;
        }

        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;

        public const int TVM_SETITEMA = 0x110d;
        public const int TVM_SETITEM = 0x110d;
        public const int TVM_SETITEMW = 0x113f;

        public const int TVM_GETITEM = 0x110C;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, int msg, int wParam, ref TV_ITEM lParam);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc(ref m);

            // trap TVM_SETITEM message
            if (m.Msg == TVM_SETITEM || m.Msg == TVM_SETITEMA || m.Msg == TVM_SETITEMW)
                // check if CheckBoxes are turned on
                if (CheckBoxes)
                {
                    // get information about the node
                    TV_ITEM tv_item = (TV_ITEM)m.GetLParam(typeof(TV_ITEM));
                    if (tv_item.StateMask != TVIS_STATEIMAGEMASK)
                        HideCheckBox(tv_item);
                }
        }

        protected void HideCheckBox(TV_ITEM tv_item)
        {
            if (tv_item.ItemHandle != IntPtr.Zero)
            {
                // get TreeNode-object, that corresponds to TV_ITEM-object
                TreeNode currentTN = TreeNode.FromHandle(this, tv_item.ItemHandle);

                HiddenCheckBoxTreeNode hiddenCheckBoxTreeNode = currentTN as HiddenCheckBoxTreeNode;
                // check if it's HiddenCheckBoxTreeNode and
                // if its checkbox already has been hidden

                if (hiddenCheckBoxTreeNode != null)
                {
                    HandleRef treeHandleRef = new HandleRef(this, Handle);

                    // check if checkbox already has been hidden
                    TV_ITEM currentTvItem = new TV_ITEM();
                    currentTvItem.ItemHandle = tv_item.ItemHandle;
                    currentTvItem.StateMask = TVIS_STATEIMAGEMASK;
                    currentTvItem.State = 0;

                    IntPtr res = SendMessage(treeHandleRef, TVM_GETITEM, 0, ref currentTvItem);
                    bool needToHide = res.ToInt32() > 0 && currentTvItem.State != 0;

                    if (needToHide)
                    {
                        // specify attributes to update
                        TV_ITEM updatedTvItem = new TV_ITEM();
                        updatedTvItem.ItemHandle = tv_item.ItemHandle;
                        updatedTvItem.Mask = TVIF_STATE;
                        updatedTvItem.StateMask = TVIS_STATEIMAGEMASK;
                        updatedTvItem.State = 0;

                        // send TVM_SETITEM message
                        SendMessage(treeHandleRef, TVM_SETITEM, 0, ref updatedTvItem);
                    }
                }
            }
        }

        protected override void OnBeforeCheck(TreeViewCancelEventArgs e)
        {
            base.OnBeforeCheck(e);

            // prevent checking/unchecking of HiddenCheckBoxTreeNode,
            // otherwise, we will have to repeat checkbox hiding
            if (e.Node is HiddenCheckBoxTreeNode)
                e.Cancel = true;
        }

        public TreeLE()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
             if (e.Button == MouseButtons.Right)
             {
                TreeNode tn = this.GetNodeAt(e.X, e.Y);
//                 if (tn != null)
//                     this.SelectedNode = tn;
             
            }
             else if (e.Button == System.Windows.Forms.MouseButtons.Left)
             {
                 TreeNode tn = this.GetNodeAt(e.X, e.Y);
                 if (tn != null)
                     this.SelectedNode = tn;
             }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            TreeNode tn;
            if (e.Button == MouseButtons.Left)
            {
                tn = editNode;
                if(tn==null)
                     tn = this.SelectedNode;
                if (tn == this.GetNodeAt(e.X, e.Y))
                {
                    if (wasDoubleClick)
                        wasDoubleClick = false;
                    else
                    {
                        TriggerLabelEdit = true;
                    }
                }
            }
            base.OnMouseUp(e);
        }

        private const int WM_TIMER = 0x0113;
        private bool TriggerLabelEdit = false;
        private string viewedLabel;
        private string editedLabel;

        protected override void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
        {
            // put node label to initial state
            // to ensure that in case of label editing cancelled
            // the initial state of label is preserved
            if (editNode == null)
                this.SelectedNode.Text = viewedLabel;
            else editNode.Text = viewedLabel;
            // base.OnBeforeLabelEdit is not called here
            // it is called only from StartLabelEdit
        }

        protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
        {
            this.LabelEdit = false;
            e.CancelEdit = true;
            if (e.Label == null)
            {
               // base.OnAfterLabelEdit(e);
                return;
            }
            ValidateLabelEditEventArgs ea = new ValidateLabelEditEventArgs(e.Label);
            OnValidateLabelEdit(ea);
            if (ea.Cancel == true)
            {
                e.Node.Text = editedLabel;
                this.LabelEdit = true;
                e.Node.BeginEdit();
            }
            else
            {
                base.OnAfterLabelEdit(e);
               // e.Node.Text = e.Label;
            }
        }

        private TreeNode editNode;
        public void BeginEdit(TreeNode tn=null)
        {
            editNode = tn;
            StartLabelEdit();
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (TriggerLabelEdit)
                if (m.Msg == WM_TIMER)
                {
                    TriggerLabelEdit = false;
                    if (this.LabelEdit)
                        StartLabelEdit();
                }
            base.OnNotifyMessage(m);
        }

        public void StartLabelEdit()
        {
            TreeNode tn = editNode;
            if(tn==null)
                tn=this.SelectedNode; 
            viewedLabel = tn.Text;

            NodeLabelEditEventArgs e = new NodeLabelEditEventArgs(tn);
            base.OnBeforeLabelEdit(e);
            if (e.CancelEdit)
                return;
            editedLabel = tn.Text;

            this.LabelEdit = true;
            tn.BeginEdit();
            editNode = null;
        }


        protected override void OnClick(EventArgs e)
        {
            TriggerLabelEdit = false;
            base.OnClick(e);
        }

        private bool wasDoubleClick = false;
        protected override void OnDoubleClick(EventArgs e)
        {
            wasDoubleClick = true;
            base.OnDoubleClick(e);
        }

        public event ValidateLabelEditEventHandler ValidateLabelEdit;

        protected virtual void OnValidateLabelEdit(ValidateLabelEditEventArgs e)
        {
            if(ValidateLabelEdit!=null)
                ValidateLabelEdit(this, e);
        }
        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    if (!LabelEdit)
        //        return;
        //    if (this.SelectedNode != null)
        //        switch (e.KeyCode)
        //        {
        //            case Keys.Enter:
        //                this.BeginEdit();
        //                break;
        //            case Keys.F2:
        //                this.BeginEdit();
        //                break;
        //            case Keys.Space:
        //                this.SelectedNode.Toggle();
        //                break;
        //            default:
        //                break;
        //        }
        //}
        public delegate void ValidateLabelEditEventHandler(object sender, ValidateLabelEditEventArgs e);

    }

    public class ValidateLabelEditEventArgs : System.ComponentModel.CancelEventArgs
    {

        public ValidateLabelEditEventArgs(string label)
        {
            this.label = label;
            this.Cancel = false;
        }

        private string label;
        public string Label
        {
            get { return label; }
            set { label = value; }
        }

    }
    public  class TreeViewEx:TreeLE
    {
        private System.ComponentModel.IContainer components;
        public Func<TreeNode,TreeNode, bool> MovePredicate;
        public event Action<TreeNode, TreeNode> AfterDragDrop;
        private bool draged = false;
        private Action MenuStripClosed;
        public TreeViewEx()
        {
            ContextMenuStrip = new ContextMenuStrip();
            ContextMenuStrip.Closed += MenuStrip_Closed;
            ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            ContextMenuStrip.ItemClicked += ContextMenuStrip_ItemClicked;
//             base.SetStyle(
//               ControlStyles.UserPaint  |                 // 控件将自行绘制，而不是通过操作系统来绘制
//               ControlStyles.OptimizedDoubleBuffer         // 该控件首先在缓冲区中绘制，而不是直接绘制到屏幕上，这样可以减少闪烁
//               | ControlStyles.AllPaintingInWmPaint      // 控件将忽略 WM_ERASEBKGND 窗口消息以减少闪烁
//               | ControlStyles.ResizeRedraw                    // 在调整控件大小时重绘控件
//               | ControlStyles.SupportsTransparentBackColor,    // 控件接受 alpha 组件小于 255 的 BackColor 以模拟透明
//               true);                                         // 设置以上值为 true
//             base.UpdateStyles();
            this.Font = new Font("Microsoft YaHei",this.Font.Size);          
        }

        private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
       {
           
       }
        

        private TreeNode lastRightClicedNode;

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
       {
           if (ContextMenuStrip.Items.Count > 0)
           {
               var tt = ((System.Windows.Forms.ContextMenuStrip)(sender)).SourceControl;
               //var hitTest = this.HitTest(this.PointToClient(new Point(ContextMenuStrip.Left, ContextMenuStrip.Top)));
//                if (hitTest.Node != null)
//                {
//                    AddCustomDraw(hitTest.Node);
//                } 
               if (lastRightClicedNode != null)
                   AddCustomDraw(lastRightClicedNode);
           }
           else
           {
               e.Cancel = true;
           }
       }

        private void MenuStrip_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (MenuStripClosed != null) MenuStripClosed();
            ContextMenuStrip.Items.Clear();
        }
       protected override void OnMouseDown(MouseEventArgs e)
       {
           draged = false;
             if (e.Button == System.Windows.Forms.MouseButtons.Right)
             {
                 var node = this.GetNodeAt(e.Location);
                 if (node == null)
                 {
                     RemoveCustomDraw(lastRightClicedNode);
                     lastRightClicedNode = null;
                 }
             }
           base.OnMouseDown(e);
       }
       protected override void OnMouseUp(MouseEventArgs e)
       {            
           if (e.Button == System.Windows.Forms.MouseButtons.Right)
           {
               var node = this.GetNodeAt(e.Location);
               if (node!= null)
               {
                   MenuStripClosed = () =>
                   {
                       RemoveCustomDraw(node);
                   };
               }
           }
           base.OnMouseUp(e);
       }
       protected override void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
       {
           if (e.Button == System.Windows.Forms.MouseButtons.Right)
           {
               lastRightClicedNode = e.Node;
           }
           base.OnNodeMouseClick(e);

           if (e.Button == MouseButtons.Left)
           {
               if (AllowDrop)
               {
                   if (DragHelper.ImageList_BeginDrag(this.ImageList.Handle, 0, 0, 0))
                   {
                       this.DoDragDrop(e.Node, DragDropEffects.Copy | DragDropEffects.Move);
                       DragHelper.ImageList_EndDrag();
                   }
               }
           } 
           return;


           if (e.Button == System.Windows.Forms.MouseButtons.Right)
           {
               lastRightClicedNode = e.Node;
           }
           base.OnNodeMouseClick(e);

           if (e.Button == MouseButtons.Left)
           {
               if (AllowDrop)
               {
                   if (DragHelper.ImageList_BeginDrag(this.ImageList.Handle, 0, 0, 0))
                   {
                       this.DoDragDrop(e.Node, DragDropEffects.Copy | DragDropEffects.Move);
                       DragHelper.ImageList_EndDrag();
                   }
               }
           }
           else if (e.Button == System.Windows.Forms.MouseButtons.Right)
           {
               MenuStripClosed = () =>
               {
                   RemoveCustomDraw(e.Node);

               };
           }
       }
       protected override void OnItemDrag(ItemDragEventArgs e)
       {
           base.OnItemDrag(e);
           if (!this.AllowDrop)
               return;
           if (MovePredicate != null)
           {
//                Point p = new Point(e..X, e.Y);           
//                TreeNode DropNode = this.GetNodeAt(     PointToClient(p);
//                if (!MovePredicate((TreeNode)e.Item, DropNode))
//                    return;
           }
           if (DragHelper.ImageList_BeginDrag(this.ImageList.Handle, ((TreeNode)(e.Item)).ImageIndex, 0, 0))
           {
               this.DoDragDrop(e.Item, DragDropEffects.Copy | DragDropEffects.Move);
               DragHelper.ImageList_EndDrag();
           }
       }
       protected override void OnDragEnter(DragEventArgs drgevent)
       {
           if (!this.AllowDrop)
               return;
           if(MovePredicate==null)
           {
               if (drgevent.Data.GetDataPresent(typeof(TreeNode)))
                   drgevent.Effect = DragDropEffects.Move;
               else
                   drgevent.Effect = DragDropEffects.None;
           }
           else
           {
               Point p = new Point(drgevent.X, drgevent.Y);
               p = PointToClient(p);
               TreeNode node0 = (TreeNode)drgevent.Data.GetData(typeof(TreeNode)) ??
                                (TreeNode)drgevent.Data.GetData(typeof(HiddenCheckBoxTreeNode));
               var dropNode = this.GetNodeAt(p);
               if (MovePredicate(node0,dropNode))
                   drgevent.Effect = DragDropEffects.Move;
               else drgevent.Effect = DragDropEffects.None;
           }
           DragHelper.ImageList_DragEnter(this.Handle, 0, 0);
       }
       protected override void OnDragDrop(DragEventArgs drgevent)
       {
           base.OnDragDrop(drgevent);
           if (!this.AllowDrop)
               return;
           DragHelper.ImageList_EndDrag();
           if (AfterDragDrop != null && draged)
           {
               Point p = new Point(drgevent.X, drgevent.Y);
               p = PointToClient(p);
               TreeNode node0=(TreeNode)drgevent.Data.GetData(typeof(TreeNode));
               if(node0 ==null)
                   node0 = (TreeNode)drgevent.Data.GetData(typeof(HiddenCheckBoxTreeNode));
               TreeNode DropNode = this.GetNodeAt(p);
              // if(node0!=DropNode)
            
                   AfterDragDrop(node0, DropNode);
           }
           draged = false;
           Refresh();
       }
       protected override void OnDragLeave(EventArgs e)
       {
           base.OnDragLeave(e);
           if (!this.AllowDrop)
               return;
           DragHelper.ImageList_DragLeave(this.Handle);
       }
       protected override void OnDragOver(DragEventArgs e)
       {
           base.OnDragOver(e);
           if (!this.AllowDrop)
               return;
           try
           {
               draged = true;
               Point pt = this.PointToClient(new Point(e.X, e.Y));
               DragHelper.ImageList_DragMove(pt.X, pt.Y); 
               int scrollRegion = 20;
               // See if we need to scroll up or down
               if ((pt.Y + scrollRegion) > this.Height)
               {
                   // Call the API to scroll down
                  Win32Api.SendMessage(Handle, (uint)277, (IntPtr)1, IntPtr.Zero);
               }
               else if (pt.Y < (Top + scrollRegion))
               {
                   // Call thje API to scroll up
                   Win32Api.SendMessage(Handle, (uint)277, IntPtr.Zero, IntPtr.Zero);
                   Refresh();
               } 
           }
           catch (Exception ex)
           {
             //  MessageBox.Show("ListView_DragOver()!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       } 
       public List<TreeNode> tnCustomDrawLst = new List<TreeNode>();
       public void AddCustomDraw(TreeNode tn)
       {
           if (tn == null)
               return;
           if (!tnCustomDrawLst.Contains(tn))
           {
               tnCustomDrawLst.Add(tn);
               Invalidate();
           }
       }
       public void RemoveCustomDraw(TreeNode tn)
       {
           if (tnCustomDrawLst.Remove(tn))
               Invalidate();
       }
        public void ClearCustomDraw()
       {
           tnCustomDrawLst.Clear();
           Invalidate();
       }
       protected override void OnDrawNode(DrawTreeNodeEventArgs e)
       {
        //   e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显
          
        //   return;

          // if ((e.State & TreeNodeStates.Selected) != 0) 
           if (e.Node.IsEditing)
           {
               base.OnDrawNode(e);
               return;
           }
          
             if(tnCustomDrawLst.Contains(e.Node))
           {
               e.Graphics.FillRectangle(Brushes.DarkSlateGray, e.Node.Bounds);
               Font nodeFont = e.Node.NodeFont;
               if (nodeFont == null)
                   nodeFont = this.Font;
               //int top = (int)((e.Bounds.Height - e.Graphics.MeasureString(e.Node.Text, nodeFont).Height) / 2);
               e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Azure, Rectangle.Inflate(e.Bounds, 0, 0));
           }
             else if (SelectedNode == e.Node)
             {
                 //演示为绿底白字
                 e.Graphics.FillRectangle(Brushes.MediumBlue, e.Node.Bounds);

                 Font nodeFont = e.Node.NodeFont;
                 if (nodeFont == null)
                     nodeFont = this.Font;
                 //int top = (int)((e.Bounds.Height - e.Graphics.MeasureString(e.Node.Text, nodeFont).Height) / 2);
                 e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 0, 0));
             }
           else
           {
//                e.Graphics.FillRectangle(Brushes.White, e.Node.Bounds);
//                Font nodeFont = e.Node.NodeFont;
//                if (nodeFont == null)
//                    nodeFont = this.Font;
//                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.Black, Rectangle.Inflate(e.Bounds, 2, 0));
      
               e.DrawDefault = true;
           }

           if ((e.State & TreeNodeStates.Focused) != 0)
           {
               using (Pen focusPen = new Pen(Color.MediumBlue))
               {
                   focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                   Rectangle focusBounds = e.Node.Bounds;
                   focusBounds.Size = new Size(focusBounds.Width - 1,
                   focusBounds.Height - 1);
                   e.Graphics.DrawRectangle(focusPen, focusBounds);
               }
           }
           base.OnDrawNode(e);
       }

       private void InitializeComponent()
       {
            this.SuspendLayout();
            this.ResumeLayout(false);

       }
    }
}
