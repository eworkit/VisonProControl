using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using System.Windows.Forms;
using Cognex.VisionPro;

namespace VisionApplication
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
        {       Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
            FileInfo file = new FileInfo(Environment.CurrentDirectory + "\\log4net.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);
            Application.Run(new MainForm());
            
      CogFrameGrabbers frameGrabbers = new CogFrameGrabbers();
      for (int i = 0; i < frameGrabbers.Count; i++)
      {
        frameGrabbers[i].Disconnect(false);
      }
    } 
        static Assembly _assembly = Assembly.GetExecutingAssembly();
        internal static string Title => _assembly.GetCustomAttribute<AssemblyTitleAttribute>().Title;
        internal static string Version => _assembly.GetName().Version.ToString();
  }
}