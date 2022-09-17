using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cognex.VisionPro.QuickBuild;

namespace VisionControl
{
    public partial class UCOneJob : Sunny.UI.UIUserControl
    {
        private CogJob _CogJob;
        public CogJob CogJob => _CogJob;
        public event Action RunClicked;
        CogJobStateConstants _State;
        ToolTip toolTip = new ToolTip();
        public int Index { get; set; } = -1;
        public UCOneJob()
        {
            InitializeComponent();
            cogRecordsDisplay1.Hide();
            button1.Size = new Size(28, 28);
            button1.Top = 5;
            button1.Paint += CogRecordDisplay1_Paint;
          
            button1.Click += (_, __) => RunClicked?.Invoke();
            UpdateUIStat(CogJobStateConstants.Stopped);
        }
         

        private void CogRecordDisplay1_Paint(object sender, PaintEventArgs e)
        {
            var c = (Control)sender;
            var g = e.Graphics;
             g.Clear(Color.White);
            var img = _State == CogJobStateConstants.Stopped ? Resource.Run : Resource.Stop;
            var x = 0;
            var y =0;
           e.Graphics.DrawImage(img, x, y, c.Width, c.Height);
            var path = new GraphicsPath();
            //path.AddPolygon(new[] { new Point() });
        
        }

        public UCOneJob(CogJob cogJob):this()
        {
            _CogJob = cogJob;

        }
        public void UpdateUIStat(CogJobStateConstants state)
        {
            _State = state;
            button1.Refresh();
            toolTip.SetToolTip(button1, state == CogJobStateConstants.Stopped ? "点击开始运行" : "点击停止运行");
        }
       
    }
}
