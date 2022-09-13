using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

namespace Utilities.IO
{
    public class IniFile
    {
        private string _fileName;

        public IniFile(string filename)
        {
            _fileName = filename;
        
        }
        public bool Create(string filename)
        {
            if (filename.Trim() == "")
                return (false);

            _fileName = filename;

            return (true);
        }
        public string GetString(string section, string key, string def="")
        {
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(section, key, def, temp, 1024, _fileName);
            return temp.ToString();
        }
        public ArrayList GetIniSectionValue(string section)
        {
            byte[] buffer = new byte[5120];
            int rel = GetPrivateProfileSection(section, buffer, buffer.GetUpperBound(0), this._fileName);

            int iCnt, iPos;
            ArrayList arrayList = new ArrayList();
            string tmp;
            if (rel > 0)
            {
                iCnt = 0; iPos = 0;
                for (iCnt = 0; iCnt < rel; iCnt++)
                {
                    if (buffer[iCnt] == 0x00)
                    {
                        tmp = System.Text.ASCIIEncoding.Default.GetString(buffer, iPos, iCnt - iPos).Trim();
                        iPos = iCnt + 1;
                        if (tmp != "")
                            arrayList.Add(tmp);
                    }
                }
            }
            return arrayList;
        }
        public void WriteString(string section, string key, string strVal)
        {
            WritePrivateProfileString(section, key, strVal, _fileName);
        }
        //删除键
        //参数：section  文件节
        //      key      键值
        public void DelKey(string section, string key)
        {
            WritePrivateProfileString(section, key, null, _fileName);
        }

        //删除节
        //参数：section  文件节
        //      key      键值
        public void DelSection(string section)
        {
            WritePrivateProfileString(section, null, null, _fileName);
        }

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileInt(
           string lpAppName,
           string lpKeyName,
           int nDefault,
           string lpFileName
           );
        [DllImport("kernel32"/*, EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi*/)]
        private static extern int GetPrivateProfileString(
           string lpAppName,
           string lpKeyName,
           string lpDefault,
           StringBuilder lpReturnedString,
           int nSize,
           string lpFileName
           );
    [DllImport("kernel32"/*, EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi*/)]
        private static extern bool WritePrivateProfileString(
           string lpAppName,
           string lpKeyName,
           string lpString,
           string lpFileName
           );


    //获取指定Section的key和value        
    [DllImport("Kernel32.dll")]
    private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string lpFileName); 
    }
}
