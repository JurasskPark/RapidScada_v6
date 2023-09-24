namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    partial class FrmConfig
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            txtSelectQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            cmnuSelectQuery = new ContextMenuStrip(components);
            cmnuSelectQueryCut = new ToolStripMenuItem();
            cmnuSelectQueryCopy = new ToolStripMenuItem();
            cmnuSelectQueryPaste = new ToolStripMenuItem();
            cmnuSelectQuerySelectAll = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            cmnuSelectQueryUndo = new ToolStripMenuItem();
            cmnuSelectQueryRedo = new ToolStripMenuItem();
            txtCmdQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            cmnuCmdQuery = new ContextMenuStrip(components);
            cmnuCmdQueryCut = new ToolStripMenuItem();
            cmnuCmdQueryCopy = new ToolStripMenuItem();
            cmnuCmdQueryPaste = new ToolStripMenuItem();
            cmnuCmdQuerySelectAll = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            cmnuCmdQueryUndo = new ToolStripMenuItem();
            cmnuCmdQueryRedo = new ToolStripMenuItem();
            txtHelp = new FastColoredTextBoxNS.FastColoredTextBox();
            cbDataSourceType = new ComboBox();
            gbConnection = new GroupBox();
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
            pageDatabase = new TabPage();
            gbDataSourceType = new GroupBox();
            btnConnectionTest = new Button();
            pageQuery = new TabPage();
            lblSelectQuery = new Label();
            pageData = new TabPage();
            tlpPanel = new TableLayoutPanel();
            dgvData = new DataGridView();
            btnExecuteSQLQuery = new Button();
            pageCommands = new TabPage();
            gbCommandParams = new GroupBox();
            lblCmdCode = new Label();
            txtCmdCode = new TextBox();
            lblCmdQuery = new Label();
            txtName = new TextBox();
            lblName = new Label();
            numCmdNum = new NumericUpDown();
            lblCmdNum = new Label();
            gbCommand = new GroupBox();
            cbCommand = new ComboBox();
            btnDeleteCommand = new Button();
            btnCreateCommand = new Button();
            pageSettings = new TabPage();
            ckbWriteDriverLog = new CheckBox();
            gpbTags = new GroupBox();
            tlpTags = new TableLayoutPanel();
            lstTags = new ListView();
            clmTagname = new ColumnHeader();
            clmTagCode = new ColumnHeader();
            clmTagFormat = new ColumnHeader();
            clmTagEnabled = new ColumnHeader();
            cmnuLstTags = new ContextMenuStrip(components);
            cmnuTagRefresh = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            cmnuTagAdd = new ToolStripMenuItem();
            cmnuListTagAdd = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            cmnuTagChange = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            cmnuTagDelete = new ToolStripMenuItem();
            cmnuTagAllDelete = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            cmnuUp = new ToolStripMenuItem();
            cmnuDown = new ToolStripMenuItem();
            gpbTagFormatDatabase = new GroupBox();
            rdbKPTagsBasedRequestedTableRows = new RadioButton();
            rdbKPTagsBasedRequestedTableColumns = new RadioButton();
            pageHelp = new TabPage();
            pnlBottom = new Panel();
            btnClose = new Button();
            btnSave = new Button();
            ((System.ComponentModel.ISupportInitialize)txtSelectQuery).BeginInit();
            cmnuSelectQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtCmdQuery).BeginInit();
            cmnuCmdQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)txtHelp).BeginInit();
            gbConnection.SuspendLayout();
            tabControl.SuspendLayout();
            pageDatabase.SuspendLayout();
            gbDataSourceType.SuspendLayout();
            pageQuery.SuspendLayout();
            pageData.SuspendLayout();
            tlpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            pageCommands.SuspendLayout();
            gbCommandParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCmdNum).BeginInit();
            gbCommand.SuspendLayout();
            pageSettings.SuspendLayout();
            gpbTags.SuspendLayout();
            tlpTags.SuspendLayout();
            cmnuLstTags.SuspendLayout();
            gpbTagFormatDatabase.SuspendLayout();
            pageHelp.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // txtSelectQuery
            // 
            txtSelectQuery.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtSelectQuery.AutoCompleteBracketsList = new char[] { '(', ')', '{', '}', '[', ']', '"', '"', '\'', '\'' };
            txtSelectQuery.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            txtSelectQuery.AutoScrollMinSize = new Size(0, 14);
            txtSelectQuery.BackBrush = null;
            txtSelectQuery.BorderStyle = BorderStyle.FixedSingle;
            txtSelectQuery.CharHeight = 14;
            txtSelectQuery.CharWidth = 8;
            txtSelectQuery.ContextMenuStrip = cmnuSelectQuery;
            txtSelectQuery.Cursor = Cursors.IBeam;
            txtSelectQuery.DefaultMarkerSize = 8;
            txtSelectQuery.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            txtSelectQuery.IsReplaceMode = false;
            txtSelectQuery.Location = new Point(8, 20);
            txtSelectQuery.Name = "txtSelectQuery";
            txtSelectQuery.Paddings = new Padding(0);
            txtSelectQuery.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            txtSelectQuery.ServiceColors = null;
            txtSelectQuery.Size = new Size(632, 481);
            txtSelectQuery.TabIndex = 5;
            txtSelectQuery.WordWrap = true;
            txtSelectQuery.Zoom = 100;
            txtSelectQuery.TextChanged += txtSelectQuery_TextChanged;
            // 
            // cmnuSelectQuery
            // 
            cmnuSelectQuery.Items.AddRange(new ToolStripItem[] { cmnuSelectQueryCut, cmnuSelectQueryCopy, cmnuSelectQueryPaste, cmnuSelectQuerySelectAll, toolStripSeparator1, cmnuSelectQueryUndo, cmnuSelectQueryRedo });
            cmnuSelectQuery.Name = "cmnuSelectQuery";
            cmnuSelectQuery.Size = new Size(123, 142);
            // 
            // cmnuSelectQueryCut
            // 
            cmnuSelectQueryCut.Image = (Image)resources.GetObject("cmnuSelectQueryCut.Image");
            cmnuSelectQueryCut.Name = "cmnuSelectQueryCut";
            cmnuSelectQueryCut.Size = new Size(122, 22);
            cmnuSelectQueryCut.Text = "Cut";
            cmnuSelectQueryCut.Click += cmnuSelectQueryCut_Click;
            // 
            // cmnuSelectQueryCopy
            // 
            cmnuSelectQueryCopy.Image = (Image)resources.GetObject("cmnuSelectQueryCopy.Image");
            cmnuSelectQueryCopy.Name = "cmnuSelectQueryCopy";
            cmnuSelectQueryCopy.Size = new Size(122, 22);
            cmnuSelectQueryCopy.Text = "Copy";
            cmnuSelectQueryCopy.Click += cmnuSelectQueryCopy_Click;
            // 
            // cmnuSelectQueryPaste
            // 
            cmnuSelectQueryPaste.Image = (Image)resources.GetObject("cmnuSelectQueryPaste.Image");
            cmnuSelectQueryPaste.Name = "cmnuSelectQueryPaste";
            cmnuSelectQueryPaste.Size = new Size(122, 22);
            cmnuSelectQueryPaste.Text = "Paste";
            cmnuSelectQueryPaste.Click += cmnuSelectQueryPaste_Click;
            // 
            // cmnuSelectQuerySelectAll
            // 
            cmnuSelectQuerySelectAll.Name = "cmnuSelectQuerySelectAll";
            cmnuSelectQuerySelectAll.Size = new Size(122, 22);
            cmnuSelectQuerySelectAll.Text = "Select All";
            cmnuSelectQuerySelectAll.Click += cmnuSelectQuerySelectAll_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(119, 6);
            // 
            // cmnuSelectQueryUndo
            // 
            cmnuSelectQueryUndo.Image = (Image)resources.GetObject("cmnuSelectQueryUndo.Image");
            cmnuSelectQueryUndo.Name = "cmnuSelectQueryUndo";
            cmnuSelectQueryUndo.Size = new Size(122, 22);
            cmnuSelectQueryUndo.Text = "Undo";
            cmnuSelectQueryUndo.Click += cmnuSelectQueryUndo_Click;
            // 
            // cmnuSelectQueryRedo
            // 
            cmnuSelectQueryRedo.Image = (Image)resources.GetObject("cmnuSelectQueryRedo.Image");
            cmnuSelectQueryRedo.Name = "cmnuSelectQueryRedo";
            cmnuSelectQueryRedo.Size = new Size(122, 22);
            cmnuSelectQueryRedo.Text = "Redo";
            cmnuSelectQueryRedo.Click += cmnuSelectQueryRedo_Click;
            // 
            // txtCmdQuery
            // 
            txtCmdQuery.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtCmdQuery.AutoCompleteBracketsList = new char[] { '(', ')', '{', '}', '[', ']', '"', '"', '\'', '\'' };
            txtCmdQuery.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            txtCmdQuery.AutoScrollMinSize = new Size(0, 14);
            txtCmdQuery.BackBrush = null;
            txtCmdQuery.BorderStyle = BorderStyle.FixedSingle;
            txtCmdQuery.CharHeight = 14;
            txtCmdQuery.CharWidth = 8;
            txtCmdQuery.ContextMenuStrip = cmnuCmdQuery;
            txtCmdQuery.Cursor = Cursors.IBeam;
            txtCmdQuery.DefaultMarkerSize = 8;
            txtCmdQuery.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            txtCmdQuery.IsReplaceMode = false;
            txtCmdQuery.Location = new Point(12, 81);
            txtCmdQuery.Name = "txtCmdQuery";
            txtCmdQuery.Paddings = new Padding(0);
            txtCmdQuery.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            txtCmdQuery.ServiceColors = null;
            txtCmdQuery.Size = new Size(606, 325);
            txtCmdQuery.TabIndex = 6;
            txtCmdQuery.WordWrap = true;
            txtCmdQuery.Zoom = 100;
            txtCmdQuery.TextChanged += txtCmdQuery_TextChanged;
            // 
            // cmnuCmdQuery
            // 
            cmnuCmdQuery.Items.AddRange(new ToolStripItem[] { cmnuCmdQueryCut, cmnuCmdQueryCopy, cmnuCmdQueryPaste, cmnuCmdQuerySelectAll, toolStripSeparator2, cmnuCmdQueryUndo, cmnuCmdQueryRedo });
            cmnuCmdQuery.Name = "cmnuSelectQuery";
            cmnuCmdQuery.Size = new Size(123, 142);
            // 
            // cmnuCmdQueryCut
            // 
            cmnuCmdQueryCut.Image = (Image)resources.GetObject("cmnuCmdQueryCut.Image");
            cmnuCmdQueryCut.Name = "cmnuCmdQueryCut";
            cmnuCmdQueryCut.Size = new Size(122, 22);
            cmnuCmdQueryCut.Text = "Cut";
            cmnuCmdQueryCut.Click += cmnuCmdQueryCut_Click;
            // 
            // cmnuCmdQueryCopy
            // 
            cmnuCmdQueryCopy.Image = (Image)resources.GetObject("cmnuCmdQueryCopy.Image");
            cmnuCmdQueryCopy.Name = "cmnuCmdQueryCopy";
            cmnuCmdQueryCopy.Size = new Size(122, 22);
            cmnuCmdQueryCopy.Text = "Copy";
            cmnuCmdQueryCopy.Click += cmnuCmdQueryCopy_Click;
            // 
            // cmnuCmdQueryPaste
            // 
            cmnuCmdQueryPaste.Image = (Image)resources.GetObject("cmnuCmdQueryPaste.Image");
            cmnuCmdQueryPaste.Name = "cmnuCmdQueryPaste";
            cmnuCmdQueryPaste.Size = new Size(122, 22);
            cmnuCmdQueryPaste.Text = "Paste";
            cmnuCmdQueryPaste.Click += cmnuCmdQueryPaste_Click;
            // 
            // cmnuCmdQuerySelectAll
            // 
            cmnuCmdQuerySelectAll.Name = "cmnuCmdQuerySelectAll";
            cmnuCmdQuerySelectAll.Size = new Size(122, 22);
            cmnuCmdQuerySelectAll.Text = "Select All";
            cmnuCmdQuerySelectAll.Click += cmnuCmdQuerySelectAll_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(119, 6);
            // 
            // cmnuCmdQueryUndo
            // 
            cmnuCmdQueryUndo.Image = (Image)resources.GetObject("cmnuCmdQueryUndo.Image");
            cmnuCmdQueryUndo.Name = "cmnuCmdQueryUndo";
            cmnuCmdQueryUndo.Size = new Size(122, 22);
            cmnuCmdQueryUndo.Text = "Undo";
            cmnuCmdQueryUndo.Click += cmnuCmdQueryUndo_Click;
            // 
            // cmnuCmdQueryRedo
            // 
            cmnuCmdQueryRedo.Image = (Image)resources.GetObject("cmnuCmdQueryRedo.Image");
            cmnuCmdQueryRedo.Name = "cmnuCmdQueryRedo";
            cmnuCmdQueryRedo.Size = new Size(122, 22);
            cmnuCmdQueryRedo.Text = "Redo";
            cmnuCmdQueryRedo.Click += cmnuCmdQueryRedo_Click;
            // 
            // txtHelp
            // 
            txtHelp.AutoCompleteBracketsList = new char[] { '(', ')', '{', '}', '[', ']', '"', '"', '\'', '\'' };
            txtHelp.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            txtHelp.AutoScrollMinSize = new Size(0, 14);
            txtHelp.BackBrush = null;
            txtHelp.BorderStyle = BorderStyle.FixedSingle;
            txtHelp.CharHeight = 14;
            txtHelp.CharWidth = 8;
            txtHelp.Cursor = Cursors.IBeam;
            txtHelp.DefaultMarkerSize = 8;
            txtHelp.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            txtHelp.Dock = DockStyle.Fill;
            txtHelp.Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtHelp.IsReplaceMode = false;
            txtHelp.Location = new Point(0, 0);
            txtHelp.Name = "txtHelp";
            txtHelp.Paddings = new Padding(0);
            txtHelp.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            txtHelp.ServiceColors = null;
            txtHelp.Size = new Size(649, 507);
            txtHelp.TabIndex = 7;
            txtHelp.WordWrap = true;
            txtHelp.Zoom = 100;
            // 
            // cbDataSourceType
            // 
            cbDataSourceType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbDataSourceType.FormattingEnabled = true;
            cbDataSourceType.Items.AddRange(new object[] { "<Choose database type>", "Microsoft SQL Server", "Oracle", "PostgreSQL", "MySQL", "OLE DB", "ODBC", "Firebird" });
            cbDataSourceType.Location = new Point(15, 22);
            cbDataSourceType.Margin = new Padding(4, 3, 4, 3);
            cbDataSourceType.Name = "cbDataSourceType";
            cbDataSourceType.Size = new Size(272, 23);
            cbDataSourceType.TabIndex = 0;
            cbDataSourceType.SelectedIndexChanged += cbDataSourceType_SelectedIndexChanged;
            // 
            // gbConnection
            // 
            gbConnection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
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
            gbConnection.Location = new Point(7, 75);
            gbConnection.Margin = new Padding(4, 3, 4, 3);
            gbConnection.Name = "gbConnection";
            gbConnection.Padding = new Padding(12, 3, 12, 12);
            gbConnection.Size = new Size(634, 424);
            gbConnection.TabIndex = 1;
            gbConnection.TabStop = false;
            gbConnection.Text = "Connection";
            // 
            // txtOptionalOptions
            // 
            txtOptionalOptions.Location = new Point(15, 172);
            txtOptionalOptions.Margin = new Padding(4, 3, 4, 3);
            txtOptionalOptions.Name = "txtOptionalOptions";
            txtOptionalOptions.Size = new Size(452, 23);
            txtOptionalOptions.TabIndex = 13;
            txtOptionalOptions.TextChanged += txtConnProp_TextChanged;
            // 
            // lblOptionalOptions
            // 
            lblOptionalOptions.AutoSize = true;
            lblOptionalOptions.Location = new Point(12, 153);
            lblOptionalOptions.Margin = new Padding(4, 0, 4, 0);
            lblOptionalOptions.Name = "lblOptionalOptions";
            lblOptionalOptions.Size = new Size(98, 15);
            lblOptionalOptions.TabIndex = 12;
            lblOptionalOptions.Text = "Optional Options";
            // 
            // txtPort
            // 
            txtPort.Location = new Point(245, 37);
            txtPort.Margin = new Padding(4, 3, 4, 3);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(222, 23);
            txtPort.TabIndex = 11;
            txtPort.TextChanged += txtConnProp_TextChanged;
            // 
            // lblPort
            // 
            lblPort.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPort.AutoSize = true;
            lblPort.Location = new Point(241, 18);
            lblPort.Margin = new Padding(4, 0, 4, 0);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(29, 15);
            lblPort.TabIndex = 10;
            lblPort.Text = "Port";
            // 
            // txtConnectionString
            // 
            txtConnectionString.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtConnectionString.Location = new Point(15, 217);
            txtConnectionString.Margin = new Padding(4, 3, 4, 3);
            txtConnectionString.Multiline = true;
            txtConnectionString.Name = "txtConnectionString";
            txtConnectionString.Size = new Size(603, 191);
            txtConnectionString.TabIndex = 9;
            txtConnectionString.TextChanged += txtConnectionString_TextChanged;
            // 
            // lblConnectionString
            // 
            lblConnectionString.AutoSize = true;
            lblConnectionString.Location = new Point(12, 198);
            lblConnectionString.Margin = new Padding(4, 0, 4, 0);
            lblConnectionString.Name = "lblConnectionString";
            lblConnectionString.Size = new Size(102, 15);
            lblConnectionString.TabIndex = 8;
            lblConnectionString.Text = "Connection string";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(245, 127);
            txtPassword.Margin = new Padding(4, 3, 4, 3);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(222, 23);
            txtPassword.TabIndex = 7;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += txtConnProp_TextChanged;
            // 
            // lblPassword
            // 
            lblPassword.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(241, 108);
            lblPassword.Margin = new Padding(4, 0, 4, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(57, 15);
            lblPassword.TabIndex = 6;
            lblPassword.Text = "Password";
            // 
            // txtUser
            // 
            txtUser.Location = new Point(15, 127);
            txtUser.Margin = new Padding(4, 3, 4, 3);
            txtUser.Name = "txtUser";
            txtUser.Size = new Size(222, 23);
            txtUser.TabIndex = 5;
            txtUser.TextChanged += txtConnProp_TextChanged;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.Location = new Point(12, 108);
            lblUser.Margin = new Padding(4, 0, 4, 0);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(30, 15);
            lblUser.TabIndex = 4;
            lblUser.Text = "User";
            // 
            // txtDatabase
            // 
            txtDatabase.Location = new Point(15, 82);
            txtDatabase.Margin = new Padding(4, 3, 4, 3);
            txtDatabase.Name = "txtDatabase";
            txtDatabase.Size = new Size(452, 23);
            txtDatabase.TabIndex = 3;
            txtDatabase.TextChanged += txtConnProp_TextChanged;
            // 
            // lblDatabase
            // 
            lblDatabase.AutoSize = true;
            lblDatabase.Location = new Point(12, 63);
            lblDatabase.Margin = new Padding(4, 0, 4, 0);
            lblDatabase.Name = "lblDatabase";
            lblDatabase.Size = new Size(55, 15);
            lblDatabase.TabIndex = 2;
            lblDatabase.Text = "Database";
            // 
            // txtServer
            // 
            txtServer.Location = new Point(15, 37);
            txtServer.Margin = new Padding(4, 3, 4, 3);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(222, 23);
            txtServer.TabIndex = 1;
            txtServer.TextChanged += txtConnProp_TextChanged;
            // 
            // lblServer
            // 
            lblServer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblServer.AutoSize = true;
            lblServer.Location = new Point(12, 18);
            lblServer.Margin = new Padding(4, 0, 4, 0);
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(39, 15);
            lblServer.TabIndex = 0;
            lblServer.Text = "Server";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(pageDatabase);
            tabControl.Controls.Add(pageQuery);
            tabControl.Controls.Add(pageData);
            tabControl.Controls.Add(pageCommands);
            tabControl.Controls.Add(pageSettings);
            tabControl.Controls.Add(pageHelp);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(657, 535);
            tabControl.TabIndex = 0;
            // 
            // pageDatabase
            // 
            pageDatabase.Controls.Add(gbDataSourceType);
            pageDatabase.Controls.Add(gbConnection);
            pageDatabase.Location = new Point(4, 24);
            pageDatabase.Margin = new Padding(4, 3, 4, 3);
            pageDatabase.Name = "pageDatabase";
            pageDatabase.Padding = new Padding(4, 3, 4, 3);
            pageDatabase.Size = new Size(649, 507);
            pageDatabase.TabIndex = 0;
            pageDatabase.Text = "Database";
            pageDatabase.UseVisualStyleBackColor = true;
            // 
            // gbDataSourceType
            // 
            gbDataSourceType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbDataSourceType.Controls.Add(btnConnectionTest);
            gbDataSourceType.Controls.Add(cbDataSourceType);
            gbDataSourceType.Location = new Point(7, 7);
            gbDataSourceType.Margin = new Padding(4, 3, 4, 3);
            gbDataSourceType.Name = "gbDataSourceType";
            gbDataSourceType.Padding = new Padding(12, 3, 12, 12);
            gbDataSourceType.Size = new Size(634, 61);
            gbDataSourceType.TabIndex = 0;
            gbDataSourceType.TabStop = false;
            gbDataSourceType.Text = "Data Source Type";
            // 
            // btnConnectionTest
            // 
            btnConnectionTest.Location = new Point(298, 21);
            btnConnectionTest.Margin = new Padding(4, 3, 4, 3);
            btnConnectionTest.Name = "btnConnectionTest";
            btnConnectionTest.Size = new Size(170, 27);
            btnConnectionTest.TabIndex = 3;
            btnConnectionTest.Text = "Testing connection...";
            btnConnectionTest.UseVisualStyleBackColor = true;
            btnConnectionTest.Click += btnConnectionTest_Click;
            // 
            // pageQuery
            // 
            pageQuery.Controls.Add(lblSelectQuery);
            pageQuery.Controls.Add(txtSelectQuery);
            pageQuery.Location = new Point(4, 24);
            pageQuery.Margin = new Padding(4, 3, 4, 3);
            pageQuery.Name = "pageQuery";
            pageQuery.Padding = new Padding(4, 3, 4, 3);
            pageQuery.Size = new Size(649, 507);
            pageQuery.TabIndex = 1;
            pageQuery.Text = "Data Retrieval";
            pageQuery.UseVisualStyleBackColor = true;
            // 
            // lblSelectQuery
            // 
            lblSelectQuery.AutoSize = true;
            lblSelectQuery.Location = new Point(4, 5);
            lblSelectQuery.Margin = new Padding(4, 0, 4, 0);
            lblSelectQuery.Name = "lblSelectQuery";
            lblSelectQuery.Size = new Size(28, 15);
            lblSelectQuery.TabIndex = 0;
            lblSelectQuery.Text = "SQL";
            // 
            // pageData
            // 
            pageData.Controls.Add(tlpPanel);
            pageData.Location = new Point(4, 24);
            pageData.Margin = new Padding(4, 3, 4, 3);
            pageData.Name = "pageData";
            pageData.Size = new Size(649, 507);
            pageData.TabIndex = 4;
            pageData.Text = "Data";
            pageData.UseVisualStyleBackColor = true;
            // 
            // tlpPanel
            // 
            tlpPanel.ColumnCount = 1;
            tlpPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPanel.Controls.Add(dgvData, 0, 0);
            tlpPanel.Controls.Add(btnExecuteSQLQuery, 0, 1);
            tlpPanel.Dock = DockStyle.Fill;
            tlpPanel.Location = new Point(0, 0);
            tlpPanel.Margin = new Padding(4, 3, 4, 3);
            tlpPanel.Name = "tlpPanel";
            tlpPanel.RowCount = 2;
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 35F));
            tlpPanel.Size = new Size(649, 507);
            tlpPanel.TabIndex = 10;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Control;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle6;
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(4, 3);
            dgvData.Margin = new Padding(4, 3, 4, 3);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.Size = new Size(641, 466);
            dgvData.TabIndex = 8;
            dgvData.DataError += dgvData_DataError;
            // 
            // btnExecuteSQLQuery
            // 
            btnExecuteSQLQuery.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnExecuteSQLQuery.Location = new Point(4, 475);
            btnExecuteSQLQuery.Margin = new Padding(4, 3, 4, 3);
            btnExecuteSQLQuery.Name = "btnExecuteSQLQuery";
            btnExecuteSQLQuery.Size = new Size(641, 27);
            btnExecuteSQLQuery.TabIndex = 9;
            btnExecuteSQLQuery.Text = "Execute SQL query";
            btnExecuteSQLQuery.UseVisualStyleBackColor = true;
            btnExecuteSQLQuery.Click += btnExecuteSQLQuery_Click;
            // 
            // pageCommands
            // 
            pageCommands.Controls.Add(gbCommandParams);
            pageCommands.Controls.Add(gbCommand);
            pageCommands.Location = new Point(4, 24);
            pageCommands.Margin = new Padding(4, 3, 4, 3);
            pageCommands.Name = "pageCommands";
            pageCommands.Padding = new Padding(4, 3, 4, 3);
            pageCommands.Size = new Size(649, 507);
            pageCommands.TabIndex = 2;
            pageCommands.Text = "Commands";
            pageCommands.UseVisualStyleBackColor = true;
            // 
            // gbCommandParams
            // 
            gbCommandParams.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbCommandParams.Controls.Add(lblCmdCode);
            gbCommandParams.Controls.Add(txtCmdCode);
            gbCommandParams.Controls.Add(lblCmdQuery);
            gbCommandParams.Controls.Add(txtName);
            gbCommandParams.Controls.Add(lblName);
            gbCommandParams.Controls.Add(numCmdNum);
            gbCommandParams.Controls.Add(lblCmdNum);
            gbCommandParams.Controls.Add(txtCmdQuery);
            gbCommandParams.Location = new Point(7, 77);
            gbCommandParams.Margin = new Padding(4, 3, 4, 3);
            gbCommandParams.Name = "gbCommandParams";
            gbCommandParams.Padding = new Padding(12, 3, 12, 12);
            gbCommandParams.Size = new Size(634, 421);
            gbCommandParams.TabIndex = 1;
            gbCommandParams.TabStop = false;
            gbCommandParams.Text = "Command Parameters";
            // 
            // lblCmdCode
            // 
            lblCmdCode.AutoSize = true;
            lblCmdCode.Location = new Point(127, 18);
            lblCmdCode.Margin = new Padding(4, 0, 4, 0);
            lblCmdCode.Name = "lblCmdCode";
            lblCmdCode.Size = new Size(93, 15);
            lblCmdCode.TabIndex = 8;
            lblCmdCode.Text = "Command code";
            // 
            // txtCmdCode
            // 
            txtCmdCode.Location = new Point(127, 37);
            txtCmdCode.Margin = new Padding(4, 3, 4, 3);
            txtCmdCode.Name = "txtCmdCode";
            txtCmdCode.Size = new Size(106, 23);
            txtCmdCode.TabIndex = 7;
            txtCmdCode.TextChanged += txtCmdCode_TextChanged;
            // 
            // lblCmdQuery
            // 
            lblCmdQuery.AutoSize = true;
            lblCmdQuery.Location = new Point(12, 63);
            lblCmdQuery.Margin = new Padding(4, 0, 4, 0);
            lblCmdQuery.Name = "lblCmdQuery";
            lblCmdQuery.Size = new Size(28, 15);
            lblCmdQuery.TabIndex = 4;
            lblCmdQuery.Text = "SQL";
            // 
            // txtName
            // 
            txtName.Location = new Point(241, 37);
            txtName.Margin = new Padding(4, 3, 4, 3);
            txtName.Name = "txtName";
            txtName.Size = new Size(226, 23);
            txtName.TabIndex = 3;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(241, 18);
            lblName.Margin = new Padding(4, 0, 4, 0);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 2;
            lblName.Text = "Name";
            // 
            // numCmdNum
            // 
            numCmdNum.Location = new Point(15, 37);
            numCmdNum.Margin = new Padding(4, 3, 4, 3);
            numCmdNum.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numCmdNum.Name = "numCmdNum";
            numCmdNum.Size = new Size(106, 23);
            numCmdNum.TabIndex = 1;
            numCmdNum.ValueChanged += numCmdNum_ValueChanged;
            // 
            // lblCmdNum
            // 
            lblCmdNum.AutoSize = true;
            lblCmdNum.Location = new Point(12, 18);
            lblCmdNum.Margin = new Padding(4, 0, 4, 0);
            lblCmdNum.Name = "lblCmdNum";
            lblCmdNum.Size = new Size(109, 15);
            lblCmdNum.TabIndex = 0;
            lblCmdNum.Text = "Command number";
            // 
            // gbCommand
            // 
            gbCommand.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbCommand.Controls.Add(cbCommand);
            gbCommand.Controls.Add(btnDeleteCommand);
            gbCommand.Controls.Add(btnCreateCommand);
            gbCommand.Location = new Point(7, 7);
            gbCommand.Margin = new Padding(4, 3, 4, 3);
            gbCommand.Name = "gbCommand";
            gbCommand.Padding = new Padding(12, 3, 12, 12);
            gbCommand.Size = new Size(634, 63);
            gbCommand.TabIndex = 0;
            gbCommand.TabStop = false;
            gbCommand.Text = "Command";
            // 
            // cbCommand
            // 
            cbCommand.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCommand.FormattingEnabled = true;
            cbCommand.Location = new Point(15, 23);
            cbCommand.Margin = new Padding(4, 3, 4, 3);
            cbCommand.Name = "cbCommand";
            cbCommand.Size = new Size(263, 23);
            cbCommand.TabIndex = 0;
            cbCommand.SelectedIndexChanged += cbCommand_SelectedIndexChanged;
            // 
            // btnDeleteCommand
            // 
            btnDeleteCommand.Location = new Point(380, 22);
            btnDeleteCommand.Margin = new Padding(4, 3, 4, 3);
            btnDeleteCommand.Name = "btnDeleteCommand";
            btnDeleteCommand.Size = new Size(88, 27);
            btnDeleteCommand.TabIndex = 2;
            btnDeleteCommand.Text = "Delete";
            btnDeleteCommand.UseVisualStyleBackColor = true;
            btnDeleteCommand.Click += btnDeleteCommand_Click;
            // 
            // btnCreateCommand
            // 
            btnCreateCommand.Location = new Point(286, 22);
            btnCreateCommand.Margin = new Padding(4, 3, 4, 3);
            btnCreateCommand.Name = "btnCreateCommand";
            btnCreateCommand.Size = new Size(88, 27);
            btnCreateCommand.TabIndex = 1;
            btnCreateCommand.Text = "Create";
            btnCreateCommand.UseVisualStyleBackColor = true;
            btnCreateCommand.Click += btnCreateCommand_Click;
            // 
            // pageSettings
            // 
            pageSettings.Controls.Add(ckbWriteDriverLog);
            pageSettings.Controls.Add(gpbTags);
            pageSettings.Controls.Add(gpbTagFormatDatabase);
            pageSettings.Location = new Point(4, 24);
            pageSettings.Margin = new Padding(4, 3, 4, 3);
            pageSettings.Name = "pageSettings";
            pageSettings.Size = new Size(649, 507);
            pageSettings.TabIndex = 3;
            pageSettings.Text = "Settings";
            pageSettings.UseVisualStyleBackColor = true;
            // 
            // ckbWriteDriverLog
            // 
            ckbWriteDriverLog.AutoSize = true;
            ckbWriteDriverLog.Location = new Point(24, 12);
            ckbWriteDriverLog.Name = "ckbWriteDriverLog";
            ckbWriteDriverLog.Size = new Size(322, 19);
            ckbWriteDriverLog.TabIndex = 13;
            ckbWriteDriverLog.Text = "Log the result of a query from the database (debugging)";
            ckbWriteDriverLog.UseVisualStyleBackColor = true;
            ckbWriteDriverLog.CheckedChanged += ckbWriteDriverLog_CheckedChanged;
            // 
            // gpbTags
            // 
            gpbTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gpbTags.Controls.Add(tlpTags);
            gpbTags.Location = new Point(7, 120);
            gpbTags.Margin = new Padding(4, 3, 4, 3);
            gpbTags.Name = "gpbTags";
            gpbTags.Padding = new Padding(12, 3, 12, 12);
            gpbTags.Size = new Size(632, 375);
            gpbTags.TabIndex = 12;
            gpbTags.TabStop = false;
            gpbTags.Text = "Tags";
            // 
            // tlpTags
            // 
            tlpTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tlpTags.ColumnCount = 1;
            tlpTags.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.5955F));
            tlpTags.Controls.Add(lstTags, 0, 0);
            tlpTags.Location = new Point(14, 22);
            tlpTags.Name = "tlpTags";
            tlpTags.RowCount = 1;
            tlpTags.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTags.Size = new Size(603, 338);
            tlpTags.TabIndex = 1;
            // 
            // lstTags
            // 
            lstTags.Alignment = ListViewAlignment.Default;
            lstTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstTags.Columns.AddRange(new ColumnHeader[] { clmTagname, clmTagCode, clmTagFormat, clmTagEnabled });
            lstTags.ContextMenuStrip = cmnuLstTags;
            lstTags.FullRowSelect = true;
            lstTags.GridLines = true;
            lstTags.Location = new Point(3, 3);
            lstTags.MultiSelect = false;
            lstTags.Name = "lstTags";
            lstTags.Size = new Size(597, 332);
            lstTags.TabIndex = 2;
            lstTags.UseCompatibleStateImageBehavior = false;
            lstTags.View = System.Windows.Forms.View.Details;
            lstTags.KeyDown += lstTags_KeyDown;
            lstTags.MouseClick += lstTags_MouseClick;
            lstTags.MouseDoubleClick += lstTags_MouseDoubleClick;
            // 
            // clmTagname
            // 
            clmTagname.Text = "Name";
            clmTagname.Width = 200;
            // 
            // clmTagCode
            // 
            clmTagCode.Text = "Сode";
            clmTagCode.Width = 200;
            // 
            // clmTagFormat
            // 
            clmTagFormat.Text = "Format";
            clmTagFormat.Width = 110;
            // 
            // clmTagEnabled
            // 
            clmTagEnabled.Text = "Enabled";
            clmTagEnabled.Width = 80;
            // 
            // cmnuLstTags
            // 
            cmnuLstTags.Items.AddRange(new ToolStripItem[] { cmnuTagRefresh, toolStripSeparator6, cmnuTagAdd, cmnuListTagAdd, toolStripSeparator3, cmnuTagChange, toolStripSeparator4, cmnuTagDelete, cmnuTagAllDelete, toolStripSeparator5, cmnuUp, cmnuDown });
            cmnuLstTags.Name = "cmnuSelectQuery";
            cmnuLstTags.Size = new Size(181, 226);
            // 
            // cmnuTagRefresh
            // 
            cmnuTagRefresh.Image = (Image)resources.GetObject("cmnuTagRefresh.Image");
            cmnuTagRefresh.Name = "cmnuTagRefresh";
            cmnuTagRefresh.Size = new Size(180, 22);
            cmnuTagRefresh.Text = "Refresh";
            cmnuTagRefresh.Click += cmnuTagRefresh_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(177, 6);
            // 
            // cmnuTagAdd
            // 
            cmnuTagAdd.Image = (Image)resources.GetObject("cmnuTagAdd.Image");
            cmnuTagAdd.Name = "cmnuTagAdd";
            cmnuTagAdd.Size = new Size(180, 22);
            cmnuTagAdd.Text = "Add Tag";
            cmnuTagAdd.Click += cmnuTagAdd_Click;
            // 
            // cmnuListTagAdd
            // 
            cmnuListTagAdd.Image = (Image)resources.GetObject("cmnuListTagAdd.Image");
            cmnuListTagAdd.Name = "cmnuListTagAdd";
            cmnuListTagAdd.Size = new Size(180, 22);
            cmnuListTagAdd.Text = "Add list of Tags";
            cmnuListTagAdd.Click += cmnuListTagAdd_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(177, 6);
            // 
            // cmnuTagChange
            // 
            cmnuTagChange.Image = (Image)resources.GetObject("cmnuTagChange.Image");
            cmnuTagChange.Name = "cmnuTagChange";
            cmnuTagChange.Size = new Size(180, 22);
            cmnuTagChange.Text = "Change Tag";
            cmnuTagChange.Click += cmnuTagChange_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(177, 6);
            // 
            // cmnuTagDelete
            // 
            cmnuTagDelete.Image = (Image)resources.GetObject("cmnuTagDelete.Image");
            cmnuTagDelete.Name = "cmnuTagDelete";
            cmnuTagDelete.Size = new Size(180, 22);
            cmnuTagDelete.Text = "Delete Tag";
            cmnuTagDelete.Click += cmnuTagDelete_Click;
            // 
            // cmnuTagAllDelete
            // 
            cmnuTagAllDelete.Image = (Image)resources.GetObject("cmnuTagAllDelete.Image");
            cmnuTagAllDelete.Name = "cmnuTagAllDelete";
            cmnuTagAllDelete.Size = new Size(180, 22);
            cmnuTagAllDelete.Text = "Delete all Tags";
            cmnuTagAllDelete.Click += cmnuTagAllDelete_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(177, 6);
            // 
            // cmnuUp
            // 
            cmnuUp.Image = (Image)resources.GetObject("cmnuUp.Image");
            cmnuUp.Name = "cmnuUp";
            cmnuUp.Size = new Size(180, 22);
            cmnuUp.Text = "Up";
            cmnuUp.Click += cmnuUp_Click;
            // 
            // cmnuDown
            // 
            cmnuDown.Image = (Image)resources.GetObject("cmnuDown.Image");
            cmnuDown.Name = "cmnuDown";
            cmnuDown.Size = new Size(180, 22);
            cmnuDown.Text = "Down";
            cmnuDown.Click += cmnuDown_Click;
            // 
            // gpbTagFormatDatabase
            // 
            gpbTagFormatDatabase.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gpbTagFormatDatabase.Controls.Add(rdbKPTagsBasedRequestedTableRows);
            gpbTagFormatDatabase.Controls.Add(rdbKPTagsBasedRequestedTableColumns);
            gpbTagFormatDatabase.Location = new Point(7, 37);
            gpbTagFormatDatabase.Margin = new Padding(4, 3, 4, 3);
            gpbTagFormatDatabase.Name = "gpbTagFormatDatabase";
            gpbTagFormatDatabase.Padding = new Padding(12, 3, 12, 12);
            gpbTagFormatDatabase.Size = new Size(632, 77);
            gpbTagFormatDatabase.TabIndex = 7;
            gpbTagFormatDatabase.TabStop = false;
            gpbTagFormatDatabase.Text = "Format of Tags from the Database";
            // 
            // rdbKPTagsBasedRequestedTableRows
            // 
            rdbKPTagsBasedRequestedTableRows.AutoSize = true;
            rdbKPTagsBasedRequestedTableRows.Location = new Point(15, 48);
            rdbKPTagsBasedRequestedTableRows.Margin = new Padding(4, 3, 4, 3);
            rdbKPTagsBasedRequestedTableRows.Name = "rdbKPTagsBasedRequestedTableRows";
            rdbKPTagsBasedRequestedTableRows.Size = new Size(263, 19);
            rdbKPTagsBasedRequestedTableRows.TabIndex = 1;
            rdbKPTagsBasedRequestedTableRows.TabStop = true;
            rdbKPTagsBasedRequestedTableRows.Text = "Tags based on the list of requested table rows";
            rdbKPTagsBasedRequestedTableRows.UseVisualStyleBackColor = true;
            rdbKPTagsBasedRequestedTableRows.CheckedChanged += rdbKPTagsBasedRequestedTableRows_CheckedChanged;
            // 
            // rdbKPTagsBasedRequestedTableColumns
            // 
            rdbKPTagsBasedRequestedTableColumns.AutoSize = true;
            rdbKPTagsBasedRequestedTableColumns.Location = new Point(15, 22);
            rdbKPTagsBasedRequestedTableColumns.Margin = new Padding(4, 3, 4, 3);
            rdbKPTagsBasedRequestedTableColumns.Name = "rdbKPTagsBasedRequestedTableColumns";
            rdbKPTagsBasedRequestedTableColumns.Size = new Size(284, 19);
            rdbKPTagsBasedRequestedTableColumns.TabIndex = 0;
            rdbKPTagsBasedRequestedTableColumns.TabStop = true;
            rdbKPTagsBasedRequestedTableColumns.Text = "Tags based on the list of requested table columns";
            rdbKPTagsBasedRequestedTableColumns.UseVisualStyleBackColor = true;
            rdbKPTagsBasedRequestedTableColumns.CheckedChanged += rdbKPTagsBasedRequestedTableColumns_CheckedChanged;
            // 
            // pageHelp
            // 
            pageHelp.Controls.Add(txtHelp);
            pageHelp.Location = new Point(4, 24);
            pageHelp.Margin = new Padding(4, 3, 4, 3);
            pageHelp.Name = "pageHelp";
            pageHelp.Size = new Size(649, 507);
            pageHelp.TabIndex = 5;
            pageHelp.Text = "Help";
            pageHelp.UseVisualStyleBackColor = true;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 535);
            pnlBottom.Margin = new Padding(4, 3, 4, 3);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(657, 47);
            pnlBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(556, 7);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 27);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(461, 7);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FrmConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnClose;
            ClientSize = new Size(657, 582);
            Controls.Add(tabControl);
            Controls.Add(pnlBottom);
            Margin = new Padding(4, 3, 4, 3);
            MinimizeBox = false;
            MinimumSize = new Size(522, 525);
            Name = "FrmConfig";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "DB Import Plus - Device {0} Properties";
            FormClosing += FrmConfig_FormClosing;
            Load += FrmConfig_Load;
            ((System.ComponentModel.ISupportInitialize)txtSelectQuery).EndInit();
            cmnuSelectQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtCmdQuery).EndInit();
            cmnuCmdQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)txtHelp).EndInit();
            gbConnection.ResumeLayout(false);
            gbConnection.PerformLayout();
            tabControl.ResumeLayout(false);
            pageDatabase.ResumeLayout(false);
            gbDataSourceType.ResumeLayout(false);
            pageQuery.ResumeLayout(false);
            pageQuery.PerformLayout();
            pageData.ResumeLayout(false);
            tlpPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            pageCommands.ResumeLayout(false);
            gbCommandParams.ResumeLayout(false);
            gbCommandParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCmdNum).EndInit();
            gbCommand.ResumeLayout(false);
            pageSettings.ResumeLayout(false);
            pageSettings.PerformLayout();
            gpbTags.ResumeLayout(false);
            tlpTags.ResumeLayout(false);
            cmnuLstTags.ResumeLayout(false);
            gpbTagFormatDatabase.ResumeLayout(false);
            gpbTagFormatDatabase.PerformLayout();
            pageHelp.ResumeLayout(false);
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
        private TabPage pageQuery;
        private Panel pnlBottom;
        private Button btnClose;
        private Button btnSave;
        private GroupBox gbDataSourceType;
        private Label lblSelectQuery;
        private TabPage pageCommands;
        private ComboBox cbCommand;
        private GroupBox gbCommand;
        private Button btnDeleteCommand;
        private Button btnCreateCommand;
        private GroupBox gbCommandParams;
        private Label lblCmdQuery;
        private TextBox txtName;
        private Label lblName;
        private NumericUpDown numCmdNum;
        private Label lblCmdNum;
        private FastColoredTextBoxNS.FastColoredTextBox txtSelectQuery;
        private TextBox txtOptionalOptions;
        private Label lblOptionalOptions;
        private TextBox txtPort;
        private Label lblPort;
        private Button btnConnectionTest;
        private FastColoredTextBoxNS.FastColoredTextBox txtCmdQuery;
        private TabPage pageSettings;
        private GroupBox gpbTagFormatDatabase;
        private RadioButton rdbKPTagsBasedRequestedTableRows;
        private RadioButton rdbKPTagsBasedRequestedTableColumns;
        private TabPage pageData;
        private TableLayoutPanel tlpPanel;
        private DataGridView dgvData;
        private Button btnExecuteSQLQuery;
        private TabPage pageHelp;
        private FastColoredTextBoxNS.FastColoredTextBox txtHelp;
        private Label lblCmdCode;
        private TextBox txtCmdCode;
        private GroupBox gpbTags;
        private TableLayoutPanel tlpTags;
        private ContextMenuStrip cmnuSelectQuery;
        private ToolStripMenuItem cmnuSelectQuerySelectAll;
        private ToolStripMenuItem cmnuSelectQueryCopy;
        private ToolStripMenuItem cmnuSelectQueryCut;
        private ToolStripMenuItem cmnuSelectQueryPaste;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cmnuSelectQueryUndo;
        private ToolStripMenuItem cmnuSelectQueryRedo;
        private ContextMenuStrip cmnuCmdQuery;
        private ToolStripMenuItem cmnuCmdQueryCut;
        private ToolStripMenuItem cmnuCmdQueryCopy;
        private ToolStripMenuItem cmnuCmdQueryPaste;
        private ToolStripMenuItem cmnuCmdQuerySelectAll;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem cmnuCmdQueryUndo;
        private ToolStripMenuItem cmnuCmdQueryRedo;
        private ContextMenuStrip cmnuLstTags;
        private ToolStripMenuItem cmnuUp;
        private ToolStripMenuItem cmnuDown;
        private ListView lstTags;
        private ColumnHeader clmTagname;
        private ColumnHeader clmTagEnabled;
        private ToolStripMenuItem cmnuTagAdd;
        private ToolStripMenuItem cmnuListTagAdd;
        private ToolStripMenuItem cmnuTagDelete;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cmnuTagChange;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem cmnuTagAllDelete;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem cmnuTagRefresh;
        private ToolStripSeparator toolStripSeparator6;
        private ColumnHeader clmTagCode;
        private ColumnHeader clmTagFormat;
        private CheckBox ckbWriteDriverLog;
    }
}