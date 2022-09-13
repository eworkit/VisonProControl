using System;
using System.Drawing;

namespace Utilities.UI
{
    public class ThemeProgressBarBlue : ThemeProgressBarGreen
    {
        public ThemeProgressBarBlue()
        {
            CoveredColor = Color.FromArgb(181, 205, 242);
            LeadingEdgeColor = Color.FromArgb(153, 163, 180);
        }
    }
}
