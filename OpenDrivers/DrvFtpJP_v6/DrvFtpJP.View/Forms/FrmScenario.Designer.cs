
namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    partial class FrmScenario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmScenario));
            btnCancel = new Button();
            btnSave = new Button();
            txtName = new TextBox();
            lblName = new Label();
            lblEnabled = new Label();
            ckbEnabled = new CheckBox();
            txtDescription = new TextBox();
            lblDescription = new Label();
            cmnuMenu = new ContextMenuStrip(components);
            cmnuListAdd = new ToolStripMenuItem();
            cmnuLocalCreateDirectory = new ToolStripMenuItem();
            cmnuRemoteCreateDirectory = new ToolStripMenuItem();
            cmnuLocalRename = new ToolStripMenuItem();
            cmnuRemoteRename = new ToolStripMenuItem();
            cmnuLocalDeleteFile = new ToolStripMenuItem();
            cmnuLocalDeleteDirectory = new ToolStripMenuItem();
            cmnuRemoteDeleteFile = new ToolStripMenuItem();
            cmnuRemoteDeleteDirectory = new ToolStripMenuItem();
            cmnuLocalUploadFile = new ToolStripMenuItem();
            cmnuLocalUploadDirectory = new ToolStripMenuItem();
            cmnuRemoteDownloadFile = new ToolStripMenuItem();
            cmnuRemoteDownloadDirectory = new ToolStripMenuItem();
            cmnuSeparator01 = new ToolStripSeparator();
            cmnuListChange = new ToolStripMenuItem();
            cmnuSeparator02 = new ToolStripSeparator();
            cmnuListDelete = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            cmnuListUp = new ToolStripMenuItem();
            cmnuListDown = new ToolStripMenuItem();
            lstActions = new ListView();
            clmName = new ColumnHeader();
            clmEnabled = new ColumnHeader();
            cmnuMenu.SuspendLayout();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Location = new Point(888, 37);
            btnCancel.Margin = new Padding(4, 3, 4, 3);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(107, 27);
            btnCancel.TabIndex = 101;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSave.Location = new Point(888, 4);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(107, 27);
            btnSave.TabIndex = 100;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // txtName
            // 
            txtName.Location = new Point(170, 29);
            txtName.Name = "txtName";
            txtName.Size = new Size(315, 23);
            txtName.TabIndex = 121;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(11, 33);
            lblName.Name = "lblName";
            lblName.Size = new Size(39, 15);
            lblName.TabIndex = 120;
            lblName.Text = "Name";
            // 
            // lblEnabled
            // 
            lblEnabled.AutoSize = true;
            lblEnabled.Location = new Point(11, 9);
            lblEnabled.Name = "lblEnabled";
            lblEnabled.Size = new Size(49, 15);
            lblEnabled.TabIndex = 138;
            lblEnabled.Text = "Enabled";
            // 
            // ckbEnabled
            // 
            ckbEnabled.AutoSize = true;
            ckbEnabled.Location = new Point(170, 9);
            ckbEnabled.Name = "ckbEnabled";
            ckbEnabled.Size = new Size(15, 14);
            ckbEnabled.TabIndex = 139;
            ckbEnabled.UseVisualStyleBackColor = true;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(170, 58);
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(315, 23);
            txtDescription.TabIndex = 143;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(11, 62);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(67, 15);
            lblDescription.TabIndex = 142;
            lblDescription.Text = "Description";
            // 
            // cmnuMenu
            // 
            cmnuMenu.Items.AddRange(new ToolStripItem[] { cmnuListAdd, cmnuSeparator01, cmnuListChange, cmnuSeparator02, cmnuListDelete, toolStripSeparator1, cmnuListUp, cmnuListDown });
            cmnuMenu.Name = "cmnuSelectQuery";
            cmnuMenu.Size = new Size(171, 132);
            // 
            // cmnuListAdd
            // 
            cmnuListAdd.DropDownItems.AddRange(new ToolStripItem[] { cmnuLocalCreateDirectory, cmnuRemoteCreateDirectory, cmnuLocalRename, cmnuRemoteRename, cmnuLocalDeleteFile, cmnuLocalDeleteDirectory, cmnuRemoteDeleteFile, cmnuRemoteDeleteDirectory, cmnuLocalUploadFile, cmnuLocalUploadDirectory, cmnuRemoteDownloadFile, cmnuRemoteDownloadDirectory });
            cmnuListAdd.Image = (Image)resources.GetObject("cmnuListAdd.Image");
            cmnuListAdd.Name = "cmnuListAdd";
            cmnuListAdd.Size = new Size(170, 22);
            cmnuListAdd.Text = "Add Action";
            // 
            // cmnuLocalCreateDirectory
            // 
            cmnuLocalCreateDirectory.Image = (Image)resources.GetObject("cmnuLocalCreateDirectory.Image");
            cmnuLocalCreateDirectory.Name = "cmnuLocalCreateDirectory";
            cmnuLocalCreateDirectory.Size = new Size(223, 22);
            cmnuLocalCreateDirectory.Tag = "LocalCreateDirectory";
            cmnuLocalCreateDirectory.Text = "Local Create Directory";
            cmnuLocalCreateDirectory.Click += cmnuListAdd_Click;
            // 
            // cmnuRemoteCreateDirectory
            // 
            cmnuRemoteCreateDirectory.Image = (Image)resources.GetObject("cmnuRemoteCreateDirectory.Image");
            cmnuRemoteCreateDirectory.Name = "cmnuRemoteCreateDirectory";
            cmnuRemoteCreateDirectory.Size = new Size(223, 22);
            cmnuRemoteCreateDirectory.Tag = "RemoteCreateDirectory";
            cmnuRemoteCreateDirectory.Text = "Remote Create Directory";
            cmnuRemoteCreateDirectory.Click += cmnuListAdd_Click;
            // 
            // cmnuLocalRename
            // 
            cmnuLocalRename.Image = (Image)resources.GetObject("cmnuLocalRename.Image");
            cmnuLocalRename.Name = "cmnuLocalRename";
            cmnuLocalRename.Size = new Size(223, 22);
            cmnuLocalRename.Tag = "LocalRename";
            cmnuLocalRename.Text = "Local Rename";
            cmnuLocalRename.Click += cmnuListAdd_Click;
            // 
            // cmnuRemoteRename
            // 
            cmnuRemoteRename.Image = (Image)resources.GetObject("cmnuRemoteRename.Image");
            cmnuRemoteRename.Name = "cmnuRemoteRename";
            cmnuRemoteRename.Size = new Size(223, 22);
            cmnuRemoteRename.Tag = "RemoteRename";
            cmnuRemoteRename.Text = "Remote Rename";
            cmnuRemoteRename.Click += cmnuListAdd_Click;
            // 
            // cmnuLocalDeleteFile
            // 
            cmnuLocalDeleteFile.Image = (Image)resources.GetObject("cmnuLocalDeleteFile.Image");
            cmnuLocalDeleteFile.Name = "cmnuLocalDeleteFile";
            cmnuLocalDeleteFile.Size = new Size(223, 22);
            cmnuLocalDeleteFile.Tag = "LocalDeleteFile";
            cmnuLocalDeleteFile.Text = "Local Delete File";
            cmnuLocalDeleteFile.Click += cmnuListAdd_Click;
            // 
            // cmnuLocalDeleteDirectory
            // 
            cmnuLocalDeleteDirectory.Image = (Image)resources.GetObject("cmnuLocalDeleteDirectory.Image");
            cmnuLocalDeleteDirectory.Name = "cmnuLocalDeleteDirectory";
            cmnuLocalDeleteDirectory.Size = new Size(223, 22);
            cmnuLocalDeleteDirectory.Tag = "LocalDeleteDirectory";
            cmnuLocalDeleteDirectory.Text = "Local Delete Directory";
            cmnuLocalDeleteDirectory.Click += cmnuListAdd_Click;
            // 
            // cmnuRemoteDeleteFile
            // 
            cmnuRemoteDeleteFile.Image = (Image)resources.GetObject("cmnuRemoteDeleteFile.Image");
            cmnuRemoteDeleteFile.Name = "cmnuRemoteDeleteFile";
            cmnuRemoteDeleteFile.Size = new Size(223, 22);
            cmnuRemoteDeleteFile.Tag = "RemoteDeleteFile";
            cmnuRemoteDeleteFile.Text = "Remote Delete File";
            cmnuRemoteDeleteFile.Click += cmnuListAdd_Click;
            // 
            // cmnuRemoteDeleteDirectory
            // 
            cmnuRemoteDeleteDirectory.Image = (Image)resources.GetObject("cmnuRemoteDeleteDirectory.Image");
            cmnuRemoteDeleteDirectory.Name = "cmnuRemoteDeleteDirectory";
            cmnuRemoteDeleteDirectory.Size = new Size(223, 22);
            cmnuRemoteDeleteDirectory.Tag = "RemoteDeleteDirectory";
            cmnuRemoteDeleteDirectory.Text = "Remote Delete Directory";
            cmnuRemoteDeleteDirectory.Click += cmnuListAdd_Click;
            // 
            // cmnuLocalUploadFile
            // 
            cmnuLocalUploadFile.Image = (Image)resources.GetObject("cmnuLocalUploadFile.Image");
            cmnuLocalUploadFile.Name = "cmnuLocalUploadFile";
            cmnuLocalUploadFile.Size = new Size(223, 22);
            cmnuLocalUploadFile.Tag = "LocalUploadFile";
            cmnuLocalUploadFile.Text = "Local Upload File";
            cmnuLocalUploadFile.Click += cmnuListAdd_Click;
            // 
            // cmnuLocalUploadDirectory
            // 
            cmnuLocalUploadDirectory.Image = (Image)resources.GetObject("cmnuLocalUploadDirectory.Image");
            cmnuLocalUploadDirectory.Name = "cmnuLocalUploadDirectory";
            cmnuLocalUploadDirectory.Size = new Size(223, 22);
            cmnuLocalUploadDirectory.Tag = "LocalUploadDirectory";
            cmnuLocalUploadDirectory.Text = "Local Upload Directory";
            cmnuLocalUploadDirectory.Click += cmnuListAdd_Click;
            // 
            // cmnuRemoteDownloadFile
            // 
            cmnuRemoteDownloadFile.Image = (Image)resources.GetObject("cmnuRemoteDownloadFile.Image");
            cmnuRemoteDownloadFile.Name = "cmnuRemoteDownloadFile";
            cmnuRemoteDownloadFile.Size = new Size(223, 22);
            cmnuRemoteDownloadFile.Tag = "RemoteDownloadFile";
            cmnuRemoteDownloadFile.Text = "Remote Download File";
            cmnuRemoteDownloadFile.Click += cmnuListAdd_Click;
            // 
            // cmnuRemoteDownloadDirectory
            // 
            cmnuRemoteDownloadDirectory.Image = (Image)resources.GetObject("cmnuRemoteDownloadDirectory.Image");
            cmnuRemoteDownloadDirectory.Name = "cmnuRemoteDownloadDirectory";
            cmnuRemoteDownloadDirectory.Size = new Size(223, 22);
            cmnuRemoteDownloadDirectory.Tag = "RemoteDownloadDirectory";
            cmnuRemoteDownloadDirectory.Text = "Remote Download Directory";
            cmnuRemoteDownloadDirectory.Click += cmnuListAdd_Click;
            // 
            // cmnuSeparator01
            // 
            cmnuSeparator01.Name = "cmnuSeparator01";
            cmnuSeparator01.Size = new Size(167, 6);
            // 
            // cmnuListChange
            // 
            cmnuListChange.Image = (Image)resources.GetObject("cmnuListChange.Image");
            cmnuListChange.Name = "cmnuListChange";
            cmnuListChange.Size = new Size(170, 22);
            cmnuListChange.Text = "Change Action";
            cmnuListChange.Click += cmnuListChange_Click;
            // 
            // cmnuSeparator02
            // 
            cmnuSeparator02.Name = "cmnuSeparator02";
            cmnuSeparator02.Size = new Size(167, 6);
            // 
            // cmnuListDelete
            // 
            cmnuListDelete.Image = (Image)resources.GetObject("cmnuListDelete.Image");
            cmnuListDelete.Name = "cmnuListDelete";
            cmnuListDelete.Size = new Size(170, 22);
            cmnuListDelete.Text = "Delete Action";
            cmnuListDelete.Click += cmnuListDelete_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(167, 6);
            // 
            // cmnuListUp
            // 
            cmnuListUp.Image = (Image)resources.GetObject("cmnuListUp.Image");
            cmnuListUp.Name = "cmnuListUp";
            cmnuListUp.ShortcutKeys = Keys.Control | Keys.Up;
            cmnuListUp.Size = new Size(170, 22);
            cmnuListUp.Text = "Up";
            cmnuListUp.Click += cmnuListUp_Click;
            // 
            // cmnuListDown
            // 
            cmnuListDown.Image = (Image)resources.GetObject("cmnuListDown.Image");
            cmnuListDown.Name = "cmnuListDown";
            cmnuListDown.ShortcutKeys = Keys.Control | Keys.Down;
            cmnuListDown.Size = new Size(170, 22);
            cmnuListDown.Text = "Down";
            cmnuListDown.Click += cmnuListDown_Click;
            // 
            // lstActions
            // 
            lstActions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstActions.Columns.AddRange(new ColumnHeader[] { clmName, clmEnabled });
            lstActions.ContextMenuStrip = cmnuMenu;
            lstActions.FullRowSelect = true;
            lstActions.GridLines = true;
            lstActions.Location = new Point(11, 87);
            lstActions.Name = "lstActions";
            lstActions.Size = new Size(984, 344);
            lstActions.TabIndex = 148;
            lstActions.UseCompatibleStateImageBehavior = false;
            lstActions.View = System.Windows.Forms.View.Details;
            lstActions.MouseClick += lstAction_MouseClick;
            lstActions.MouseDoubleClick += lstAction_MouseDoubleClick;
            // 
            // clmName
            // 
            clmName.Text = "Name";
            clmName.Width = 208;
            // 
            // clmEnabled
            // 
            clmEnabled.Text = "Enabled";
            clmEnabled.Width = 100;
            // 
            // FrmScenario
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 443);
            Controls.Add(lstActions);
            Controls.Add(txtDescription);
            Controls.Add(lblDescription);
            Controls.Add(ckbEnabled);
            Controls.Add(lblEnabled);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(txtName);
            Controls.Add(lblName);
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmScenario";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Scenario";
            WindowState = FormWindowState.Maximized;
            Load += FrmAction_Load;
            cmnuMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private TextBox txtID;
        private Label lblID;
        private TextBox txtName;
        private Label lblName;
        private TextBox txtPath;
        private Label lblEnabled;
        private CheckBox ckbEnabled;
        private TextBox txtDescription;
        private Label lblDescription;
        private ContextMenuStrip cmnuMenu;
        private ToolStripMenuItem cmnuListAdd;
        private ToolStripSeparator cmnuSeparator01;
        private ToolStripMenuItem cmnuListChange;
        private ToolStripSeparator cmnuSeparator02;
        private ToolStripMenuItem cmnuListDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cmnuListUp;
        private ToolStripMenuItem cmnuListDown;
        private ListView lstActions;
        private ColumnHeader clmName;
        private ColumnHeader clmEnabled;
        private ToolStripMenuItem cmnuLocalCreateDirectory;
        private ToolStripMenuItem cmnuRemoteCreateDirectory;
        private ToolStripMenuItem cmnuLocalRename;
        private ToolStripMenuItem cmnuRemoteRename;
        private ToolStripMenuItem cmnuLocalDeleteFile;
        private ToolStripMenuItem cmnuRemoteDeleteFile;
        private ToolStripMenuItem cmnuLocalUploadFile;
        private ToolStripMenuItem cmnuLocalUploadDirectory;
        private ToolStripMenuItem cmnuRemoteDownloadFile;
        private ToolStripMenuItem cmnuRemoteDownloadDirectory;
        private ToolStripMenuItem cmnuLocalDeleteDirectory;
        private ToolStripMenuItem cmnuRemoteDeleteDirectory;
    }
}