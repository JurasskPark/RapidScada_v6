namespace Scada.Comm.Drivers.DrvDDEJP.View.Forms
{
    partial class FrmTag
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

        private void InitializeComponent()
        {
            lblTagName = new Label();
            txtTagName = new TextBox();
            lblTopic = new Label();
            txtTopic = new TextBox();
            lblItemName = new Label();
            txtItemName = new TextBox();
            lblFormat = new Label();
            cbTagFormat = new ComboBox();
            lblDataLength = new Label();
            nudDataLength = new NumericUpDown();
            lblChannel = new Label();
            nudChannel = new NumericUpDown();
            lblEnabled = new Label();
            ckbTagEnabled = new CheckBox();
            btnClose = new Button();
            btnSave = new Button();
            btnAdd = new Button();
            ((System.ComponentModel.ISupportInitialize)nudDataLength).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudChannel).BeginInit();
            SuspendLayout();
            // 
            // lblTagName
            // 
            lblTagName.AutoSize = true;
            lblTagName.Location = new Point(18, 20);
            lblTagName.Name = "lblTagName";
            lblTagName.Size = new Size(39, 15);
            lblTagName.TabIndex = 0;
            lblTagName.Text = "Name";
            // 
            // txtTagName
            // 
            txtTagName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTagName.Location = new Point(164, 16);
            txtTagName.Name = "txtTagName";
            txtTagName.Size = new Size(311, 23);
            txtTagName.TabIndex = 1;
            // 
            // lblTopic
            // 
            lblTopic.AutoSize = true;
            lblTopic.Location = new Point(18, 52);
            lblTopic.Name = "lblTopic";
            lblTopic.Size = new Size(35, 15);
            lblTopic.TabIndex = 2;
            lblTopic.Text = "Topic";
            // 
            // txtTopic
            // 
            txtTopic.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtTopic.Location = new Point(164, 48);
            txtTopic.Name = "txtTopic";
            txtTopic.Size = new Size(311, 23);
            txtTopic.TabIndex = 3;
            // 
            // lblItemName
            // 
            lblItemName.AutoSize = true;
            lblItemName.Location = new Point(18, 84);
            lblItemName.Name = "lblItemName";
            lblItemName.Size = new Size(64, 15);
            lblItemName.TabIndex = 4;
            lblItemName.Text = "Item name";
            // 
            // txtItemName
            // 
            txtItemName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtItemName.Location = new Point(164, 80);
            txtItemName.Name = "txtItemName";
            txtItemName.Size = new Size(311, 23);
            txtItemName.TabIndex = 5;
            // 
            // lblFormat
            // 
            lblFormat.AutoSize = true;
            lblFormat.Location = new Point(18, 116);
            lblFormat.Name = "lblFormat";
            lblFormat.Size = new Size(45, 15);
            lblFormat.TabIndex = 6;
            lblFormat.Text = "Format";
            // 
            // cbTagFormat
            // 
            cbTagFormat.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbTagFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTagFormat.FormattingEnabled = true;
            cbTagFormat.Location = new Point(164, 112);
            cbTagFormat.Name = "cbTagFormat";
            cbTagFormat.Size = new Size(311, 23);
            cbTagFormat.TabIndex = 7;
            // 
            // lblDataLength
            // 
            lblDataLength.AutoSize = true;
            lblDataLength.Location = new Point(18, 148);
            lblDataLength.Name = "lblDataLength";
            lblDataLength.Size = new Size(68, 15);
            lblDataLength.TabIndex = 8;
            lblDataLength.Text = "Data length";
            // 
            // nudDataLength
            // 
            nudDataLength.Location = new Point(164, 144);
            nudDataLength.Maximum = new decimal(new int[] { 1024, 0, 0, 0 });
            nudDataLength.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudDataLength.Name = "nudDataLength";
            nudDataLength.Size = new Size(120, 23);
            nudDataLength.TabIndex = 9;
            nudDataLength.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // lblChannel
            // 
            lblChannel.AutoSize = true;
            lblChannel.Location = new Point(18, 180);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(51, 15);
            lblChannel.TabIndex = 10;
            lblChannel.Text = "Channel";
            // 
            // nudChannel
            // 
            nudChannel.Location = new Point(164, 176);
            nudChannel.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudChannel.Name = "nudChannel";
            nudChannel.Size = new Size(120, 23);
            nudChannel.TabIndex = 11;
            // 
            // lblEnabled
            // 
            lblEnabled.AutoSize = true;
            lblEnabled.Location = new Point(18, 210);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(49, 15);
            lblEnabled.TabIndex = 12;
            lblEnabled.Text = "Enabled";
            // 
            // ckbTagEnabled
            // 
            ckbTagEnabled.AutoSize = true;
            ckbTagEnabled.Location = new Point(164, 210);
            ckbTagEnabled.Name = "ckbTagEnabled";
            ckbTagEnabled.Size = new Size(15, 14);
            ckbTagEnabled.TabIndex = 13;
            ckbTagEnabled.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.Cancel;
            btnClose.Location = new Point(391, 248);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(84, 30);
            btnClose.TabIndex = 16;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(301, 248);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(84, 30);
            btnSave.TabIndex = 15;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(301, 248);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(84, 30);
            btnAdd.TabIndex = 14;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // FrmTag
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 292);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(btnAdd);
            Controls.Add(ckbTagEnabled);
            Controls.Add(lblEnabled);
            Controls.Add(nudChannel);
            Controls.Add(lblChannel);
            Controls.Add(nudDataLength);
            Controls.Add(lblDataLength);
            Controls.Add(cbTagFormat);
            Controls.Add(lblFormat);
            Controls.Add(txtItemName);
            Controls.Add(lblItemName);
            Controls.Add(txtTopic);
            Controls.Add(lblTopic);
            Controls.Add(txtTagName);
            Controls.Add(lblTagName);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTag";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tag";
            Load += FrmTag_Load;
            ((System.ComponentModel.ISupportInitialize)nudDataLength).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudChannel).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTagName;
        private TextBox txtTagName;
        private Label lblTopic;
        private TextBox txtTopic;
        private Label lblItemName;
        private TextBox txtItemName;
        private Label lblFormat;
        private ComboBox cbTagFormat;
        private Label lblDataLength;
        private NumericUpDown nudDataLength;
        private Label lblChannel;
        private NumericUpDown nudChannel;
        private Label lblEnabled;
        private CheckBox ckbTagEnabled;
        private Button btnClose;
        private Button btnSave;
        private Button btnAdd;
    }
}
