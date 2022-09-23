using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text; 
using System.Windows.Forms;
using log4net;
using VisionControl;
using Utilities.ExMethod;
using Sunny.UI;
using Utilities;
using Cognex.VisionPro.Implementation.Internal;
using Utilities.Data;
using Utilities.UI;
using Utilities.UI.ExMethod;
using System.Threading;
using System.Threading.Tasks;
using Sunny.UI.Win32;

namespace VisionApplication
{
    public partial class MainForm :Sunny.UI.UIForm
    {
        LoginUser loginUser = null;
        DBConnInfo dbConn;
        ILog log = LogManager.GetLogger(typeof(MainForm));
        public MainForm()
        {
            InitializeComponent();
            this.Text = Program.Title;
            this.DoubleBuffered = true;
            this.MinimumSize = new Size(900, 600);
            visionControl1.RunStateUpdated += x => this.SafeInvoke(() => VisionControl1_RunStateUpdated(x));
            visionControl1.ErrorMsgRcv += x => this.SafeInvoke(() => VisionControl1_ErrorMsgRcv(x));
            visionControl1.AutoRunModeChanged += x => this.SafeInvoke(() => VisionControl1_AutoRunModeChanged(x));
            visionControl1.PreviewChanged += VisionControl1_PreviewChanged;
            visionControl1.ProjectOpened += x => this.SafeInvoke(() => VisionControl1_ProjectOpened(x));
            this.SizeChanged += MainForm_SizeChanged;
            foreach (var tsb in toolStrip1.Items.OfType<ToolStripButton>())
                tsb.Paint += ToolStripButton_Paint;
            if (MyAppConfig.AutoLogin)
            {
                LoginUser = MyAppConfig.User;
            }
            btnRunMannul.Checked = !MyAppConfig.AutoRunMode;
            visionControl1.AutoRunMode = MyAppConfig.AutoRunMode;
            tsbDatabase.Click += TsbDatabase_Click;
            this.Load += MainForm_Load;
            Application.ThreadException += Application_ThreadException;
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception.InnerException != null)
                log.Error(e.Exception.InnerException);
            log.Error(e.Exception);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dbConn = MyAppConfig.DbInfo;
            try
            {
                if (dbConn == null || !DbOperation.TestConn(dbConn))
                {
                    tsbDatabase.Text = "数据库未设置";
                    return;
                }
                dataBaseChanged();
            }
            catch (Exception ex)
            {
                MessageBoxE.Show(this, "数据库连接失败\r\n"+ex.Message);
                return;
            }
            if (MyAppConfig.AutoOpenFile)
            {
                string lastFile = MyAppConfig.LastRunFile;
                if (!string.IsNullOrEmpty(lastFile) && File.Exists(lastFile))
                {
                    ShowWaitForm("正在打开上次工程");
                    log.Info("自动打开上次vpp文件：" + MyAppConfig.LastRunFile);
                    new Thread(() => this.SafeInvoke(() => visionControl1.OpenVpp(MyAppConfig.LastRunFile))).Start();
                }
            }
        }
      

        private void ToolStripButton_Paint(object sender, PaintEventArgs e)
        { 
            var t = sender as ToolStripButton;
            var bcolor = t.BackColor;
            if (bcolor == Color.Transparent)
                bcolor = toolStrip1.Parent.BackColor;
            var fillRect = t.Checked ? new Rectangle(0, 0, t.Width, t.Height) : new Rectangle(1, 1, t.Width - 2, t.Height - 2);
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.FillRectangle(new SolidBrush(bcolor), fillRect);
            var textSize = e.Graphics.MeasureString(t.Text, t.Font);
            if (t.Checked)
                using (var pen = new Pen(Color.Blue))
                    e.Graphics.DrawRectangle(pen, new Rectangle(Point.Empty, btnRunMannul.Size));

            int imgW = toolStrip1.ImageScalingSize.Width;
            var img = t.Enabled ? t.Image : t.Image.CreateDisabledImage(null);
            g.DrawImage(img, 3, (t.Height - imgW) / 2, imgW, imgW);
            g.DrawString(t.Text, t.Font, new SolidBrush(t.Enabled ? t.ForeColor : Color.DarkGray), imgW + 6, (t.Height - textSize.Height) / 2);
        }

        private void VisionControl1_ErrorMsgRcv(string x)
        {
            //tsbMsg.Visible = true;
            //tsbMsg.Text = x;
            log.Error(x);
            HideWaitForm();
            MessageBoxE.Show(this, x, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void VisionControl1_AutoRunModeChanged(bool autoRun)
        {
            if (autoRun)
            {
                btnRunMannul.BackColor = Color.Transparent;
                btnRunMannul.ToolTipText = "点击切换为手动运行模式";
            }
            else
            {
                btnRunMannul.BackColor = Color.Orange;
                btnRunMannul.ToolTipText = "点击切换为自动运行模式";
            }
            toolStrip1.Refresh();
        }
 

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            visionControl1.Width = this.Width - this.Padding.Left;
            visionControl1.Height = this.Height - visionControl1.Top-statusStrip1.Height;
            statusStrip1.Top = visionControl1.Bottom;
        }
         
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    // 使用双缓冲
        //    this.DoubleBuffered = true;
        //    // 背景重绘移动到此
        //    if (this.BackgroundImage != null)
        //    {
        //        e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        //        e.Graphics.DrawImage(
        //            this.BackgroundImage,
        //            new System.Drawing.Rectangle(0, 0, this.Width, this.Height),
        //            0,
        //            0,
        //            this.BackgroundImage.Width,
        //            this.BackgroundImage.Height,
        //            System.Drawing.GraphicsUnit.Pixel);
        //    }
        //    base.OnPaint(e);
        //}

        private void VisionControl1_RunStateUpdated(VisionControl.RunState mCurrentRunState)
        {
            var access = visionControl1.CurrentAccessLevel;
            bool running = mCurrentRunState != RunState.Stopped;
            bool runningLive = mCurrentRunState == RunState.RunningLive;
            bool runningContinuous = mCurrentRunState == RunState.RunningContinuous;
            var mJM = visionControl1.JobManager;
            bool currentJobCanLive = mJM != null &&   mJM.JobCount > visionControl1. SelectedTab && mJM.Job(visionControl1.SelectedTab).AcqFifo != null;

            bool canConfig = !running && access == AccessLevel.Administrator;
            bool canSaveSettings = !running && access >= AccessLevel.Supervisor;
        
            btnRun.Enabled = btnRunOnce.Enabled = !visionControl1.InitError && !running;
            btnPause.Enabled = !visionControl1.InitError && running;
            btnOpen.Enabled = !running;
            btnPreview.Enabled= !visionControl1.InitError;
            button_Configuration.Enabled = /*!visionControl1.InitError &&*/ !string.IsNullOrEmpty(visionControl1.LoadedVppFilename) && File.Exists(visionControl1.LoadedVppFilename) && canConfig;
            //button_SaveSettings.Enabled = !visionControl1.InitError && canSaveSettings;
            //button_About.Enabled = !running;
            //checkBox_LiveDisplay.Enabled = !mInitError && currentJobCanLive &&
            //     ((mCurrentRunState == RunState.Stopped && checkBox_LiveDisplay.Checked == false) ||
            //      (mCurrentRunState == RunState.RunningLive && checkBox_LiveDisplay.Checked == true));

            //btnRunCont.Enabled = !mInitError && (!running || runningContinuous);
            //btnRunCont.Text = runningContinuous ? (ResourceUtility.GetString("RtStopButton")) :
            //  (ResourceUtility.GetString("RtRunContinuouslyButton"));

            //button_ResetStatistics.Enabled = !mInitError && !runningLive;
            //button_ResetStatisticsForAllJobs.Enabled = !mInitError && !runningLive;
        }

        private void VisionControl1_ProjectOpened(string file)
        {
            tsbOpenedFile.Text = "工程文件：" + Path.GetFileName(file);
            tsbOpenedFile.ToolTipText = file;
            this.Text = Program.Title + $"({Path.GetFileNameWithoutExtension(file)})";
            tsbMsg.Text = null;
            tsbMsg.ToolTipText = null;
            HideWaitForm();
        }

        private void VisionControl1_PreviewChanged(bool preview)
        {
            if (preview)
            {
                btnPreview.Tag = 1;
                btnPreview.Text = "取消预览";
                btnPreview.Image = Properties.Resources.UnPreview;
            }
            else
            {
                btnPreview.Tag = null;
                btnPreview.Text = "预览";
                btnPreview.Image = Properties.Resources.Preview;
            }
        }

        private void FormClosingHandler(object sender, FormClosingEventArgs e)
        {
            visionControl1.Close();
        }   

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void visionControl1_Load(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.vpp"; //设置要选择的文件的类型
            string file = string.Empty;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.FileName;//返回文件的完整路径      
                log.Info("打开 Vpp 文件:" + file);
                if (IsPreView)
                    visionControl1.Preview(false);
                ShowWaitForm("正在加载工程...");
                new Thread(() => this.SafeInvoke(() => visionControl1.OpenVpp(file))).Start();
                MyAppConfig.LastRunFile = file;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            visionControl1.RunCont();
        }

        private void btnRunMannul_Click(object sender, EventArgs e)
        {
            //visionControl1.RunJobCont(visionControl1.SelectedTab);
            btnRunMannul.Checked = !btnRunMannul.Checked;
           MyAppConfig.AutoRunMode = visionControl1.AutoRunMode = !btnRunMannul.Checked;
            log.Info(visionControl1.AutoRunMode ? "进入自动运行模式" : "进入手动运行模式");
        }
        bool IsPreView => 1.Equals(btnPreview.Tag);

        public LoginUser LoginUser
        {
            get
            {
                return loginUser;
            }

            set
            {
                loginUser = value;
                UserChnaged();
            }
        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            log.Info("执行持续运行所有Job操作");
            bool needPreview = !this.IsPreView;
            visionControl1.Preview(needPreview);
            
        }
        private void button_Configuration_Click(object sender, System.EventArgs e)
        {
            // detach from job manager before displaying edit control
            visionControl1.AttachToJobManager(false);

            // put up a new dialog containing QB editor
            FormQB frm = new FormQB(visionControl1.JobManager);
            frm.Show(this);
            // prompt for save of vpp file
            string vpp = visionControl1.LoadedVppFilename;
            string quotedvpp = "\"" + vpp + "\"";
            string saveButtonName = ResourceUtility.GetString("RtSaveSettingsButton");
            string quotedSaveButtonName = "\"" + saveButtonName + "\"";
            string promptStr = ResourceUtility.FormatString("RtSaveSettingsTextAfterConfig", quotedvpp, quotedSaveButtonName);
            frm.FormClosing += (_, __) => visionControl1.PromptToSaveSettings(promptStr);

            // re-attach
            visionControl1.AttachToJobManager(true);
        }

        private void btnPause_Click(object sender, EventArgs e)
       {
            //    if (visionControl1.CurrentRunState == RunState.RunningContinuous)
            //        visionControl1.RunCont();
            //    else if(visionControl1.CurrentRunState == RunState.RunningOnce)
            //        //visionControl1.RunJob(visionControl1.SelectedTab);
            //        visionControl1.RunOnce();
            log.Info("执行停止所有Job操作");
            visionControl1.JobManager.Stop();
        }

        private void uiContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           if( e.ClickedItem ==   miAbout)
            {
                new FormAbout("版本 "+Program.Version, visionControl1) { Text= "关于 " + Program.Title}.ShowDialog(this);
            }
        }

        private void btnRunOnce_Click(object sender, EventArgs e)
        {
            log.Info("执行单次运行所有Job操作");
            visionControl1.RunOnce();
        }

        private void openLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = Program.LogFileAppender().File;
            System.Diagnostics.Process.Start(f);
        }

        private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FLogin f = new FLogin();
            if (f.ShowDialog() == DialogResult.OK)
            {
                LoginUser = f.User;
            }
        }

        private void tsbLoginInfo_Click(object sender, EventArgs e)
        {
            ContextMenu ms = new ContextMenu();
            if (LoginUser == null)
            {
                LoginToolStripMenuItem_Click(sender, e);
                return;
            }
            ms.MenuItems.Add("切换登录",   LoginToolStripMenuItem_Click);
            ms.MenuItems.Add("退出登录",   (_, __) =>
            {
                if (MessageBoxE.Show(this, "确定退出登录吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    LoginUser = null;
                    MyAppConfig.User = null;
                    MyAppConfig.SaveLoginSet(null, false);
                }
            });
            ms.Show(statusStrip1, statusStrip1.Location);
        }
        private void TsbDatabase_Click(object sender, EventArgs e)
        {
            databaseToolStripMenuItem_Click(sender, e);
        }
        private void databaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FDatabaseConn();
            if (f.ShowDialog() == DialogResult.OK)
            {
                dbConn = f.dbInfo;
                dataBaseChanged();
            }
        }
        void UserChnaged()
        { 
            if (LoginUser == null)
            {
                tsbLoginInfo.Text = "未登录";
                tsbLoginInfo.ForeColor = Color.Red;
                visionControl1.CurrentAccessLevel = AccessLevel.Operator;
            }
            else
            {
                visionControl1.CurrentAccessLevel = LoginUser?.Level ?? AccessLevel.Operator;
                tsbLoginInfo.Text = visionControl1.CurrentAccessLevel.GetDescription();
                tsbLoginInfo.ForeColor = Color.Blue;
            } 
        }
         

        void dataBaseChanged()
        {
            tsbDatabase.Text = $"数据库：[{dbConn.host}] {dbConn.dbName}";
        }

        private void systemConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FSysConfig().ShowDialog(this);
        }
    }
    
}