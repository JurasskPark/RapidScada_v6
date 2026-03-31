namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    partial class FrmImportCmd
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
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportCmd));
            gbCommandParams = new GroupBox();
            lblCmdEnabled = new Label();
            ckbEnabled = new CheckBox();
            tabControlCommand = new TabControl();
            pageData = new TabPage();
            tlpPanel = new TableLayoutPanel();
            btnExecuteSQLQuery = new Button();
            dgvData = new DataGridView();
            lblData = new Label();
            fctCmdQuery = new FastColoredTextBoxNS.FastColoredTextBox();
            cmnuMenuScriptQuery = new ContextMenuStrip(components);
            cmnuScriptQueryCut = new ToolStripMenuItem();
            cmnuScriptQueryCopy = new ToolStripMenuItem();
            cmnuScriptQueryPaste = new ToolStripMenuItem();
            cmnuScriptQuerySelectAll = new ToolStripMenuItem();
            cmnuSeparator01 = new ToolStripSeparator();
            cmnuScriptQueryUndo = new ToolStripMenuItem();
            cmnuScriptQueryRedo = new ToolStripMenuItem();
            lblCmdQuery = new Label();
            pageTags = new TabPage();
            gpbTags = new GroupBox();
            tlpTags = new TableLayoutPanel();
            lstTags = new ListView();
            clmName = new ColumnHeader();
            clmCode = new ColumnHeader();
            clmFormat = new ColumnHeader();
            clmEnabled = new ColumnHeader();
            cmnuMenuListTags = new ContextMenuStrip(components);
            cmnuTagAdd = new ToolStripMenuItem();
            cmnuTagListAdd = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            cmnuTagChange = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            cmnuTagDelete = new ToolStripMenuItem();
            cmnuTagListDelete = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            cmnuTagUp = new ToolStripMenuItem();
            cmnuTagDown = new ToolStripMenuItem();
            gpbTagFormatDatabase = new GroupBox();
            rdbTagsBasedRequestedTableRows = new RadioButton();
            rdbTagsBasedRequestedTableColumns = new RadioButton();
            pageResult = new TabPage();
            fctResult = new FastColoredTextBoxNS.FastColoredTextBox();
            txtCmdDescription = new TextBox();
            lblDescription = new Label();
            lblCmdStringLenght = new Label();
            nudCmdStringLenght = new NumericUpDown();
            lblCmdCode = new Label();
            txtCmdCode = new TextBox();
            txtCmdName = new TextBox();
            lblCmdName = new Label();
            nudCmdNum = new NumericUpDown();
            lblCmdNum = new Label();
            pnlBottom = new Panel();
            btnClose = new Button();
            btnSave = new Button();
            gbCommandParams.SuspendLayout();
            tabControlCommand.SuspendLayout();
            pageData.SuspendLayout();
            tlpPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).BeginInit();
            ((System.ComponentModel.ISupportInitialize)fctCmdQuery).BeginInit();
            cmnuMenuScriptQuery.SuspendLayout();
            pageTags.SuspendLayout();
            gpbTags.SuspendLayout();
            tlpTags.SuspendLayout();
            cmnuMenuListTags.SuspendLayout();
            gpbTagFormatDatabase.SuspendLayout();
            pageResult.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)fctResult).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCmdStringLenght).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCmdNum).BeginInit();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // gbCommandParams
            // 
            gbCommandParams.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbCommandParams.Controls.Add(lblCmdEnabled);
            gbCommandParams.Controls.Add(ckbEnabled);
            gbCommandParams.Controls.Add(tabControlCommand);
            gbCommandParams.Controls.Add(txtCmdDescription);
            gbCommandParams.Controls.Add(lblDescription);
            gbCommandParams.Controls.Add(lblCmdStringLenght);
            gbCommandParams.Controls.Add(nudCmdStringLenght);
            gbCommandParams.Controls.Add(lblCmdCode);
            gbCommandParams.Controls.Add(txtCmdCode);
            gbCommandParams.Controls.Add(txtCmdName);
            gbCommandParams.Controls.Add(lblCmdName);
            gbCommandParams.Controls.Add(nudCmdNum);
            gbCommandParams.Controls.Add(lblCmdNum);
            gbCommandParams.Location = new Point(0, 2);
            gbCommandParams.Margin = new Padding(6, 5, 6, 5);
            gbCommandParams.Name = "gbCommandParams";
            gbCommandParams.Padding = new Padding(17, 5, 17, 20);
            gbCommandParams.Size = new Size(1177, 858);
            gbCommandParams.TabIndex = 3;
            gbCommandParams.TabStop = false;
            gbCommandParams.Text = "Command Parameters";
            // 
            // lblCmdEnabled
            // 
            lblCmdEnabled.AutoSize = true;
            lblCmdEnabled.Location = new Point(23, 28);
            lblCmdEnabled.Margin = new Padding(6, 0, 6, 0);
            lblCmdEnabled.Name = "lblCmdEnabled";
            lblCmdEnabled.Size = new Size(75, 25);
            lblCmdEnabled.TabIndex = 15;
            lblCmdEnabled.Text = "Enabled";
            // 
            // ckbEnabled
            // 
            ckbEnabled.AutoSize = true;
            ckbEnabled.Location = new Point(23, 68);
            ckbEnabled.Name = "ckbEnabled";
            ckbEnabled.Size = new Size(22, 21);
            ckbEnabled.TabIndex = 14;
            ckbEnabled.UseVisualStyleBackColor = true;
            // 
            // tabControlCommand
            // 
            tabControlCommand.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlCommand.Controls.Add(pageData);
            tabControlCommand.Controls.Add(pageTags);
            tabControlCommand.Controls.Add(pageResult);
            tabControlCommand.Location = new Point(23, 345);
            tabControlCommand.Name = "tabControlCommand";
            tabControlCommand.SelectedIndex = 0;
            tabControlCommand.Size = new Size(1130, 492);
            tabControlCommand.TabIndex = 13;
            // 
            // pageData
            // 
            pageData.Controls.Add(tlpPanel);
            pageData.Location = new Point(4, 34);
            pageData.Name = "pageData";
            pageData.Padding = new Padding(3);
            pageData.Size = new Size(1122, 454);
            pageData.TabIndex = 0;
            pageData.Text = "Data";
            pageData.UseVisualStyleBackColor = true;
            // 
            // tlpPanel
            // 
            tlpPanel.ColumnCount = 1;
            tlpPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPanel.Controls.Add(btnExecuteSQLQuery, 0, 4);
            tlpPanel.Controls.Add(dgvData, 0, 1);
            tlpPanel.Controls.Add(lblData, 0, 0);
            tlpPanel.Controls.Add(fctCmdQuery, 0, 3);
            tlpPanel.Controls.Add(lblCmdQuery, 0, 2);
            tlpPanel.Dock = DockStyle.Fill;
            tlpPanel.Location = new Point(3, 3);
            tlpPanel.Name = "tlpPanel";
            tlpPanel.RowCount = 5;
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 27F));
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tlpPanel.Size = new Size(1116, 448);
            tlpPanel.TabIndex = 0;
            // 
            // btnExecuteSQLQuery
            // 
            btnExecuteSQLQuery.Dock = DockStyle.Fill;
            btnExecuteSQLQuery.Location = new Point(6, 403);
            btnExecuteSQLQuery.Margin = new Padding(6, 5, 6, 5);
            btnExecuteSQLQuery.Name = "btnExecuteSQLQuery";
            btnExecuteSQLQuery.Size = new Size(1104, 40);
            btnExecuteSQLQuery.TabIndex = 16;
            btnExecuteSQLQuery.Text = "Execute SQL query";
            btnExecuteSQLQuery.UseVisualStyleBackColor = true;
            btnExecuteSQLQuery.Click += btnExecuteSQLQuery_Click;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Window;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle4;
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(6, 32);
            dgvData.Margin = new Padding(6, 5, 6, 5);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 62;
            dgvData.Size = new Size(1104, 162);
            dgvData.TabIndex = 15;
            dgvData.DataError += dgvData_DataError;
            // 
            // lblData
            // 
            lblData.AutoSize = true;
            lblData.Location = new Point(3, 0);
            lblData.Name = "lblData";
            lblData.Size = new Size(49, 25);
            lblData.TabIndex = 14;
            lblData.Text = "Data";
            // 
            // fctCmdQuery
            // 
            fctCmdQuery.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            fctCmdQuery.AutoCompleteBracketsList = new char[]
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
            fctCmdQuery.AutoIndentCharsPatterns = "";
            fctCmdQuery.AutoScrollMinSize = new Size(0, 22);
            fctCmdQuery.BackBrush = null;
            fctCmdQuery.BorderStyle = BorderStyle.FixedSingle;
            fctCmdQuery.CharHeight = 22;
            fctCmdQuery.CharWidth = 12;
            fctCmdQuery.CommentPrefix = "--";
            fctCmdQuery.ContextMenuStrip = cmnuMenuScriptQuery;
            fctCmdQuery.Cursor = Cursors.IBeam;
            fctCmdQuery.DefaultMarkerSize = 8;
            fctCmdQuery.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            fctCmdQuery.Font = new Font("Courier New", 9.75F);
            fctCmdQuery.Hotkeys = resources.GetString("fctCmdQuery.Hotkeys");
            fctCmdQuery.IsReplaceMode = false;
            fctCmdQuery.Language = FastColoredTextBoxNS.Language.SQL;
            fctCmdQuery.LeftBracket = '(';
            fctCmdQuery.Location = new Point(4, 231);
            fctCmdQuery.Margin = new Padding(4, 5, 4, 5);
            fctCmdQuery.Name = "fctCmdQuery";
            fctCmdQuery.Paddings = new Padding(0);
            fctCmdQuery.RightBracket = ')';
            fctCmdQuery.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            fctCmdQuery.ServiceColors = null;
            fctCmdQuery.Size = new Size(1108, 162);
            fctCmdQuery.TabIndex = 6;
            fctCmdQuery.WordWrap = true;
            fctCmdQuery.Zoom = 100;
            // 
            // cmnuMenuScriptQuery
            // 
            cmnuMenuScriptQuery.ImageScalingSize = new Size(24, 24);
            cmnuMenuScriptQuery.Items.AddRange(new ToolStripItem[] { cmnuScriptQueryCut, cmnuScriptQueryCopy, cmnuScriptQueryPaste, cmnuScriptQuerySelectAll, cmnuSeparator01, cmnuScriptQueryUndo, cmnuScriptQueryRedo });
            cmnuMenuScriptQuery.Name = "cmnuSelectQuery";
            cmnuMenuScriptQuery.Size = new Size(227, 202);
            // 
            // cmnuScriptQueryCut
            // 
            cmnuScriptQueryCut.Image = (Image)resources.GetObject("cmnuScriptQueryCut.Image");
            cmnuScriptQueryCut.Name = "cmnuScriptQueryCut";
            cmnuScriptQueryCut.ShortcutKeys = Keys.Control | Keys.X;
            cmnuScriptQueryCut.Size = new Size(226, 32);
            cmnuScriptQueryCut.Text = "Cut";
            cmnuScriptQueryCut.Click += cmnuScriptQueryCut_Click;
            // 
            // cmnuScriptQueryCopy
            // 
            cmnuScriptQueryCopy.Image = (Image)resources.GetObject("cmnuScriptQueryCopy.Image");
            cmnuScriptQueryCopy.Name = "cmnuScriptQueryCopy";
            cmnuScriptQueryCopy.ShortcutKeys = Keys.Control | Keys.C;
            cmnuScriptQueryCopy.Size = new Size(226, 32);
            cmnuScriptQueryCopy.Text = "Copy";
            cmnuScriptQueryCopy.Click += cmnuScriptQueryCopy_Click;
            // 
            // cmnuScriptQueryPaste
            // 
            cmnuScriptQueryPaste.Image = (Image)resources.GetObject("cmnuScriptQueryPaste.Image");
            cmnuScriptQueryPaste.Name = "cmnuScriptQueryPaste";
            cmnuScriptQueryPaste.ShortcutKeys = Keys.Control | Keys.V;
            cmnuScriptQueryPaste.Size = new Size(226, 32);
            cmnuScriptQueryPaste.Text = "Paste";
            cmnuScriptQueryPaste.Click += cmnuScriptQueryPaste_Click;
            // 
            // cmnuScriptQuerySelectAll
            // 
            cmnuScriptQuerySelectAll.Image = (Image)resources.GetObject("cmnuScriptQuerySelectAll.Image");
            cmnuScriptQuerySelectAll.Name = "cmnuScriptQuerySelectAll";
            cmnuScriptQuerySelectAll.ShortcutKeys = Keys.Control | Keys.A;
            cmnuScriptQuerySelectAll.Size = new Size(226, 32);
            cmnuScriptQuerySelectAll.Text = "Select All";
            cmnuScriptQuerySelectAll.Click += cmnuScriptQuerySelectAll_Click;
            // 
            // cmnuSeparator01
            // 
            cmnuSeparator01.Name = "cmnuSeparator01";
            cmnuSeparator01.Size = new Size(223, 6);
            // 
            // cmnuScriptQueryUndo
            // 
            cmnuScriptQueryUndo.Image = (Image)resources.GetObject("cmnuScriptQueryUndo.Image");
            cmnuScriptQueryUndo.Name = "cmnuScriptQueryUndo";
            cmnuScriptQueryUndo.ShortcutKeys = Keys.Control | Keys.Z;
            cmnuScriptQueryUndo.Size = new Size(226, 32);
            cmnuScriptQueryUndo.Text = "Undo";
            cmnuScriptQueryUndo.Click += cmnuScriptQueryUndo_Click;
            // 
            // cmnuScriptQueryRedo
            // 
            cmnuScriptQueryRedo.Image = (Image)resources.GetObject("cmnuScriptQueryRedo.Image");
            cmnuScriptQueryRedo.Name = "cmnuScriptQueryRedo";
            cmnuScriptQueryRedo.ShortcutKeys = Keys.Control | Keys.Y;
            cmnuScriptQueryRedo.Size = new Size(226, 32);
            cmnuScriptQueryRedo.Text = "Redo";
            cmnuScriptQueryRedo.Click += cmnuScriptQueryRedo_Click;
            // 
            // lblCmdQuery
            // 
            lblCmdQuery.AutoSize = true;
            lblCmdQuery.Location = new Point(6, 199);
            lblCmdQuery.Margin = new Padding(6, 0, 6, 0);
            lblCmdQuery.Name = "lblCmdQuery";
            lblCmdQuery.Size = new Size(44, 25);
            lblCmdQuery.TabIndex = 4;
            lblCmdQuery.Text = "SQL";
            // 
            // pageTags
            // 
            pageTags.Controls.Add(gpbTags);
            pageTags.Controls.Add(gpbTagFormatDatabase);
            pageTags.Location = new Point(4, 34);
            pageTags.Name = "pageTags";
            pageTags.Padding = new Padding(3);
            pageTags.Size = new Size(1122, 454);
            pageTags.TabIndex = 1;
            pageTags.Text = "Tags";
            pageTags.UseVisualStyleBackColor = true;
            // 
            // gpbTags
            // 
            gpbTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gpbTags.Controls.Add(tlpTags);
            gpbTags.Location = new Point(9, 147);
            gpbTags.Margin = new Padding(6, 5, 6, 5);
            gpbTags.Name = "gpbTags";
            gpbTags.Padding = new Padding(17, 5, 17, 20);
            gpbTags.Size = new Size(1074, 299);
            gpbTags.TabIndex = 13;
            gpbTags.TabStop = false;
            gpbTags.Text = "Tags";
            // 
            // tlpTags
            // 
            tlpTags.ColumnCount = 1;
            tlpTags.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.5955F));
            tlpTags.Controls.Add(lstTags, 0, 0);
            tlpTags.Dock = DockStyle.Fill;
            tlpTags.Location = new Point(17, 29);
            tlpTags.Margin = new Padding(4, 5, 4, 5);
            tlpTags.Name = "tlpTags";
            tlpTags.RowCount = 1;
            tlpTags.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTags.Size = new Size(1040, 250);
            tlpTags.TabIndex = 1;
            // 
            // lstTags
            // 
            lstTags.Alignment = ListViewAlignment.Default;
            lstTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstTags.Columns.AddRange(new ColumnHeader[] { clmName, clmCode, clmFormat, clmEnabled });
            lstTags.ContextMenuStrip = cmnuMenuListTags;
            lstTags.FullRowSelect = true;
            lstTags.GridLines = true;
            lstTags.Location = new Point(4, 5);
            lstTags.Margin = new Padding(4, 5, 4, 5);
            lstTags.MultiSelect = false;
            lstTags.Name = "lstTags";
            lstTags.Size = new Size(1032, 240);
            lstTags.TabIndex = 2;
            lstTags.UseCompatibleStateImageBehavior = false;
            lstTags.View = System.Windows.Forms.View.Details;
            lstTags.MouseClick += lstTags_MouseClick;
            lstTags.MouseDoubleClick += lstTags_MouseDoubleClick;
            // 
            // clmName
            // 
            clmName.Text = "Name";
            clmName.Width = 200;
            // 
            // clmCode
            // 
            clmCode.Text = "Сode";
            clmCode.Width = 200;
            // 
            // clmFormat
            // 
            clmFormat.Text = "Format";
            clmFormat.Width = 110;
            // 
            // clmEnabled
            // 
            clmEnabled.Text = "Enabled";
            clmEnabled.Width = 80;
            // 
            // cmnuMenuListTags
            // 
            cmnuMenuListTags.ImageScalingSize = new Size(24, 24);
            cmnuMenuListTags.Items.AddRange(new ToolStripItem[] { cmnuTagAdd, cmnuTagListAdd, toolStripSeparator3, cmnuTagChange, toolStripSeparator4, cmnuTagDelete, cmnuTagListDelete, toolStripSeparator5, cmnuTagUp, cmnuTagDown });
            cmnuMenuListTags.Name = "cmnuSelectQuery";
            cmnuMenuListTags.Size = new Size(216, 246);
            // 
            // cmnuTagAdd
            // 
            cmnuTagAdd.Image = (Image)resources.GetObject("cmnuTagAdd.Image");
            cmnuTagAdd.Name = "cmnuTagAdd";
            cmnuTagAdd.Size = new Size(215, 32);
            cmnuTagAdd.Text = "Add Tag";
            cmnuTagAdd.Click += cmnuTagAdd_Click;
            // 
            // cmnuTagListAdd
            // 
            cmnuTagListAdd.Image = (Image)resources.GetObject("cmnuTagListAdd.Image");
            cmnuTagListAdd.Name = "cmnuTagListAdd";
            cmnuTagListAdd.Size = new Size(215, 32);
            cmnuTagListAdd.Text = "Add list of Tags";
            cmnuTagListAdd.Click += cmnuListTagAdd_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(212, 6);
            // 
            // cmnuTagChange
            // 
            cmnuTagChange.Image = (Image)resources.GetObject("cmnuTagChange.Image");
            cmnuTagChange.Name = "cmnuTagChange";
            cmnuTagChange.Size = new Size(215, 32);
            cmnuTagChange.Text = "Change Tag";
            cmnuTagChange.Click += cmnuTagChange_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(212, 6);
            // 
            // cmnuTagDelete
            // 
            cmnuTagDelete.Image = (Image)resources.GetObject("cmnuTagDelete.Image");
            cmnuTagDelete.Name = "cmnuTagDelete";
            cmnuTagDelete.Size = new Size(215, 32);
            cmnuTagDelete.Text = "Delete Tag";
            cmnuTagDelete.Click += cmnuTagDelete_Click;
            // 
            // cmnuTagListDelete
            // 
            cmnuTagListDelete.Image = (Image)resources.GetObject("cmnuTagListDelete.Image");
            cmnuTagListDelete.Name = "cmnuTagListDelete";
            cmnuTagListDelete.Size = new Size(215, 32);
            cmnuTagListDelete.Text = "Delete all Tags";
            cmnuTagListDelete.Click += cmnuTagListDelete_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(212, 6);
            // 
            // cmnuTagUp
            // 
            cmnuTagUp.Image = (Image)resources.GetObject("cmnuTagUp.Image");
            cmnuTagUp.Name = "cmnuTagUp";
            cmnuTagUp.Size = new Size(215, 32);
            cmnuTagUp.Text = "Up";
            // 
            // cmnuTagDown
            // 
            cmnuTagDown.Image = (Image)resources.GetObject("cmnuTagDown.Image");
            cmnuTagDown.Name = "cmnuTagDown";
            cmnuTagDown.Size = new Size(215, 32);
            cmnuTagDown.Text = "Down";
            // 
            // gpbTagFormatDatabase
            // 
            gpbTagFormatDatabase.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gpbTagFormatDatabase.Controls.Add(rdbTagsBasedRequestedTableRows);
            gpbTagFormatDatabase.Controls.Add(rdbTagsBasedRequestedTableColumns);
            gpbTagFormatDatabase.Location = new Point(9, 8);
            gpbTagFormatDatabase.Margin = new Padding(6, 5, 6, 5);
            gpbTagFormatDatabase.Name = "gpbTagFormatDatabase";
            gpbTagFormatDatabase.Padding = new Padding(17, 5, 17, 20);
            gpbTagFormatDatabase.Size = new Size(1074, 128);
            gpbTagFormatDatabase.TabIndex = 8;
            gpbTagFormatDatabase.TabStop = false;
            gpbTagFormatDatabase.Text = "Format of Tags from the Database";
            // 
            // rdbTagsBasedRequestedTableRows
            // 
            rdbTagsBasedRequestedTableRows.AutoSize = true;
            rdbTagsBasedRequestedTableRows.Location = new Point(34, 82);
            rdbTagsBasedRequestedTableRows.Margin = new Padding(6, 5, 6, 5);
            rdbTagsBasedRequestedTableRows.Name = "rdbTagsBasedRequestedTableRows";
            rdbTagsBasedRequestedTableRows.Size = new Size(401, 29);
            rdbTagsBasedRequestedTableRows.TabIndex = 1;
            rdbTagsBasedRequestedTableRows.Text = "Tags based on the list of requested table rows";
            rdbTagsBasedRequestedTableRows.UseVisualStyleBackColor = true;
            // 
            // rdbTagsBasedRequestedTableColumns
            // 
            rdbTagsBasedRequestedTableColumns.AutoSize = true;
            rdbTagsBasedRequestedTableColumns.Checked = true;
            rdbTagsBasedRequestedTableColumns.Location = new Point(34, 38);
            rdbTagsBasedRequestedTableColumns.Margin = new Padding(6, 5, 6, 5);
            rdbTagsBasedRequestedTableColumns.Name = "rdbTagsBasedRequestedTableColumns";
            rdbTagsBasedRequestedTableColumns.Size = new Size(430, 29);
            rdbTagsBasedRequestedTableColumns.TabIndex = 0;
            rdbTagsBasedRequestedTableColumns.TabStop = true;
            rdbTagsBasedRequestedTableColumns.Text = "Tags based on the list of requested table columns";
            rdbTagsBasedRequestedTableColumns.UseVisualStyleBackColor = true;
            // 
            // pageResult
            // 
            pageResult.Controls.Add(fctResult);
            pageResult.Location = new Point(4, 34);
            pageResult.Name = "pageResult";
            pageResult.Size = new Size(1122, 454);
            pageResult.TabIndex = 2;
            pageResult.Text = "Result";
            pageResult.UseVisualStyleBackColor = true;
            // 
            // fctResult
            // 
            fctResult.AutoCompleteBracketsList = new char[]
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
            fctResult.AutoIndentCharsPatterns = "";
            fctResult.AutoScrollMinSize = new Size(0, 22);
            fctResult.BackBrush = null;
            fctResult.BorderStyle = BorderStyle.FixedSingle;
            fctResult.CharHeight = 22;
            fctResult.CharWidth = 12;
            fctResult.CommentPrefix = "--";
            fctResult.Cursor = Cursors.IBeam;
            fctResult.DefaultMarkerSize = 8;
            fctResult.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            fctResult.Dock = DockStyle.Fill;
            fctResult.Font = new Font("Courier New", 9.75F);
            fctResult.Hotkeys = resources.GetString("fctResult.Hotkeys");
            fctResult.IsReplaceMode = false;
            fctResult.LeftBracket = '(';
            fctResult.Location = new Point(0, 0);
            fctResult.Margin = new Padding(4, 5, 4, 5);
            fctResult.Name = "fctResult";
            fctResult.Paddings = new Padding(0);
            fctResult.RightBracket = ')';
            fctResult.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            fctResult.ServiceColors = null;
            fctResult.Size = new Size(1122, 454);
            fctResult.TabIndex = 7;
            fctResult.WordWrap = true;
            fctResult.Zoom = 100;
            // 
            // txtCmdDescription
            // 
            txtCmdDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCmdDescription.Location = new Point(23, 202);
            txtCmdDescription.Margin = new Padding(6, 5, 6, 5);
            txtCmdDescription.Multiline = true;
            txtCmdDescription.Name = "txtCmdDescription";
            txtCmdDescription.Size = new Size(1131, 62);
            txtCmdDescription.TabIndex = 12;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(23, 172);
            lblDescription.Margin = new Padding(6, 0, 6, 0);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(102, 25);
            lblDescription.TabIndex = 11;
            lblDescription.Text = "Description";
            // 
            // lblCmdStringLenght
            // 
            lblCmdStringLenght.AutoSize = true;
            lblCmdStringLenght.Location = new Point(23, 272);
            lblCmdStringLenght.Name = "lblCmdStringLenght";
            lblCmdStringLenght.Size = new Size(433, 25);
            lblCmdStringLenght.TabIndex = 10;
            lblCmdStringLenght.Text = "Maximum number of characters in a string command";
            lblCmdStringLenght.Visible = false;
            // 
            // nudCmdStringLenght
            // 
            nudCmdStringLenght.Location = new Point(27, 300);
            nudCmdStringLenght.Name = "nudCmdStringLenght";
            nudCmdStringLenght.Size = new Size(161, 31);
            nudCmdStringLenght.TabIndex = 9;
            nudCmdStringLenght.Visible = false;
            // 
            // lblCmdCode
            // 
            lblCmdCode.AutoSize = true;
            lblCmdCode.Location = new Point(332, 29);
            lblCmdCode.Margin = new Padding(6, 0, 6, 0);
            lblCmdCode.Name = "lblCmdCode";
            lblCmdCode.Size = new Size(140, 25);
            lblCmdCode.TabIndex = 8;
            lblCmdCode.Text = "Command code";
            // 
            // txtCmdCode
            // 
            txtCmdCode.Location = new Point(332, 63);
            txtCmdCode.Margin = new Padding(6, 5, 6, 5);
            txtCmdCode.Name = "txtCmdCode";
            txtCmdCode.Size = new Size(170, 31);
            txtCmdCode.TabIndex = 7;
            // 
            // txtCmdName
            // 
            txtCmdName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCmdName.Location = new Point(23, 128);
            txtCmdName.Margin = new Padding(6, 5, 6, 5);
            txtCmdName.Name = "txtCmdName";
            txtCmdName.Size = new Size(1131, 31);
            txtCmdName.TabIndex = 3;
            // 
            // lblCmdName
            // 
            lblCmdName.AutoSize = true;
            lblCmdName.Location = new Point(23, 98);
            lblCmdName.Margin = new Padding(6, 0, 6, 0);
            lblCmdName.Name = "lblCmdName";
            lblCmdName.Size = new Size(59, 25);
            lblCmdName.TabIndex = 2;
            lblCmdName.Text = "Name";
            // 
            // nudCmdNum
            // 
            nudCmdNum.Location = new Point(149, 63);
            nudCmdNum.Margin = new Padding(6, 5, 6, 5);
            nudCmdNum.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            nudCmdNum.Name = "nudCmdNum";
            nudCmdNum.Size = new Size(171, 31);
            nudCmdNum.TabIndex = 1;
            // 
            // lblCmdNum
            // 
            lblCmdNum.AutoSize = true;
            lblCmdNum.Location = new Point(149, 29);
            lblCmdNum.Margin = new Padding(6, 0, 6, 0);
            lblCmdNum.Name = "lblCmdNum";
            lblCmdNum.Size = new Size(163, 25);
            lblCmdNum.TabIndex = 0;
            lblCmdNum.Text = "Command number";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 857);
            pnlBottom.Margin = new Padding(6, 5, 6, 5);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1177, 78);
            pnlBottom.TabIndex = 4;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(1036, 12);
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
            btnSave.Location = new Point(897, 12);
            btnSave.Margin = new Padding(6, 5, 6, 5);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(126, 45);
            btnSave.TabIndex = 2;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FrmImportCmd
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 935);
            Controls.Add(pnlBottom);
            Controls.Add(gbCommandParams);
            KeyPreview = true;
            MinimumSize = new Size(1191, 963);
            Name = "FrmImportCmd";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Import Command";
            WindowState = FormWindowState.Maximized;
            Load += FrmImportCmd_Load;
            KeyDown += FrmImportCmd_KeyDown;
            gbCommandParams.ResumeLayout(false);
            gbCommandParams.PerformLayout();
            tabControlCommand.ResumeLayout(false);
            pageData.ResumeLayout(false);
            tlpPanel.ResumeLayout(false);
            tlpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ((System.ComponentModel.ISupportInitialize)fctCmdQuery).EndInit();
            cmnuMenuScriptQuery.ResumeLayout(false);
            pageTags.ResumeLayout(false);
            gpbTags.ResumeLayout(false);
            tlpTags.ResumeLayout(false);
            cmnuMenuListTags.ResumeLayout(false);
            gpbTagFormatDatabase.ResumeLayout(false);
            gpbTagFormatDatabase.PerformLayout();
            pageResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)fctResult).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCmdStringLenght).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCmdNum).EndInit();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbCommandParams;
        private Label lblCmdStringLenght;
        private NumericUpDown nudCmdStringLenght;
        private Label lblCmdCode;
        private TextBox txtCmdCode;
        private Label lblCmdQuery;
        private TextBox txtCmdName;
        private Label lblCmdName;
        private NumericUpDown nudCmdNum;
        private Label lblCmdNum;
        private FastColoredTextBoxNS.FastColoredTextBox fctCmdQuery;
        private TextBox txtCmdDescription;
        private Label lblDescription;
        private TabControl tabControlCommand;
        private TabPage pageData;
        private TableLayoutPanel tlpPanel;
        private TabPage pageTags;
        private Label lblData;
        private DataGridView dgvData;
        private Button btnExecuteSQLQuery;
        private GroupBox gpbTagFormatDatabase;
        private RadioButton rdbTagsBasedRequestedTableRows;
        private RadioButton rdbTagsBasedRequestedTableColumns;
        private TabPage pageResult;
        private GroupBox gpbTags;
        private TableLayoutPanel tlpTags;
        private ListView lstTags;
        private ColumnHeader clmName;
        private ColumnHeader clmCode;
        private ColumnHeader clmFormat;
        private ColumnHeader clmEnabled;
        private FastColoredTextBoxNS.FastColoredTextBox fctResult;
        private CheckBox ckbEnabled;
        private Label lblCmdEnabled;
        private Panel pnlBottom;
        private Button btnClose;
        private Button btnSave;
        private ContextMenuStrip cmnuMenuScriptQuery;
        private ToolStripMenuItem cmnuScriptQueryCut;
        private ToolStripMenuItem cmnuScriptQueryCopy;
        private ToolStripMenuItem cmnuScriptQueryPaste;
        private ToolStripMenuItem cmnuScriptQuerySelectAll;
        private ToolStripSeparator cmnuSeparator01;
        private ToolStripMenuItem cmnuScriptQueryUndo;
        private ToolStripMenuItem cmnuScriptQueryRedo;
        private ContextMenuStrip cmnuMenuListTags;
        private ToolStripMenuItem cmnuTagAdd;
        private ToolStripMenuItem cmnuTagListAdd;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cmnuTagChange;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem cmnuTagDelete;
        private ToolStripMenuItem cmnuTagListDelete;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem cmnuTagUp;
        private ToolStripMenuItem cmnuTagDown;
    }
}