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
            lblMaxNumberCharactersInWord = new Label();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfDecimalPlaces).BeginInit();
            SuspendLayout();
            // 
            // lblTagname
            // 
            lblTagname.AutoSize = true;
            lblTagname.Location = new Point(17, 25);
            lblTagname.Margin = new Padding(4, 0, 4, 0);
            lblTagname.Name = "lblTagname";
            lblTagname.Size = new Size(83, 25);
            lblTagname.TabIndex = 0;
            lblTagname.Text = "Tagname";
            // 
            // txtTagname
            // 
            txtTagname.Location = new Point(375, 20);
            txtTagname.Margin = new Padding(4, 5, 4, 5);
            txtTagname.Name = "txtTagname";
            txtTagname.Size = new Size(320, 31);
            txtTagname.TabIndex = 1;
            // 
            // ckbTagEnabled
            // 
            ckbTagEnabled.AutoSize = true;
            ckbTagEnabled.Location = new Point(375, 212);
            ckbTagEnabled.Margin = new Padding(4, 5, 4, 5);
            ckbTagEnabled.Name = "ckbTagEnabled";
            ckbTagEnabled.Size = new Size(22, 21);
            ckbTagEnabled.TabIndex = 5;
            ckbTagEnabled.UseVisualStyleBackColor = true;
            // 
            // lblEnabled
            // 
            lblEnabled.AutoSize = true;
            lblEnabled.Location = new Point(17, 210);
            lblEnabled.Margin = new Padding(4, 0, 4, 0);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(75, 25);
            lblEnabled.TabIndex = 7;
            lblEnabled.Text = "Enabled";
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(571, 272);
            btnClose.Margin = new Padding(6, 5, 6, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(126, 45);
            btnClose.TabIndex = 8;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(434, 272);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(126, 45);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(434, 272);
            btnAdd.Margin = new Padding(6, 5, 6, 5);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(126, 45);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // txtTagCode
            // 
            txtTagCode.Location = new Point(375, 68);
            txtTagCode.Margin = new Padding(4, 5, 4, 5);
            txtTagCode.Name = "txtTagCode";
            txtTagCode.Size = new Size(320, 31);
            txtTagCode.TabIndex = 2;
            // 
            // lblTagCode
            // 
            lblTagCode.AutoSize = true;
            lblTagCode.Location = new Point(17, 73);
            lblTagCode.Margin = new Padding(4, 0, 4, 0);
            lblTagCode.Name = "lblTagCode";
            lblTagCode.Size = new Size(83, 25);
            lblTagCode.TabIndex = 12;
            lblTagCode.Text = "Tag code";
            // 
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Location = new Point(17, 120);
            lblFormat.Margin = new Padding(4, 0, 4, 0);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(69, 25);
            lblFormat.TabIndex = 15;
            lblFormat.Text = "Format";
            // 
            // cbTagFormat
            // 
            cbTagFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTagFormat.FormattingEnabled = true;
            cbTagFormat.ItemHeight = 25;
            cbTagFormat.Location = new Point(375, 115);
            cbTagFormat.Margin = new Padding(4, 5, 4, 5);
            cbTagFormat.Name = "cbTagFormat";
            cbTagFormat.Size = new Size(320, 33);
            cbTagFormat.TabIndex = 3;
            cbTagFormat.SelectedIndexChanged += cbTagFormat_SelectedIndexChanged;
            // 
            // nudNumberOfDecimalPlaces
            // 
            nudNumberOfDecimalPlaces.Location = new Point(375, 163);
            nudNumberOfDecimalPlaces.Margin = new Padding(4, 5, 4, 5);
            nudNumberOfDecimalPlaces.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            nudNumberOfDecimalPlaces.Name = "nudNumberOfDecimalPlaces";
            nudNumberOfDecimalPlaces.Size = new Size(319, 31);
            nudNumberOfDecimalPlaces.TabIndex = 4;
            // 
            // lblNumberOfDecimalPlaces
            // 
            lblNumberOfDecimalPlaces.AutoSize = true;
            lblNumberOfDecimalPlaces.Location = new Point(17, 166);
            lblNumberOfDecimalPlaces.Margin = new Padding(4, 0, 4, 0);
            lblNumberOfDecimalPlaces.Name = "lblNumberOfDecimalPlaces";
            lblNumberOfDecimalPlaces.Size = new Size(219, 25);
            lblNumberOfDecimalPlaces.TabIndex = 17;
            lblNumberOfDecimalPlaces.Text = "Number of decimal places";
            // 
            // lblMaxNumberCharactersInWord
            // 
            lblMaxNumberCharactersInWord.AutoSize = true;
            lblMaxNumberCharactersInWord.Location = new Point(17, 166);
            lblMaxNumberCharactersInWord.Margin = new Padding(4, 0, 4, 0);
            lblMaxNumberCharactersInWord.Name = "lblMaxNumberCharactersInWord";
            lblMaxNumberCharactersInWord.Size = new Size(329, 25);
            lblMaxNumberCharactersInWord.TabIndex = 18;
            lblMaxNumberCharactersInWord.Text = "Maximum number of characters in word";
            lblMaxNumberCharactersInWord.Visible = false;
            // 
            // FrmTag
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(711, 337);
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
            Controls.Add(lblMaxNumberCharactersInWord);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
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
        private Label lblMaxNumberCharactersInWord;
    }
}