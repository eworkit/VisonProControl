using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace Utilities.UI
{
    public class ButtonCollapse : Label
    {
        Color backClr = Color.Empty;
        public Orientation Orientation { get; set; }
        bool isMouseEnter=false;
        bool isExpand = true;
        public bool IsExpand
        {
            get { return isExpand; }
            set
            {
                if (isExpand != value)
                {
                    isExpand = value;
                    if (OnExpandChanged != null)
                        OnExpandChanged(value);
                    Refresh();
                }
            }
        }private Rectangle _clientRectangle;
        public event Action<bool> OnExpandChanged;
        public ButtonCollapse()
        {
            Orientation = Orientation.Vertical;
            this.BackColor = Color.Transparent;
           
            Height = 17;
            Width = Height;
            int i = 1;
            int w = Height;
            this._clientRectangle = new Rectangle(Width - w + i, Top + i, w - 2 * i, w - 2 * i);
            Text = string.Empty;
           
            Click += new EventHandler(ButtonCollapse_Click);
            
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!isMouseEnter)
            {
                isMouseEnter = true;
                Refresh();
            }
         
          //  BackColor = Color.DeepSkyBlue;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isMouseEnter = false;
            Refresh();
          //  BackColor = backClr;
        }
        void ButtonCollapse_Click(object sender, EventArgs e)
        {
            IsExpand = !IsExpand;
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            if (backClr == Color.Empty)
                backClr = BackColor;
            Draw(pevent.Graphics);
            return;
            var g = pevent.Graphics;
            if (Orientation == Orientation.Horizontal)
            {

            }
            else
            {
                if (IsExpand)
                {
                    GraphicsPath path = new GraphicsPath();
                    RectangleF ballRect = new RectangleF(3, 3, Width - 6, Height - 6);
                    path.AddEllipse(ballRect);
                    PathGradientBrush pthGrBrush = new PathGradientBrush(path);

                    pthGrBrush.CenterColor = Color.FromArgb(255, 255, 64, 0);
                    pthGrBrush.SurroundColors = new Color[] { Color.FromArgb(255, 100, 20, 20) };
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;      
                    g.FillEllipse(pthGrBrush, ballRect);
                    return;
                    // PointF[] p =new PointF[3];
                    //p[0]=new PointF(ballRect.Left+1,ballRect.Top*1.3F);
                    //p[1] = new PointF(ballRect.Right - 1, ballRect.Top * 1.3F);
                    //p[2] = new PointF(ballRect.Left+ballRect.Width / 2, ballRect.Bottom * 0.8F);
                    //g.FillPolygon(Brushes.AliceBlue, p);
                    int len = (int) ballRect.Width;

                    int x =(int)ballRect.Left;

                    int y = (int)ballRect.Top;

　             Point[] pntArr = new Point[3];

　             pntArr[0] = new Point(x, y);

　             pntArr[1] = new Point(x + len, y);

　             pntArr[2] = new Point(x + len / 2, (int)(len * Math.Sqrt(3) / 2 + y));

　             GraphicsPath path2 = new GraphicsPath();

              path2.AddLines(pntArr);

　              g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

              g.FillPath(Brushes.AliceBlue, path2);

　             g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;      

　            
                }
            }
        }
        public void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (isMouseEnter)
            {
                g.FillEllipse(Brushes.RoyalBlue, this._clientRectangle);
                g.DrawEllipse(Pens.OliveDrab, this._clientRectangle);
            }
            else
            {
                g.FillEllipse(Brushes.White, this._clientRectangle);
                g.DrawEllipse(Pens.Gray, this._clientRectangle);
            }
            PointF centerpoint = new PointF(this._clientRectangle.X + this._clientRectangle.Width / 2, this._clientRectangle.Y + this._clientRectangle.Height / 2);
            float w = this._clientRectangle.Width / 4F;
            Pen pen = null;
            if (isMouseEnter)
                pen = new Pen(Color.White, 1.6f);
            else
              pen =  new Pen(Color.Black, 1.6f);
            if (!IsExpand)
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
    }
 
}
