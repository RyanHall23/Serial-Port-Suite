namespace SerialSuite
{
    partial class Form1
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
            this.buttonOptions = new System.Windows.Forms.Button();
            this.comboBoxBaud = new System.Windows.Forms.ComboBox();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.labelBaudrate = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelStatusMsg = new System.Windows.Forms.Label();
            this.tabControlViewer = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBoxHexView = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxRawViewer = new System.Windows.Forms.TextBox();
            this.tabControlViewer.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOptions
            // 
            this.buttonOptions.Location = new System.Drawing.Point(666, 15);
            this.buttonOptions.Name = "buttonOptions";
            this.buttonOptions.Size = new System.Drawing.Size(122, 23);
            this.buttonOptions.TabIndex = 6;
            this.buttonOptions.Text = "Advanced Options";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // comboBoxBaud
            // 
            this.comboBoxBaud.FormattingEnabled = true;
            this.comboBoxBaud.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200",
            "125000",
            "256000",
            "460800",
            "512000"});
            this.comboBoxBaud.Location = new System.Drawing.Point(412, 3);
            this.comboBoxBaud.Name = "comboBoxBaud";
            this.comboBoxBaud.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBaud.TabIndex = 0;
            this.comboBoxBaud.Text = "125000";
            this.comboBoxBaud.SelectedIndexChanged += new System.EventHandler(this.comboBoxBaud_SelectedIndexChanged);
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(412, 30);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPort.TabIndex = 1;
            this.comboBoxPort.Text = "0 Port(s) found";
            this.comboBoxPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxPort_SelectedIndexChanged);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(105, 13);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 2;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 13);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(199, 13);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 23);
            this.buttonPause.TabIndex = 4;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // labelBaudrate
            // 
            this.labelBaudrate.AutoSize = true;
            this.labelBaudrate.Location = new System.Drawing.Point(325, 6);
            this.labelBaudrate.Name = "labelBaudrate";
            this.labelBaudrate.Size = new System.Drawing.Size(50, 13);
            this.labelBaudrate.TabIndex = 7;
            this.labelBaudrate.Text = "Baudrate";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(325, 33);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(26, 13);
            this.labelPort.TabIndex = 8;
            this.labelPort.Text = "Port";
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(13, 431);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(43, 13);
            this.labelStatus.TabIndex = 9;
            this.labelStatus.Text = "Status :";
            // 
            // labelStatusMsg
            // 
            this.labelStatusMsg.AutoSize = true;
            this.labelStatusMsg.Location = new System.Drawing.Point(66, 431);
            this.labelStatusMsg.Name = "labelStatusMsg";
            this.labelStatusMsg.Size = new System.Drawing.Size(22, 13);
            this.labelStatusMsg.TabIndex = 10;
            this.labelStatusMsg.Text = "OK";
            // 
            // tabControlViewer
            // 
            this.tabControlViewer.Controls.Add(this.tabPage1);
            this.tabControlViewer.Controls.Add(this.tabPage2);
            this.tabControlViewer.Location = new System.Drawing.Point(12, 57);
            this.tabControlViewer.Name = "tabControlViewer";
            this.tabControlViewer.SelectedIndex = 0;
            this.tabControlViewer.ShowToolTips = true;
            this.tabControlViewer.Size = new System.Drawing.Size(776, 371);
            this.tabControlViewer.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBoxHexView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(768, 345);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "HEX";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBoxHexView
            // 
            this.textBoxHexView.Location = new System.Drawing.Point(7, 7);
            this.textBoxHexView.Multiline = true;
            this.textBoxHexView.Name = "textBoxHexView";
            this.textBoxHexView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHexView.Size = new System.Drawing.Size(755, 332);
            this.textBoxHexView.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxRawViewer);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(768, 345);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "RAW";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxRawViewer
            // 
            this.textBoxRawViewer.Location = new System.Drawing.Point(7, 7);
            this.textBoxRawViewer.Multiline = true;
            this.textBoxRawViewer.Name = "textBoxRawViewer";
            this.textBoxRawViewer.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRawViewer.Size = new System.Drawing.Size(755, 332);
            this.textBoxRawViewer.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlViewer);
            this.Controls.Add(this.labelStatusMsg);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelBaudrate);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.comboBoxPort);
            this.Controls.Add(this.comboBoxBaud);
            this.Name = "Form1";
            this.Text = "Serial Port Suite";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlViewer.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxBaud;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonOptions;
        private System.Windows.Forms.Label labelBaudrate;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelStatusMsg;
        private System.Windows.Forms.TabControl tabControlViewer;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBoxHexView;
        private System.Windows.Forms.TextBox textBoxRawViewer;
    }
}

