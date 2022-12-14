using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Utilities.UI.ExControls
{
    public partial class LabelX : System.Windows.Forms.Label
    {
        int lineDistance = 5;//行间距   
        Graphics gcs;
        int iHeight = 0, height = 200;
        string[] nrLine;
        string[] nrLinePos;
        int searchPos = 0;
        int section = 1;
        public int LineDistance
        {
            get { return lineDistance; }
            set
            {
                lineDistance = value;
                Changed(this.Font, this.Width, this.Text);
            }
        }
        public LabelX()
            : base()
        {
            //this.TextChanged += new EventHandler(LabelTx_TextChanged);  
            this.SizeChanged += new EventHandler(LabelTx_SizeChanged);
            this.FontChanged += new EventHandler(LabelTx_FontChanged);
            //this.Font = new Font(this.Font.FontFamily, this.Font.Size, GraphicsUnit.Pixel);  
        }

        void LabelTx_FontChanged(object sender, EventArgs e)
        {
            Changed(this.Font, this.Width, this.Text);
        }

        void LabelTx_SizeChanged(object sender, EventArgs e)
        {
            Changed(this.Font, this.Width, this.Text);

        }

        public LabelX(IContainer container)
        {
            container.Add(this);
            //base.Height  

            //InitializeComponent();  
        }
        public int FHeight
        {
            get { return this.Font.Height; }
        }
        protected int Height
        {
            get { return height; }
            set
            {
                height = value;
                base.Height = value;
            }
        }
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                //is.Font.Size.                      
                base.Text = value;
                Changed(this.Font, this.Width, value);
            }
        }

        protected void Changed(Font ft, int iWidth, string value)
        {
            iHeight = 0;
            if (value != "")
            {
                if (gcs == null)
                {

                    gcs = this.CreateGraphics();
                    SizeF sf0 = gcs.MeasureString(new string('测', 20), ft);
                    searchPos = (int)(iWidth * 20 / sf0.Width);

                }

                nrLine = value.Split(new string[1] { "/r/n" }, StringSplitOptions.RemoveEmptyEntries);
                section = nrLine.Length;
                nrLinePos = new string[section];
                SizeF sf1, sf2;
                string temps, tempt;
                string drawstring;
                int temPos, ipos;
                for (int i = 0; i < section; i++)
                {
                    ipos = 0;
                    temPos = searchPos;
                    if (searchPos >= nrLine[i].Length)
                    {
                        ipos += nrLine[i].Length;
                        nrLinePos[i] += "," + ipos.ToString();
                        iHeight++;
                        continue;
                    }
                    drawstring = nrLine[i];
                    nrLinePos[i] = "";
                    while (drawstring.Length > searchPos)
                    {
                        bool isfind = false;
                        for (int j = searchPos; j < drawstring.Length; j++)
                        {
                            temps = drawstring.Substring(0, j);
                            tempt = drawstring.Substring(0, j + 1);
                            sf1 = gcs.MeasureString(temps, ft);
                            sf2 = gcs.MeasureString(tempt, ft);
                            if (sf1.Width < iWidth && sf2.Width > iWidth)
                            {
                                iHeight++;
                                ipos += j;
                                nrLinePos[i] += "," + ipos.ToString();
                                isfind = true;
                                drawstring = drawstring.Substring(j);
                                break;

                            }

                        }
                        if (!isfind)
                        {
                            //drawstring = drawstring.Substring(searchPos);  
                            //iHeight++;  
                            break;
                        }


                    }
                    ipos += drawstring.Length;
                    nrLinePos[i] += "," + ipos.ToString();
                    iHeight++;

                    //tempLine = (int)(sf1.Width - 1) / this.Width + 1;                          
                    //iHeight += tempLine;  
                }
            }
            this.Height = iHeight * (ft.Height + lineDistance);
            Refresh();
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaint(e);             
            //if (isPaint) return;  
            //isPaint = true;  
            Graphics g = e.Graphics;
            String drawString = this.Text;
            Font drawFont = this.Font;
            SolidBrush drawBrush = new SolidBrush(this.ForeColor);
            SizeF textSize = g.MeasureString(this.Text, this.Font);//文本的矩形区域大小     
            int lineCount = Convert.ToInt16(textSize.Width / this.Width) + 1;//计算行数     
            int fHeight = this.Font.Height;
            int htHeight = 0;


            this.AutoSize = false;
            float x = 0.0F;
            float y = 0.0F;
            StringFormat drawFormat = new StringFormat();
            int step = 1;
            bool isFirst = true;
            SizeF sf1, sf2;
            string subN, subN1;
            lineCount = drawString.Length;//行数不超过总字符数目     
            int i, idx, first;
            string subStr, tmpStr = "", midStr = "";
            string[] idxs;
            for (i = 0; i < section; i++)
            {
                first = 0;
                subStr = nrLine[i];
                if (nrLinePos[i] != null) tmpStr = nrLinePos[i].TrimStart(',');
                midStr = subStr.Substring(first);
                if (tmpStr != "")
                {
                    idxs = tmpStr.Split(',');
                    for (int j = 0; j < idxs.Length; j++)
                    {

                        idx = int.Parse(idxs[j]);
                        midStr = subStr.Substring(first, idx - first);
                        e.Graphics.DrawString(midStr, drawFont, drawBrush, x, Convert.ToInt16(htHeight), drawFormat);
                        htHeight += (fHeight + lineDistance);
                        first = idx;
                    }
                    //midStr = subStr.Substring(first);  
                }

                //e.Graphics.DrawString(midStr, drawFont, drawBrush, x, Convert.ToInt16(htHeight), drawFormat);  
                //htHeight += ( lineDistance);//fHeight +  

            }

        }
    }


  
}
 
