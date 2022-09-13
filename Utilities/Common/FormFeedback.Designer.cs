namespace Utilities
{
    partial class FormFeedback
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
            this.gmButton1 = new Utilities.UI.GMButton();
            this.gmButton2 = new Utilities.UI.GMButton();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gmButton1
            // 
            this.gmButton1.AutoSize = false;
            this.gmButton1.ForeImage = null;
            this.gmButton1.ForeImageSize = new System.Drawing.Size(0, 0);
            this.gmButton1.ForePathAlign = Utilities.UI.ButtonImageAlignment.Left;
            this.gmButton1.ForePathGetter = null;
            this.gmButton1.ForePathSize = new System.Drawing.Size(0, 0);
            this.gmButton1.Image = null;
            this.gmButton1.Location = new System.Drawing.Point(103, 384);
            this.gmButton1.Name = "gmButton1";
            this.gmButton1.Size = new System.Drawing.Size(75, 23);
            this.gmButton1.SpaceBetweenPathAndText = 0;
            this.gmButton1.TabIndex = 0;
            this.gmButton1.Text = "发送";
            this.gmButton1.UseVisualStyleBackColor = true;
            this.gmButton1.Click += new System.EventHandler(this.gmButton1_Click);
            // 
            // gmButton2
            // 
            this.gmButton2.AutoSize = false;
            this.gmButton2.ForeImage = null;
            this.gmButton2.ForeImageSize = new System.Drawing.Size(0, 0);
            this.gmButton2.ForePathAlign = Utilities.UI.ButtonImageAlignment.Left;
            this.gmButton2.ForePathGetter = null;
            this.gmButton2.ForePathSize = new System.Drawing.Size(0, 0);
            this.gmButton2.Image = null;
            this.gmButton2.Location = new System.Drawing.Point(219, 384);
            this.gmButton2.Name = "gmButton2";
            this.gmButton2.Size = new System.Drawing.Size(75, 23);
            this.gmButton2.SpaceBetweenPathAndText = 0;
            this.gmButton2.TabIndex = 0;
            this.gmButton2.Text = "取消";
            this.gmButton2.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "问题或意见";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(20, 58);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(368, 233);
            this.textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(20, 317);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(368, 52);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 302);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "您的联系方式";
            // 
            // FormFeedback
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 429);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gmButton2);
            this.Controls.Add(this.gmButton1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.IconLeftMargin = 0;
            this.IconSize = new System.Drawing.Size(0, 0);
            this.IconTopMargin = 0;
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(411, 429);
            this.MinimizeBox = false;
            this.Name = "FormFeedback";
            this.ShowIcon = false;
            this.Text = "反馈";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.GMButton gmButton1;
        private UI.GMButton gmButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
    }
}