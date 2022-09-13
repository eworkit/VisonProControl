using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        public UCOneJob()
        {
            InitializeComponent();
            cogRecordsDisplay1.Hide(); 
        }
        public UCOneJob(CogJob cogJob):this()
        {
            _CogJob = cogJob;
        }
    }
}
