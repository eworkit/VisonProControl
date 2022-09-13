using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using Utilities.UI.ExMethod;

namespace Utilities.UI
{
    public class SplashFormBase : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
private static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);

/*
1. AW_SLIDE : 使用滑动类型, 默认为该类型. 当使用 AW_CENTER 效果时, 此效果被忽略
2. AW_ACTIVE: 激活窗口, 在使用了 AW_HIDE 效果时不可使用此效果
3. AW_BLEND: 使用淡入效果
4. AW_HIDE: 隐藏窗口
5. AW_CENTER: 与 AW_HIDE 效果配合使用则效果为窗口几内重叠,  单独使用窗口向外扩展.
6. AW_HOR_POSITIVE : 自左向右显示窗口
7. AW_HOR_NEGATIVE: 自右向左显示窗口
8. AW_VER_POSITVE: 自顶向下显示窗口
9. AW_VER_NEGATIVE : 自下向上显示窗口
*/
public const Int32 AW_HOR_POSITIVE = 0x00000001;
public const Int32 AW_HOR_NEGATIVE = 0x00000002;
public const Int32 AW_VER_POSITIVE = 0x00000004;
public const Int32 AW_VER_NEGATIVE = 0x00000008;
public const Int32 AW_CENTER = 0x00000010;
public const Int32 AW_HIDE = 0x00010000;
public const Int32 AW_ACTIVATE = 0x00020000;
public const Int32 AW_SLIDE = 0x00040000;
public const Int32 AW_BLEND = 0x00080000;
public static void HideFXCenter(IntPtr wnd, int dwtime)
{
    AnimateWindow(wnd, dwtime, AW_CENTER | AW_HIDE | AW_SLIDE);
}
        private delegate void CloseHandler();
        private IContainer components;
       System.Timers.Timer timer1;
        private int count = 100;
        private static SplashFormBase sp;
        private bool autoClose;
        public event EventHandler Ended;
        public virtual bool AutoClose
        {
            get
            {
                return this.autoClose;
            }
            set
            {
                this.autoClose = value;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new Container();
           // this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Timers.Timer();
            base.SuspendLayout();
            this.timer1.Interval = 35;
           // this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.timer1.Elapsed += timer1_Tick;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(400, 130);
            base.FormBorderStyle = FormBorderStyle.None;
            base.Name = "SplashFormBase";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "";
          //  base.TopMost = true;
            base.ResumeLayout(false);
            FormClosed += SplashFormBase_FormClosed;
        }

        void SplashFormBase_FormClosed(object sender, FormClosedEventArgs e)
        {
           // HideFXCenter(SplashFormBase.sp.Handle, 400);
        }
   
        public SplashFormBase()
        {
            this.InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.SafeInvoke(() =>
            {
                this.count -= 4;
                if (this.count < 10)
                {
                    this.timer1.Enabled = false;
                  //  this.timer1.Tick -= new EventHandler(this.timer1_Tick);
                    this.timer1.Elapsed -= timer1_Tick;
                    base.Close();
                    base.Dispose();
                }
                else
                {
                    base.Opacity = (double)this.count / 100.0;
                }
                this.Ending();
                if (Ended != null)
                    Ended(this, EventArgs.Empty);
            });
        }
        public virtual void Ending()
        {
        }
        public static void StartFlashForm(SplashFormBase frm)
        {
            var thread = new System.Threading. Thread(new ParameterizedThreadStart(SplashFormBase.StartFlashForm1));
            thread.Start(frm);
          //  SplashFormBase.StartFlashForm1(frm);
        }
        private static void StartFlashForm1(object obj)
        {
            SplashFormBase splashFormBase = obj as SplashFormBase;
            if (splashFormBase != null)
            {
                SplashFormBase.sp = splashFormBase;
            //    SplashFormBase.sp.TopLevel = true;
               // SplashFormBase.sp.TopMost = true;
             // 
                SplashFormBase.sp.ShowDialog();
            }
        }
        public virtual void EndFlashForm()
        {
            this.timer1.Enabled = true;
            this.timer1.Start();
        }
        public static void Stop()
        {
            if (SplashFormBase.sp == null)
            {
                return;
            }
            if (SplashFormBase.sp.InvokeRequired)
            {
                SplashFormBase.CloseHandler method = new SplashFormBase.CloseHandler(SplashFormBase.Stop);
                SplashFormBase.sp.Invoke(method);
                return;
            }
            SplashFormBase.sp.EndFlashForm();
        }
        public static void CloseFlashForm()
        {
            SplashFormBase.Stop();
        }
    }
}
