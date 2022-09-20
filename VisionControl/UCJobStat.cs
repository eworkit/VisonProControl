using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionControl
{
    public partial class UCJobStat : UserControl
    {
        internal Stopwatch StopWatch = new Stopwatch();
        public string JobName { get => uiGroupBox1.Text; set => uiGroupBox1.Text = value; }
        public string ElapsedText { get=>tbElapse.Text; set => tbElapse.Text = value; } 

        public UCJobStat()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
        }
        public void Start()
        {
            StopWatch.Start();
        }
        public void Stop()
        {
            StopWatch.Stop();
        }
        public void Reset()
        {
            StopWatch.Reset();
        }
        public TimeSpan Elapsed => StopWatch.Elapsed;
    }
}
