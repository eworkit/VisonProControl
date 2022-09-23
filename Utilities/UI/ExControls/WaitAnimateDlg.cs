using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utilities.ExMethod;
using Utilities.UI.ExMethod;

namespace Utilities.UI
{
    public  partial class WaitAnimateDlg : Form
    {
        Bitmap bitmap = Properties.Resources.animated;
        bool current = false;
        public bool CancelButton=false;
        public event Action OnCancel;
      //  ToolTip tt = new ToolTip();
        public override string Text
        {
            get
            {
                lock (this)
                {
                    if (!base.IsDisposed)
                        return base.Text;               
                return null;
                }
            }
            set
            {
                lock (this)
                {
                    if (!base.IsDisposed)
                    {
                        base.Text = value;
                        var siz = this.CreateGraphics().MeasureString(value, label1.Font);
                        int n = (int)Math.Ceiling(siz.Width / label1.Width);
                        label1.Text = value;
                        label1.Left = this.Width / 2 - label1.Width / 2;
                        label1.Height = n * (int)(siz.Height + 2);
                        if (CancelButton)
                            this.Height = 65 + label1.Height + 35;
                        else
                            this.Height = 65 + label1.Height + 7;
                        label1.Top = 65;
                      //  tt.SetToolTip(label1, value);
                    }
                }

            }
        }

        public WaitAnimateDlg(string language="")
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            // this.TopMost = true;
            this.ShowInTaskbar = false;
            this.AutoSize = true;
            this.Opacity = 1;
            //  bitmap = new Bitmap(bitmap, 60, 60);
            this.Load += new EventHandler(WaitAnimateDlg_Load);
            this.StartPosition = FormStartPosition.CenterParent;
            this.DoubleBuffered = true;
            this.SetMove(this);
            label1.MaximumSize = new System.Drawing.Size(Width, 600);
            this.Text = "Please wait...";
            if(language.IsEmpty())
                language = System.Globalization.CultureInfo.CurrentCulture.Name;
            if (language.EqualsNoCase("zh-CN"))
                this.Text = "请稍等...";
            else if (language.EqualsNoCase("zh-TW"))
                this.Text = "請稍等...";

            label1.ForeColor = Color.DodgerBlue;
            label1.Font = new System.Drawing.Font(label1.Font.FontFamily, 10, FontStyle.Regular);
            ImageAnimator.Animate(bitmap, new EventHandler(this.OnFrameChanged));//播放
        }

        void WaitAnimateDlg_Load(object sender, EventArgs e)
        {
            if (CancelButton)
            {
                GMButton btnCancel = new GMButton();
                btnCancel.Text = "Cancel operation";
                btnCancel.Click += btnCancel_Click;
                var s = System.Globalization.CultureInfo.CurrentCulture.Name;
                if (s.EqualsNoCase("zh-CN"))
                    btnCancel.Text = "取消操作";
                else if (s.EqualsNoCase("zh-TW"))
                    btnCancel.Text = "取消操作";
                btnCancel.AutoSize = true;
                this.Controls.Add(btnCancel);
                label1.Top = 50;
                btnCancel.Top = label1.Bottom + 10;
                btnCancel.Left = this.Width / 2 - btnCancel.Width / 2;
                this.Height = btnCancel.Bottom + 8;
            }
            if (Owner == null)
            {
                var size = Screen.GetWorkingArea(this).Size;
                Left = (size.Width - Width) / 2;
                Top = (size.Height - Height) / 2;
            }
            else
            {
                resize();
                Owner.SizeChanged -= (s1, e1) => resize();
                Owner.LocationChanged -= (s1, e1) => resize();
                Owner.SizeChanged += (s1, e1) => resize();
                Owner.LocationChanged += (s1, e1) => resize();
                if(Owner.ParentForm!=null)
                {
                    Owner.ParentForm.SizeChanged -= (s1, e1) => resize();
                    Owner.ParentForm.LocationChanged -= (s1, e1) => resize();
                    Owner.ParentForm.SizeChanged += (s1, e1) => resize();
                    Owner.ParentForm.LocationChanged += (s1, e1) => resize();
                }
            }
        }
        void resize()
        {
            if (IsDisposed)
                return;
            var size = Owner.Size;
            var l = Owner.PointToScreen(Owner.Location);
            Left = l.X + (size.Width - Width) / 2;
            Top = l.Y + (size.Height - Height) / 2;
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            btn.Enabled = false;
            ImageAnimator.CanAnimate(bitmap);
            if (OnCancel != null)
                OnCancel();
       
        }

       
        public void AnimateImage()
        {
            if (!current)
            {
                ImageAnimator.Animate(bitmap, new EventHandler(this.OnFrameChanged));
                current = true;
            }
        }
        private void OnFrameChanged(object o, EventArgs e)
        {
            this.Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //   base.OnPaint(e);
            e.Graphics.DrawRectangle(Pens.DodgerBlue, 0, 0, Width - 1, Height - 1);
            AnimateImage();
            ImageAnimator.UpdateFrames();
            e.Graphics.DrawImage(this.bitmap, (this.Width / 2 - 26), 6, 52, 52);
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ImageAnimator.StopAnimate(bitmap, new EventHandler(this.OnFrameChanged)); 
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            ImageAnimator.Animate(bitmap, new EventHandler(this.OnFrameChanged)); 
        }
    }

    public class WaitAnimate :IWaitMessage 
    {
        BackgroundWorker bw = new BackgroundWorker();
        public WaitAnimateDlg dlg;
        public event EventHandler CancelClick;
        public string Text
        {
            get {  string s="";dlg.SafeInvoke(()=>s= dlg.Text);return s; }
            set { dlg.SafeInvoke(() => { if (dlg.IsDisposed)return; if (dlg.Text != value) dlg.Text = value; }); }
        }
        public bool CancelButton { get { return dlg.CancelButton; }
            set {
                dlg.CancelButton = value;
            }
        }
        public WaitAnimate(Form owner = null, string language = "")
        {
           dlg = new WaitAnimateDlg(language);
            if (owner == null)
                dlg.Owner = Control.FromHandle(System.Diagnostics.Process.GetCurrentProcess().MainWindowHandle) as Form;
            else
                dlg.Owner = owner;
            dlg.OnCancel += dlg_OnCancel;
            dlg.Text = dlg.Text;
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            RunWorkerCompleted += (s, e) => dlg.SafeInvoke(() => { if (!dlg.IsDisposed) dlg.Close(); });
        }

        void dlg_OnCancel()
        {
            bw.CancelAsync();
            if (CancelClick != null)
                CancelClick(this, EventArgs.Empty);
        }
        public event DoWorkEventHandler DoWork { add { bw.DoWork +=value; } remove { bw.DoWork -=value; } }
        public event RunWorkerCompletedEventHandler RunWorkerCompleted { add { bw.RunWorkerCompleted += (s, e) => value(this, e); } remove { bw.RunWorkerCompleted -= (s, e) => value(this, e); ; } }
    
        public static void RunWorkerAsync(Action a)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += (s1, e1) => a();             
            bw.RunWorkerAsync();
        }
        public void RunWorkerAsync(IWin32Window owner = null, object argument = null)
        {
            if (bw.IsBusy)
            {
                return;
            }
            if (dlg.IsDisposed)
                dlg = new WaitAnimateDlg(); 

            dlg.Show(owner);
            bw.RunWorkerAsync(argument);
            dlg.Activate();
        }
        public void Close()
        {
            dlg.SafeInvoke(() => { if (!dlg.IsDisposed) dlg.Close(); });
        }

        public string Message
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
            }
        }


        public void Dispose()
        {
            dlg.Close();
        }
    }
   
}
