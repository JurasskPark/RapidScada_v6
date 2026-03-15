namespace Scada.Comm.Drivers.DrvDebug.View.Forms
{
    partial class FrmProject
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpType = new GroupBox();
            rdbMixed = new RadioButton();
            rdbSlave = new RadioButton();
            rdbMaster = new RadioButton();
            grpStopCondition = new GroupBox();
            lblMarkerHint = new Label();
            lblLengthHint = new Label();
            chkLengthIncludesItself = new CheckBox();
            rdbLengthMode = new RadioButton();
            rdbMarkerMode = new RadioButton();
            lblMode = new Label();
            txtCheckValue = new TextBox();
            cmbCheckFormat = new ComboBox();
            nudCheckLength = new NumericUpDown();
            nudCheckAddress = new NumericUpDown();
            lblCheckValue = new Label();
            lblCheckFormat = new Label();
            lblCheckLength = new Label();
            lblCheckLengthMode = new Label();
            lblCheckAddress = new Label();
            lblCheckAddressMode = new Label();
            tabMain = new TabControl();
            tabCommands = new TabPage();
            btnCommandDown = new Button();
            btnCommandUp = new Button();
            btnDeleteCommand = new Button();
            btnAddCommand = new Button();
            dgvCommands = new DataGridView();
            colCmdEnabled = new DataGridViewCheckBoxColumn();
            colCmdName = new DataGridViewTextBoxColumn();
            colCmdKind = new DataGridViewComboBoxColumn();
            colCmdPayload = new DataGridViewTextBoxColumn();
            colCmdDelay = new DataGridViewTextBoxColumn();
            colCmdNote = new DataGridViewTextBoxColumn();
            tabTags = new TabPage();
            btnEditTags = new Button();
            dgvTags = new DataGridView();
            colTagName = new DataGridViewTextBoxColumn();
            colTagChannel = new DataGridViewTextBoxColumn();
            colTagMode = new DataGridViewTextBoxColumn();
            colTagIndex = new DataGridViewTextBoxColumn();
            colTagLength = new DataGridViewTextBoxColumn();
            colTagFormat = new DataGridViewTextBoxColumn();
            colTagSimulation = new DataGridViewTextBoxColumn();
            colTagPreview = new DataGridViewTextBoxColumn();
            grpOptions = new GroupBox();
            cmbLogType = new ComboBox();
            chkWriteLog = new CheckBox();
            lblLogType = new Label();
            btnSave = new Button();
            btnClose = new Button();
            grpType.SuspendLayout();
            grpStopCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCheckLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCheckAddress).BeginInit();
            tabMain.SuspendLayout();
            tabCommands.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvCommands).BeginInit();
            tabTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            grpOptions.SuspendLayout();
            SuspendLayout();
            // 
            // grpType
            // 
            grpType.Controls.Add(rdbMixed);
            grpType.Controls.Add(rdbSlave);
            grpType.Controls.Add(rdbMaster);
            grpType.Location = new Point(12, 12);
            grpType.Name = "grpType";
            grpType.Size = new Size(180, 166);
            grpType.TabIndex = 5;
            grpType.TabStop = false;
            grpType.Text = "Type";
            // 
            // rdbMixed
            // 
            rdbMixed.AutoSize = true;
            rdbMixed.Location = new Point(16, 74);
            rdbMixed.Name = "rdbMixed";
            rdbMixed.Size = new Size(58, 19);
            rdbMixed.TabIndex = 0;
            rdbMixed.Text = "Mixed";
            rdbMixed.CheckedChanged += controls_Changed;
            // 
            // rdbSlave
            // 
            rdbSlave.AutoSize = true;
            rdbSlave.Location = new Point(16, 49);
            rdbSlave.Name = "rdbSlave";
            rdbSlave.Size = new Size(52, 19);
            rdbSlave.TabIndex = 1;
            rdbSlave.Text = "Slave";
            rdbSlave.CheckedChanged += controls_Changed;
            // 
            // rdbMaster
            // 
            rdbMaster.AutoSize = true;
            rdbMaster.Location = new Point(16, 24);
            rdbMaster.Name = "rdbMaster";
            rdbMaster.Size = new Size(61, 19);
            rdbMaster.TabIndex = 2;
            rdbMaster.Text = "Master";
            rdbMaster.CheckedChanged += controls_Changed;
            // 
            // grpStopCondition
            // 
            grpStopCondition.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpStopCondition.Controls.Add(lblMarkerHint);
            grpStopCondition.Controls.Add(lblLengthHint);
            grpStopCondition.Controls.Add(chkLengthIncludesItself);
            grpStopCondition.Controls.Add(rdbLengthMode);
            grpStopCondition.Controls.Add(rdbMarkerMode);
            grpStopCondition.Controls.Add(lblMode);
            grpStopCondition.Controls.Add(txtCheckValue);
            grpStopCondition.Controls.Add(cmbCheckFormat);
            grpStopCondition.Controls.Add(nudCheckLength);
            grpStopCondition.Controls.Add(nudCheckAddress);
            grpStopCondition.Controls.Add(lblCheckValue);
            grpStopCondition.Controls.Add(lblCheckFormat);
            grpStopCondition.Controls.Add(lblCheckLength);
            grpStopCondition.Controls.Add(lblCheckLengthMode);
            grpStopCondition.Controls.Add(lblCheckAddress);
            grpStopCondition.Controls.Add(lblCheckAddressMode);
            grpStopCondition.Location = new Point(198, 12);
            grpStopCondition.Name = "grpStopCondition";
            grpStopCondition.Size = new Size(754, 166);
            grpStopCondition.TabIndex = 4;
            grpStopCondition.TabStop = false;
            grpStopCondition.Text = "Stop condition";
            // 
            // lblMarkerHint
            // 
            lblMarkerHint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblMarkerHint.AutoSize = true;
            lblMarkerHint.Location = new Point(16, 138);
            lblMarkerHint.Name = "lblMarkerHint";
            lblMarkerHint.Size = new Size(155, 15);
            lblMarkerHint.TabIndex = 0;
            lblMarkerHint.Text = "Example: 0xFF, 123, 0xABCD";
            // 
            // lblLengthHint
            // 
            lblLengthHint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblLengthHint.AutoSize = true;
            lblLengthHint.Location = new Point(414, 138);
            lblLengthHint.Name = "lblLengthHint";
            lblLengthHint.Size = new Size(197, 15);
            lblLengthHint.TabIndex = 1;
            lblLengthHint.Text = "Length mode uses field value as size";
            // 
            // chkLengthIncludesItself
            // 
            chkLengthIncludesItself.AutoSize = true;
            chkLengthIncludesItself.Location = new Point(580, 54);
            chkLengthIncludesItself.Name = "chkLengthIncludesItself";
            chkLengthIncludesItself.Size = new Size(138, 19);
            chkLengthIncludesItself.TabIndex = 2;
            chkLengthIncludesItself.Text = "Length includes itself";
            chkLengthIncludesItself.CheckedChanged += controls_Changed;
            // 
            // rdbLengthMode
            // 
            rdbLengthMode.AutoSize = true;
            rdbLengthMode.Location = new Point(580, 22);
            rdbLengthMode.Name = "rdbLengthMode";
            rdbLengthMode.Size = new Size(62, 19);
            rdbLengthMode.TabIndex = 3;
            rdbLengthMode.Text = "Length";
            rdbLengthMode.CheckedChanged += StopConditionMode_Changed;
            // 
            // rdbMarkerMode
            // 
            rdbMarkerMode.AutoSize = true;
            rdbMarkerMode.Checked = true;
            rdbMarkerMode.Location = new Point(503, 22);
            rdbMarkerMode.Name = "rdbMarkerMode";
            rdbMarkerMode.Size = new Size(62, 19);
            rdbMarkerMode.TabIndex = 4;
            rdbMarkerMode.TabStop = true;
            rdbMarkerMode.Text = "Marker";
            rdbMarkerMode.CheckedChanged += StopConditionMode_Changed;
            // 
            // lblMode
            // 
            lblMode.AutoSize = true;
            lblMode.Location = new Point(414, 26);
            lblMode.Name = "lblMode";
            lblMode.Size = new Size(38, 15);
            lblMode.TabIndex = 5;
            lblMode.Text = "Mode";
            // 
            // txtCheckValue
            // 
            txtCheckValue.Location = new Point(162, 108);
            txtCheckValue.Name = "txtCheckValue";
            txtCheckValue.Size = new Size(120, 23);
            txtCheckValue.TabIndex = 6;
            txtCheckValue.TextChanged += controls_Changed;
            // 
            // cmbCheckFormat
            // 
            cmbCheckFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCheckFormat.Location = new Point(162, 79);
            cmbCheckFormat.Name = "cmbCheckFormat";
            cmbCheckFormat.Size = new Size(120, 23);
            cmbCheckFormat.TabIndex = 7;
            cmbCheckFormat.SelectedIndexChanged += cmbCheckFormat_SelectedIndexChanged;
            // 
            // nudCheckLength
            // 
            nudCheckLength.Location = new Point(162, 50);
            nudCheckLength.Maximum = new decimal(new int[] { 8, 0, 0, 0 });
            nudCheckLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCheckLength.Name = "nudCheckLength";
            nudCheckLength.Size = new Size(120, 23);
            nudCheckLength.TabIndex = 8;
            nudCheckLength.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudCheckLength.ValueChanged += controls_Changed;
            // 
            // nudCheckAddress
            // 
            nudCheckAddress.Location = new Point(162, 22);
            nudCheckAddress.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            nudCheckAddress.Name = "nudCheckAddress";
            nudCheckAddress.Size = new Size(120, 23);
            nudCheckAddress.TabIndex = 9;
            nudCheckAddress.ValueChanged += controls_Changed;
            // 
            // lblCheckValue
            // 
            lblCheckValue.AutoSize = true;
            lblCheckValue.Location = new Point(16, 112);
            lblCheckValue.Name = "lblCheckValue";
            lblCheckValue.Size = new Size(84, 15);
            lblCheckValue.TabIndex = 10;
            lblCheckValue.Text = "Hex/Dec value";
            // 
            // lblCheckFormat
            // 
            lblCheckFormat.AutoSize = true;
            lblCheckFormat.Location = new Point(16, 83);
            lblCheckFormat.Name = "lblCheckFormat";
            lblCheckFormat.Size = new Size(45, 15);
            lblCheckFormat.TabIndex = 11;
            lblCheckFormat.Text = "Format";
            // 
            // lblCheckLength
            // 
            lblCheckLength.AutoSize = true;
            lblCheckLength.Location = new Point(16, 54);
            lblCheckLength.Name = "lblCheckLength";
            lblCheckLength.Size = new Size(77, 15);
            lblCheckLength.TabIndex = 12;
            lblCheckLength.Text = "Check length";
            // 
            // lblCheckLengthMode
            // 
            lblCheckLengthMode.AutoSize = true;
            lblCheckLengthMode.Location = new Point(16, 54);
            lblCheckLengthMode.Name = "lblCheckLengthMode";
            lblCheckLengthMode.Size = new Size(92, 15);
            lblCheckLengthMode.TabIndex = 14;
            lblCheckLengthMode.Text = "Length field size";
            lblCheckLengthMode.Visible = false;
            // 
            // lblCheckAddress
            // 
            lblCheckAddress.AutoSize = true;
            lblCheckAddress.Location = new Point(16, 26);
            lblCheckAddress.Name = "lblCheckAddress";
            lblCheckAddress.Size = new Size(83, 15);
            lblCheckAddress.TabIndex = 13;
            lblCheckAddress.Text = "Check address";
            // 
            // lblCheckAddressMode
            // 
            lblCheckAddressMode.AutoSize = true;
            lblCheckAddressMode.Location = new Point(16, 26);
            lblCheckAddressMode.Name = "lblCheckAddressMode";
            lblCheckAddressMode.Size = new Size(113, 15);
            lblCheckAddressMode.TabIndex = 15;
            lblCheckAddressMode.Text = "Length field address";
            lblCheckAddressMode.Visible = false;
            // 
            // tabMain
            // 
            tabMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabMain.Controls.Add(tabCommands);
            tabMain.Controls.Add(tabTags);
            tabMain.Location = new Point(12, 184);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(940, 412);
            tabMain.TabIndex = 3;
            // 
            // tabCommands
            // 
            tabCommands.Controls.Add(btnCommandDown);
            tabCommands.Controls.Add(btnCommandUp);
            tabCommands.Controls.Add(btnDeleteCommand);
            tabCommands.Controls.Add(btnAddCommand);
            tabCommands.Controls.Add(dgvCommands);
            tabCommands.Location = new Point(4, 24);
            tabCommands.Name = "tabCommands";
            tabCommands.Size = new Size(932, 384);
            tabCommands.TabIndex = 0;
            tabCommands.Text = "Commands";
            // 
            // btnCommandDown
            // 
            btnCommandDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCommandDown.Location = new Point(303, 353);
            btnCommandDown.Name = "btnCommandDown";
            btnCommandDown.Size = new Size(90, 27);
            btnCommandDown.TabIndex = 0;
            btnCommandDown.Text = "Down";
            btnCommandDown.Click += btnCommandDown_Click;
            // 
            // btnCommandUp
            // 
            btnCommandUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCommandUp.Location = new Point(207, 353);
            btnCommandUp.Name = "btnCommandUp";
            btnCommandUp.Size = new Size(90, 27);
            btnCommandUp.TabIndex = 1;
            btnCommandUp.Text = "Up";
            btnCommandUp.Click += btnCommandUp_Click;
            // 
            // btnDeleteCommand
            // 
            btnDeleteCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDeleteCommand.Location = new Point(111, 353);
            btnDeleteCommand.Name = "btnDeleteCommand";
            btnDeleteCommand.Size = new Size(90, 27);
            btnDeleteCommand.TabIndex = 2;
            btnDeleteCommand.Text = "Delete";
            btnDeleteCommand.Click += btnDeleteCommand_Click;
            // 
            // btnAddCommand
            // 
            btnAddCommand.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddCommand.Location = new Point(15, 353);
            btnAddCommand.Name = "btnAddCommand";
            btnAddCommand.Size = new Size(90, 27);
            btnAddCommand.TabIndex = 3;
            btnAddCommand.Text = "Add";
            btnAddCommand.Click += btnAddCommand_Click;
            // 
            // dgvCommands
            // 
            dgvCommands.AllowUserToAddRows = false;
            dgvCommands.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCommands.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvCommands.Columns.AddRange(new DataGridViewColumn[] { colCmdEnabled, colCmdName, colCmdKind, colCmdPayload, colCmdDelay, colCmdNote });
            dgvCommands.Location = new Point(15, 15);
            dgvCommands.Name = "dgvCommands";
            dgvCommands.RowHeadersWidth = 62;
            dgvCommands.Size = new Size(902, 328);
            dgvCommands.TabIndex = 4;
            dgvCommands.CellValueChanged += dgvCommands_CellValueChanged;
            dgvCommands.CurrentCellDirtyStateChanged += dgvCommands_CurrentCellDirtyStateChanged;
            // 
            // colCmdEnabled
            // 
            colCmdEnabled.DataPropertyName = "Enabled";
            colCmdEnabled.HeaderText = "On";
            colCmdEnabled.MinimumWidth = 8;
            colCmdEnabled.Name = "colCmdEnabled";
            colCmdEnabled.Width = 70;
            // 
            // colCmdName
            // 
            colCmdName.DataPropertyName = "Name";
            colCmdName.HeaderText = "Name";
            colCmdName.MinimumWidth = 8;
            colCmdName.Name = "colCmdName";
            colCmdName.Width = 180;
            // 
            // colCmdKind
            // 
            colCmdKind.DataPropertyName = "DataKind";
            colCmdKind.HeaderText = "Type";
            colCmdKind.MinimumWidth = 8;
            colCmdKind.Name = "colCmdKind";
            colCmdKind.Width = 120;
            // 
            // colCmdPayload
            // 
            colCmdPayload.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colCmdPayload.DataPropertyName = "Payload";
            colCmdPayload.HeaderText = "Payload";
            colCmdPayload.MinimumWidth = 8;
            colCmdPayload.Name = "colCmdPayload";
            // 
            // colCmdDelay
            // 
            colCmdDelay.DataPropertyName = "DelayMs";
            colCmdDelay.HeaderText = "Delay ms";
            colCmdDelay.MinimumWidth = 8;
            colCmdDelay.Name = "colCmdDelay";
            colCmdDelay.Width = 110;
            // 
            // colCmdNote
            // 
            colCmdNote.DataPropertyName = "Note";
            colCmdNote.HeaderText = "Note";
            colCmdNote.MinimumWidth = 8;
            colCmdNote.Name = "colCmdNote";
            colCmdNote.Width = 180;
            // 
            // tabTags
            // 
            tabTags.Controls.Add(btnEditTags);
            tabTags.Controls.Add(dgvTags);
            tabTags.Location = new Point(4, 24);
            tabTags.Name = "tabTags";
            tabTags.Size = new Size(932, 384);
            tabTags.TabIndex = 1;
            tabTags.Text = "Tags";
            // 
            // btnEditTags
            // 
            btnEditTags.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEditTags.Location = new Point(15, 353);
            btnEditTags.Name = "btnEditTags";
            btnEditTags.Size = new Size(180, 27);
            btnEditTags.TabIndex = 0;
            btnEditTags.Text = "Edit tags";
            btnEditTags.Click += btnEditTags_Click;
            // 
            // dgvTags
            // 
            dgvTags.AllowUserToAddRows = false;
            dgvTags.AllowUserToDeleteRows = false;
            dgvTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTags.Columns.AddRange(new DataGridViewColumn[] { colTagName, colTagChannel, colTagMode, colTagIndex, colTagLength, colTagFormat, colTagSimulation, colTagPreview });
            dgvTags.Location = new Point(15, 15);
            dgvTags.Name = "dgvTags";
            dgvTags.ReadOnly = true;
            dgvTags.RowHeadersVisible = false;
            dgvTags.RowHeadersWidth = 62;
            dgvTags.Size = new Size(902, 328);
            dgvTags.TabIndex = 1;
            // 
            // colTagName
            // 
            colTagName.HeaderText = "Name";
            colTagName.MinimumWidth = 8;
            colTagName.Name = "colTagName";
            colTagName.ReadOnly = true;
            colTagName.Width = 180;
            // 
            // colTagChannel
            // 
            colTagChannel.HeaderText = "Channel";
            colTagChannel.MinimumWidth = 8;
            colTagChannel.Name = "colTagChannel";
            colTagChannel.ReadOnly = true;
            colTagChannel.Width = 150;
            // 
            // colTagMode
            // 
            colTagMode.HeaderText = "Mode";
            colTagMode.MinimumWidth = 8;
            colTagMode.Name = "colTagMode";
            colTagMode.ReadOnly = true;
            colTagMode.Width = 150;
            // 
            // colTagIndex
            // 
            colTagIndex.HeaderText = "Index";
            colTagIndex.MinimumWidth = 8;
            colTagIndex.Name = "colTagIndex";
            colTagIndex.ReadOnly = true;
            colTagIndex.Width = 150;
            // 
            // colTagLength
            // 
            colTagLength.HeaderText = "Length";
            colTagLength.MinimumWidth = 8;
            colTagLength.Name = "colTagLength";
            colTagLength.ReadOnly = true;
            colTagLength.Width = 150;
            // 
            // colTagFormat
            // 
            colTagFormat.HeaderText = "Format";
            colTagFormat.MinimumWidth = 8;
            colTagFormat.Name = "colTagFormat";
            colTagFormat.ReadOnly = true;
            colTagFormat.Width = 120;
            // 
            // colTagSimulation
            // 
            colTagSimulation.HeaderText = "Simulation";
            colTagSimulation.MinimumWidth = 8;
            colTagSimulation.Name = "colTagSimulation";
            colTagSimulation.ReadOnly = true;
            colTagSimulation.Width = 120;
            // 
            // colTagPreview
            // 
            colTagPreview.HeaderText = "Preview";
            colTagPreview.MinimumWidth = 8;
            colTagPreview.Name = "colTagPreview";
            colTagPreview.ReadOnly = true;
            colTagPreview.Width = 180;
            // 
            // grpOptions
            // 
            grpOptions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpOptions.Controls.Add(cmbLogType);
            grpOptions.Controls.Add(chkWriteLog);
            grpOptions.Controls.Add(lblLogType);
            grpOptions.Location = new Point(12, 602);
            grpOptions.Name = "grpOptions";
            grpOptions.Size = new Size(940, 104);
            grpOptions.TabIndex = 2;
            grpOptions.TabStop = false;
            grpOptions.Text = "Options";
            // 
            // cmbLogType
            // 
            cmbLogType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbLogType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLogType.Location = new Point(733, 53);
            cmbLogType.Name = "cmbLogType";
            cmbLogType.Size = new Size(180, 23);
            cmbLogType.TabIndex = 0;
            cmbLogType.SelectedIndexChanged += controls_Changed;
            // 
            // chkWriteLog
            // 
            chkWriteLog.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkWriteLog.AutoSize = true;
            chkWriteLog.Location = new Point(639, 24);
            chkWriteLog.Name = "chkWriteLog";
            chkWriteLog.Size = new Size(74, 19);
            chkWriteLog.TabIndex = 1;
            chkWriteLog.Text = "Write log";
            chkWriteLog.CheckedChanged += controls_Changed;
            // 
            // lblLogType
            // 
            lblLogType.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblLogType.AutoSize = true;
            lblLogType.Location = new Point(639, 56);
            lblLogType.Name = "lblLogType";
            lblLogType.Size = new Size(53, 15);
            lblLogType.TabIndex = 2;
            lblLogType.Text = "Log type";
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(766, 716);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 27);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(862, 716);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(90, 27);
            btnClose.TabIndex = 0;
            btnClose.Text = "Close";
            btnClose.Click += btnClose_Click;
            // 
            // FrmProject
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(964, 755);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(grpOptions);
            Controls.Add(tabMain);
            Controls.Add(grpStopCondition);
            Controls.Add(grpType);
            MinimumSize = new Size(845, 700);
            Name = "FrmProject";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DrvDebug";
            Load += FrmProject_Load;
            grpType.ResumeLayout(false);
            grpType.PerformLayout();
            grpStopCondition.ResumeLayout(false);
            grpStopCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCheckLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCheckAddress).EndInit();
            tabMain.ResumeLayout(false);
            tabCommands.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvCommands).EndInit();
            tabTags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            grpOptions.ResumeLayout(false);
            grpOptions.PerformLayout();
            ResumeLayout(false);
        }

        private GroupBox grpType;
        private RadioButton rdbMixed;
        private RadioButton rdbSlave;
        private RadioButton rdbMaster;
        private GroupBox grpStopCondition;
        private Label lblMarkerHint;
        private Label lblLengthHint;
        private CheckBox chkLengthIncludesItself;
        private RadioButton rdbLengthMode;
        private RadioButton rdbMarkerMode;
        private Label lblMode;
        private TextBox txtCheckValue;
        private ComboBox cmbCheckFormat;
        private NumericUpDown nudCheckLength;
        private NumericUpDown nudCheckAddress;
        private Label lblCheckValue;
        private Label lblCheckFormat;
        private Label lblCheckLength;
        private Label lblCheckLengthMode;
        private Label lblCheckAddress;
        private Label lblCheckAddressMode;
        private TabControl tabMain;
        private TabPage tabCommands;
        private TabPage tabTags;
        private DataGridView dgvCommands;
        private DataGridViewCheckBoxColumn colCmdEnabled;
        private DataGridViewTextBoxColumn colCmdName;
        private DataGridViewComboBoxColumn colCmdKind;
        private DataGridViewTextBoxColumn colCmdPayload;
        private DataGridViewTextBoxColumn colCmdDelay;
        private DataGridViewTextBoxColumn colCmdNote;
        private Button btnAddCommand;
        private Button btnDeleteCommand;
        private Button btnCommandUp;
        private Button btnCommandDown;
        private DataGridView dgvTags;
        private DataGridViewTextBoxColumn colTagName;
        private DataGridViewTextBoxColumn colTagChannel;
        private DataGridViewTextBoxColumn colTagMode;
        private DataGridViewTextBoxColumn colTagIndex;
        private DataGridViewTextBoxColumn colTagLength;
        private DataGridViewTextBoxColumn colTagFormat;
        private DataGridViewTextBoxColumn colTagSimulation;
        private DataGridViewTextBoxColumn colTagPreview;
        private Button btnEditTags;
        private GroupBox grpOptions;
        private ComboBox cmbLogType;
        private CheckBox chkWriteLog;
        private Label lblLogType;
        private Button btnSave;
        private Button btnClose;
    }
}
