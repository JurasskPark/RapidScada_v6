namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    partial class FrmHostSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmHostSearch));
            lblRangeEnd = new Label();
            lblRangeStart = new Label();
            lblType = new Label();
            btnStartStop = new Button();
            cmbRangeType = new ComboBox();
            txtRangeStart = new TextBox();
            txtRangeEnd = new TextBox();
            lblRangeSep = new Label();
            colQuality = new ColumnHeader();
            colIPAddress = new ColumnHeader();
            clmAvgResponseTime = new ColumnHeader();
            colLosses = new ColumnHeader();
            colHostName = new ColumnHeader();
            colUserName = new ColumnHeader();
            gpbTags = new GroupBox();
            tlpTags = new TableLayoutPanel();
            lstTags = new ListView();
            clmTagname = new ColumnHeader();
            clmTagCode = new ColumnHeader();
            clmTagIPAddress = new ColumnHeader();
            clmTagEnabled = new ColumnHeader();
            btnSave = new Button();
            button1 = new Button();
            gpbTags.SuspendLayout();
            tlpTags.SuspendLayout();
            SuspendLayout();
            // 
            // lblRangeEnd
            // 
            lblRangeEnd.AutoSize = true;
            lblRangeEnd.Location = new Point(306, 9);
            lblRangeEnd.Margin = new Padding(4, 0, 4, 0);
            lblRangeEnd.Name = "lblRangeEnd";
            lblRangeEnd.Size = new Size(74, 15);
            lblRangeEnd.TabIndex = 27;
            lblRangeEnd.Text = "End of range";
            // 
            // lblRangeStart
            // 
            lblRangeStart.AutoSize = true;
            lblRangeStart.Location = new Point(127, 9);
            lblRangeStart.Margin = new Padding(4, 0, 4, 0);
            lblRangeStart.Name = "lblRangeStart";
            lblRangeStart.Size = new Size(78, 15);
            lblRangeStart.TabIndex = 24;
            lblRangeStart.Text = "Start of range";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Location = new Point(13, 9);
            lblType.Margin = new Padding(4, 0, 4, 0);
            lblType.Name = "lblType";
            lblType.Size = new Size(31, 15);
            lblType.TabIndex = 20;
            lblType.Text = "Type";
            // 
            // btnStartStop
            // 
            btnStartStop.Location = new Point(465, 27);
            btnStartStop.Margin = new Padding(4, 3, 4, 3);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(88, 27);
            btnStartStop.TabIndex = 41;
            btnStartStop.Text = "Start";
            btnStartStop.UseVisualStyleBackColor = true;
            btnStartStop.Click += btnStartStop_Click;
            // 
            // cmbRangeType
            // 
            cmbRangeType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRangeType.FormattingEnabled = true;
            cmbRangeType.Items.AddRange(new object[] { "Range", "Subnet" });
            cmbRangeType.Location = new Point(13, 29);
            cmbRangeType.Margin = new Padding(4, 3, 4, 3);
            cmbRangeType.Name = "cmbRangeType";
            cmbRangeType.Size = new Size(106, 23);
            cmbRangeType.TabIndex = 22;
            cmbRangeType.SelectedIndexChanged += cmbRangeType_SelectedIndexChanged;
            // 
            // txtRangeStart
            // 
            txtRangeStart.Location = new Point(127, 29);
            txtRangeStart.Margin = new Padding(4, 3, 4, 3);
            txtRangeStart.Name = "txtRangeStart";
            txtRangeStart.Size = new Size(151, 23);
            txtRangeStart.TabIndex = 26;
            // 
            // txtRangeEnd
            // 
            txtRangeEnd.Location = new Point(306, 29);
            txtRangeEnd.Margin = new Padding(4, 3, 4, 3);
            txtRangeEnd.Name = "txtRangeEnd";
            txtRangeEnd.Size = new Size(151, 23);
            txtRangeEnd.TabIndex = 28;
            // 
            // lblRangeSep
            // 
            lblRangeSep.AutoSize = true;
            lblRangeSep.Location = new Point(286, 33);
            lblRangeSep.Margin = new Padding(4, 0, 4, 0);
            lblRangeSep.Name = "lblRangeSep";
            lblRangeSep.Size = new Size(12, 15);
            lblRangeSep.TabIndex = 42;
            lblRangeSep.Text = "-";
            // 
            // colQuality
            // 
            colQuality.Text = "К";
            colQuality.Width = 20;
            // 
            // colIPAddress
            // 
            colIPAddress.Text = "IP адрес";
            colIPAddress.Width = 99;
            // 
            // clmAvgResponseTime
            // 
            clmAvgResponseTime.Text = "Время ответа";
            clmAvgResponseTime.Width = 86;
            // 
            // colLosses
            // 
            colLosses.Text = "Потери";
            colLosses.Width = 50;
            // 
            // colHostName
            // 
            colHostName.Text = "Имя на DNS | Имя через WMI";
            colHostName.Width = 238;
            // 
            // colUserName
            // 
            colUserName.Text = "Пользователь";
            colUserName.Width = 120;
            // 
            // gpbTags
            // 
            gpbTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gpbTags.Controls.Add(tlpTags);
            gpbTags.Location = new Point(13, 60);
            gpbTags.Margin = new Padding(4, 3, 4, 3);
            gpbTags.Name = "gpbTags";
            gpbTags.Padding = new Padding(12, 3, 12, 12);
            gpbTags.Size = new Size(731, 415);
            gpbTags.TabIndex = 43;
            gpbTags.TabStop = false;
            gpbTags.Text = "Tags";
            // 
            // tlpTags
            // 
            tlpTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tlpTags.ColumnCount = 1;
            tlpTags.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 73.5955F));
            tlpTags.Controls.Add(lstTags, 0, 0);
            tlpTags.Location = new Point(15, 22);
            tlpTags.Name = "tlpTags";
            tlpTags.RowCount = 1;
            tlpTags.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tlpTags.Size = new Size(701, 378);
            tlpTags.TabIndex = 1;
            // 
            // lstTags
            // 
            lstTags.Alignment = ListViewAlignment.Default;
            lstTags.Columns.AddRange(new ColumnHeader[] { clmTagname, clmTagCode, clmTagIPAddress, clmTagEnabled });
            lstTags.Dock = DockStyle.Fill;
            lstTags.FullRowSelect = true;
            lstTags.GridLines = true;
            lstTags.Location = new Point(3, 3);
            lstTags.MultiSelect = false;
            lstTags.Name = "lstTags";
            lstTags.Size = new Size(695, 372);
            lstTags.TabIndex = 2;
            lstTags.UseCompatibleStateImageBehavior = false;
            lstTags.View = System.Windows.Forms.View.Details;
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
            clmTagIPAddress.Width = 110;
            // 
            // clmTagEnabled
            // 
            clmTagEnabled.Text = "Enabled";
            clmTagEnabled.Width = 80;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(561, 27);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 44;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(657, 27);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(88, 27);
            button1.TabIndex = 45;
            button1.Text = "Close";
            button1.UseVisualStyleBackColor = true;
            // 
            // FrmHostSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 487);
            Controls.Add(button1);
            Controls.Add(btnSave);
            Controls.Add(gpbTags);
            Controls.Add(lblRangeEnd);
            Controls.Add(lblRangeStart);
            Controls.Add(lblType);
            Controls.Add(btnStartStop);
            Controls.Add(cmbRangeType);
            Controls.Add(txtRangeStart);
            Controls.Add(txtRangeEnd);
            Controls.Add(lblRangeSep);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(764, 526);
            Name = "FrmHostSearch";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Пингвинатор";
            Load += FrmHostSearch_Load;
            gpbTags.ResumeLayout(false);
            tlpTags.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRangeEnd;
        private System.Windows.Forms.Label lblRangeStart;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.ComboBox cmbRangeType;
        private System.Windows.Forms.TextBox txtRangeStart;
        private System.Windows.Forms.TextBox txtRangeEnd;
        private System.Windows.Forms.Label lblRangeSep;
        private System.Windows.Forms.ColumnHeader colIPAddress;
        private System.Windows.Forms.ColumnHeader clmAvgResponseTime;
        private System.Windows.Forms.ColumnHeader colLosses;
        private System.Windows.Forms.ColumnHeader colHostName;
        private ProgressBarEx prg_ScanProgress;
        public System.Windows.Forms.ColumnHeader colQuality;
        private System.Windows.Forms.ColumnHeader colUserName;
        private GroupBox gpbTags;
        private TableLayoutPanel tlpTags;
        private ListView lstTags;
        private ColumnHeader clmTagname;
        private ColumnHeader clmTagCode;
        private ColumnHeader clmTagIPAddress;
        private ColumnHeader clmTagEnabled;
        private Button btnSave;
        private Button button1;
    }
}