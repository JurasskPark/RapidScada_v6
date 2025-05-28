namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    partial class FrmTask
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
            txtDescription = new TextBox();
            lblDescription = new Label();
            ckbEnabled = new CheckBox();
            lblEnabled = new Label();
            txtPath = new TextBox();
            lblPath = new Label();
            btnCancel = new Button();
            btnSave = new Button();
            txtName = new TextBox();
            lblName = new Label();
            gpbAction = new GroupBox();
            txtCompressMove = new TextBox();
            rdbActionCompressMove = new RadioButton();
            rdbActionDelete = new RadioButton();
            rdbActionNone = new RadioButton();
            nudPercentageOfFreeSpace = new NumericUpDown();
            txtDiskName = new TextBox();
            lblDiskName = new Label();
            lblPercentageOfFreeSpace = new Label();
            cmbDiskName = new ComboBox();
            txtValidate = new TextBox();
            lblValiadate = new Label();
            btnValidate = new Button();
            gpbAction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudPercentageOfFreeSpace).BeginInit();
            SuspendLayout();
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(177, 58);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(315, 23);
            txtDescription.TabIndex = 172;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(12, 61);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 171;
            lblDescription.Text = "Description";
            // 
            // ckbEnabled
            // 
            ckbEnabled.AutoSize = true;
            ckbEnabled.Location = new Point(177, 10);
            ckbEnabled.Name = "ckbEnabled";
            ckbEnabled.Size = new Size(15, 14);
            ckbEnabled.TabIndex = 168;
            ckbEnabled.UseVisualStyleBackColor = true;
            ckbEnabled.Click += control_Changed;
            // 
            // lblEnabled
            // 
            lblEnabled.AutoSize = true;
            lblEnabled.Location = new Point(12, 9);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(49, 15);
            lblEnabled.TabIndex = 167;
            lblEnabled.Text = "Enabled";
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPath.Location = new Point(177, 145);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(664, 23);
            txtPath.TabIndex = 165;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(12, 148);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(31, 15);
            lblPath.TabIndex = 164;
            lblPath.Text = "Path";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Location = new Point(740, 45);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(107, 27);
            btnCancel.TabIndex = 155;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(740, 12);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(107, 27);
            btnSave.TabIndex = 154;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(177, 29);
            txtName.Name = "txtName";
            txtName.Size = new Size(315, 23);
            txtName.TabIndex = 157;
            txtName.Click += control_Changed;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(12, 32);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 156;
            lblName.Text = "Name";
            // 
            // gpbAction
            // 
            gpbAction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gpbAction.Controls.Add(txtCompressMove);
            gpbAction.Controls.Add(rdbActionCompressMove);
            gpbAction.Controls.Add(rdbActionDelete);
            gpbAction.Controls.Add(rdbActionNone);
            gpbAction.Location = new Point(10, 173);
            gpbAction.Name = "gpbAction";
            gpbAction.Size = new Size(837, 101);
            gpbAction.TabIndex = 175;
            gpbAction.TabStop = false;
            gpbAction.Text = "Action";
            // 
            // txtCompressMove
            // 
            txtCompressMove.Location = new Point(167, 68);
            txtCompressMove.Name = "txtCompressMove";
            txtCompressMove.Size = new Size(664, 23);
            txtCompressMove.TabIndex = 178;
            // 
            // rdbActionCompressMove
            // 
            rdbActionCompressMove.AutoSize = true;
            rdbActionCompressMove.Location = new Point(6, 72);
            rdbActionCompressMove.Name = "rdbActionCompressMove";
            rdbActionCompressMove.Size = new Size(134, 19);
            rdbActionCompressMove.TabIndex = 177;
            rdbActionCompressMove.Text = "Compress and move";
            rdbActionCompressMove.UseVisualStyleBackColor = true;
            // 
            // rdbActionDelete
            // 
            rdbActionDelete.AutoSize = true;
            rdbActionDelete.Location = new Point(6, 47);
            rdbActionDelete.Name = "rdbActionDelete";
            rdbActionDelete.Size = new Size(58, 19);
            rdbActionDelete.TabIndex = 176;
            rdbActionDelete.Text = "Delete";
            rdbActionDelete.UseVisualStyleBackColor = true;
            // 
            // rdbActionNone
            // 
            rdbActionNone.AutoSize = true;
            rdbActionNone.Checked = true;
            rdbActionNone.Location = new Point(6, 22);
            rdbActionNone.Name = "rdbActionNone";
            rdbActionNone.Size = new Size(54, 19);
            rdbActionNone.TabIndex = 0;
            rdbActionNone.TabStop = true;
            rdbActionNone.Text = "None";
            rdbActionNone.UseVisualStyleBackColor = true;
            // 
            // nudPercentageOfFreeSpace
            // 
            nudPercentageOfFreeSpace.DecimalPlaces = 5;
            nudPercentageOfFreeSpace.Location = new Point(177, 116);
            nudPercentageOfFreeSpace.Name = "nudPercentageOfFreeSpace";
            nudPercentageOfFreeSpace.Size = new Size(120, 23);
            nudPercentageOfFreeSpace.TabIndex = 176;
            // 
            // txtDiskName
            // 
            txtDiskName.Location = new Point(177, 87);
            txtDiskName.Name = "txtDiskName";
            txtDiskName.Size = new Size(315, 23);
            txtDiskName.TabIndex = 177;
            // 
            // lblDiskName
            // 
            lblDiskName.AutoSize = true;
            lblDiskName.Location = new Point(12, 90);
            lblDiskName.Name = "lblDiskName";
            lblDiskName.Size = new Size(62, 15);
            lblDiskName.TabIndex = 178;
            lblDiskName.Text = "Disk name";
            // 
            // lblPercentageOfFreeSpace
            // 
            lblPercentageOfFreeSpace.AutoSize = true;
            lblPercentageOfFreeSpace.Location = new Point(12, 120);
            lblPercentageOfFreeSpace.Name = "lblPercentageOfFreeSpace";
            lblPercentageOfFreeSpace.Size = new Size(136, 15);
            lblPercentageOfFreeSpace.TabIndex = 179;
            lblPercentageOfFreeSpace.Text = "Percentage of free space";
            // 
            // cmbDiskName
            // 
            cmbDiskName.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDiskName.FormattingEnabled = true;
            cmbDiskName.Location = new Point(498, 87);
            cmbDiskName.Name = "cmbDiskName";
            cmbDiskName.Size = new Size(121, 23);
            cmbDiskName.TabIndex = 180;
            cmbDiskName.SelectedIndexChanged += cmbDiskName_SelectedIndexChanged;
            // 
            // txtValidate
            // 
            txtValidate.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtValidate.Location = new Point(12, 313);
            txtValidate.Multiline = true;
            txtValidate.Name = "txtValidate";
            txtValidate.Size = new Size(835, 116);
            txtValidate.TabIndex = 181;
            // 
            // lblValiadate
            // 
            lblValiadate.AutoSize = true;
            lblValiadate.Location = new Point(12, 286);
            lblValiadate.Name = "lblValiadate";
            lblValiadate.Size = new Size(48, 15);
            lblValiadate.TabIndex = 182;
            lblValiadate.Text = "Validate";
            // 
            // btnValidate
            // 
            btnValidate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnValidate.Location = new Point(740, 280);
            btnValidate.Margin = new Padding(4, 3, 4, 3);
            btnValidate.Name = "btnValidate";
            btnValidate.Size = new Size(107, 27);
            btnValidate.TabIndex = 183;
            btnValidate.Text = "Validate";
            btnValidate.UseVisualStyleBackColor = true;
            btnValidate.Click += btnValidate_Click;
            // 
            // FrmTask
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(860, 441);
            Controls.Add(btnValidate);
            Controls.Add(lblValiadate);
            Controls.Add(txtValidate);
            Controls.Add(cmbDiskName);
            Controls.Add(lblPercentageOfFreeSpace);
            Controls.Add(lblDiskName);
            Controls.Add(txtDiskName);
            Controls.Add(nudPercentageOfFreeSpace);
            Controls.Add(gpbAction);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(ckbEnabled);
            Controls.Add(lblEnabled);
            Controls.Add(txtPath);
            Controls.Add(lblPath);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Name = "FrmTask";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Task";
            Load += FrmTask_Load;
            gpbAction.ResumeLayout(false);
            gpbAction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudPercentageOfFreeSpace).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtDescription;
        private Label lblDescription;
        private CheckBox ckbEnabled;
        private Label lblEnabled;
        private TextBox txtPath;
        private Label lblPath;
        private Button btnCancel;
        private Button btnSave;
        private TextBox txtName;
        private Label lblName;
        private GroupBox gpbAction;
        private RadioButton rdbActionNone;
        private RadioButton rdbActionCompressMove;
        private RadioButton rdbActionDelete;
        private NumericUpDown nudPercentageOfFreeSpace;
        private TextBox txtDiskName;
        private Label lblDiskName;
        private Label lblPercentageOfFreeSpace;
        private ComboBox cmbDiskName;
        private TextBox txtValidate;
        private Label lblValiadate;
        private Button btnValidate;
        private TextBox txtCompressMove;
    }
}