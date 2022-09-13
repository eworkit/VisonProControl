using System;
using System.Collections.Generic;
using System.IO;
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
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
            FileInfo file = new FileInfo(Environment.CurrentDirectory + "\\app.config");
            log4net.Config.XmlConfigurator.ConfigureAndWatch(file);
            Application.Run(new MainForm());

      CogFrameGrabbers frameGrabbers = new CogFrameGrabbers();
      for (int i = 0; i < frameGrabbers.Count; i++)
      {
        frameGrabbers[i].Disconnect(false);
      }
    }
  }
}