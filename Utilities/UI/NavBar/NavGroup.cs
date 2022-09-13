using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utilities.UI
{
    public partial class NavGroup : Control
    {
        private Rectangle _titleRectangle;
        public  NavBar OwnerBar;
        private NavBarButton _button;
        private NavBarItemCollection _items;
        public NavBarItemCollection Items
        {
            get { return this._items; }
            set { this._items = value; }
        }
        public NavBarItem this[int index]
        {
            get
            {
                if (index > -1)
                    return this._items[index];
                else
                    return null;
            }
        }
        private NavGroupState _groupState = NavGroupState.expand;
        /// <summary>
        /// 组的状态
        /// </summary>
        public NavGroupState GroupState
        {
            get { return this._groupState; }
            set 
            { 
                this._groupState = value;
                this.SetGroupState(value);
            }
        }
        private int _groupIndex;
        /// <summary>
        /// 组索引
        /// </summary>
        public int GroupIndex
        {
            get { return this._groupIndex; }
            set { this._groupIndex = value; }
        }
        private int _titleHeight = 20;
        /// <summary>
        /// 标题高度
        /// </summary>
        public int TitleHeight
        {
            get { return this._titleHeight; }
            set
            {
                this._titleHeight = value;
                this.SetTitleRectangle();
            }
        }
        private string _title;
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return this._title; }
            set { this._title = value; }
        }
        private Color _titleStartColor = Color.White;
        /// <summary>
        /// 标题渐变开始色
        /// </summary>
        public Color TitleStartColor
        {
            get { return this._titleStartColor; }
            set
            {
                this._titleStartColor = value;
            }
        }
        private Color _titleEndColor = Color.FromArgb(199, 211, 247);
        /// <summary>
        /// 标题渐变结束色
        /// </summary>
        public Color TitleEndColor
        {
            get { return this._titleEndColor; }
            set
            {
                this._titleEndColor = value;
            }
        }
        private int _itemSpace = 5;
        /// <summary>
        /// Item间距
        /// </summary>
        public int ItemSpace
        {
            get { return this._itemSpace; }
            set { this._itemSpace = value; }
        }
        private int _itemMargin = 5;
        /// <summary>
        /// Item边距
        /// </summary>
        public int ItemMargin
        {
            get { return this._itemMargin; }
            set { this._itemMargin = value; }
        }
        private bool _isSelected = false;
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsSelected
        {
            get { return this._isSelected; }
            set 
            { 
                this._isSelected = value;
                this.Invalidate();
            }
        }
        private ImageList _smallImageList;
        public ImageList SmallImageList
        {
            get { return this._smallImageList; }
            set { this._smallImageList = value; }
        }

        public NavGroup()
        {
            this._items = new NavBarItemCollection(this);
            InitializeComponent();
            this._title = "新建组";
            this.BackColor = Color.FromArgb(214,223,247);
            SetTitleRectangle();
            this._button = new NavBarButton(this);
            this._button.Click += new EventHandler
                (
                delegate(object sender, EventArgs e)
                {
                    if (this._groupState == NavGroupState.collapse)
                        this.GroupState = NavGroupState.expand;
                    else
                        this.GroupState = NavGroupState.collapse;
                });
         
        }
        public NavGroup(NavBar ownerbar):this()
        {
            this.OwnerBar = ownerbar;
        }
        /// <summary>
        /// 设置标题区域
        /// </summary>
        private void SetTitleRectangle()
        {
            this._titleRectangle = new Rectangle(0, 0, this.Width, _titleHeight);
        }
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <returns></returns>
        public NavBarItem AddItem()
        {
            this._items.Add();
            return this._items[this._items.Count - 1];
        }
        /// <summary>
        /// 设置组状态
        /// </summary>
        /// <param name="value"></param>
        private void SetGroupState(NavGroupState value)
        {
            if (value == NavGroupState.collapse)
            {
                this.Height = this._titleHeight;
            }
            else
            {
                if (this._items.Count > 0)
                {
                    this.Height = this._items[this._items.Count - 1].Bottom + this._itemSpace;
                }
            }
            this.OwnerBar.SetLayOut();
        }
        /// <summary>
        /// 根据新增项布局
        /// </summary>
        /// <param name="item"></param>
        public void SetLayOut(NavBarItem item)
        {
            this.SuspendLayout();
            this.Controls.Add(item);
            if (this._items.Count == 0)
            {
                item.Top = this._titleHeight + 10;
            }
            else
            {
                item.Top = this[this._items.Count - 1].Bottom + this.ItemSpace;
            }
            item.Width = this.Width - 2 * this.ItemMargin;
            item.Left = (this.Width - item.Width) / 2;
            this.Height = item.Bottom + this.ItemSpace;
            this.OwnerBar.SetLayOut();
            this.ResumeLayout();
        }
        /// <summary>
        /// 重新布局，这个需要完善
        /// </summary>
        /// <param name="index"></param>
        public void SetLayOut(int index)
        {
            for (int i = index + 1; i < this._items.Count; i++)
            {
                this._items[i].Top = this._items[i-1].Bottom + this._itemSpace;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.IsSelected = true;
            this.OwnerBar.SelectedIndex = this._groupIndex;
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (this._button.ClientRectangle.Contains(e.Location))
            {
                this._button.DoClick();
                this.Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            SizeF size = e.Graphics.MeasureString(this._title, this.Font);
            Font titlefont = new Font(this.Font.FontFamily,this.Font.Size, this.Font.Style | FontStyle.Bold);
            //未选中
            if (!this._isSelected)
            {
                LinearGradientBrush brush = new LinearGradientBrush(this._titleRectangle, this._titleStartColor, this._titleEndColor, 0f);
                e.Graphics.FillRectangle(brush, this._titleRectangle);
                e.Graphics.DrawString(this._title, titlefont, Brushes.Black, this._titleRectangle.X, this._titleRectangle.Top + (this._titleRectangle.Height - size.Height) / 2);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(35, 90, 200)), this._titleRectangle);
                e.Graphics.DrawString(this._title, titlefont, Brushes.White, this._titleRectangle.X, this._titleRectangle.Top + (this._titleRectangle.Height - size.Height) / 2);
            }
            e.Graphics.DrawRectangle(Pens.White, new Rectangle(0, 0, this.Width - 1, this.Height - 1));
            //绘制右侧原型按钮
            this._button.Draw(e.Graphics);
            
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SetTitleRectangle();
            this._button.SetClientRectangle(this._titleRectangle);
        }
    }
}
