namespace Scada.Comm.Drivers.DrvDDEJP.View.Forms
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmProject));
            tabMain = new TabControl();
            tabSettings = new TabPage();
            grpClient = new GroupBox();
            nudReconnectDelay = new NumericUpDown();
            nudRequestTimeout = new NumericUpDown();
            txtDefaultTopic = new TextBox();
            txtServiceName = new TextBox();
            lblReconnectDelay = new Label();
            lblRequestTimeout = new Label();
            lblDefaultTopic = new Label();
            lblServiceName = new Label();
            lvTags = new ListView();
            colTagName = new ColumnHeader();
            colTagChannel = new ColumnHeader();
            colTagTopic = new ColumnHeader();
            colTagItem = new ColumnHeader();
            colTagFormat = new ColumnHeader();
            colTagValue = new ColumnHeader();
            cmnuMenuListTags = new ContextMenuStrip(components);
            cmnuTagAdd = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            cmnuTagChange = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            cmnuTagDelete = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            cmnuTagUp = new ToolStripMenuItem();
            cmnuTagDown = new ToolStripMenuItem();
            btnConnect = new Button();
            btnDisconnect = new Button();
            btnSave = new Button();
            btnClose = new Button();
            tmrPoll = new System.Windows.Forms.Timer(components);
            tabMain.SuspendLayout();
            tabSettings.SuspendLayout();
            grpClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudReconnectDelay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudRequestTimeout).BeginInit();
            cmnuMenuListTags.SuspendLayout();
            SuspendLayout();
            // 
            // tabMain
            // 
            tabMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabMain.Controls.Add(tabSettings);
            tabMain.Location = new Point(12, 12);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(1060, 567);
            tabMain.TabIndex = 0;
            // 
            // tabSettings
            // 
            tabSettings.Controls.Add(grpClient);
            tabSettings.Controls.Add(lvTags);
            tabSettings.Controls.Add(btnConnect);
            tabSettings.Controls.Add(btnDisconnect);
            tabSettings.Controls.Add(btnSave);
            tabSettings.Controls.Add(btnClose);
            tabSettings.Location = new Point(4, 24);
            tabSettings.Name = "tabSettings";
            tabSettings.Padding = new Padding(3);
            tabSettings.Size = new Size(1052, 539);
            tabSettings.TabIndex = 0;
            tabSettings.Text = "Settings";
            tabSettings.UseVisualStyleBackColor = true;
            // 
            // grpClient
            // 
            grpClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpClient.Controls.Add(nudReconnectDelay);
            grpClient.Controls.Add(nudRequestTimeout);
            grpClient.Controls.Add(txtDefaultTopic);
            grpClient.Controls.Add(txtServiceName);
            grpClient.Controls.Add(lblReconnectDelay);
            grpClient.Controls.Add(lblRequestTimeout);
            grpClient.Controls.Add(lblDefaultTopic);
            grpClient.Controls.Add(lblServiceName);
            grpClient.Location = new Point(6, 6);
            grpClient.Name = "grpClient";
            grpClient.Size = new Size(1040, 96);
            grpClient.TabIndex = 0;
            grpClient.TabStop = false;
            grpClient.Text = "DDE Connection";
            // 
            // nudReconnectDelay
            // 
            nudReconnectDelay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nudReconnectDelay.Location = new Point(832, 56);
            nudReconnectDelay.Maximum = new decimal(new int[] { 600000, 0, 0, 0 });
            nudReconnectDelay.Name = "nudReconnectDelay";
            nudReconnectDelay.Size = new Size(180, 23);
            nudReconnectDelay.TabIndex = 7;
            nudReconnectDelay.ValueChanged += controls_Changed;
            // 
            // nudRequestTimeout
            // 
            nudRequestTimeout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nudRequestTimeout.Location = new Point(832, 24);
            nudRequestTimeout.Maximum = new decimal(new int[] { 600000, 0, 0, 0 });
            nudRequestTimeout.Minimum = new decimal(new int[] { 100, 0, 0, 0 });
            nudRequestTimeout.Name = "nudRequestTimeout";
            nudRequestTimeout.Size = new Size(180, 23);
            nudRequestTimeout.TabIndex = 6;
            nudRequestTimeout.Value = new decimal(new int[] { 5000, 0, 0, 0 });
            nudRequestTimeout.ValueChanged += controls_Changed;
            // 
            // txtDefaultTopic
            // 
            txtDefaultTopic.Location = new Point(174, 56);
            txtDefaultTopic.Name = "txtDefaultTopic";
            txtDefaultTopic.Size = new Size(220, 23);
            txtDefaultTopic.TabIndex = 5;
            txtDefaultTopic.TextChanged += controls_Changed;
            // 
            // txtServiceName
            // 
            txtServiceName.Location = new Point(174, 24);
            txtServiceName.Name = "txtServiceName";
            txtServiceName.Size = new Size(220, 23);
            txtServiceName.TabIndex = 4;
            txtServiceName.TextChanged += controls_Changed;
            // 
            // lblReconnectDelay
            // 
            lblReconnectDelay.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblReconnectDelay.AutoSize = true;
            lblReconnectDelay.Location = new Point(633, 60);
            lblReconnectDelay.Name = "lblReconnectDelay";
            lblReconnectDelay.Size = new Size(113, 15);
            lblReconnectDelay.TabIndex = 3;
            lblReconnectDelay.Text = "Reconnect delay, ms";
            // 
            // lblRequestTimeout
            // 
            lblRequestTimeout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRequestTimeout.AutoSize = true;
            lblRequestTimeout.Location = new Point(633, 28);
            lblRequestTimeout.Name = "lblRequestTimeout";
            lblRequestTimeout.Size = new Size(94, 15);
            lblRequestTimeout.TabIndex = 2;
            lblRequestTimeout.Text = "Request timeout";
            // 
            // lblDefaultTopic
            // 
            lblDefaultTopic.AutoSize = true;
            lblDefaultTopic.Location = new Point(18, 60);
            lblDefaultTopic.Name = "lblDefaultTopic";
            lblDefaultTopic.Size = new Size(75, 15);
            lblDefaultTopic.TabIndex = 1;
            lblDefaultTopic.Text = "Default topic";
            // 
            // lblServiceName
            // 
            lblServiceName.AutoSize = true;
            lblServiceName.Location = new Point(18, 28);
            lblServiceName.Name = "lblServiceName";
            lblServiceName.Size = new Size(77, 15);
            lblServiceName.TabIndex = 0;
            lblServiceName.Text = "Service name";
            // 
            // lvTags
            // 
            lvTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lvTags.Columns.AddRange(new ColumnHeader[] { colTagName, colTagChannel, colTagTopic, colTagItem, colTagFormat, colTagValue });
            lvTags.ContextMenuStrip = cmnuMenuListTags;
            lvTags.FullRowSelect = true;
            lvTags.GridLines = true;
            lvTags.Location = new Point(6, 108);
            lvTags.MultiSelect = false;
            lvTags.Name = "lvTags";
            lvTags.Size = new Size(1040, 389);
            lvTags.TabIndex = 1;
            lvTags.UseCompatibleStateImageBehavior = false;
            lvTags.View = System.Windows.Forms.View.Details;
            lvTags.DoubleClick += lvTags_DoubleClick;
            // 
            // colTagName
            // 
            colTagName.Text = "Name";
            colTagName.Width = 170;
            // 
            // colTagChannel
            // 
            colTagChannel.Text = "Channel";
            colTagChannel.Width = 90;
            // 
            // colTagTopic
            // 
            colTagTopic.Text = "Topic";
            colTagTopic.Width = 150;
            // 
            // colTagItem
            // 
            colTagItem.Text = "Item";
            colTagItem.Width = 260;
            // 
            // colTagFormat
            // 
            colTagFormat.Text = "Format";
            colTagFormat.Width = 110;
            // 
            // colTagValue
            // 
            colTagValue.Text = "Value";
            colTagValue.Width = 240;
            // 
            // cmnuMenuListTags
            // 
            cmnuMenuListTags.Items.AddRange(new ToolStripItem[] { cmnuTagAdd, toolStripSeparator3, cmnuTagChange, toolStripSeparator4, cmnuTagDelete, toolStripSeparator5, cmnuTagUp, cmnuTagDown });
            cmnuMenuListTags.Name = "cmnuSelectQuery";
            cmnuMenuListTags.Size = new Size(137, 132);
            // 
            // cmnuTagAdd
            // 
            cmnuTagAdd.Image = (Image)resources.GetObject("cmnuTagAdd.Image");
            cmnuTagAdd.Name = "cmnuTagAdd";
            cmnuTagAdd.Size = new Size(136, 22);
            cmnuTagAdd.Text = "Add Tag";
            cmnuTagAdd.Click += cmnuTagAdd_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(133, 6);
            // 
            // cmnuTagChange
            // 
            cmnuTagChange.Image = (Image)resources.GetObject("cmnuTagChange.Image");
            cmnuTagChange.Name = "cmnuTagChange";
            cmnuTagChange.Size = new Size(136, 22);
            cmnuTagChange.Text = "Change Tag";
            cmnuTagChange.Click += cmnuTagChange_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(133, 6);
            // 
            // cmnuTagDelete
            // 
            cmnuTagDelete.Image = (Image)resources.GetObject("cmnuTagDelete.Image");
            cmnuTagDelete.Name = "cmnuTagDelete";
            cmnuTagDelete.Size = new Size(136, 22);
            cmnuTagDelete.Text = "Delete Tag";
            cmnuTagDelete.Click += cmnuTagDelete_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(133, 6);
            toolStripSeparator5.Visible = false;
            // 
            // cmnuTagUp
            // 
            cmnuTagUp.Image = (Image)resources.GetObject("cmnuTagUp.Image");
            cmnuTagUp.Name = "cmnuTagUp";
            cmnuTagUp.Size = new Size(136, 22);
            cmnuTagUp.Text = "Up";
            cmnuTagUp.Visible = false;
            // 
            // cmnuTagDown
            // 
            cmnuTagDown.Image = (Image)resources.GetObject("cmnuTagDown.Image");
            cmnuTagDown.Name = "cmnuTagDown";
            cmnuTagDown.Size = new Size(136, 22);
            cmnuTagDown.Text = "Down";
            cmnuTagDown.Visible = false;
            // 
            // btnConnect
            // 
            btnConnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnConnect.Location = new Point(6, 503);
            btnConnect.Name = "btnConnect";
            btnConnect.Size = new Size(100, 27);
            btnConnect.TabIndex = 5;
            btnConnect.Text = "Connect";
            btnConnect.Click += btnConnect_Click;
            // 
            // btnDisconnect
            // 
            btnDisconnect.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDisconnect.Enabled = false;
            btnDisconnect.Location = new Point(112, 503);
            btnDisconnect.Name = "btnDisconnect";
            btnDisconnect.Size = new Size(100, 27);
            btnDisconnect.TabIndex = 6;
            btnDisconnect.Text = "Disconnect";
            btnDisconnect.Click += btnDisconnect_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(860, 503);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(90, 27);
            btnSave.TabIndex = 7;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(956, 503);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(90, 27);
            btnClose.TabIndex = 8;
            btnClose.Text = "Close";
            btnClose.Click += btnClose_Click;
            // 
            // tmrPoll
            // 
            tmrPoll.Interval = 1000;
            tmrPoll.Tick += tmrPoll_Tick;
            // 
            // FrmProject
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1084, 591);
            Controls.Add(tabMain);
            Name = "FrmProject";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DrvDDEJP";
            Load += FrmProject_Load;
            tabMain.ResumeLayout(false);
            tabSettings.ResumeLayout(false);
            grpClient.ResumeLayout(false);
            grpClient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudReconnectDelay).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudRequestTimeout).EndInit();
            cmnuMenuListTags.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TabControl tabMain;
        private TabPage tabSettings;
        private GroupBox grpClient;
        private NumericUpDown nudReconnectDelay;
        private NumericUpDown nudRequestTimeout;
        private TextBox txtDefaultTopic;
        private TextBox txtServiceName;
        private Label lblReconnectDelay;
        private Label lblRequestTimeout;
        private Label lblDefaultTopic;
        private Label lblServiceName;
        private ListView lvTags;
        private ColumnHeader colTagName;
        private ColumnHeader colTagChannel;
        private ColumnHeader colTagTopic;
        private ColumnHeader colTagItem;
        private ColumnHeader colTagFormat;
        private ColumnHeader colTagValue;
        private Button btnConnect;
        private Button btnDisconnect;
        private Button btnSave;
        private Button btnClose;
        private System.Windows.Forms.Timer tmrPoll;
        private ContextMenuStrip cmnuMenuListTags;
        private ToolStripMenuItem cmnuTagAdd;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cmnuTagChange;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem cmnuTagDelete;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem cmnuTagUp;
        private ToolStripMenuItem cmnuTagDown;
    }
}
