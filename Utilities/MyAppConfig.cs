// IteUtils.AppConfig
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Xml.Linq; 
using Utilities.Data;
using Utilities.ExMethod;
using Utilities.IO;
using Utilities.Net;

namespace Utilities
{
    public enum AccessLevel {
        [Description("操作员")]
        Operator, 
        Supervisor,
        [Description("管理员")]
        Administrator }
    [Serializable]
    public class LoginUser : ICloneable
    {
        public string Password { get; set; }
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
            public string SendText { get; set; }
            public bool AutoSend { get; set; }
            public int AutoSendInterval { get; set; }
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
            public string SendText { get; set; }
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
        private static bool autoRunMode = true;
        private static string lastRunFile;
        private static bool autoOpenFile = false;
        private static bool autoStart;

        public static bool AutoRunMode
        {
            get
            {
                return autoRunMode;
            }
            set
            {
                autoRunMode = value;
                SaveElement("Run", x => x.SetAttributeValue("Manual", value ? 1 : 0));
            }
        }
        public static bool AutoOpenFile
        {
            get
            {
                return autoOpenFile;
            }
            set
            {
                autoOpenFile = value;
                SaveElement("Run", x => x.SetAttributeValue("AutoOpen", value ? 1 : 0));
            }
        }
        public static string LastRunFile
        {
            get
            {
                return lastRunFile;
            }
            set
            {
                lastRunFile = value;
                SaveElement("Run", x => x.SetAttributeValue("LastRunFile", value));
            }
        }
        public static bool AutoStart
        {
            get
            {
                return autoStart;
            }
            set
            {
                autoStart = value;
                SaveElement("Sys", x => x.SetAttributeValue("AutoStart", value ? 1 : 0));
            }
        }
        public static LoginUser User { get; set; }

        public static LoginUser GetUser(AccessLevel level)
        {
            var root = GetRoot();
            if (root == null)
                return null;
            var user = new LoginUser();
            var eles = root.Elements("User");
            foreach (var e in eles)
            {
                if ((int)level == e.Attribute("Level").SafeValue().ToInt32())
                {
                    var u = new LoginUser();
                    u.Level = user.Level;
                    var pwd = e.Attribute("Pwd").SafeValue();
                    if (!string.IsNullOrEmpty(pwd))
                    {
                        u.Password = Security.DESEncrypt.Decode(pwd);
                    }
                    return u;
                }
            }
            return null;
        }

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
            }
        }
        static MyAppConfig()
        {
            AppPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            DefaultDatabase = "VisionData";

            XElement xRoot = GetRoot();
            if (xRoot != null)
            {
                var eles = xRoot.Elements("User");
                var ele = xRoot.Element("Login");
                if (ele != null)
                {
                    AutoLogin = ele.Attribute("Auto").SafeValue() == "1";

                    var user = new LoginUser
                    {
                        Level = (AccessLevel)ele.Attribute("Level").SafeValue().ToInt32(),
                        Password = Security.DESEncrypt.Decode(ele.Attribute("Pwd").SafeValue()),
                    };
                    if (AutoLogin)
                    {
                        if (CheckPwd(user))
                            User = user;
                    }
                }
                ele = xRoot.Element("Database");
                if (ele != null)
                {
                    DbInfo = new DBConnInfo();
                    DbInfo.host = ele.Attribute("Host").SafeValue();
                    DbInfo.dbName = ele.Attribute("DbName").SafeValue();
                    DbInfo.port = ele.Attribute("Port").SafeValue();
                    DbInfo.user = ele.Attribute("User").SafeValue();
                    DbInfo.pwd = Security.DESEncrypt.Decode(ele.Attribute("Pwd").SafeValue());
                }
                ele = xRoot.Element("TcpServer");
                if (ele != null)
                {
                    TcpServerInfo = new TcpServer();
                    TcpServerInfo.Port = ele.Attribute("Port").SafeValue().ToInt32(1024);
                    TcpServerInfo.HexReceive = ele.Attribute("HexReceive").SafeValue() == "1";
                    TcpServerInfo.HexSend = ele.Attribute("HexSend").SafeValue() == "1";
                    TcpServerInfo.AutoStart = ele.Attribute("AutoStart").SafeValue() == "1";
                    TcpServerInfo.AutoSend = ele.Attribute("AutoSend").SafeValue() == "1";
                    TcpServerInfo.AutoSendInterval = ele.Attribute("SendInterval").SafeValue().ToInt32();
                    TcpServerInfo.SendText = ele.Attribute("SendData").SafeValue();
                }
                ele = xRoot.Element("TcpClient");
                if (ele != null)
                {
                    TcpClientInfo = new TcpClient();
                    TcpClientInfo.Server = ele.Attribute("Server").SafeValue();
                    TcpClientInfo.Port = ele.Attribute("Port").SafeValue().ToInt32(1024);
                    TcpClientInfo.HexReceive = ele.Attribute("HexReceive").SafeValue() == "1";
                    TcpClientInfo.HexSend = ele.Attribute("HexSend").SafeValue() == "1";
                    TcpServerInfo.AutoSendInterval = ele.Attribute("SendInterval").SafeValue().ToInt32();
                    TcpClientInfo.AutoConn = ele.Attribute("AutoConn").SafeValue() == "1";
                    TcpClientInfo.SendText = ele.Attribute("SendData").SafeValue();
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
                ele = xRoot.Element("Run");
                if (ele != null)
                {
                    autoRunMode = ele.Attribute("Manual").SafeValue() != "1";
                    autoOpenFile = ele.Attribute("AutoOpen").SafeValue() == "1";
                    lastRunFile = ele.Attribute("LastRunFile").SafeValue();
                }
                ele = xRoot.Element("Sys");
                if(ele != null)
                {
                    autoStart = ele.Attribute("AutoStart").SafeValue() == "1";
                }
            }
        }
        static XElement GetRoot()
        {
            string cfgFile = Path.Combine(AppPath, "Config.inf");
            if (!File.Exists(cfgFile))
            {
                return null;
            }
            try
            {
                return XDocument.Load(cfgFile).Element("Root");
            }
            catch { }
            return null;
        }
        static void SaveElement(string eleName, Action<XElement> save, Predicate<XElement> p = null)
        {
            var cfgFile = Path.Combine(AppPath, "Config.inf");
            var xdoc1 = (File.Exists(cfgFile) ? XDocument.Load(cfgFile) : new XDocument());
            var xRoot = xdoc1.ElementX("Root");
            var xElement = xRoot.ElementX(eleName, p);
            save(xElement);
            xdoc1.Save(cfgFile);
        }

        public static void Save(LoginUser user)
        {
            if (user != null)
            {
                SaveElement("User", x =>
                {
                    x.SetAttributeValue("Pwd", Security.DESEncrypt.Encode(user.Password));
                    x.SetAttributeValue("Level", (int)user.Level);
                },
                x => x.Attribute("Level").SafeValue().ToInt32() != (int)user.Level);
                User = user;
            }
        }
        public static void SaveLoginSet(LoginUser currentUser, bool autoLogin)
        {
            SaveElement("Login", x =>
            {
                if (currentUser == null)
                {
                    x.Remove();
                    return;
                }
                x.SetAttributeValue("Auto", autoLogin ? 1 : 0);
                x.SetAttributeValue("Level", (int)currentUser.Level);
                if (!string.IsNullOrEmpty(currentUser.Password))
                    x.SetAttributeValue("Pwd", Security.DESEncrypt.Encode(currentUser.Password));
            });
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
                        x.SetAttributeValue("Pwd", Security.DESEncrypt.Encode(db.pwd));
                    }
                });
                DbInfo = db;
            }
        }
        public static void Save(TcpServer info)
        {

            SaveElement("TcpServer", x =>
            {
                x.SetAttributeValue("Port", info.Port);
                x.SetAttributeValue("HexReceive", info.HexReceive ? 1 : 0);
                x.SetAttributeValue("HexSend", info.HexSend ? 1 : 0);
                x.SetAttributeValue("AutoStart", info.AutoStart ? 1 : 0);
                x.SetAttributeValue("SendData", info.SendText);
                x.SetAttributeValue("SendInterval", info.AutoSendInterval);
            });
        }
        public static void Save(TcpClient info)
        {

            SaveElement("TcpClient", x =>
            {
                x.SetAttributeValue("Port", info.Port);
                x.SetAttributeValue("Server", info.Server);
                x.SetAttributeValue("HexReceive", info.HexReceive ? 1 : 0);
                x.SetAttributeValue("HexSend", info.HexSend ? 1 : 0);
                x.SetAttributeValue("AutoConn", info.AutoConn ? 1 : 0);
                x.SetAttributeValue("SendData", info.SendText);
                x.SetAttributeValue("SendInterval", info.AutoSendInterval);
            });
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
                XElement xElement3 = xRoot.ElementX("TcpClient");
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
        static bool CheckPwd(LoginUser user)
        {
            return (GetUser(user.Level)?.Password ?? string.Empty) == (user.Password ?? string.Empty);

        }
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}