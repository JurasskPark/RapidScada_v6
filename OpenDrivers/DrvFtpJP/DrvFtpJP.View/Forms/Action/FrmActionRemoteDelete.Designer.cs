namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    partial class FrmActionRemoteDelete
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
            txtRemotePath = new TextBox();
            lblRemotePath = new Label();
            btnCancel = new Button();
            btnSave = new Button();
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
            // txtRemotePath
            // 
            txtRemotePath.Location = new Point(171, 58);
            txtRemotePath.Name = "txtRemotePath";
            txtRemotePath.Size = new Size(617, 23);
            txtRemotePath.TabIndex = 154;
            // 
            // lblRemotePath
            // 
            lblRemotePath.AutoSize = true;
            lblRemotePath.Location = new Point(12, 61);
            lblRemotePath.Name = "lblRemotePath";
            lblRemotePath.Size = new Size(75, 15);
            lblRemotePath.TabIndex = 155;
            lblRemotePath.Text = "Remote Path";
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
            // FrmActionRemoteDelete
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 317);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(lblRemotePath);
            Controls.Add(txtRemotePath);
            Controls.Add(cmbAction);
            Controls.Add(lblAction);
            Controls.Add(ckbEnabled);
            Controls.Add(lblEnabled);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmActionRemoteDelete";
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
        private TextBox txtRemotePath;
        private Label lblRemotePath;
        private Button btnCancel;
        private Button btnSave;
    }
}