namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    partial class FrmActionLocalUploadFile
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
            cmbRemoteExists = new ComboBox();
            lblRemoteExists = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            cmbFtpVerify = new ComboBox();
            lblFtpVerify = new Label();
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
            lblEnabled.Location = new Point(13, 9);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(49, 15);
            lblEnabled.TabIndex = 146;
            lblEnabled.Text = "Enabled";
            // 
            // lblAction
            // 
            lblAction.AutoSize = true;
            lblAction.Location = new Point(13, 32);
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
            lblLocalPath.Location = new Point(13, 60);
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
            lblRemotePath.Location = new Point(13, 89);
            lblRemotePath.Name = "lblRemotePath";
            lblRemotePath.Size = new Size(75, 15);
            lblRemotePath.TabIndex = 155;
            lblRemotePath.Text = "Remote Path";
            // 
            // cmbRemoteExists
            // 
            cmbRemoteExists.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRemoteExists.FormattingEnabled = true;
            cmbRemoteExists.Location = new Point(171, 115);
            cmbRemoteExists.Name = "cmbRemoteExists";
            cmbRemoteExists.Size = new Size(315, 23);
            cmbRemoteExists.TabIndex = 158;
            // 
            // lblRemoteExists
            // 
            lblRemoteExists.AutoSize = true;
            lblRemoteExists.Location = new Point(13, 118);
            lblRemoteExists.Name = "lblRemoteExists";
            lblRemoteExists.Size = new Size(80, 15);
            lblRemoteExists.TabIndex = 160;
            lblRemoteExists.Text = "Remote Exists";
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
            // cmbFtpVerify
            // 
            cmbFtpVerify.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFtpVerify.FormattingEnabled = true;
            cmbFtpVerify.Location = new Point(171, 144);
            cmbFtpVerify.Name = "cmbFtpVerify";
            cmbFtpVerify.Size = new Size(315, 23);
            cmbFtpVerify.TabIndex = 170;
            // 
            // lblFtpVerify
            // 
            lblFtpVerify.AutoSize = true;
            lblFtpVerify.Location = new Point(13, 147);
            lblFtpVerify.Name = "lblFtpVerify";
            lblFtpVerify.Size = new Size(56, 15);
            lblFtpVerify.TabIndex = 171;
            lblFtpVerify.Text = "Ftp Verify";
            // 
            // FrmActionLocalUploadFile
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 317);
            Controls.Add(lblFtpVerify);
            Controls.Add(cmbFtpVerify);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblRemoteExists);
            Controls.Add(cmbRemoteExists);
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
            Name = "FrmActionLocalUploadFile";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Action";
            Load += FrmAction_Load;
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
        private ComboBox cmbRemoteExists;
        private Label lblRemoteExists;
        private Button btnCancel;
        private Button btnSave;
        private ComboBox cmbFtpVerify;
        private Label lblFtpVerify;
    }
}