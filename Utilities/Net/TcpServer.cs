using System;
using System.Collections;
using System.Collections.Generic; 
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.AxHost;
using System.ComponentModel; 

namespace Utilities.Net
{
    public delegate void tcpServerConnectionChanged(TcpServerConnection connection);
    public delegate void tcpServerError(TcpServer server, Exception e);
    public class TcpServer
    {
        private List<TcpServerConnection> connections;
        private TcpListener listener;

        private Thread listenThread;
        private Thread sendThread;

        private bool m_isOpen;

        private int m_port;
        private int m_maxSendAttempts;
        private int m_idleTime;
        private int m_maxCallbackThreads;
        private int m_verifyConnectionInterval;
        private Encoding m_encoding;

        private SemaphoreSlim sem;
        private bool waiting;

        private int activeThreads;
        private object activeThreadsLock = new object();

        public event tcpServerConnectionChanged OnConnect = null;
        public event tcpServerConnectionChanged OnDataAvailable = null;
        public event tcpServerConnectionChanged OnLostConnect;
        public event tcpServerError OnError = null;

        public TcpServer()
        { 

            initialise();
        }
         

        private void initialise()
        {
            connections = new List<TcpServerConnection>();
            listener = null;

            listenThread = null;
            sendThread = null;

            m_port = -1;
            m_maxSendAttempts = 3;
            m_isOpen = false;
            m_idleTime = 50;
            m_maxCallbackThreads = 100;
            m_verifyConnectionInterval = 100;
            m_encoding = Encoding.ASCII;

            sem = new SemaphoreSlim(0);
            waiting = false;

            activeThreads = 0;
        }

        public int Port
        {
            get
            {
                return m_port;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }

                if (m_port == value)
                {
                    return;
                }

                if (m_isOpen)
                {
                    throw new Exception("Invalid attempt to change port while still open.\nPlease close port before changing.");
                }

                m_port = value;
                if (listener == null)
                {
                    //this should only be called the first time.
                    listener = new TcpListener(IPAddress.Any, m_port);
                }
                else
                {
                    listener.Server.Bind(new IPEndPoint(IPAddress.Any, m_port));
                }
            }
        }

        public int MaxSendAttempts
        {
            get
            {
                return m_maxSendAttempts;
            }
            set
            {
                m_maxSendAttempts = value;
            }
        }

        [Browsable(false)]
        public bool IsOpen
        {
            get
            {
                return m_isOpen;
            }
            set
            {
                if (m_isOpen == value)
                {
                    return;
                }

                if (value)
                {
                    Open(m_port);
                }
                else
                {
                    Close();
                }
            }
        }

        public List<TcpServerConnection> Connections
        {
            get
            {
                List<TcpServerConnection> rv = new List<TcpServerConnection>();
                rv.AddRange(connections);
                return rv;
            }
        }

        public int IdleTime
        {
            get
            {
                return m_idleTime;
            }
            set
            {
                m_idleTime = value;
            }
        }

        public int MaxCallbackThreads
        {
            get
            {
                return m_maxCallbackThreads;
            }
            set
            {
                m_maxCallbackThreads = value;
            }
        }

        public int VerifyConnectionInterval
        {
            get
            {
                return m_verifyConnectionInterval;
            }
            set
            {
                m_verifyConnectionInterval = value;
            }
        }

        public Encoding Encoding
        {
            get
            {
                return m_encoding;
            }
            set
            {
                Encoding oldEncoding = m_encoding;
                m_encoding = value;
                foreach (TcpServerConnection client in connections)
                {
                    if (client.Encoding == oldEncoding)
                    {
                        client.Encoding = m_encoding;
                    }
                }
            }
        }

        public void setEncoding(Encoding encoding, bool changeAllClients)
        {
            Encoding oldEncoding = m_encoding;
            m_encoding = encoding;
            if (changeAllClients)
            {
                foreach (TcpServerConnection client in connections)
                {
                    client.Encoding = m_encoding;
                }
            }
        }

        private void runListener()
        {
            while (m_isOpen && m_port >= 0)
            {
                try
                {
                    if (listener.Pending())
                    {
                        TcpClient socket = listener.AcceptTcpClient();
                        TcpServerConnection conn = new TcpServerConnection(socket, m_encoding);

                        if (OnConnect != null)
                        {
                            lock (activeThreadsLock)
                            {
                                activeThreads++;
                            }
                            conn.CallbackThread = new Thread(() =>
                            {
                                OnConnect(conn);
                            });
                            conn.CallbackThread.Start();
                        }

                        lock (connections)
                        {
                            connections.Add(conn);
                        }
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(m_idleTime);
                    }
                    for (int i= connections.Count-1;i>=0; i-- )
                    {
                        var conn = connections[i];
                        var client = conn.Socket;
                        if (((client.Client.Poll(1000, SelectMode.SelectRead) && (client.Available == 0)) || !client.Connected))
                        {
                            OnLostConnect?.Invoke(conn);
                            connections.RemoveAt(i);
                        }
                    }
                }
                catch (ThreadInterruptedException) { } //thread is interrupted when we quit
                catch (Exception e)
                {
                    if (m_isOpen && OnError != null)
                    {
                        OnError(this, e);
                    }
                }
            }
        }

        private void runSender()
        {
            while (m_isOpen && m_port >= 0)
            {
                try
                {
                    bool moreWork = false;
                    for (int i = 0; i < connections.Count; i++)
                    {
                        if (connections[i].CallbackThread != null)
                        {
                            try
                            {
                                connections[i].CallbackThread = null;
                                lock (activeThreadsLock)
                                {
                                    activeThreads--;
                                }
                            }
                            catch (Exception)
                            {
                                //an exception is thrown when setting thread and old thread hasn't terminated
                                //we don't need to handle the exception, it just prevents decrementing activeThreads
                            }
                        }

                        if (connections[i].CallbackThread != null) { }
                        else if (connections[i].connected() &&
                            (connections[i].LastVerifyTime.AddMilliseconds(m_verifyConnectionInterval) > DateTime.UtcNow ||
                             connections[i].verifyConnected()))
                        {
                            moreWork = moreWork || processConnection(connections[i]);
                        }
                        else
                        {
                            lock (connections)
                            {
                                connections.RemoveAt(i);
                                i--;
                            }
                        }
                    }

                    if (!moreWork)
                    {
                        System.Threading.Thread.Yield();
                        lock (sem)
                        {
                            foreach (TcpServerConnection conn in connections)
                            {
                                if (conn.hasMoreWork())
                                {
                                    moreWork = true;
                                    break;
                                }
                            }
                        }
                        if (!moreWork)
                        {
                            waiting = true;
                            sem.Wait(m_idleTime);
                            waiting = false;
                        }
                    }
                }
                catch (ThreadInterruptedException) { } //thread is interrupted when we quit
                catch (Exception e)
                {
                    if (m_isOpen && OnError != null)
                    {
                        OnError(this, e);
                    }
                }
            }
        }

        private bool processConnection(TcpServerConnection conn)
        {
            bool moreWork = false;
            if (conn.processOutgoing(m_maxSendAttempts))
            {
                moreWork = true;
            }

            if (OnDataAvailable != null && activeThreads < m_maxCallbackThreads && conn.Socket.Available > 0)
            {
                lock (activeThreadsLock)
                {
                    activeThreads++;
                }
                conn.CallbackThread = new Thread(() =>
                {
                    OnDataAvailable(conn);
                });
                conn.CallbackThread.Start();
                Thread.Yield();
            }
            return moreWork;
        }

        public void Open(int port)
        {
            m_port=port;
            lock (this)
            {
                if (m_isOpen)
                {
                    //already open, no work to do
                    return;
                }
                if (m_port < 0)
                {
                    throw new Exception("Invalid port");
                }

                try
                {
                    if (listener == null)
                    {
                        //this should only be called the first time.
                        listener = new TcpListener(IPAddress.Any, m_port);
                    }
                    listener.Start(5);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                m_isOpen = true;

                listenThread = new Thread(new ThreadStart(runListener));
                listenThread.Start();

                sendThread = new Thread(new ThreadStart(runSender));
                sendThread.Start();
            }
        }

        public void Close()
        {
            if (!m_isOpen)
            {
                return;
            }

            lock (this)
            {
                m_isOpen = false;
                foreach (TcpServerConnection conn in connections)
                {
                    conn.forceDisconnect();
                }
                try
                {
                    if (listenThread.IsAlive)
                    {
                        listenThread.Interrupt();

                        Thread.Yield();
                        if (listenThread.IsAlive)
                        {
                            listenThread.Abort();
                        }
                    }
                }
                catch (System.Security.SecurityException) { }
                try
                {
                    if (sendThread.IsAlive)
                    {
                        sendThread.Interrupt();

                        Thread.Yield();
                        if (sendThread.IsAlive)
                        {
                            sendThread.Abort();
                        }
                    }
                }
                catch (System.Security.SecurityException) { }
            }
            listener.Stop();

            lock (connections)
            {
                connections.Clear();
            }

            listenThread = null;
            sendThread = null;
            GC.Collect();
        }

        public void Send(byte[] data)
        {
            lock (sem)
            {
                foreach (TcpServerConnection conn in connections)
                {
                    conn.sendData(data);
                }
                Thread.Yield();
                if (waiting)
                {
                    sem.Release();
                    waiting = false;
                }
            }
        }
    }
    public class TcpServerConnection
    {
        private TcpClient m_socket;
        private List<byte[]> messagesToSend;
        private int attemptCount;

        private Thread m_thread = null;

        private DateTime m_lastVerifyTime;

        private Encoding m_encoding;

        public TcpServerConnection(TcpClient sock, Encoding encoding)
        {
            m_socket = sock;
            messagesToSend = new List<byte[]>();
            attemptCount = 0;

            m_lastVerifyTime = DateTime.UtcNow;
            m_encoding = encoding;
        }

        public bool connected()
        {
            try
            {
                return m_socket.Connected;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool verifyConnected()
        {
            //note: `Available` is checked before because it's faster,
            //`Available` is also checked after to prevent a race condition.
            bool connected = m_socket.Client.Available != 0 ||
                !m_socket.Client.Poll(1, SelectMode.SelectRead) ||
                m_socket.Client.Available != 0;
            m_lastVerifyTime = DateTime.UtcNow;
            return connected;
        }

        public bool processOutgoing(int maxSendAttempts)
        {
            lock (m_socket)
            {
                if (!m_socket.Connected)
                {
                    messagesToSend.Clear();
                    return false;
                }

                if (messagesToSend.Count == 0)
                {
                    return false;
                }

                NetworkStream stream = m_socket.GetStream();
                try
                {
                    stream.Write(messagesToSend[0], 0, messagesToSend[0].Length);

                    lock (messagesToSend)
                    {
                        messagesToSend.RemoveAt(0);
                    }
                    attemptCount = 0;
                }
                catch (System.IO.IOException)
                {
                    //occurs when there's an error writing to network
                    attemptCount++;
                    if (attemptCount >= maxSendAttempts)
                    {
                        //TODO log error

                        lock (messagesToSend)
                        {
                            messagesToSend.RemoveAt(0);
                        }
                        attemptCount = 0;
                    }
                }
                catch (ObjectDisposedException)
                {
                    //occurs when stream is closed
                    m_socket.Close();
                    return false;
                }
            }
            return messagesToSend.Count != 0;
        }

        public void sendData(byte[] data)
        { 
            lock (messagesToSend)
            {
                messagesToSend.Add(data);
            }
        }

        public void forceDisconnect()
        {
            lock (m_socket)
            {
                m_socket.Close();
            }
        }

        public bool hasMoreWork()
        {
            return messagesToSend.Count > 0 || (Socket.Available > 0 && canStartNewThread());
        }

        public byte[] Read()
        {
            return readStream(m_socket);
        }
        protected byte[] readStream(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            if (stream.DataAvailable)
            {
                byte[] data = new byte[client.Available];

                int bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(data, 0, data.Length);
                }
                catch (System.IO.IOException)
                {
                }

                if (bytesRead < data.Length)
                {
                    byte[] lastData = data;
                    data = new byte[bytesRead];
                    Array.ConstrainedCopy(lastData, 0, data, 0, bytesRead);
                }
                return data;
            }
            return null;
        }

        private bool canStartNewThread()
        {
            if (m_thread == null)
            {
                return true;
            }
            return (m_thread.ThreadState & (ThreadState.Aborted | ThreadState.Stopped)) != 0 &&
                   (m_thread.ThreadState & ThreadState.Unstarted) == 0;
        }

        public TcpClient Socket
        {
            get
            {
                return m_socket;
            }
            set
            {
                m_socket = value;
            }
        }

        public Thread CallbackThread
        {
            get
            {
                return m_thread;
            }
            set
            {
                if (!canStartNewThread())
                {
                    throw new Exception("Cannot override TcpServerConnection Callback Thread. The old thread is still running.");
                }
                m_thread = value;
            }
        }

        public DateTime LastVerifyTime
        {
            get
            {
                return m_lastVerifyTime;
            }
        }

        public Encoding Encoding
        {
            get
            {
                return m_encoding;
            }
            set
            {
                m_encoding = value;
            }
        }
    }
}