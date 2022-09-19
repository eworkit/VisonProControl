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
using Utilities.UI.ExMethod;

namespace VisionControl
{
    public partial class UcTcpServer : UserControl
    {
        TcpServer _tcpServer;
        public UcTcpServer()
        {
            InitializeComponent();
            _tcpServer = new TcpServer(); 
            _tcpServer.OnConnect += _tcpServer_OnConnect;
            _tcpServer.OnDataAvailable += _tcpServer_OnDataAvailable;
            _tcpServer.OnError += _tcpServer_OnError;
            _tcpServer.OnLostConnect += _tcpServer_OnLostConnect;

            this.Disposed += UcTcpServer_Disposed;
            tbIP.Text = Utilities.Net.TcpHelper.GetInnerIP(); 
            btnClose.Enabled=false;
            var cfg = MyAppConfig.TcpServerInfo;
            if(cfg!=null)
            {
                tbPort.Text = cfg.Port.ToString();
                ckSentHex.Checked = cfg.HexSend;
                ckRcvHex.Checked = cfg.HexReceive;
                if(cfg.AutoStart)
                {
                    btnTcpSvrOpen_Click(null, EventArgs.Empty);
                }
            }
        }

        private void _tcpServer_OnLostConnect(TcpServerConnection connection)
        {
            var client = connection.Socket.Client;
            var s = ((System.Net.IPEndPoint)client.RemoteEndPoint).Address.ToString();
            for(int i=lbRcvClients.Items.Count-1; i>=0;i--)
            {
                var item = lbRcvClients.Items[i];
                if (((string)item).Contains(s))
                    lbRcvClients.SafeInvoke(()=>lbRcvClients.Items.RemoveAt(i));
            }
        }

        void SetUIStat()
        {
            bool open = _tcpServer.IsOpen;
            this.SafeInvoke(() =>
            {
                btnTcpSvrOpen.Enabled = !open;
                btnClose.Enabled = open;
            });
        }
        private void _tcpServer_OnError(TcpServer server, Exception e)
        {
            SetUIStat();
        }

        private void UcTcpServer_Disposed(object sender, EventArgs e)
        {
            _tcpServer.Close();
            SetUIStat();
        }

        private void _tcpServer_OnDataAvailable(TcpServerConnection connection)
        {
            var data = connection.Read();
            string text = ByteConverter.ToSocketString(data, ckRcvHex.Checked); 
            
            tbRcvData.SafeInvoke(()=>
            {
                if (!string.IsNullOrEmpty(tbRcvData.Text))
                    text = Environment.NewLine + text; 
                tbRcvData.AppendText(text);
            });
        }

        private void btnTcpSvrRcvClear_Click(object sender, EventArgs e)
        {
            tbRcvData.Clear();
        }

        private void btnTcpSvrOpen_Click(object sender, EventArgs e)
        {
            try
            {
                _tcpServer.Open(tbPort.Text.ToInt32());
                btnTcpSvrOpen.Enabled=false;
                btnClose.Enabled = true;
            }catch(Exception ex)
            {
                MessageBoxE.Show(this, ex.Message);
            }
        }

        private void _tcpServer_OnConnect(TcpServerConnection connection)
        {
            var t = connection.LastVerifyTime.ToString("mm-dd HH:mm:ss");
            var client = connection.Socket.Client; 
            lbRcvClients.SafeInvoke(()=>lbRcvClients.Items.Add($"[{t}] {((System.Net.IPEndPoint)client.RemoteEndPoint).Address.ToString()}"));
        }
 

        private void btnClose_Click(object sender, EventArgs e)
        {
            _tcpServer.Close();
            btnClose.Enabled = false;
            btnTcpSvrOpen.Enabled = true;
            lbRcvClients.Items.Clear();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var data = ByteConverter.ToSocketBytes(tbSendData.Text, ckRcvHex.Checked);
            _tcpServer.Send(data);
        }
    }
}
