// IteUtils.AppConfig
using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq; 
using Utilities.Data;
using Utilities.ExMethod;
using Utilities.IO;

namespace Utilities
{
    public enum AccessLevel { Operator, Supervisor, Administrator }
    [Serializable]
    public class LoginUser : ICloneable
    {
        public string PwdAdmin { get; set; }
        public string PwdOpera { get; set; }
        public string Password
        {
            get { return Level == AccessLevel.Administrator ? PwdAdmin : PwdOpera; }
            set
            {
                if (Level == AccessLevel.Administrator)
                    PwdAdmin = value;
                else
                    PwdOpera = value;
            }
        }
        public AccessLevel Level { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    [Serializable]
    public class MyAppConfig : ICloneable
    {
        public class TcpServer : ICloneable
        {
            public int Port { get; set; }

            public bool AutoStart { get; set; }

            public bool HexReceive { get; set; }

            public bool HexSend { get; set; }
            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        [Serializable]
        public class TcpClient : ICloneable
        {
            public string Server { get; set; }

            public int Port { get; set; }

            public bool AutoConn { get; set; }

            public bool HexReceive { get; set; }

            public bool HexSend { get; set; }

            public bool AutoSend { get; set; }

            public int AutoSendInterval { get; set; }
            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        [Serializable]
        public class SerialPort : ICloneable
        {
            public string Port { get; set; }

            public int DataBit { get; set; }

            public int StopBit { get; set; }

            public int BaundRate { get; set; }

            public int Parity { get; set; }
            public object Clone()
            {
                return MemberwiseClone();
            }
        }

        public static readonly string AppPath;

        public static readonly string DefaultDatabase;
        private static bool autoLogin = false;

        public static LoginUser User { get; set; }

        public static DBConnInfo DbInfo { get; set; }

        public static TcpServer TcpServerInfo { get; set; }

        public static TcpClient TcpClientInfo { get; set; }

        public static SerialPort SerialPortInfo { get; set; }
        public static bool AutoLogin
        {
            get
            {
                return autoLogin;
            }
            set
            {
                autoLogin = value;
                SaveElement("Login", x => x.SetAttributeValue("Auto", value ? 1 : 0));
            }
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
                    XElement ele = xRoot.Element("User");
                    if (ele != null)
                    {
                        User = new LoginUser();
                        var pwd = ele.Attribute("PwdAdmin").SafeValue();
                        if (!string.IsNullOrEmpty(pwd))
                        {
                            User.PwdAdmin = Security.DESEncrypt.Decode(pwd);
                        }
                        pwd = ele.Attribute("PwdOper").SafeValue();
                        if (!string.IsNullOrEmpty(pwd))
                        {
                            User.PwdOpera = Security.DESEncrypt.Decode(pwd);
                        }
                        User.Level = (AccessLevel)ele.Attribute("Level").SafeValue().ToInt32();
                    }
                    ele = xRoot.Element("Database");
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
                    ele = xRoot.Element("Login");
                    if (ele != null)
                    {
                        AutoLogin = ele.Attribute("Auto").SafeValue() == "1";
                    }
                }
            }
            catch
            {
            }
        }
        static void SaveElement(string eleName, Action<XElement> save)
        {
            string cfgFile = Path.Combine(AppPath, "Config.inf");
            XDocument xdoc1 = (File.Exists(cfgFile) ? XDocument.Load(cfgFile) : new XDocument());
            XElement xRoot = xdoc1.ElementX("Root");
            XElement xElement = xRoot.ElementX(eleName);
            save(xElement);
            xdoc1.Save(cfgFile);
        }
   
        public static void Save(LoginUser user)
        {
            if (user != null)
            {
                SaveElement( "User", x =>
                {
                    if (!string.IsNullOrEmpty(user.PwdAdmin))
                        x.SetAttributeValue("PwdAdmin", Security.DESEncrypt.Encode(user.PwdAdmin));
                    if (!string.IsNullOrEmpty(user.PwdOpera))
                        x.SetAttributeValue("PwdOper", Security.DESEncrypt.Encode(user.PwdOpera));
                    x.SetAttributeValue("Level", (int)user.Level);
                });
                User = user;
            }
        }
        public static void Save(DBConnInfo db)
        {
            if (db != null)
            {
                SaveElement("Database", x =>
                {
                    if (db != null)
                    {
                        x.SetAttributeValue("Host", db.host);
                        x.SetAttributeValue("DbName", db.dbName);
                        x.SetAttributeValue("Port", db.port);
                        x.SetAttributeValue("User", db.user);
                        x.SetAttributeValue("Pwd", db.pwd);
                    }
                });
                DbInfo = db;
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
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}