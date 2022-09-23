namespace VisionApplication
{
    partial class FLogin
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
            this.rbAdmin = new Sunny.UI.UIRadioButton();
            this.rbOperator = new Sunny.UI.UIRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbPwd = new System.Windows.Forms.TextBox();
            this.tbNewPass1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNewPass2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnModify = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.uiCheckBox1 = new Sunny.UI.UICheckBox();
            this.pnlBtm.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtm
            // 
            this.pnlBtm.Controls.Add(this.uiCheckBox1);
            this.pnlBtm.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pnlBtm.Location = new System.Drawing.Point(1, 259);
            this.pnlBtm.Size = new System.Drawing.Size(451, 55);
            this.pnlBtm.Controls.SetChildIndex(this.btnOK, 0);
            this.pnlBtm.Controls.SetChildIndex(this.btnCancel, 0);
            this.pnlBtm.Controls.SetChildIndex(this.uiCheckBox1, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(323, 12);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(208, 12);
            this.btnOK.Text = "登录";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // rbAdmin
            // 
            this.rbAdmin.Checked = true;
            this.rbAdmin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbAdmin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbAdmin.Location = new System.Drawing.Point(81, 47);
            this.rbAdmin.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbAdmin.Name = "rbAdmin";
            this.rbAdmin.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbAdmin.Size = new System.Drawing.Size(116, 29);
            this.rbAdmin.TabIndex = 2;
            this.rbAdmin.Text = "管理员";
            this.rbAdmin.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // rbOperator
            // 
            this.rbOperator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbOperator.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rbOperator.Location = new System.Drawing.Point(209, 47);
            this.rbOperator.MinimumSize = new System.Drawing.Size(1, 1);
            this.rbOperator.Name = "rbOperator";
            this.rbOperator.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.rbOperator.Size = new System.Drawing.Size(119, 29);
            this.rbOperator.TabIndex = 2;
            this.rbOperator.Text = "操作员";
            this.rbOperator.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(59, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "密码";
            // 
            // tbPwd
            // 
            this.tbPwd.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPwd.Location = new System.Drawing.Point(117, 107);
            this.tbPwd.Name = "tbPwd";
            this.tbPwd.PasswordChar = '*';
            this.tbPwd.Size = new System.Drawing.Size(178, 31);
            this.tbPwd.TabIndex = 4;
            // 
            // tbNewPass1
            // 
            this.tbNewPass1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbNewPass1.Location = new System.Drawing.Point(120, 11);
            this.tbNewPass1.Name = "tbNewPass1";
            this.tbNewPass1.PasswordChar = '*';
            this.tbNewPass1.Size = new System.Drawing.Size(178, 31);
            this.tbNewPass1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(42, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "新密码";
            // 
            // tbNewPass2
            // 
            this.tbNewPass2.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbNewPass2.Location = new System.Drawing.Point(120, 54);
            this.tbNewPass2.Name = "tbNewPass2";
            this.tbNewPass2.PasswordChar = '*';
            this.tbNewPass2.Size = new System.Drawing.Size(178, 31);
            this.tbNewPass2.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(26, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "确认密码";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnModify);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbNewPass2);
            this.panel1.Controls.Add(this.tbNewPass1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(16, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(408, 100);
            this.panel1.TabIndex = 9;
            this.panel1.Visible = false;
            // 
            // btnModify
            // 
            this.btnModify.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnModify.Location = new System.Drawing.Point(314, 52);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(87, 34);
            this.btnModify.TabIndex = 9;
            this.btnModify.Text = "修改";
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("微软雅黑 Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linkLabel1.LinkColor = System.Drawing.Color.Black;
            this.linkLabel1.Location = new System.Drawing.Point(319, 108);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(92, 27);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "修改密码";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // uiCheckBox1
            // 
            this.uiCheckBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiCheckBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiCheckBox1.Location = new System.Drawing.Point(46, 12);
            this.uiCheckBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiCheckBox1.Name = "uiCheckBox1";
            this.uiCheckBox1.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.uiCheckBox1.Size = new System.Drawing.Size(150, 29);
            this.uiCheckBox1.TabIndex = 2;
            this.uiCheckBox1.Text = "自动登录";
            this.uiCheckBox1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // FLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(453, 317);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tbPwd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbOperator);
            this.Controls.Add(this.rbAdmin);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MaximumSize = new System.Drawing.Size(614, 317);
            this.Name = "FLogin";
            this.Text = "登录";
            this.TitleFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ZoomScaleRect = new System.Drawing.Rectangle(19, 19, 614, 411);
            this.Controls.SetChildIndex(this.rbAdmin, 0);
            this.Controls.SetChildIndex(this.rbOperator, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.tbPwd, 0);
            this.Controls.SetChildIndex(this.panel1, 0);
            this.Controls.SetChildIndex(this.linkLabel1, 0);
            this.Controls.SetChildIndex(this.pnlBtm, 0);
            this.pnlBtm.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.UIRadioButton rbAdmin;
        private Sunny.UI.UIRadioButton rbOperator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbPwd;
        private System.Windows.Forms.TextBox tbNewPass1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNewPass2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private Sunny.UI.UICheckBox uiCheckBox1;
    }
}