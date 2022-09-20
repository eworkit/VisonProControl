using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities.UI;
using Utilities.Data;

namespace VisionApplication
{
    public partial class UCDBConfig : UserControl
    {
        public UCDBConfig()
        {
            InitializeComponent();
            if (UIhelper.IsDesignMode())
                return; 
        } 
        public event Action<UCDBConfig, string> DBhostChanged;
        public event Action<UCDBConfig,string> DBNameChanged;
        public DBConnInfo DBInfo
        {
            get
            {
                return new DBConnInfo() { dbName = tbDatabase.Text.Trim(), host = tbHost.Text.Trim(), port = tbPort.Text.Trim(), pwd = tbPassword.Text, user = tbUser.Text };
            }
            set
            {
                tbDatabase.Text = value.dbName;
                tbHost.Text = value.host;
                tbPort.Text = value.port;
                tbUser.Text = value.user;
                tbPassword.Text = value.pwd;
            }
        }
 
        private void UCDBConfig_Load(object sender, EventArgs e)
        {
            
            tbHost.TextChanged += (s1, e1) =>{if(DBhostChanged!=null) DBhostChanged(this,tbHost.Text);};
            tbDatabase.TextChanged += (s1, e1) => { if (DBNameChanged != null) DBNameChanged(this, tbDatabase.Text); };
        }
    }
}
