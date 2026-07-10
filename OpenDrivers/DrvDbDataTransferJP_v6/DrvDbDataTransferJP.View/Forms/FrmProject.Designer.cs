namespace Scada.Comm.Drivers.DrvDbDataTransferJP.View.Forms
{
    partial class FrmProject
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProject));
            cbSrcDataSourceType = new ComboBox();
            tabControl = new TabControl();
            pageCommandsImport = new TabPage();
            lstImportCommands = new ListView();
            clmImportCmdNum = new ColumnHeader();
            clmImportCmdCode = new ColumnHeader();
            clmImportCmdName = new ColumnHeader();
            clmImportCmdDescription = new ColumnHeader();
            clmImportCmdEnabled = new ColumnHeader();
            cmnuImportCommands = new ContextMenuStrip(components);
            cmnuListImportCmdAdd = new ToolStripMenuItem();
            cmnuListImportCmdChange = new ToolStripMenuItem();
            cmnuListImportCmdDelete = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            cmnuListImportCmdUp = new ToolStripMenuItem();
            cmnuListImportCmdDown = new ToolStripMenuItem();
            pageCommandsExport = new TabPage();
            lstExportCommands = new ListView();
            clmExportCmdNum = new ColumnHeader();
            clmExportCmdCode = new ColumnHeader();
            clmExportCmdName = new ColumnHeader();
            clmExportCmdDescription = new ColumnHeader();
            clmExportCmdEnabled = new ColumnHeader();
            cmnuExportCommands = new ContextMenuStrip(components);
            cmnuListExportCmdAdd = new ToolStripMenuItem();
            cmnuListExportCmdChange = new ToolStripMenuItem();
            cmnuListExportCmdDelete = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            cmnuListExportCmdUp = new ToolStripMenuItem();
            cmnuListExportCmdDown = new ToolStripMenuItem();
            pageDatabase = new TabPage();
            gbSrcDatabase = new GroupBox();
            btnSrcConnectionTest = new Button();
            pageSettings = new TabPage();
            ckbWriteDriverLog = new CheckBox();
            pageHelp = new TabPage();
            txtHelp = new FastColoredTextBoxNS.FastColoredTextBox();
            imgList = new ImageList(components);
            pnlBottom = new Panel();
            btnClose = new Button();
            btnSave = new Button();
            txtSrcTimeout = new TextBox();
            lblSrcTimeout = new Label();
            txtSrcOptionalOptions = new TextBox();
            lblSrcOptionalOptions = new Label();
            txtSrcPort = new TextBox();
            lblSrcPort = new Label();
            txtSrcConnectionString = new TextBox();
            lblSrcConnectionString = new Label();
            txtSrcPassword = new TextBox();
            lblSrcPassword = new Label();
            txtSrcUser = new TextBox();
            lblSrcUser = new Label();
            txtSrcDatabase = new TextBox();
            lblSrcDatabase = new Label();
            txtSrcServer = new TextBox();
            lblSrcServer = new Label();
            gbTgtDatabase = new GroupBox();
            txtTgtTimeout = new TextBox();
            lblTgtTimeout = new Label();
            txtTgtOptionalOptions = new TextBox();
            lblTgtOptionalOptions = new Label();
            txtTgtPort = new TextBox();
            lblTgtPort = new Label();
            txtTgtConnectionString = new TextBox();
            lblTgtConnectionString = new Label();
            txtTgtPassword = new TextBox();
            lblTgtPassword = new Label();
            txtTgtUser = new TextBox();
            lblTgtUser = new Label();
            txtTgtDatabase = new TextBox();
            lblTgtDatabase = new Label();
            txtTgtServer = new TextBox();
            lblTgtServer = new Label();
            btnTgtConnectionTest = new Button();
            cbTgtDataSourceType = new ComboBox();
            tabControl.SuspendLayout();
            pageCommandsImport.SuspendLayout();
            cmnuImportCommands.SuspendLayout();
            pageCommandsExport.SuspendLayout();
            cmnuExportCommands.SuspendLayout();
            pageDatabase.SuspendLayout();
            gbSrcDatabase.SuspendLayout();
            pageSettings.SuspendLayout();
            pageHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtHelp).BeginInit();
            pnlBottom.SuspendLayout();
            gbTgtDatabase.SuspendLayout();
            SuspendLayout();
            // 
            // cbSrcDataSourceType
            // 
            cbSrcDataSourceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSrcDataSourceType.FormattingEnabled = true;
            cbSrcDataSourceType.Items.AddRange(new object[] { "<Choose database type>", "Microsoft SQL Server", "Oracle", "PostgreSQL", "MySQL", "Firebird", "InfluxDBv2", "InfluxDBv3" });
            cbSrcDataSourceType.Location = new Point(21, 38);
            cbSrcDataSourceType.Margin = new Padding(6, 5, 6, 5);
            cbSrcDataSourceType.Name = "cbSrcDataSourceType";
            cbSrcDataSourceType.Size = new Size(387, 33);
            cbSrcDataSourceType.TabIndex = 0;
            cbSrcDataSourceType.SelectedIndexChanged += cbDataSourceType_SelectedIndexChanged;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(pageCommandsImport);
            tabControl.Controls.Add(pageCommandsExport);
            tabControl.Controls.Add(pageDatabase);
            tabControl.Controls.Add(pageSettings);
            tabControl.Controls.Add(pageHelp);
            tabControl.Dock = DockStyle.Fill;
            tabControl.ImageList = imgList;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(6, 5, 6, 5);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1704, 680);
            tabControl.TabIndex = 0;
            // 
            // pageCommandsImport
            // 
            pageCommandsImport.Controls.Add(lstImportCommands);
            pageCommandsImport.ImageIndex = 2;
            pageCommandsImport.Location = new Point(4, 34);
            pageCommandsImport.Margin = new Padding(6, 5, 6, 5);
            pageCommandsImport.Name = "pageCommandsImport";
            pageCommandsImport.Padding = new Padding(6, 5, 6, 5);
            pageCommandsImport.Size = new Size(1696, 824);
            pageCommandsImport.TabIndex = 1;
            pageCommandsImport.Text = "Import Commands";
            pageCommandsImport.UseVisualStyleBackColor = true;
            // 
            // lstImportCommands
            // 
            lstImportCommands.Columns.AddRange(new ColumnHeader[] { clmImportCmdNum, clmImportCmdCode, clmImportCmdName, clmImportCmdDescription, clmImportCmdEnabled });
            lstImportCommands.ContextMenuStrip = cmnuImportCommands;
            lstImportCommands.Dock = DockStyle.Fill;
            lstImportCommands.FullRowSelect = true;
            lstImportCommands.GridLines = true;
            lstImportCommands.Location = new Point(6, 5);
            lstImportCommands.Name = "lstImportCommands";
            lstImportCommands.Size = new Size(1684, 814);
            lstImportCommands.TabIndex = 1;
            lstImportCommands.UseCompatibleStateImageBehavior = false;
            lstImportCommands.View = System.Windows.Forms.View.Details;
            // 
            // clmImportCmdNum
            // 
            clmImportCmdNum.Text = "Number";
            clmImportCmdNum.Width = 120;
            // 
            // clmImportCmdCode
            // 
            clmImportCmdCode.Text = "Code";
            clmImportCmdCode.Width = 120;
            // 
            // clmImportCmdName
            // 
            clmImportCmdName.Text = "Name";
            clmImportCmdName.Width = 200;
            // 
            // clmImportCmdDescription
            // 
            clmImportCmdDescription.Text = "Description";
            clmImportCmdDescription.Width = 350;
            // 
            // clmImportCmdEnabled
            // 
            clmImportCmdEnabled.Text = "Enabled";
            clmImportCmdEnabled.Width = 100;
            // 
            // cmnuImportCommands
            // 
            cmnuImportCommands.ImageScalingSize = new Size(24, 24);
            cmnuImportCommands.Items.AddRange(new ToolStripItem[] { cmnuListImportCmdAdd, cmnuListImportCmdChange, cmnuListImportCmdDelete, toolStripSeparator1, cmnuListImportCmdUp, cmnuListImportCmdDown });
            cmnuImportCommands.Name = "cmnuImportCommands";
            cmnuImportCommands.Size = new Size(242, 170);
            // 
            // cmnuListImportCmdAdd
            // 
            cmnuListImportCmdAdd.Image = (Image)resources.GetObject("cmnuListImportCmdAdd.Image");
            cmnuListImportCmdAdd.Name = "cmnuListImportCmdAdd";
            cmnuListImportCmdAdd.Size = new Size(241, 32);
            cmnuListImportCmdAdd.Text = "Add Command";
            cmnuListImportCmdAdd.Click += cmnuListImportCmdAdd_Click;
            // 
            // cmnuListImportCmdChange
            // 
            cmnuListImportCmdChange.Image = (Image)resources.GetObject("cmnuListImportCmdChange.Image");
            cmnuListImportCmdChange.Name = "cmnuListImportCmdChange";
            cmnuListImportCmdChange.Size = new Size(241, 32);
            cmnuListImportCmdChange.Text = "Change Command";
            cmnuListImportCmdChange.Click += cmnuListImportCmdChange_Click;
            // 
            // cmnuListImportCmdDelete
            // 
            cmnuListImportCmdDelete.Image = (Image)resources.GetObject("cmnuListImportCmdDelete.Image");
            cmnuListImportCmdDelete.Name = "cmnuListImportCmdDelete";
            cmnuListImportCmdDelete.Size = new Size(241, 32);
            cmnuListImportCmdDelete.Text = "Delete Command";
            cmnuListImportCmdDelete.Click += cmnuListImportCmdDelete_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(238, 6);
            // 
            // cmnuListImportCmdUp
            // 
            cmnuListImportCmdUp.Image = (Image)resources.GetObject("cmnuListImportCmdUp.Image");
            cmnuListImportCmdUp.Name = "cmnuListImportCmdUp";
            cmnuListImportCmdUp.Size = new Size(241, 32);
            cmnuListImportCmdUp.Text = "Up";
            cmnuListImportCmdUp.Click += cmnuListImportCmdUp_Click;
            // 
            // cmnuListImportCmdDown
            // 
            cmnuListImportCmdDown.Image = (Image)resources.GetObject("cmnuListImportCmdDown.Image");
            cmnuListImportCmdDown.Name = "cmnuListImportCmdDown";
            cmnuListImportCmdDown.Size = new Size(241, 32);
            cmnuListImportCmdDown.Text = "Down";
            cmnuListImportCmdDown.Click += cmnuListImportCmdDown_Click;
            // 
            // pageCommandsExport
            // 
            pageCommandsExport.Controls.Add(lstExportCommands);
            pageCommandsExport.ImageIndex = 3;
            pageCommandsExport.Location = new Point(4, 34);
            pageCommandsExport.Margin = new Padding(6, 5, 6, 5);
            pageCommandsExport.Name = "pageCommandsExport";
            pageCommandsExport.Padding = new Padding(6, 5, 6, 5);
            pageCommandsExport.Size = new Size(1696, 824);
            pageCommandsExport.TabIndex = 2;
            pageCommandsExport.Text = "Export Commands";
            pageCommandsExport.UseVisualStyleBackColor = true;
            // 
            // lstExportCommands
            // 
            lstExportCommands.Columns.AddRange(new ColumnHeader[] { clmExportCmdNum, clmExportCmdCode, clmExportCmdName, clmExportCmdDescription, clmExportCmdEnabled });
            lstExportCommands.ContextMenuStrip = cmnuExportCommands;
            lstExportCommands.Dock = DockStyle.Fill;
            lstExportCommands.FullRowSelect = true;
            lstExportCommands.GridLines = true;
            lstExportCommands.Location = new Point(6, 5);
            lstExportCommands.Name = "lstExportCommands";
            lstExportCommands.Size = new Size(1684, 814);
            lstExportCommands.TabIndex = 2;
            lstExportCommands.UseCompatibleStateImageBehavior = false;
            lstExportCommands.View = System.Windows.Forms.View.Details;
            // 
            // clmExportCmdNum
            // 
            clmExportCmdNum.Text = "Number";
            clmExportCmdNum.Width = 120;
            // 
            // clmExportCmdCode
            // 
            clmExportCmdCode.Text = "Code";
            clmExportCmdCode.Width = 120;
            // 
            // clmExportCmdName
            // 
            clmExportCmdName.Text = "Name";
            clmExportCmdName.Width = 200;
            // 
            // clmExportCmdDescription
            // 
            clmExportCmdDescription.Text = "Description";
            clmExportCmdDescription.Width = 350;
            // 
            // clmExportCmdEnabled
            // 
            clmExportCmdEnabled.Text = "Enabled";
            clmExportCmdEnabled.Width = 100;
            // 
            // cmnuExportCommands
            // 
            cmnuExportCommands.ImageScalingSize = new Size(24, 24);
            cmnuExportCommands.Items.AddRange(new ToolStripItem[] { cmnuListExportCmdAdd, cmnuListExportCmdChange, cmnuListExportCmdDelete, toolStripSeparator2, cmnuListExportCmdUp, cmnuListExportCmdDown });
            cmnuExportCommands.Name = "cmnuImportCommands";
            cmnuExportCommands.Size = new Size(242, 170);
            // 
            // cmnuListExportCmdAdd
            // 
            cmnuListExportCmdAdd.Image = (Image)resources.GetObject("cmnuListExportCmdAdd.Image");
            cmnuListExportCmdAdd.Name = "cmnuListExportCmdAdd";
            cmnuListExportCmdAdd.Size = new Size(241, 32);
            cmnuListExportCmdAdd.Text = "Add Command";
            cmnuListExportCmdAdd.Click += cmnuListExportCmdAdd_Click;
            // 
            // cmnuListExportCmdChange
            // 
            cmnuListExportCmdChange.Image = (Image)resources.GetObject("cmnuListExportCmdChange.Image");
            cmnuListExportCmdChange.Name = "cmnuListExportCmdChange";
            cmnuListExportCmdChange.Size = new Size(241, 32);
            cmnuListExportCmdChange.Text = "Change Command";
            cmnuListExportCmdChange.Click += cmnuListExportCmdChange_Click;
            // 
            // cmnuListExportCmdDelete
            // 
            cmnuListExportCmdDelete.Image = (Image)resources.GetObject("cmnuListExportCmdDelete.Image");
            cmnuListExportCmdDelete.Name = "cmnuListExportCmdDelete";
            cmnuListExportCmdDelete.Size = new Size(241, 32);
            cmnuListExportCmdDelete.Text = "Delete Command";
            cmnuListExportCmdDelete.Click += cmnuListExportCmdDelete_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(238, 6);
            // 
            // cmnuListExportCmdUp
            // 
            cmnuListExportCmdUp.Image = (Image)resources.GetObject("cmnuListExportCmdUp.Image");
            cmnuListExportCmdUp.Name = "cmnuListExportCmdUp";
            cmnuListExportCmdUp.Size = new Size(241, 32);
            cmnuListExportCmdUp.Text = "Up";
            cmnuListExportCmdUp.Click += cmnuListExportCmdDown_Click;
            // 
            // cmnuListExportCmdDown
            // 
            cmnuListExportCmdDown.Image = (Image)resources.GetObject("cmnuListExportCmdDown.Image");
            cmnuListExportCmdDown.Name = "cmnuListExportCmdDown";
            cmnuListExportCmdDown.Size = new Size(241, 32);
            cmnuListExportCmdDown.Text = "Down";
            cmnuListExportCmdDown.Click += cmnuListExportCmdDown_Click;
            // 
            // pageDatabase
            // 
            pageDatabase.Controls.Add(gbTgtDatabase);
            pageDatabase.Controls.Add(gbSrcDatabase);
            pageDatabase.ImageIndex = 0;
            pageDatabase.Location = new Point(4, 34);
            pageDatabase.Margin = new Padding(6, 5, 6, 5);
            pageDatabase.Name = "pageDatabase";
            pageDatabase.Padding = new Padding(6, 5, 6, 5);
            pageDatabase.Size = new Size(1696, 642);
            pageDatabase.TabIndex = 0;
            pageDatabase.Text = "Database";
            pageDatabase.UseVisualStyleBackColor = true;
            // 
            // gbSrcDatabase
            // 
            gbSrcDatabase.Controls.Add(txtSrcTimeout);
            gbSrcDatabase.Controls.Add(lblSrcTimeout);
            gbSrcDatabase.Controls.Add(txtSrcOptionalOptions);
            gbSrcDatabase.Controls.Add(lblSrcOptionalOptions);
            gbSrcDatabase.Controls.Add(txtSrcPort);
            gbSrcDatabase.Controls.Add(lblSrcPort);
            gbSrcDatabase.Controls.Add(txtSrcConnectionString);
            gbSrcDatabase.Controls.Add(lblSrcConnectionString);
            gbSrcDatabase.Controls.Add(txtSrcPassword);
            gbSrcDatabase.Controls.Add(lblSrcPassword);
            gbSrcDatabase.Controls.Add(txtSrcUser);
            gbSrcDatabase.Controls.Add(lblSrcUser);
            gbSrcDatabase.Controls.Add(txtSrcDatabase);
            gbSrcDatabase.Controls.Add(lblSrcDatabase);
            gbSrcDatabase.Controls.Add(txtSrcServer);
            gbSrcDatabase.Controls.Add(lblSrcServer);
            gbSrcDatabase.Controls.Add(btnSrcConnectionTest);
            gbSrcDatabase.Controls.Add(cbSrcDataSourceType);
            gbSrcDatabase.Location = new Point(10, 12);
            gbSrcDatabase.Margin = new Padding(6, 5, 6, 5);
            gbSrcDatabase.Name = "gbSrcDatabase";
            gbSrcDatabase.Padding = new Padding(17, 5, 17, 20);
            gbSrcDatabase.Size = new Size(828, 532);
            gbSrcDatabase.TabIndex = 0;
            gbSrcDatabase.TabStop = false;
            gbSrcDatabase.Text = "Source Database";
            // 
            // btnSrcConnectionTest
            // 
            btnSrcConnectionTest.Location = new Point(426, 33);
            btnSrcConnectionTest.Margin = new Padding(6, 5, 6, 5);
            btnSrcConnectionTest.Name = "btnSrcConnectionTest";
            btnSrcConnectionTest.Size = new Size(243, 45);
            btnSrcConnectionTest.TabIndex = 3;
            btnSrcConnectionTest.Text = "Testing connection...";
            btnSrcConnectionTest.UseVisualStyleBackColor = true;
            btnSrcConnectionTest.Click += btnSrcConnectionTest_Click;
            // 
            // pageSettings
            // 
            pageSettings.Controls.Add(ckbWriteDriverLog);
            pageSettings.ImageIndex = 4;
            pageSettings.Location = new Point(4, 34);
            pageSettings.Margin = new Padding(6, 5, 6, 5);
            pageSettings.Name = "pageSettings";
            pageSettings.Size = new Size(1696, 642);
            pageSettings.TabIndex = 3;
            pageSettings.Text = "Settings";
            pageSettings.UseVisualStyleBackColor = true;
            // 
            // ckbWriteDriverLog
            // 
            ckbWriteDriverLog.AutoSize = true;
            ckbWriteDriverLog.Location = new Point(34, 20);
            ckbWriteDriverLog.Margin = new Padding(4, 5, 4, 5);
            ckbWriteDriverLog.Name = "ckbWriteDriverLog";
            ckbWriteDriverLog.Size = new Size(486, 29);
            ckbWriteDriverLog.TabIndex = 13;
            ckbWriteDriverLog.Text = "Log the result of a query from the database (debugging)";
            ckbWriteDriverLog.UseVisualStyleBackColor = true;
            ckbWriteDriverLog.CheckedChanged += ckbWriteDriverLog_CheckedChanged;
            // 
            // pageHelp
            // 
            pageHelp.Controls.Add(txtHelp);
            pageHelp.ImageIndex = 5;
            pageHelp.Location = new Point(4, 34);
            pageHelp.Margin = new Padding(6, 5, 6, 5);
            pageHelp.Name = "pageHelp";
            pageHelp.Size = new Size(1696, 642);
            pageHelp.TabIndex = 5;
            pageHelp.Text = "Help";
            pageHelp.UseVisualStyleBackColor = true;
            // 
            // txtHelp
            // 
            txtHelp.AutoCompleteBracketsList = new char[]
    {
    '(',
    ')',
    '{',
    '}',
    '[',
    ']',
    '"',
    '"',
    '\'',
    '\''
    };
            txtHelp.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            txtHelp.AutoScrollMinSize = new Size(35, 22);
            txtHelp.BackBrush = null;
            txtHelp.CharHeight = 22;
            txtHelp.CharWidth = 12;
            txtHelp.DefaultMarkerSize = 8;
            txtHelp.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            txtHelp.Dock = DockStyle.Fill;
            txtHelp.Hotkeys = resources.GetString("txtHelp.Hotkeys");
            txtHelp.IsReplaceMode = false;
            txtHelp.Location = new Point(0, 0);
            txtHelp.Name = "txtHelp";
            txtHelp.Paddings = new Padding(0);
            txtHelp.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            txtHelp.ServiceColors = null;
            txtHelp.Size = new Size(1696, 642);
            txtHelp.TabIndex = 0;
            txtHelp.Zoom = 100;
            // 
            // imgList
            // 
            imgList.ColorDepth = ColorDepth.Depth32Bit;
            imgList.ImageStream = (ImageListStreamer)resources.GetObject("imgList.ImageStream");
            imgList.TransparentColor = Color.Transparent;
            imgList.Images.SetKeyName(0, "database_connect.png");
            imgList.Images.SetKeyName(1, "sql.png");
            imgList.Images.SetKeyName(2, "database_import.png");
            imgList.Images.SetKeyName(3, "database_export.png");
            imgList.Images.SetKeyName(4, "database_table.png");
            imgList.Images.SetKeyName(5, "setting_tools.png");
            imgList.Images.SetKeyName(6, "help.png");
            imgList.Images.SetKeyName(7, "application_view_list_add.png");
            imgList.Images.SetKeyName(8, "application_view_list.png");
            imgList.Images.SetKeyName(9, "application_view_list_delete.png");
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 680);
            pnlBottom.Margin = new Padding(6, 5, 6, 5);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1704, 78);
            pnlBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(1559, 12);
            btnClose.Margin = new Padding(6, 5, 6, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(126, 45);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(1424, 12);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(126, 45);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtSrcTimeout
            // 
            txtSrcTimeout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtSrcTimeout.Location = new Point(685, 130);
            txtSrcTimeout.Margin = new Padding(6, 5, 6, 5);
            txtSrcTimeout.Name = "txtSrcTimeout";
            txtSrcTimeout.Size = new Size(121, 31);
            txtSrcTimeout.TabIndex = 33;
            // 
            // lblSrcTimeout
            // 
            lblSrcTimeout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSrcTimeout.AutoSize = true;
            lblSrcTimeout.Location = new Point(685, 98);
            lblSrcTimeout.Margin = new Padding(6, 0, 6, 0);
            lblSrcTimeout.Name = "lblSrcTimeout";
            lblSrcTimeout.Size = new Size(121, 25);
            lblSrcTimeout.TabIndex = 32;
            lblSrcTimeout.Text = "Timeout (sec.)";
            // 
            // txtSrcOptionalOptions
            // 
            txtSrcOptionalOptions.Location = new Point(27, 355);
            txtSrcOptionalOptions.Margin = new Padding(6, 5, 6, 5);
            txtSrcOptionalOptions.Name = "txtSrcOptionalOptions";
            txtSrcOptionalOptions.Size = new Size(644, 31);
            txtSrcOptionalOptions.TabIndex = 31;
            // 
            // lblSrcOptionalOptions
            // 
            lblSrcOptionalOptions.AutoSize = true;
            lblSrcOptionalOptions.Location = new Point(27, 323);
            lblSrcOptionalOptions.Margin = new Padding(6, 0, 6, 0);
            lblSrcOptionalOptions.Name = "lblSrcOptionalOptions";
            lblSrcOptionalOptions.Size = new Size(150, 25);
            lblSrcOptionalOptions.TabIndex = 30;
            lblSrcOptionalOptions.Text = "Optional Options";
            // 
            // txtSrcPort
            // 
            txtSrcPort.Location = new Point(356, 130);
            txtSrcPort.Margin = new Padding(6, 5, 6, 5);
            txtSrcPort.Name = "txtSrcPort";
            txtSrcPort.Size = new Size(315, 31);
            txtSrcPort.TabIndex = 29;
            // 
            // lblSrcPort
            // 
            lblSrcPort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSrcPort.AutoSize = true;
            lblSrcPort.Location = new Point(356, 98);
            lblSrcPort.Margin = new Padding(6, 0, 6, 0);
            lblSrcPort.Name = "lblSrcPort";
            lblSrcPort.Size = new Size(44, 25);
            lblSrcPort.TabIndex = 28;
            lblSrcPort.Text = "Port";
            // 
            // txtSrcConnectionString
            // 
            txtSrcConnectionString.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtSrcConnectionString.Location = new Point(27, 430);
            txtSrcConnectionString.Margin = new Padding(6, 5, 6, 5);
            txtSrcConnectionString.Multiline = true;
            txtSrcConnectionString.Name = "txtSrcConnectionString";
            txtSrcConnectionString.Size = new Size(779, 77);
            txtSrcConnectionString.TabIndex = 27;
            // 
            // lblSrcConnectionString
            // 
            lblSrcConnectionString.AutoSize = true;
            lblSrcConnectionString.Location = new Point(27, 398);
            lblSrcConnectionString.Margin = new Padding(6, 0, 6, 0);
            lblSrcConnectionString.Name = "lblSrcConnectionString";
            lblSrcConnectionString.Size = new Size(152, 25);
            lblSrcConnectionString.TabIndex = 26;
            lblSrcConnectionString.Text = "Connection string";
            // 
            // txtSrcPassword
            // 
            txtSrcPassword.Location = new Point(356, 280);
            txtSrcPassword.Margin = new Padding(6, 5, 6, 5);
            txtSrcPassword.Name = "txtSrcPassword";
            txtSrcPassword.Size = new Size(315, 31);
            txtSrcPassword.TabIndex = 25;
            txtSrcPassword.UseSystemPasswordChar = true;
            // 
            // lblSrcPassword
            // 
            lblSrcPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSrcPassword.AutoSize = true;
            lblSrcPassword.Location = new Point(356, 248);
            lblSrcPassword.Margin = new Padding(6, 0, 6, 0);
            lblSrcPassword.Name = "lblSrcPassword";
            lblSrcPassword.Size = new Size(87, 25);
            lblSrcPassword.TabIndex = 24;
            lblSrcPassword.Text = "Password";
            // 
            // txtSrcUser
            // 
            txtSrcUser.Location = new Point(27, 280);
            txtSrcUser.Margin = new Padding(6, 5, 6, 5);
            txtSrcUser.Name = "txtSrcUser";
            txtSrcUser.Size = new Size(315, 31);
            txtSrcUser.TabIndex = 23;
            // 
            // lblSrcUser
            // 
            lblSrcUser.AutoSize = true;
            lblSrcUser.Location = new Point(27, 248);
            lblSrcUser.Margin = new Padding(6, 0, 6, 0);
            lblSrcUser.Name = "lblSrcUser";
            lblSrcUser.Size = new Size(47, 25);
            lblSrcUser.TabIndex = 22;
            lblSrcUser.Text = "User";
            // 
            // txtSrcDatabase
            // 
            txtSrcDatabase.Location = new Point(27, 205);
            txtSrcDatabase.Margin = new Padding(6, 5, 6, 5);
            txtSrcDatabase.Name = "txtSrcDatabase";
            txtSrcDatabase.Size = new Size(644, 31);
            txtSrcDatabase.TabIndex = 21;
            // 
            // lblSrcDatabase
            // 
            lblSrcDatabase.AutoSize = true;
            lblSrcDatabase.Location = new Point(27, 173);
            lblSrcDatabase.Margin = new Padding(6, 0, 6, 0);
            lblSrcDatabase.Name = "lblSrcDatabase";
            lblSrcDatabase.Size = new Size(86, 25);
            lblSrcDatabase.TabIndex = 20;
            lblSrcDatabase.Text = "Database";
            // 
            // txtSrcServer
            // 
            txtSrcServer.Location = new Point(27, 130);
            txtSrcServer.Margin = new Padding(6, 5, 6, 5);
            txtSrcServer.Name = "txtSrcServer";
            txtSrcServer.Size = new Size(315, 31);
            txtSrcServer.TabIndex = 19;
            // 
            // lblSrcServer
            // 
            lblSrcServer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblSrcServer.AutoSize = true;
            lblSrcServer.Location = new Point(27, 98);
            lblSrcServer.Margin = new Padding(6, 0, 6, 0);
            lblSrcServer.Name = "lblSrcServer";
            lblSrcServer.Size = new Size(61, 25);
            lblSrcServer.TabIndex = 18;
            lblSrcServer.Text = "Server";
            // 
            // gbTgtDatabase
            // 
            gbTgtDatabase.Controls.Add(txtTgtTimeout);
            gbTgtDatabase.Controls.Add(lblTgtTimeout);
            gbTgtDatabase.Controls.Add(txtTgtOptionalOptions);
            gbTgtDatabase.Controls.Add(lblTgtOptionalOptions);
            gbTgtDatabase.Controls.Add(txtTgtPort);
            gbTgtDatabase.Controls.Add(lblTgtPort);
            gbTgtDatabase.Controls.Add(txtTgtConnectionString);
            gbTgtDatabase.Controls.Add(lblTgtConnectionString);
            gbTgtDatabase.Controls.Add(txtTgtPassword);
            gbTgtDatabase.Controls.Add(lblTgtPassword);
            gbTgtDatabase.Controls.Add(txtTgtUser);
            gbTgtDatabase.Controls.Add(lblTgtUser);
            gbTgtDatabase.Controls.Add(txtTgtDatabase);
            gbTgtDatabase.Controls.Add(lblTgtDatabase);
            gbTgtDatabase.Controls.Add(txtTgtServer);
            gbTgtDatabase.Controls.Add(lblTgtServer);
            gbTgtDatabase.Controls.Add(btnTgtConnectionTest);
            gbTgtDatabase.Controls.Add(cbTgtDataSourceType);
            gbTgtDatabase.Location = new Point(850, 12);
            gbTgtDatabase.Margin = new Padding(6, 5, 6, 5);
            gbTgtDatabase.Name = "gbTgtDatabase";
            gbTgtDatabase.Padding = new Padding(17, 5, 17, 20);
            gbTgtDatabase.Size = new Size(828, 532);
            gbTgtDatabase.TabIndex = 2;
            gbTgtDatabase.TabStop = false;
            gbTgtDatabase.Text = "Target Database";
            // 
            // txtTgtTimeout
            // 
            txtTgtTimeout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTgtTimeout.Location = new Point(685, 130);
            txtTgtTimeout.Margin = new Padding(6, 5, 6, 5);
            txtTgtTimeout.Name = "txtTgtTimeout";
            txtTgtTimeout.Size = new Size(121, 31);
            txtTgtTimeout.TabIndex = 33;
            // 
            // lblTgtTimeout
            // 
            lblTgtTimeout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTgtTimeout.AutoSize = true;
            lblTgtTimeout.Location = new Point(685, 102);
            lblTgtTimeout.Margin = new Padding(6, 0, 6, 0);
            lblTgtTimeout.Name = "lblTgtTimeout";
            lblTgtTimeout.Size = new Size(121, 25);
            lblTgtTimeout.TabIndex = 32;
            lblTgtTimeout.Text = "Timeout (sec.)";
            // 
            // txtTgtOptionalOptions
            // 
            txtTgtOptionalOptions.Location = new Point(27, 355);
            txtTgtOptionalOptions.Margin = new Padding(6, 5, 6, 5);
            txtTgtOptionalOptions.Name = "txtTgtOptionalOptions";
            txtTgtOptionalOptions.Size = new Size(644, 31);
            txtTgtOptionalOptions.TabIndex = 31;
            // 
            // lblTgtOptionalOptions
            // 
            lblTgtOptionalOptions.AutoSize = true;
            lblTgtOptionalOptions.Location = new Point(27, 327);
            lblTgtOptionalOptions.Margin = new Padding(6, 0, 6, 0);
            lblTgtOptionalOptions.Name = "lblTgtOptionalOptions";
            lblTgtOptionalOptions.Size = new Size(150, 25);
            lblTgtOptionalOptions.TabIndex = 30;
            lblTgtOptionalOptions.Text = "Optional Options";
            // 
            // txtTgtPort
            // 
            txtTgtPort.Location = new Point(356, 130);
            txtTgtPort.Margin = new Padding(6, 5, 6, 5);
            txtTgtPort.Name = "txtTgtPort";
            txtTgtPort.Size = new Size(315, 31);
            txtTgtPort.TabIndex = 29;
            // 
            // lblTgtPort
            // 
            lblTgtPort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTgtPort.AutoSize = true;
            lblTgtPort.Location = new Point(356, 102);
            lblTgtPort.Margin = new Padding(6, 0, 6, 0);
            lblTgtPort.Name = "lblTgtPort";
            lblTgtPort.Size = new Size(44, 25);
            lblTgtPort.TabIndex = 28;
            lblTgtPort.Text = "Port";
            // 
            // txtTgtConnectionString
            // 
            txtTgtConnectionString.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            txtTgtConnectionString.Location = new Point(27, 430);
            txtTgtConnectionString.Margin = new Padding(6, 5, 6, 5);
            txtTgtConnectionString.Multiline = true;
            txtTgtConnectionString.Name = "txtTgtConnectionString";
            txtTgtConnectionString.Size = new Size(779, 77);
            txtTgtConnectionString.TabIndex = 27;
            // 
            // lblTgtConnectionString
            // 
            lblTgtConnectionString.AutoSize = true;
            lblTgtConnectionString.Location = new Point(27, 398);
            lblTgtConnectionString.Margin = new Padding(6, 0, 6, 0);
            lblTgtConnectionString.Name = "lblTgtConnectionString";
            lblTgtConnectionString.Size = new Size(152, 25);
            lblTgtConnectionString.TabIndex = 26;
            lblTgtConnectionString.Text = "Connection string";
            // 
            // txtTgtPassword
            // 
            txtTgtPassword.Location = new Point(356, 280);
            txtTgtPassword.Margin = new Padding(6, 5, 6, 5);
            txtTgtPassword.Name = "txtTgtPassword";
            txtTgtPassword.Size = new Size(315, 31);
            txtTgtPassword.TabIndex = 25;
            txtTgtPassword.UseSystemPasswordChar = true;
            // 
            // lblTgtPassword
            // 
            lblTgtPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTgtPassword.AutoSize = true;
            lblTgtPassword.Location = new Point(356, 252);
            lblTgtPassword.Margin = new Padding(6, 0, 6, 0);
            lblTgtPassword.Name = "lblTgtPassword";
            lblTgtPassword.Size = new Size(87, 25);
            lblTgtPassword.TabIndex = 24;
            lblTgtPassword.Text = "Password";
            // 
            // txtTgtUser
            // 
            txtTgtUser.Location = new Point(27, 280);
            txtTgtUser.Margin = new Padding(6, 5, 6, 5);
            txtTgtUser.Name = "txtTgtUser";
            txtTgtUser.Size = new Size(315, 31);
            txtTgtUser.TabIndex = 23;
            // 
            // lblTgtUser
            // 
            lblTgtUser.AutoSize = true;
            lblTgtUser.Location = new Point(27, 252);
            lblTgtUser.Margin = new Padding(6, 0, 6, 0);
            lblTgtUser.Name = "lblTgtUser";
            lblTgtUser.Size = new Size(47, 25);
            lblTgtUser.TabIndex = 22;
            lblTgtUser.Text = "User";
            // 
            // txtTgtDatabase
            // 
            txtTgtDatabase.Location = new Point(27, 205);
            txtTgtDatabase.Margin = new Padding(6, 5, 6, 5);
            txtTgtDatabase.Name = "txtTgtDatabase";
            txtTgtDatabase.Size = new Size(644, 31);
            txtTgtDatabase.TabIndex = 21;
            // 
            // lblTgtDatabase
            // 
            lblTgtDatabase.AutoSize = true;
            lblTgtDatabase.Location = new Point(27, 177);
            lblTgtDatabase.Margin = new Padding(6, 0, 6, 0);
            lblTgtDatabase.Name = "lblTgtDatabase";
            lblTgtDatabase.Size = new Size(86, 25);
            lblTgtDatabase.TabIndex = 20;
            lblTgtDatabase.Text = "Database";
            // 
            // txtTgtServer
            // 
            txtTgtServer.Location = new Point(27, 130);
            txtTgtServer.Margin = new Padding(6, 5, 6, 5);
            txtTgtServer.Name = "txtTgtServer";
            txtTgtServer.Size = new Size(315, 31);
            txtTgtServer.TabIndex = 19;
            // 
            // lblTgtServer
            // 
            lblTgtServer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTgtServer.AutoSize = true;
            lblTgtServer.Location = new Point(27, 102);
            lblTgtServer.Margin = new Padding(6, 0, 6, 0);
            lblTgtServer.Name = "lblTgtServer";
            lblTgtServer.Size = new Size(61, 25);
            lblTgtServer.TabIndex = 18;
            lblTgtServer.Text = "Server";
            // 
            // btnTgtConnectionTest
            // 
            btnTgtConnectionTest.Location = new Point(426, 33);
            btnTgtConnectionTest.Margin = new Padding(6, 5, 6, 5);
            btnTgtConnectionTest.Name = "btnTgtConnectionTest";
            btnTgtConnectionTest.Size = new Size(243, 45);
            btnTgtConnectionTest.TabIndex = 3;
            btnTgtConnectionTest.Text = "Testing connection...";
            btnTgtConnectionTest.UseVisualStyleBackColor = true;
            btnTgtConnectionTest.Click += btnTgtConnectionTest_Click;
            // 
            // cbTgtDataSourceType
            // 
            cbTgtDataSourceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTgtDataSourceType.FormattingEnabled = true;
            cbTgtDataSourceType.Items.AddRange(new object[] { "<Choose database type>", "Microsoft SQL Server", "Oracle", "PostgreSQL", "MySQL", "Firebird", "InfluxDBv2", "InfluxDBv3" });
            cbTgtDataSourceType.Location = new Point(21, 38);
            cbTgtDataSourceType.Margin = new Padding(6, 5, 6, 5);
            cbTgtDataSourceType.Name = "cbTgtDataSourceType";
            cbTgtDataSourceType.Size = new Size(387, 33);
            cbTgtDataSourceType.TabIndex = 0;
            cbTgtDataSourceType.SelectedIndexChanged += cbTgtDataSourceType_SelectedIndexChanged;
            // 
            // FrmProject
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(1704, 758);
            Controls.Add(tabControl);
            Controls.Add(pnlBottom);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(6, 5, 6, 5);
            MinimizeBox = false;
            MinimumSize = new Size(731, 814);
            Name = "FrmProject";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DB Import Plus - Device {0} Properties Version {1}";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmProject_FormClosing;
            Load += FrmProject_Load;
            tabControl.ResumeLayout(false);
            pageCommandsImport.ResumeLayout(false);
            cmnuImportCommands.ResumeLayout(false);
            pageCommandsExport.ResumeLayout(false);
            cmnuExportCommands.ResumeLayout(false);
            pageDatabase.ResumeLayout(false);
            gbSrcDatabase.ResumeLayout(false);
            gbSrcDatabase.PerformLayout();
            pageSettings.ResumeLayout(false);
            pageSettings.PerformLayout();
            pageHelp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtHelp).EndInit();
            pnlBottom.ResumeLayout(false);
            gbTgtDatabase.ResumeLayout(false);
            gbTgtDatabase.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private ComboBox cbSrcDataSourceType;
        private TabControl tabControl;
        private TabPage pageDatabase;
        private TabPage pageCommandsImport;
        private Panel pnlBottom;
        private Button btnClose;
        private Button btnSave;
        private GroupBox gbSrcDatabase;
        private TabPage pageCommandsExport;
        private Button btnSrcConnectionTest;
        private TabPage pageSettings;
        private TabPage pageHelp;
        private CheckBox ckbWriteDriverLog;
        private ImageList imgList;
        private ListView lstImportCommands;
        private ColumnHeader clmImportCmdName;
        private ColumnHeader clmImportCmdDescription;
        private ColumnHeader clmImportCmdEnabled;
        private ColumnHeader clmImportCmdNum;
        private ColumnHeader clmImportCmdCode;
        private ListView lstExportCommands;
        private ColumnHeader clmExportCmdNum;
        private ColumnHeader clmExportCmdCode;
        private ColumnHeader clmExportCmdName;
        private ColumnHeader clmExportCmdDescription;
        private ColumnHeader clmExportCmdEnabled;
        private ContextMenuStrip cmnuImportCommands;
        private ToolStripMenuItem cmnuListImportCmdAdd;
        private ToolStripMenuItem cmnuListImportCmdChange;
        private ToolStripMenuItem cmnuListImportCmdDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cmnuListImportCmdUp;
        private ToolStripMenuItem cmnuListImportCmdDown;
        private ContextMenuStrip cmnuExportCommands;
        private ToolStripMenuItem cmnuListExportCmdAdd;
        private ToolStripMenuItem cmnuListExportCmdChange;
        private ToolStripMenuItem cmnuListExportCmdDelete;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem cmnuListExportCmdUp;
        private ToolStripMenuItem cmnuListExportCmdDown;
        private FastColoredTextBoxNS.FastColoredTextBox txtHelp;
        private TextBox txtSrcTimeout;
        private Label lblSrcTimeout;
        private TextBox txtSrcOptionalOptions;
        private Label lblSrcOptionalOptions;
        private TextBox txtSrcPort;
        private Label lblSrcPort;
        private TextBox txtSrcConnectionString;
        private Label lblSrcConnectionString;
        private TextBox txtSrcPassword;
        private Label lblSrcPassword;
        private TextBox txtSrcUser;
        private Label lblSrcUser;
        private TextBox txtSrcDatabase;
        private Label lblSrcDatabase;
        private TextBox txtSrcServer;
        private Label lblSrcServer;
        private GroupBox gbTgtDatabase;
        private TextBox txtTgtTimeout;
        private Label lblTgtTimeout;
        private TextBox txtTgtOptionalOptions;
        private Label lblTgtOptionalOptions;
        private TextBox txtTgtPort;
        private Label lblTgtPort;
        private TextBox txtTgtConnectionString;
        private Label lblTgtConnectionString;
        private TextBox txtTgtPassword;
        private Label lblTgtPassword;
        private TextBox txtTgtUser;
        private Label lblTgtUser;
        private TextBox txtTgtDatabase;
        private Label lblTgtDatabase;
        private TextBox txtTgtServer;
        private Label lblTgtServer;
        private Button btnTgtConnectionTest;
        private ComboBox cbTgtDataSourceType;
    }
}