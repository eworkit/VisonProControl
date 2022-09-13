using System;

namespace Utilities.UI
{
    public class ThemeScrollbarEllipse : GMScrollBarThemeBase
    {
        public ThemeScrollbarEllipse()
        {
            SideButtonBorderType = ButtonBorderType.Ellipse;
            MiddleButtonOutterSpace2 = 1;
          //  MiddleButtonRadius = 12;
            SideButtonLength = BestUndirectLen;
        }
    }
}
