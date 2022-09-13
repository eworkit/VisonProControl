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

        }

        private void TcpClient_OnReceive(object sender, TcpClientEventArgs e)
        {
            string text = ByteConverter.ToSocketString(e.Read, ckRcvHex.Checked);
            if (string.IsNullOrEmpty(tbRcvData.Text))
                text = Environment.NewLine + text;
            tbRcvData.SafeInvoke(()=> tbRcvData.AppendText(text));
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                tcpClient.BeginConnect(tbServer.Text, tbPort.Text.ToInt());
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDisCon_Click(object sender, EventArgs e)
        {
            tcpClient.Stop();
        }

        private void btnSend_Click(object sender, EventArgs e)
        { 
            if(string.IsNullOrEmpty( tbSentData.Text))
            {
                MessageBox.Show("请输入要发送的数据");
            }

            var data = ByteConverter.ToSocketBytes(tbSentData.Text, ckSentHex.Checked);
            tcpClient.Send(data);
        }
    }
}
