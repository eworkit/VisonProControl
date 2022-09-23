using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        public event Action<string> Received;

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
                if (cfg.AutoConn)
                {
                    new Thread(() =>
                    {
                        Thread.Sleep(1000);
                        btnConnect_Click(this, EventArgs.Empty);
                    }).Start();
                }
            }
            this.Load += UcTcpClient_Load;
            
        }

        private void UcTcpClient_Load(object sender, EventArgs e)
        {
            
        }

        private void UcTcpClient_Disposed(object sender, EventArgs e)
        {
            tcpClient.Dispose();
        }

        private void TcpClient_OnReceive(object sender, TcpClientEventArgs e)
        {
            string text = ByteConverter.ToSocketString(e.Read, ckRcvHex.Checked);
            Received?.Invoke(text);
            tbRcvData.SafeInvoke(() =>
            {
                tbRcvData.AppendText($"[{DateTime.Now.ToString("MM-dd HH:mm:ss")}]{text}\r\n");
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
            }
            catch (Exception ex)
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
            if (string.IsNullOrEmpty(tbSendData.Text))
            {
                MessageBoxE.Show("请输入要发送的数据");
            }
            try
            {
                if (!tcpClient.IsOnline)
                {
                    if (ckAutoConn.Checked)
                    {
                        btnConnect_Click(sender, e);
                    }
                }
                var data = ByteConverter.ToSocketBytes(tbSendData.Text, ckSentHex.Checked);
                if (!tcpClient.client.Connected)
                {
                    MessageBoxE.Show(this, "未连接到服务器");
                    return;
                }
                tcpClient.Send(data);
            }
            catch (Exception ex)
            {
                MessageBoxE.Show(this, ex.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbRcvData.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ckTimeToSent.Checked)
            {
                if (string.IsNullOrEmpty(tbSendData.Text))
                {
                    MessageBoxE.Show(this, "设为定时发送模式时发送的数据不能为空", "提示");
                    tbSendData.Focus();
                    return;
                }
            }
            var cfg = MyAppConfig.TcpClientInfo ?? new MyAppConfig.TcpClient();
            cfg.Server = tbServer.Text;
            cfg.Port = tbPort.Text.ToInt();
            cfg.HexReceive = ckRcvHex.Checked;
            cfg.HexSend = ckSentHex.Checked;
            cfg.AutoConn = ckbAutoStart.Checked;
            cfg.AutoSendInterval = (int)uiDoubleUpDown1.Value;
            try
            {
                MyAppConfig.Save(cfg);
                MessageBoxE.Show(this, " 保存成功");
            }
            catch (Exception ex) { MessageBoxE.Show(this, " 保存失败\r\n" + ex.Message); }
        }
    }
}
