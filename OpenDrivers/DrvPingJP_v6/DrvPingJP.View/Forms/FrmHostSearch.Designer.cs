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
            lblRangeEnd = new Label();
            lblRangeStart = new Label();
            btnStart = new Button();
            txtRangeStart = new TextBox();
            txtRangeEnd = new TextBox();
            lblRangeSep = new Label();
            gpbTags = new GroupBox();
            tlpTags = new TableLayoutPanel();
            lstTags = new ListView();
            clmTagname = new ColumnHeader();
            clmTagCode = new ColumnHeader();
            clmTagIPAddress = new ColumnHeader();
            clmTagEnabled = new ColumnHeader();
            btnSave = new Button();
            btnClose = new Button();
            rdbAddHostAll = new RadioButton();
            rdbAddHostActive = new RadioButton();
            gpbTags.SuspendLayout();
            tlpTags.SuspendLayout();
            SuspendLayout();
            // 
            // lblRangeEnd
            // 
            lblRangeEnd.AutoSize = true;
            lblRangeEnd.Location = new Point(192, 9);
            lblRangeEnd.Margin = new Padding(4, 0, 4, 0);
            lblRangeEnd.Name = "lblRangeEnd";
            lblRangeEnd.Size = new Size(74, 15);
            lblRangeEnd.TabIndex = 27;
            lblRangeEnd.Text = "End of range";
            // 
            // lblRangeStart
            // 
            lblRangeStart.AutoSize = true;
            lblRangeStart.Location = new Point(13, 9);
            lblRangeStart.Margin = new Padding(4, 0, 4, 0);
            lblRangeStart.Name = "lblRangeStart";
            lblRangeStart.Size = new Size(78, 15);
            lblRangeStart.TabIndex = 24;
            lblRangeStart.Text = "Start of range";
            // 
            // btnStart
            // 
            btnStart.Location = new Point(351, 27);
            btnStart.Margin = new Padding(4, 3, 4, 3);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(88, 27);
            btnStart.TabIndex = 41;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // txtRangeStart
            // 
            txtRangeStart.Location = new Point(13, 29);
            txtRangeStart.Margin = new Padding(4, 3, 4, 3);
            txtRangeStart.Name = "txtRangeStart";
            txtRangeStart.Size = new Size(151, 23);
            txtRangeStart.TabIndex = 26;
            txtRangeStart.TextChanged += txtRangeStart_TextChanged;
            // 
            // txtRangeEnd
            // 
            txtRangeEnd.Location = new Point(192, 29);
            txtRangeEnd.Margin = new Padding(4, 3, 4, 3);
            txtRangeEnd.Name = "txtRangeEnd";
            txtRangeEnd.Size = new Size(151, 23);
            txtRangeEnd.TabIndex = 28;
            txtRangeEnd.TextChanged += txtRangeEnd_TextChanged;
            // 
            // lblRangeSep
            // 
            lblRangeSep.AutoSize = true;
            lblRangeSep.Location = new Point(172, 33);
            lblRangeSep.Margin = new Padding(4, 0, 4, 0);
            lblRangeSep.Name = "lblRangeSep";
            lblRangeSep.Size = new Size(12, 15);
            lblRangeSep.TabIndex = 42;
            lblRangeSep.Text = "-";
            // 
            // gpbTags
            // 
            gpbTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gpbTags.Controls.Add(tlpTags);
            gpbTags.Location = new Point(13, 78);
            gpbTags.Margin = new Padding(4, 3, 4, 3);
            gpbTags.Name = "gpbTags";
            gpbTags.Padding = new Padding(12, 3, 12, 12);
            gpbTags.Size = new Size(731, 397);
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
            tlpTags.Size = new Size(701, 360);
            tlpTags.TabIndex = 1;
            // 
            // lstTags
            // 
            lstTags.Alignment = ListViewAlignment.Default;
            lstTags.AutoArrange = false;
            lstTags.Columns.AddRange(new ColumnHeader[] { clmTagname, clmTagCode, clmTagIPAddress, clmTagEnabled });
            lstTags.Dock = DockStyle.Fill;
            lstTags.FullRowSelect = true;
            lstTags.GridLines = true;
            lstTags.Location = new Point(3, 3);
            lstTags.MultiSelect = false;
            lstTags.Name = "lstTags";
            lstTags.Size = new Size(695, 354);
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
            clmTagIPAddress.Width = 200;
            // 
            // clmTagEnabled
            // 
            clmTagEnabled.Text = "Enabled";
            clmTagEnabled.Width = 120;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(656, 12);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(88, 27);
            btnSave.TabIndex = 44;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(656, 45);
            btnClose.Margin = new Padding(4, 3, 4, 3);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(88, 27);
            btnClose.TabIndex = 45;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // rdbAddHostAll
            // 
            rdbAddHostAll.AutoSize = true;
            rdbAddHostAll.Location = new Point(470, 49);
            rdbAddHostAll.Name = "rdbAddHostAll";
            rdbAddHostAll.Size = new Size(93, 19);
            rdbAddHostAll.TabIndex = 15;
            rdbAddHostAll.Text = "Add all hosts";
            rdbAddHostAll.UseVisualStyleBackColor = true;
            // 
            // rdbAddHostActive
            // 
            rdbAddHostActive.AutoSize = true;
            rdbAddHostActive.Checked = true;
            rdbAddHostActive.Location = new Point(470, 16);
            rdbAddHostActive.Name = "rdbAddHostActive";
            rdbAddHostActive.Size = new Size(112, 19);
            rdbAddHostActive.TabIndex = 14;
            rdbAddHostActive.TabStop = true;
            rdbAddHostActive.Text = "Add active hosts";
            rdbAddHostActive.UseVisualStyleBackColor = true;
            // 
            // FrmHostSearch
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(757, 487);
            Controls.Add(rdbAddHostAll);
            Controls.Add(rdbAddHostActive);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(gpbTags);
            Controls.Add(lblRangeEnd);
            Controls.Add(lblRangeStart);
            Controls.Add(btnStart);
            Controls.Add(txtRangeStart);
            Controls.Add(txtRangeEnd);
            Controls.Add(lblRangeSep);
            Margin = new Padding(4, 3, 4, 3);
            MinimumSize = new Size(764, 526);
            Name = "FrmHostSearch";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Host Search";
            Load += FrmHostSearch_Load;
            gpbTags.ResumeLayout(false);
            tlpTags.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblRangeEnd;
        private System.Windows.Forms.Label lblRangeStart;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtRangeStart;
        private System.Windows.Forms.TextBox txtRangeEnd;
        private System.Windows.Forms.Label lblRangeSep;
        private System.Windows.Forms.ColumnHeader colIPAddress;
        private System.Windows.Forms.ColumnHeader clmAvgResponseTime;
        private System.Windows.Forms.ColumnHeader colLosses;
        private System.Windows.Forms.ColumnHeader colHostName;
        private System.Windows.Forms.ColumnHeader colUserName;
        private GroupBox gpbTags;
        private TableLayoutPanel tlpTags;
        private ListView lstTags;
        private ColumnHeader clmTagname;
        private ColumnHeader clmTagCode;
        private ColumnHeader clmTagIPAddress;
        private ColumnHeader clmTagEnabled;
        private Button btnSave;
        private Button btnClose;
        private RadioButton rdbAddHostAll;
        private RadioButton rdbAddHostActive;
    }
}