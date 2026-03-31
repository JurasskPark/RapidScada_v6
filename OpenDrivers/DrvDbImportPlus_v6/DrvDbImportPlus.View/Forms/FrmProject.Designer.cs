namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
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
            cbDataSourceType = new ComboBox();
            gbConnection = new GroupBox();
            txtTimeout = new TextBox();
            lblTimeout = new Label();
            txtOptionalOptions = new TextBox();
            lblOptionalOptions = new Label();
            txtPort = new TextBox();
            lblPort = new Label();
            txtConnectionString = new TextBox();
            lblConnectionString = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUser = new TextBox();
            lblUser = new Label();
            txtDatabase = new TextBox();
            lblDatabase = new Label();
            txtServer = new TextBox();
            lblServer = new Label();
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
            gbDataSourceType = new GroupBox();
            btnConnectionTest = new Button();
            pageSettings = new TabPage();
            ckbWriteDriverLog = new CheckBox();
            pageHelp = new TabPage();
            imgList = new ImageList(components);
            pnlBottom = new Panel();
            btnClose = new Button();
            btnSave = new Button();
            gbConnection.SuspendLayout();
            tabControl.SuspendLayout();
            pageCommandsImport.SuspendLayout();
            cmnuImportCommands.SuspendLayout();
            pageCommandsExport.SuspendLayout();
            cmnuExportCommands.SuspendLayout();
            pageDatabase.SuspendLayout();
            gbDataSourceType.SuspendLayout();
            pageSettings.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // cbDataSourceType
            // 
            cbDataSourceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDataSourceType.FormattingEnabled = true;
            cbDataSourceType.Items.AddRange(new object[] { "<Choose database type>", "Microsoft SQL Server", "Oracle", "PostgreSQL", "MySQL", "Firebird", "InfluxDBv2", "InfluxDBv3" });
            cbDataSourceType.Location = new Point(21, 38);
            cbDataSourceType.Margin = new Padding(6, 5, 6, 5);
            cbDataSourceType.Name = "cbDataSourceType";
            cbDataSourceType.Size = new Size(387, 33);
            cbDataSourceType.TabIndex = 0;
            cbDataSourceType.SelectedIndexChanged += cbDataSourceType_SelectedIndexChanged;
            // 
            // gbConnection
            // 
            gbConnection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbConnection.Controls.Add(txtTimeout);
            gbConnection.Controls.Add(lblTimeout);
            gbConnection.Controls.Add(txtOptionalOptions);
            gbConnection.Controls.Add(lblOptionalOptions);
            gbConnection.Controls.Add(txtPort);
            gbConnection.Controls.Add(lblPort);
            gbConnection.Controls.Add(txtConnectionString);
            gbConnection.Controls.Add(lblConnectionString);
            gbConnection.Controls.Add(txtPassword);
            gbConnection.Controls.Add(lblPassword);
            gbConnection.Controls.Add(txtUser);
            gbConnection.Controls.Add(lblUser);
            gbConnection.Controls.Add(txtDatabase);
            gbConnection.Controls.Add(lblDatabase);
            gbConnection.Controls.Add(txtServer);
            gbConnection.Controls.Add(lblServer);
            gbConnection.Location = new Point(10, 125);
            gbConnection.Margin = new Padding(6, 5, 6, 5);
            gbConnection.Name = "gbConnection";
            gbConnection.Padding = new Padding(17, 5, 17, 20);
            gbConnection.Size = new Size(906, 679);
            gbConnection.TabIndex = 1;
            gbConnection.TabStop = false;
            gbConnection.Text = "Connection";
            // 
            // txtTimeout
            // 
            txtTimeout.Location = new Point(679, 62);
            txtTimeout.Margin = new Padding(6, 5, 6, 5);
            txtTimeout.Name = "txtTimeout";
            txtTimeout.Size = new Size(203, 31);
            txtTimeout.TabIndex = 17;
            // 
            // lblTimeout
            // 
            lblTimeout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblTimeout.AutoSize = true;
            lblTimeout.Location = new Point(679, 30);
            lblTimeout.Margin = new Padding(6, 0, 6, 0);
            lblTimeout.Name = "lblTimeout";
            lblTimeout.Size = new Size(121, 25);
            lblTimeout.TabIndex = 16;
            lblTimeout.Text = "Timeout (sec.)";
            // 
            // txtOptionalOptions
            // 
            txtOptionalOptions.Location = new Point(21, 287);
            txtOptionalOptions.Margin = new Padding(6, 5, 6, 5);
            txtOptionalOptions.Name = "txtOptionalOptions";
            txtOptionalOptions.Size = new Size(644, 31);
            txtOptionalOptions.TabIndex = 13;
            txtOptionalOptions.TextChanged += txtConnProp_TextChanged;
            // 
            // lblOptionalOptions
            // 
            lblOptionalOptions.AutoSize = true;
            lblOptionalOptions.Location = new Point(17, 255);
            lblOptionalOptions.Margin = new Padding(6, 0, 6, 0);
            lblOptionalOptions.Name = "lblOptionalOptions";
            lblOptionalOptions.Size = new Size(150, 25);
            lblOptionalOptions.TabIndex = 12;
            lblOptionalOptions.Text = "Optional Options";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(350, 62);
            txtPort.Margin = new Padding(6, 5, 6, 5);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(315, 31);
            txtPort.TabIndex = 11;
            txtPort.TextChanged += txtConnProp_TextChanged;
            // 
            // lblPort
            // 
            lblPort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPort.AutoSize = true;
            lblPort.Location = new Point(344, 30);
            lblPort.Margin = new Padding(6, 0, 6, 0);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(44, 25);
            lblPort.TabIndex = 10;
            lblPort.Text = "Port";
            // 
            // txtConnectionString
            // 
            txtConnectionString.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtConnectionString.Location = new Point(21, 362);
            txtConnectionString.Margin = new Padding(6, 5, 6, 5);
            txtConnectionString.Multiline = true;
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(860, 288);
            txtConnectionString.TabIndex = 9;
            txtConnectionString.TextChanged += txtConnectionString_TextChanged;
            // 
            // lblConnectionString
            // 
            lblConnectionString.AutoSize = true;
            lblConnectionString.Location = new Point(17, 330);
            lblConnectionString.Margin = new Padding(6, 0, 6, 0);
            lblConnectionString.Name = "lblConnectionString";
            lblConnectionString.Size = new Size(152, 25);
            lblConnectionString.TabIndex = 8;
            lblConnectionString.Text = "Connection string";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(350, 212);
            txtPassword.Margin = new Padding(6, 5, 6, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(315, 31);
            txtPassword.TabIndex = 7;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += txtConnProp_TextChanged;
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(344, 180);
            lblPassword.Margin = new Padding(6, 0, 6, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(87, 25);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(21, 212);
            txtUser.Margin = new Padding(6, 5, 6, 5);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(315, 31);
            txtUser.TabIndex = 5;
            txtUser.TextChanged += txtConnProp_TextChanged;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(17, 180);
            lblUser.Margin = new Padding(6, 0, 6, 0);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(47, 25);
            lblUser.TabIndex = 4;
            lblUser.Text = "User";
            // 
            // txtDatabase
            // 
            txtDatabase.Location = new Point(21, 137);
            txtDatabase.Margin = new Padding(6, 5, 6, 5);
            txtDatabase.Name = "txtDatabase";
            txtDatabase.Size = new Size(644, 31);
            txtDatabase.TabIndex = 3;
            txtDatabase.TextChanged += txtConnProp_TextChanged;
            // 
            // lblDatabase
            // 
            lblDatabase.AutoSize = true;
            lblDatabase.Location = new Point(17, 105);
            lblDatabase.Margin = new Padding(6, 0, 6, 0);
            lblDatabase.Name = "lblDatabase";
            lblDatabase.Size = new Size(86, 25);
            lblDatabase.TabIndex = 2;
            lblDatabase.Text = "Database";
            // 
            // txtServer
            // 
            txtServer.Location = new Point(21, 62);
            txtServer.Margin = new Padding(6, 5, 6, 5);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(315, 31);
            txtServer.TabIndex = 1;
            txtServer.TextChanged += txtConnProp_TextChanged;
            // 
            // lblServer
            // 
            lblServer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblServer.AutoSize = true;
            lblServer.Location = new Point(17, 30);
            lblServer.Margin = new Padding(6, 0, 6, 0);
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(61, 25);
            lblServer.TabIndex = 0;
            lblServer.Text = "Server";
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
            tabControl.Size = new Size(1297, 862);
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
            pageCommandsImport.Size = new Size(1289, 824);
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
            lstImportCommands.Size = new Size(1277, 814);
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
            pageCommandsExport.Size = new Size(1289, 824);
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
            lstExportCommands.Size = new Size(1277, 814);
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
            cmnuExportCommands.Size = new Size(249, 203);
            // 
            // cmnuListExportCmdAdd
            // 
            cmnuListExportCmdAdd.Image = (Image)resources.GetObject("cmnuListExportCmdAdd.Image");
            cmnuListExportCmdAdd.Name = "cmnuListExportCmdAdd";
            cmnuListExportCmdAdd.Size = new Size(248, 32);
            cmnuListExportCmdAdd.Text = "Add Command";
            cmnuListExportCmdAdd.Click += cmnuListExportCmdAdd_Click;
            // 
            // cmnuListExportCmdChange
            // 
            cmnuListExportCmdChange.Image = (Image)resources.GetObject("cmnuListExportCmdChange.Image");
            cmnuListExportCmdChange.Name = "cmnuListExportCmdChange";
            cmnuListExportCmdChange.Size = new Size(248, 32);
            cmnuListExportCmdChange.Text = "Change Command";
            cmnuListExportCmdChange.Click += cmnuListExportCmdChange_Click;
            // 
            // cmnuListExportCmdDelete
            // 
            cmnuListExportCmdDelete.Image = (Image)resources.GetObject("cmnuListExportCmdDelete.Image");
            cmnuListExportCmdDelete.Name = "cmnuListExportCmdDelete";
            cmnuListExportCmdDelete.Size = new Size(248, 32);
            cmnuListExportCmdDelete.Text = "Delete Command";
            cmnuListExportCmdDelete.Click += cmnuListExportCmdDelete_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(245, 6);
            // 
            // cmnuListExportCmdUp
            // 
            cmnuListExportCmdUp.Image = (Image)resources.GetObject("cmnuListExportCmdUp.Image");
            cmnuListExportCmdUp.Name = "cmnuListExportCmdUp";
            cmnuListExportCmdUp.Size = new Size(248, 32);
            cmnuListExportCmdUp.Text = "Up";
            cmnuListExportCmdUp.Click += cmnuListExportCmdDown_Click;
            // 
            // cmnuListExportCmdDown
            // 
            cmnuListExportCmdDown.Image = (Image)resources.GetObject("cmnuListExportCmdDown.Image");
            cmnuListExportCmdDown.Name = "cmnuListExportCmdDown";
            cmnuListExportCmdDown.Size = new Size(248, 32);
            cmnuListExportCmdDown.Text = "Down";
            cmnuListExportCmdDown.Click += cmnuListExportCmdDown_Click;
            // 
            // pageDatabase
            // 
            pageDatabase.Controls.Add(gbDataSourceType);
            pageDatabase.Controls.Add(gbConnection);
            pageDatabase.ImageIndex = 0;
            pageDatabase.Location = new Point(4, 34);
            pageDatabase.Margin = new Padding(6, 5, 6, 5);
            pageDatabase.Name = "pageDatabase";
            pageDatabase.Padding = new Padding(6, 5, 6, 5);
            pageDatabase.Size = new Size(1289, 824);
            pageDatabase.TabIndex = 0;
            pageDatabase.Text = "Database";
            pageDatabase.UseVisualStyleBackColor = true;
            // 
            // gbDataSourceType
            // 
            gbDataSourceType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataSourceType.Controls.Add(btnConnectionTest);
            gbDataSourceType.Controls.Add(cbDataSourceType);
            gbDataSourceType.Location = new Point(10, 12);
            gbDataSourceType.Margin = new Padding(6, 5, 6, 5);
            gbDataSourceType.Name = "gbDataSourceType";
            gbDataSourceType.Padding = new Padding(17, 5, 17, 20);
            gbDataSourceType.Size = new Size(906, 102);
            gbDataSourceType.TabIndex = 0;
            gbDataSourceType.TabStop = false;
            gbDataSourceType.Text = "Data Source Type";
            // 
            // btnConnectionTest
            // 
            btnConnectionTest.Location = new Point(426, 33);
            btnConnectionTest.Margin = new Padding(6, 5, 6, 5);
            btnConnectionTest.Name = "btnConnectionTest";
            btnConnectionTest.Size = new Size(243, 45);
            btnConnectionTest.TabIndex = 3;
            btnConnectionTest.Text = "Testing connection...";
            btnConnectionTest.UseVisualStyleBackColor = true;
            btnConnectionTest.Click += btnConnectionTest_Click;
            // 
            // pageSettings
            // 
            pageSettings.Controls.Add(ckbWriteDriverLog);
            pageSettings.ImageIndex = 4;
            pageSettings.Location = new Point(4, 34);
            pageSettings.Margin = new Padding(6, 5, 6, 5);
            pageSettings.Name = "pageSettings";
            pageSettings.Size = new Size(1289, 824);
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
            pageHelp.ImageIndex = 5;
            pageHelp.Location = new Point(4, 34);
            pageHelp.Margin = new Padding(6, 5, 6, 5);
            pageHelp.Name = "pageHelp";
            pageHelp.Size = new Size(1289, 824);
            pageHelp.TabIndex = 5;
            pageHelp.Text = "Help";
            pageHelp.UseVisualStyleBackColor = true;
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
            pnlBottom.Location = new Point(0, 862);
            pnlBottom.Margin = new Padding(6, 5, 6, 5);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1297, 78);
            pnlBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(1152, 12);
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
            btnSave.Location = new Point(1017, 12);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(126, 45);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FrmProject
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(1297, 940);
            Controls.Add(tabControl);
            Controls.Add(pnlBottom);
            Margin = new Padding(6, 5, 6, 5);
            MinimizeBox = false;
            MinimumSize = new Size(731, 814);
            Name = "FrmProject";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "DB Import Plus - Device {0} Properties Version {1}";
            WindowState = FormWindowState.Maximized;
            FormClosing += FrmProject_FormClosing;
            Load += FrmProject_Load;
            gbConnection.ResumeLayout(false);
            gbConnection.PerformLayout();
            tabControl.ResumeLayout(false);
            pageCommandsImport.ResumeLayout(false);
            cmnuImportCommands.ResumeLayout(false);
            pageCommandsExport.ResumeLayout(false);
            cmnuExportCommands.ResumeLayout(false);
            pageDatabase.ResumeLayout(false);
            gbDataSourceType.ResumeLayout(false);
            pageSettings.ResumeLayout(false);
            pageSettings.PerformLayout();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private ComboBox cbDataSourceType;
        private GroupBox gbConnection;
        private TextBox txtDatabase;
        private Label lblDatabase;
        private TextBox txtServer;
        private Label lblServer;
        private TextBox txtUser;
        private Label lblUser;
        private TextBox txtPassword;
        private Label lblPassword;
        private TextBox txtConnectionString;
        private Label lblConnectionString;
        private TabControl tabControl;
        private TabPage pageDatabase;
        private TabPage pageCommandsImport;
        private Panel pnlBottom;
        private Button btnClose;
        private Button btnSave;
        private GroupBox gbDataSourceType;
        private TabPage pageCommandsExport;
        private TextBox txtOptionalOptions;
        private Label lblOptionalOptions;
        private TextBox txtPort;
        private Label lblPort;
        private Button btnConnectionTest;
        private TabPage pageSettings;
        private TabPage pageHelp;
        private FastColoredTextBoxNS.FastColoredTextBox txtHelp;
        private CheckBox ckbWriteDriverLog;
        private ImageList imgList;
        private TextBox txtTimeout;
        private Label lblTimeout;
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
    }
}