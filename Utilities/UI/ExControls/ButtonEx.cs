using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utilities.UI.ExControls
{
    public class ButtonEx : Button
    {
        public ButtonEx()
        {

        }

        /// <summary>
        /// 重载OnPaint方法
        /// </summary>
        /// <param name="pevent"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            if (BackgroundImage != null)
            {
                Image img = this.BackgroundImage;
                g.DrawImage(img, e.ClipRectangle, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
            }
            g.DrawString(Text, Font, new SolidBrush(Color.Black), e.ClipRectangle, sf);
        }
    }
}