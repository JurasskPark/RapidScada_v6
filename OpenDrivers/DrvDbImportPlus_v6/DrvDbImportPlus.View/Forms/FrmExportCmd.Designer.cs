namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    partial class FrmExportCmd
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmExportCmd));
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
            gbCommandParams.Name = "gbCommandParams";
            gbCommandParams.Padding = new Padding(17, 5, 17, 20);
            gbCommandParams.Size = new Size(1177, 858);
            gbCommandParams.TabIndex = 0;
            gbCommandParams.TabStop = false;
            gbCommandParams.Text = "Command Parameters";
            // 
            // lblCmdEnabled
            // 
            lblCmdEnabled.AutoSize = true;
            lblCmdEnabled.Location = new Point(23, 28);
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
            ckbEnabled.CheckedChanged += control_Changed;
            // 
            // tabControlCommand
            // 
            tabControlCommand.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlCommand.Controls.Add(pageData);
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
            btnExecuteSQLQuery.TabIndex = 4;
            btnExecuteSQLQuery.Text = "Execute SQL query";
            btnExecuteSQLQuery.UseVisualStyleBackColor = true;
            btnExecuteSQLQuery.Click += btnExecuteSQLQuery_Click;
            // 
            // dgvData
            // 
            dgvData.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvData.DefaultCellStyle = dataGridViewCellStyle2;
            dgvData.Dock = DockStyle.Fill;
            dgvData.Location = new Point(6, 32);
            dgvData.Margin = new Padding(6, 5, 6, 5);
            dgvData.Name = "dgvData";
            dgvData.ReadOnly = true;
            dgvData.RowHeadersWidth = 62;
            dgvData.Size = new Size(1104, 162);
            dgvData.TabIndex = 1;
            dgvData.DataError += dgvData_DataError;
            // 
            // lblData
            // 
            lblData.AutoSize = true;
            lblData.Location = new Point(3, 0);
            lblData.Name = "lblData";
            lblData.Size = new Size(49, 25);
            lblData.TabIndex = 0;
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
            fctCmdQuery.TabIndex = 3;
            fctCmdQuery.WordWrap = true;
            fctCmdQuery.Zoom = 100;
            fctCmdQuery.TextChanged += control_Changed;
            // 
            // cmnuMenuScriptQuery
            // 
            cmnuMenuScriptQuery.ImageScalingSize = new Size(24, 24);
            cmnuMenuScriptQuery.Items.AddRange(new ToolStripItem[] { cmnuScriptQueryCut, cmnuScriptQueryCopy, cmnuScriptQueryPaste, cmnuScriptQuerySelectAll, cmnuSeparator01, cmnuScriptQueryUndo, cmnuScriptQueryRedo });
            cmnuMenuScriptQuery.Name = "cmnuMenuScriptQuery";
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
            lblCmdQuery.Location = new Point(3, 199);
            lblCmdQuery.Name = "lblCmdQuery";
            lblCmdQuery.Size = new Size(44, 25);
            lblCmdQuery.TabIndex = 2;
            lblCmdQuery.Text = "SQL";
            // 
            // pageResult
            // 
            pageResult.Controls.Add(fctResult);
            pageResult.Location = new Point(4, 34);
            pageResult.Name = "pageResult";
            pageResult.Padding = new Padding(3);
            pageResult.Size = new Size(1122, 454);
            pageResult.TabIndex = 1;
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
            fctResult.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            fctResult.AutoScrollMinSize = new Size(2, 22);
            fctResult.BackBrush = null;
            fctResult.CharHeight = 22;
            fctResult.CharWidth = 12;
            fctResult.DefaultMarkerSize = 8;
            fctResult.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            fctResult.Dock = DockStyle.Fill;
            fctResult.Hotkeys = resources.GetString("fctResult.Hotkeys");
            fctResult.IsReplaceMode = false;
            fctResult.Location = new Point(3, 3);
            fctResult.Name = "fctResult";
            fctResult.Paddings = new Padding(0);
            fctResult.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            fctResult.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("fctResult.ServiceColors");
            fctResult.Size = new Size(1116, 448);
            fctResult.TabIndex = 0;
            fctResult.Zoom = 100;
            // 
            // txtCmdDescription
            // 
            txtCmdDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCmdDescription.Location = new Point(23, 202);
            txtCmdDescription.Multiline = true;
            txtCmdDescription.Name = "txtCmdDescription";
            txtCmdDescription.Size = new Size(1131, 62);
            txtCmdDescription.TabIndex = 12;
            txtCmdDescription.TextChanged += control_Changed;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(23, 172);
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
            // 
            // nudCmdStringLenght
            // 
            nudCmdStringLenght.Location = new Point(27, 300);
            nudCmdStringLenght.Maximum = new decimal(new int[] { 32767, 0, 0, 0 });
            nudCmdStringLenght.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudCmdStringLenght.Name = "nudCmdStringLenght";
            nudCmdStringLenght.Size = new Size(180, 31);
            nudCmdStringLenght.TabIndex = 9;
            nudCmdStringLenght.Value = new decimal(new int[] { 80, 0, 0, 0 });
            nudCmdStringLenght.ValueChanged += control_Changed;
            // 
            // lblCmdCode
            // 
            lblCmdCode.AutoSize = true;
            lblCmdCode.Location = new Point(205, 102);
            lblCmdCode.Name = "lblCmdCode";
            lblCmdCode.Size = new Size(140, 25);
            lblCmdCode.TabIndex = 8;
            lblCmdCode.Text = "Command code";
            // 
            // txtCmdCode
            // 
            txtCmdCode.Location = new Point(205, 132);
            txtCmdCode.Name = "txtCmdCode";
            txtCmdCode.Size = new Size(220, 31);
            txtCmdCode.TabIndex = 7;
            txtCmdCode.TextChanged += control_Changed;
            // 
            // txtCmdName
            // 
            txtCmdName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCmdName.Location = new Point(445, 132);
            txtCmdName.Name = "txtCmdName";
            txtCmdName.Size = new Size(708, 31);
            txtCmdName.TabIndex = 6;
            txtCmdName.TextChanged += control_Changed;
            // 
            // lblCmdName
            // 
            lblCmdName.AutoSize = true;
            lblCmdName.Location = new Point(445, 102);
            lblCmdName.Name = "lblCmdName";
            lblCmdName.Size = new Size(59, 25);
            lblCmdName.TabIndex = 5;
            lblCmdName.Text = "Name";
            // 
            // nudCmdNum
            // 
            nudCmdNum.Location = new Point(23, 132);
            nudCmdNum.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            nudCmdNum.Name = "nudCmdNum";
            nudCmdNum.Size = new Size(160, 31);
            nudCmdNum.TabIndex = 4;
            nudCmdNum.ValueChanged += control_Changed;
            // 
            // lblCmdNum
            // 
            lblCmdNum.AutoSize = true;
            lblCmdNum.Location = new Point(23, 102);
            lblCmdNum.Name = "lblCmdNum";
            lblCmdNum.Size = new Size(163, 25);
            lblCmdNum.TabIndex = 3;
            lblCmdNum.Text = "Command number";
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 860);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1177, 58);
            pnlBottom.TabIndex = 1;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(1046, 8);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(119, 40);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Enabled = false;
            btnSave.Location = new Point(919, 8);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(119, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // FrmExportCmd
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1177, 918);
            Controls.Add(pnlBottom);
            Controls.Add(gbCommandParams);
            KeyPreview = true;
            Name = "FrmExportCmd";
            Text = "Export Command";
            WindowState = FormWindowState.Maximized;
            Load += FrmExportCmd_Load;
            KeyDown += FrmExportCmd_KeyDown;
            gbCommandParams.ResumeLayout(false);
            gbCommandParams.PerformLayout();
            tabControlCommand.ResumeLayout(false);
            pageData.ResumeLayout(false);
            tlpPanel.ResumeLayout(false);
            tlpPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvData).EndInit();
            ((System.ComponentModel.ISupportInitialize)fctCmdQuery).EndInit();
            cmnuMenuScriptQuery.ResumeLayout(false);
            pageResult.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)fctResult).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCmdStringLenght).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCmdNum).EndInit();
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox gbCommandParams;
        private Label lblCmdEnabled;
        private CheckBox ckbEnabled;
        private TabControl tabControlCommand;
        private TabPage pageData;
        private TableLayoutPanel tlpPanel;
        private Button btnExecuteSQLQuery;
        private DataGridView dgvData;
        private Label lblData;
        private FastColoredTextBoxNS.FastColoredTextBox fctCmdQuery;
        private ContextMenuStrip cmnuMenuScriptQuery;
        private ToolStripMenuItem cmnuScriptQueryCut;
        private ToolStripMenuItem cmnuScriptQueryCopy;
        private ToolStripMenuItem cmnuScriptQueryPaste;
        private ToolStripMenuItem cmnuScriptQuerySelectAll;
        private ToolStripSeparator cmnuSeparator01;
        private ToolStripMenuItem cmnuScriptQueryUndo;
        private ToolStripMenuItem cmnuScriptQueryRedo;
        private Label lblCmdQuery;
        private TabPage pageResult;
        private FastColoredTextBoxNS.FastColoredTextBox fctResult;
        private TextBox txtCmdDescription;
        private Label lblDescription;
        private Label lblCmdStringLenght;
        private NumericUpDown nudCmdStringLenght;
        private Label lblCmdCode;
        private TextBox txtCmdCode;
        private TextBox txtCmdName;
        private Label lblCmdName;
        private NumericUpDown nudCmdNum;
        private Label lblCmdNum;
        private Panel pnlBottom;
        private Button btnClose;
        private Button btnSave;
    }
}
