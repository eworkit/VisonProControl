namespace VisionControl
{
    partial class UCSerialPort
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
            this.tbPort = new Sunny.UI.UIComboBox();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.cbBaudRate = new Sunny.UI.UIComboBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.cbDataBit = new Sunny.UI.UIComboBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.cbParity = new Sunny.UI.UIComboBox();
            this.cbStopBit = new Sunny.UI.UIComboBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.btnClear = new System.Windows.Forms.Button();
            this.ckRcvHex = new Sunny.UI.UICheckBox();
            this.ckSentHex = new Sunny.UI.UICheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbSentData = new Sunny.UI.UIRichTextBox();
            this.tbRcvData = new Sunny.UI.UIRichTextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbPort
            // 
            this.tbPort.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.tbPort.DataSource = null;
            this.tbPort.FillColor = System.Drawing.Color.White;
            this.tbPort.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.tbPort.Location = new System.Drawing.Point(98, 18);
            this.tbPort.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPort.MinimumSize = new System.Drawing.Size(1, 16);
            this.tbPort.Name = "tbPort";
            this.tbPort.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.tbPort.ShowText = false;
            this.tbPort.Size = new System.Drawing.Size(150, 33);
            this.tbPort.TabIndex = 4;
            this.tbPort.Text = "COM1";
            this.tbPort.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbPort.Watermark = "";
            this.tbPort.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel5
            // 
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel5.Location = new System.Drawing.Point(-7, 20);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(96, 23);
            this.uiLabel5.TabIndex = 3;
            this.uiLabel5.Text = "端口";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel5.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel1.Location = new System.Drawing.Point(-7, 57);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(96, 23);
            this.uiLabel1.TabIndex = 5;
            this.uiLabel1.Text = "波特率";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel1.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.DataSource = null;
            this.cbBaudRate.FillColor = System.Drawing.Color.White;
            this.cbBaudRate.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBaudRate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "445200"});
            this.cbBaudRate.Location = new System.Drawing.Point(98, 55);
            this.cbBaudRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbBaudRate.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbBaudRate.Size = new System.Drawing.Size(150, 33);
            this.cbBaudRate.TabIndex = 6;
            this.cbBaudRate.Text = "9600";
            this.cbBaudRate.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbBaudRate.Watermark = "";
            this.cbBaudRate.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel2
            // 
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel2.Location = new System.Drawing.Point(-7, 94);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(96, 23);
            this.uiLabel2.TabIndex = 5;
            this.uiLabel2.Text = "数据位";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel2.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbDataBit
            // 
            this.cbDataBit.DataSource = null;
            this.cbDataBit.FillColor = System.Drawing.Color.White;
            this.cbDataBit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbDataBit.Items.AddRange(new object[] {
            "5",
            "6",
            "7",
            "8"});
            this.cbDataBit.Location = new System.Drawing.Point(98, 92);
            this.cbDataBit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDataBit.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbDataBit.Name = "cbDataBit";
            this.cbDataBit.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbDataBit.Size = new System.Drawing.Size(150, 33);
            this.cbDataBit.TabIndex = 6;
            this.cbDataBit.Text = "8";
            this.cbDataBit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbDataBit.Watermark = "";
            this.cbDataBit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel3
            // 
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel3.Location = new System.Drawing.Point(-7, 131);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(96, 23);
            this.uiLabel3.TabIndex = 5;
            this.uiLabel3.Text = "校验位";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel3.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbParity
            // 
            this.cbParity.DataSource = null;
            this.cbParity.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbParity.FillColor = System.Drawing.Color.White;
            this.cbParity.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbParity.Location = new System.Drawing.Point(98, 129);
            this.cbParity.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbParity.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbParity.Name = "cbParity";
            this.cbParity.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbParity.Size = new System.Drawing.Size(150, 33);
            this.cbParity.TabIndex = 6;
            this.cbParity.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbParity.Watermark = "";
            this.cbParity.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // cbStopBit
            // 
            this.cbStopBit.DataSource = null;
            this.cbStopBit.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.cbStopBit.FillColor = System.Drawing.Color.White;
            this.cbStopBit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbStopBit.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2"});
            this.cbStopBit.Location = new System.Drawing.Point(98, 166);
            this.cbStopBit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbStopBit.MinimumSize = new System.Drawing.Size(63, 0);
            this.cbStopBit.Name = "cbStopBit";
            this.cbStopBit.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.cbStopBit.Size = new System.Drawing.Size(150, 33);
            this.cbStopBit.TabIndex = 8;
            this.cbStopBit.Text = "1";
            this.cbStopBit.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.cbStopBit.Watermark = "";
            this.cbStopBit.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel4
            // 
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel4.Location = new System.Drawing.Point(-7, 168);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(96, 23);
            this.uiLabel4.TabIndex = 7;
            this.uiLabel4.Text = "停止位";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.uiLabel4.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnClear
            // 
            this.btnClear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClear.Location = new System.Drawing.Point(371, 200);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(81, 29);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "清空";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // ckRcvHex
            // 
            this.ckRcvHex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckRcvHex.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ckRcvHex.Location = new System.Drawing.Point(137, 206);
            this.ckRcvHex.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckRcvHex.Name = "ckRcvHex";
            this.ckRcvHex.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckRcvHex.Size = new System.Drawing.Size(150, 23);
            this.ckRcvHex.TabIndex = 19;
            this.ckRcvHex.Text = "十六进制接收";
            this.ckRcvHex.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // ckSentHex
            // 
            this.ckSentHex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ckSentHex.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.ckSentHex.Location = new System.Drawing.Point(137, 422);
            this.ckSentHex.MinimumSize = new System.Drawing.Size(1, 1);
            this.ckSentHex.Name = "ckSentHex";
            this.ckSentHex.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.ckSentHex.Size = new System.Drawing.Size(150, 23);
            this.ckSentHex.TabIndex = 18;
            this.ckSentHex.Text = "十六进制发送";
            this.ckSentHex.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSend.Location = new System.Drawing.Point(8, 632);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(81, 29);
            this.btnSend.TabIndex = 17;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbSentData
            // 
            this.tbSentData.FillColor = System.Drawing.Color.White;
            this.tbSentData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbSentData.Location = new System.Drawing.Point(8, 450);
            this.tbSentData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbSentData.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbSentData.Name = "tbSentData";
            this.tbSentData.Padding = new System.Windows.Forms.Padding(2);
            this.tbSentData.ShowText = false;
            this.tbSentData.Size = new System.Drawing.Size(446, 174);
            this.tbSentData.Style = Sunny.UI.UIStyle.Custom;
            this.tbSentData.TabIndex = 15;
            this.tbSentData.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.tbSentData.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // tbRcvData
            // 
            this.tbRcvData.FillColor = System.Drawing.Color.White;
            this.tbRcvData.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRcvData.Location = new System.Drawing.Point(8, 234);
            this.tbRcvData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbRcvData.MinimumSize = new System.Drawing.Size(1, 1);
            this.tbRcvData.Name = "tbRcvData";
            this.tbRcvData.Padding = new System.Windows.Forms.Padding(2);
            this.tbRcvData.ShowText = false;
            this.tbRcvData.Size = new System.Drawing.Size(446, 174);
            this.tbRcvData.Style = Sunny.UI.UIStyle.Custom;
            this.tbRcvData.TabIndex = 16;
            this.tbRcvData.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.tbRcvData.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel6
            // 
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel6.Location = new System.Drawing.Point(3, 422);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(128, 23);
            this.uiLabel6.TabIndex = 13;
            this.uiLabel6.Text = "发送的数据";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel6.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // uiLabel7
            // 
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel7.Location = new System.Drawing.Point(3, 206);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(128, 23);
            this.uiLabel7.TabIndex = 14;
            this.uiLabel7.Text = "接收的数据";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.uiLabel7.ZoomScaleRect = new System.Drawing.Rectangle(0, 0, 0, 0);
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.Location = new System.Drawing.Point(287, 18);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(81, 29);
            this.btnOpen.TabIndex = 21;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClose.Location = new System.Drawing.Point(384, 18);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 29);
            this.btnClose.TabIndex = 22;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // UCSerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.ckRcvHex);
            this.Controls.Add(this.ckSentHex);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbSentData);
            this.Controls.Add(this.tbRcvData);
            this.Controls.Add(this.uiLabel6);
            this.Controls.Add(this.uiLabel7);
            this.Controls.Add(this.cbStopBit);
            this.Controls.Add(this.uiLabel4);
            this.Controls.Add(this.cbParity);
            this.Controls.Add(this.cbDataBit);
            this.Controls.Add(this.uiLabel3);
            this.Controls.Add(this.uiLabel2);
            this.Controls.Add(this.cbBaudRate);
            this.Controls.Add(this.uiLabel1);
            this.Controls.Add(this.tbPort);
            this.Controls.Add(this.uiLabel5);
            this.Name = "UCSerialPort";
            this.Size = new System.Drawing.Size(482, 682);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIComboBox tbPort;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIComboBox cbBaudRate;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIComboBox cbDataBit;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UIComboBox cbParity;
        private Sunny.UI.UIComboBox cbStopBit;
        private Sunny.UI.UILabel uiLabel4;
        private System.Windows.Forms.Button btnClear;
        private Sunny.UI.UICheckBox ckRcvHex;
        private Sunny.UI.UICheckBox ckSentHex;
        private System.Windows.Forms.Button btnSend;
        private Sunny.UI.UIRichTextBox tbSentData;
        private Sunny.UI.UIRichTextBox tbRcvData;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UILabel uiLabel7;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
    }
}
