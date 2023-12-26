namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    partial class FrmTag
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
            lblTagname = new Label();
            txtTagname = new TextBox();
            ckbTagEnabled = new CheckBox();
            lblEnabled = new Label();
            btnClose = new Button();
            btnSave = new Button();
            btnAdd = new Button();
            txtTagCode = new TextBox();
            lblTagCode = new Label();
            txtIPAddress = new TextBox();
            lblIPAddress = new Label();
            lblTimeout = new Label();
            nudTimeout = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nudTimeout).BeginInit();
            SuspendLayout();
            // 
            // lblTagname
            // 
            lblTagname.AutoSize = true;
            lblTagname.Location = new Point(12, 15);
            lblTagname.Name = "lblTagname";
            lblTagname.Size = new Size(55, 15);
            lblTagname.TabIndex = 0;
            lblTagname.Text = "Tagname";
            // 
            // txtTagname
            // 
            txtTagname.Location = new Point(164, 12);
            txtTagname.Name = "txtTagname";
            txtTagname.Size = new Size(225, 23);
            txtTagname.TabIndex = 1;
            // 
            // ckbTagEnabled
            // 
            ckbTagEnabled.AutoSize = true;
            ckbTagEnabled.Location = new Point(164, 128);
            ckbTagEnabled.Name = "ckbTagEnabled";
            ckbTagEnabled.Size = new Size(15, 14);
            ckbTagEnabled.TabIndex = 4;
            ckbTagEnabled.UseVisualStyleBackColor = true;
            // 
            // lblEnabled
            // 
            lblEnabled.AutoSize = true;
            lblEnabled.Location = new Point(12, 127);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(49, 15);
            lblEnabled.TabIndex = 7;
            lblEnabled.Text = "Enabled";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(302, 149);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 27);
            btnClose.TabIndex = 10;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(206, 149);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 9;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(206, 149);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(88, 27);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtTagCode
            // 
            txtTagCode.Location = new Point(164, 41);
            txtTagCode.Name = "txtTagCode";
            txtTagCode.Size = new Size(225, 23);
            txtTagCode.TabIndex = 13;
            // 
            // lblTagCode
            // 
            lblTagCode.AutoSize = true;
            lblTagCode.Location = new Point(12, 44);
            lblTagCode.Name = "lblTagCode";
            lblTagCode.Size = new Size(54, 15);
            lblTagCode.TabIndex = 12;
            lblTagCode.Text = "Tag code";
            // 
            // txtIPAddress
            // 
            txtIPAddress.Location = new Point(164, 70);
            txtIPAddress.Name = "txtIPAddress";
            txtIPAddress.Size = new Size(225, 23);
            txtIPAddress.TabIndex = 15;
            // 
            // lblIPAddress
            // 
            lblIPAddress.AutoSize = true;
            lblIPAddress.Location = new Point(12, 73);
            lblIPAddress.Name = "lblIPAddress";
            lblIPAddress.Size = new Size(60, 15);
            lblIPAddress.TabIndex = 14;
            lblIPAddress.Text = "IP address";
            // 
            // lblTimeout
            // 
            lblTimeout.AutoSize = true;
            lblTimeout.Location = new Point(12, 102);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(51, 15);
            lblTimeout.TabIndex = 16;
            lblTimeout.Text = "Timeout";
            // 
            // nudTimeout
            // 
            nudTimeout.Location = new Point(164, 99);
            nudTimeout.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            nudTimeout.Name = "nudTimeout";
            nudTimeout.Size = new Size(225, 23);
            nudTimeout.TabIndex = 17;
            // 
            // FrmTag
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 188);
            Controls.Add(nudTimeout);
            Controls.Add(lblTimeout);
            Controls.Add(txtIPAddress);
            Controls.Add(lblIPAddress);
            Controls.Add(txtTagCode);
            Controls.Add(lblTagCode);
            Controls.Add(btnClose);
            Controls.Add(lblEnabled);
            Controls.Add(ckbTagEnabled);
            Controls.Add(txtTagname);
            Controls.Add(lblTagname);
            Controls.Add(btnAdd);
            Controls.Add(btnSave);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTag";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterParent;
            Load += FrmTag_Load;
            ((System.ComponentModel.ISupportInitialize)nudTimeout).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTagname;
        private TextBox txtTagname;
        private CheckBox ckbTagEnabled;
        private Label lblEnabled;
        private Button btnClose;
        private Button btnSave;
        private Button btnAdd;
        private TextBox txtTagCode;
        private Label lblTagCode;
        private TextBox txtIPAddress;
        private Label lblIPAddress;
        private Label lblTimeout;
        private NumericUpDown nudTimeout;
    }
}