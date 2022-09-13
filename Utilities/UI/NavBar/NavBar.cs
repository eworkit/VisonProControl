using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace  Utilities.UI
{
    public partial class NavBar : Panel
    {
        private NavGroupCollection _groups;
        public NavGroupCollection Groups
        {
            get { return this._groups; }
        }
        public NavGroup this[int index]
        {
            get
            {
                if (index > -1)
                    return this._groups[index];
                else
                    return null;
            }
        }
        private int _selectedIndex = -1;
        /// <summary>
        /// 选择组的索引
        /// </summary>
        [DefaultValue(-1)]
        public int SelectedIndex
        {
            get { return this._selectedIndex; }
            set 
            { 
                this._selectedIndex = value;
                this.SelectGroup(value);
            }
        }
        private int _groupSpace = 20;
        /// <summary>
        /// Group间距
        /// </summary>
        public int GroupSpace
        {
            get { return this._groupSpace; }
            set { this._groupSpace = value; }
        }
        private int _groupMargin = 5;
        /// <summary>
        /// Group边距
        /// </summary>
        public int GroupMargin
        {
            get { return this._groupMargin; }
            set { this._groupMargin = value; }
        }
        private ImageList _smallImageList;
        /// <summary>
        /// 设置图像列表
        /// </summary>
        public ImageList SmallImageList
        {
            get { return this._smallImageList; }
            set { this._smallImageList = value; }
        }
        public NavBar()
        {
            this.BackColor = Color.FromArgb(112, 140, 225);
            this._groups = new NavGroupCollection(this);
            this.AutoScroll = true;
            InitializeComponent();

            SizeChanged += new EventHandler(NavBar_SizeChanged);
        }

        void NavBar_SizeChanged(object sender, EventArgs e)
        {

            foreach (var g in _groups)
            {
                SetLayOut(g);
                //  if(g.GroupState !=g.GroupState )
                //  g.GroupState = g.GroupState;
                g.IsSelected = g.IsSelected;

            }
        }
        /// <summary>
        /// 根据添加的项，布局
        /// </summary>
        /// <param name="item"></param>
        public void SetLayOut(NavGroup item)
        {
            this.SuspendLayout();
            if (!Controls.Contains(item))
                this.Controls.Add(item);
            if (_groups.IndexOf(item) <= 0)
            {
                item.Top = 10;
            }
            else
            {
                if (item.GroupState == NavGroupState.collapse)
                {
                    item.Height = item.TitleHeight;
                }
                else
                {
                    if (item.Items.Count > 0)
                    {
                        item.Height = item.Items[item.Items.Count - 1].Bottom + item.ItemSpace;
                    }
                }
                item.Top = _groups[_groups.IndexOf(item) - 1].Bottom + this.GroupSpace;
            }
            item.Width = this.Width - 2 * this.GroupMargin;
            item.Left = (this.Width - item.Width) / 2;
            // item.Height = item.TitleHeight;
            this.ResumeLayout();
            //item.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        }
        /// <summary>
        /// 重新布局组件
        /// </summary>
        public void SetLayOut()
        {
            for (int i = 0; i < this._groups.Count; i++)
            {
                if (i == 0)
                    this._groups[i].Top = 10;
                else
                {
                    this._groups[i].Top = this._groups[i - 1].Bottom + this._groupSpace;
                    this._groups[i].GroupIndex = i;
                }
            }
        }
        /// <summary>
        /// 添加分组
        /// </summary>
        /// <returns></returns>
        public NavGroup AddGroup()
        {
            this._groups.Add();
            return this._groups[this._groups.Count - 1];
        }
        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="item"></param>
        public void RemoveGroup(NavGroup item)
        {
            int i = item.GroupIndex;
            this._groups.Remove(item);
            this.Controls.Remove(item);
            this.SetLayOut();
        }
        /// <summary>
        /// 删除指定索引的分组
        /// </summary>
        /// <param name="index"></param>
        public void RemoveGroupAt(int index)
        {
            this.Controls.Remove(this._groups[index]);
            this._groups.RemoveAt(index);            
            this.SetLayOut();
        }
        /// <summary>
        /// 选中指定索引的分组
        /// </summary>
        /// <param name="index"></param>
        private void SelectGroup(int index)
        {
            foreach (NavGroup g in this._groups)
            {
                if (g.GroupIndex == this._selectedIndex) continue;
                g.IsSelected = false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}
