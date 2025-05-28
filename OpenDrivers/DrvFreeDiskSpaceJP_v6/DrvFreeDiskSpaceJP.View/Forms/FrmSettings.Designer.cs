namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    partial class FrmSettings
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
            lblLogDays = new Label();
            gpbLog = new GroupBox();
            nudLogDays = new NumericUpDown();
            ckbWriteDriverLog = new CheckBox();
            gpbLanguage = new GroupBox();
            cmbLanguage = new ComboBox();
            lblLanguage = new Label();
            btnSave = new Button();
            btnCancel = new Button();
            gpbLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudLogDays).BeginInit();
            gpbLanguage.SuspendLayout();
            SuspendLayout();
            // 
            // lblLogDays
            // 
            lblLogDays.AutoSize = true;
            lblLogDays.Location = new Point(361, 22);
            lblLogDays.Name = "lblLogDays";
            lblLogDays.Size = new Size(55, 15);
            lblLogDays.TabIndex = 0;
            lblLogDays.Text = "Log Days";
            // 
            // gpbLog
            // 
            gpbLog.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gpbLog.Controls.Add(nudLogDays);
            gpbLog.Controls.Add(ckbWriteDriverLog);
            gpbLog.Controls.Add(lblLogDays);
            gpbLog.Location = new Point(15, 12);
            gpbLog.Name = "gpbLog";
            gpbLog.Size = new Size(776, 47);
            gpbLog.TabIndex = 2;
            gpbLog.TabStop = false;
            gpbLog.Text = "Log";
            // 
            // nudLogDays
            // 
            nudLogDays.Location = new Point(480, 20);
            nudLogDays.Name = "nudLogDays";
            nudLogDays.Size = new Size(69, 23);
            nudLogDays.TabIndex = 3;
            nudLogDays.ValueChanged += control_Changed;
            // 
            // ckbWriteDriverLog
            // 
            ckbWriteDriverLog.AutoSize = true;
            ckbWriteDriverLog.Location = new Point(6, 20);
            ckbWriteDriverLog.Name = "ckbWriteDriverLog";
            ckbWriteDriverLog.Size = new Size(256, 19);
            ckbWriteDriverLog.TabIndex = 0;
            ckbWriteDriverLog.Text = "Recording the execution result (debugging)";
            ckbWriteDriverLog.UseVisualStyleBackColor = true;
            ckbWriteDriverLog.CheckedChanged += control_Changed;
            // 
            // gpbLanguage
            // 
            gpbLanguage.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gpbLanguage.Controls.Add(cmbLanguage);
            gpbLanguage.Controls.Add(lblLanguage);
            gpbLanguage.Location = new Point(15, 65);
            gpbLanguage.Name = "gpbLanguage";
            gpbLanguage.Size = new Size(776, 50);
            gpbLanguage.TabIndex = 3;
            gpbLanguage.TabStop = false;
            gpbLanguage.Text = "Language";
            // 
            // cmbLanguage
            // 
            cmbLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLanguage.FormattingEnabled = true;
            cmbLanguage.Items.AddRange(new object[] { "English", "Russian" });
            cmbLanguage.Location = new Point(87, 17);
            cmbLanguage.Name = "cmbLanguage";
            cmbLanguage.Size = new Size(165, 23);
            cmbLanguage.TabIndex = 4;
            cmbLanguage.SelectedIndexChanged += control_Changed;
            // 
            // lblLanguage
            // 
            lblLanguage.AutoSize = true;
            lblLanguage.Location = new Point(6, 19);
            lblLanguage.Name = "lblLanguage";
            lblLanguage.Size = new Size(59, 15);
            lblLanguage.TabIndex = 5;
            lblLanguage.Text = "Language";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Enabled = false;
            btnSave.Location = new Point(290, 125);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(107, 27);
            btnSave.TabIndex = 48;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(403, 125);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(107, 27);
            btnCancel.TabIndex = 49;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmSettings
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 164);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(gpbLanguage);
            Controls.Add(gpbLog);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSettings";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Settings";
            Load += FrmSettings_Load;
            gpbLog.ResumeLayout(false);
            gpbLog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudLogDays).EndInit();
            gpbLanguage.ResumeLayout(false);
            gpbLanguage.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblLogDays;
        private GroupBox gpbLog;
        private CheckBox ckbWriteDriverLog;
        private NumericUpDown nudLogDays;
        private GroupBox gpbLanguage;
        private Label lblLanguage;
        private ComboBox cmbLanguage;
        private Button btnSave;
        private Button btnCancel;
    }
}