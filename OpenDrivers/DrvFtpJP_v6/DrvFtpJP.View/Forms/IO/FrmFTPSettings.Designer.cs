namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    partial class FrmFTPSettings
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
            btnCancel = new Button();
            btnSave = new Button();
            btnConnectionTest = new Button();
            lblPassword = new Label();
            lblUsername = new Label();
            lblHost = new Label();
            txtPassword = new TextBox();
            txtUsername = new TextBox();
            txtHost = new TextBox();
            txtName = new TextBox();
            lblName = new Label();
            lblPort = new Label();
            ckbDefaultPort = new CheckBox();
            btnOpenSshKey = new Button();
            lblSshKey = new Label();
            txtSshKey = new TextBox();
            btnShowPassword = new Button();
            cmbFtpDataType = new ComboBox();
            lblFtpDataType = new Label();
            ckbUseTLS = new CheckBox();
            lblTLS = new Label();
            nudPort = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)nudPort).BeginInit();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(393, 377);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(295, 377);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnConnectionTest
            // 
            btnConnectionTest.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnConnectionTest.Location = new Point(10, 344);
            btnConnectionTest.Margin = new Padding(4, 3, 4, 3);
            btnConnectionTest.Name = "btnConnectionTest";
            btnConnectionTest.Size = new Size(470, 27);
            btnConnectionTest.TabIndex = 3;
            btnConnectionTest.Text = "Connection Test";
            btnConnectionTest.UseVisualStyleBackColor = true;
            btnConnectionTest.Click += btnConnectionTest_Click;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(8, 106);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 11;
            lblPassword.Text = "Password";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(8, 76);
            lblUsername.Margin = new Padding(4, 0, 4, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(60, 15);
            lblUsername.TabIndex = 10;
            lblUsername.Text = "Username";
            // 
            // lblHost
            // 
            lblHost.AutoSize = true;
            lblHost.Location = new Point(10, 46);
            lblHost.Margin = new Padding(4, 0, 4, 0);
            lblHost.Name = "lblHost";
            lblHost.Size = new Size(32, 15);
            lblHost.TabIndex = 9;
            lblHost.Text = "Host";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(116, 103);
            txtPassword.Margin = new Padding(4, 3, 4, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(337, 23);
            txtPassword.TabIndex = 3;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.KeyDown += txtPassword_KeyDown;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(116, 73);
            txtUsername.Margin = new Padding(4, 3, 4, 3);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(364, 23);
            txtUsername.TabIndex = 2;
            // 
            // txtHost
            // 
            txtHost.Location = new Point(116, 43);
            txtHost.Margin = new Padding(4, 3, 4, 3);
            txtHost.Name = "txtHost";
            txtHost.Size = new Size(364, 23);
            txtHost.TabIndex = 1;
            // 
            // txtName
            // 
            txtName.Location = new Point(116, 12);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(364, 23);
            txtName.TabIndex = 0;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 15);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 8;
            lblName.Text = "Name";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(9, 137);
            lblPort.Margin = new Padding(4, 0, 4, 0);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 12;
            lblPort.Text = "Port";
            // 
            // ckbDefaultPort
            // 
            ckbDefaultPort.AutoSize = true;
            ckbDefaultPort.Checked = true;
            ckbDefaultPort.CheckState = CheckState.Checked;
            ckbDefaultPort.Location = new Point(116, 136);
            ckbDefaultPort.Margin = new Padding(4, 3, 4, 3);
            ckbDefaultPort.Name = "ckbDefaultPort";
            ckbDefaultPort.Size = new Size(64, 19);
            ckbDefaultPort.TabIndex = 4;
            ckbDefaultPort.Text = "Default";
            ckbDefaultPort.UseVisualStyleBackColor = true;
            ckbDefaultPort.CheckedChanged += ckbDefaultPort_CheckedChanged;
            // 
            // btnOpenSshKey
            // 
            btnOpenSshKey.Location = new Point(20, 259);
            btnOpenSshKey.Margin = new Padding(4, 3, 4, 3);
            btnOpenSshKey.Name = "btnOpenSshKey";
            btnOpenSshKey.Size = new Size(30, 27);
            btnOpenSshKey.TabIndex = 37;
            btnOpenSshKey.Text = "...";
            btnOpenSshKey.UseVisualStyleBackColor = true;
            btnOpenSshKey.Visible = false;
            btnOpenSshKey.Click += btnOpenSshKey_Click;
            // 
            // lblSshKey
            // 
            lblSshKey.AutoSize = true;
            lblSshKey.Location = new Point(9, 227);
            lblSshKey.Margin = new Padding(4, 0, 4, 0);
            lblSshKey.Name = "lblSshKey";
            lblSshKey.Size = new Size(44, 15);
            lblSshKey.TabIndex = 36;
            lblSshKey.Text = "SshKey";
            lblSshKey.Visible = false;
            // 
            // txtSshKey
            // 
            txtSshKey.AcceptsReturn = true;
            txtSshKey.Font = new Font("Consolas", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSshKey.Location = new Point(116, 226);
            txtSshKey.Margin = new Padding(4, 3, 4, 3);
            txtSshKey.Multiline = true;
            txtSshKey.Name = "txtSshKey";
            txtSshKey.ScrollBars = ScrollBars.Both;
            txtSshKey.Size = new Size(363, 103);
            txtSshKey.TabIndex = 8;
            txtSshKey.Visible = false;
            txtSshKey.WordWrap = false;
            // 
            // btnShowPassword
            // 
            btnShowPassword.Image = Properties.Resources.eye;
            btnShowPassword.Location = new Point(456, 102);
            btnShowPassword.Margin = new Padding(4, 3, 4, 3);
            btnShowPassword.Name = "btnShowPassword";
            btnShowPassword.Size = new Size(25, 25);
            btnShowPassword.TabIndex = 38;
            btnShowPassword.UseVisualStyleBackColor = true;
            btnShowPassword.Click += btnShowPassword_Click;
            // 
            // cmbFtpDataType
            // 
            cmbFtpDataType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFtpDataType.FormattingEnabled = true;
            cmbFtpDataType.Location = new Point(116, 164);
            cmbFtpDataType.Margin = new Padding(4, 3, 4, 3);
            cmbFtpDataType.Name = "cmbFtpDataType";
            cmbFtpDataType.Size = new Size(363, 23);
            cmbFtpDataType.TabIndex = 6;
            // 
            // lblFtpDataType
            // 
            lblFtpDataType.AutoSize = true;
            lblFtpDataType.Location = new Point(9, 167);
            lblFtpDataType.Margin = new Padding(4, 0, 4, 0);
            lblFtpDataType.Name = "lblFtpDataType";
            lblFtpDataType.Size = new Size(96, 15);
            lblFtpDataType.TabIndex = 40;
            lblFtpDataType.Text = "Connection Type";
            // 
            // ckbUseTLS
            // 
            ckbUseTLS.AutoSize = true;
            ckbUseTLS.Checked = true;
            ckbUseTLS.CheckState = CheckState.Checked;
            ckbUseTLS.Location = new Point(116, 200);
            ckbUseTLS.Margin = new Padding(4, 3, 4, 3);
            ckbUseTLS.Name = "ckbUseTLS";
            ckbUseTLS.Size = new Size(168, 19);
            ckbUseTLS.TabIndex = 7;
            ckbUseTLS.Text = "Use TLS to connect to FTPS";
            ckbUseTLS.UseVisualStyleBackColor = true;
            ckbUseTLS.Visible = false;
            // 
            // lblTLS
            // 
            lblTLS.AutoSize = true;
            lblTLS.Location = new Point(7, 201);
            lblTLS.Margin = new Padding(4, 0, 4, 0);
            lblTLS.Name = "lblTLS";
            lblTLS.Size = new Size(25, 15);
            lblTLS.TabIndex = 42;
            lblTLS.Text = "TLS";
            lblTLS.Visible = false;
            // 
            // nudPort
            // 
            nudPort.Location = new Point(228, 133);
            nudPort.Margin = new Padding(4, 3, 4, 3);
            nudPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            nudPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudPort.Name = "nudPort";
            nudPort.Size = new Size(253, 23);
            nudPort.TabIndex = 5;
            nudPort.Value = new decimal(new int[] { 21, 0, 0, 0 });
            // 
            // FrmFTPSettings
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(493, 415);
            Controls.Add(btnConnectionTest);
            Controls.Add(nudPort);
            Controls.Add(ckbUseTLS);
            Controls.Add(lblTLS);
            Controls.Add(lblFtpDataType);
            Controls.Add(cmbFtpDataType);
            Controls.Add(btnShowPassword);
            Controls.Add(btnOpenSshKey);
            Controls.Add(lblSshKey);
            Controls.Add(txtSshKey);
            Controls.Add(ckbDefaultPort);
            Controls.Add(lblPort);
            Controls.Add(lblPassword);
            Controls.Add(lblUsername);
            Controls.Add(lblHost);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(txtHost);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmFTPSettings";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "FTP Settings";
            Load += FrmFTPSettings_Load;
            ((System.ComponentModel.ISupportInitialize)nudPort).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion


        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.CheckBox ckbDefaultPort;
        private System.Windows.Forms.Button btnOpenSshKey;
        private System.Windows.Forms.Label lblSshKey;
        private System.Windows.Forms.TextBox txtSshKey;
        private System.Windows.Forms.Button btnShowPassword;
        private System.Windows.Forms.ComboBox cmbFtpDataType;
        private System.Windows.Forms.Label lblFtpDataType;
        private System.Windows.Forms.CheckBox ckbUseTLS;
        private System.Windows.Forms.Label lblTLS;
        private System.Windows.Forms.NumericUpDown nudPort;
        private System.Windows.Forms.Button btnConnectionTest;
    }
}