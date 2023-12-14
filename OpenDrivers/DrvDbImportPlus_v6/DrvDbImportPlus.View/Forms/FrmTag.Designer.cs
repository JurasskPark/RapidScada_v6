namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
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
            lblFormat = new Label();
            cbTagFormat = new ComboBox();
            nudNumberOfDecimalPlaces = new NumericUpDown();
            lblNumberOfDecimalPlaces = new Label();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfDecimalPlaces).BeginInit();
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
            txtTagname.Location = new Point(192, 12);
            txtTagname.Name = "txtTagname";
            txtTagname.Size = new Size(225, 23);
            txtTagname.TabIndex = 1;
            // 
            // ckbTagEnabled
            // 
            ckbTagEnabled.AutoSize = true;
            ckbTagEnabled.Location = new Point(192, 127);
            ckbTagEnabled.Name = "ckbTagEnabled";
            ckbTagEnabled.Size = new Size(15, 14);
            ckbTagEnabled.TabIndex = 5;
            ckbTagEnabled.UseVisualStyleBackColor = true;
            // 
            // lblEnabled
            // 
            lblEnabled.AutoSize = true;
            lblEnabled.Location = new Point(12, 126);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(49, 15);
            lblEnabled.TabIndex = 7;
            lblEnabled.Text = "Enabled";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(329, 163);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 27);
            btnClose.TabIndex = 8;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(233, 163);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(233, 163);
            btnAdd.Margin = new Padding(4, 3, 4, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(88, 27);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtTagCode
            // 
            txtTagCode.Location = new Point(192, 41);
            txtTagCode.Name = "txtTagCode";
            txtTagCode.Size = new Size(225, 23);
            txtTagCode.TabIndex = 2;
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
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Location = new Point(12, 72);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(45, 15);
            lblFormat.TabIndex = 15;
            lblFormat.Text = "Format";
            // 
            // cbTagFormat
            // 
            cbTagFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTagFormat.FormattingEnabled = true;
            cbTagFormat.ItemHeight = 15;
            cbTagFormat.Location = new Point(192, 69);
            cbTagFormat.Name = "cbTagFormat";
            cbTagFormat.Size = new Size(225, 23);
            cbTagFormat.TabIndex = 3;
            cbTagFormat.SelectedIndexChanged += cbTagFormat_SelectedIndexChanged;
            // 
            // nudNumberOfDecimalPlaces
            // 
            nudNumberOfDecimalPlaces.Location = new Point(192, 98);
            nudNumberOfDecimalPlaces.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            nudNumberOfDecimalPlaces.Name = "nudNumberOfDecimalPlaces";
            nudNumberOfDecimalPlaces.Size = new Size(223, 23);
            nudNumberOfDecimalPlaces.TabIndex = 4;
            // 
            // lblNumberOfDecimalPlaces
            // 
            lblNumberOfDecimalPlaces.AutoSize = true;
            lblNumberOfDecimalPlaces.Location = new Point(12, 100);
            lblNumberOfDecimalPlaces.Name = "lblNumberOfDecimalPlaces";
            lblNumberOfDecimalPlaces.Size = new Size(146, 15);
            lblNumberOfDecimalPlaces.TabIndex = 17;
            lblNumberOfDecimalPlaces.Text = "Number of decimal places";
            // 
            // FrmTag
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(427, 202);
            Controls.Add(lblNumberOfDecimalPlaces);
            Controls.Add(nudNumberOfDecimalPlaces);
            Controls.Add(lblFormat);
            Controls.Add(cbTagFormat);
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
            ((System.ComponentModel.ISupportInitialize)nudNumberOfDecimalPlaces).EndInit();
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
        private Label lblFormat;
        private ComboBox cbTagFormat;
        private NumericUpDown nudNumberOfDecimalPlaces;
        private Label lblNumberOfDecimalPlaces;
    }
}