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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtSelectQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            this.cmnuSelectQuery = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuSelectQueryCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuSelectQueryCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuSelectQueryPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuSelectQuerySelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuSelectQueryUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuSelectQueryRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.txtCmdQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            this.cmnuCmdQuery = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuCmdQueryCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCmdQueryCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCmdQueryPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCmdQuerySelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuCmdQueryUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCmdQueryRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.txtHelp = new FastColoredTextBoxNS.FastColoredTextBox();
            this.cbDataSourceType = new System.Windows.Forms.ComboBox();
            this.gbConnection = new System.Windows.Forms.GroupBox();
            this.txtOptionalOptions = new System.Windows.Forms.TextBox();
            this.lblOptionalOptions = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pageDatabase = new System.Windows.Forms.TabPage();
            this.gbDataSourceType = new System.Windows.Forms.GroupBox();
            this.btnConnectionTest = new System.Windows.Forms.Button();
            this.pageQuery = new System.Windows.Forms.TabPage();
            this.lblSelectQuery = new System.Windows.Forms.Label();
            this.pageData = new System.Windows.Forms.TabPage();
            this.tlpPanel = new System.Windows.Forms.TableLayoutPanel();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnExecuteSQLQuery = new System.Windows.Forms.Button();
            this.pageCommands = new System.Windows.Forms.TabPage();
            this.gbCommandParams = new System.Windows.Forms.GroupBox();
            this.lblCmdCode = new System.Windows.Forms.Label();
            this.txtCmdCode = new System.Windows.Forms.TextBox();
            this.lblCmdQuery = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.numCmdNum = new System.Windows.Forms.NumericUpDown();
            this.lblCmdNum = new System.Windows.Forms.Label();
            this.gbCommand = new System.Windows.Forms.GroupBox();
            this.cbCommand = new System.Windows.Forms.ComboBox();
            this.btnDeleteCommand = new System.Windows.Forms.Button();
            this.btnCreateCommand = new System.Windows.Forms.Button();
            this.pageSettings = new System.Windows.Forms.TabPage();
            this.gpbTags = new System.Windows.Forms.GroupBox();
            this.tlpTags = new System.Windows.Forms.TableLayoutPanel();
            this.lstTags = new System.Windows.Forms.ListView();
            this.clmTagname = new System.Windows.Forms.ColumnHeader();
            this.clmTagCode = new System.Windows.Forms.ColumnHeader();
            this.clmTagEnabled = new System.Windows.Forms.ColumnHeader();
            this.cmnuLstTags = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmnuTagRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuTagAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuListTagAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuTagChange = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuTagDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuTagAllDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuUp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuDown = new System.Windows.Forms.ToolStripMenuItem();
            this.gpbTagFormatDatabase = new System.Windows.Forms.GroupBox();
            this.rdbKPTagsBasedRequestedTableRows = new System.Windows.Forms.RadioButton();
            this.rdbKPTagsBasedRequestedTableColumns = new System.Windows.Forms.RadioButton();
            this.pageHelp = new System.Windows.Forms.TabPage();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectQuery)).BeginInit();
            this.cmnuSelectQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCmdQuery)).BeginInit();
            this.cmnuCmdQuery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtHelp)).BeginInit();
            this.gbConnection.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.pageDatabase.SuspendLayout();
            this.gbDataSourceType.SuspendLayout();
            this.pageQuery.SuspendLayout();
            this.pageData.SuspendLayout();
            this.tlpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.pageCommands.SuspendLayout();
            this.gbCommandParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCmdNum)).BeginInit();
            this.gbCommand.SuspendLayout();
            this.pageSettings.SuspendLayout();
            this.gpbTags.SuspendLayout();
            this.tlpTags.SuspendLayout();
            this.cmnuLstTags.SuspendLayout();
            this.gpbTagFormatDatabase.SuspendLayout();
            this.pageHelp.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSelectQuery
            // 
            this.txtSelectQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSelectQuery.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtSelectQuery.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.txtSelectQuery.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.txtSelectQuery.BackBrush = null;
            this.txtSelectQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSelectQuery.CharHeight = 14;
            this.txtSelectQuery.CharWidth = 8;
            this.txtSelectQuery.ContextMenuStrip = this.cmnuSelectQuery;
            this.txtSelectQuery.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSelectQuery.DefaultMarkerSize = 8;
            this.txtSelectQuery.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtSelectQuery.IsReplaceMode = false;
            this.txtSelectQuery.Location = new System.Drawing.Point(8, 20);
            this.txtSelectQuery.Name = "txtSelectQuery";
            this.txtSelectQuery.Paddings = new System.Windows.Forms.Padding(0);
            this.txtSelectQuery.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtSelectQuery.ServiceColors = null;
            this.txtSelectQuery.Size = new System.Drawing.Size(632, 443);
            this.txtSelectQuery.TabIndex = 5;
            this.txtSelectQuery.WordWrap = true;
            this.txtSelectQuery.Zoom = 100;
            this.txtSelectQuery.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtSelectQuery_TextChanged);
            // 
            // cmnuSelectQuery
            // 
            this.cmnuSelectQuery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuSelectQueryCut,
            this.cmnuSelectQueryCopy,
            this.cmnuSelectQueryPaste,
            this.cmnuSelectQuerySelectAll,
            this.toolStripSeparator1,
            this.cmnuSelectQueryUndo,
            this.cmnuSelectQueryRedo});
            this.cmnuSelectQuery.Name = "cmnuSelectQuery";
            this.cmnuSelectQuery.Size = new System.Drawing.Size(123, 142);
            // 
            // cmnuSelectQueryCut
            // 
            this.cmnuSelectQueryCut.Image = ((System.Drawing.Image)(resources.GetObject("cmnuSelectQueryCut.Image")));
            this.cmnuSelectQueryCut.Name = "cmnuSelectQueryCut";
            this.cmnuSelectQueryCut.Size = new System.Drawing.Size(122, 22);
            this.cmnuSelectQueryCut.Text = "Cut";
            this.cmnuSelectQueryCut.Click += new System.EventHandler(this.cmnuSelectQueryCut_Click);
            // 
            // cmnuSelectQueryCopy
            // 
            this.cmnuSelectQueryCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmnuSelectQueryCopy.Image")));
            this.cmnuSelectQueryCopy.Name = "cmnuSelectQueryCopy";
            this.cmnuSelectQueryCopy.Size = new System.Drawing.Size(122, 22);
            this.cmnuSelectQueryCopy.Text = "Copy";
            this.cmnuSelectQueryCopy.Click += new System.EventHandler(this.cmnuSelectQueryCopy_Click);
            // 
            // cmnuSelectQueryPaste
            // 
            this.cmnuSelectQueryPaste.Image = ((System.Drawing.Image)(resources.GetObject("cmnuSelectQueryPaste.Image")));
            this.cmnuSelectQueryPaste.Name = "cmnuSelectQueryPaste";
            this.cmnuSelectQueryPaste.Size = new System.Drawing.Size(122, 22);
            this.cmnuSelectQueryPaste.Text = "Paste";
            this.cmnuSelectQueryPaste.Click += new System.EventHandler(this.cmnuSelectQueryPaste_Click);
            // 
            // cmnuSelectQuerySelectAll
            // 
            this.cmnuSelectQuerySelectAll.Name = "cmnuSelectQuerySelectAll";
            this.cmnuSelectQuerySelectAll.Size = new System.Drawing.Size(122, 22);
            this.cmnuSelectQuerySelectAll.Text = "Select All";
            this.cmnuSelectQuerySelectAll.Click += new System.EventHandler(this.cmnuSelectQuerySelectAll_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(119, 6);
            // 
            // cmnuSelectQueryUndo
            // 
            this.cmnuSelectQueryUndo.Image = ((System.Drawing.Image)(resources.GetObject("cmnuSelectQueryUndo.Image")));
            this.cmnuSelectQueryUndo.Name = "cmnuSelectQueryUndo";
            this.cmnuSelectQueryUndo.Size = new System.Drawing.Size(122, 22);
            this.cmnuSelectQueryUndo.Text = "Undo";
            this.cmnuSelectQueryUndo.Click += new System.EventHandler(this.cmnuSelectQueryUndo_Click);
            // 
            // cmnuSelectQueryRedo
            // 
            this.cmnuSelectQueryRedo.Image = ((System.Drawing.Image)(resources.GetObject("cmnuSelectQueryRedo.Image")));
            this.cmnuSelectQueryRedo.Name = "cmnuSelectQueryRedo";
            this.cmnuSelectQueryRedo.Size = new System.Drawing.Size(122, 22);
            this.cmnuSelectQueryRedo.Text = "Redo";
            this.cmnuSelectQueryRedo.Click += new System.EventHandler(this.cmnuSelectQueryRedo_Click);
            // 
            // txtCmdQuery
            // 
            this.txtCmdQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCmdQuery.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtCmdQuery.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.txtCmdQuery.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.txtCmdQuery.BackBrush = null;
            this.txtCmdQuery.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCmdQuery.CharHeight = 14;
            this.txtCmdQuery.CharWidth = 8;
            this.txtCmdQuery.ContextMenuStrip = this.cmnuCmdQuery;
            this.txtCmdQuery.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCmdQuery.DefaultMarkerSize = 8;
            this.txtCmdQuery.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtCmdQuery.IsReplaceMode = false;
            this.txtCmdQuery.Location = new System.Drawing.Point(12, 81);
            this.txtCmdQuery.Name = "txtCmdQuery";
            this.txtCmdQuery.Paddings = new System.Windows.Forms.Padding(0);
            this.txtCmdQuery.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtCmdQuery.ServiceColors = null;
            this.txtCmdQuery.Size = new System.Drawing.Size(606, 287);
            this.txtCmdQuery.TabIndex = 6;
            this.txtCmdQuery.WordWrap = true;
            this.txtCmdQuery.Zoom = 100;
            this.txtCmdQuery.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtCmdQuery_TextChanged);
            // 
            // cmnuCmdQuery
            // 
            this.cmnuCmdQuery.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuCmdQueryCut,
            this.cmnuCmdQueryCopy,
            this.cmnuCmdQueryPaste,
            this.cmnuCmdQuerySelectAll,
            this.toolStripSeparator2,
            this.cmnuCmdQueryUndo,
            this.cmnuCmdQueryRedo});
            this.cmnuCmdQuery.Name = "cmnuSelectQuery";
            this.cmnuCmdQuery.Size = new System.Drawing.Size(123, 142);
            // 
            // cmnuCmdQueryCut
            // 
            this.cmnuCmdQueryCut.Image = ((System.Drawing.Image)(resources.GetObject("cmnuCmdQueryCut.Image")));
            this.cmnuCmdQueryCut.Name = "cmnuCmdQueryCut";
            this.cmnuCmdQueryCut.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryCut.Text = "Cut";
            this.cmnuCmdQueryCut.Click += new System.EventHandler(this.cmnuCmdQueryCut_Click);
            // 
            // cmnuCmdQueryCopy
            // 
            this.cmnuCmdQueryCopy.Image = ((System.Drawing.Image)(resources.GetObject("cmnuCmdQueryCopy.Image")));
            this.cmnuCmdQueryCopy.Name = "cmnuCmdQueryCopy";
            this.cmnuCmdQueryCopy.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryCopy.Text = "Copy";
            this.cmnuCmdQueryCopy.Click += new System.EventHandler(this.cmnuCmdQueryCopy_Click);
            // 
            // cmnuCmdQueryPaste
            // 
            this.cmnuCmdQueryPaste.Image = ((System.Drawing.Image)(resources.GetObject("cmnuCmdQueryPaste.Image")));
            this.cmnuCmdQueryPaste.Name = "cmnuCmdQueryPaste";
            this.cmnuCmdQueryPaste.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryPaste.Text = "Paste";
            this.cmnuCmdQueryPaste.Click += new System.EventHandler(this.cmnuCmdQueryPaste_Click);
            // 
            // cmnuCmdQuerySelectAll
            // 
            this.cmnuCmdQuerySelectAll.Name = "cmnuCmdQuerySelectAll";
            this.cmnuCmdQuerySelectAll.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQuerySelectAll.Text = "Select All";
            this.cmnuCmdQuerySelectAll.Click += new System.EventHandler(this.cmnuCmdQuerySelectAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
            // 
            // cmnuCmdQueryUndo
            // 
            this.cmnuCmdQueryUndo.Image = ((System.Drawing.Image)(resources.GetObject("cmnuCmdQueryUndo.Image")));
            this.cmnuCmdQueryUndo.Name = "cmnuCmdQueryUndo";
            this.cmnuCmdQueryUndo.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryUndo.Text = "Undo";
            this.cmnuCmdQueryUndo.Click += new System.EventHandler(this.cmnuCmdQueryUndo_Click);
            // 
            // cmnuCmdQueryRedo
            // 
            this.cmnuCmdQueryRedo.Image = ((System.Drawing.Image)(resources.GetObject("cmnuCmdQueryRedo.Image")));
            this.cmnuCmdQueryRedo.Name = "cmnuCmdQueryRedo";
            this.cmnuCmdQueryRedo.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryRedo.Text = "Redo";
            this.cmnuCmdQueryRedo.Click += new System.EventHandler(this.cmnuCmdQueryRedo_Click);
            // 
            // txtHelp
            // 
            this.txtHelp.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtHelp.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.txtHelp.AutoScrollMinSize = new System.Drawing.Size(0, 14);
            this.txtHelp.BackBrush = null;
            this.txtHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHelp.CharHeight = 14;
            this.txtHelp.CharWidth = 8;
            this.txtHelp.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHelp.DefaultMarkerSize = 8;
            this.txtHelp.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHelp.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtHelp.IsReplaceMode = false;
            this.txtHelp.Location = new System.Drawing.Point(0, 0);
            this.txtHelp.Name = "txtHelp";
            this.txtHelp.Paddings = new System.Windows.Forms.Padding(0);
            this.txtHelp.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtHelp.ServiceColors = null;
            this.txtHelp.Size = new System.Drawing.Size(649, 469);
            this.txtHelp.TabIndex = 7;
            this.txtHelp.WordWrap = true;
            this.txtHelp.Zoom = 100;
            // 
            // cbDataSourceType
            // 
            this.cbDataSourceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataSourceType.FormattingEnabled = true;
            this.cbDataSourceType.Items.AddRange(new object[] {
            "<Choose database type>",
            "Microsoft SQL Server",
            "Oracle",
            "PostgreSQL",
            "MySQL",
            "OLE DB",
            "ODBC",
            "Firebird"});
            this.cbDataSourceType.Location = new System.Drawing.Point(15, 22);
            this.cbDataSourceType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbDataSourceType.Name = "cbDataSourceType";
            this.cbDataSourceType.Size = new System.Drawing.Size(272, 23);
            this.cbDataSourceType.TabIndex = 0;
            this.cbDataSourceType.SelectedIndexChanged += new System.EventHandler(this.cbDataSourceType_SelectedIndexChanged);
            // 
            // gbConnection
            // 
            this.gbConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbConnection.Controls.Add(this.txtOptionalOptions);
            this.gbConnection.Controls.Add(this.lblOptionalOptions);
            this.gbConnection.Controls.Add(this.txtPort);
            this.gbConnection.Controls.Add(this.lblPort);
            this.gbConnection.Controls.Add(this.txtConnectionString);
            this.gbConnection.Controls.Add(this.lblConnectionString);
            this.gbConnection.Controls.Add(this.txtPassword);
            this.gbConnection.Controls.Add(this.lblPassword);
            this.gbConnection.Controls.Add(this.txtUser);
            this.gbConnection.Controls.Add(this.lblUser);
            this.gbConnection.Controls.Add(this.txtDatabase);
            this.gbConnection.Controls.Add(this.lblDatabase);
            this.gbConnection.Controls.Add(this.txtServer);
            this.gbConnection.Controls.Add(this.lblServer);
            this.gbConnection.Location = new System.Drawing.Point(7, 75);
            this.gbConnection.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbConnection.Name = "gbConnection";
            this.gbConnection.Padding = new System.Windows.Forms.Padding(12, 3, 12, 12);
            this.gbConnection.Size = new System.Drawing.Size(634, 386);
            this.gbConnection.TabIndex = 1;
            this.gbConnection.TabStop = false;
            this.gbConnection.Text = "Connection";
            // 
            // txtOptionalOptions
            // 
            this.txtOptionalOptions.Location = new System.Drawing.Point(15, 172);
            this.txtOptionalOptions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtOptionalOptions.Name = "txtOptionalOptions";
            this.txtOptionalOptions.Size = new System.Drawing.Size(452, 23);
            this.txtOptionalOptions.TabIndex = 13;
            this.txtOptionalOptions.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblOptionalOptions
            // 
            this.lblOptionalOptions.AutoSize = true;
            this.lblOptionalOptions.Location = new System.Drawing.Point(12, 153);
            this.lblOptionalOptions.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOptionalOptions.Name = "lblOptionalOptions";
            this.lblOptionalOptions.Size = new System.Drawing.Size(98, 15);
            this.lblOptionalOptions.TabIndex = 12;
            this.lblOptionalOptions.Text = "Optional Options";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(245, 37);
            this.txtPort.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(222, 23);
            this.txtPort.TabIndex = 11;
            this.txtPort.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblPort
            // 
            this.lblPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(241, 18);
            this.lblPort.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 15);
            this.lblPort.TabIndex = 10;
            this.lblPort.Text = "Port";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConnectionString.Location = new System.Drawing.Point(15, 217);
            this.txtConnectionString.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtConnectionString.Multiline = true;
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(603, 153);
            this.txtConnectionString.TabIndex = 9;
            this.txtConnectionString.TextChanged += new System.EventHandler(this.txtConnectionString_TextChanged);
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(12, 198);
            this.lblConnectionString.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(102, 15);
            this.lblConnectionString.TabIndex = 8;
            this.lblConnectionString.Text = "Connection string";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(245, 127);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(222, 23);
            this.txtPassword.TabIndex = 7;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(241, 108);
            this.lblPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(15, 127);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(222, 23);
            this.txtUser.TabIndex = 5;
            this.txtUser.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(12, 108);
            this.lblUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(30, 15);
            this.lblUser.TabIndex = 4;
            this.lblUser.Text = "User";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(15, 82);
            this.txtDatabase.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(452, 23);
            this.txtDatabase.TabIndex = 3;
            this.txtDatabase.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(12, 63);
            this.lblDatabase.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(55, 15);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Database";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(15, 37);
            this.txtServer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(222, 23);
            this.txtServer.TabIndex = 1;
            this.txtServer.TextChanged += new System.EventHandler(this.txtConnProp_TextChanged);
            // 
            // lblServer
            // 
            this.lblServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(12, 18);
            this.lblServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(39, 15);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.pageDatabase);
            this.tabControl.Controls.Add(this.pageQuery);
            this.tabControl.Controls.Add(this.pageData);
            this.tabControl.Controls.Add(this.pageCommands);
            this.tabControl.Controls.Add(this.pageSettings);
            this.tabControl.Controls.Add(this.pageHelp);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(657, 497);
            this.tabControl.TabIndex = 0;
            // 
            // pageDatabase
            // 
            this.pageDatabase.Controls.Add(this.gbDataSourceType);
            this.pageDatabase.Controls.Add(this.gbConnection);
            this.pageDatabase.Location = new System.Drawing.Point(4, 24);
            this.pageDatabase.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageDatabase.Name = "pageDatabase";
            this.pageDatabase.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageDatabase.Size = new System.Drawing.Size(649, 469);
            this.pageDatabase.TabIndex = 0;
            this.pageDatabase.Text = "Database";
            this.pageDatabase.UseVisualStyleBackColor = true;
            // 
            // gbDataSourceType
            // 
            this.gbDataSourceType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDataSourceType.Controls.Add(this.btnConnectionTest);
            this.gbDataSourceType.Controls.Add(this.cbDataSourceType);
            this.gbDataSourceType.Location = new System.Drawing.Point(7, 7);
            this.gbDataSourceType.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbDataSourceType.Name = "gbDataSourceType";
            this.gbDataSourceType.Padding = new System.Windows.Forms.Padding(12, 3, 12, 12);
            this.gbDataSourceType.Size = new System.Drawing.Size(634, 61);
            this.gbDataSourceType.TabIndex = 0;
            this.gbDataSourceType.TabStop = false;
            this.gbDataSourceType.Text = "Data Source Type";
            // 
            // btnConnectionTest
            // 
            this.btnConnectionTest.Location = new System.Drawing.Point(298, 21);
            this.btnConnectionTest.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnConnectionTest.Name = "btnConnectionTest";
            this.btnConnectionTest.Size = new System.Drawing.Size(170, 27);
            this.btnConnectionTest.TabIndex = 3;
            this.btnConnectionTest.Text = "Testing connection...";
            this.btnConnectionTest.UseVisualStyleBackColor = true;
            this.btnConnectionTest.Click += new System.EventHandler(this.btnConnectionTest_Click);
            // 
            // pageQuery
            // 
            this.pageQuery.Controls.Add(this.lblSelectQuery);
            this.pageQuery.Controls.Add(this.txtSelectQuery);
            this.pageQuery.Location = new System.Drawing.Point(4, 24);
            this.pageQuery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageQuery.Name = "pageQuery";
            this.pageQuery.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageQuery.Size = new System.Drawing.Size(649, 469);
            this.pageQuery.TabIndex = 1;
            this.pageQuery.Text = "Data Retrieval";
            this.pageQuery.UseVisualStyleBackColor = true;
            // 
            // lblSelectQuery
            // 
            this.lblSelectQuery.AutoSize = true;
            this.lblSelectQuery.Location = new System.Drawing.Point(4, 5);
            this.lblSelectQuery.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectQuery.Name = "lblSelectQuery";
            this.lblSelectQuery.Size = new System.Drawing.Size(28, 15);
            this.lblSelectQuery.TabIndex = 0;
            this.lblSelectQuery.Text = "SQL";
            // 
            // pageData
            // 
            this.pageData.Controls.Add(this.tlpPanel);
            this.pageData.Location = new System.Drawing.Point(4, 24);
            this.pageData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageData.Name = "pageData";
            this.pageData.Size = new System.Drawing.Size(649, 469);
            this.pageData.TabIndex = 4;
            this.pageData.Text = "Data";
            this.pageData.UseVisualStyleBackColor = true;
            // 
            // tlpPanel
            // 
            this.tlpPanel.ColumnCount = 1;
            this.tlpPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPanel.Controls.Add(this.dgvData, 0, 0);
            this.tlpPanel.Controls.Add(this.btnExecuteSQLQuery, 0, 1);
            this.tlpPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPanel.Location = new System.Drawing.Point(0, 0);
            this.tlpPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tlpPanel.Name = "tlpPanel";
            this.tlpPanel.RowCount = 2;
            this.tlpPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tlpPanel.Size = new System.Drawing.Size(649, 469);
            this.tlpPanel.TabIndex = 10;
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvData.Location = new System.Drawing.Point(4, 3);
            this.dgvData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.Size = new System.Drawing.Size(641, 428);
            this.dgvData.TabIndex = 8;
            this.dgvData.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvData_DataError);
            // 
            // btnExecuteSQLQuery
            // 
            this.btnExecuteSQLQuery.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExecuteSQLQuery.Location = new System.Drawing.Point(4, 437);
            this.btnExecuteSQLQuery.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnExecuteSQLQuery.Name = "btnExecuteSQLQuery";
            this.btnExecuteSQLQuery.Size = new System.Drawing.Size(641, 27);
            this.btnExecuteSQLQuery.TabIndex = 9;
            this.btnExecuteSQLQuery.Text = "Execute SQL query";
            this.btnExecuteSQLQuery.UseVisualStyleBackColor = true;
            this.btnExecuteSQLQuery.Click += new System.EventHandler(this.btnExecuteSQLQuery_Click);
            // 
            // pageCommands
            // 
            this.pageCommands.Controls.Add(this.gbCommandParams);
            this.pageCommands.Controls.Add(this.gbCommand);
            this.pageCommands.Location = new System.Drawing.Point(4, 24);
            this.pageCommands.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageCommands.Name = "pageCommands";
            this.pageCommands.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageCommands.Size = new System.Drawing.Size(649, 469);
            this.pageCommands.TabIndex = 2;
            this.pageCommands.Text = "Commands";
            this.pageCommands.UseVisualStyleBackColor = true;
            // 
            // gbCommandParams
            // 
            this.gbCommandParams.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCommandParams.Controls.Add(this.lblCmdCode);
            this.gbCommandParams.Controls.Add(this.txtCmdCode);
            this.gbCommandParams.Controls.Add(this.lblCmdQuery);
            this.gbCommandParams.Controls.Add(this.txtName);
            this.gbCommandParams.Controls.Add(this.lblName);
            this.gbCommandParams.Controls.Add(this.numCmdNum);
            this.gbCommandParams.Controls.Add(this.lblCmdNum);
            this.gbCommandParams.Controls.Add(this.txtCmdQuery);
            this.gbCommandParams.Location = new System.Drawing.Point(7, 77);
            this.gbCommandParams.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbCommandParams.Name = "gbCommandParams";
            this.gbCommandParams.Padding = new System.Windows.Forms.Padding(12, 3, 12, 12);
            this.gbCommandParams.Size = new System.Drawing.Size(634, 383);
            this.gbCommandParams.TabIndex = 1;
            this.gbCommandParams.TabStop = false;
            this.gbCommandParams.Text = "Command Parameters";
            // 
            // lblCmdCode
            // 
            this.lblCmdCode.AutoSize = true;
            this.lblCmdCode.Location = new System.Drawing.Point(127, 18);
            this.lblCmdCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCmdCode.Name = "lblCmdCode";
            this.lblCmdCode.Size = new System.Drawing.Size(93, 15);
            this.lblCmdCode.TabIndex = 8;
            this.lblCmdCode.Text = "Command code";
            // 
            // txtCmdCode
            // 
            this.txtCmdCode.Location = new System.Drawing.Point(127, 37);
            this.txtCmdCode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtCmdCode.Name = "txtCmdCode";
            this.txtCmdCode.Size = new System.Drawing.Size(106, 23);
            this.txtCmdCode.TabIndex = 7;
            this.txtCmdCode.TextChanged += new System.EventHandler(this.txtCmdCode_TextChanged);
            // 
            // lblCmdQuery
            // 
            this.lblCmdQuery.AutoSize = true;
            this.lblCmdQuery.Location = new System.Drawing.Point(12, 63);
            this.lblCmdQuery.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCmdQuery.Name = "lblCmdQuery";
            this.lblCmdQuery.Size = new System.Drawing.Size(28, 15);
            this.lblCmdQuery.TabIndex = 4;
            this.lblCmdQuery.Text = "SQL";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(241, 37);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(226, 23);
            this.txtName.TabIndex = 3;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(241, 18);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "Name";
            // 
            // numCmdNum
            // 
            this.numCmdNum.Location = new System.Drawing.Point(15, 37);
            this.numCmdNum.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.numCmdNum.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numCmdNum.Name = "numCmdNum";
            this.numCmdNum.Size = new System.Drawing.Size(106, 23);
            this.numCmdNum.TabIndex = 1;
            this.numCmdNum.ValueChanged += new System.EventHandler(this.numCmdNum_ValueChanged);
            // 
            // lblCmdNum
            // 
            this.lblCmdNum.AutoSize = true;
            this.lblCmdNum.Location = new System.Drawing.Point(12, 18);
            this.lblCmdNum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCmdNum.Name = "lblCmdNum";
            this.lblCmdNum.Size = new System.Drawing.Size(109, 15);
            this.lblCmdNum.TabIndex = 0;
            this.lblCmdNum.Text = "Command number";
            // 
            // gbCommand
            // 
            this.gbCommand.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCommand.Controls.Add(this.cbCommand);
            this.gbCommand.Controls.Add(this.btnDeleteCommand);
            this.gbCommand.Controls.Add(this.btnCreateCommand);
            this.gbCommand.Location = new System.Drawing.Point(7, 7);
            this.gbCommand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gbCommand.Name = "gbCommand";
            this.gbCommand.Padding = new System.Windows.Forms.Padding(12, 3, 12, 12);
            this.gbCommand.Size = new System.Drawing.Size(634, 63);
            this.gbCommand.TabIndex = 0;
            this.gbCommand.TabStop = false;
            this.gbCommand.Text = "Command";
            // 
            // cbCommand
            // 
            this.cbCommand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCommand.FormattingEnabled = true;
            this.cbCommand.Location = new System.Drawing.Point(15, 23);
            this.cbCommand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbCommand.Name = "cbCommand";
            this.cbCommand.Size = new System.Drawing.Size(263, 23);
            this.cbCommand.TabIndex = 0;
            this.cbCommand.SelectedIndexChanged += new System.EventHandler(this.cbCommand_SelectedIndexChanged);
            // 
            // btnDeleteCommand
            // 
            this.btnDeleteCommand.Location = new System.Drawing.Point(380, 22);
            this.btnDeleteCommand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnDeleteCommand.Name = "btnDeleteCommand";
            this.btnDeleteCommand.Size = new System.Drawing.Size(88, 27);
            this.btnDeleteCommand.TabIndex = 2;
            this.btnDeleteCommand.Text = "Delete";
            this.btnDeleteCommand.UseVisualStyleBackColor = true;
            this.btnDeleteCommand.Click += new System.EventHandler(this.btnDeleteCommand_Click);
            // 
            // btnCreateCommand
            // 
            this.btnCreateCommand.Location = new System.Drawing.Point(286, 22);
            this.btnCreateCommand.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnCreateCommand.Name = "btnCreateCommand";
            this.btnCreateCommand.Size = new System.Drawing.Size(88, 27);
            this.btnCreateCommand.TabIndex = 1;
            this.btnCreateCommand.Text = "Create";
            this.btnCreateCommand.UseVisualStyleBackColor = true;
            this.btnCreateCommand.Click += new System.EventHandler(this.btnCreateCommand_Click);
            // 
            // pageSettings
            // 
            this.pageSettings.Controls.Add(this.gpbTags);
            this.pageSettings.Controls.Add(this.gpbTagFormatDatabase);
            this.pageSettings.Location = new System.Drawing.Point(4, 24);
            this.pageSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageSettings.Name = "pageSettings";
            this.pageSettings.Size = new System.Drawing.Size(649, 469);
            this.pageSettings.TabIndex = 3;
            this.pageSettings.Text = "Settings";
            this.pageSettings.UseVisualStyleBackColor = true;
            // 
            // gpbTags
            // 
            this.gpbTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbTags.Controls.Add(this.tlpTags);
            this.gpbTags.Location = new System.Drawing.Point(7, 90);
            this.gpbTags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gpbTags.Name = "gpbTags";
            this.gpbTags.Padding = new System.Windows.Forms.Padding(12, 3, 12, 12);
            this.gpbTags.Size = new System.Drawing.Size(632, 367);
            this.gpbTags.TabIndex = 12;
            this.gpbTags.TabStop = false;
            this.gpbTags.Text = "Tags";
            // 
            // tlpTags
            // 
            this.tlpTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpTags.ColumnCount = 1;
            this.tlpTags.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.5955F));
            this.tlpTags.Controls.Add(this.lstTags, 0, 0);
            this.tlpTags.Location = new System.Drawing.Point(14, 22);
            this.tlpTags.Name = "tlpTags";
            this.tlpTags.RowCount = 1;
            this.tlpTags.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpTags.Size = new System.Drawing.Size(603, 330);
            this.tlpTags.TabIndex = 1;
            // 
            // lstTags
            // 
            this.lstTags.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lstTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmTagname,
            this.clmTagCode,
            this.clmTagEnabled});
            this.lstTags.ContextMenuStrip = this.cmnuLstTags;
            this.lstTags.FullRowSelect = true;
            this.lstTags.GridLines = true;
            this.lstTags.Location = new System.Drawing.Point(3, 3);
            this.lstTags.MultiSelect = false;
            this.lstTags.Name = "lstTags";
            this.lstTags.Size = new System.Drawing.Size(597, 324);
            this.lstTags.TabIndex = 2;
            this.lstTags.UseCompatibleStateImageBehavior = false;
            this.lstTags.View = System.Windows.Forms.View.Details;
            this.lstTags.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstTags_KeyDown);
            this.lstTags.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstTags_MouseClick);
            this.lstTags.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstTags_MouseDoubleClick);
            // 
            // clmTagname
            // 
            this.clmTagname.Text = "Name";
            this.clmTagname.Width = 200;
            // 
            // clmTagCode
            // 
            this.clmTagCode.Text = "Tag code";
            this.clmTagCode.Width = 200;
            // 
            // clmTagEnabled
            // 
            this.clmTagEnabled.Text = "Enabled";
            this.clmTagEnabled.Width = 80;
            // 
            // cmnuLstTags
            // 
            this.cmnuLstTags.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmnuTagRefresh,
            this.toolStripSeparator6,
            this.cmnuTagAdd,
            this.cmnuListTagAdd,
            this.toolStripSeparator3,
            this.cmnuTagChange,
            this.toolStripSeparator4,
            this.cmnuTagDelete,
            this.cmnuTagAllDelete,
            this.toolStripSeparator5,
            this.cmnuUp,
            this.cmnuDown});
            this.cmnuLstTags.Name = "cmnuSelectQuery";
            this.cmnuLstTags.Size = new System.Drawing.Size(181, 226);
            // 
            // cmnuTagRefresh
            // 
            this.cmnuTagRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagRefresh.Image")));
            this.cmnuTagRefresh.Name = "cmnuTagRefresh";
            this.cmnuTagRefresh.Size = new System.Drawing.Size(180, 22);
            this.cmnuTagRefresh.Text = "Refresh";
            this.cmnuTagRefresh.Click += new System.EventHandler(this.cmnuTagRefresh_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(177, 6);
            // 
            // cmnuTagAdd
            // 
            this.cmnuTagAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagAdd.Image")));
            this.cmnuTagAdd.Name = "cmnuTagAdd";
            this.cmnuTagAdd.Size = new System.Drawing.Size(180, 22);
            this.cmnuTagAdd.Text = "Add Tag";
            this.cmnuTagAdd.Click += new System.EventHandler(this.cmnuTagAdd_Click);
            // 
            // cmnuListTagAdd
            // 
            this.cmnuListTagAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmnuListTagAdd.Image")));
            this.cmnuListTagAdd.Name = "cmnuListTagAdd";
            this.cmnuListTagAdd.Size = new System.Drawing.Size(180, 22);
            this.cmnuListTagAdd.Text = "Add list of Tags";
            this.cmnuListTagAdd.Click += new System.EventHandler(this.cmnuListTagAdd_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // cmnuTagChange
            // 
            this.cmnuTagChange.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagChange.Image")));
            this.cmnuTagChange.Name = "cmnuTagChange";
            this.cmnuTagChange.Size = new System.Drawing.Size(180, 22);
            this.cmnuTagChange.Text = "Change Tag";
            this.cmnuTagChange.Click += new System.EventHandler(this.cmnuTagChange_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(177, 6);
            // 
            // cmnuTagDelete
            // 
            this.cmnuTagDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagDelete.Image")));
            this.cmnuTagDelete.Name = "cmnuTagDelete";
            this.cmnuTagDelete.Size = new System.Drawing.Size(180, 22);
            this.cmnuTagDelete.Text = "Delete Tag";
            this.cmnuTagDelete.Click += new System.EventHandler(this.cmnuTagDelete_Click);
            // 
            // cmnuTagAllDelete
            // 
            this.cmnuTagAllDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagAllDelete.Image")));
            this.cmnuTagAllDelete.Name = "cmnuTagAllDelete";
            this.cmnuTagAllDelete.Size = new System.Drawing.Size(180, 22);
            this.cmnuTagAllDelete.Text = "Delete all Tags";
            this.cmnuTagAllDelete.Click += new System.EventHandler(this.cmnuTagAllDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // cmnuUp
            // 
            this.cmnuUp.Image = ((System.Drawing.Image)(resources.GetObject("cmnuUp.Image")));
            this.cmnuUp.Name = "cmnuUp";
            this.cmnuUp.Size = new System.Drawing.Size(180, 22);
            this.cmnuUp.Text = "Up";
            this.cmnuUp.Click += new System.EventHandler(this.cmnuUp_Click);
            // 
            // cmnuDown
            // 
            this.cmnuDown.Image = ((System.Drawing.Image)(resources.GetObject("cmnuDown.Image")));
            this.cmnuDown.Name = "cmnuDown";
            this.cmnuDown.Size = new System.Drawing.Size(180, 22);
            this.cmnuDown.Text = "Down";
            this.cmnuDown.Click += new System.EventHandler(this.cmnuDown_Click);
            // 
            // gpbTagFormatDatabase
            // 
            this.gpbTagFormatDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbTagFormatDatabase.Controls.Add(this.rdbKPTagsBasedRequestedTableRows);
            this.gpbTagFormatDatabase.Controls.Add(this.rdbKPTagsBasedRequestedTableColumns);
            this.gpbTagFormatDatabase.Location = new System.Drawing.Point(7, 7);
            this.gpbTagFormatDatabase.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gpbTagFormatDatabase.Name = "gpbTagFormatDatabase";
            this.gpbTagFormatDatabase.Padding = new System.Windows.Forms.Padding(12, 3, 12, 12);
            this.gpbTagFormatDatabase.Size = new System.Drawing.Size(632, 77);
            this.gpbTagFormatDatabase.TabIndex = 7;
            this.gpbTagFormatDatabase.TabStop = false;
            this.gpbTagFormatDatabase.Text = "Format of Tags from the Database";
            // 
            // rdbKPTagsBasedRequestedTableRows
            // 
            this.rdbKPTagsBasedRequestedTableRows.AutoSize = true;
            this.rdbKPTagsBasedRequestedTableRows.Location = new System.Drawing.Point(15, 48);
            this.rdbKPTagsBasedRequestedTableRows.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdbKPTagsBasedRequestedTableRows.Name = "rdbKPTagsBasedRequestedTableRows";
            this.rdbKPTagsBasedRequestedTableRows.Size = new System.Drawing.Size(290, 19);
            this.rdbKPTagsBasedRequestedTableRows.TabIndex = 1;
            this.rdbKPTagsBasedRequestedTableRows.TabStop = true;
            this.rdbKPTagsBasedRequestedTableRows.Text = "KP Tags Based on the List of Requested Table Rows";
            this.rdbKPTagsBasedRequestedTableRows.UseVisualStyleBackColor = true;
            this.rdbKPTagsBasedRequestedTableRows.CheckedChanged += new System.EventHandler(this.rdbKPTagsBasedRequestedTableRows_CheckedChanged);
            // 
            // rdbKPTagsBasedRequestedTableColumns
            // 
            this.rdbKPTagsBasedRequestedTableColumns.AutoSize = true;
            this.rdbKPTagsBasedRequestedTableColumns.Location = new System.Drawing.Point(15, 22);
            this.rdbKPTagsBasedRequestedTableColumns.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rdbKPTagsBasedRequestedTableColumns.Name = "rdbKPTagsBasedRequestedTableColumns";
            this.rdbKPTagsBasedRequestedTableColumns.Size = new System.Drawing.Size(310, 19);
            this.rdbKPTagsBasedRequestedTableColumns.TabIndex = 0;
            this.rdbKPTagsBasedRequestedTableColumns.TabStop = true;
            this.rdbKPTagsBasedRequestedTableColumns.Text = "KP Tags Based on the List of Requested Table Columns";
            this.rdbKPTagsBasedRequestedTableColumns.UseVisualStyleBackColor = true;
            this.rdbKPTagsBasedRequestedTableColumns.CheckedChanged += new System.EventHandler(this.rdbKPTagsBasedRequestedTableColumns_CheckedChanged);
            // 
            // pageHelp
            // 
            this.pageHelp.Controls.Add(this.txtHelp);
            this.pageHelp.Location = new System.Drawing.Point(4, 24);
            this.pageHelp.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageHelp.Name = "pageHelp";
            this.pageHelp.Size = new System.Drawing.Size(649, 469);
            this.pageHelp.TabIndex = 5;
            this.pageHelp.Text = "Help";
            this.pageHelp.UseVisualStyleBackColor = true;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnClose);
            this.pnlBottom.Controls.Add(this.btnSave);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 497);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(657, 47);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(556, 7);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(88, 27);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(461, 7);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 27);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(657, 544);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlBottom);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(522, 525);
            this.Name = "FrmConfig";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DB Import Plus - Device {0} Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtSelectQuery)).EndInit();
            this.cmnuSelectQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtCmdQuery)).EndInit();
            this.cmnuCmdQuery.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtHelp)).EndInit();
            this.gbConnection.ResumeLayout(false);
            this.gbConnection.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.pageDatabase.ResumeLayout(false);
            this.gbDataSourceType.ResumeLayout(false);
            this.pageQuery.ResumeLayout(false);
            this.pageQuery.PerformLayout();
            this.pageData.ResumeLayout(false);
            this.tlpPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.pageCommands.ResumeLayout(false);
            this.gbCommandParams.ResumeLayout(false);
            this.gbCommandParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCmdNum)).EndInit();
            this.gbCommand.ResumeLayout(false);
            this.pageSettings.ResumeLayout(false);
            this.gpbTags.ResumeLayout(false);
            this.tlpTags.ResumeLayout(false);
            this.cmnuLstTags.ResumeLayout(false);
            this.gpbTagFormatDatabase.ResumeLayout(false);
            this.gpbTagFormatDatabase.PerformLayout();
            this.pageHelp.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cbDataSourceType;
        private System.Windows.Forms.GroupBox gbConnection;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage pageDatabase;
        private System.Windows.Forms.TabPage pageQuery;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox gbDataSourceType;
        private System.Windows.Forms.Label lblSelectQuery;
        private System.Windows.Forms.TabPage pageCommands;
        private System.Windows.Forms.ComboBox cbCommand;
        private System.Windows.Forms.GroupBox gbCommand;
        private System.Windows.Forms.Button btnDeleteCommand;
        private System.Windows.Forms.Button btnCreateCommand;
        private System.Windows.Forms.GroupBox gbCommandParams;
        private System.Windows.Forms.Label lblCmdQuery;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.NumericUpDown numCmdNum;
        private System.Windows.Forms.Label lblCmdNum;
        private FastColoredTextBoxNS.FastColoredTextBox txtSelectQuery;
        private System.Windows.Forms.TextBox txtOptionalOptions;
        private System.Windows.Forms.Label lblOptionalOptions;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnConnectionTest;
        private FastColoredTextBoxNS.FastColoredTextBox txtCmdQuery;
        private System.Windows.Forms.TabPage pageSettings;
        private System.Windows.Forms.GroupBox gpbTagFormatDatabase;
        private System.Windows.Forms.RadioButton rdbKPTagsBasedRequestedTableRows;
        private System.Windows.Forms.RadioButton rdbKPTagsBasedRequestedTableColumns;
        private System.Windows.Forms.TabPage pageData;
        private System.Windows.Forms.TableLayoutPanel tlpPanel;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnExecuteSQLQuery;
        private System.Windows.Forms.TabPage pageHelp;
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
    }
}