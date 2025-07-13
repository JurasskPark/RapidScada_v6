namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
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
            cmnuCmdQueryCut = new ToolStripMenuItem();
            cmnuCmdQueryCopy = new ToolStripMenuItem();
            cmnuCmdQueryPaste = new ToolStripMenuItem();
            cmnuCmdQuerySelectAll = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            cmnuCmdQueryUndo = new ToolStripMenuItem();
            cmnuCmdQueryRedo = new ToolStripMenuItem();
            tabControl = new TabControl();
            pageSettings = new TabPage();
            gbMode = new GroupBox();
            rdbPingAsync = new RadioButton();
            rdbPingSync = new RadioButton();
            gbLog = new GroupBox();
            cbLog = new CheckBox();
            gpbTags = new GroupBox();
            tlpTags = new TableLayoutPanel();
            lstTags = new ListView();
            clmTagname = new ColumnHeader();
            clmTagCode = new ColumnHeader();
            clmTagIPAddress = new ColumnHeader();
            clmTagEnabled = new ColumnHeader();
            cmnuLstTags = new ContextMenuStrip(components);
            cmnuTagAdd = new ToolStripMenuItem();
            cmnuListTagAdd = new ToolStripMenuItem();
            cmnuFoundIpAddresses = new ToolStripMenuItem();
            cmnuSeparator01 = new ToolStripSeparator();
            cmnuTagChange = new ToolStripMenuItem();
            cmnuSeparator02 = new ToolStripSeparator();
            cmnuTagDelete = new ToolStripMenuItem();
            cmnuTagAllDelete = new ToolStripMenuItem();
            cmnuSeparator03 = new ToolStripSeparator();
            cmnuUp = new ToolStripMenuItem();
            cmnuDown = new ToolStripMenuItem();
            pnlBottom = new Panel();
            lblStatusInfo = new Label();
            btnClose = new Button();
            btnSave = new Button();
            tabControl.SuspendLayout();
            pageSettings.SuspendLayout();
            gbMode.SuspendLayout();
            gbLog.SuspendLayout();
            gpbTags.SuspendLayout();
            tlpTags.SuspendLayout();
            cmnuLstTags.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // cmnuCmdQueryCut
            // 
            cmnuCmdQueryCut.Name = "cmnuCmdQueryCut";
            cmnuCmdQueryCut.Size = new Size(122, 22);
            cmnuCmdQueryCut.Text = "Cut";
            // 
            // cmnuCmdQueryCopy
            // 
            cmnuCmdQueryCopy.Name = "cmnuCmdQueryCopy";
            cmnuCmdQueryCopy.Size = new Size(122, 22);
            cmnuCmdQueryCopy.Text = "Copy";
            // 
            // cmnuCmdQueryPaste
            // 
            cmnuCmdQueryPaste.Name = "cmnuCmdQueryPaste";
            cmnuCmdQueryPaste.Size = new Size(122, 22);
            cmnuCmdQueryPaste.Text = "Paste";
            // 
            // cmnuCmdQuerySelectAll
            // 
            cmnuCmdQuerySelectAll.Name = "cmnuCmdQuerySelectAll";
            cmnuCmdQuerySelectAll.Size = new Size(122, 22);
            cmnuCmdQuerySelectAll.Text = "Select All";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(119, 6);
            // 
            // cmnuCmdQueryUndo
            // 
            cmnuCmdQueryUndo.Name = "cmnuCmdQueryUndo";
            cmnuCmdQueryUndo.Size = new Size(122, 22);
            cmnuCmdQueryUndo.Text = "Undo";
            // 
            // cmnuCmdQueryRedo
            // 
            cmnuCmdQueryRedo.Name = "cmnuCmdQueryRedo";
            cmnuCmdQueryRedo.Size = new Size(122, 22);
            cmnuCmdQueryRedo.Text = "Redo";
            // 
            // tabControl
            // 
            tabControl.Controls.Add(pageSettings);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(4, 3, 4, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(785, 497);
            tabControl.TabIndex = 0;
            // 
            // pageSettings
            // 
            pageSettings.Controls.Add(gbMode);
            pageSettings.Controls.Add(gbLog);
            pageSettings.Controls.Add(gpbTags);
            pageSettings.Location = new Point(4, 24);
            pageSettings.Margin = new Padding(4, 3, 4, 3);
            pageSettings.Name = "pageSettings";
            pageSettings.Size = new Size(777, 469);
            pageSettings.TabIndex = 3;
            pageSettings.Text = "Settings";
            pageSettings.UseVisualStyleBackColor = true;
            // 
            // gbMode
            // 
            gbMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            gbMode.Controls.Add(rdbPingAsync);
            gbMode.Controls.Add(rdbPingSync);
            gbMode.Location = new Point(323, 3);
            gbMode.Name = "gbMode";
            gbMode.Size = new Size(444, 48);
            gbMode.TabIndex = 17;
            gbMode.TabStop = false;
            gbMode.Text = "Mode";
            // 
            // rdbPingAsync
            // 
            rdbPingAsync.AutoSize = true;
            rdbPingAsync.Location = new Point(116, 22);
            rdbPingAsync.Name = "rdbPingAsync";
            rdbPingAsync.Size = new Size(101, 19);
            rdbPingAsync.TabIndex = 15;
            rdbPingAsync.TabStop = true;
            rdbPingAsync.Text = "Asynchronous";
            rdbPingAsync.UseVisualStyleBackColor = true;
            rdbPingAsync.CheckedChanged += rdbPingAsync_CheckedChanged;
            // 
            // rdbPingSync
            // 
            rdbPingSync.AutoSize = true;
            rdbPingSync.Location = new Point(16, 22);
            rdbPingSync.Name = "rdbPingSync";
            rdbPingSync.Size = new Size(94, 19);
            rdbPingSync.TabIndex = 14;
            rdbPingSync.TabStop = true;
            rdbPingSync.Text = "Synchronous";
            rdbPingSync.UseVisualStyleBackColor = true;
            rdbPingSync.CheckedChanged += rdbPingSync_CheckedChanged;
            // 
            // gbLog
            // 
            gbLog.Controls.Add(cbLog);
            gbLog.Location = new Point(7, 3);
            gbLog.Name = "gbLog";
            gbLog.Size = new Size(307, 48);
            gbLog.TabIndex = 16;
            gbLog.TabStop = false;
            gbLog.Text = "Log";
            // 
            // cbLog
            // 
            cbLog.AutoSize = true;
            cbLog.Location = new Point(17, 22);
            cbLog.Name = "cbLog";
            cbLog.Size = new Size(79, 19);
            cbLog.TabIndex = 13;
            cbLog.Text = "Write logs";
            cbLog.UseVisualStyleBackColor = true;
            cbLog.CheckedChanged += control_Changed;
            // 
            // gpbTags
            // 
            gpbTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gpbTags.Controls.Add(tlpTags);
            gpbTags.Location = new Point(7, 57);
            gpbTags.Margin = new Padding(4, 3, 4, 3);
            gpbTags.Name = "gpbTags";
            gpbTags.Padding = new Padding(12, 3, 12, 12);
            gpbTags.Size = new Size(760, 401);
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
            tlpTags.Size = new Size(731, 364);
            tlpTags.TabIndex = 1;
            // 
            // lstTags
            // 
            lstTags.Alignment = ListViewAlignment.Default;
            lstTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstTags.Columns.AddRange(new ColumnHeader[] { clmTagname, clmTagCode, clmTagIPAddress, clmTagEnabled });
            lstTags.ContextMenuStrip = cmnuLstTags;
            lstTags.FullRowSelect = true;
            lstTags.GridLines = true;
            lstTags.Location = new Point(3, 3);
            lstTags.MultiSelect = false;
            lstTags.Name = "lstTags";
            lstTags.Size = new Size(725, 358);
            lstTags.TabIndex = 2;
            lstTags.UseCompatibleStateImageBehavior = false;
            lstTags.View = System.Windows.Forms.View.Details;
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
            clmTagCode.Text = "Tag code";
            clmTagCode.Width = 200;
            // 
            // clmTagIPAddress
            // 
            clmTagIPAddress.Text = "IP Address";
            clmTagIPAddress.Width = 200;
            // 
            // clmTagEnabled
            // 
            clmTagEnabled.Text = "Enabled";
            clmTagEnabled.Width = 120;
            // 
            // cmnuLstTags
            // 
            cmnuLstTags.ImageScalingSize = new Size(24, 24);
            cmnuLstTags.Items.AddRange(new ToolStripItem[] { cmnuTagAdd, cmnuListTagAdd, cmnuFoundIpAddresses, cmnuSeparator01, cmnuTagChange, cmnuSeparator02, cmnuTagDelete, cmnuTagAllDelete, cmnuSeparator03, cmnuUp, cmnuDown });
            cmnuLstTags.Name = "cmnuSelectQuery";
            cmnuLstTags.Size = new Size(207, 262);
            // 
            // cmnuTagAdd
            // 
            cmnuTagAdd.Image = (Image)resources.GetObject("cmnuTagAdd.Image");
            cmnuTagAdd.Name = "cmnuTagAdd";
            cmnuTagAdd.Size = new Size(206, 30);
            cmnuTagAdd.Text = "Add Tag";
            cmnuTagAdd.Click += cmnuTagAdd_Click;
            // 
            // cmnuListTagAdd
            // 
            cmnuListTagAdd.Image = (Image)resources.GetObject("cmnuListTagAdd.Image");
            cmnuListTagAdd.Name = "cmnuListTagAdd";
            cmnuListTagAdd.Size = new Size(206, 30);
            cmnuListTagAdd.Text = "Add list of Tags";
            cmnuListTagAdd.Click += cmnuListTagAdd_Click;
            // 
            // cmnuFoundIpAddresses
            // 
            cmnuFoundIpAddresses.Image = (Image)resources.GetObject("cmnuFoundIpAddresses.Image");
            cmnuFoundIpAddresses.Name = "cmnuFoundIpAddresses";
            cmnuFoundIpAddresses.Size = new Size(206, 30);
            cmnuFoundIpAddresses.Text = "Add found Ip addresses";
            cmnuFoundIpAddresses.Click += cmnuFoundIpAddresses_Click;
            // 
            // cmnuSeparator01
            // 
            cmnuSeparator01.Name = "cmnuSeparator01";
            cmnuSeparator01.Size = new Size(203, 6);
            // 
            // cmnuTagChange
            // 
            cmnuTagChange.Image = (Image)resources.GetObject("cmnuTagChange.Image");
            cmnuTagChange.Name = "cmnuTagChange";
            cmnuTagChange.Size = new Size(206, 30);
            cmnuTagChange.Text = "Change Tag";
            cmnuTagChange.Click += cmnuTagChange_Click;
            // 
            // cmnuSeparator02
            // 
            cmnuSeparator02.Name = "cmnuSeparator02";
            cmnuSeparator02.Size = new Size(203, 6);
            // 
            // cmnuTagDelete
            // 
            cmnuTagDelete.Image = (Image)resources.GetObject("cmnuTagDelete.Image");
            cmnuTagDelete.Name = "cmnuTagDelete";
            cmnuTagDelete.ShortcutKeys = Keys.Control | Keys.Delete;
            cmnuTagDelete.Size = new Size(206, 30);
            cmnuTagDelete.Text = "Delete Tag";
            cmnuTagDelete.Click += cmnuTagDelete_Click;
            // 
            // cmnuTagAllDelete
            // 
            cmnuTagAllDelete.Image = (Image)resources.GetObject("cmnuTagAllDelete.Image");
            cmnuTagAllDelete.Name = "cmnuTagAllDelete";
            cmnuTagAllDelete.Size = new Size(206, 30);
            cmnuTagAllDelete.Text = "Delete all Tags";
            cmnuTagAllDelete.Click += cmnuTagAllDelete_Click;
            // 
            // cmnuSeparator03
            // 
            cmnuSeparator03.Name = "cmnuSeparator03";
            cmnuSeparator03.Size = new Size(203, 6);
            // 
            // cmnuUp
            // 
            cmnuUp.Image = (Image)resources.GetObject("cmnuUp.Image");
            cmnuUp.Name = "cmnuUp";
            cmnuUp.ShortcutKeys = Keys.Control | Keys.Up;
            cmnuUp.Size = new Size(206, 30);
            cmnuUp.Text = "Up";
            cmnuUp.Click += cmnuUp_Click;
            // 
            // cmnuDown
            // 
            cmnuDown.Image = (Image)resources.GetObject("cmnuDown.Image");
            cmnuDown.Name = "cmnuDown";
            cmnuDown.ShortcutKeys = Keys.Control | Keys.Down;
            cmnuDown.Size = new Size(206, 30);
            cmnuDown.Text = "Down";
            cmnuDown.Click += cmnuDown_Click;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(lblStatusInfo);
            pnlBottom.Controls.Add(btnClose);
            pnlBottom.Controls.Add(btnSave);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 497);
            pnlBottom.Margin = new Padding(4, 3, 4, 3);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(785, 47);
            pnlBottom.TabIndex = 1;
            // 
            // lblStatusInfo
            // 
            lblStatusInfo.AutoSize = true;
            lblStatusInfo.Location = new Point(12, 13);
            lblStatusInfo.Name = "lblStatusInfo";
            lblStatusInfo.Size = new Size(0, 15);
            lblStatusInfo.TabIndex = 2;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(684, 7);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 27);
            btnClose.TabIndex = 1;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(589, 7);
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
            ClientSize = new Size(785, 544);
            Controls.Add(tabControl);
            Controls.Add(pnlBottom);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(671, 566);
            Name = "FrmConfig";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ping JP - Device {0} Properties Version {1}";
            FormClosing += FrmConfig_FormClosing;
            Load += FrmConfig_Load;
            Shown += FrmConfig_Shown;
            tabControl.ResumeLayout(false);
            pageSettings.ResumeLayout(false);
            gbMode.ResumeLayout(false);
            gbMode.PerformLayout();
            gbLog.ResumeLayout(false);
            gbLog.PerformLayout();
            gpbTags.ResumeLayout(false);
            tlpTags.ResumeLayout(false);
            cmnuLstTags.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;

        private System.Windows.Forms.TabPage pageSettings;
        private GroupBox gpbTags;
        private TableLayoutPanel tlpTags;
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
        private ToolStripSeparator cmnuSeparator01;
        private ToolStripMenuItem cmnuTagChange;
        private ToolStripSeparator cmnuSeparator02;
        private ToolStripMenuItem cmnuTagAllDelete;
        private ToolStripSeparator cmnuSeparator03;
        private ColumnHeader clmTagCode;
        private ColumnHeader clmTagIPAddress;
        private CheckBox cbLog;
        private RadioButton rdbPingAsync;
        private RadioButton rdbPingSync;
        private GroupBox gbMode;
        private GroupBox gbLog;
        private ToolStripMenuItem cmnuFoundIpAddresses;
        private Label lblStatusInfo;
    }
}