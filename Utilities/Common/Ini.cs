using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace Utilities
{
    public  class Ini
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32.dll",  EntryPoint="GetPrivateProfileStringW",CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public static bool WriteString(string sFile, string section, string key, string val)
        {
            if (WritePrivateProfileString(section, key, val, sFile) != 0)
                return true;
            return false;
        }

        public static bool ReadString(string sFile, string section, string key, out string val)
        {
            val = "";
            StringBuilder str = new StringBuilder();
            if (GetPrivateProfileString(section, key, "", str, 512, sFile) <= 512)
            {
                val = str.ToString();
                return true;
            }
            return false;
        }

        public static bool WriteGPrm(string key, string val)
        {
            string sGlobalFile = "Config\\GPrm_TempIni.ini";
            if (WritePrivateProfileString("MAIN", key, val, sGlobalFile) != 0)
                return true;
            return false;
        }

        public static bool ReadGPrm(string key, out string val)
        {
            string sGlobalFile = "Config\\GPrm_TempIni.ini";
            val = "";
            StringBuilder sb = new StringBuilder(512);
            if (GetPrivateProfileString("MAIN", key, "", sb, 512, sGlobalFile) <= 512)
            {
                val = sb.ToString();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 读ini文件中的键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool ReadIniPair(string sFile, string section, out string[] keys, out string[] vals)
        {
            List<string> listKey = new List<string>();
            List<string> listVal = new List<string>();
            try
            {
                StreamReader sr = new StreamReader(sFile);
                if (sr != null)
                {
                    string sec = string.Format("[{0}]", section);
                    while (sr.ReadLine() != sec) ;

                    while (!sr.EndOfStream)
                    {
                        string sTmp = sr.ReadLine();
                        if (sTmp.StartsWith("[") && sTmp.EndsWith("]"))
                        {
                            break;
                        }
                        string[] arr = sTmp.Split('=');
                        if (arr.Length == 2)
                        {
                            string sKey = arr[0].Trim();
                            string sVal = arr[1].Trim();
                            if (sKey != "")
                            {
                                listKey.Add(sKey);
                                listVal.Add(sVal);
                            }
                        }
                    }

                    keys = listKey.ToArray();
                    vals = listVal.ToArray();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ReadIniPair()!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            keys = listKey.ToArray();
            vals = listVal.ToArray();
            return false;
        }
    }
}
