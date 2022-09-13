using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace Utilities.UI
{
    public class GMListBox : GMBarControlBase, IGMControl
    {
        #region constructors

        public GMListBox()
        {
            base.TabStop = true;
        }

        #endregion

        #region IGMControl

        [Browsable(false)]
        public GMControlType ControlType
        {
            get { return GMControlType.ListBox; }
        }

        #endregion

        #region Inner properties

        private WLListBox _innerListBox;

        private WLListBox InnerListBox
        {
            get
            {
                if (_innerListBox == null)
                {
                    _innerListBox = new WLListBox(this);
                }
                return _innerListBox;
            }
        }

        #endregion

        #region override base methods

        #region mouse operation transfer

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            InnerListBox.MouseOperation(e, MouseOperationType.Move);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            base.Focus();
            InnerListBox.MouseOperation(e, MouseOperationType.Down);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            InnerListBox.MouseOperation(e, MouseOperationType.Up);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            InnerListBox.MouseOperation(Point.Empty, MouseOperationType.Leave);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            InnerListBox.MouseOperation(e, MouseOperationType.Wheel);
        }

        #endregion

        #region key operation transfer

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            InnerListBox.KeyOperation(e, KeyOperationType.KeyDown);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            InnerListBox.KeyOperation(e, KeyOperationType.KeyUp);
        }

        #endregion

        #region property chnages

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            InnerListBox.Bounds = base.ClientRectangle;
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            //InnerListBox.Bounds = base.ClientRectangle;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            InnerListBox.Enabled = base.Enabled;
        }

        #endregion

        #region change behaviour

        protected override bool IsInputKey(Keys keyData)
        {
            if ((keyData & Keys.Alt) == Keys.Alt)
            {
                return false;
            }
            switch ((keyData & Keys.KeyCode))
            {
                case Keys.PageUp:
                case Keys.PageDown:
                case Keys.End:
                case Keys.Home:

                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                    return true;
            }
            return base.IsInputKey(keyData);
        }

        #endregion

        #region paint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            InnerListBox.PaintControl(e.Graphics, e.ClipRectangle);
        }

        #endregion

        #endregion

        #region public events

        public event EventHandler SelectedIndexChanged
        {
            add { InnerListBox.SelectedIndexChanged += value; }
            remove { InnerListBox.SelectedIndexChanged -= value; }
        }

        public event EventHandler SelectionChanged
        {
            add { InnerListBox.SelectionChanged += value; }
            remove { InnerListBox.SelectionChanged -= value; }
        }

        public event ListItemClickHandler ItemClick
        {
            add { InnerListBox.ItemClick += value; }
            remove { InnerListBox.ItemClick -= value; }
        }

        public event ListItemClickHandler ItemDoubleClick
        {
            add { InnerListBox.ItemDoubleClick += value; }
            remove { InnerListBox.ItemDoubleClick -= value; }
        }

        public event ListItemPaintHandler DrawItem
        {
            add { InnerListBox.DrawItem += value; }
            remove { InnerListBox.DrawItem -= value; }
        }

        public event MeasureItemEventHandler MeasureItem
        {
            add { InnerListBox.MeasureItem += value; }
            remove { InnerListBox.MeasureItem -= value; }
        }

        #endregion

        #region public methods

        public void SetNewTheme(GMListBoxThemeBase xtheme)
        {
            InnerListBox.SetNewTheme(xtheme);
        }

        public void ScrollToIndex(int index)
        {
            InnerListBox.ScrollToIndex(index);
        }

        public void RefleshTheme()
        {
            InnerListBox.RefleshTheme();
        }

        public int GetBoxHeightForTheseRows(int rows)
        {
            return InnerListBox.GetBoxHeightForTheseRows(rows);
        }

        #endregion

        #region public properties

        /// <summary>
        /// ��ȡһ�����ϣ��ü��ϱ�ʾ�б������е������ͨ���ü�����ӡ�ɾ����
        /// </summary>
        [Browsable(false)]
        public GMEventsCollection Items
        {
            get
            {
                return InnerListBox.Items;
            }
        }

        /// <summary>
        /// ��ȡ��ǰListBox������
        /// </summary>
        [Browsable(false)]
        public GMListBoxThemeBase XTheme
        {
            get
            {
                return InnerListBox.XTheme;
            }
        }

        /// <summary>
        /// ��ȡ�����ô�ֱ����������ʾģʽ
        /// </summary>
        [DefaultValue(typeof(ScrollBarShowMode),"2")]
        public ScrollBarShowMode ScrollBarVShowMode
        {
            get
            {
                return InnerListBox.ScrollBarVShowMode;
            }
            set
            {
                InnerListBox.ScrollBarVShowMode = value;
            }
        }

        /// <summary>
        /// ��ȡ������ˮƽ����������ʾģʽ
        /// </summary>
        [DefaultValue(typeof(ScrollBarShowMode), "0")]
        public ScrollBarShowMode ScrollBarHShowMode
        {
            get
            {
                return InnerListBox.ScrollBarHShowMode;
            }
            set
            {
                InnerListBox.ScrollBarHShowMode = value;
            }
        }

        /// <summary>
        /// ��ȡ�������б���ÿ��ĸ߶�
        /// </summary>
        [DefaultValue(18)]
        public int ItemHeight
        {
            get
            {
                return InnerListBox.ItemHeight;
            }
            set
            {
                InnerListBox.ItemHeight = value;
            }
        }

        /// <summary>
        /// ��ȡ�������б�������ѡ�е�����±�
        /// </summary>
        [Browsable(false)]
        public int SelectedIndex
        {
            get
            {
                return InnerListBox.SelectedIndex;
            }
            set
            {
                InnerListBox.SelectedIndex = value;
            }
        }

        /// <summary>
        /// ��ȡ�������Ƿ�������ʾ�б������������
        /// </summary>
        [DefaultValue(true)]
        public bool TopItemFullyShow
        {
            get
            {
                return InnerListBox.TopItemFullyShow;
            }
            set
            {
                InnerListBox.TopItemFullyShow = value;
            }
        }

        /// <summary>
        /// ��ȡ�������б������ʾ������±�
        /// </summary>
        [Browsable(false)]
        public int TopIndex
        {
            get
            {
                return InnerListBox.TopIndex;
            }
            set
            {
                InnerListBox.TopIndex = value;
            }
        }

        /// <summary>
        /// ��ȡ��ǰѡ�е����������object������ѡ�����򷵻�null
        /// </summary>
        [Browsable(false)]
        public object SelectedValue
        {
            get
            {
                return InnerListBox.SelectedValue;
            }
        }

        /// <summary>
        /// ��ȡ�������б�ѡ��ģʽ����ѡ���������ѡ
        /// </summary>
        [DefaultValue(typeof(ListSelectionMode), "1")]
        public ListSelectionMode ItemSelectionMode
        {
            get
            {
                return InnerListBox.ItemSelectionMode;
            }
            set
            {
                InnerListBox.ItemSelectionMode = value;
            }
        }

        /// <summary>
        /// ��ȡ������һ��ֵ����ֵ��ʾ������б����ƶ�ʱ���Ƿ�������������
        /// </summary>
        [DefaultValue(false)]
        public bool HighLightItemWhenMouseMove
        {
            get
            {
                return InnerListBox.HighLightItemWhenMouseMove;
            }
            set
            {
                InnerListBox.HighLightItemWhenMouseMove = value;
            }
        }

        /// <summary>
        /// ��ȡ�������б���Ļ��Ʒ�ʽ���Զ����ƻ����û����л���
        /// </summary>
        [DefaultValue(typeof(ListDrawMode),"0")]
        public ListDrawMode ItemDrawMode
        {
            get
            {
                return InnerListBox.ItemDrawMode;
            }
            set
            {
                InnerListBox.ItemDrawMode = value;
            }
        }

        /// <summary>
        /// ��ȡ��ǰѡ�е��������Ŀ
        /// </summary>
        [Browsable(false)]
        public int SelectionCount
        {
            get
            {
                return InnerListBox.SelectionCount;
            }
        }

        /// <summary>
        /// ��ȡ���е�ǰѡ������±������
        /// </summary>
        [Browsable(false)]
        public int[] SelectedIndexes
        {
            get
            {
                return InnerListBox.SelectedIndexes;
            }
        }

        /// <summary>
        /// ��ȡ��ʾ��ǰ����ѡ���������
        /// </summary>
        [Browsable(false)]
        public object[] SelectedValues
        {
            get
            {
                return InnerListBox.SelectedValues;
            }
        }

        #endregion

        #region hide base properties



        #endregion
    }
}
