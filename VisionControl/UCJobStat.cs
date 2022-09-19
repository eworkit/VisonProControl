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
        Stopwatch stopWatch = new Stopwatch();
        public string JobName { get => uiGroupBox1.Text; set => uiGroupBox1.Text = value; }
        public string ElapsedText { get=>tbElapse.Text; set => tbElapse.Text = value; } 

        public UCJobStat()
        {
            InitializeComponent();
            this.BackColor = Color.Transparent;
        }
        public void Start()
        {
            stopWatch.Start();
        }
        public void Stop()
        {
            stopWatch.Stop();
        }
        public void Reset()
        {
            stopWatch.Reset();
        }
        public TimeSpan Elapsed => stopWatch.Elapsed;
    }
}
