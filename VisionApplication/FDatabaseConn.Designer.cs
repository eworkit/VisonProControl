namespace VisionApplication
{
    partial class FDatabaseConn
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FDatabaseConn));
            this.ucdbConfig1 = new VisionApplication.UCDBConfig();
            this.pnlBtm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlBtm.Location = new System.Drawing.Point(1, 300);
            this.pnlBtm.Size = new System.Drawing.Size(507, 55);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(379, 12);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(264, 12);
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // ucdbConfig1
            // 
            this.ucdbConfig1.DBInfo = ((Utilities.Data.DBConnInfo)(resources.GetObject("ucdbConfig1.DBInfo")));
            this.ucdbConfig1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucdbConfig1.Location = new System.Drawing.Point(30, 51);
            this.ucdbConfig1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.ucdbConfig1.Name = "ucdbConfig1";
            this.ucdbConfig1.Size = new System.Drawing.Size(429, 234);
            this.ucdbConfig1.TabIndex = 2;
            // 
            // FDatabaseConn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(509, 358);
            this.Controls.Add(this.ucdbConfig1);
            this.Name = "FDatabaseConn";
            this.Text = "数据库设置";
            this.TitleFont = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 800, 450);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.Controls.SetChildIndex(this.ucdbConfig1, 0);
            this.pnlBtm.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private UCDBConfig ucdbConfig1;
    }
}