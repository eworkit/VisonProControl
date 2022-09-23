namespace VisionControl
{
    partial class UcTcpClient
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
            this.btnConnect = new System.Windows.Forms.Button();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.tbServer = new Sunny.UI.UITextBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.tbRcvData = new Sunny.UI.UIRichTextBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.tbSendData = new Sunny.UI.UIRichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.ckTimeToSent = new Sunny.UI.UICheckBox();
            this.uiDoubleUpDown1 = new Sunny.UI.UIDoubleUpDown();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.btnDisCon = new System.Windows.Forms.Button();
            this.ckSentHex = new Sunny.UI.UICheckBox();
            this.ckRcvHex = new Sunny.UI.UICheckBox();
            this.ckAutoConn = new Sunny.UI.UICheckBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.ckbAutoStart = new Sunny.UI.UICheckBox();
            this.btnSave = new Sunny.UI.UIButton();
            this.SuspendLayout();
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel5.Location = new System.Drawing.Point(6, 44);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(71, 23);
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
            this.tbPort.Location = new System.Drawing.Point(80, 43);
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
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConnect.Location = new System.Drawing.Point(282, 42);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(81, 29);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel1.Location = new System.Drawing.Point(2, 5);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(75, 23);
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.Text = "客户端";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbServer
            // 
            this.tbServer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbServer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbServer.Location = new System.Drawing.Point(80, 5);
            this.tbServer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbServer.MinimumSize = new System.Drawing.Size(1, 16);
            this.tbServer.Name = "tbServer";
            this.tbServer.ShowText = false;
            this.tbServer.Size = new System.Drawing.Size(250, 29);
            this.tbServer.TabIndex = 2;
            this.tbServer.Text = "127.0.0.1";
            this.tbServer.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbServer.Watermark = "";
            this.tbServer.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(4, 113);
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
            this.tbRcvData.Location = new System.Drawing.Point(9, 141);
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
            this.uiLabel4.Location = new System.Drawing.Point(4, 329);
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
            this.tbSendData.Location = new System.Drawing.Point(9, 357);
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
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(9, 539);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(81, 29);
            this.btnSend.TabIndex = 6;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ckTimeToSent
            // 
            this.ckTimeToSent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckTimeToSent.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.ckTimeToSent.Location = new System.Drawing.Point(122, 539);
            this.ckTimeToSent.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckTimeToSent.Name = "ckTimeToSent";
            this.ckTimeToSent.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckTimeToSent.Size = new System.Drawing.Size(150, 29);
            this.ckTimeToSent.TabIndex = 7;
            this.ckTimeToSent.Text = "定时发送";
            this.ckTimeToSent.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.ckTimeToSent.CheckedChanged += new System.EventHandler(this.ckTimeToSent_CheckedChanged);
            // 
            // uiDoubleUpDown1
            // 
            this.uiDoubleUpDown1.DecimalPlaces = 0;
            this.uiDoubleUpDown1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiDoubleUpDown1.Location = new System.Drawing.Point(244, 541);
            this.uiDoubleUpDown1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiDoubleUpDown1.MinimumSize = new System.Drawing.Size(100, 0);
            this.uiDoubleUpDown1.Name = "uiDoubleUpDown1";
            this.uiDoubleUpDown1.ShowText = false;
            this.uiDoubleUpDown1.Size = new System.Drawing.Size(129, 29);
            this.uiDoubleUpDown1.Step = 10D;
            this.uiDoubleUpDown1.TabIndex = 8;
            this.uiDoubleUpDown1.Text = "uiDoubleUpDown1";
            this.uiDoubleUpDown1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.uiDoubleUpDown1.Value = 500D;
            this.uiDoubleUpDown1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.uiDoubleUpDown1.ValueChanged += new Sunny.UI.UIDoubleUpDown.OnValueChanged(this.uiDoubleUpDown1_ValueChanged);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.uiLabel2.Location = new System.Drawing.Point(378, 542);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(54, 23);
            this.uiLabel2.TabIndex = 9;
            this.uiLabel2.Text = "ms";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnDisCon
            // 
            this.btnDisCon.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDisCon.Location = new System.Drawing.Point(370, 42);
            this.btnDisCon.Name = "btnDisCon";
            this.btnDisCon.Size = new System.Drawing.Size(81, 29);
            this.btnDisCon.TabIndex = 3;
            this.btnDisCon.Text = "断开";
            this.btnDisCon.UseVisualStyleBackColor = true;
            this.btnDisCon.Click += new System.EventHandler(this.btnDisCon_Click);
            // 
            // ckSentHex
            // 
            this.ckSentHex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckSentHex.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ckSentHex.Location = new System.Drawing.Point(138, 329);
            this.ckSentHex.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckSentHex.Name = "ckSentHex";
            this.ckSentHex.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckSentHex.Size = new System.Drawing.Size(150, 23);
            this.ckSentHex.TabIndex = 7;
            this.ckSentHex.Text = "十六进制发送";
            this.ckSentHex.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ckRcvHex
            // 
            this.ckRcvHex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckRcvHex.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ckRcvHex.Location = new System.Drawing.Point(138, 113);
            this.ckRcvHex.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckRcvHex.Name = "ckRcvHex";
            this.ckRcvHex.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckRcvHex.Size = new System.Drawing.Size(150, 23);
            this.ckRcvHex.TabIndex = 10;
            this.ckRcvHex.Text = "十六进制接收";
            this.ckRcvHex.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ckAutoConn
            // 
            this.ckAutoConn.Checked = true;
            this.ckAutoConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckAutoConn.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ckAutoConn.Location = new System.Drawing.Point(9, 80);
            this.ckAutoConn.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckAutoConn.Name = "ckAutoConn";
            this.ckAutoConn.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckAutoConn.Size = new System.Drawing.Size(150, 23);
            this.ckAutoConn.TabIndex = 11;
            this.ckAutoConn.Text = "断开后自动连接";
            this.ckAutoConn.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(372, 107);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(81, 29);
            this.btnClear.TabIndex = 12;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ckbAutoStart
            // 
            this.ckbAutoStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckbAutoStart.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ckbAutoStart.Location = new System.Drawing.Point(15, 589);
            this.ckbAutoStart.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckbAutoStart.Name = "ckbAutoStart";
            this.ckbAutoStart.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckbAutoStart.Size = new System.Drawing.Size(150, 29);
            this.ckbAutoStart.TabIndex = 21;
            this.ckbAutoStart.Text = "自动连接";
            this.ckbAutoStart.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSave
            // 
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.Location = new System.Drawing.Point(351, 584);
            this.btnSave.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 35);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "保存配置";
            this.btnSave.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSave.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // UcTcpClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ckbAutoStart);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.ckAutoConn);
            this.Controls.Add(this.ckRcvHex);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.uiDoubleUpDown1);
            this.Controls.Add(this.ckSentHex);
            this.Controls.Add(this.ckTimeToSent);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbSendData);
            this.Controls.Add(this.tbRcvData);
            this.Controls.Add(this.btnDisCon);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.uiLabel5);
            this.Name = "UcTcpClient";
            this.Size = new System.Drawing.Size(470, 622);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UITextBox tbPort;
        private System.Windows.Forms.Button btnConnect;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox tbServer;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIRichTextBox tbRcvData;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UIRichTextBox tbSendData;
        private System.Windows.Forms.Button btnSend;
        private Sunny.UI.UICheckBox ckTimeToSent;
        private Sunny.UI.UIDoubleUpDown uiDoubleUpDown1;
        private Sunny.UI.UILabel uiLabel2;
        private System.Windows.Forms.Button btnDisCon;
        private Sunny.UI.UICheckBox ckSentHex;
        private Sunny.UI.UICheckBox ckRcvHex;
        private Sunny.UI.UICheckBox ckAutoConn;
        private System.Windows.Forms.Button btnClear;
        private Sunny.UI.UICheckBox ckbAutoStart;
        private Sunny.UI.UIButton btnSave;
    }
}
