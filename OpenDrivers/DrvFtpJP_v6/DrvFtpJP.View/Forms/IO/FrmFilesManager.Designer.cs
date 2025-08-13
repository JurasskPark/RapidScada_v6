namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    partial class FrmFilesManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFilesManager));
            stsMain = new StatusStrip();
            stsMainStatus = new ToolStripStatusLabel();
            tlpPanelMain = new TableLayoutPanel();
            tlpPanelRight = new TableLayoutPanel();
            lblStatus = new Label();
            tlpPanelRightHeader = new TableLayoutPanel();
            txtRemoteCurrentPath = new TextBox();
            btnRemoteRootDirectory = new Button();
            btnRemoteUpOneDirectory = new Button();
            lstRemoteFiles = new ListView();
            cmnuRemoteOperation = new ContextMenuStrip(components);
            cmnuRemoteOpearationDownload = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            cmnuRemoteOpearationRefresh = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            cmnuRemoteOpearationCreateDirectory = new ToolStripMenuItem();
            cmnuRemoteOpearationRename = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            cmnuRemoteOpearationDelete = new ToolStripMenuItem();
            imgList16 = new ImageList(components);
            rtbStatus = new RichTextBox();
            tlpPanelLeft = new TableLayoutPanel();
            tlpPanelLeftHeader = new TableLayoutPanel();
            cmbLocalDrivers = new ComboBox();
            txtLocalCurrentPath = new TextBox();
            btnLocalRootDirectory = new Button();
            btnLocalUpOneDirectory = new Button();
            lstLocalFiles = new ListView();
            cmnuLocalOperation = new ContextMenuStrip(components);
            cmnuLocalOparationUpload = new ToolStripMenuItem();
            cmnuLocalOparationSeparator01 = new ToolStripSeparator();
            cmnuLocalOparationRefresh = new ToolStripMenuItem();
            cmnuLocalOparationSeparator02 = new ToolStripSeparator();
            cmnuLocalOparationCreateDirectory = new ToolStripMenuItem();
            cmnuLocalOparationRename = new ToolStripMenuItem();
            cmnuLocalOparationSeparator03 = new ToolStripSeparator();
            cmnuLocalOparationDelete = new ToolStripMenuItem();
            rtbFilesOperation = new RichTextBox();
            lblFileOperation = new Label();
            stsMain.SuspendLayout();
            tlpPanelMain.SuspendLayout();
            tlpPanelRight.SuspendLayout();
            tlpPanelRightHeader.SuspendLayout();
            cmnuRemoteOperation.SuspendLayout();
            tlpPanelLeft.SuspendLayout();
            tlpPanelLeftHeader.SuspendLayout();
            cmnuLocalOperation.SuspendLayout();
            SuspendLayout();
            // 
            // stsMain
            // 
            stsMain.Items.AddRange(new ToolStripItem[] { stsMainStatus });
            stsMain.Location = new Point(0, 776);
            stsMain.Name = "stsMain";
            stsMain.Padding = new Padding(1, 0, 16, 0);
            stsMain.RenderMode = ToolStripRenderMode.ManagerRenderMode;
            stsMain.Size = new Size(1045, 22);
            stsMain.TabIndex = 1;
            stsMain.Text = "statusMain";
            // 
            // stsMainStatus
            // 
            stsMainStatus.Name = "stsMainStatus";
            stsMainStatus.Size = new Size(0, 17);
            // 
            // tlpPanelMain
            // 
            tlpPanelMain.ColumnCount = 2;
            tlpPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpPanelMain.Controls.Add(tlpPanelRight, 1, 0);
            tlpPanelMain.Controls.Add(tlpPanelLeft, 0, 0);
            tlpPanelMain.Dock = DockStyle.Fill;
            tlpPanelMain.Location = new Point(0, 0);
            tlpPanelMain.Name = "tlpPanelMain";
            tlpPanelMain.RowCount = 1;
            tlpPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanelMain.Size = new Size(1045, 776);
            tlpPanelMain.TabIndex = 2;
            // 
            // tlpPanelRight
            // 
            tlpPanelRight.ColumnCount = 1;
            tlpPanelRight.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPanelRight.Controls.Add(lblStatus, 0, 2);
            tlpPanelRight.Controls.Add(tlpPanelRightHeader, 0, 0);
            tlpPanelRight.Controls.Add(lstRemoteFiles, 0, 1);
            tlpPanelRight.Controls.Add(rtbStatus, 0, 3);
            tlpPanelRight.Dock = DockStyle.Fill;
            tlpPanelRight.Location = new Point(525, 3);
            tlpPanelRight.Name = "tlpPanelRight";
            tlpPanelRight.RowCount = 4;
            tlpPanelRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpPanelRight.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanelRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpPanelRight.RowStyles.Add(new RowStyle(SizeType.Absolute, 233F));
            tlpPanelRight.Size = new Size(517, 770);
            tlpPanelRight.TabIndex = 1;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Dock = DockStyle.Fill;
            lblStatus.Location = new Point(3, 501);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(511, 36);
            lblStatus.TabIndex = 8;
            lblStatus.Text = "Status";
            lblStatus.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tlpPanelRightHeader
            // 
            tlpPanelRightHeader.ColumnCount = 4;
            tlpPanelRightHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 0F));
            tlpPanelRightHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPanelRightHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tlpPanelRightHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tlpPanelRightHeader.Controls.Add(txtRemoteCurrentPath, 1, 0);
            tlpPanelRightHeader.Controls.Add(btnRemoteRootDirectory, 2, 0);
            tlpPanelRightHeader.Controls.Add(btnRemoteUpOneDirectory, 3, 0);
            tlpPanelRightHeader.Dock = DockStyle.Fill;
            tlpPanelRightHeader.Location = new Point(3, 3);
            tlpPanelRightHeader.Name = "tlpPanelRightHeader";
            tlpPanelRightHeader.RowCount = 1;
            tlpPanelRightHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanelRightHeader.Size = new Size(511, 30);
            tlpPanelRightHeader.TabIndex = 4;
            // 
            // txtRemoteCurrentPath
            // 
            txtRemoteCurrentPath.Dock = DockStyle.Fill;
            txtRemoteCurrentPath.Location = new Point(3, 3);
            txtRemoteCurrentPath.Name = "txtRemoteCurrentPath";
            txtRemoteCurrentPath.Size = new Size(441, 23);
            txtRemoteCurrentPath.TabIndex = 2;
            // 
            // btnRemoteRootDirectory
            // 
            btnRemoteRootDirectory.Location = new Point(450, 3);
            btnRemoteRootDirectory.Name = "btnRemoteRootDirectory";
            btnRemoteRootDirectory.Size = new Size(26, 23);
            btnRemoteRootDirectory.TabIndex = 3;
            btnRemoteRootDirectory.Text = "\\";
            btnRemoteRootDirectory.UseVisualStyleBackColor = true;
            btnRemoteRootDirectory.Click += btnRemoteRootDirectory_Click;
            // 
            // btnRemoteUpOneDirectory
            // 
            btnRemoteUpOneDirectory.Location = new Point(482, 3);
            btnRemoteUpOneDirectory.Name = "btnRemoteUpOneDirectory";
            btnRemoteUpOneDirectory.Size = new Size(26, 23);
            btnRemoteUpOneDirectory.TabIndex = 4;
            btnRemoteUpOneDirectory.Text = ". .";
            btnRemoteUpOneDirectory.UseVisualStyleBackColor = true;
            btnRemoteUpOneDirectory.Click += btnRemoteUpOneDirectory_Click;
            // 
            // lstRemoteFiles
            // 
            lstRemoteFiles.ContextMenuStrip = cmnuRemoteOperation;
            lstRemoteFiles.Dock = DockStyle.Fill;
            lstRemoteFiles.FullRowSelect = true;
            lstRemoteFiles.GridLines = true;
            lstRemoteFiles.GroupImageList = imgList16;
            lstRemoteFiles.Location = new Point(3, 39);
            lstRemoteFiles.MultiSelect = false;
            lstRemoteFiles.Name = "lstRemoteFiles";
            lstRemoteFiles.Size = new Size(511, 459);
            lstRemoteFiles.TabIndex = 3;
            lstRemoteFiles.UseCompatibleStateImageBehavior = false;
            lstRemoteFiles.View = System.Windows.Forms.View.Details;
            lstRemoteFiles.DoubleClick += lstRemoteFiles_DoubleClick;
            // 
            // cmnuRemoteOperation
            // 
            cmnuRemoteOperation.Items.AddRange(new ToolStripItem[] { cmnuRemoteOpearationDownload, toolStripSeparator4, cmnuRemoteOpearationRefresh, toolStripSeparator5, cmnuRemoteOpearationCreateDirectory, cmnuRemoteOpearationRename, toolStripSeparator6, cmnuRemoteOpearationDelete });
            cmnuRemoteOperation.Name = "cmnuLocal";
            cmnuRemoteOperation.Size = new Size(160, 132);
            // 
            // cmnuRemoteOpearationDownload
            // 
            cmnuRemoteOpearationDownload.Image = (Image)resources.GetObject("cmnuRemoteOpearationDownload.Image");
            cmnuRemoteOpearationDownload.Name = "cmnuRemoteOpearationDownload";
            cmnuRemoteOpearationDownload.Size = new Size(159, 22);
            cmnuRemoteOpearationDownload.Text = "Download";
            cmnuRemoteOpearationDownload.Click += cmnuRemoteOperationDownload_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(156, 6);
            // 
            // cmnuRemoteOpearationRefresh
            // 
            cmnuRemoteOpearationRefresh.Image = (Image)resources.GetObject("cmnuRemoteOpearationRefresh.Image");
            cmnuRemoteOpearationRefresh.Name = "cmnuRemoteOpearationRefresh";
            cmnuRemoteOpearationRefresh.Size = new Size(159, 22);
            cmnuRemoteOpearationRefresh.Text = "Refresh";
            cmnuRemoteOpearationRefresh.Click += cmnuRemoteOperationRefresh_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(156, 6);
            // 
            // cmnuRemoteOpearationCreateDirectory
            // 
            cmnuRemoteOpearationCreateDirectory.Image = (Image)resources.GetObject("cmnuRemoteOpearationCreateDirectory.Image");
            cmnuRemoteOpearationCreateDirectory.Name = "cmnuRemoteOpearationCreateDirectory";
            cmnuRemoteOpearationCreateDirectory.Size = new Size(159, 22);
            cmnuRemoteOpearationCreateDirectory.Text = "Create Directory";
            cmnuRemoteOpearationCreateDirectory.Click += cmnuRemoteOperationCreateDirectory_Click;
            // 
            // cmnuRemoteOpearationRename
            // 
            cmnuRemoteOpearationRename.Image = (Image)resources.GetObject("cmnuRemoteOpearationRename.Image");
            cmnuRemoteOpearationRename.Name = "cmnuRemoteOpearationRename";
            cmnuRemoteOpearationRename.Size = new Size(159, 22);
            cmnuRemoteOpearationRename.Text = "Rename";
            cmnuRemoteOpearationRename.Click += cmnuRemoteOperationRename_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(156, 6);
            // 
            // cmnuRemoteOpearationDelete
            // 
            cmnuRemoteOpearationDelete.Image = (Image)resources.GetObject("cmnuRemoteOpearationDelete.Image");
            cmnuRemoteOpearationDelete.Name = "cmnuRemoteOpearationDelete";
            cmnuRemoteOpearationDelete.Size = new Size(159, 22);
            cmnuRemoteOpearationDelete.Text = "Delete";
            cmnuRemoteOpearationDelete.Click += cmnuRemoteOperationDelete_Click;
            // 
            // imgList16
            // 
            imgList16.ColorDepth = ColorDepth.Depth32Bit;
            imgList16.ImageStream = (ImageListStreamer)resources.GetObject("imgList16.ImageStream");
            imgList16.TransparentColor = Color.Transparent;
            imgList16.Images.SetKeyName(0, "hdd.png");
            imgList16.Images.SetKeyName(1, "cd.png");
            imgList16.Images.SetKeyName(2, "arrowup.png");
            imgList16.Images.SetKeyName(3, "folder.png");
            imgList16.Images.SetKeyName(4, "folderlink.png");
            imgList16.Images.SetKeyName(5, "file.png");
            imgList16.Images.SetKeyName(6, "3gp.png");
            imgList16.Images.SetKeyName(7, "7z.png");
            imgList16.Images.SetKeyName(8, "ace.png");
            imgList16.Images.SetKeyName(9, "ai.png");
            imgList16.Images.SetKeyName(10, "aif.png");
            imgList16.Images.SetKeyName(11, "aiff.png");
            imgList16.Images.SetKeyName(12, "amr.png");
            imgList16.Images.SetKeyName(13, "asf.png");
            imgList16.Images.SetKeyName(14, "asx.png");
            imgList16.Images.SetKeyName(15, "bat.png");
            imgList16.Images.SetKeyName(16, "bin.png");
            imgList16.Images.SetKeyName(17, "bmp.png");
            imgList16.Images.SetKeyName(18, "bup.png");
            imgList16.Images.SetKeyName(19, "cab.png");
            imgList16.Images.SetKeyName(20, "cbr.png");
            imgList16.Images.SetKeyName(21, "cda.png");
            imgList16.Images.SetKeyName(22, "cdl.png");
            imgList16.Images.SetKeyName(23, "cdr.png");
            imgList16.Images.SetKeyName(24, "chm.png");
            imgList16.Images.SetKeyName(25, "dat.png");
            imgList16.Images.SetKeyName(26, "divx.png");
            imgList16.Images.SetKeyName(27, "dll.png");
            imgList16.Images.SetKeyName(28, "dmg.png");
            imgList16.Images.SetKeyName(29, "doc.png");
            imgList16.Images.SetKeyName(30, "dss.png");
            imgList16.Images.SetKeyName(31, "dvf.png");
            imgList16.Images.SetKeyName(32, "dwg.png");
            imgList16.Images.SetKeyName(33, "eml.png");
            imgList16.Images.SetKeyName(34, "eps.png");
            imgList16.Images.SetKeyName(35, "exe.png");
            imgList16.Images.SetKeyName(36, "fla.png");
            imgList16.Images.SetKeyName(37, "flv.png");
            imgList16.Images.SetKeyName(38, "gif.png");
            imgList16.Images.SetKeyName(39, "gz.png");
            imgList16.Images.SetKeyName(40, "hqx.png");
            imgList16.Images.SetKeyName(41, "htm.png");
            imgList16.Images.SetKeyName(42, "html.png");
            imgList16.Images.SetKeyName(43, "ifo.png");
            imgList16.Images.SetKeyName(44, "indd.png");
            imgList16.Images.SetKeyName(45, "iso.png");
            imgList16.Images.SetKeyName(46, "jar.png");
            imgList16.Images.SetKeyName(47, "jpeg.png");
            imgList16.Images.SetKeyName(48, "jpg.png");
            imgList16.Images.SetKeyName(49, "lnk.png");
            imgList16.Images.SetKeyName(50, "log.png");
            imgList16.Images.SetKeyName(51, "m4a.png");
            imgList16.Images.SetKeyName(52, "m4b.png");
            imgList16.Images.SetKeyName(53, "m4p.png");
            imgList16.Images.SetKeyName(54, "m4v.png");
            imgList16.Images.SetKeyName(55, "mcd.png");
            imgList16.Images.SetKeyName(56, "mdb.png");
            imgList16.Images.SetKeyName(57, "mid.png");
            imgList16.Images.SetKeyName(58, "mov.png");
            imgList16.Images.SetKeyName(59, "mp2.png");
            imgList16.Images.SetKeyName(60, "mp4.png");
            imgList16.Images.SetKeyName(61, "mpeg.png");
            imgList16.Images.SetKeyName(62, "mpg.png");
            imgList16.Images.SetKeyName(63, "msi.png");
            imgList16.Images.SetKeyName(64, "mswmm.png");
            imgList16.Images.SetKeyName(65, "ogg.png");
            imgList16.Images.SetKeyName(66, "pdf.png");
            imgList16.Images.SetKeyName(67, "png.png");
            imgList16.Images.SetKeyName(68, "pps.png");
            imgList16.Images.SetKeyName(69, "ps.png");
            imgList16.Images.SetKeyName(70, "psd.png");
            imgList16.Images.SetKeyName(71, "pst.png");
            imgList16.Images.SetKeyName(72, "ptb.png");
            imgList16.Images.SetKeyName(73, "pub.png");
            imgList16.Images.SetKeyName(74, "qbb.png");
            imgList16.Images.SetKeyName(75, "qbw.png");
            imgList16.Images.SetKeyName(76, "qxd.png");
            imgList16.Images.SetKeyName(77, "ram.png");
            imgList16.Images.SetKeyName(78, "rar.png");
            imgList16.Images.SetKeyName(79, "rm.png");
            imgList16.Images.SetKeyName(80, "rmvb.png");
            imgList16.Images.SetKeyName(81, "rtf.png");
            imgList16.Images.SetKeyName(82, "sea.png");
            imgList16.Images.SetKeyName(83, "ses.png");
            imgList16.Images.SetKeyName(84, "sit.png");
            imgList16.Images.SetKeyName(85, "sitx.png");
            imgList16.Images.SetKeyName(86, "ss.png");
            imgList16.Images.SetKeyName(87, "swf.png");
            imgList16.Images.SetKeyName(88, "tgz.png");
            imgList16.Images.SetKeyName(89, "thm.png");
            imgList16.Images.SetKeyName(90, "tif.png");
            imgList16.Images.SetKeyName(91, "tmp.png");
            imgList16.Images.SetKeyName(92, "torrent.png");
            imgList16.Images.SetKeyName(93, "ttf.png");
            imgList16.Images.SetKeyName(94, "txt.png");
            imgList16.Images.SetKeyName(95, "vcd.png");
            imgList16.Images.SetKeyName(96, "vob.png");
            imgList16.Images.SetKeyName(97, "wav.png");
            imgList16.Images.SetKeyName(98, "wma.png");
            imgList16.Images.SetKeyName(99, "wmv.png");
            imgList16.Images.SetKeyName(100, "wps.png");
            imgList16.Images.SetKeyName(101, "xls.png");
            imgList16.Images.SetKeyName(102, "xpi.png");
            imgList16.Images.SetKeyName(103, "zip.png");
            // 
            // rtbStatus
            // 
            rtbStatus.BorderStyle = BorderStyle.FixedSingle;
            rtbStatus.DetectUrls = false;
            rtbStatus.Dock = DockStyle.Fill;
            rtbStatus.Font = new Font("Consolas", 8.25F);
            rtbStatus.Location = new Point(3, 540);
            rtbStatus.Name = "rtbStatus";
            rtbStatus.ReadOnly = true;
            rtbStatus.Size = new Size(511, 227);
            rtbStatus.TabIndex = 5;
            rtbStatus.Text = "";
            // 
            // tlpPanelLeft
            // 
            tlpPanelLeft.ColumnCount = 1;
            tlpPanelLeft.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpPanelLeft.Controls.Add(tlpPanelLeftHeader, 0, 0);
            tlpPanelLeft.Controls.Add(lstLocalFiles, 0, 1);
            tlpPanelLeft.Controls.Add(rtbFilesOperation, 0, 3);
            tlpPanelLeft.Controls.Add(lblFileOperation, 0, 2);
            tlpPanelLeft.Dock = DockStyle.Fill;
            tlpPanelLeft.Location = new Point(3, 3);
            tlpPanelLeft.Name = "tlpPanelLeft";
            tlpPanelLeft.RowCount = 4;
            tlpPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpPanelLeft.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlpPanelLeft.RowStyles.Add(new RowStyle(SizeType.Absolute, 233F));
            tlpPanelLeft.Size = new Size(516, 770);
            tlpPanelLeft.TabIndex = 0;
            // 
            // tlpPanelLeftHeader
            // 
            tlpPanelLeftHeader.ColumnCount = 4;
            tlpPanelLeftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15.99147F));
            tlpPanelLeftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 84.00853F));
            tlpPanelLeftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tlpPanelLeftHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 32F));
            tlpPanelLeftHeader.Controls.Add(cmbLocalDrivers, 0, 0);
            tlpPanelLeftHeader.Controls.Add(txtLocalCurrentPath, 1, 0);
            tlpPanelLeftHeader.Controls.Add(btnLocalRootDirectory, 2, 0);
            tlpPanelLeftHeader.Controls.Add(btnLocalUpOneDirectory, 3, 0);
            tlpPanelLeftHeader.Dock = DockStyle.Fill;
            tlpPanelLeftHeader.Location = new Point(3, 3);
            tlpPanelLeftHeader.Name = "tlpPanelLeftHeader";
            tlpPanelLeftHeader.RowCount = 1;
            tlpPanelLeftHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanelLeftHeader.Size = new Size(510, 30);
            tlpPanelLeftHeader.TabIndex = 0;
            // 
            // cmbLocalDrivers
            // 
            cmbLocalDrivers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLocalDrivers.FormattingEnabled = true;
            cmbLocalDrivers.Location = new Point(3, 3);
            cmbLocalDrivers.Name = "cmbLocalDrivers";
            cmbLocalDrivers.Size = new Size(65, 23);
            cmbLocalDrivers.TabIndex = 0;
            cmbLocalDrivers.SelectedIndexChanged += cmbLocalDrivers_SelectedIndexChanged;
            // 
            // txtLocalCurrentPath
            // 
            txtLocalCurrentPath.Dock = DockStyle.Fill;
            txtLocalCurrentPath.Location = new Point(74, 3);
            txtLocalCurrentPath.Name = "txtLocalCurrentPath";
            txtLocalCurrentPath.Size = new Size(368, 23);
            txtLocalCurrentPath.TabIndex = 1;
            // 
            // btnLocalRootDirectory
            // 
            btnLocalRootDirectory.Location = new Point(448, 3);
            btnLocalRootDirectory.Name = "btnLocalRootDirectory";
            btnLocalRootDirectory.Size = new Size(26, 23);
            btnLocalRootDirectory.TabIndex = 2;
            btnLocalRootDirectory.Text = "\\";
            btnLocalRootDirectory.UseVisualStyleBackColor = true;
            btnLocalRootDirectory.Click += btnLocalRootDirectory_Click;
            // 
            // btnLocalUpOneDirectory
            // 
            btnLocalUpOneDirectory.Location = new Point(480, 3);
            btnLocalUpOneDirectory.Name = "btnLocalUpOneDirectory";
            btnLocalUpOneDirectory.Size = new Size(27, 23);
            btnLocalUpOneDirectory.TabIndex = 3;
            btnLocalUpOneDirectory.Text = ". .";
            btnLocalUpOneDirectory.UseVisualStyleBackColor = true;
            btnLocalUpOneDirectory.Click += btnLocalUpOneDirectory_Click;
            // 
            // lstLocalFiles
            // 
            lstLocalFiles.ContextMenuStrip = cmnuLocalOperation;
            lstLocalFiles.Dock = DockStyle.Fill;
            lstLocalFiles.FullRowSelect = true;
            lstLocalFiles.GridLines = true;
            lstLocalFiles.GroupImageList = imgList16;
            lstLocalFiles.Location = new Point(3, 39);
            lstLocalFiles.MultiSelect = false;
            lstLocalFiles.Name = "lstLocalFiles";
            lstLocalFiles.Size = new Size(510, 459);
            lstLocalFiles.TabIndex = 1;
            lstLocalFiles.UseCompatibleStateImageBehavior = false;
            lstLocalFiles.View = System.Windows.Forms.View.Details;
            lstLocalFiles.MouseDoubleClick += lstLocalFiles_MouseDoubleClick;
            // 
            // cmnuLocalOperation
            // 
            cmnuLocalOperation.Items.AddRange(new ToolStripItem[] { cmnuLocalOparationUpload, cmnuLocalOparationSeparator01, cmnuLocalOparationRefresh, cmnuLocalOparationSeparator02, cmnuLocalOparationCreateDirectory, cmnuLocalOparationRename, cmnuLocalOparationSeparator03, cmnuLocalOparationDelete });
            cmnuLocalOperation.Name = "cmnuLocalOperation";
            cmnuLocalOperation.Size = new Size(160, 132);
            // 
            // cmnuLocalOparationUpload
            // 
            cmnuLocalOparationUpload.Image = (Image)resources.GetObject("cmnuLocalOparationUpload.Image");
            cmnuLocalOparationUpload.Name = "cmnuLocalOparationUpload";
            cmnuLocalOparationUpload.Size = new Size(159, 22);
            cmnuLocalOparationUpload.Text = "Upload";
            cmnuLocalOparationUpload.Click += cmnuLocalOperationUpload_Click;
            // 
            // cmnuLocalOparationSeparator01
            // 
            cmnuLocalOparationSeparator01.Name = "cmnuLocalOparationSeparator01";
            cmnuLocalOparationSeparator01.Size = new Size(156, 6);
            // 
            // cmnuLocalOparationRefresh
            // 
            cmnuLocalOparationRefresh.Image = (Image)resources.GetObject("cmnuLocalOparationRefresh.Image");
            cmnuLocalOparationRefresh.Name = "cmnuLocalOparationRefresh";
            cmnuLocalOparationRefresh.Size = new Size(159, 22);
            cmnuLocalOparationRefresh.Text = "Refresh";
            cmnuLocalOparationRefresh.Click += cmnuLocalOperationRefresh_Click;
            // 
            // cmnuLocalOparationSeparator02
            // 
            cmnuLocalOparationSeparator02.Name = "cmnuLocalOparationSeparator02";
            cmnuLocalOparationSeparator02.Size = new Size(156, 6);
            // 
            // cmnuLocalOparationCreateDirectory
            // 
            cmnuLocalOparationCreateDirectory.Image = (Image)resources.GetObject("cmnuLocalOparationCreateDirectory.Image");
            cmnuLocalOparationCreateDirectory.Name = "cmnuLocalOparationCreateDirectory";
            cmnuLocalOparationCreateDirectory.Size = new Size(159, 22);
            cmnuLocalOparationCreateDirectory.Text = "Create Directory";
            cmnuLocalOparationCreateDirectory.Click += cmnuLocalOperationCreateDirectory_Click;
            // 
            // cmnuLocalOparationRename
            // 
            cmnuLocalOparationRename.Image = (Image)resources.GetObject("cmnuLocalOparationRename.Image");
            cmnuLocalOparationRename.Name = "cmnuLocalOparationRename";
            cmnuLocalOparationRename.Size = new Size(159, 22);
            cmnuLocalOparationRename.Text = "Rename";
            cmnuLocalOparationRename.Click += cmnuLocalOperationRename_Click;
            // 
            // cmnuLocalOparationSeparator03
            // 
            cmnuLocalOparationSeparator03.Name = "cmnuLocalOparationSeparator03";
            cmnuLocalOparationSeparator03.Size = new Size(156, 6);
            // 
            // cmnuLocalOparationDelete
            // 
            cmnuLocalOparationDelete.Image = (Image)resources.GetObject("cmnuLocalOparationDelete.Image");
            cmnuLocalOparationDelete.Name = "cmnuLocalOparationDelete";
            cmnuLocalOparationDelete.Size = new Size(159, 22);
            cmnuLocalOparationDelete.Text = "Delete";
            cmnuLocalOparationDelete.Click += cmnuLocalOperationDelete_Click;
            // 
            // rtbFilesOperation
            // 
            rtbFilesOperation.BorderStyle = BorderStyle.FixedSingle;
            rtbFilesOperation.DetectUrls = false;
            rtbFilesOperation.Dock = DockStyle.Fill;
            rtbFilesOperation.Font = new Font("Consolas", 8.25F);
            rtbFilesOperation.Location = new Point(3, 540);
            rtbFilesOperation.Name = "rtbFilesOperation";
            rtbFilesOperation.ReadOnly = true;
            rtbFilesOperation.Size = new Size(510, 227);
            rtbFilesOperation.TabIndex = 6;
            rtbFilesOperation.Text = "";
            // 
            // lblFileOperation
            // 
            lblFileOperation.AutoSize = true;
            lblFileOperation.Dock = DockStyle.Fill;
            lblFileOperation.Location = new Point(3, 501);
            lblFileOperation.Name = "lblFileOperation";
            lblFileOperation.Size = new Size(510, 36);
            lblFileOperation.TabIndex = 7;
            lblFileOperation.Text = "File operations";
            lblFileOperation.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FrmFilesManager
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1045, 798);
            Controls.Add(tlpPanelMain);
            Controls.Add(stsMain);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            Name = "FrmFilesManager";
            Text = "FTP client";
            Load += OnLoad;
            stsMain.ResumeLayout(false);
            stsMain.PerformLayout();
            tlpPanelMain.ResumeLayout(false);
            tlpPanelRight.ResumeLayout(false);
            tlpPanelRight.PerformLayout();
            tlpPanelRightHeader.ResumeLayout(false);
            tlpPanelRightHeader.PerformLayout();
            cmnuRemoteOperation.ResumeLayout(false);
            tlpPanelLeft.ResumeLayout(false);
            tlpPanelLeft.PerformLayout();
            tlpPanelLeftHeader.ResumeLayout(false);
            tlpPanelLeftHeader.PerformLayout();
            cmnuLocalOperation.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private ComboBox cmbLocalDrivers;
        private ImageList imgList16;
        private ListView lstLocalFiles;
        private ListView lstRemoteFiles;
        private RichTextBox rtbFilesOperation;
        private RichTextBox rtbStatus;
        private TableLayoutPanel tlpPanelLeft;
        private TableLayoutPanel tlpPanelLeftHeader;
        private TableLayoutPanel tlpPanelMain;
        private TableLayoutPanel tlpPanelRight;
        private TableLayoutPanel tlpPanelRightHeader;
        private TextBox txtLocalCurrentPath;
        private TextBox txtRemoteCurrentPath;
        public System.Windows.Forms.StatusStrip stsMain;
        private ContextMenuStrip cmnuLocalOperation;
        private ContextMenuStrip cmnuRemoteOperation;
        private ToolStripMenuItem cmnuLocalOparationUpload;
        private ToolStripSeparator cmnuLocalOparationSeparator01;
        private ToolStripMenuItem cmnuLocalOparationRefresh;
        private ToolStripSeparator cmnuLocalOparationSeparator02;
        private ToolStripMenuItem cmnuLocalOparationCreateDirectory;
        private ToolStripMenuItem cmnuLocalOparationRename;
        private ToolStripSeparator cmnuLocalOparationSeparator03;
        private ToolStripMenuItem cmnuLocalOparationDelete;
        private ToolStripMenuItem cmnuRemoteOpearationDownload;
        private ToolStripMenuItem cmnuRemoteOpearationRefresh;
        private ToolStripMenuItem cmnuRemoteOpearationCreateDirectory;
        private ToolStripMenuItem cmnuRemoteOpearationRename;
        private ToolStripMenuItem cmnuRemoteOpearationDelete;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator6;
        private Button btnRemoteRootDirectory;
        private Button btnRemoteUpOneDirectory;
        private Button btnLocalRootDirectory;
        private Button btnLocalUpOneDirectory;
        private Label lblStatus;
        private Label lblFileOperation;
        private ToolStripStatusLabel stsMainStatus;
    }
}