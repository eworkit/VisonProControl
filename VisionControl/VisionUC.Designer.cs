using System.Windows.Forms;
using Sunny.UI;

namespace VisionControl
{
  partial class VisionUC
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        if (mJM != null)
          mJM.Shutdown();
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    // cognex.wizard.initializecomponent
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("ÔËÐÐ×´Ì¬");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("TCP");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Í¨ÓÃIO");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("´®¿Ú");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ModBus");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Í¨ÐÅÉèÖÃ", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("ÈÕÖ¾");
            this.comboBox_Login = new System.Windows.Forms.ComboBox();
            this.button_SaveSettings = new System.Windows.Forms.Button();
            this.checkBox_LiveDisplay = new System.Windows.Forms.CheckBox();
            this.button_About = new System.Windows.Forms.Button();
            this.label_Login = new System.Windows.Forms.Label();
            this.button_Configuration = new System.Windows.Forms.Button();
            this.applicationErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.cogJobResultHistoryCollectionEdit1 = new Cognex.VisionPro.QuickBuild.CogJobResultHistoryCollectionEdit();
            this.button_Chose = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControl_JobTabs = new Sunny.UI.UITabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ucOneJob1 = new VisionControl.UCOneJob();
            this.uiSplitContainer1 = new Sunny.UI.UISplitContainer();
            this.uiTabControl1 = new Sunny.UI.UITabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label_Online = new Sunny.UI.UILabel();
            this.uiButton1 = new Sunny.UI.UIButton();
            this.uiTextBox4 = new Sunny.UI.UITextBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.uiTextBox3 = new Sunny.UI.UITextBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiTextBox2 = new Sunny.UI.UITextBox();
            this.tbElapse = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new Sunny.UI.UITabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.uiNavBar1 = new Sunny.UI.UINavBar();
            this.ucTcpServer1 = new VisionControl.UcTcpServer();
            this.ucTcpClient1 = new VisionControl.UcTcpClient();
            ((System.ComponentModel.ISupportInitialize)(this.applicationErrorProvider)).BeginInit();
            this.tabControl_JobTabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitContainer1)).BeginInit();
            this.uiSplitContainer1.Panel1.SuspendLayout();
            this.uiSplitContainer1.Panel2.SuspendLayout();
            this.uiSplitContainer1.SuspendLayout();
            this.uiTabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox_Login
            // 
            this.comboBox_Login.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_Login.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_Login.Location = new System.Drawing.Point(816, 330);
            this.comboBox_Login.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_Login.Name = "comboBox_Login";
            this.comboBox_Login.Size = new System.Drawing.Size(175, 28);
            this.comboBox_Login.TabIndex = 41;
            this.comboBox_Login.SelectionChangeCommitted += new System.EventHandler(this.comboBox_Login_SelectionChangeCommitted);
            // 
            // button_SaveSettings
            // 
            this.button_SaveSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_SaveSettings.Location = new System.Drawing.Point(837, 282);
            this.button_SaveSettings.Margin = new System.Windows.Forms.Padding(4);
            this.button_SaveSettings.Name = "button_SaveSettings";
            this.button_SaveSettings.Size = new System.Drawing.Size(139, 35);
            this.button_SaveSettings.TabIndex = 47;
            this.button_SaveSettings.Text = "Save Settings...";
            this.button_SaveSettings.Click += new System.EventHandler(this.button_SaveSettings_Click);
            // 
            // checkBox_LiveDisplay
            // 
            this.checkBox_LiveDisplay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_LiveDisplay.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_LiveDisplay.Location = new System.Drawing.Point(688, 282);
            this.checkBox_LiveDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_LiveDisplay.Name = "checkBox_LiveDisplay";
            this.checkBox_LiveDisplay.Size = new System.Drawing.Size(139, 35);
            this.checkBox_LiveDisplay.TabIndex = 46;
            this.checkBox_LiveDisplay.Text = "Live Image";
            this.checkBox_LiveDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_LiveDisplay.CheckedChanged += new System.EventHandler(this.checkBox_LiveDisplay_CheckedChanged);
            // 
            // button_About
            // 
            this.button_About.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_About.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_About.Location = new System.Drawing.Point(688, 237);
            this.button_About.Margin = new System.Windows.Forms.Padding(4);
            this.button_About.Name = "button_About";
            this.button_About.Size = new System.Drawing.Size(139, 35);
            this.button_About.TabIndex = 44;
            this.button_About.Text = "About...";
            this.button_About.Click += new System.EventHandler(this.button_About_Click);
            // 
            // label_Login
            // 
            this.label_Login.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Login.Location = new System.Drawing.Point(674, 333);
            this.label_Login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Login.Name = "label_Login";
            this.label_Login.Size = new System.Drawing.Size(141, 24);
            this.label_Login.TabIndex = 49;
            this.label_Login.Text = "Current login:";
            // 
            // button_Configuration
            // 
            this.button_Configuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Configuration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Configuration.Location = new System.Drawing.Point(837, 237);
            this.button_Configuration.Margin = new System.Windows.Forms.Padding(4);
            this.button_Configuration.Name = "button_Configuration";
            this.button_Configuration.Size = new System.Drawing.Size(139, 35);
            this.button_Configuration.TabIndex = 45;
            this.button_Configuration.Text = "Configuration...";
            this.button_Configuration.Click += new System.EventHandler(this.button_Configuration_Click);
            // 
            // applicationErrorProvider
            // 
            this.applicationErrorProvider.ContainerControl = this;
            // 
            // cogJobResultHistoryCollectionEdit1
            // 
            this.cogJobResultHistoryCollectionEdit1.AutoScroll = true;
            this.cogJobResultHistoryCollectionEdit1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.cogJobResultHistoryCollectionEdit1.Location = new System.Drawing.Point(552, 30);
            this.cogJobResultHistoryCollectionEdit1.Margin = new System.Windows.Forms.Padding(4);
            this.cogJobResultHistoryCollectionEdit1.Name = "cogJobResultHistoryCollectionEdit1";
            this.cogJobResultHistoryCollectionEdit1.Padding = new System.Windows.Forms.Padding(5);
            this.cogJobResultHistoryCollectionEdit1.SelectedIndex = -1;
            this.cogJobResultHistoryCollectionEdit1.SelectorControlHeight = 29;
            this.cogJobResultHistoryCollectionEdit1.Size = new System.Drawing.Size(658, 88);
            this.cogJobResultHistoryCollectionEdit1.Subject = null;
            this.cogJobResultHistoryCollectionEdit1.TabIndex = 43;
            this.cogJobResultHistoryCollectionEdit1.Visible = false;
            this.cogJobResultHistoryCollectionEdit1.SelectedIndexChanged += new System.EventHandler(this.cogJobResultHistoryCollectionEdit1_SelectedJobIndexChanged);
            // 
            // button_Chose
            // 
            this.button_Chose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Chose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Chose.Location = new System.Drawing.Point(688, 193);
            this.button_Chose.Margin = new System.Windows.Forms.Padding(4);
            this.button_Chose.Name = "button_Chose";
            this.button_Chose.Size = new System.Drawing.Size(139, 35);
            this.button_Chose.TabIndex = 52;
            this.button_Chose.Text = "Chose Vpp";
            this.button_Chose.Click += new System.EventHandler(this.button_Chose_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // tabControl_JobTabs
            // 
            this.tabControl_JobTabs.Controls.Add(this.tabPage1);
            this.tabControl_JobTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_JobTabs.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl_JobTabs.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(249)))), ((int)(((byte)(241)))));
            this.tabControl_JobTabs.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl_JobTabs.Frame = null;
            this.tabControl_JobTabs.ItemSize = new System.Drawing.Size(150, 40);
            this.tabControl_JobTabs.Location = new System.Drawing.Point(0, 0);
            this.tabControl_JobTabs.MainPage = "";
            this.tabControl_JobTabs.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.tabControl_JobTabs.Multiline = true;
            this.tabControl_JobTabs.Name = "tabControl_JobTabs";
            this.tabControl_JobTabs.SelectedIndex = 0;
            this.tabControl_JobTabs.ShowToolTips = true;
            this.tabControl_JobTabs.Size = new System.Drawing.Size(638, 641);
            this.tabControl_JobTabs.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl_JobTabs.Style = Sunny.UI.UIStyle.Custom;
            this.tabControl_JobTabs.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.tabControl_JobTabs.TabIndex = 53;
            this.tabControl_JobTabs.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(220)))), ((int)(((byte)(230)))));
            this.tabControl_JobTabs.TabSelectedForeColor = System.Drawing.Color.White;
            this.tabControl_JobTabs.TabSelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.tabControl_JobTabs.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.tabControl_JobTabs.TagString = "";
            this.tabControl_JobTabs.TipsColor = System.Drawing.Color.RoyalBlue;
            this.tabControl_JobTabs.TipsFont = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl_JobTabs.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(249)))), ((int)(((byte)(241)))));
            this.tabPage1.Controls.Add(this.ucOneJob1);
            this.tabPage1.Location = new System.Drawing.Point(0, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(638, 601);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Î´ÅäÖÃ";
            // 
            // ucOneJob1
            // 
            this.ucOneJob1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucOneJob1.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucOneJob1.Location = new System.Drawing.Point(0, 0);
            this.ucOneJob1.MinimumSize = new System.Drawing.Size(1, 1);
            this.ucOneJob1.Name = "ucOneJob1";
            this.ucOneJob1.Size = new System.Drawing.Size(638, 601);
            this.ucOneJob1.TabIndex = 0;
            this.ucOneJob1.Text = "ucOneJob1";
            this.ucOneJob1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.ucOneJob1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiSplitContainer1
            // 
            this.uiSplitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.uiSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.uiSplitContainer1.MinimumSize = new System.Drawing.Size(20, 20);
            this.uiSplitContainer1.Name = "uiSplitContainer1";
            // 
            // uiSplitContainer1.Panel1
            // 
            this.uiSplitContainer1.Panel1.Controls.Add(this.tabControl_JobTabs);
            // 
            // uiSplitContainer1.Panel2
            // 
            this.uiSplitContainer1.Panel2.Controls.Add(this.uiTabControl1);
            this.uiSplitContainer1.Panel2.Controls.Add(this.uiNavBar1);
            this.uiSplitContainer1.Size = new System.Drawing.Size(1195, 641);
            this.uiSplitContainer1.SplitterDistance = 638;
            this.uiSplitContainer1.SplitterWidth = 11;
            this.uiSplitContainer1.TabIndex = 54;
            this.uiSplitContainer1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiTabControl1
            // 
            this.uiTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiTabControl1.Controls.Add(this.tabPage3);
            this.uiTabControl1.Controls.Add(this.tabPage4);
            this.uiTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControl1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.uiTabControl1.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTabControl1.Frame = null;
            this.uiTabControl1.ItemSize = new System.Drawing.Size(150, 40);
            this.uiTabControl1.Location = new System.Drawing.Point(-1, 50);
            this.uiTabControl1.MainPage = "";
            this.uiTabControl1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiTabControl1.Name = "uiTabControl1";
            this.uiTabControl1.SelectedIndex = 0;
            this.uiTabControl1.Size = new System.Drawing.Size(541, 590);
            this.uiTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControl1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTabControl1.TabIndex = 1;
            this.uiTabControl1.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.uiTabControl1.TabSelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.uiTabControl1.TipsFont = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTabControl1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage3.Controls.Add(this.label_Online);
            this.tabPage3.Controls.Add(this.uiButton1);
            this.tabPage3.Controls.Add(this.uiTextBox4);
            this.tabPage3.Controls.Add(this.uiLabel4);
            this.tabPage3.Controls.Add(this.uiTextBox3);
            this.tabPage3.Controls.Add(this.uiLabel3);
            this.tabPage3.Controls.Add(this.uiTextBox2);
            this.tabPage3.Controls.Add(this.tbElapse);
            this.tabPage3.Controls.Add(this.uiLabel2);
            this.tabPage3.Controls.Add(this.uiLabel1);
            this.tabPage3.Location = new System.Drawing.Point(0, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(541, 550);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Status";
            // 
            // label_Online
            // 
            this.label_Online.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Online.Location = new System.Drawing.Point(79, 285);
            this.label_Online.Name = "label_Online";
            this.label_Online.Size = new System.Drawing.Size(100, 23);
            this.label_Online.TabIndex = 7;
            this.label_Online.Text = "label_Online";
            this.label_Online.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_Online.Visible = false;
            this.label_Online.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiButton1
            // 
            this.uiButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton1.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.Location = new System.Drawing.Point(287, 16);
            this.uiButton1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton1.Name = "uiButton1";
            this.uiButton1.Size = new System.Drawing.Size(100, 35);
            this.uiButton1.TabIndex = 6;
            this.uiButton1.Text = "¸´Î»";
            this.uiButton1.TipsFont = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiButton1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiTextBox4
            // 
            this.uiTextBox4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox4.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox4.Location = new System.Drawing.Point(112, 140);
            this.uiTextBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox4.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox4.Name = "uiTextBox4";
            this.uiTextBox4.ReadOnly = true;
            this.uiTextBox4.ShowText = false;
            this.uiTextBox4.Size = new System.Drawing.Size(150, 29);
            this.uiTextBox4.TabIndex = 5;
            this.uiTextBox4.Text = "0/0";
            this.uiTextBox4.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox4.Watermark = "";
            this.uiTextBox4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.uiLabel4.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(17, 140);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(88, 23);
            this.uiLabel4.TabIndex = 4;
            this.uiLabel4.Text = "ºÏ¸ñÂÊ";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiTextBox3
            // 
            this.uiTextBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox3.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox3.Location = new System.Drawing.Point(112, 106);
            this.uiTextBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox3.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox3.Name = "uiTextBox3";
            this.uiTextBox3.ReadOnly = true;
            this.uiTextBox3.ShowText = false;
            this.uiTextBox3.Size = new System.Drawing.Size(150, 29);
            this.uiTextBox3.TabIndex = 3;
            this.uiTextBox3.Text = "0/0";
            this.uiTextBox3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox3.Watermark = "";
            this.uiTextBox3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel3
            // 
            this.uiLabel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.uiLabel3.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(17, 106);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(88, 23);
            this.uiLabel3.TabIndex = 2;
            this.uiLabel3.Text = "Ê§   °Ü";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiTextBox2
            // 
            this.uiTextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox2.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiTextBox2.Location = new System.Drawing.Point(112, 66);
            this.uiTextBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox2.MinimumSize = new System.Drawing.Size(1, 16);
            this.uiTextBox2.Name = "uiTextBox2";
            this.uiTextBox2.ReadOnly = true;
            this.uiTextBox2.ShowText = false;
            this.uiTextBox2.Size = new System.Drawing.Size(150, 29);
            this.uiTextBox2.TabIndex = 1;
            this.uiTextBox2.Text = "0/0";
            this.uiTextBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiTextBox2.Watermark = "";
            this.uiTextBox2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbElapse
            // 
            this.tbElapse.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbElapse.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbElapse.Location = new System.Drawing.Point(112, 22);
            this.tbElapse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbElapse.MinimumSize = new System.Drawing.Size(1, 16);
            this.tbElapse.Name = "tbElapse";
            this.tbElapse.ReadOnly = true;
            this.tbElapse.ShowText = false;
            this.tbElapse.Size = new System.Drawing.Size(150, 29);
            this.tbElapse.TabIndex = 1;
            this.tbElapse.Text = "00:00:00.000";
            this.tbElapse.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbElapse.Watermark = "";
            this.tbElapse.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.uiLabel2.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(17, 66);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(88, 23);
            this.uiLabel2.TabIndex = 0;
            this.uiLabel2.Text = "Í¨   ¹ý";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            this.uiLabel1.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(17, 22);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(88, 23);
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "Ê±   ¼ä";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tabPage4.Controls.Add(this.tabControl1);
            this.tabPage4.Location = new System.Drawing.Point(0, 40);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(541, 550);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "tabPage4";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.Font = new System.Drawing.Font("ËÎÌå", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Frame = null;
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 40);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.MainPage = "";
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(541, 550);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TipsFont = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Location = new System.Drawing.Point(0, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(541, 510);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "·þÎñ¶Ë";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ucTcpServer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 510);
            this.panel1.TabIndex = 0;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.ucTcpClient1);
            this.tabPage5.Location = new System.Drawing.Point(0, 40);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(541, 510);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "¿Í»§¶Ë";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // uiNavBar1
            // 
            this.uiNavBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiNavBar1.BackColor = System.Drawing.Color.LightSlateGray;
            this.uiNavBar1.DropMenuFont = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavBar1.Font = new System.Drawing.Font("Î¢ÈíÑÅºÚ", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiNavBar1.Location = new System.Drawing.Point(2, 0);
            this.uiNavBar1.MenuSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.uiNavBar1.MenuSelectedColorUsed = true;
            this.uiNavBar1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavBar1.Name = "uiNavBar1";
            this.uiNavBar1.NodeAlignment = System.Drawing.StringAlignment.Near;
            this.uiNavBar1.NodeInterval = 8;
            treeNode1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            treeNode1.Name = "TabRunState";
            treeNode1.Text = "ÔËÐÐ×´Ì¬";
            treeNode2.Name = "TCP";
            treeNode2.Text = "TCP";
            treeNode3.Name = "½Úµã0";
            treeNode3.Text = "Í¨ÓÃIO";
            treeNode4.Name = "½Úµã1";
            treeNode4.Text = "´®¿Ú";
            treeNode5.Name = "½Úµã2";
            treeNode5.Text = "ModBus";
            treeNode6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            treeNode6.Name = "TabTest";
            treeNode6.Text = "Í¨ÐÅÉèÖÃ";
            treeNode7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            treeNode7.Name = "TabMsg";
            treeNode7.Text = "ÈÕÖ¾";
            this.uiNavBar1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode6,
            treeNode7});
            this.uiNavBar1.NodeSize = new System.Drawing.Size(100, 40);
            this.uiNavBar1.SelectedForeColor = System.Drawing.Color.White;
            this.uiNavBar1.SelectedHighColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.uiNavBar1.Size = new System.Drawing.Size(541, 49);
            this.uiNavBar1.Style = Sunny.UI.UIStyle.Custom;
            this.uiNavBar1.TabControl = this.uiTabControl1;
            this.uiNavBar1.TabIndex = 0;
            this.uiNavBar1.Text = "uiNavBar1";
            this.uiNavBar1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiNavBar1.MenuItemClick += new Sunny.UI.UINavBar.OnMenuItemClick(this.uiNavBar1_MenuItemClick);
            this.uiNavBar1.NodeMouseClick += new Sunny.UI.UINavBar.OnNodeMouseClick(this.uiNavBar1_NodeMouseClick);
            this.uiNavBar1.TabIndexChanged += new System.EventHandler(this.uiNavBar1_TabIndexChanged);
            // 
            // ucTcpServer1
            // 
            this.ucTcpServer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTcpServer1.Location = new System.Drawing.Point(0, 0);
            this.ucTcpServer1.Name = "ucTcpServer1";
            this.ucTcpServer1.Size = new System.Drawing.Size(541, 510);
            this.ucTcpServer1.TabIndex = 0;
            // 
            // ucTcpClient1
            // 
            this.ucTcpClient1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTcpClient1.Location = new System.Drawing.Point(0, 0);
            this.ucTcpClient1.Name = "ucTcpClient1";
            this.ucTcpClient1.Size = new System.Drawing.Size(541, 510);
            this.ucTcpClient1.TabIndex = 0;
            // 
            // VisionUC
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.uiSplitContainer1);
            this.Controls.Add(this.button_Chose);
            this.Controls.Add(this.cogJobResultHistoryCollectionEdit1);
            this.Controls.Add(this.label_Login);
            this.Controls.Add(this.comboBox_Login);
            this.Controls.Add(this.button_SaveSettings);
            this.Controls.Add(this.button_Configuration);
            this.Controls.Add(this.checkBox_LiveDisplay);
            this.Controls.Add(this.button_About);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VisionUC";
            this.Size = new System.Drawing.Size(1195, 641);
            this.Load += new System.EventHandler(this.VisionControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.applicationErrorProvider)).EndInit();
            this.tabControl_JobTabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.uiSplitContainer1.Panel1.ResumeLayout(false);
            this.uiSplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uiSplitContainer1)).EndInit();
            this.uiSplitContainer1.ResumeLayout(false);
            this.uiTabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion
    private System.Windows.Forms.ComboBox comboBox_Login;
    private System.Windows.Forms.Button button_SaveSettings;
    private System.Windows.Forms.CheckBox checkBox_LiveDisplay;
    private System.Windows.Forms.Button button_About;
    private System.Windows.Forms.Label label_Login;
    private System.Windows.Forms.Button button_Configuration;
    private System.Windows.Forms.ErrorProvider applicationErrorProvider;
        private Button button_Chose;
        private ContextMenuStrip contextMenuStrip1;
        private Cognex.VisionPro.QuickBuild.CogJobResultHistoryCollectionEdit cogJobResultHistoryCollectionEdit1;
        private Sunny.UI.UITabControl tabControl_JobTabs;
        private TabPage tabPage1;
        private Sunny.UI.UISplitContainer uiSplitContainer1;
        private Sunny.UI.UITabControl uiTabControl1;
        private TabPage tabPage4;
        private TabPage tabPage3;
        private Sunny.UI.UILabel label_Online;
        private Sunny.UI.UIButton uiButton1;
        private Sunny.UI.UITextBox uiTextBox4;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UITextBox uiTextBox3;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UITextBox uiTextBox2;
        private Sunny.UI.UITextBox tbElapse;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel1;
        private VisionControl.UCOneJob ucOneJob1;
        private Sunny.UI.UINavBar uiNavBar1;
        private UITabControl tabControl1;
        private TabPage tabPage2;
        private TabPage tabPage5;
        private Panel panel1;
        private UcTcpServer ucTcpServer1;
        private UcTcpClient ucTcpClient1;
    }
}
