using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Utilities.UI
{
    public partial class FlowLayoutPanelEx : FlowLayoutPanel
    {
        HorizontalAlignment hAlign;
        public HorizontalAlignment HAlign
        {
            get { return hAlign; }
            set { hAlign = value;
            FlowLayoutPanelEx_Resize(this, null);
            }
        }
        public VerticalAlignment VAglin { get; set; }
        public Padding ItemSpace { get; set; }
        public FlowLayoutPanelEx()
        {
            InitializeComponent();
           hAlign = HorizontalAlignment.Center;
            ItemSpace = new Padding(3,3,3,3);


           // this.Resize += new EventHandler(FlowLayoutPanelEx_Resize);
         //   this.Layout += new LayoutEventHandler(FlowLayoutPanelEx_Layout);
            this.Layout += FlowLayoutPanelEx_Resize;
        }

        void FlowLayoutPanelEx_Layout(object sender, LayoutEventArgs e)
        {
           
             
        }

        void FlowLayoutPanelEx_Resize(object sender, EventArgs e)
        {
            if (Controls.Count == 0) return;
            if (!this.Visible) return;
            if (this.FlowDirection == FlowDirection.LeftToRight)
            {
                if (HAlign == HorizontalAlignment.Center)
                {
                    int widthAll = 0;
                    foreach (Control c in Controls)
                    {
                        if (c.Visible)
                        { widthAll += c.Width +ItemSpace.Left +ItemSpace.Right; }
                    }

                    int left = (Width - widthAll) / 2;
                    int n = 0;
                    foreach (Control c in Controls)
                    {
                        if (c.Visible)
                        {
                            if (n++ == 0)
                                c.Margin = new Padding(left, 3, ItemSpace.Right , Height - 3 - c.Height);
                            else
                                c.Margin = new Padding(ItemSpace.Left , 3, ItemSpace.Right , Height - 3 - c.Height);
                           
                          //  left += Space / 2 + c.Width;
                        }
                    }
                }
                else
                {

                }
            }
            else if (this.FlowDirection == FlowDirection.TopDown)
            {
                if (HAlign == HorizontalAlignment.Center)
                {
                    int i = 0;
                    foreach (Control c in this.Controls)
                    {
                        int sp = ItemSpace.Left;
                        if (i++ == 0)
                            sp = c.Margin.Top;
                        c.Margin = new Padding((Width - c.Width) / 2, ItemSpace.Top, ItemSpace.Right, ItemSpace.Bottom);
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

        }
    }
}
