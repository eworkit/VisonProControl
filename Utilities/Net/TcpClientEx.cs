using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Utilities.ExMethod;

namespace Utilities.Net
{
    public class TcpClientEx
    {

        public TcpClient client { get; private set; }
        NetworkStream netstream;
        public  string Server { get; private set; }
        public  int Port { get; private set; }
      // public   System.Net.IPEndPoint IPEndPoint { get; private set; }
        public event EventHandler<TcpClientEventArgs> OnReceive;
        public event EventHandler LostedConnect;
        public TcpClientEx()
        {
            client = new TcpClient();
        }

        public void Start(string strServerIP, int serverPort)
        {          
            try { Stop(); }
            catch { }
            client = new TcpClient(strServerIP, serverPort);
            netstream = client.GetStream();
           // IPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(strServerIP), serverPort);
            this.Server = strServerIP;
            this.Port = serverPort;
        }

       
        public void Listen()
        {
            byte[] buffer = new byte[100 * 1024];
            while (client.Connected)
            {
                Array.Clear(buffer, 0, buffer.Length);
                int n = Read(ref buffer);
                if (OnReceive != null)
                    OnReceive(this, new TcpClientEventArgs(buffer));
            }
        }
        ManualResetEvent connectDone = new ManualResetEvent(false);
        /// <summary>
        /// 异步连接
        /// </summary>
        public void BeginConnect(string server, int port)
        {
            try { Stop(); }
            catch { }
            //IPEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port);
            this.Server = server;
            this.Port = port;
            client = new TcpClient();
            client.ReceiveTimeout = 10;
            connectDone.Reset();
           Exception sockExc=null;
            //  client.BeginConnect(ip, port, new AsyncCallback(ConnectCallback), client);
            client.BeginConnect(server, port, new AsyncCallback((ar) =>
            {
             //   connectDone.Set();
                TcpClient t = (TcpClient)ar.AsyncState;
                try
                {
                    if(t.Client !=null)
                    {
                        if (t.Connected)
                        {
                            t.EndConnect(ar);
                        }
                        else
                        {
                            t.EndConnect(ar);
                        }
                    }

                }
                catch (SocketException se)
                {
                    sockExc = se; 
                }
               
                catch (Exception ex)
                {
                    sockExc = ex;
                }
                connectDone.Set();
            }), client);
            connectDone.WaitOne();
            if (sockExc != null)
            {
                DebugLog.WriteLine("TcpClientEx:BeginConnect(): " + sockExc.Message);
                throw sockExc;
            }
            if ((client != null) && (client.Connected))
            {
                netstream = client.GetStream();
                asyncread(client);
            }
//             else
//                 return false;
//             return true;
        }
        public void OnLostConnection(object sender = null, Exception ex = default(Exception))
        {
            if (LostedConnect != null)
            {
                if (sender == null)
                    LostedConnect(this, new TcpClientEventArgs(null, ex));
                else
                    LostedConnect(sender, new TcpClientEventArgs(null, ex));
            }
        }
        /// <summary>
        /// 异步连接的回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void ConnectCallback(IAsyncResult ar)
        {
            connectDone.Set();
            TcpClient t = (TcpClient)ar.AsyncState;
            try
            {
                if (t.Connected)
                {
                    t.EndConnect(ar);
                }
                else
                { 
                    t.EndConnect(ar);
                }

            }
            catch (SocketException se)
            {
                 
            }
        }

        /// <summary>
        /// 异步读TCP数据
        /// </summary>
        /// <param name="sock"></param>
        private void asyncread(TcpClient sock)
        {
            StateObject state = new StateObject();
            state.client = sock;
            NetworkStream stream = sock.GetStream();
            if (stream.CanRead)
            {
                try
                {
                    IAsyncResult ar = stream.BeginRead(state.buffer, 0, StateObject.BufferSize,
                            new AsyncCallback(TCPReadCallBack), state);
                }
                catch (Exception e)
                {
                    DebugLog.WriteLine("TcpClientEx:asyncread():"+e.Message);
                    if (OnReceive != null)
                        OnReceive(this, new TcpClientEventArgs(null, e));
                }
            }
        }

        /// <summary>
        /// TCP读数据的回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void TCPReadCallBack(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            //主动断开时

            if (state ==null || state.client == null || !state.client.Connected)
                return;
            int numberOfBytesRead = 0;
            NetworkStream mas = state.client.GetStream();
            EventHandler Close = (s, e) => {
                try
                {
                    mas.Close();
                    state.client.Close();
                }
                catch { }
                mas = null;
                state = null;
                if (LostedConnect != null)
                    LostedConnect(s,e);
            };
            string type = null;
            try
            {
                numberOfBytesRead = mas.EndRead(ar);
                state.totalBytesRead += numberOfBytesRead;
            }
            catch (Exception ex)
            {
                Close(this, new TcpClientEventArgs(null, ex));
                return;
            }
            if (numberOfBytesRead > 0)
            {
                byte[] dd = new byte[numberOfBytesRead];
               // System.Diagnostics.Debug.WriteLine("Read:" + numberOfBytesRead);
                Array.Copy(state.buffer, 0, dd, 0, numberOfBytesRead);
//                 System.Diagnostics.Debug.Indent();
//                 System.Diagnostics.Debug.WriteLine(dd.Select(p => p.ToString("X").PadLeft(2,'0')).Merge(' '), "it9320-TCPRcv");
//                 System.Diagnostics.Debug.Unindent();
         
                if (OnReceive != null)
                    OnReceive(this, new TcpClientEventArgs(dd));
                try
                {
                    if(state.client.Client.Connected)
                    mas.BeginRead(state.buffer, 0, StateObject.BufferSize,
                        new AsyncCallback(TCPReadCallBack), state);
                }
                catch (Exception ex2)
                {
                    DebugLog.WriteLine("TCPReadCallBack():try mas.BeginRead:" + ex2.Message);
                    if (OnReceive != null)
                        OnReceive(this, new TcpClientEventArgs(null, ex2));
                }
            }
            else
            {
                Close(this, EventArgs.Empty);
            }
        }
      

        public string CalcFileHash(string FilePath)
        {
            return TcpHelper.CalcFileHash(FilePath);
        }

        public bool SendFile(string filePath)
        {
            return TcpHelper.SendFile(filePath, netstream);
        }


        public bool ReceiveFile(string filePath)
        {
            return TcpHelper.ReceiveFile(filePath, netstream);
        }

        public void Send(byte[] data)
        { 
            netstream.Write(data, 0, data.Length);
        }
        public bool SendMessage(string message)
        {
            return TcpHelper.SendMessage(message, netstream);
        }

        public string ReadMessage()
        {
            return TcpHelper.ReadMessage(netstream);
        }
        public int Read(ref byte[] buffer)
        {
            return TcpHelper.Read(netstream, ref buffer);
        }
        public void Stop()
        {
            if (client != null)
            {
                if (client.Client.Connected)
                    client.Client.Disconnect(false);
                client.Close();
            }
            if (netstream != null)
            {
                netstream.Close(3000);
            }
        }
        #region IDisposable 成员

        public void Dispose()
        {
            if (netstream != null)
            {
                netstream.Close();
            }

            if (client != null)
            {
                client.Close();
            }
        }

        #endregion
    }

    internal class StateObject
    {
        public TcpClient client = null;
        public int totalBytesRead = 0;
        public const int BufferSize = 1024 * 1024;
        public string readType = null;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder messageBuffer = new StringBuilder();
    }

    public class TcpClientEventArgs : EventArgs
    {
        public string ReadStr { get { return Encoding.Default.GetString(Read).Replace("\0", ""); } }
        public byte[] Read { get; private set; }
        public Exception Exception { get; private set; }
       
        public TcpClientEventArgs(byte[] read,Exception ex=null)
        {
            this.Read = read;
            this.Exception = ex;
        }

    }

}
