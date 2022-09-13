/*
 * 本代码受中华人民共和国著作权法保护，作者仅授权下载代码之人在学习和交流范围内
 * 自由使用与修改代码；欲将代码用于商业用途的，请先与作者联系。
 * 使用本代码请保留此处信息。作者联系方式：ping3108@163.com, 欢迎进行技术交流
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Utilities.UI
{
    public abstract class WLButtonBase : WLControlBase
    {
        #region constructors

        public WLButtonBase(Control owner)
            : base(owner)
        {
        }

        #endregion

        #region public properties

        private bool _capture = false;
        private GMButtonState _state = GMButtonState.Normal;
        private string _text;

        /// <summary>
        /// 获取控件是否捕获了鼠标
        /// </summary>
        public bool Capture
        {
            get { return _capture; }
        }

        /// <summary>
        /// 获取或设置按钮的状态
        /// </summary>
        public GMButtonState State
        {
            get { return _state; }
            set
            {
                if (value != _state)
                {
                    _state = value;
                    Invalidate(Bounds);
                }
            }
        }

        /// <summary>
        /// 获取或设置控件的文本
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    Invalidate(Bounds);
                }
            }
        }

        #endregion

        #region new protected methods

        protected virtual void OnClick(EventArgs e)
        {

        }

        #endregion

        #region override methods

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Bounds.Contains(e.Location))
            {
                State = GMButtonState.Pressed;
                _capture = true;
            }
            else
            {
                _capture = false;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (Bounds.Contains(e.Location))
            {
                if (State == GMButtonState.Normal)
                {
                    // 没有在窗体其他地方按下按钮
                    if (!Owner.Capture)
                    {
                        State = GMButtonState.Hover;
                    }
                }
                else if (State == GMButtonState.PressLeave)
                {
                    State = GMButtonState.Pressed;
                }

            }
            else
            {
                if (_capture)
                {
                    State = GMButtonState.PressLeave;
                }
                else
                {
                    State = GMButtonState.Normal;
                }
            }
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            State = GMButtonState.Normal;
            _capture = false;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (Bounds.Contains(e.Location))
            {
                State = GMButtonState.Hover;
                if (_capture)
                    OnClick(EventArgs.Empty);
            }
            else
            {
                State = GMButtonState.Normal;
            }
            _capture = false;
        }

        #endregion
    }
}
