namespace VisionControl
{
    partial class UcTcpServer
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.tbPort = new Sunny.UI.UITextBox();
            this.btnTcpSvrOpen = new System.Windows.Forms.Button();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.tbIP = new Sunny.UI.UITextBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.lbRcvClients = new Sunny.UI.UIListBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.tbRcvData = new Sunny.UI.UIRichTextBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.tbSendData = new Sunny.UI.UIRichTextBox();
            this.btnTcpSvrRcvClear = new System.Windows.Forms.Button();
            this.ckRcvHex = new Sunny.UI.UICheckBox();
            this.ckSentHex = new Sunny.UI.UICheckBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.uiDoubleUpDown1 = new Sunny.UI.UIDoubleUpDown();
            this.ckTimeToSent = new Sunny.UI.UICheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(3, 12);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(76, 23);
            this.uiLabel5.TabIndex = 1;
            this.uiLabel5.Text = "端口";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbPort
            // 
            this.tbPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbPort.DoubleValue = 3000D;
            this.tbPort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPort.IntValue = 3000;
            this.tbPort.Location = new System.Drawing.Point(76, 11);
            this.tbPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPort.MinimumSize = new System.Drawing.Size(1, 16);
            this.tbPort.Name = "tbPort";
            this.tbPort.ShowText = false;
            this.tbPort.Size = new System.Drawing.Size(150, 29);
            this.tbPort.TabIndex = 2;
            this.tbPort.Text = "3000";
            this.tbPort.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbPort.Watermark = "";
            this.tbPort.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnTcpSvrOpen
            // 
            this.btnTcpSvrOpen.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTcpSvrOpen.Location = new System.Drawing.Point(245, 10);
            this.btnTcpSvrOpen.Name = "btnTcpSvrOpen";
            this.btnTcpSvrOpen.Size = new System.Drawing.Size(81, 29);
            this.btnTcpSvrOpen.TabIndex = 3;
            this.btnTcpSvrOpen.Text = "开启";
            this.btnTcpSvrOpen.UseVisualStyleBackColor = true;
            this.btnTcpSvrOpen.Click += new System.EventHandler(this.btnTcpSvrOpen_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(-4, 49);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(83, 23);
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.Text = "本机IP";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbIP
            // 
            this.tbIP.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbIP.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbIP.Location = new System.Drawing.Point(77, 49);
            this.tbIP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbIP.MinimumSize = new System.Drawing.Size(1, 16);
            this.tbIP.Name = "tbIP";
            this.tbIP.ShowText = false;
            this.tbIP.Size = new System.Drawing.Size(250, 29);
            this.tbIP.TabIndex = 2;
            this.tbIP.Text = "127.0.0.1";
            this.tbIP.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbIP.Watermark = "";
            this.tbIP.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.Location = new System.Drawing.Point(5, 82);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(128, 23);
            this.uiLabel2.TabIndex = 1;
            this.uiLabel2.Text = "连接的客户";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // lbRcvClients
            // 
            this.lbRcvClients.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRcvClients.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(200)))), ((int)(((byte)(255)))));
            this.lbRcvClients.ItemSelectForeColor = System.Drawing.Color.White;
            this.lbRcvClients.Location = new System.Drawing.Point(10, 110);
            this.lbRcvClients.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbRcvClients.MinimumSize = new System.Drawing.Size(1, 1);
            this.lbRcvClients.Name = "lbRcvClients";
            this.lbRcvClients.Padding = new System.Windows.Forms.Padding(2);
            this.lbRcvClients.ShowText = false;
            this.lbRcvClients.Size = new System.Drawing.Size(446, 164);
            this.lbRcvClients.Style = Sunny.UI.UIStyle.Custom;
            this.lbRcvClients.TabIndex = 4;
            this.lbRcvClients.Text = "uiListBox1";
            this.lbRcvClients.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(5, 276);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(128, 23);
            this.uiLabel3.TabIndex = 1;
            this.uiLabel3.Text = "接收的数据";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbRcvData
            // 
            this.tbRcvData.FillColor = System.Drawing.Color.White;
            this.tbRcvData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRcvData.Location = new System.Drawing.Point(10, 306);
            this.tbRcvData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbRcvData.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbRcvData.Name = "tbRcvData";
            this.tbRcvData.Padding = new System.Windows.Forms.Padding(2);
            this.tbRcvData.ShowText = false;
            this.tbRcvData.Size = new System.Drawing.Size(446, 174);
            this.tbRcvData.Style = Sunny.UI.UIStyle.Custom;
            this.tbRcvData.TabIndex = 5;
            this.tbRcvData.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.tbRcvData.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(18, 485);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(128, 23);
            this.uiLabel4.TabIndex = 1;
            this.uiLabel4.Text = "发送的数据";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbSendData
            // 
            this.tbSendData.FillColor = System.Drawing.Color.White;
            this.tbSendData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSendData.Location = new System.Drawing.Point(10, 513);
            this.tbSendData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbSendData.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbSendData.Name = "tbSendData";
            this.tbSendData.Padding = new System.Windows.Forms.Padding(2);
            this.tbSendData.ShowText = false;
            this.tbSendData.Size = new System.Drawing.Size(446, 174);
            this.tbSendData.Style = Sunny.UI.UIStyle.Custom;
            this.tbSendData.TabIndex = 5;
            this.tbSendData.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.tbSendData.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnTcpSvrRcvClear
            // 
            this.btnTcpSvrRcvClear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTcpSvrRcvClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTcpSvrRcvClear.Location = new System.Drawing.Point(365, 276);
            this.btnTcpSvrRcvClear.Name = "btnTcpSvrRcvClear";
            this.btnTcpSvrRcvClear.Size = new System.Drawing.Size(81, 29);
            this.btnTcpSvrRcvClear.TabIndex = 3;
            this.btnTcpSvrRcvClear.Text = "清空";
            this.btnTcpSvrRcvClear.UseVisualStyleBackColor = true;
            this.btnTcpSvrRcvClear.Click += new System.EventHandler(this.btnTcpSvrRcvClear_Click);
            // 
            // ckRcvHex
            // 
            this.ckRcvHex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckRcvHex.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ckRcvHex.Location = new System.Drawing.Point(137, 280);
            this.ckRcvHex.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckRcvHex.Name = "ckRcvHex";
            this.ckRcvHex.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckRcvHex.Size = new System.Drawing.Size(150, 23);
            this.ckRcvHex.TabIndex = 11;
            this.ckRcvHex.Text = "十六进制接收";
            this.ckRcvHex.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ckSentHex
            // 
            this.ckSentHex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckSentHex.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ckSentHex.Location = new System.Drawing.Point(137, 488);
            this.ckSentHex.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckSentHex.Name = "ckSentHex";
            this.ckSentHex.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckSentHex.Size = new System.Drawing.Size(150, 23);
            this.ckSentHex.TabIndex = 12;
            this.ckSentHex.Text = "十六进制发送";
            this.ckSentHex.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(342, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 29);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "停止";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // uiLabel6
            // 
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiLabel6.Location = new System.Drawing.Point(365, 698);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(54, 23);
            this.uiLabel6.TabIndex = 17;
            this.uiLabel6.Text = "ms";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel6.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiDoubleUpDown1
            // 
            this.uiDoubleUpDown1.DecimalPlaces = 0;
            this.uiDoubleUpDown1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiDoubleUpDown1.Location = new System.Drawing.Point(231, 697);
            this.uiDoubleUpDown1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiDoubleUpDown1.MinimumSize = new System.Drawing.Size(100, 0);
            this.uiDoubleUpDown1.Name = "uiDoubleUpDown1";
            this.uiDoubleUpDown1.ShowText = false;
            this.uiDoubleUpDown1.Size = new System.Drawing.Size(129, 29);
            this.uiDoubleUpDown1.Step = 10D;
            this.uiDoubleUpDown1.TabIndex = 16;
            this.uiDoubleUpDown1.Text = "uiDoubleUpDown1";
            this.uiDoubleUpDown1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiDoubleUpDown1.Value = 500D;
            this.uiDoubleUpDown1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ckTimeToSent
            // 
            this.ckTimeToSent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckTimeToSent.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ckTimeToSent.Location = new System.Drawing.Point(109, 695);
            this.ckTimeToSent.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckTimeToSent.Name = "ckTimeToSent";
            this.ckTimeToSent.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckTimeToSent.Size = new System.Drawing.Size(150, 29);
            this.ckTimeToSent.TabIndex = 15;
            this.ckTimeToSent.Text = "定时发送";
            this.ckTimeToSent.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(10, 695);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(81, 29);
            this.btnSend.TabIndex = 14;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // UcTcpServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.uiLabel6);
            this.Controls.Add(this.uiDoubleUpDown1);
            this.Controls.Add(this.ckTimeToSent);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ckSentHex);
            this.Controls.Add(this.ckRcvHex);
            this.Controls.Add(this.tbSendData);
            this.Controls.Add(this.tbRcvData);
            this.Controls.Add(this.lbRcvClients);
            this.Controls.Add(this.btnTcpSvrRcvClear);
            this.Controls.Add(this.btnTcpSvrOpen);
            this.Controls.Add(this.tbIP);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.uiLabel5);
            this.MinimumSize = new System.Drawing.Size(400, 0);
            this.Name = "UcTcpServer";
            this.Size = new System.Drawing.Size(470, 747);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UITextBox tbPort;
        private System.Windows.Forms.Button btnTcpSvrOpen;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox tbIP;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIListBox lbRcvClients;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIRichTextBox tbRcvData;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIRichTextBox tbSendData;
        private System.Windows.Forms.Button btnTcpSvrRcvClear;
        private Sunny.UI.UICheckBox ckRcvHex;
        private Sunny.UI.UICheckBox ckSentHex;
        private System.Windows.Forms.Button btnClose;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UIDoubleUpDown uiDoubleUpDown1;
        private Sunny.UI.UICheckBox ckTimeToSent;
        private System.Windows.Forms.Button btnSend;
    }
}
