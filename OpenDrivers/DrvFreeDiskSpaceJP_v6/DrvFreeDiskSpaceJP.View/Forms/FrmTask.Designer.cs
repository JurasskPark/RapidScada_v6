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
            txtID = new TextBox();
            lblID = new Label();
            txtName = new TextBox();
            lblName = new Label();
            lblDiskName = new Label();
            txtDiskName = new TextBox();
            SuspendLayout();
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(297, 97);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(315, 23);
            txtDescription.TabIndex = 172;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(10, 101);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 171;
            lblDescription.Text = "Description";
            // 
            // ckbEnabled
            // 
            ckbEnabled.AutoSize = true;
            ckbEnabled.Location = new Point(297, 49);
            ckbEnabled.Name = "ckbEnabled";
            ckbEnabled.Size = new Size(15, 14);
            ckbEnabled.TabIndex = 168;
            ckbEnabled.UseVisualStyleBackColor = true;
            // 
            // lblEnabled
            // 
            lblEnabled.AutoSize = true;
            lblEnabled.Location = new Point(10, 49);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(49, 15);
            lblEnabled.TabIndex = 167;
            lblEnabled.Text = "Enabled";
            // 
            // txtPath
            // 
            txtPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtPath.Location = new Point(297, 190);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(698, 23);
            txtPath.TabIndex = 165;
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(10, 194);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(31, 15);
            lblPath.TabIndex = 164;
            lblPath.Text = "Path";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Location = new Point(888, 45);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(107, 27);
            btnCancel.TabIndex = 155;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(888, 12);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(107, 27);
            btnSave.TabIndex = 154;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            txtID.Location = new Point(297, 20);
            txtID.Name = "txtID";
            txtID.Size = new Size(315, 23);
            txtID.TabIndex = 159;
            // 
            // lblID
            // 
            lblID.AutoSize = true;
            lblID.Location = new Point(10, 24);
            lblID.Name = "lblID";
            lblID.Size = new Size(18, 15);
            lblID.TabIndex = 158;
            lblID.Text = "ID";
            // 
            // txtName
            // 
            txtName.Location = new Point(297, 68);
            txtName.Name = "txtName";
            txtName.Size = new Size(315, 23);
            txtName.TabIndex = 157;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 72);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 156;
            lblName.Text = "Name";
            // 
            // lblDiskName
            // 
            lblDiskName.AutoSize = true;
            lblDiskName.Location = new Point(12, 129);
            lblDiskName.Name = "lblDiskName";
            lblDiskName.Size = new Size(62, 15);
            lblDiskName.TabIndex = 173;
            lblDiskName.Text = "Disk name";
            // 
            // txtDiskName
            // 
            txtDiskName.Location = new Point(297, 126);
            txtDiskName.Name = "txtDiskName";
            txtDiskName.Size = new Size(315, 23);
            txtDiskName.TabIndex = 174;
            // 
            // FrmTask
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 600);
            Controls.Add(txtDiskName);
            Controls.Add(lblDiskName);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(ckbEnabled);
            Controls.Add(lblEnabled);
            Controls.Add(txtPath);
            Controls.Add(lblPath);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtID);
            Controls.Add(lblID);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Name = "FrmTask";
            Text = "Task";
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
        private TextBox txtID;
        private Label lblID;
        private TextBox txtName;
        private Label lblName;
        private Label lblDiskName;
        private TextBox txtDiskName;
    }
}