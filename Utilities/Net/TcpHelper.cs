using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace Utilities.Net
{
    public class TcpHelper
    {
        private static readonly int _blockLength = 500 * 1024;

        /// <summary>
        /// 计算文件的hash值 
        /// </summary>
        internal static string CalcFileHash(string FilePath)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] hash;
            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096))
            {
                hash = md5.ComputeHash(fs);
            }
            return BitConverter.ToString(hash);
        }

        /// <summary>
        /// 发送文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal static bool SendFile(string filePath, NetworkStream stream)
        {
            FileStream fs = File.Open(filePath, FileMode.Open);
            int readLength = 0;
            byte[] data = new byte[_blockLength];

            //发送大小
            byte[] length = new byte[8];
            BitConverter.GetBytes(new FileInfo(filePath).Length).CopyTo(length, 0);
            stream.Write(length, 0, 8);

            //发送文件
            while ((readLength = fs.Read(data, 0, _blockLength)) > 0)
            {
                stream.Write(data, 0, readLength);
            }
            fs.Close();
            return true;
        }

        /// <summary>
        /// 接收文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal static bool ReceiveFile(string filePath, NetworkStream stream)
        {
            try
            {
                long count = GetSize(stream);
                if (count == 0)
                {
                    return false;
                }

                long index = 0;
                byte[] clientData = new byte[_blockLength];
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                string path = new FileInfo(filePath).Directory.FullName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                FileStream fs = File.Open(filePath, FileMode.OpenOrCreate);
                try
                {
                    //计算当前要读取的块的大小
                    int currentBlockLength = 0;
                    if (_blockLength < count - index)
                    {
                        currentBlockLength = _blockLength;
                    }
                    else
                    {
                        currentBlockLength = (int)(count - index);
                    }

                    int receivedBytesLen = stream.Read(clientData, 0, currentBlockLength);
                    index += receivedBytesLen;
                    fs.Write(clientData, 0, receivedBytesLen);

                    while (receivedBytesLen > 0 && index < count)
                    {
                        clientData = new byte[_blockLength];
                        receivedBytesLen = 0;

                        if (_blockLength < count - index)
                        {
                            currentBlockLength = _blockLength;
                        }
                        else
                        {
                            currentBlockLength = (int)(count - index);
                        }
                        receivedBytesLen = stream.Read(clientData, 0, currentBlockLength);
                        index += receivedBytesLen;
                        fs.Write(clientData, 0, receivedBytesLen);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal static bool SendMessage(string message, NetworkStream stream)
        {
            if (stream == null)
                return false;
            // byte[] data = Encoding.UTF8.GetBytes(message);
            //byte[] resultData = new byte[8 + data.Length];
            //BitConverter.GetBytes(data.Length).CopyTo(resultData, 0);
            //data.CopyTo(resultData, 8);
            // stream.Write(resultData, 0, resultData.Length);
            byte[] data = Encoding.Unicode.GetBytes(message);
            stream.Write(data, 0, data.Length);
            return true;
        }

        /// <summary>
        /// 读取消息
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        internal static string ReadMessage(NetworkStream stream)
        {
            string result = "";
            int messageLength = 0;

            byte[] resultbyte = new byte[500 * 1024];
            //读取数据大小
            int index = Read(stream, ref resultbyte);

            result = Encoding.UTF8.GetString(resultbyte, 0, index);
            return result;
        }
        internal static int Read(NetworkStream stream, ref byte[] buffer)
        {
            string result = "";
            int messageLength = 0;
            if (!stream.CanRead)
                return 0;
            // byte[] buffer = new byte[500 * 1024]; 
            //读取数据大小
            int index = 0;
            int count = GetSize(stream);
            if (count > buffer.Length)
                count = buffer.Length;
            byte[] data = new byte[count];
            while (index < count && (messageLength = stream.Read(data, 0, count - index)) != 0)
            {
                data.Take(messageLength).ToArray().CopyTo(buffer, index);
                index += messageLength;
            }
            return index;

       /*     if (myNetworkStream.CanRead)
            {
                byte[] myReadBuffer = new byte[1024];
                StringBuilder myCompleteMessage = new StringBuilder();
                int numberOfBytesRead = 0;

                // Incoming message may be larger than the buffer size.
                do
                {
                    numberOfBytesRead = myNetworkStream.Read(myReadBuffer, 0, myReadBuffer.Length);

                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));

                }
                while (myNetworkStream.DataAvailable);

                // Print out the received message to the console.
                Console.WriteLine("You received the following message : " +
                                             myCompleteMessage);
            }
            else
            {
                Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
            }
            */
        }
        /// <summary>
        /// 获取要读取的数据的大小
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private static int GetSize(NetworkStream stream)
        {
            int count = 0;
            byte[] countBytes = new byte[8];
            try
            {
                if (stream.Read(countBytes, 0, 8) == 8)
                {
                    count = BitConverter.ToInt32(countBytes, 0);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {

            }
            return count;
        }

        public static string GetInnerIP()
        {
            var addrs = System.Net.Dns.GetHostEntry(System.Environment.MachineName).AddressList;
            if (addrs.Length == 1)
                return addrs[0].ToString();
            if (addrs.Length > 1)
                foreach (var addr in addrs)
                {
                    string saddr = addr.ToString();
                    var arr = addr.ToString().Split(new[] { '.' });
                    if (!IpStrIsValidate(saddr))
                        continue;
                    return saddr;
                }
            return "127.0.0.1";
        }
        public static string GetOuterIP()
        {
            try
            {
                Uri uri = new Uri("http://city.ip138.com/ip2city.asp");
                System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uri);
                req.Method = "get";
                using (Stream s = req.GetResponse().GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(s))
                    {
                        char[] ch = { '[', ']' };
                        string str = reader.ReadToEnd();
                        System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(str, @"\[(?<IP>[0-9\.]*)\]");
                        return m.Value.Trim(ch);
                    }
                }
            }
            catch { return string.Empty; }
        }
        private static bool IpStrIsValidate(string ip)
        {
            var arr = ip.Split(new[] { '.' });
            if (arr.Length < 4)
                return false;
            foreach (var s in arr)
            {
                int n;
                if (int.TryParse(s, out n))
                {
                    if (n < 0 || n > 255)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IpIsValidate(string ip)
        {
             IPAddress iPAddress;
             if (IPAddress.TryParse(ip, out iPAddress))
             { 
                return IpStrIsValidate (iPAddress.ToString());
             }
             return false;
        }
        public  static IPHostEntry ParseIPAddress(string hostname)
        {
            IPHostEntry iPHostEntry = null;
            IPAddress iPAddress;
            if (IPAddress.TryParse(hostname, out iPAddress))
            {
                iPHostEntry = new IPHostEntry();
                iPHostEntry.AddressList = new IPAddress[1];
                iPHostEntry.AddressList[0] = iPAddress; 
            }
            return iPHostEntry;
        }

        public  static IPHostEntry GetHostEntry(string hostname)
        {
            IPHostEntry iPHostEntry = ParseIPAddress(hostname);
            if (iPHostEntry != null)
            {
                return iPHostEntry;
            }
            return Dns.GetHostEntry(hostname);
        }
    }
    public class TcpLibException : ApplicationException
    {
        public TcpLibException(string msg)
            : base(msg)
        {
        }
    }
}
