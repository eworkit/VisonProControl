using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Utilities.UI
{
    public class NavBarButton
    {
        private NavGroup _navgroup;
        private Rectangle _clientRectangle;
        public Rectangle ClientRectangle
        {
            get { return this._clientRectangle; }
            set { this._clientRectangle = value; }
        }

        public NavBarButton() { }
        public NavBarButton(NavGroup navgroup)
        {
            this._navgroup = navgroup;
        }

        public event EventHandler Click;

        public void SetClientRectangle(Rectangle parentRect)
        {
            int i =2;
            int w = parentRect.Height;
            this._clientRectangle = new Rectangle(parentRect.Width - w + i, parentRect.Top + i, w - 2 * i, w - 2 * i);
        }
        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillEllipse(Brushes.White, this._clientRectangle);
            g.DrawEllipse(Pens.Gray, this._clientRectangle);
            Point centerpoint = new Point(this._clientRectangle.X + this._clientRectangle.Width / 2, this._clientRectangle.Y + this._clientRectangle.Height / 2);
            int w = this._clientRectangle.Width / 4;
            Pen pen = new Pen(Color.Black, 1.6f);
            if (this._navgroup.GroupState == NavGroupState.collapse)
            {
                g.DrawLine(pen, centerpoint.X, centerpoint.Y, centerpoint.X - w, centerpoint.Y - w);
                g.DrawLine(pen, centerpoint.X, centerpoint.Y, centerpoint.X + w, centerpoint.Y - w);
                g.DrawLine(pen, centerpoint.X, centerpoint.Y + 4, centerpoint.X - w, centerpoint.Y + w - 4);
                g.DrawLine(pen, centerpoint.X, centerpoint.Y + 4, centerpoint.X + w, centerpoint.Y + w - 4);
            }
            else
            {
                g.DrawLine(pen, centerpoint.X, centerpoint.Y, centerpoint.X - w, centerpoint.Y + w);
                g.DrawLine(pen, centerpoint.X, centerpoint.Y, centerpoint.X + w, centerpoint.Y + w);
                g.DrawLine(pen, centerpoint.X, centerpoint.Y - 4, centerpoint.X - w, centerpoint.Y + w - 4);
                g.DrawLine(pen, centerpoint.X, centerpoint.Y - 4, centerpoint.X + w, centerpoint.Y + w - 4);
            }
        }
        public void DoClick()
        {
            if (this.Click != null)
            {
                this.Click(this, new EventArgs());
            }
        }
    }
}
