using System;
using System.Drawing;

namespace Utilities.UI
{
    /// <summary>
    /// for diamond ring style only
    /// </summary>
    public class ThemeRollingBarRing : GMRollingBarThemeBase
    {
        public ThemeRollingBarRing()
        {
            Radius1 = 18;
            BaseColor = Color.Gold;            
            DiamondColor = Color.FromArgb(160, Color.Red);
        }
    }
}
