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
        }

        private void _tcpServer_OnDataAvailable(TcpServerConnection connection)
        {
            var data = connection.Read();
            string text = ByteConverter.ToSocketString(data, ckRcvHex.Checked); 
            if (string.IsNullOrEmpty(tbRcvData.Text))
                text = Environment.NewLine + text;
            tbRcvData.SafeInvoke(()=> tbRcvData.AppendText(text));
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
            }catch(Exception ex)
            {
                MessageBox.Show(this, ex.Message);
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
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var data = ByteConverter.ToSocketBytes(tbSendData.Text, ckRcvHex.Checked);
            _tcpServer.Send(data);
        }
    }
}
