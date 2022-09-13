/*
 * 本代码受中华人民共和国著作权法保护，作者仅授权下载代码之人在学习和交流范围内
 * 自由使用与修改代码；欲将代码用于商业用途的，请先与作者联系。
 * 使用本代码请保留此处信息。作者联系方式：ping3108@163.com, 欢迎进行技术交流
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Utilities.UI
{
    public abstract class WLContainerBase : WLControlBase
    {
        #region constructors

        public WLContainerBase(Control owner)
            : base(owner)
        {
        }

        #endregion

        #region public proterties

        private GMEventsCollection _wlControls;

        /// <summary>
        /// 获取子控件集合
        /// </summary>
        public GMEventsCollection WLControls
        {
            get
            {
                if (_wlControls == null)
                {
                    _wlControls = new GMEventsCollection();
                    _wlControls.CollectionChange += new GMCollectionChangeHandler(WLControls_CollectionChange);
                }
                return _wlControls;
            }
        }        

        #endregion

        #region private vars

        void WLControls_CollectionChange(GMCollectionChangeArgs e)
        {
            if (e.Action == GMCollectionChangeAction.AfterInsert)
            {
                WLControlBase ctl = e.Value as WLControlBase;
                if (ctl != null)
                    listChildControls.Add(ctl);
            }
            else if (e.Action == GMCollectionChangeAction.AfterRemove)
            {
                WLControlBase ctl = e.Value as WLControlBase;
                if (ctl != null)
                    listChildControls.Remove(ctl);
            }
            else if (e.Action == GMCollectionChangeAction.AfterClear)
                listChildControls.Clear();
        }

        /// <summary>
        /// 表示捕获了鼠标焦点的控件，即鼠标在这个控件上点击且未松开
        /// </summary>
        private WLControlBase capturedControl;

        /// <summary>
        /// 表示最近一次鼠标在上面正常移动（按钮未按下）的控件
        /// </summary>
        private WLControlBase lastMouseMoveControl;

        private bool isMouseDown = false;

        private List<WLControlBase> listChildControls = new List<WLControlBase>();

        #endregion

        #region mouse operation override

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (isMouseDown)
            {
                if (capturedControl != null)
                    capturedControl.MouseOperation(e, MouseOperationType.Move);
            }
            else
            {
                for (int i = listChildControls.Count - 1; i >= 0; i--)
                {
                    WLControlBase ctl = listChildControls[i];

                    if (!ctl.Visible)
                        continue;

                    if (ctl.Bounds.Contains(e.Location))
                    {                        
                        if (lastMouseMoveControl != null && lastMouseMoveControl != ctl)
                            lastMouseMoveControl.MouseOperation(Point.Empty, MouseOperationType.Leave);
                        ctl.MouseOperation(e, MouseOperationType.Move);
                        lastMouseMoveControl = ctl;
                        return;
                    }
                }
                if(lastMouseMoveControl!=null)
                    lastMouseMoveControl.MouseOperation(Point.Empty, MouseOperationType.Leave);
                lastMouseMoveControl = null;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            isMouseDown = base.Bounds.Contains(e.Location);

            for (int i = listChildControls.Count - 1; i >= 0; i--)
            {
                WLControlBase ctl = listChildControls[i];
                if (!ctl.Visible)
                    continue;
                if (ctl.Bounds.Contains(e.Location))
                {
                    if (ctl.Enabled)
                    {
                        capturedControl = ctl;
                        ctl.MouseOperation(e, MouseOperationType.Down);
                    }
                    break;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (capturedControl != null)
                capturedControl.MouseOperation(e, MouseOperationType.Up);
            capturedControl = null;
            isMouseDown = false;
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            if (lastMouseMoveControl != null)
                lastMouseMoveControl.MouseOperation(Point.Empty, MouseOperationType.Leave);
            lastMouseMoveControl = null;
            isMouseDown = false;
        }

        #endregion

        #region paint override

        protected override void OnPaintContent(Graphics g, Rectangle clipRect)
        {
            base.OnPaintContent(g, clipRect);
            for (int i = 0; i < listChildControls.Count; i++)
            {
                WLControlBase ctl = listChildControls[i];
                if (!ctl.Visible)
                    continue;
                if(clipRect.IntersectsWith(ctl.Bounds))
                    ctl.PaintControl(g,clipRect);
            }
        }

        #endregion
    }
}
