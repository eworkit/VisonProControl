// IteUtils.AppConfig
using System;
using System.IO;
using System.Reflection;
using System.Xml.Linq; 
using Utilities.Data;
using Utilities.ExMethod;
using Utilities.IO;

namespace Utilities
{
    public class MyAppConfig
    {
        public class TcpServer
        {
            public int Port { get; set; }

            public bool AutoStart { get; set; }

            public bool HexReceive { get; set; }

            public bool HexSend { get; set; }
        }

        public class TcpClient
        {
            public string Server { get; set; }

            public int Port { get; set; }

            public bool AutoConn { get; set; }

            public bool HexReceive { get; set; }

            public bool HexSend { get; set; }

            public bool AutoSend { get; set; }

            public int AutoSendInterval { get; set; }
        }

        public class SerialPort
        {
            public string Port { get; set; }

            public int DataBit { get; set; }

            public int StopBit { get; set; }

            public int BaundRate { get; set; }

            public int Parity { get; set; }
        }

        public static readonly string AppPath;

        public static readonly string DefaultDatabase;

        public static DBConnInfo DbInfo { get; set; }

        public static TcpServer TcpServerInfo { get; set; }

        public static TcpClient TcpClientInfo { get; set; }

        public static SerialPort SerialPortInfo { get; set; }

        public static event Action UpdateTree;

        public static void OnUpdateTree()
        {
            MyAppConfig.UpdateTree?.Invoke();
        }

        static MyAppConfig()
        {
            AppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DefaultDatabase = "VisionData";
            string cfgFile = Path.Combine(AppPath, "Config.inf");
            if (!File.Exists(cfgFile))
            {
                return;
            }
            try
            {
                XElement xRoot = XDocument.Load(cfgFile).Element("Root");
                if (xRoot != null)
                {
                    XElement ele = xRoot.Element("Database");
                    if (ele != null)
                    {
                        DbInfo = new DBConnInfo();
                        DbInfo.host = ele.Attribute("Host").SafeValue();
                        DbInfo.dbName = ele.Attribute("DbName").SafeValue();
                        DbInfo.port = ele.Attribute("Port").SafeValue();
                        DbInfo.user = ele.Attribute("User").SafeValue();
                        DbInfo.pwd = ele.Attribute("Pwd").SafeValue();
                    }
                    ele = xRoot.Element("TcpServer");
                    if (ele != null)
                    {
                        TcpServerInfo = new TcpServer();
                        TcpServerInfo.Port = ele.Attribute("Port").SafeValue().ToInt32(1024);
                        TcpServerInfo.HexReceive = ele.Attribute("HexReceive").SafeValue() == "1";
                        TcpServerInfo.HexSend = ele.Attribute("HexSend").SafeValue() == "1";
                        TcpServerInfo.AutoStart = ele.Attribute("AutoStart").SafeValue() == "1";
                    }
                    ele = xRoot.Element("TcpClient");
                    if (ele != null)
                    {
                        TcpClientInfo = new TcpClient();
                        TcpClientInfo.Server = ele.Attribute("Server").SafeValue();
                        TcpClientInfo.Port = ele.Attribute("Port").SafeValue().ToInt32(1024);
                        TcpClientInfo.HexReceive = ele.Attribute("HexReceive").SafeValue() == "1";
                        TcpClientInfo.HexSend = ele.Attribute("HexSend").SafeValue() == "1";
                        TcpClientInfo.AutoConn = ele.Attribute("AutoConn").SafeValue() == "1";
                    }
                    ele = xRoot.Element("SerialPort");
                    if (ele != null)
                    {
                        SerialPortInfo = new SerialPort();
                        SerialPortInfo.Port = ele.Attribute("Port").SafeValue();
                        SerialPortInfo.DataBit = ele.Attribute("DataBit").SafeValue().ToInt32(8);
                        SerialPortInfo.StopBit = ele.Attribute("StopBit").SafeValue().ToInt32(1);
                        SerialPortInfo.Parity = ele.Attribute("Parity").SafeValue().ToInt32();
                        SerialPortInfo.BaundRate = ele.Attribute("BaundRate").SafeValue().ToInt32(9600);
                    }
                }
            }
            catch
            {
            }
        }

        public static void Save()
        {
            string cfgFile = Path.Combine(AppPath, "Config.inf");
            XDocument xdoc1 = (File.Exists(cfgFile) ? XDocument.Load(cfgFile) : new XDocument());
            XElement xRoot = xdoc1.ElementX("Root");
            if (DbInfo != null)
            {
                XElement xElement = xRoot.ElementX("Database");
                xElement.SetAttributeValue("Host", DbInfo.host);
                xElement.SetAttributeValue("DbName", DbInfo.dbName);
                xElement.SetAttributeValue("Port", DbInfo.port);
                xElement.SetAttributeValue("User", DbInfo.user);
                xElement.SetAttributeValue("Pwd", DbInfo.pwd);
            }
            if (TcpServerInfo != null)
            {
                XElement xElement2 = xRoot.ElementX("TcpServer");
                xElement2.SetAttributeValue("Port", TcpServerInfo.Port);
                xElement2.SetAttributeValue("HexReceive", TcpServerInfo.HexReceive);
                xElement2.SetAttributeValue("HexSend", TcpServerInfo.HexSend);
                xElement2.SetAttributeValue("AutoStart", TcpServerInfo.AutoStart);
            }
            if (TcpClientInfo != null)
            {
                XElement xElement3 = xRoot.ElementX("TcpServer");
                xElement3.SetAttributeValue("Server", TcpClientInfo.Server);
                xElement3.SetAttributeValue("Port", TcpClientInfo.Port);
                xElement3.SetAttributeValue("HexReceive", TcpClientInfo.HexReceive);
                xElement3.SetAttributeValue("HexSend", TcpClientInfo.HexSend);
                xElement3.SetAttributeValue("AutoConn", TcpClientInfo.AutoConn);
            }
            if (SerialPortInfo != null)
            {
                XElement xElement4 = xRoot.ElementX("SerialPort");
                xElement4.SetAttributeValue("Port", SerialPortInfo.Port);
                xElement4.SetAttributeValue("BaundRate", SerialPortInfo.BaundRate);
                xElement4.SetAttributeValue("DataBit", SerialPortInfo.DataBit);
                xElement4.SetAttributeValue("StopBit", SerialPortInfo.StopBit);
                xElement4.SetAttributeValue("Parity", SerialPortInfo.Parity);
            }
            xdoc1.Save(cfgFile);
        }
    }
}