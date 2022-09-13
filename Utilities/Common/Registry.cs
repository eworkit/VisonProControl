using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Utilities
{
    public class Registry
    {
        public static void SetValue(string key, string keyValue)
        {
            RegistryKey IT9501 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\ITECHATE\\IT9501", true);
            if (IT9501 == null)
                IT9501 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\ITECHATE\\IT9501", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
            IT9501.SetValue(key, keyValue);
            IT9501.Close();
        }

        public static string GetValue(string key)
        {
            string str = "";
            try
            {
                RegistryKey res = Microsoft.Win32.Registry.LocalMachine;
                res = res.OpenSubKey("SOFTWARE\\ITECHATE\\IT9501");
                object v = res.GetValue(key);
                if (v != null)
                {
                    if (v is string)
                        str = (string)v;
                    else if (v is string[])
                        str = ((string[])v)[0];
                    else if (v != null)
                        str = v.ToString();
                } 
            }
            catch (Exception ex) { throw ex; }
            return str;
        }
    }

    public class ITS9000Registry
    {
        public static void SetValue(string key, string keyValue)
        {
            RegistryKey IT9501 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\ITECHATE\\ITS9000", true);
            if (IT9501 == null)
                IT9501 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\ITECHATE\\ITS9000", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
            IT9501.SetValue(key, keyValue);
            IT9501.Close();
        }

        public static string GetValue(string key)
        {
            string str = "";
            try
            {
                RegistryKey res = Microsoft.Win32.Registry.LocalMachine;
                res = res.OpenSubKey("SOFTWARE\\ITECHATE\\ITS9000");
                object v = res.GetValue(key);
                if (v != null)
                {
                    if (v is string)
                        str = (string)v;
                    else if (v is string[])
                        str = ((string[])v)[0];
                    else if (v != null)
                        str = v.ToString();
                }
                res.Close();
            }
            catch (Exception ex) { throw ex; }
            return str;
        }
    }

    public class ITS5000Registry
    {
        public static void SetValue(string key, string keyValue)
        {
            RegistryKey IT9501 = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\ITECHATE\\ITS5000", true);
            if (IT9501 == null)
                IT9501 = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\ITECHATE\\ITS5000", RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
            IT9501.SetValue(key, keyValue);
            IT9501.Close(); 
        }

        public static string GetValue(string key)
        {
            string str = "";
            try
            {
                RegistryKey res = Microsoft.Win32.Registry.LocalMachine;
                res = res.OpenSubKey("SOFTWARE\\ITECHATE\\ITS5000");
                object v = res.GetValue(key);
                if (v != null)
                {
                    if (v is string)
                        str = (string)v;
                    else if (v is string[])
                        str = ((string[])v)[0];
                    else if (v != null)
                        str = v.ToString();
                }
                res.Close();
            }
            catch (Exception ex) { throw ex; }
            return str;
        }
    }
}
