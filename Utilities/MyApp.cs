using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Utilities.ExMethod;

namespace Utilities
{
    public static class MyApp
    {
        public static readonly string AppPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
        public static readonly string AppConfigPath = Path.Combine(AppPath, "Config");
        public static bool IsForDevelop = false;
        public static readonly string DefaultDecimalSeparator =".";
        public static readonly bool IsDefaultDecimalSeparator = DefaultDecimalSeparator == System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        public static readonly string DecimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;

        static MyApp()
        {
            if (DecimalSeparator.IsEmpty())
                DecimalSeparator = DefaultDecimalSeparator;
            else
            {
                bool b = DecimalSeparator.Length > 1;
                if (b)
                    DecimalSeparator = DecimalSeparator.Substring(0, 1);
                if (!".,·'`".Contains(DecimalSeparator))
                {
                    DecimalSeparator = DefaultDecimalSeparator;
                    b = true;
                }
                if (b)
                {
                   var ci= (System.Globalization.CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();
                    ci.NumberFormat .NumberDecimalSeparator = DecimalSeparator;
                    System.Threading.Thread.CurrentThread.CurrentCulture = ci;
                }
                IsDefaultDecimalSeparator = DefaultDecimalSeparator == System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
            }
        }
        public static IFormatProvider FormatProvider
        {
            get
            {
                //return System.Globalization.NumberFormatInfo.CurrentInfo;
                return System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat;
            }
        }
        /// <summary>
        /// true:内部用，false：客户用
        /// </summary>
        public static bool IsInner = false;
        public static void CreatePathIfNotExist(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public static string ToLocalString(this double? d,string format=null)
        {
            if (d.HasValue)
            {
                if(format==null)
                    return d.Value.ToString(System.Globalization.NumberFormatInfo.CurrentInfo);
                return d.Value.ToString(format, System.Globalization.NumberFormatInfo.CurrentInfo);
            }
            return null;
        }
        public static string ToStandString(this double? d, string format = null)
        {
            if (d.HasValue)
            {
                if (format == null)
                    return d.Value.ToString(System.Globalization.NumberFormatInfo.InvariantInfo);
                return d.Value.ToString(format, System.Globalization.NumberFormatInfo.InvariantInfo);
            }
            return null;
        }

        public static double? LocalStringToDoubleX(this string obj, double? defaultValue = null)
        {
            if (obj == null) return defaultValue;
            double n;
            if (double.TryParse(obj, System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.CurrentInfo, out n))
                return n;
            return defaultValue;
        }
    }

    public static class DebugLog
    {
        static readonly string logFile = MyApp.AppPath + "\\log\\app.log";
        static readonly string logFile0 = MyApp.AppPath + "\\log\\app_0.log";
        static DebugLog()
        {
            string dir = Path.Combine(MyApp.AppPath, "log");
            try
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                if (File.Exists(logFile))
                {
                    FileInfo f = new FileInfo(logFile);
                    if (f.Length > 1024 * 1024 * 2)
                    {
                        f.CopyTo(logFile0, true);
                        f.Delete();
                    }
                }
                System.Diagnostics.Trace.AutoFlush = true;
                System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(logFile));
            }
            catch (Exception ex) { }
        }
        public static void WriteLine(string s, LogLevle levle = LogLevle.log, string caption = "")
        {
            //    Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //    Debug.AutoFlush = true;
            //    Debug.Indent();
            //Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            //Trace.AutoFlush = true;
            //Trace.Indent();
            //   System.Diagnostics. Trace.WriteLine(s);
            //if ((int)levle > (int)LogLevle.warning)
            //    s += new StackTrace().ToString();

            if (caption.IsEmpty())
            {
                switch (levle)
                {
                    case LogLevle.log: caption = "Log"; break;
                    case LogLevle.warning: caption = "Waring"; break;
                    case LogLevle.wrong: caption = "Wrong"; break;
                    case LogLevle.exception: caption = "Exception"; break;
                }
            }
            try
            {
                StackTrace st = new StackTrace(1, true);
                StackFrame sf = st.GetFrame(0);
                s += string.Format("\r\n            File: {0};", sf.GetFileName());                                                //文件名
                s += string.Format("---- Method:{0};", sf.GetMethod().Name);                                 //函数名
                s += string.Format(" Line: {0};", sf.GetFileLineNumber());
            }
            catch { }  //文件行号
            System.Diagnostics.Trace.WriteLine(s, DateTime.Now.ToString("[yyMMdd HH:mm:ss.fff]") + caption);
            //   Debug.Unindent();
        }
        public static void WriteDbg(this object obj, string s, LogLevle levle = LogLevle.log, string caption = "")
        {
            WriteLine(obj.GetType().Name + ":" + s, levle, caption);
        }
    }
    public enum LogLevle
    {
        log = 0, warning = 1, exception = 2, wrong = 3
    }
     
}
