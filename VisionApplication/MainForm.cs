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

namespace VisionApplication
{
    public partial class MainForm :Sunny.UI.UIForm
    {
        AccessLevel mCurrentAccessLevel = AccessLevel.Administrator;
        ILog log = LogManager.GetLogger(typeof(MainForm));
        public MainForm()
        {
            InitializeComponent();
            this.Text = Program.Title;
            this.DoubleBuffered=true;
            this.MinimumSize = new Size(900, 600);
            visionControl1.RunStateUpdated += VisionControl1_RunStateUpdated;
            visionControl1.ErrorMsgRcv += x => toolStripStatusLabel2.Text = x;
            visionControl1.AutoRunModeChanged += VisionControl1_AutoRunModeChanged;
            visionControl1.PreviewChanged += VisionControl1_PreviewChanged;
            this.SizeChanged += MainForm_SizeChanged;
            foreach(var tsb in toolStrip1.Items.OfType<ToolStripButton>())
                tsb.Paint += ToolStripButton_Paint;
        }

        private void ToolStripButton_Paint(object sender, PaintEventArgs e)
        {
            return;
            var t = sender as ToolStripButton;
            var bcolor = t.BackColor;
            if (bcolor == Color.Transparent)
                bcolor = toolStrip1.Parent.BackColor;
            var fillRect = t.Checked ? new Rectangle(0, 0, t.Width, t.Height) : new Rectangle(1, 1, t.Width - 2, t.Height - 2);

            e.Graphics.FillRectangle(new SolidBrush(bcolor), fillRect);
            var textSize = e.Graphics.MeasureString(t.Text, t.Font);
            if (t.Checked)
                using (var pen = new Pen(Color.Blue))
                    e.Graphics.DrawRectangle(pen, new Rectangle(Point.Empty, btnRunMannul.Size));

            int imgW = (int)textSize.Height + 3;
            var img = t.Enabled ? t.Image : t.Image.CreateDisabledImage(null);
            e.Graphics.DrawImage(img, 3, (t.Height - imgW) / 2, imgW, imgW);
            e.Graphics.DrawString(t.Text, t.Font, new SolidBrush(t.Enabled ? t.ForeColor : Color.DarkGray), imgW + 6, (t.Height - textSize.Height) / 2);
        }

        private void VisionControl1_AutoRunModeChanged(bool autoRun)
        {
            if (autoRun)
                btnRunMannul.BackColor = Color.Transparent;
            else
                btnRunMannul.BackColor = Color.Orange;
            toolStrip1.Refresh();
        }
 

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            visionControl1.Width = visionControl1.Parent.Width - visionControl1.Parent.Padding.Left;
        }

        /// <param name="e"></param>
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    // ʹ��˫����
        //    this.DoubleBuffered = true;
        //    // �����ػ��ƶ�����
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
        
            btnRun.Enabled = btnRunOnce.Enabled = !visionControl1.InitError && !running;
            btnPause.Enabled = !visionControl1.InitError && running;
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

        private void VisionControl1_PreviewChanged(bool preview)
        {
            if (preview)
            {
                btnPreview.Tag = 1;
                btnPreview.Text = "ȡ��Ԥ��";
                btnPreview.Image = Properties.Resources.UnPreview;
            }
            else
            {
                btnPreview.Tag = null;
                btnPreview.Text = "Ԥ��";
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
            fileDialog.Title = "��ѡ���ļ�";
            fileDialog.Filter = "�����ļ�(*.*)|*.vpp"; //����Ҫѡ����ļ�������
            string file = string.Empty;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.FileName;//�����ļ�������·��      
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
            //visionControl1.RunJobCont(visionControl1.SelectedTab);
            btnRunMannul.Checked = !btnRunMannul.Checked;
            visionControl1.AutoRunMode = !btnRunMannul.Checked;
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
            frm.Show();
            //frm.Dispose();

            // prompt for save of vpp file
            string vpp = visionControl1.LoadedVppFilename;
            string quotedvpp = "\"" + vpp + "\"";
            string saveButtonName = ResourceUtility.GetString("RtSaveSettingsButton");
            string quotedSaveButtonName = "\"" + saveButtonName + "\"";
            string promptStr = ResourceUtility.FormatString("RtSaveSettingsTextAfterConfig", quotedvpp, quotedSaveButtonName);
            frm.FormClosing+=(_,__)=>  visionControl1.PromptToSaveSettings(promptStr);

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

        private void uiContextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
           if( e.ClickedItem ==   miAbout)
            {
                new FormAbout("�汾 "+Program.Version, visionControl1) { Text= "���� " + Program.Title}.ShowDialog(this);
            }
        }

        private void btnRunOnce_Click(object sender, EventArgs e)
        {
            visionControl1.RunOnce();
        }
    }
    
}