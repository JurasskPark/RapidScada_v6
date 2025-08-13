namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    partial class FrmActionLocalUploadDirectory
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
            ckbEnabled = new CheckBox();
            lblEnabled = new Label();
            lblAction = new Label();
            cmbAction = new ComboBox();
            lblLocalPath = new Label();
            txtLocalPath = new TextBox();
            txtRemotePath = new TextBox();
            lblRemotePath = new Label();
            cmbMode = new ComboBox();
            lblMode = new Label();
            cmbRemoteExists = new ComboBox();
            lblRemoteExists = new Label();
            txtFormats = new TextBox();
            lblFormats = new Label();
            nudSize = new NumericUpDown();
            cmbTypeSize = new ComboBox();
            lblSize = new Label();
            ckbSize = new CheckBox();
            btnCancel = new Button();
            btnSave = new Button();
            lblFtpVerify = new Label();
            cmbFtpVerify = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)nudSize).BeginInit();
            SuspendLayout();
            // 
            // ckbEnabled
            // 
            ckbEnabled.AutoSize = true;
            ckbEnabled.Location = new Point(171, 9);
            ckbEnabled.Name = "ckbEnabled";
            ckbEnabled.Size = new Size(15, 14);
            ckbEnabled.TabIndex = 147;
            ckbEnabled.UseVisualStyleBackColor = true;
            // 
            // lblEnabled
            // 
            lblEnabled.AutoSize = true;
            lblEnabled.Location = new Point(12, 9);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(49, 15);
            lblEnabled.TabIndex = 146;
            lblEnabled.Text = "Enabled";
            // 
            // lblAction
            // 
            lblAction.AutoSize = true;
            lblAction.Location = new Point(12, 32);
            lblAction.Name = "lblAction";
            lblAction.Size = new Size(42, 15);
            lblAction.TabIndex = 150;
            lblAction.Text = "Action";
            // 
            // cmbAction
            // 
            cmbAction.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAction.Enabled = false;
            cmbAction.FormattingEnabled = true;
            cmbAction.Location = new Point(171, 29);
            cmbAction.Name = "cmbAction";
            cmbAction.Size = new Size(315, 23);
            cmbAction.TabIndex = 151;
            // 
            // lblLocalPath
            // 
            lblLocalPath.AutoSize = true;
            lblLocalPath.Location = new Point(12, 60);
            lblLocalPath.Name = "lblLocalPath";
            lblLocalPath.Size = new Size(62, 15);
            lblLocalPath.TabIndex = 152;
            lblLocalPath.Text = "Local Path";
            // 
            // txtLocalPath
            // 
            txtLocalPath.Location = new Point(171, 57);
            txtLocalPath.Name = "txtLocalPath";
            txtLocalPath.Size = new Size(617, 23);
            txtLocalPath.TabIndex = 153;
            // 
            // txtRemotePath
            // 
            txtRemotePath.Location = new Point(171, 86);
            txtRemotePath.Name = "txtRemotePath";
            txtRemotePath.Size = new Size(617, 23);
            txtRemotePath.TabIndex = 154;
            // 
            // lblRemotePath
            // 
            lblRemotePath.AutoSize = true;
            lblRemotePath.Location = new Point(12, 89);
            lblRemotePath.Name = "lblRemotePath";
            lblRemotePath.Size = new Size(75, 15);
            lblRemotePath.TabIndex = 155;
            lblRemotePath.Text = "Remote Path";
            // 
            // cmbMode
            // 
            cmbMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMode.FormattingEnabled = true;
            cmbMode.Location = new Point(171, 115);
            cmbMode.Name = "cmbMode";
            cmbMode.Size = new Size(315, 23);
            cmbMode.TabIndex = 156;
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Location = new Point(12, 118);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(38, 15);
            lblMode.TabIndex = 157;
            lblMode.Text = "Mode";
            // 
            // cmbRemoteExists
            // 
            cmbRemoteExists.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRemoteExists.FormattingEnabled = true;
            cmbRemoteExists.Location = new Point(171, 144);
            cmbRemoteExists.Name = "cmbRemoteExists";
            cmbRemoteExists.Size = new Size(315, 23);
            cmbRemoteExists.TabIndex = 158;
            // 
            // lblRemoteExists
            // 
            lblRemoteExists.AutoSize = true;
            lblRemoteExists.Location = new Point(13, 147);
            lblRemoteExists.Name = "lblRemoteExists";
            lblRemoteExists.Size = new Size(80, 15);
            lblRemoteExists.TabIndex = 160;
            lblRemoteExists.Text = "Remote Exists";
            // 
            // txtFormats
            // 
            txtFormats.Location = new Point(171, 202);
            txtFormats.Name = "txtFormats";
            txtFormats.Size = new Size(315, 23);
            txtFormats.TabIndex = 162;
            // 
            // lblFormats
            // 
            lblFormats.AutoSize = true;
            lblFormats.Location = new Point(12, 205);
            lblFormats.Name = "lblFormats";
            lblFormats.Size = new Size(50, 15);
            lblFormats.TabIndex = 163;
            lblFormats.Text = "Formats";
            // 
            // nudSize
            // 
            nudSize.Location = new Point(192, 231);
            nudSize.Name = "nudSize";
            nudSize.Size = new Size(156, 23);
            nudSize.TabIndex = 164;
            // 
            // cmbTypeSize
            // 
            cmbTypeSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTypeSize.FormattingEnabled = true;
            cmbTypeSize.Location = new Point(351, 231);
            cmbTypeSize.Name = "cmbTypeSize";
            cmbTypeSize.Size = new Size(135, 23);
            cmbTypeSize.TabIndex = 165;
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new Point(12, 234);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(27, 15);
            lblSize.TabIndex = 166;
            lblSize.Text = "Size";
            // 
            // ckbSize
            // 
            ckbSize.AutoSize = true;
            ckbSize.Location = new Point(171, 235);
            ckbSize.Name = "ckbSize";
            ckbSize.Size = new Size(15, 14);
            ckbSize.TabIndex = 167;
            ckbSize.UseVisualStyleBackColor = true;
            ckbSize.CheckedChanged += ckbSize_CheckedChanged;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(405, 282);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(107, 27);
            btnCancel.TabIndex = 169;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(290, 282);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(107, 27);
            btnSave.TabIndex = 168;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // lblFtpVerify
            // 
            lblFtpVerify.AutoSize = true;
            lblFtpVerify.Location = new Point(13, 176);
            lblFtpVerify.Name = "lblFtpVerify";
            lblFtpVerify.Size = new Size(56, 15);
            lblFtpVerify.TabIndex = 173;
            lblFtpVerify.Text = "Ftp Verify";
            // 
            // cmbFtpVerify
            // 
            cmbFtpVerify.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFtpVerify.FormattingEnabled = true;
            cmbFtpVerify.Location = new Point(171, 173);
            cmbFtpVerify.Name = "cmbFtpVerify";
            cmbFtpVerify.Size = new Size(315, 23);
            cmbFtpVerify.TabIndex = 172;
            // 
            // FrmActionLocalUploadDirectory
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 317);
            Controls.Add(lblFtpVerify);
            Controls.Add(cmbFtpVerify);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(ckbSize);
            Controls.Add(lblSize);
            Controls.Add(cmbTypeSize);
            Controls.Add(nudSize);
            Controls.Add(lblFormats);
            Controls.Add(txtFormats);
            Controls.Add(lblRemoteExists);
            Controls.Add(cmbRemoteExists);
            Controls.Add(lblMode);
            Controls.Add(cmbMode);
            Controls.Add(lblRemotePath);
            Controls.Add(txtRemotePath);
            Controls.Add(txtLocalPath);
            Controls.Add(lblLocalPath);
            Controls.Add(cmbAction);
            Controls.Add(lblAction);
            Controls.Add(ckbEnabled);
            Controls.Add(lblEnabled);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmActionLocalUploadDirectory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Action";
            Load += FrmAction_Load;
            ((System.ComponentModel.ISupportInitialize)nudSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CheckBox ckbEnabled;
        private Label lblEnabled;
        private Label lblAction;
        private ComboBox cmbAction;
        private Label lblLocalPath;
        private TextBox txtLocalPath;
        private TextBox txtRemotePath;
        private Label lblRemotePath;
        private ComboBox cmbMode;
        private Label lblMode;
        private ComboBox cmbRemoteExists;
        private Label lblRemoteExists;
        private TextBox txtFormats;
        private Label lblFormats;
        private NumericUpDown nudSize;
        private ComboBox cmbTypeSize;
        private Label lblSize;
        private CheckBox ckbSize;
        private Button btnCancel;
        private Button btnSave;
        private Label lblFtpVerify;
        private ComboBox cmbFtpVerify;
    }
}