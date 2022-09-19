using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using Sunny.UI;
using Utilities.Net;
using Utilities.UI;
using Utilities.UI.ExMethod;

namespace VisionControl
{
    public partial class UCSerialPort : UserControl
    {
        SerialPort _SerialPort = new SerialPort();
        public UCSerialPort()
        {
            InitializeComponent();
            cbDataBit.SetType(TextInputType.Int, false);
            cbBaudRate.SetType(TextInputType.Int, false);   
            cbDataBit.SetType(TextInputType.Int,false);
            _SerialPort.DataReceived += _SerialPort_DataReceived;
            btnClose.Enabled = false;
            cbParity.Items.Clear();
            foreach (var item in Enum.GetValues(typeof(Parity)))
            {
                cbParity.Items.Add(item.ToString());
            }
            cbParity.SelectedIndex = 0;
            var cfg = MyAppConfig.SerialPortInfo;
            if(cfg!=null)
            {
                tbPort.Text = cfg.Port;
                cbDataBit.Text = cfg.DataBit.ToString();
                cbStopBit.Text = cfg.StopBit.ToString();
                cbBaudRate.Text = cfg.BaundRate.ToString();
                cbParity.SelectedIndex = cfg.Parity;
            }
        }
        void SetUIStat()
        {
            bool open = _SerialPort.IsOpen;
            btnOpen.Enabled = !open;
            btnClose.Enabled = open;
        }
        private List<byte> buffer = new List<byte>(1048576);
        private void _SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int n = _SerialPort.BytesToRead;//待读字节个数
            byte[] buf = new byte[n];//创建n个字节的缓存
            byte[] ReceiveBytes = new byte[9];
            _SerialPort.Read(buf, 0, n);//读到在数据存储到buf
            tbRcvData.SafeInvoke(()=> tbRcvData.AppendText(ByteConverter.ToSocketString(buf, ckRcvHex.Checked)));
                                //缓存数据
             buffer.AddRange(buf);//不断地将接收到的数据加入到buffer链表中
            while (buffer.Count >= 6) //至少包含帧头（1字节）、数据（3字节）、帧尾（1字节）、效验位（1字节）；
            {
                if (buffer[0] == 0x02) //到帧头  02H
                {
                    if (buffer.Count < 6) //数据区尚未接收完整，
                    {
                        break;//跳出接收函数后之后继续接收数据
                    }
                    //得到完整的数据，复制到ReceiveBytes中进行校验
                    buffer.CopyTo(0, ReceiveBytes, 0, 6);//复制数据
                    byte check; //开始校验
                    check = 0x03;//帧尾
                    if (check != ReceiveBytes[4]) //帧尾验证位失败  
                    {
                        buffer.RemoveRange(0, 6);//从链表中移除接收到的校验失败的数据，
                        continue;                //继续执行while循环程序,
                    }

                    buffer.RemoveRange(0, 6);
                    string strRcv = null;
                    for (int i = 1; i < 4; i++) //窗体显示
                    {
                        strRcv += ReceiveBytes[i].ToString("X2");  //16进制显示
                    }
                  //tbRcvData.AppendText(strRcv);


                }
                else //帧头不正确时，清除
                {
                    buffer.RemoveAt(0);//清除第一个字节，继续检测下一个。
                }
            }
         }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            _SerialPort.PortName = tbPort.Text;
            _SerialPort.DataBits = cbDataBit.Text.ToInt();
            _SerialPort.BaudRate = cbBaudRate.Text.ToInt();
            _SerialPort.Parity = (Parity)cbParity.SelectedIndex;
            _SerialPort.StopBits = GetStopBit(cbStopBit.Text.ToInt());
            try
            {
                _SerialPort.Open();
                SetUIStat();
            }
            catch (Exception ex)
            {
                MessageBoxE.Show(this, ex.Message);
            }
        }

        StopBits GetStopBit(int bit)
        {
            if (bit == 1)
                return StopBits.One;
            if (bit == 1.5)
                return StopBits.OnePointFive;
            if (bit == 2)
                return StopBits.Two;
            return StopBits.None;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (ckSentHex.Checked)
                {
                    var data = ByteConverter.ToSocketBytes(tbSentData.Text, true);
                    _SerialPort.Write(data, 0, data.Length);
                }
                else
                    _SerialPort.Write(tbSentData.Text);
            }
            catch (Exception ex)
            {
                MessageBoxE.Show(this, ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try 
            { 
                _SerialPort.Close();
                SetUIStat();
            }
            catch (IOException ex)
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
