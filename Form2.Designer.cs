namespace SerialSuite
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.label1 = new System.Windows.Forms.Label();
            this.labelParity = new System.Windows.Forms.Label();
            this.comboBoxParity = new System.Windows.Forms.ComboBox();
            this.labelRTS = new System.Windows.Forms.Label();
            this.labelHandshake = new System.Windows.Forms.Label();
            this.labelDataBits = new System.Windows.Forms.Label();
            this.labelStopbits = new System.Windows.Forms.Label();
            this.comboBoxStopBits = new System.Windows.Forms.ComboBox();
            this.comboBoxRTS = new System.Windows.Forms.ComboBox();
            this.comboBoxHandshake = new System.Windows.Forms.ComboBox();
            this.comboBoxDataBits = new System.Windows.Forms.ComboBox();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 14;
            // 
            // labelParity
            // 
            this.labelParity.AutoSize = true;
            this.labelParity.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelParity.Location = new System.Drawing.Point(15, 40);
            this.labelParity.Name = "labelParity";
            this.labelParity.Size = new System.Drawing.Size(33, 13);
            this.labelParity.TabIndex = 1;
            this.labelParity.Text = "Parity";
            // 
            // comboBoxParity
            // 
            this.comboBoxParity.FormattingEnabled = true;
            this.comboBoxParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.comboBoxParity.Location = new System.Drawing.Point(200, 40);
            this.comboBoxParity.Name = "comboBoxParity";
            this.comboBoxParity.Size = new System.Drawing.Size(120, 21);
            this.comboBoxParity.TabIndex = 2;
            this.comboBoxParity.Text = "None";
            this.comboBoxParity.SelectedIndexChanged += new System.EventHandler(this.ComboBoxParity_SelectedIndexChanged);
            // 
            // labelRTS
            // 
            this.labelRTS.AutoSize = true;
            this.labelRTS.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelRTS.Location = new System.Drawing.Point(15, 200);
            this.labelRTS.Name = "labelRTS";
            this.labelRTS.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelRTS.Size = new System.Drawing.Size(91, 13);
            this.labelRTS.TabIndex = 3;
            this.labelRTS.Text = "Request To Send";
            // 
            // labelHandshake
            // 
            this.labelHandshake.AutoSize = true;
            this.labelHandshake.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelHandshake.Location = new System.Drawing.Point(15, 160);
            this.labelHandshake.Name = "labelHandshake";
            this.labelHandshake.Size = new System.Drawing.Size(62, 13);
            this.labelHandshake.TabIndex = 4;
            this.labelHandshake.Text = "Handshake";
            // 
            // labelDataBits
            // 
            this.labelDataBits.AutoSize = true;
            this.labelDataBits.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelDataBits.Location = new System.Drawing.Point(15, 120);
            this.labelDataBits.Name = "labelDataBits";
            this.labelDataBits.Size = new System.Drawing.Size(50, 13);
            this.labelDataBits.TabIndex = 5;
            this.labelDataBits.Text = "Data Bits";
            // 
            // labelStopbits
            // 
            this.labelStopbits.AutoSize = true;
            this.labelStopbits.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelStopbits.Location = new System.Drawing.Point(15, 80);
            this.labelStopbits.Name = "labelStopbits";
            this.labelStopbits.Size = new System.Drawing.Size(49, 13);
            this.labelStopbits.TabIndex = 6;
            this.labelStopbits.Text = "Stop Bits";
            // 
            // comboBoxStopBits
            // 
            this.comboBoxStopBits.FormattingEnabled = true;
            this.comboBoxStopBits.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2",
            "None"});
            this.comboBoxStopBits.Location = new System.Drawing.Point(200, 80);
            this.comboBoxStopBits.Name = "comboBoxStopBits";
            this.comboBoxStopBits.Size = new System.Drawing.Size(120, 21);
            this.comboBoxStopBits.TabIndex = 7;
            this.comboBoxStopBits.Text = "1";
            this.comboBoxStopBits.SelectedIndexChanged += new System.EventHandler(this.ComboBoxStopBits_SelectedIndexChanged);
            // 
            // comboBoxRTS
            // 
            this.comboBoxRTS.FormattingEnabled = true;
            this.comboBoxRTS.Items.AddRange(new object[] {
            "True",
            "False"});
            this.comboBoxRTS.Location = new System.Drawing.Point(200, 200);
            this.comboBoxRTS.Name = "comboBoxRTS";
            this.comboBoxRTS.Size = new System.Drawing.Size(120, 21);
            this.comboBoxRTS.TabIndex = 8;
            this.comboBoxRTS.Text = "True";
            this.comboBoxRTS.SelectedIndexChanged += new System.EventHandler(this.ComboBoxRTS_SelectedIndexChanged);
            // 
            // comboBoxHandshake
            // 
            this.comboBoxHandshake.FormattingEnabled = true;
            this.comboBoxHandshake.Items.AddRange(new object[] {
            "None",
            "RequestToSend",
            "RequestToSendXOnXOff",
            "XOnXOff"});
            this.comboBoxHandshake.Location = new System.Drawing.Point(200, 160);
            this.comboBoxHandshake.Name = "comboBoxHandshake";
            this.comboBoxHandshake.Size = new System.Drawing.Size(120, 21);
            this.comboBoxHandshake.TabIndex = 9;
            this.comboBoxHandshake.Text = "None";
            this.comboBoxHandshake.SelectedIndexChanged += new System.EventHandler(this.ComboBoxHandshake_SelectedIndexChanged);
            // 
            // comboBoxDataBits
            // 
            this.comboBoxDataBits.FormattingEnabled = true;
            this.comboBoxDataBits.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.comboBoxDataBits.Location = new System.Drawing.Point(200, 120);
            this.comboBoxDataBits.Name = "comboBoxDataBits";
            this.comboBoxDataBits.Size = new System.Drawing.Size(120, 21);
            this.comboBoxDataBits.TabIndex = 10;
            this.comboBoxDataBits.Text = "8";
            this.comboBoxDataBits.SelectedIndexChanged += new System.EventHandler(this.ComboBoxDataBits_SelectedIndexChanged);
            // 
            // buttonConfirm
            // 
            this.buttonConfirm.Location = new System.Drawing.Point(245, 240);
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.Size = new System.Drawing.Size(75, 23);
            this.buttonConfirm.TabIndex = 11;
            this.buttonConfirm.Text = "Confirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.ButtonConfirm_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(15, 240);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 12;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // buttonDefault
            // 
            this.buttonDefault.Location = new System.Drawing.Point(110, 270);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(114, 23);
            this.buttonDefault.TabIndex = 13;
            this.buttonDefault.Text = "Reset to Default";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.ButtonDefault_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.comboBoxDataBits);
            this.Controls.Add(this.comboBoxHandshake);
            this.Controls.Add(this.comboBoxRTS);
            this.Controls.Add(this.comboBoxStopBits);
            this.Controls.Add(this.labelStopbits);
            this.Controls.Add(this.labelDataBits);
            this.Controls.Add(this.labelHandshake);
            this.Controls.Add(this.labelRTS);
            this.Controls.Add(this.comboBoxParity);
            this.Controls.Add(this.labelParity);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Advanced Options";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelParity;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.Label labelRTS;
        private System.Windows.Forms.Label labelHandshake;
        private System.Windows.Forms.Label labelDataBits;
        private System.Windows.Forms.Label labelStopbits;
        private System.Windows.Forms.ComboBox comboBoxStopBits;
        private System.Windows.Forms.ComboBox comboBoxRTS;
        private System.Windows.Forms.ComboBox comboBoxHandshake;
        private System.Windows.Forms.ComboBox comboBoxDataBits;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonDefault;
    }
}