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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmConfig));
            this.cmnuCmdQueryCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCmdQueryCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCmdQueryPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCmdQuerySelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmnuCmdQueryUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.cmnuCmdQueryRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.pageSettings = new System.Windows.Forms.TabPage();
            this.cbLog = new System.Windows.Forms.CheckBox();
            this.gpbTags = new System.Windows.Forms.GroupBox();
            this.tlpTags = new System.Windows.Forms.TableLayoutPanel();
            this.lstTags = new System.Windows.Forms.ListView();
            this.clmTagname = new System.Windows.Forms.ColumnHeader();
            this.clmTagCode = new System.Windows.Forms.ColumnHeader();
            this.clmTagIPAddress = new System.Windows.Forms.ColumnHeader();
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
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.tmrTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl.SuspendLayout();
            this.pageSettings.SuspendLayout();
            this.gpbTags.SuspendLayout();
            this.tlpTags.SuspendLayout();
            this.cmnuLstTags.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmnuCmdQueryCut
            // 
            this.cmnuCmdQueryCut.Name = "cmnuCmdQueryCut";
            this.cmnuCmdQueryCut.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryCut.Text = "Cut";
            // 
            // cmnuCmdQueryCopy
            // 
            this.cmnuCmdQueryCopy.Name = "cmnuCmdQueryCopy";
            this.cmnuCmdQueryCopy.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryCopy.Text = "Copy";
            // 
            // cmnuCmdQueryPaste
            // 
            this.cmnuCmdQueryPaste.Name = "cmnuCmdQueryPaste";
            this.cmnuCmdQueryPaste.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryPaste.Text = "Paste";
            // 
            // cmnuCmdQuerySelectAll
            // 
            this.cmnuCmdQuerySelectAll.Name = "cmnuCmdQuerySelectAll";
            this.cmnuCmdQuerySelectAll.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQuerySelectAll.Text = "Select All";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(119, 6);
            // 
            // cmnuCmdQueryUndo
            // 
            this.cmnuCmdQueryUndo.Name = "cmnuCmdQueryUndo";
            this.cmnuCmdQueryUndo.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryUndo.Text = "Undo";
            // 
            // cmnuCmdQueryRedo
            // 
            this.cmnuCmdQueryRedo.Name = "cmnuCmdQueryRedo";
            this.cmnuCmdQueryRedo.Size = new System.Drawing.Size(122, 22);
            this.cmnuCmdQueryRedo.Text = "Redo";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.pageSettings);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(657, 497);
            this.tabControl.TabIndex = 0;
            // 
            // pageSettings
            // 
            this.pageSettings.Controls.Add(this.cbLog);
            this.pageSettings.Controls.Add(this.gpbTags);
            this.pageSettings.Location = new System.Drawing.Point(4, 24);
            this.pageSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pageSettings.Name = "pageSettings";
            this.pageSettings.Size = new System.Drawing.Size(649, 469);
            this.pageSettings.TabIndex = 3;
            this.pageSettings.Text = "Settings";
            this.pageSettings.UseVisualStyleBackColor = true;
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Location = new System.Drawing.Point(7, 9);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(79, 19);
            this.cbLog.TabIndex = 13;
            this.cbLog.Text = "Write logs";
            this.cbLog.UseVisualStyleBackColor = true;
            this.cbLog.CheckedChanged += new System.EventHandler(this.control_Changed);
            // 
            // gpbTags
            // 
            this.gpbTags.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpbTags.Controls.Add(this.tlpTags);
            this.gpbTags.Location = new System.Drawing.Point(7, 34);
            this.gpbTags.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.gpbTags.Name = "gpbTags";
            this.gpbTags.Padding = new System.Windows.Forms.Padding(12, 3, 12, 12);
            this.gpbTags.Size = new System.Drawing.Size(632, 423);
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
            this.tlpTags.Size = new System.Drawing.Size(603, 386);
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
            this.clmTagIPAddress,
            this.clmTagEnabled});
            this.lstTags.ContextMenuStrip = this.cmnuLstTags;
            this.lstTags.FullRowSelect = true;
            this.lstTags.GridLines = true;
            this.lstTags.Location = new System.Drawing.Point(3, 3);
            this.lstTags.MultiSelect = false;
            this.lstTags.Name = "lstTags";
            this.lstTags.Size = new System.Drawing.Size(597, 380);
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
            // clmTagIPAddress
            // 
            this.clmTagIPAddress.Text = "IP Address";
            this.clmTagIPAddress.Width = 110;
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
            this.cmnuLstTags.Size = new System.Drawing.Size(155, 204);
            // 
            // cmnuTagRefresh
            // 
            this.cmnuTagRefresh.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagRefresh.Image")));
            this.cmnuTagRefresh.Name = "cmnuTagRefresh";
            this.cmnuTagRefresh.Size = new System.Drawing.Size(154, 22);
            this.cmnuTagRefresh.Text = "Refresh";
            this.cmnuTagRefresh.Click += new System.EventHandler(this.cmnuTagRefresh_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(151, 6);
            // 
            // cmnuTagAdd
            // 
            this.cmnuTagAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagAdd.Image")));
            this.cmnuTagAdd.Name = "cmnuTagAdd";
            this.cmnuTagAdd.Size = new System.Drawing.Size(154, 22);
            this.cmnuTagAdd.Text = "Add Tag";
            this.cmnuTagAdd.Click += new System.EventHandler(this.cmnuTagAdd_Click);
            // 
            // cmnuListTagAdd
            // 
            this.cmnuListTagAdd.Image = ((System.Drawing.Image)(resources.GetObject("cmnuListTagAdd.Image")));
            this.cmnuListTagAdd.Name = "cmnuListTagAdd";
            this.cmnuListTagAdd.Size = new System.Drawing.Size(154, 22);
            this.cmnuListTagAdd.Text = "Add list of Tags";
            this.cmnuListTagAdd.Click += new System.EventHandler(this.cmnuListTagAdd_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(151, 6);
            // 
            // cmnuTagChange
            // 
            this.cmnuTagChange.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagChange.Image")));
            this.cmnuTagChange.Name = "cmnuTagChange";
            this.cmnuTagChange.Size = new System.Drawing.Size(154, 22);
            this.cmnuTagChange.Text = "Change Tag";
            this.cmnuTagChange.Click += new System.EventHandler(this.cmnuTagChange_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(151, 6);
            // 
            // cmnuTagDelete
            // 
            this.cmnuTagDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagDelete.Image")));
            this.cmnuTagDelete.Name = "cmnuTagDelete";
            this.cmnuTagDelete.Size = new System.Drawing.Size(154, 22);
            this.cmnuTagDelete.Text = "Delete Tag";
            this.cmnuTagDelete.Click += new System.EventHandler(this.cmnuTagDelete_Click);
            // 
            // cmnuTagAllDelete
            // 
            this.cmnuTagAllDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmnuTagAllDelete.Image")));
            this.cmnuTagAllDelete.Name = "cmnuTagAllDelete";
            this.cmnuTagAllDelete.Size = new System.Drawing.Size(154, 22);
            this.cmnuTagAllDelete.Text = "Delete all Tags";
            this.cmnuTagAllDelete.Click += new System.EventHandler(this.cmnuTagAllDelete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(151, 6);
            // 
            // cmnuUp
            // 
            this.cmnuUp.Image = ((System.Drawing.Image)(resources.GetObject("cmnuUp.Image")));
            this.cmnuUp.Name = "cmnuUp";
            this.cmnuUp.Size = new System.Drawing.Size(154, 22);
            this.cmnuUp.Text = "Up";
            this.cmnuUp.Click += new System.EventHandler(this.cmnuUp_Click);
            // 
            // cmnuDown
            // 
            this.cmnuDown.Image = ((System.Drawing.Image)(resources.GetObject("cmnuDown.Image")));
            this.cmnuDown.Name = "cmnuDown";
            this.cmnuDown.Size = new System.Drawing.Size(154, 22);
            this.cmnuDown.Text = "Down";
            this.cmnuDown.Click += new System.EventHandler(this.cmnuDown_Click);
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
            // tmrTimer
            // 
            this.tmrTimer.Enabled = true;
            this.tmrTimer.Tick += new System.EventHandler(this.tmrTimer_Tick);
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
            this.Text = "Ping JP - Device {0} Properties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmConfig_FormClosing);
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.tabControl.ResumeLayout(false);
            this.pageSettings.ResumeLayout(false);
            this.pageSettings.PerformLayout();
            this.gpbTags.ResumeLayout(false);
            this.tlpTags.ResumeLayout(false);
            this.cmnuLstTags.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

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
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cmnuTagChange;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem cmnuTagAllDelete;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem cmnuTagRefresh;
        private ToolStripSeparator toolStripSeparator6;
        private ColumnHeader clmTagCode;
        private ColumnHeader clmTagIPAddress;
        private System.Windows.Forms.Timer tmrTimer;
        private CheckBox cbLog;
    }
}