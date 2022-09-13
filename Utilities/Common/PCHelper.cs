using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
using System.Management.Instrumentation;
using System.Xml;
using System.Windows.Forms;

namespace Utilities
{
    public class SystemHelper
    {
        public static string GetHardId()
        {
            String HDid = "";
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid = (string)mo.Properties["Model"].Value;
                // Response.Write ("硬盘序列号："+HDid.ToString ());  
            }
            return HDid;
        }

        private void GetInfo()
        {
            string cpuInfo = "";//cpu序列号  
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                //Response.Write ("cpu序列号："+cpuInfo.ToString ());  
            }

            //获取硬盘ID  
            String HDid;
            ManagementClass cimobject1 = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            foreach (ManagementObject mo in moc1)
            {
                HDid = (string)mo.Properties["Model"].Value;
                // Response.Write ("硬盘序列号："+HDid.ToString ());  
            }


            //获取网卡硬件地址  



            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc2 = mc.GetInstances();
            foreach (ManagementObject mo in moc2)
            {
                if ((bool)mo["IPEnabled"] == true)
                    // Response.Write("MAC address\t{0}"+mo["MacAddress"].ToString());  
                    mo.Dispose();
            }
        }

        /// <summary>
        /// 判断当前登录的用户是否属于系统管理员组
        /// </summary>
        /// <returns></returns>
        public static bool IsAdministrator()
        {
            AppDomain myDomain = System.Threading.Thread.GetDomain();
            myDomain.SetPrincipalPolicy(System.Security.Principal.PrincipalPolicy.WindowsPrincipal);
            var myPrincipal = (System.Security.Principal.WindowsPrincipal)System.Threading.Thread.CurrentPrincipal;
            return myPrincipal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }
        public static void StartWithAdmin(string[] args)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.FileName = Application.ExecutablePath;
            //设置启动动作,确保以管理员身份运行
            startInfo.Verb = "runas";
            if (args != null && args.Any())
                startInfo.Arguments = string.Concat(args.Select(c => c + " ")).TrimEnd(' ');
            System.Diagnostics.Process.Start(startInfo);
        }
        public static void StartWithAdmin(string file, string[] args)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.FileName = file;
            //设置启动动作,确保以管理员身份运行
            startInfo.Verb = "runas";
            if (args != null && args.Any())
                startInfo.Arguments = string.Concat(args.Select(c => c + " ")).TrimEnd(' ');
            System.Diagnostics.Process.Start(startInfo);
        }
        static System.Threading.Mutex runInstance;
        public static System.Diagnostics.Process CheckAppStarted()
        {
            bool runone;
            runInstance = new System.Threading.Mutex(true, "single_" + Application.ProductName, out runone);
            if (!runone)
            {
                var processes = System.Diagnostics.Process.GetProcesses();// System.Diagnostics.Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Application.ExecutablePath));// (Application.ProductName);

                int curId = System.Diagnostics.Process.GetCurrentProcess().Id;
                foreach (var p2 in processes)
                {                  
                    if (p2.Id != curId)
                    {
                        if (string.IsNullOrEmpty(p2.MainWindowTitle))
                            continue;
                        try
                        {
                            if (p2.MainModule.FileVersionInfo.ProductName == Application.ProductName)
                            {
                                if (!runone)
                                {
                                    return p2;
                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            return null;
        }
    }
}


namespace Utilities.IteLicense
{
    public class IteRegist
    { 
        /// <summary>
        /// 软件注册
        /// </summary>
        /// <param name="DeformatterData">注册码</param>
        /// <returns></returns>
        public static  bool Regist(string DeformatterData)
        {
            try
            {
                if(DeformatterData==getRNum())
                  return SignVerifyEnvelope.SignFile(DeformatterData);
                else
                {
                    MessageBox.Show("注册码验证失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception ex) { MessageBox.Show("注册码验证失败。"+ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            return false;
        }

        public static void CreateSomeXml(string FileName)
        {
            // Create a new XmlDocument object.
            XmlDocument document = new XmlDocument();

            // Create a new XmlNode object.
            XmlNode node = document.CreateNode(XmlNodeType.Element, "", "APP", "Sign");

            // Add some text to the node.
            node.InnerText = "signed.";

            // Append the node to the document.
            document.AppendChild(node);

            // Save the XML document to the file name specified.
            XmlTextWriter xmltw = new XmlTextWriter(FileName, new UTF8Encoding(false));
            document.WriteTo(xmltw);
            xmltw.Close();
        }
        /// <summary>
        /// 取得设备硬盘的卷标号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskVolumeSerialNumber()
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }

        /// <summary>
        /// 获得CPU的序列号
        /// </summary>
        /// <returns></returns>
        public static string getCpu()
        {
            string strCpu = null;
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                strCpu = myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return strCpu;
        }

        /// <summary>
        /// 生成机器码
        /// </summary>
        /// <returns></returns>
        public static string getMNum()
        {
            string strNum = getCpu() + GetDiskVolumeSerialNumber();//获得24位Cpu和硬盘序列号
            string strMNum = strNum.Substring(0, 24);//从生成的字符串中取出前24个字符做为机器码
            return strMNum;
        }
        public static int[] intCode = new int[127];//存储密钥
        public static int[] intNumber = new int[25];//存机器码的Ascii值
        public static char[] Charcode = new char[25];//存储机器码字
        public static void setIntCode()//给数组赋值小于10的数
        {
            for (int i = 1; i < intCode.Length; i++)
            {
                intCode[i] = i % 9;
            }
        }

        /// <summary>
        /// 生成注册码
        /// </summary>
        /// <returns></returns>
        public static string getRNum()
        {
            setIntCode();//初始化127位数组
            string MNum = getMNum();//获取注册码
            for (int i = 1; i < Charcode.Length; i++)//把机器码存入数组中
            {
                Charcode[i] = Convert.ToChar(MNum.Substring(i - 1, 1));
            }
            for (int j = 1; j < intNumber.Length; j++)//把字符的ASCII值存入一个整数组中。
            {
                intNumber[j] = intCode[Convert.ToInt32(Charcode[j])] + Convert.ToInt32(Charcode[j]);
            }
            string strAsciiName = "";//用于存储注册码
            for (int j = 1; j < intNumber.Length; j++)
            {
                if (intNumber[j] >= 48 && intNumber[j] <= 57)//判断字符ASCII值是否0－9之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 65 && intNumber[j] <= 90)//判断字符ASCII值是否A－Z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else if (intNumber[j] >= 97 && intNumber[j] <= 122)//判断字符ASCII值是否a－z之间
                {
                    strAsciiName += Convert.ToChar(intNumber[j]).ToString();
                }
                else//判断字符ASCII值不在以上范围内
                {
                    if (intNumber[j] > 122)//判断字符ASCII值是否大于z
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 10).ToString();
                    }
                    else
                    {
                        strAsciiName += Convert.ToChar(intNumber[j] - 9).ToString();
                    }
                }
            }
            return strAsciiName;//返回注册码
        }
    }
}
