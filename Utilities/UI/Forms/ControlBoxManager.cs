using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

using Utilities.UI.MyGraphics;

namespace Utilities.UI
{
    public class ControlBoxManager
    {
        private GMForm _owner;

        private WLButton closeBtn;
        private WLButton maxBtn;
        private WLButton resBtn;
        private WLButton minBtn;
        private WLButton optionBtn;
        public event EventHandler OnOptionboxClick;
        #region event handler

        private void CloseBtnClick(object sender, EventArgs e)
        {
            _owner.Close();
        }

        private void MaxBtnClick(object sender, EventArgs e)
        {
            _owner.WindowState = FormWindowState.Maximized;
        }

        private void ResBtnClick(object sender, EventArgs e)
        {
            _owner.WindowState = FormWindowState.Normal;
        }

        private void MinBtnClick(object sender, EventArgs e)
        {
            _owner.WindowState = FormWindowState.Minimized;
        }

        #endregion

        private void SetControlBoxColor()
        {

            closeBtn.XTheme.ColorTable = _owner.XTheme.CloseBoxColor;
            closeBtn.XTheme.RoundedRadius = 0;
            closeBtn.BackImageNormal = _owner.XTheme.CloseBoxBackImageNormal;
            closeBtn.BackImageHover = _owner.XTheme.CloseBoxBackImageHover;
            closeBtn.BackImagePressed = _owner.XTheme.CloseBoxBackImagePressed;

            // max res box
            if (_owner.MaximizeBox)
            {
                maxBtn.XTheme.ColorTable = _owner.XTheme.MaxBoxColor;
                maxBtn.XTheme.RoundedRadius = 0;
                maxBtn.BackImageNormal = _owner.XTheme.MaxBoxBackImageNormal;
                maxBtn.BackImageHover = _owner.XTheme.MaxBoxBackImageHover;
                maxBtn.BackImagePressed = _owner.XTheme.MaxBoxBackImagePressed;

                resBtn.XTheme.ColorTable = _owner.XTheme.MaxBoxColor;
                resBtn.XTheme.RoundedRadius = 0;
                resBtn.BackImageNormal = _owner.XTheme.ResBoxBackImageNormal;
                resBtn.BackImageHover = _owner.XTheme.ResBoxBackImageHover;
                resBtn.BackImagePressed = _owner.XTheme.ResBoxBackImagePressed;
            }

            // min box
            if (_owner.MinimizeBox)
            {
                minBtn.XTheme.ColorTable = _owner.XTheme.MinBoxColor;
                minBtn.XTheme.RoundedRadius = 0;
                minBtn.BackImageNormal = _owner.XTheme.MinBoxBackImageNormal;
                minBtn.BackImageHover = _owner.XTheme.MinBoxBackImageHover;
                minBtn.BackImagePressed = _owner.XTheme.MinBoxBackImagePressed;
            }
            if (_owner.OptionBox)
            {
                optionBtn.XTheme.ColorTable = _owner.XTheme.OptionBoxColor;
                optionBtn.XTheme.RoundedRadius = 0;
                optionBtn.BackImageNormal = _owner.XTheme.OptionBoxBackImageNormal;
                optionBtn.BackImageHover = _owner.XTheme.OptionBoxBackImageHover;
                optionBtn.BackImagePressed = _owner.XTheme.OptionBoxBackImagePressed;
            }

        }

        private void BtnInit()
        {
            // close box
            closeBtn = new WLButton(_owner);
            closeBtn.Visible = true;
            closeBtn.Bounds = _owner.CloseBoxRect;

            closeBtn.Click += new EventHandler(CloseBtnClick);
            closeBtn.ForePathGetter = new ButtonForePathGetter(GraphicsPathHelper.CreateCloseFlagPath);
            maxBtn = new WLButton(_owner);
            resBtn = new WLButton(_owner);
            minBtn = new WLButton(_owner);
            optionBtn = new WLButton(_owner);
            // max res box
          //  if (_owner.MaximizeBox)
            {
//                 maxBtn = new WLButton(_owner);
//                 resBtn = new WLButton(_owner);

                if (_owner.WindowState == FormWindowState.Normal)
                {
                    maxBtn.Visible = true;
                    resBtn.Visible = false;
                }
                else
                {
                    maxBtn.Visible = false;
                    resBtn.Visible = true;
                }

                maxBtn.Bounds = _owner.MaxBoxRect;
                resBtn.Bounds = _owner.MaxBoxRect;

                maxBtn.Click += new EventHandler(MaxBtnClick);
                maxBtn.ForePathGetter = new ButtonForePathGetter(GraphicsPathHelper.CreateMaximizeFlagPath);

                resBtn.Click += new EventHandler(ResBtnClick);
                resBtn.ForePathGetter = new ButtonForePathGetter(GraphicsPathHelper.CreateRestoreFlagPath);
            }

            // min box
      //      if (_owner.MinimizeBox)
            {
             //   minBtn = new WLButton(_owner);

                minBtn.Visible = true;
                minBtn.Bounds = _owner.MinBoxRect;

                minBtn.Click += new EventHandler(MinBtnClick);
                minBtn.ForePathGetter = new ButtonForePathGetter(GraphicsPathHelper.CreateMinimizeFlagPath);
            }
        //    if (_owner.OptionBox)
            {
               // optionBtn = new WLButton(_owner);
                optionBtn.Visible = true;
                optionBtn.Bounds = _owner.OptionBoxRect;
                optionBtn.Click += optionBtn_Click;
                optionBtn.ForePathGetter = new ButtonForePathGetter(GraphicsPathHelper.CreateOptionFlagPath);
            }
            SetControlBoxColor();
        }

        void optionBtn_Click(object sender, EventArgs e)
        {
            if (OnOptionboxClick != null)
                OnOptionboxClick(sender, e);
        }

        public ControlBoxManager(GMForm owner)
        {
            _owner = owner;
            BtnInit();
        }

        public void FormResize()
        {
            if (maxBtn != null)
            {
                if (_owner.WindowState == FormWindowState.Normal)
                {
                    maxBtn.Visible = true;
                    resBtn.Visible = false;
                    resBtn.State = GMButtonState.Normal;
                }
                else
                {
                    resBtn.Visible = true;
                    maxBtn.Visible = false;
                    maxBtn.State = GMButtonState.Normal;
                }
                resBtn.Bounds = maxBtn.Bounds = _owner.MaxBoxRect;
            }
            if (minBtn != null)
                minBtn.Bounds = _owner.MinBoxRect;
            if (optionBtn != null)
                optionBtn.Bounds = _owner.OptionBoxRect;
            closeBtn.Bounds = _owner.CloseBoxRect;
        }

        public void MouseOperation(Point location, MouseOperationType type)
        {
            closeBtn.MouseOperation(location, type);
            if (maxBtn != null && maxBtn.Visible)
                maxBtn.MouseOperation(location, type);
            if (resBtn != null && resBtn.Visible)
                resBtn.MouseOperation(location, type);
            if (minBtn != null)
                minBtn.MouseOperation(location, type);
            if (optionBtn != null)
                optionBtn.MouseOperation(location, type);
        }

        public void DrawBoxes(Graphics g)
        {
            if (_owner.ControlBox)
            {
                closeBtn.PaintControl(g);
                if (_owner.MinimizeBox && minBtn != null)
                    minBtn.PaintControl(g);
                if (_owner.MaximizeBox)
                {
                    if (maxBtn != null & maxBtn.Visible)
                        maxBtn.PaintControl(g);
                    if (resBtn != null && resBtn.Visible)
                        resBtn.PaintControl(g);
                }
                if (_owner.OptionBox && optionBtn != null)
                    optionBtn.PaintControl(g);
            }
        }

        public void ResetBoxColor()
        {
            SetControlBoxColor();
        }
    }
}
