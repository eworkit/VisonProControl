using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Utilities.UI
{
   public   class PanelEx:System.Windows.Forms.Panel
    {
       public int BorderWidth { get; set; }
       public Color BorderColor { get; set; }
       public PanelEx ()
       {
           BorderWidth = 0;
           BorderColor = BackColor;
       }
       protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
       {
           base.OnPaint(e);
           if (BorderWidth > 0)
           {
               using (Pen pen = new Pen(BorderColor, BorderWidth))
               {
                   e.Graphics.DrawRectangle(pen, this.ClientRectangle);
               }
           }
       }
    }
}
