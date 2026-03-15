namespace Scada.Comm.Drivers.DrvDebug.View.Forms.GroupTag
{
    partial class FrmGroupTag
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
            dgvTags = new DataGridView();
            colName = new DataGridViewTextBoxColumn();
            colChannel = new DataGridViewTextBoxColumn();
            colMode = new DataGridViewTextBoxColumn();
            colIndex = new DataGridViewTextBoxColumn();
            colLength = new DataGridViewTextBoxColumn();
            colFormat = new DataGridViewTextBoxColumn();
            colSimulation = new DataGridViewTextBoxColumn();
            colPreview = new DataGridViewTextBoxColumn();
            btnAdd = new Button();
            btnEdit = new Button();
            btnDelete = new Button();
            btnUp = new Button();
            btnDown = new Button();
            btnOk = new Button();
            btnCancel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvTags).BeginInit();
            SuspendLayout();
            // 
            // dgvTags
            // 
            dgvTags.AllowUserToAddRows = false;
            dgvTags.AllowUserToDeleteRows = false;
            dgvTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTags.AutoGenerateColumns = false;
            dgvTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTags.Columns.AddRange(new DataGridViewColumn[] { colName, colChannel, colMode, colIndex, colLength, colFormat, colSimulation, colPreview });
            dgvTags.Location = new Point(12, 12);
            dgvTags.MultiSelect = false;
            dgvTags.Name = "dgvTags";
            dgvTags.ReadOnly = true;
            dgvTags.RowHeadersVisible = false;
            dgvTags.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTags.Size = new Size(960, 396);
            dgvTags.TabIndex = 7;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.HeaderText = "Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            colName.Width = 170;
            // 
            // colChannel
            // 
            colChannel.DataPropertyName = "Channel";
            colChannel.HeaderText = "Channel";
            colChannel.Name = "colChannel";
            colChannel.ReadOnly = true;
            colChannel.Width = 70;
            // 
            // colMode
            // 
            colMode.DataPropertyName = "Mode";
            colMode.HeaderText = "Mode";
            colMode.Name = "colMode";
            colMode.ReadOnly = true;
            colMode.Width = 120;
            // 
            // colIndex
            // 
            colIndex.DataPropertyName = "ArrayIndex";
            colIndex.HeaderText = "Index";
            colIndex.Name = "colIndex";
            colIndex.ReadOnly = true;
            colIndex.Width = 70;
            // 
            // colLength
            // 
            colLength.DataPropertyName = "DataLength";
            colLength.HeaderText = "Length";
            colLength.Name = "colLength";
            colLength.ReadOnly = true;
            colLength.Width = 70;
            // 
            // colFormat
            // 
            colFormat.DataPropertyName = "DataFormat";
            colFormat.HeaderText = "Format";
            colFormat.Name = "colFormat";
            colFormat.ReadOnly = true;
            colFormat.Width = 120;
            // 
            // colSimulation
            // 
            colSimulation.DataPropertyName = "SimulationKind";
            colSimulation.HeaderText = "Simulation";
            colSimulation.Name = "colSimulation";
            colSimulation.ReadOnly = true;
            colSimulation.Width = 130;
            // 
            // colPreview
            // 
            colPreview.HeaderText = "Preview";
            colPreview.Name = "colPreview";
            colPreview.ReadOnly = true;
            colPreview.Width = 180;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAdd.Location = new Point(12, 422);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 27);
            btnAdd.TabIndex = 6;
            btnAdd.Text = "Add";
            btnAdd.Click += btnAdd_Click;
            // 
            // btnEdit
            // 
            btnEdit.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnEdit.Location = new Point(108, 422);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(90, 27);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "Edit";
            btnEdit.Click += btnEdit_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(204, 422);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 27);
            btnDelete.TabIndex = 4;
            btnDelete.Text = "Delete";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUp
            // 
            btnUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnUp.Location = new Point(300, 422);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(90, 27);
            btnUp.TabIndex = 3;
            btnUp.Text = "Up";
            btnUp.Click += btnUp_Click;
            // 
            // btnDown
            // 
            btnDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDown.Location = new Point(396, 422);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(90, 27);
            btnDown.TabIndex = 2;
            btnDown.Text = "Down";
            btnDown.Click += btnDown_Click;
            // 
            // btnOk
            // 
            btnOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOk.Location = new Point(786, 422);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(90, 27);
            btnOk.TabIndex = 1;
            btnOk.Text = "OK";
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(882, 422);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 27);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmGroupTag
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 461);
            Controls.Add(btnCancel);
            Controls.Add(btnOk);
            Controls.Add(btnDown);
            Controls.Add(btnUp);
            Controls.Add(btnDelete);
            Controls.Add(btnEdit);
            Controls.Add(btnAdd);
            Controls.Add(dgvTags);
            MinimumSize = new Size(800, 482);
            Name = "FrmGroupTag";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tags";
            Load += FrmGroupTag_Load;
            ((System.ComponentModel.ISupportInitialize)dgvTags).EndInit();
            ResumeLayout(false);
        }

        private DataGridView dgvTags;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colChannel;
        private DataGridViewTextBoxColumn colMode;
        private DataGridViewTextBoxColumn colIndex;
        private DataGridViewTextBoxColumn colLength;
        private DataGridViewTextBoxColumn colFormat;
        private DataGridViewTextBoxColumn colSimulation;
        private DataGridViewTextBoxColumn colPreview;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnUp;
        private Button btnDown;
        private Button btnOk;
        private Button btnCancel;
    }
}
