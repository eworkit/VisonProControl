using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Utilities.UI
{
    public  class TabControlEx:TabControl
    {
        const int  CLOSE_SIZE =16;
        public TabControlEx()
        {
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.SetStyle(ControlStyles.DoubleBuffer  /*| ControlStyles.UserPaint*/ | ControlStyles.AllPaintingInWmPaint, true);
          //  this.DoubleBuffer = true; 
            this.UpdateStyles();
 
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
          try
          {             
              Rectangle myTabRect = this.GetTabRect(e.Index);//返回该选项卡控件中的指定选项卡的边框
              Rectangle origRect = myTabRect;
              Pen p = new Pen(Color.Blue);
              Color recColor = Color.LightGray;
              Brush b = new LinearGradientBrush(myTabRect, Color.LightGray, Color.DarkGray,   LinearGradientMode.Vertical); 
               
             // e.Graphics.DrawRectangle(p, myTabRect);//画边框
              e.Graphics.FillRectangle(b, myTabRect);//填充矩形框内颜色

              int textLeft = 2;
              if (ImageList != null && TabPages[e.Index].ImageIndex > -1)
              {
                  var img = ImageList.Images[TabPages[e.Index].ImageIndex];
                  e.Graphics.DrawImage(img,myTabRect.X+2,myTabRect.Y+1);
                  textLeft = img.Width +2 ;
              }
              //先添加TabPage 名称
              e.Graphics.DrawString(this.TabPages[e.Index].Text
                  , this.Font, SystemBrushes.ControlText, myTabRect.X +textLeft, myTabRect.Y + 3);


              if (e.Index == this.SelectedIndex)
              {
                  textLeft = 2;
                  Brush b2 = new LinearGradientBrush(myTabRect, Color.FromArgb(68, 121, 225), Color.FromArgb(0, 70, 197), LinearGradientMode.Vertical); 
                  e.Graphics.FillRectangle(b2, myTabRect);
                  if (ImageList != null && TabPages[e.Index].ImageIndex > -1)
                  {
                      var img = ImageList.Images[TabPages[e.Index].ImageIndex];
                      e.Graphics.DrawImage(img, myTabRect.X + 2, myTabRect.Y + 1);
                      textLeft  = img.Width + 2;
                  }
                  ////再画一个矩形框
                  //myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 5), 5);
                  //myTabRect.Width = CLOSE_SIZE;
                  //myTabRect.Height = CLOSE_SIZE;
                  //e.Graphics.DrawRectangle(p, myTabRect);
                  ////填充矩形框
                  //e.Graphics.FillRectangle(new SolidBrush(Color.DodgerBlue), myTabRect);


                  ////画关闭符号
                  //using (Pen objpen = new Pen(Color.Black))
                  //{
                  //    //"\"线
                  //    Point p1 = new Point(myTabRect.X + 3, myTabRect.Y + 0);
                  //    Point p2 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + myTabRect.Height - 6);
                  //    e.Graphics.DrawLine(objpen, p1, p2);


                  //    //"/"线
                  //    Point p3 = new Point(myTabRect.X + 3, myTabRect.Y + myTabRect.Height - 6);
                  //    Point p4 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + 0);
                  //    e.Graphics.DrawLine(objpen, p3, p4);
                  //}
                  //写标签文字
                  e.Graphics.DrawString(this.TabPages[e.Index].Text, this.Font, Brushes.White, origRect.X + textLeft, origRect.Y + 3);
                  p.Dispose();
                  b.Dispose();
                  e.Graphics.Dispose();
              }
              TabPage newpag=this.TabPages[e.Index];
              if(newpag.Tag==(object)1)
              {
                  TabPage newp = this.TabPages[e.Index];
                  e.Graphics.FillRectangle(Brushes.Yellow, myTabRect);
                  e.Graphics.DrawString(this.TabPages[e.Index].Text, this.Font, SystemBrushes.ControlText, origRect.X + 2, origRect.Y + 3);
              }
          }
          catch (Exception ex)
          {
             
          }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
//             if (e.Button == MouseButtons.Left)
//             {
//                 int x = e.X, y = e.Y;
// 
//                 //计算关闭区域   
//                 Rectangle myTabRect = this.GetTabRect(this.SelectedIndex);
// 
//                 myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
//                 myTabRect.Width = CLOSE_SIZE;
//                 myTabRect.Height = CLOSE_SIZE;
// 
//                 //如果鼠标在区域内就关闭选项卡   
//                 bool isClose = x > myTabRect.X && x < myTabRect.Right
//                  && y > myTabRect.Y && y < myTabRect.Bottom;
// 
//                 if (isClose == true)
//                 {
//                     this.TabPages.Remove(this.SelectedTab);
//                 }
//             }
        }
    }
}
