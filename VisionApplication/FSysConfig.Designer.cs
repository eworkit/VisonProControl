namespace VisionApplication
{
    partial class FSysConfig
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
            this.ckbAutoStart = new Sunny.UI.UICheckBox();
            this.ckbAutoOpen = new Sunny.UI.UICheckBox();
            this.pnlBtm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Location = new System.Drawing.Point(1, 373);
            this.pnlBtm.Size = new System.Drawing.Size(541, 55);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(413, 12);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(298, 12);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ckbAutoStart
            // 
            this.ckbAutoStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckbAutoStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbAutoStart.Location = new System.Drawing.Point(52, 63);
            this.ckbAutoStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckbAutoStart.Name = "ckbAutoStart";
            this.ckbAutoStart.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckbAutoStart.Size = new System.Drawing.Size(150, 29);
            this.ckbAutoStart.TabIndex = 3;
            this.ckbAutoStart.Text = "开机启动";
            this.ckbAutoStart.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ckbAutoOpen
            // 
            this.ckbAutoOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckbAutoOpen.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbAutoOpen.Location = new System.Drawing.Point(52, 107);
            this.ckbAutoOpen.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckbAutoOpen.Name = "ckbAutoOpen";
            this.ckbAutoOpen.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckbAutoOpen.Size = new System.Drawing.Size(221, 29);
            this.ckbAutoOpen.TabIndex = 3;
            this.ckbAutoOpen.Text = "启动后打开上次工程";
            this.ckbAutoOpen.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FSysConfig
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(543, 431);
            this.Controls.Add(this.ckbAutoOpen);
            this.Controls.Add(this.ckbAutoStart);
            this.Name = "FSysConfig";
            this.Text = "系统设置";
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.ckbAutoStart, 0);
            this.Controls.SetChildIndex(this.ckbAutoOpen, 0);
            this.pnlBtm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Sunny.UI.UICheckBox ckbAutoStart;
        private Sunny.UI.UICheckBox ckbAutoOpen;
    }
}