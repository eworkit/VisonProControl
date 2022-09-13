using System;
using System.Drawing;

using Utilities.UI.MyGraphics;

namespace Utilities.UI
{
    public class ThemeFormNew : ThemeFormBase
    {
        public ThemeFormNew()
            : base()
        {
            // about theme
            ThemeName = "A New Look Theme";

            UseDefaultTopRoundingFormRegion = false;
            BorderWidth = 0;
            CaptionHeight = 26;
            IconSize = new Size(24, 24);
            ControlBoxOffset = new Point(1, 1);
            ControlBoxSpace = 2;
            OptionBoxSize = MaxBoxSize = MinBoxSize  = new Size(32, 21);
            CloseBoxSize = new Size(32, 21);
            SideResizeWidth = 4;

            CaptionBackColorBottom = ColorHelper.GetLighterColor(/*Color.Gray*/ Color.FromArgb(30,100,200), 40);
           // CaptionBackColorTop = ColorHelper.GetLighterColor(CaptionBackColorBottom, 40);
            CaptionBackColorTop = ColorHelper.GetLighterColor(/*Color.Gray*/ Color.FromArgb(30, 100, 200), 60);
            RoundedStyle = RoundStyle.None;
            CloseBoxColor = ButtonColorTable.GetDefaultCloseBtnColor();
            FormBorderOutterColor = Color.FromArgb(30, 100, 200);// Color.Black;
            FormBorderInnerColor = Color.FromArgb(30, 100, 160); Color.FromArgb(200, Color.White);
            SetClientInset = false;
            
        }
    }
}
