using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Utilities.UI
{
    public partial class NavBarItem : Control
    {
        /// <summary>
        /// 供外部调用项的点击事件
        /// </summary>
        [Browsable(false)]
        public event EventHandler ItemClick;

        private Rectangle _imageRectangle;
        private PictureBox _picbox;
        private Label _titlebox;

        private NavGroup _ownerGroup;
        /// <summary>
        /// 所属组
        /// </summary>
        public NavGroup OwnerGroup
        {
            get { return this._ownerGroup; }
            set { this._ownerGroup = value; }
        }
        public NavBar OwnerBar
        {
            get { return this._ownerGroup.OwnerBar; }
        }
        private int _itemIndex;
        public int ItemIndex
        {
            get { return this._itemIndex; }
            set { this._itemIndex = value; }
        }
        private int _imageIndex = -1;
        /// <summary>
        /// 图标的索引
        /// </summary>
        public int ImageIndex
        {
            get { return this._imageIndex; }
            set
            {
                if (this._ownerGroup.SmallImageList == null) return;
                if (value >= this._ownerGroup.SmallImageList.Images.Count) return;
                this._imageIndex = value;
                this._picbox.Image = this._ownerGroup.SmallImageList.Images[this._imageIndex];
                this.Invalidate();
            }
        }
        private new string _text;
        /// <summary>
        /// 设置显示文字
        /// </summary>
        public new string Text
        {
            get { return this._text; }
            set
            {
                this._text = value;
                this._titlebox.Text = value;
                this.Invalidate();
            }
        }
        public NavBarItem()
        {
            InitializeComponent();
            this.AutoSize = false;
            this.Height = 22;
            this.Cursor = Cursors.Hand;
            this._titlebox = new Label();
            this._titlebox.Parent = this;
            this._titlebox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            this._titlebox.TextAlign = ContentAlignment.MiddleLeft;
            this._titlebox.MouseHover += new EventHandler(this.OnTitleMouseHover);
            this._titlebox.MouseLeave += new EventHandler(this.OnTitleMouseLeave);
            this._titlebox.Click += new EventHandler(this.OnTitleClick);
            this._picbox = new PictureBox();
            this._picbox.Parent = this;
             this._picbox.Anchor = AnchorStyles.Left|AnchorStyles.Top;
            this._picbox.Size =new Size(22, 22);
            this._picbox.Left = 3;
            this._picbox.Top = 3;
            this._picbox.SizeMode = PictureBoxSizeMode.CenterImage;
            this.Text = "新建项目";
            this.ControlAdded += new ControlEventHandler(NavBarItem_ControlAdded);
            this.ControlRemoved += new ControlEventHandler(NavBarItem_ControlRemoved);    
        }

        void NavBarItem_ControlRemoved(object sender, ControlEventArgs e)
        {
            NavBarItem_ControlAdded(sender, e);
        }

        void NavBarItem_ControlAdded(object sender, ControlEventArgs e)
        {
            int h = 22;
            foreach (Control c in Controls)
            {
                int h1=c.Bottom+c.Margin.Bottom;
                if (h1 > h)
                    h = h1;
            }
            this.Height = h;
            this.OwnerGroup.GroupState = this.OwnerGroup.GroupState;
          
          
        }

        public NavBarItem(NavGroup ownergroup)
            : this()
        {
            this._ownerGroup = ownergroup;
        }

        private void OnTitleClick(object sender, EventArgs e)
        {
            base.OnClick(e);
            if (this.ItemClick != null)
            {
                this.ItemClick(this, new EventArgs());
            }
        }
        private void OnTitleMouseHover(object sender,EventArgs e)
        {
            Control c = (Control)sender;
            c.ForeColor = Color.Blue;
            c.Font = new Font(c.Font.FontFamily, c.Font.Size, c.Font.Style | FontStyle.Underline);
        }
        private void OnTitleMouseLeave(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            c.ForeColor = SystemColors.ControlText;
            c.Font = new Font(c.Font.FontFamily, c.Font.Size, FontStyle.Regular);
        }

     
    }
}
