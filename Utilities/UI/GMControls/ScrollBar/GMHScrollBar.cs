using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Utilities.UI
{
    [ToolboxItem(true)]
    public class GMHScrollBar : GMScrollBarBase
    {
        protected override System.Windows.Forms.Orientation BarOrientation
        {
            get { return System.Windows.Forms.Orientation.Horizontal; }
        }
    }
}
