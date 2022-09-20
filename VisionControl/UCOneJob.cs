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
using Utilities.UI;

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

       
        void SetMouseState(Control c,  MouseOperationType t)
        {
            MouseOType = t;
            c.Refresh();
        }

        MouseOperationType MouseOType = MouseOperationType.Leave;
        public UCOneJob()
        {
            InitializeComponent();
            cogRecordsDisplay1.Hide();
            button1.Size = new Size(24, 24);
            button1.Top = -100;
            this.SizeChanged += UCOneJob_SizeChanged;
   
            button1.Click += (_, __) => RunClicked?.Invoke();
            button1.MouseMove += (_, __) => SetMouseState(button1, MouseOperationType.Move);
            button1.MouseDown += (_, __) => SetMouseState(button1, MouseOperationType.Down);
            button1.MouseLeave +=(_, __) => SetMouseState(button1, MouseOperationType.Leave);
            button1.MouseUp += (_, __) => SetMouseState(button1, MouseOperationType.Up);
            UpdateUIStat(CogJobStateConstants.Stopped);
        }

        private void UCOneJob_SizeChanged(object sender, EventArgs e)
        {
            if (_CogJob != null)
            {
                button1.Left = cogRecordDisplay1.Right - button1.Width - SystemInformation.VerticalScrollBarWidth - 5;
                button1.Paint += CogRecordDisplay1_Paint;
            }
        }

        private void CogRecordDisplay1_Paint(object sender, PaintEventArgs e)
        {
            var c = (Control)sender;
            var g = e.Graphics;
            g.Clear(Color.White);
            var img = _State == CogJobStateConstants.Stopped ? Resource.Run : Resource.Stop;

            var x = 0;
            var y = 0;
            var w = c.Width;
            var h = c.Height;
            var m = 3;
            //  e.Graphics.DrawImage(img, x, y, c.Width, c.Height);
            var path = new GraphicsPath();
            if (e.Graphics.SmoothingMode != SmoothingMode.AntiAlias)
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var bg = Color.White;
            switch (MouseOType)
            {
                case MouseOperationType.Move:
                    bg = Color.FromArgb(220, 230, 0);
                    break;
                case MouseOperationType.Down:
                    bg = Color.FromArgb(250, 250, 0);
                    break;
                case MouseOperationType.Up:
                    bg = Color.FromArgb(250, 250, 0);
                    break;
                case MouseOperationType.Hover:
                    bg = Color.FromArgb(220, 230, 0);
                    break;
                case MouseOperationType.Leave: 
                    break;
                case MouseOperationType.Wheel:
                    break;
            }
            g.Clear(bg);
            if (_State != CogJobStateConstants.Stopped)
            {
                path.AddRectangle(new RectangleF(m + 2, m + 2, h - 2 * m - 4, h - 2 * m - 4));
            }
            else
            {
                path.AddPolygon(new[] { new Point(m + 2, m), new Point(w - m - 2, h / 2), new Point(m + 2, h - 2 * m) });
            }
            e.Graphics.FillPath(Brushes.Blue, path);
        }
        public UCOneJob(CogJob cogJob):this()
        {
            _CogJob = cogJob;
            if(cogJob!= null)
            {
                button1.Top = 5;
            }

        }
        public void UpdateUIStat(CogJobStateConstants state)
        {
            _State = state;
            button1.Refresh();
            toolTip.SetToolTip(button1, state == CogJobStateConstants.Stopped ? "点击开始运行" : "点击停止运行");
        }

    }
}
