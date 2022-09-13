using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using log4net;
using VisionControl;

namespace VisionApplication
{
    public partial class MainForm :Sunny.UI.UIForm
    {
        AccessLevel mCurrentAccessLevel = AccessLevel.Administrator;
        ILog log = LogManager.GetLogger(typeof(MainForm));
        public MainForm()
        {
            InitializeComponent();
            visionControl1.RunStateUpdated += VisionControl1_RunStateUpdated;
            visionControl1.ErrorMsgRcv += x => toolStripStatusLabel2.Text = x;
            visionControl1.PreviewChanged += VisionControl1_PreviewChanged;
            this.Text = "视觉检测系统";
            this.DoubleBuffered=true;  
        }
        /// <param name="e"></param>
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
            bool running = mCurrentRunState != RunState.Stopped;
            bool runningLive = mCurrentRunState == RunState.RunningLive;
            bool runningContinuous = mCurrentRunState == RunState.RunningContinuous;
            var mJM = visionControl1.JobManager;
            bool currentJobCanLive = mJM != null &&   mJM.JobCount > visionControl1. SelectedTab && mJM.Job(visionControl1.SelectedTab).AcqFifo != null;

            bool canConfig = !running && mCurrentAccessLevel == AccessLevel.Administrator;
            bool canSaveSettings = !running && mCurrentAccessLevel >= AccessLevel.Supervisor;
        
            btnRun.Enabled = btnRunMannul.Enabled = !visionControl1.InitError && !running;
            btnPause.Enabled = !visionControl1.InitError && running;
            btnPreview.Enabled= !visionControl1.InitError;
            button_Configuration.Enabled = !visionControl1.InitError && canConfig;
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
                log.Info("Open vpp file:" + file);
                toolStripStatusLabel1.Text=Path.GetFileName(file);
                toolStripStatusLabel1.ToolTipText = file;
                toolStripStatusLabel2.Text = null;
                toolStripStatusLabel2.ToolTipText = null;
                if (IsPreView)
                    visionControl1.Preview(false);
                visionControl1.OpenVpp(file);
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            visionControl1.RunCont();
        }

        private void btnRunMannul_Click(object sender, EventArgs e)
        {
            visionControl1.RunJobCont(visionControl1.SelectedTab);
        }
        bool IsPreView => 1.Equals(btnPreview.Tag);
        private void btnJob_Click(object sender, EventArgs e)
        {
            bool needPreview = !this.IsPreView;
            visionControl1.Preview(needPreview);
            
        }
        private void button_Configuration_Click(object sender, System.EventArgs e)
        {
            // detach from job manager before displaying edit control
            visionControl1.AttachToJobManager(false);

            // put up a new dialog containing QB editor
            FormQB frm = new FormQB(visionControl1.JobManager);
            frm.ShowDialog(this);
            frm.Dispose();

            // prompt for save of vpp file
            string vpp = visionControl1.LoadedVppFilename;
            string quotedvpp = "\"" + vpp + "\"";
            string saveButtonName = ResourceUtility.GetString("RtSaveSettingsButton");
            string quotedSaveButtonName = "\"" + saveButtonName + "\"";
            string promptStr = ResourceUtility.FormatString("RtSaveSettingsTextAfterConfig", quotedvpp, quotedSaveButtonName);
            visionControl1.PromptToSaveSettings(promptStr);

            // re-attach
            visionControl1.AttachToJobManager(true);
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (visionControl1.IsAutoRun)
                visionControl1.RunCont();
            else
                visionControl1.RunJobCont(visionControl1.SelectedTab);
        }
    }
}