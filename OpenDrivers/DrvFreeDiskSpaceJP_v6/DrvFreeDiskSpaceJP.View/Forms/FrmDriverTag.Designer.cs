namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    partial class FrmDriverTag
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
            tabControl = new TabControl();
            tabGeneral = new TabPage();
            btnSave = new Button();
            gbTableProperty = new GroupBox();
            rdbTagsBasedRequestedTableRows = new RadioButton();
            rdbTagsBasedRequestedTableColumns = new RadioButton();
            nudValueNumberOfDecimalPlaces = new NumericUpDown();
            lblValueNumberOfDecimalPlaces = new Label();
            lblValueMaxNumberCharactersInWord = new Label();
            txtColumnNamesDateTime = new TextBox();
            lblColumnNamesDateTime = new Label();
            cmbValueFormat = new ComboBox();
            lblValueType = new Label();
            txtColumnNamestValue = new TextBox();
            lblColumnNamesValue = new Label();
            txtColumnNamesTag = new TextBox();
            lblColumnNamesTag = new Label();
            txtColumnNames = new TextBox();
            lblColumnNames = new Label();
            btnCancel = new Button();
            gbTagProperty = new GroupBox();
            lblNumberOfDecimalPlaces = new Label();
            nudNumberOfDecimalPlaces = new NumericUpDown();
            lblMaxNumberCharactersInWord = new Label();
            txtTagCode = new TextBox();
            lblTagCode = new Label();
            txtTagAddressParameter = new TextBox();
            lblAddressParameter = new Label();
            txtTagAddressLine = new TextBox();
            lblAddressLine = new Label();
            lblTagEnabled = new Label();
            ckbTagEnabled = new CheckBox();
            txtTagID = new TextBox();
            lblTagID = new Label();
            lblTagDataType = new Label();
            cmbTagDataType = new ComboBox();
            lblTagDescription = new Label();
            txtTagDescription = new TextBox();
            txtTagAddressBlock = new TextBox();
            lblTagAddress = new Label();
            txtTagName = new TextBox();
            lblTagName = new Label();
            tabControl.SuspendLayout();
            tabGeneral.SuspendLayout();
            gbTableProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudValueNumberOfDecimalPlaces).BeginInit();
            gbTagProperty.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfDecimalPlaces).BeginInit();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabGeneral);
            tabControl.Location = new Point(14, 14);
            tabControl.Margin = new Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(820, 431);
            tabControl.TabIndex = 0;
            // 
            // tabGeneral
            // 
            tabGeneral.Controls.Add(btnSave);
            tabGeneral.Controls.Add(gbTableProperty);
            tabGeneral.Controls.Add(btnCancel);
            tabGeneral.Controls.Add(gbTagProperty);
            tabGeneral.Location = new Point(4, 24);
            tabGeneral.Margin = new Padding(4, 3, 4, 3);
            tabGeneral.Name = "tabGeneral";
            tabGeneral.Padding = new Padding(4, 3, 4, 3);
            tabGeneral.Size = new Size(812, 403);
            tabGeneral.TabIndex = 0;
            tabGeneral.Text = "General";
            tabGeneral.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(328, 368);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnTagSave_Click;
            // 
            // gbTableProperty
            // 
            gbTableProperty.Controls.Add(rdbTagsBasedRequestedTableRows);
            gbTableProperty.Controls.Add(rdbTagsBasedRequestedTableColumns);
            gbTableProperty.Controls.Add(nudValueNumberOfDecimalPlaces);
            gbTableProperty.Controls.Add(lblValueNumberOfDecimalPlaces);
            gbTableProperty.Controls.Add(lblValueMaxNumberCharactersInWord);
            gbTableProperty.Controls.Add(txtColumnNamesDateTime);
            gbTableProperty.Controls.Add(lblColumnNamesDateTime);
            gbTableProperty.Controls.Add(cmbValueFormat);
            gbTableProperty.Controls.Add(lblValueType);
            gbTableProperty.Controls.Add(txtColumnNamestValue);
            gbTableProperty.Controls.Add(lblColumnNamesValue);
            gbTableProperty.Controls.Add(txtColumnNamesTag);
            gbTableProperty.Controls.Add(lblColumnNamesTag);
            gbTableProperty.Controls.Add(txtColumnNames);
            gbTableProperty.Controls.Add(lblColumnNames);
            gbTableProperty.Location = new Point(423, 7);
            gbTableProperty.Name = "gbTableProperty";
            gbTableProperty.Size = new Size(382, 355);
            gbTableProperty.TabIndex = 1;
            gbTableProperty.TabStop = false;
            gbTableProperty.Text = "Table Property";
            gbTableProperty.Visible = false;
            // 
            // rdbTagsBasedRequestedTableRows
            // 
            rdbTagsBasedRequestedTableRows.AutoSize = true;
            rdbTagsBasedRequestedTableRows.Location = new Point(6, 46);
            rdbTagsBasedRequestedTableRows.Name = "rdbTagsBasedRequestedTableRows";
            rdbTagsBasedRequestedTableRows.Size = new Size(263, 19);
            rdbTagsBasedRequestedTableRows.TabIndex = 100;
            rdbTagsBasedRequestedTableRows.Text = "Tags based on the list of requested table rows";
            rdbTagsBasedRequestedTableRows.UseVisualStyleBackColor = true;
            // 
            // rdbTagsBasedRequestedTableColumns
            // 
            rdbTagsBasedRequestedTableColumns.AutoSize = true;
            rdbTagsBasedRequestedTableColumns.Checked = true;
            rdbTagsBasedRequestedTableColumns.Location = new Point(6, 23);
            rdbTagsBasedRequestedTableColumns.Name = "rdbTagsBasedRequestedTableColumns";
            rdbTagsBasedRequestedTableColumns.Size = new Size(284, 19);
            rdbTagsBasedRequestedTableColumns.TabIndex = 99;
            rdbTagsBasedRequestedTableColumns.TabStop = true;
            rdbTagsBasedRequestedTableColumns.Text = "Tags based on the list of requested table columns";
            rdbTagsBasedRequestedTableColumns.UseVisualStyleBackColor = true;
            // 
            // nudValueNumberOfDecimalPlaces
            // 
            nudValueNumberOfDecimalPlaces.Location = new Point(263, 277);
            nudValueNumberOfDecimalPlaces.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            nudValueNumberOfDecimalPlaces.Name = "nudValueNumberOfDecimalPlaces";
            nudValueNumberOfDecimalPlaces.Size = new Size(112, 23);
            nudValueNumberOfDecimalPlaces.TabIndex = 95;
            // 
            // lblValueNumberOfDecimalPlaces
            // 
            lblValueNumberOfDecimalPlaces.AutoSize = true;
            lblValueNumberOfDecimalPlaces.Location = new Point(6, 279);
            lblValueNumberOfDecimalPlaces.Name = "lblValueNumberOfDecimalPlaces";
            lblValueNumberOfDecimalPlaces.Size = new Size(146, 15);
            lblValueNumberOfDecimalPlaces.TabIndex = 97;
            lblValueNumberOfDecimalPlaces.Text = "Number of decimal places";
            // 
            // lblValueMaxNumberCharactersInWord
            // 
            lblValueMaxNumberCharactersInWord.AutoSize = true;
            lblValueMaxNumberCharactersInWord.Location = new Point(6, 279);
            lblValueMaxNumberCharactersInWord.Name = "lblValueMaxNumberCharactersInWord";
            lblValueMaxNumberCharactersInWord.Size = new Size(221, 15);
            lblValueMaxNumberCharactersInWord.TabIndex = 98;
            lblValueMaxNumberCharactersInWord.Text = "Maximum number of characters in word";
            lblValueMaxNumberCharactersInWord.Visible = false;
            // 
            // txtColumnNamesDateTime
            // 
            txtColumnNamesDateTime.Location = new Point(6, 321);
            txtColumnNamesDateTime.Margin = new Padding(4, 3, 4, 3);
            txtColumnNamesDateTime.Name = "txtColumnNamesDateTime";
            txtColumnNamesDateTime.Size = new Size(369, 23);
            txtColumnNamesDateTime.TabIndex = 11;
            // 
            // lblColumnNamesDateTime
            // 
            lblColumnNamesDateTime.AutoSize = true;
            lblColumnNamesDateTime.Location = new Point(6, 303);
            lblColumnNamesDateTime.Name = "lblColumnNamesDateTime";
            lblColumnNamesDateTime.Size = new Size(164, 15);
            lblColumnNamesDateTime.TabIndex = 10;
            lblColumnNamesDateTime.Text = "Column names with datetime";
            // 
            // cmbValueFormat
            // 
            cmbValueFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbValueFormat.FormattingEnabled = true;
            cmbValueFormat.ItemHeight = 15;
            cmbValueFormat.Location = new Point(121, 248);
            cmbValueFormat.Margin = new Padding(4, 3, 4, 3);
            cmbValueFormat.Name = "cmbValueFormat";
            cmbValueFormat.Size = new Size(254, 23);
            cmbValueFormat.TabIndex = 9;
            cmbValueFormat.SelectedIndexChanged += cmbValueFormat_SelectedIndexChanged;
            // 
            // lblValueType
            // 
            lblValueType.AutoSize = true;
            lblValueType.Location = new Point(6, 252);
            lblValueType.Margin = new Padding(4, 0, 4, 0);
            lblValueType.Name = "lblValueType";
            lblValueType.Size = new Size(61, 15);
            lblValueType.TabIndex = 8;
            lblValueType.Text = "Value type";
            // 
            // txtColumnNamestValue
            // 
            txtColumnNamestValue.Location = new Point(6, 219);
            txtColumnNamestValue.Margin = new Padding(4, 3, 4, 3);
            txtColumnNamestValue.Name = "txtColumnNamestValue";
            txtColumnNamestValue.Size = new Size(369, 23);
            txtColumnNamestValue.TabIndex = 7;
            // 
            // lblColumnNamesValue
            // 
            lblColumnNamesValue.AutoSize = true;
            lblColumnNamesValue.Location = new Point(6, 193);
            lblColumnNamesValue.Name = "lblColumnNamesValue";
            lblColumnNamesValue.Size = new Size(145, 15);
            lblColumnNamesValue.TabIndex = 6;
            lblColumnNamesValue.Text = "Column names with value";
            // 
            // txtColumnNamesTag
            // 
            txtColumnNamesTag.Location = new Point(6, 161);
            txtColumnNamesTag.Margin = new Padding(4, 3, 4, 3);
            txtColumnNamesTag.Name = "txtColumnNamesTag";
            txtColumnNamesTag.Size = new Size(369, 23);
            txtColumnNamesTag.TabIndex = 5;
            // 
            // lblColumnNamesTag
            // 
            lblColumnNamesTag.AutoSize = true;
            lblColumnNamesTag.Location = new Point(6, 135);
            lblColumnNamesTag.Name = "lblColumnNamesTag";
            lblColumnNamesTag.Size = new Size(134, 15);
            lblColumnNamesTag.TabIndex = 4;
            lblColumnNamesTag.Text = "Column names with tag";
            // 
            // txtColumnNames
            // 
            txtColumnNames.Location = new Point(6, 100);
            txtColumnNames.Margin = new Padding(4, 3, 4, 3);
            txtColumnNames.Name = "txtColumnNames";
            txtColumnNames.Size = new Size(369, 23);
            txtColumnNames.TabIndex = 3;
            // 
            // lblColumnNames
            // 
            lblColumnNames.AutoSize = true;
            lblColumnNames.Location = new Point(6, 74);
            lblColumnNames.Name = "lblColumnNames";
            lblColumnNames.Size = new Size(88, 15);
            lblColumnNames.TabIndex = 0;
            lblColumnNames.Text = "Column names";
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(423, 368);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(88, 27);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btn_TagCancel_Click;
            // 
            // gbTagProperty
            // 
            gbTagProperty.Controls.Add(lblNumberOfDecimalPlaces);
            gbTagProperty.Controls.Add(nudNumberOfDecimalPlaces);
            gbTagProperty.Controls.Add(lblMaxNumberCharactersInWord);
            gbTagProperty.Controls.Add(txtTagCode);
            gbTagProperty.Controls.Add(lblTagCode);
            gbTagProperty.Controls.Add(txtTagAddressParameter);
            gbTagProperty.Controls.Add(lblAddressParameter);
            gbTagProperty.Controls.Add(txtTagAddressLine);
            gbTagProperty.Controls.Add(lblAddressLine);
            gbTagProperty.Controls.Add(lblTagEnabled);
            gbTagProperty.Controls.Add(ckbTagEnabled);
            gbTagProperty.Controls.Add(txtTagID);
            gbTagProperty.Controls.Add(lblTagID);
            gbTagProperty.Controls.Add(lblTagDataType);
            gbTagProperty.Controls.Add(cmbTagDataType);
            gbTagProperty.Controls.Add(lblTagDescription);
            gbTagProperty.Controls.Add(txtTagDescription);
            gbTagProperty.Controls.Add(txtTagAddressBlock);
            gbTagProperty.Controls.Add(lblTagAddress);
            gbTagProperty.Controls.Add(txtTagName);
            gbTagProperty.Controls.Add(lblTagName);
            gbTagProperty.Location = new Point(7, 7);
            gbTagProperty.Margin = new Padding(4, 3, 4, 3);
            gbTagProperty.Name = "gbTagProperty";
            gbTagProperty.Padding = new Padding(4, 3, 4, 3);
            gbTagProperty.Size = new Size(409, 355);
            gbTagProperty.TabIndex = 0;
            gbTagProperty.TabStop = false;
            gbTagProperty.Text = "Tag Property";
            // 
            // lblNumberOfDecimalPlaces
            // 
            lblNumberOfDecimalPlaces.AutoSize = true;
            lblNumberOfDecimalPlaces.Location = new Point(11, 279);
            lblNumberOfDecimalPlaces.Name = "lblNumberOfDecimalPlaces";
            lblNumberOfDecimalPlaces.Size = new Size(146, 15);
            lblNumberOfDecimalPlaces.TabIndex = 95;
            lblNumberOfDecimalPlaces.Text = "Number of decimal places";
            // 
            // nudNumberOfDecimalPlaces
            // 
            nudNumberOfDecimalPlaces.Location = new Point(285, 279);
            nudNumberOfDecimalPlaces.Maximum = new decimal(new int[] { 16, 0, 0, 0 });
            nudNumberOfDecimalPlaces.Name = "nudNumberOfDecimalPlaces";
            nudNumberOfDecimalPlaces.Size = new Size(112, 23);
            nudNumberOfDecimalPlaces.TabIndex = 94;
            // 
            // lblMaxNumberCharactersInWord
            // 
            lblMaxNumberCharactersInWord.AutoSize = true;
            lblMaxNumberCharactersInWord.Location = new Point(11, 279);
            lblMaxNumberCharactersInWord.Name = "lblMaxNumberCharactersInWord";
            lblMaxNumberCharactersInWord.Size = new Size(221, 15);
            lblMaxNumberCharactersInWord.TabIndex = 96;
            lblMaxNumberCharactersInWord.Text = "Maximum number of characters in word";
            lblMaxNumberCharactersInWord.Visible = false;
            // 
            // txtTagCode
            // 
            txtTagCode.Location = new Point(128, 100);
            txtTagCode.Name = "txtTagCode";
            txtTagCode.Size = new Size(269, 23);
            txtTagCode.TabIndex = 92;
            // 
            // lblTagCode
            // 
            lblTagCode.AutoSize = true;
            lblTagCode.Location = new Point(14, 103);
            lblTagCode.Name = "lblTagCode";
            lblTagCode.Size = new Size(54, 15);
            lblTagCode.TabIndex = 93;
            lblTagCode.Text = "Tag code";
            // 
            // txtTagAddressParameter
            // 
            txtTagAddressParameter.Location = new Point(128, 219);
            txtTagAddressParameter.Margin = new Padding(4, 3, 4, 3);
            txtTagAddressParameter.Name = "txtTagAddressParameter";
            txtTagAddressParameter.Size = new Size(269, 23);
            txtTagAddressParameter.TabIndex = 6;
            // 
            // lblAddressParameter
            // 
            lblAddressParameter.AutoSize = true;
            lblAddressParameter.Location = new Point(14, 222);
            lblAddressParameter.Margin = new Padding(4, 0, 4, 0);
            lblAddressParameter.Name = "lblAddressParameter";
            lblAddressParameter.Size = new Size(106, 15);
            lblAddressParameter.TabIndex = 91;
            lblAddressParameter.Text = "Address Parameter";
            // 
            // txtTagAddressLine
            // 
            txtTagAddressLine.Location = new Point(128, 190);
            txtTagAddressLine.Margin = new Padding(4, 3, 4, 3);
            txtTagAddressLine.Name = "txtTagAddressLine";
            txtTagAddressLine.Size = new Size(269, 23);
            txtTagAddressLine.TabIndex = 5;
            // 
            // lblAddressLine
            // 
            lblAddressLine.AutoSize = true;
            lblAddressLine.Location = new Point(14, 193);
            lblAddressLine.Margin = new Padding(4, 0, 4, 0);
            lblAddressLine.Name = "lblAddressLine";
            lblAddressLine.Size = new Size(74, 15);
            lblAddressLine.TabIndex = 89;
            lblAddressLine.Text = "Address Line";
            // 
            // lblTagEnabled
            // 
            lblTagEnabled.AutoSize = true;
            lblTagEnabled.Location = new Point(14, 51);
            lblTagEnabled.Margin = new Padding(4, 0, 4, 0);
            lblTagEnabled.Name = "lblTagEnabled";
            lblTagEnabled.Size = new Size(49, 15);
            lblTagEnabled.TabIndex = 88;
            lblTagEnabled.Text = "Enabled";
            // 
            // ckbTagEnabled
            // 
            ckbTagEnabled.AutoSize = true;
            ckbTagEnabled.Location = new Point(128, 51);
            ckbTagEnabled.Margin = new Padding(4, 3, 4, 3);
            ckbTagEnabled.Name = "ckbTagEnabled";
            ckbTagEnabled.Size = new Size(15, 14);
            ckbTagEnabled.TabIndex = 1;
            ckbTagEnabled.UseVisualStyleBackColor = true;
            // 
            // txtTagID
            // 
            txtTagID.Location = new Point(128, 22);
            txtTagID.Margin = new Padding(4, 3, 4, 3);
            txtTagID.Name = "txtTagID";
            txtTagID.ReadOnly = true;
            txtTagID.Size = new Size(269, 23);
            txtTagID.TabIndex = 0;
            // 
            // lblTagID
            // 
            lblTagID.AutoSize = true;
            lblTagID.Location = new Point(14, 25);
            lblTagID.Margin = new Padding(4, 0, 4, 0);
            lblTagID.Name = "lblTagID";
            lblTagID.Size = new Size(18, 15);
            lblTagID.TabIndex = 9;
            lblTagID.Text = "ID";
            // 
            // lblTagDataType
            // 
            lblTagDataType.AutoSize = true;
            lblTagDataType.Location = new Point(14, 251);
            lblTagDataType.Margin = new Padding(4, 0, 4, 0);
            lblTagDataType.Name = "lblTagDataType";
            lblTagDataType.Size = new Size(57, 15);
            lblTagDataType.TabIndex = 6;
            lblTagDataType.Text = "Data type";
            // 
            // cmbTagDataType
            // 
            cmbTagDataType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTagDataType.FormattingEnabled = true;
            cmbTagDataType.ItemHeight = 15;
            cmbTagDataType.Location = new Point(128, 248);
            cmbTagDataType.Margin = new Padding(4, 3, 4, 3);
            cmbTagDataType.Name = "cmbTagDataType";
            cmbTagDataType.Size = new Size(269, 23);
            cmbTagDataType.TabIndex = 7;
            cmbTagDataType.SelectedIndexChanged += cmbTagDataType_SelectedIndexChanged;
            // 
            // lblTagDescription
            // 
            lblTagDescription.AutoSize = true;
            lblTagDescription.Location = new Point(14, 135);
            lblTagDescription.Margin = new Padding(4, 0, 4, 0);
            lblTagDescription.Name = "lblTagDescription";
            lblTagDescription.Size = new Size(67, 15);
            lblTagDescription.TabIndex = 5;
            lblTagDescription.Text = "Description";
            // 
            // txtTagDescription
            // 
            txtTagDescription.Location = new Point(128, 132);
            txtTagDescription.Margin = new Padding(4, 3, 4, 3);
            txtTagDescription.Name = "txtTagDescription";
            txtTagDescription.Size = new Size(269, 23);
            txtTagDescription.TabIndex = 3;
            // 
            // txtTagAddressBlock
            // 
            txtTagAddressBlock.Location = new Point(128, 161);
            txtTagAddressBlock.Margin = new Padding(4, 3, 4, 3);
            txtTagAddressBlock.Name = "txtTagAddressBlock";
            txtTagAddressBlock.Size = new Size(269, 23);
            txtTagAddressBlock.TabIndex = 4;
            // 
            // lblTagAddress
            // 
            lblTagAddress.AutoSize = true;
            lblTagAddress.Location = new Point(14, 164);
            lblTagAddress.Margin = new Padding(4, 0, 4, 0);
            lblTagAddress.Name = "lblTagAddress";
            lblTagAddress.Size = new Size(81, 15);
            lblTagAddress.TabIndex = 2;
            lblTagAddress.Text = "Address Block";
            // 
            // txtTagName
            // 
            txtTagName.Location = new Point(128, 71);
            txtTagName.Margin = new Padding(4, 3, 4, 3);
            txtTagName.Name = "txtTagName";
            txtTagName.Size = new Size(269, 23);
            txtTagName.TabIndex = 2;
            // 
            // lblTagName
            // 
            lblTagName.AutoSize = true;
            lblTagName.Location = new Point(14, 74);
            lblTagName.Margin = new Padding(4, 0, 4, 0);
            lblTagName.Name = "lblTagName";
            lblTagName.Size = new Size(39, 15);
            lblTagName.TabIndex = 0;
            lblTagName.Text = "Name";
            // 
            // FrmParserTag
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(847, 457);
            Controls.Add(tabControl);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmParserTag";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tag";
            Load += FrmTag_Load;
            tabControl.ResumeLayout(false);
            tabGeneral.ResumeLayout(false);
            gbTableProperty.ResumeLayout(false);
            gbTableProperty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudValueNumberOfDecimalPlaces).EndInit();
            gbTagProperty.ResumeLayout(false);
            gbTagProperty.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfDecimalPlaces).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox gbTagProperty;
        private System.Windows.Forms.Label lblTagAddress;
        private System.Windows.Forms.TextBox txtTagName;
        private System.Windows.Forms.Label lblTagName;
        private System.Windows.Forms.TabPage tabScaling;
        private System.Windows.Forms.Label lblTagDataType;
        private System.Windows.Forms.ComboBox cmbTagDataType;
        private System.Windows.Forms.Label lblTagDescription;
        private System.Windows.Forms.TextBox txtTagDescription;
        private System.Windows.Forms.TextBox txtTagAddressBlock;
        private System.Windows.Forms.GroupBox gbScaled;
        private System.Windows.Forms.TextBox txtCoefficient;
        private System.Windows.Forms.Label lblCoefficient;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cmb_DeviceID;
        private System.Windows.Forms.Label lbl_DeviceID;
        private System.Windows.Forms.TextBox txtTagID;
        private System.Windows.Forms.Label lblTagID;
        private System.Windows.Forms.CheckBox ckbTagEnabled;
        private System.Windows.Forms.GroupBox gbLineScaled;
        private System.Windows.Forms.Label lblLineScaledResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblScaledLowValue_2;
        private System.Windows.Forms.Label lblPlus;
        private System.Windows.Forms.Label lblBracket_5;
        private System.Windows.Forms.Label lblBracket_4;
        private System.Windows.Forms.Label lblRowLowValue_2;
        private System.Windows.Forms.Label lblLineScaledMinus_3;
        private System.Windows.Forms.Label lblLineScaledValue;
        private System.Windows.Forms.Label lblBracket_3;
        private System.Windows.Forms.Label lblLineScalEdincrease;
        private System.Windows.Forms.Label lblBracket_1;
        private System.Windows.Forms.Label lblBracket_2;
        private System.Windows.Forms.Label lblLineScaledMinus_4;
        private System.Windows.Forms.Label lblLineScaledDivision_1;
        private System.Windows.Forms.Label lblLineScaledMinus_1;
        private System.Windows.Forms.Label lblRowLowValue;
        private System.Windows.Forms.Label lblRowHighValue;
        private System.Windows.Forms.Label lblScaledLowValue;
        private System.Windows.Forms.Label lblScaledHighValue;
        private System.Windows.Forms.Button btnCalcLineScaled;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.Label lblLineScaledVal;
        private System.Windows.Forms.Label lblLineScaledHigh;
        private System.Windows.Forms.TextBox txtLineScaledHigh;
        private System.Windows.Forms.TextBox txtLineScaledLow;
        private System.Windows.Forms.Label lblLineScaledLow;
        private System.Windows.Forms.Label lblLineScaledRowHigh;
        private System.Windows.Forms.TextBox txtLineScaledRowHigh;
        private System.Windows.Forms.TextBox txtLineScaledRowLow;
        private System.Windows.Forms.Label lblLineScaledRowLow;
        private System.Windows.Forms.Label lblScaled;
        private System.Windows.Forms.ComboBox cmbScaled;
        private Label lblTagEnabled;
        private TextBox txtTagAddressParameter;
        private Label lblAddressParameter;
        private TextBox txtTagAddressLine;
        private Label lblAddressLine;
        private GroupBox gbTableProperty;
        private Label lblColumnNames;
        private TextBox txtColumnNames;
        private Label lblColumnNamesValue;
        private TextBox txtColumnNamesTag;
        private Label lblColumnNamesTag;
        private Label lblColumnNamesDateTime;
        private ComboBox cmbValueFormat;
        private Label lblValueType;
        private TextBox txtColumnNamestValue;
        private TextBox txtColumnNamesDateTime;
        private TextBox txtTagCode;
        private Label lblTagCode;
        private Label lblNumberOfDecimalPlaces;
        private NumericUpDown nudNumberOfDecimalPlaces;
        private Label lblMaxNumberCharactersInWord;
        private NumericUpDown nudValueNumberOfDecimalPlaces;
        private Label lblValueNumberOfDecimalPlaces;
        private Label lblValueMaxNumberCharactersInWord;
        private RadioButton rdbTagsBasedRequestedTableRows;
        private RadioButton rdbTagsBasedRequestedTableColumns;
    }
}