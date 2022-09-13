using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities.ExMethod;
using Utilities.UI.ExMethod;

namespace Utilities
{
    public partial class FormFeedback : Utilities.UI.GMForm
    {
        Utilities.UI.WaitAnimate waitDlg;
        public FormFeedback(string language = "")
        {
            InitializeComponent();
            if (language.IsEmpty()) ;
            language = System.Globalization.CultureInfo.CurrentCulture.Name;
            base.XTheme = new UI.ThemeFormDevExpress();
            this.StartPosition = FormStartPosition.CenterParent;
            this.AcceptButton = gmButton1;
            this.CancelButton = gmButton2;
            waitDlg = new UI.WaitAnimate(language);
            waitDlg.DoWork += waitDlg_DoWork;
            waitDlg.RunWorkerCompleted += waitDlg_RunWorkerCompleted;
            SetLanguage(language);
        }
        /// <summary>
        /// zh-CN,zh-TW
        /// </summary>
        /// <param name="Name"></param>
        public void SetLanguage(string Name)
        {
            if (Name.EqualsNoCase("zh-CN"))
            {
                waitDlg.Text = "正在发送...";
            }
            else if (Name.EqualsNoCase("zh-TW"))
            {
                this.Text = "反饋";
                label1.Text = "問題或意見";
                label2.Text = "您的聯繫方式";
                gmButton1.Text = "發送";
                gmButton2.Text = "取消";
                waitDlg.Text = "正在發送...";
            }
            else
            {
                this.Text = "Feedback";
                label1.Text = "Questions or suggestions";
                label2.Text = "Your contact way";
                gmButton1.Text = "Send";
                gmButton2.Text = "Cancel";
                waitDlg.Text = "Sending...";
            }
        }
        private void gmButton1_Click(object sender, EventArgs e)
        {
            gmButton1.Enabled = false;
            waitDlg.RunWorkerAsync();
        }

        void waitDlg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            gmButton1.SafeInvoke(() => gmButton1.Enabled = true);
            var s = System.Globalization.CultureInfo.CurrentCulture.Name;
            string sInfo = "Info", sError = "Error";
            string sOK = "Send successfully", sFail = "Send failed";
            if (s.EqualsNoCase("zh-CN"))
            {
                sOK = "发送成功";
                sInfo = "消息";
                sError = "错误";
            }
            else if (s.EqualsNoCase("zh-TW"))
            {
                sOK = "發送成功";
                sInfo = "消息";
                sError = "錯誤";
            }
            if (true.Equals(e.Result))
            {
                MessageBox.Show(sOK, sInfo, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show(e.Result.ToStr(), sError, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void waitDlg_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string s = System.Diagnostics.Process.GetCurrentProcess().MainWindowTitle;
                string ip = "";
                var addrs = System.Net.Dns.GetHostEntry(System.Environment.MachineName).AddressList;
                if (addrs.Length == 1)
                    ip = addrs[0].ToString();
                else if (addrs.Length > 1)
                    foreach (var addr in addrs)
                    {
                        if (addr.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            ip = addr.ToString();
                    }
                Utilities.Net.EmailHelper.SendEmail("来自:" + s + "[" + Net.TcpHelper.GetOuterIP() + "]", textBox1.Text + "<br/><br/><br/>" + textBox2.Text + "<br/>内部IP:" + ip + "; ComputerName:" + Environment.MachineName);
                e.Result = true;
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
            }
        }
    }
}
