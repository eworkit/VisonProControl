using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

using Utilities.UI.MyGraphics;

namespace Utilities.UI
{
    [ToolboxItem(false), DefaultEvent("ValueChanged")]
    public abstract class GMScrollBarBase: GMBarControlBase, IGMControl
    {
        #region 类内部变量, 属性

        private WLScrollBar _innerScrollBar;

        private WLScrollBar InnerScrollBar
        {
            get
            {
                if (_innerScrollBar == null)
                {
                    _innerScrollBar = new WLScrollBar(this, BarOrientation);
                }
                return _innerScrollBar;
            }
        }

        #endregion

        #region 子类要重写的属性

        protected abstract Orientation BarOrientation { get; }

        #endregion

        #region IGMControl接口实现

        [Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
        public GMControlType ControlType
        {
            get { return GMControlType.ScrollBar; }
        }

        #endregion

        #region 控件事件

        public event EventHandler ValueChanged
        {
            add
            {
                InnerScrollBar.ValueChanged += value;
            }
            remove
            {
                InnerScrollBar.ValueChanged -= value;
            }
        }

        #endregion       

        #region 公开属性

        [Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
        public GMScrollBarThemeBase XTheme
        {
            get
            {                
                return InnerScrollBar.XTheme;
            }
        }

        [DefaultValue(0)]
        public int Value
        {
            get 
            {
                return InnerScrollBar.Value;
            }
            set
            {
                InnerScrollBar.Value = value;
            }
        }

        [DefaultValue(0), RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        public int Minimum
        {
            get
            {
                return InnerScrollBar.Minimum;
            }
            set
            {
                InnerScrollBar.Minimum = value;
            }
        }

        [DefaultValue(100), RefreshProperties(System.ComponentModel.RefreshProperties.Repaint)]
        public int Maximum
        {
            get
            {
                return InnerScrollBar.Maximum;
            }
            set
            {
                InnerScrollBar.Maximum = value;
            }
        }

        public int SmallChange
        {
            get
            {
                return InnerScrollBar.SmallChange;
            }
            set
            {
                InnerScrollBar.SmallChange = value;
            }
        }

        public int LargeChange
        {
            get
            {
                return InnerScrollBar.LargeChange;
            }
            set
            {
                InnerScrollBar.LargeChange = value;
            }
        }

        public int MiddleButtonLengthPercentage
        {
            get
            {
                return InnerScrollBar.MiddleButtonLengthPercentage;
            }
            set
            {
                InnerScrollBar.MiddleButtonLengthPercentage = value;
            }
        }

        #endregion

        #region 公开事件

        public void SetNewTheme(GMScrollBarThemeBase xtheme)
        {
            InnerScrollBar.SetNewTheme(xtheme);
        }

        #endregion

        #region 构造函数及初始化

        public GMScrollBarBase()
        {

        }

        #endregion

        #region 重写的方法

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            InnerScrollBar.Bounds = ClientRectangle;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            InnerScrollBar.PaintControl(e.Graphics, e.ClipRectangle);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            InnerScrollBar.MouseOperation(e, MouseOperationType.Move);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            InnerScrollBar.MouseOperation(e, MouseOperationType.Down);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            InnerScrollBar.MouseOperation(e, MouseOperationType.Up);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            InnerScrollBar.MouseOperation(Point.Empty, MouseOperationType.Leave);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            InnerScrollBar.Enabled = base.Enabled;
        }

        #endregion
    }   
}
