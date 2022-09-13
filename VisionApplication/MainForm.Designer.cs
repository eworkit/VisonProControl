namespace VisionApplication
{
  partial class MainForm
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
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.uiFlowLayoutPanel1 = new Sunny.UI.UIFlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.visionControl1 = new VisionControl.VisionUC();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnRun = new System.Windows.Forms.ToolStripButton();
            this.btnRunMannul = new System.Windows.Forms.ToolStripButton();
            this.btnPause = new System.Windows.Forms.ToolStripButton();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.button_Configuration = new System.Windows.Forms.ToolStripButton();
            this.btnTool = new System.Windows.Forms.ToolStripButton();
            this.uiFlowLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.visionControl1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiFlowLayoutPanel1
            // 
            this.uiFlowLayoutPanel1.Controls.Add(this.toolStrip1);
            this.uiFlowLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiFlowLayoutPanel1.Location = new System.Drawing.Point(-10, 35);
            this.uiFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiFlowLayoutPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiFlowLayoutPanel1.Name = "uiFlowLayoutPanel1";
            this.uiFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(2);
            this.uiFlowLayoutPanel1.ShowText = false;
            this.uiFlowLayoutPanel1.Size = new System.Drawing.Size(1926, 53);
            this.uiFlowLayoutPanel1.TabIndex = 5;
            this.uiFlowLayoutPanel1.Text = "uiFlowLayoutPanel1";
            this.uiFlowLayoutPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiFlowLayoutPanel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnRun,
            this.btnRunMannul,
            this.btnPause,
            this.btnPreview,
            this.button_Configuration,
            this.btnTool});
            this.toolStrip1.Location = new System.Drawing.Point(2, 2);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(20, 0, 1, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1922, 46);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripContainer1
            // 
            this.toolStripContainer1.BottomToolStripPanelVisible = false;
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(150, 150);
            this.toolStripContainer1.LeftToolStripPanelVisible = false;
            this.toolStripContainer1.Location = new System.Drawing.Point(28, 35);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.RightToolStripPanelVisible = false;
            this.toolStripContainer1.Size = new System.Drawing.Size(150, 175);
            this.toolStripContainer1.TabIndex = 3;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // visionControl1
            // 
            this.visionControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visionControl1.Controls.Add(this.statusStrip1);
            this.visionControl1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.visionControl1.Location = new System.Drawing.Point(-4, 88);
            this.visionControl1.Margin = new System.Windows.Forms.Padding(4);
            this.visionControl1.MinimumSize = new System.Drawing.Size(1, 1);
            this.visionControl1.Name = "visionControl1";
            this.visionControl1.Size = new System.Drawing.Size(1924, 551);
            this.visionControl1.TabIndex = 2;
            this.visionControl1.Text = null;
            this.visionControl1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.visionControl1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 529);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(1924, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 55;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 16);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Margin = new System.Windows.Forms.Padding(50, 3, 0, 2);
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 17);
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnOpen.Image = global::VisionApplication.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Padding = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.btnOpen.Size = new System.Drawing.Size(127, 43);
            this.btnOpen.Text = "打开工程";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRun.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnRun.Image = global::VisionApplication.Properties.Resources.Run;
            this.btnRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRun.Name = "btnRun";
            this.btnRun.Padding = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.btnRun.Size = new System.Drawing.Size(87, 43);
            this.btnRun.Text = "运行";
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnRunMannul
            // 
            this.btnRunMannul.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRunMannul.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnRunMannul.Image = global::VisionApplication.Properties.Resources.Manual;
            this.btnRunMannul.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRunMannul.Name = "btnRunMannul";
            this.btnRunMannul.Padding = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.btnRunMannul.Size = new System.Drawing.Size(127, 43);
            this.btnRunMannul.Text = "手动运行";
            this.btnRunMannul.Click += new System.EventHandler(this.btnRunMannul_Click);
            // 
            // btnPause
            // 
            this.btnPause.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPause.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnPause.Image = global::VisionApplication.Properties.Resources.Stop;
            this.btnPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPause.Name = "btnPause";
            this.btnPause.Padding = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.btnPause.Size = new System.Drawing.Size(87, 43);
            this.btnPause.Text = "停止";
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPreview.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnPreview.Image = global::VisionApplication.Properties.Resources.Preview;
            this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Padding = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.btnPreview.Size = new System.Drawing.Size(87, 43);
            this.btnPreview.Text = "预览";
            this.btnPreview.Click += new System.EventHandler(this.btnJob_Click);
            // 
            // button_Configuration
            // 
            this.button_Configuration.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Configuration.ForeColor = System.Drawing.Color.MidnightBlue;
            this.button_Configuration.Image = global::VisionApplication.Properties.Resources.Config;
            this.button_Configuration.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.button_Configuration.Name = "button_Configuration";
            this.button_Configuration.Padding = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.button_Configuration.Size = new System.Drawing.Size(87, 43);
            this.button_Configuration.Text = "配置";
            this.button_Configuration.Click += new System.EventHandler(this.button_Configuration_Click);
            // 
            // btnTool
            // 
            this.btnTool.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTool.ForeColor = System.Drawing.Color.MidnightBlue;
            this.btnTool.Image = global::VisionApplication.Properties.Resources.Tool;
            this.btnTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTool.Name = "btnTool";
            this.btnTool.Padding = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.btnTool.Size = new System.Drawing.Size(87, 43);
            this.btnTool.Text = "工具";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1920, 656);
            this.Controls.Add(this.uiFlowLayoutPanel1);
            this.Controls.Add(this.visionControl1);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "MainForm";
            this.Text = "视觉检测系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 1002, 656);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingHandler);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.uiFlowLayoutPanel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.visionControl1.ResumeLayout(false);
            this.visionControl1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);

    }

        #endregion
        private VisionControl.VisionUC visionControl1;
        private Sunny.UI.UIFlowLayoutPanel uiFlowLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnRunMannul;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripButton btnRun;
        private System.Windows.Forms.ToolStripButton btnPause;
        private System.Windows.Forms.ToolStripButton btnPreview;
        private System.Windows.Forms.ToolStripButton button_Configuration;
        private System.Windows.Forms.ToolStripButton btnTool;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}

