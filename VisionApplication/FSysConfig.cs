using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace VisionApplication
{
    public partial class FSysConfig : Sunny.UI.UIEditForm
    {
        public FSysConfig()
        {
            InitializeComponent();
            ckbAutoStart.Checked = MyAppConfig.AutoStart;
            ckbAutoOpen.Checked = MyAppConfig.AutoOpenFile;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MyAppConfig.AutoLogin = ckbAutoStart.Checked;
            MyAppConfig.AutoOpenFile = ckbAutoOpen.Checked;
        }
    }
}
