using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
using Sunny.UI;
using Utilities;
using Utilities.ExMethod;
using Utilities.Net;
using Utilities.UI;
using Utilities.UI.ExMethod;

namespace VisionControl
{
    public partial class UcTcpClient : UserControl
    {
        TcpClientEx tcpClient;

        public UcTcpClient()
        {
            InitializeComponent();
            tbPort.SetType(TextInputType.Int, false);
            tcpClient = new TcpClientEx();
            tcpClient.OnReceive += TcpClient_OnReceive;
            this.Disposed += UcTcpClient_Disposed;
            btnDisCon.Enabled = false;
            this.BackColor = Color.Transparent;
            var cfg = MyAppConfig.TcpClientInfo;
            if (cfg != null)
            {
                tbServer.Text = cfg.Server;
                tbPort.Text = cfg.Port.ToString();
                ckRcvHex.Checked = cfg.HexReceive;
                ckSentHex.Checked = cfg.HexSend;
                ckAutoConn.Checked = cfg.AutoConn;
            }
        }

        private void UcTcpClient_Disposed(object sender, EventArgs e)
        {
            tcpClient.Dispose();
        }

        private void TcpClient_OnReceive(object sender, TcpClientEventArgs e)
        {
            string text = ByteConverter.ToSocketString(e.Read, ckRcvHex.Checked);
            
            tbRcvData.SafeInvoke(()=>
            {
                if (!string.IsNullOrEmpty(tbRcvData.Text))
                    text = Environment.NewLine + text;
                tbRcvData.AppendText(text);
            });
        }
        void SetUIStat()
        {
            bool conn = tcpClient.IsOnline;
            this.SafeInvoke(() =>
            {
                btnConnect.Enabled = !conn;
                btnDisCon.Enabled = conn;
            });
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient.BeginConnect(tbServer.Text, tbPort.Text.ToInt());
                SetUIStat();
            }catch(Exception ex)
            {
                MessageBoxE.Show(this, ex.Message);
            }
        }

        private void btnDisCon_Click(object sender, EventArgs e)
        {
            tcpClient.Stop();
            SetUIStat();
        }

        private void btnSend_Click(object sender, EventArgs e)
        { 
            if(string.IsNullOrEmpty( tbSentData.Text))
            {
                MessageBoxE.Show("请输入要发送的数据");
            }
            try
            {
                if(!tcpClient.IsOnline )
                {
                    if(ckAutoConn.Checked)
                    {
                        btnConnect_Click(sender, e);
                    }
                }
                var data = ByteConverter.ToSocketBytes(tbSentData.Text, ckSentHex.Checked);
                if(!tcpClient.client.Connected)
                {
                    MessageBoxE.Show(this, "未连接到服务器");
                    return;
                }
                tcpClient.Send(data);
            }
            catch(Exception ex)
            {
                MessageBoxE.Show(this, ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbRcvData.Clear();
        }
    }
}
