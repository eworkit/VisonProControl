using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Utilities
{
    public class DllInvoke
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private extern static IntPtr LoadLibrary(string sDllName);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "GetProcAddress", CallingConvention = CallingConvention.StdCall)]
        private extern static IntPtr GetProcAddressA(IntPtr hModole, string sFuncName);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetProcAddress", CallingConvention = CallingConvention.StdCall)]
        private extern static IntPtr GetProcAddressW(IntPtr hModole, string sFuncName);
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        private extern static bool FreeLibrary(IntPtr hModule);

        private IntPtr hModule = IntPtr.Zero;
        private string sDllPath;
        public bool ShowMsg { get; set; }
        public DllInvoke(string sDllPathName, bool showMsg = true)
        {
            this.sDllPath = sDllPathName;
            this.ShowMsg = ShowMsg;
        }
        ~DllInvoke()
        {
            if (hModule != IntPtr.Zero)
                FreeLibrary(hModule);
            hModule = IntPtr.Zero;
        }
        public void LoadDll()
        {
            if (hModule != IntPtr.Zero)
                FreeLibrary(hModule);
            hModule = LoadLibrary(sDllPath);
            if (hModule == IntPtr.Zero)
            {
                string exMsg = string.Format("Load '{0}' file unsuccessfully.Please check if it exists.", sDllPath);
                throw (new Exception(exMsg));
            }
        }
        public void FreeDll()
        {
            if (hModule != IntPtr.Zero)
                FreeLibrary(hModule);
            hModule = IntPtr.Zero;
        }
        //将要执行的函数转换为委托
        public Delegate InvokeFunc(string sFuncName, Type t)
        {
            try
            {
                if (hModule == IntPtr.Zero)
                {
                    throw (new Exception("The value of module is zero."));
                }
                IntPtr funcAddr = IntPtr.Zero;
                funcAddr = GetProcAddressA(hModule, sFuncName);

                if (funcAddr == IntPtr.Zero)
                {
                    string exMsg = string.Format("Unable to get the address of the function '{0}'.", sFuncName);
                    throw (new Exception(exMsg));
                }
                Delegate de = (Delegate)Marshal.GetDelegateForFunctionPointer(funcAddr, t);

                return (Delegate)Marshal.GetDelegateForFunctionPointer(funcAddr, t);
            }
            catch (System.Exception ex)
            {
                if (ShowMsg)
                    MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}

