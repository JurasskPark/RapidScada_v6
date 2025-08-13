using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
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
            lstScenarios = new ListView();
            clmName = new ColumnHeader();
            clmEnabled = new ColumnHeader();
            cmnuMenu = new ContextMenuStrip(components);
            cmnuListAdd = new ToolStripMenuItem();
            cmnuSeparator01 = new ToolStripSeparator();
            cmnuListChange = new ToolStripMenuItem();
            cmnuSeparator02 = new ToolStripSeparator();
            cmnuListDelete = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            cmnuListUp = new ToolStripMenuItem();
            cmnuListDown = new ToolStripMenuItem();
            tolToolbar = new ToolStrip();
            tolManager = new ToolStripDropDownButton();
            tolProperties = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            tolConnect = new ToolStripMenuItem();
            tolDisconnect = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            tolSave = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            tolClose = new ToolStripMenuItem();
            tolSettings = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            Toolbar_Favorites = new ToolStripDropDownButton();
            toolStripSeparator5 = new ToolStripSeparator();
            toolSettings = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolFullScreen = new ToolStripButton();
            managerToolStripMenuItem = new ToolStripMenuItem();
            tlpPanel = new TableLayoutPanel();
            tabControl = new TabControl();
            tolAbout = new ToolStripButton();
            cmnuMenu.SuspendLayout();
            tolToolbar.SuspendLayout();
            tlpPanel.SuspendLayout();
            SuspendLayout();
            // 
            // lstScenarios
            // 
            lstScenarios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstScenarios.Columns.AddRange(new ColumnHeader[] { clmName, clmEnabled });
            lstScenarios.ContextMenuStrip = cmnuMenu;
            lstScenarios.FullRowSelect = true;
            lstScenarios.GridLines = true;
            lstScenarios.Location = new Point(3, 3);
            lstScenarios.Name = "lstScenarios";
            lstScenarios.Size = new Size(213, 437);
            lstScenarios.TabIndex = 0;
            lstScenarios.UseCompatibleStateImageBehavior = false;
            lstScenarios.View = System.Windows.Forms.View.Details;
            lstScenarios.MouseClick += lstScenarios_MouseClick;
            lstScenarios.MouseDoubleClick += lstScenarios_MouseDoubleClick;
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
            // cmnuMenu
            // 
            cmnuMenu.Items.AddRange(new ToolStripItem[] { cmnuListAdd, cmnuSeparator01, cmnuListChange, cmnuSeparator02, cmnuListDelete, toolStripSeparator1, cmnuListUp, cmnuListDown });
            cmnuMenu.Name = "cmnuSelectQuery";
            cmnuMenu.Size = new Size(171, 132);
            // 
            // cmnuListAdd
            // 
            cmnuListAdd.Image = (Image)resources.GetObject("cmnuListAdd.Image");
            cmnuListAdd.Name = "cmnuListAdd";
            cmnuListAdd.Size = new Size(170, 22);
            cmnuListAdd.Text = "Add Scenario";
            cmnuListAdd.Click += cmnuListAdd_Click;
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
            cmnuListChange.Text = "Change Scenario";
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
            cmnuListDelete.Text = "Delete Scenario";
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
            // tolToolbar
            // 
            tolToolbar.Items.AddRange(new ToolStripItem[] { tolManager, tolSettings, tolAbout });
            tolToolbar.Location = new Point(0, 0);
            tolToolbar.Name = "tolToolbar";
            tolToolbar.Size = new Size(1099, 25);
            tolToolbar.TabIndex = 63;
            tolToolbar.Text = "Toolbar";
            // 
            // tolManager
            // 
            tolManager.DropDownItems.AddRange(new ToolStripItem[] { tolProperties, toolStripSeparator7, tolConnect, tolDisconnect, toolStripSeparator3, tolSave, toolStripSeparator6, tolClose });
            tolManager.Image = Properties.Resources.server;
            tolManager.ImageTransparentColor = Color.Magenta;
            tolManager.Name = "tolManager";
            tolManager.Size = new Size(83, 22);
            tolManager.Text = "Manager";
            // 
            // tolProperties
            // 
            tolProperties.Image = Properties.Resources.ftp_accounts;
            tolProperties.Name = "tolProperties";
            tolProperties.Size = new Size(180, 22);
            tolProperties.Text = "Properties";
            tolProperties.Click += tolProperties_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(177, 6);
            // 
            // tolConnect
            // 
            tolConnect.Image = Properties.Resources.connect;
            tolConnect.Name = "tolConnect";
            tolConnect.Size = new Size(180, 22);
            tolConnect.Text = "Connect";
            tolConnect.Click += tolConnect_Click;
            // 
            // tolDisconnect
            // 
            tolDisconnect.Image = Properties.Resources.disconnect;
            tolDisconnect.Name = "tolDisconnect";
            tolDisconnect.Size = new Size(180, 22);
            tolDisconnect.Text = "Disconnect";
            tolDisconnect.Click += tolDisconnect_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(177, 6);
            // 
            // tolSave
            // 
            tolSave.Image = Properties.Resources.save;
            tolSave.Name = "tolSave";
            tolSave.Size = new Size(180, 22);
            tolSave.Text = "Save";
            tolSave.Click += tolSave_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(177, 6);
            // 
            // tolClose
            // 
            tolClose.Name = "tolClose";
            tolClose.Size = new Size(180, 22);
            tolClose.Text = "Close";
            tolClose.Click += tolClose_Click;
            // 
            // tolSettings
            // 
            tolSettings.Image = (Image)resources.GetObject("tolSettings.Image");
            tolSettings.ImageTransparentColor = Color.Magenta;
            tolSettings.Name = "tolSettings";
            tolSettings.Size = new Size(69, 22);
            tolSettings.Text = "Settings";
            tolSettings.Click += tolSettings_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 25);
            // 
            // Toolbar_Favorites
            // 
            Toolbar_Favorites.Name = "Toolbar_Favorites";
            Toolbar_Favorites.Size = new Size(23, 23);
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 25);
            // 
            // toolSettings
            // 
            toolSettings.Name = "toolSettings";
            toolSettings.Size = new Size(23, 23);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Alignment = ToolStripItemAlignment.Right;
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // toolFullScreen
            // 
            toolFullScreen.Name = "toolFullScreen";
            toolFullScreen.Size = new Size(23, 23);
            // 
            // managerToolStripMenuItem
            // 
            managerToolStripMenuItem.Name = "managerToolStripMenuItem";
            managerToolStripMenuItem.Size = new Size(180, 22);
            managerToolStripMenuItem.Text = "Manager";
            // 
            // tlpPanel
            // 
            tlpPanel.ColumnCount = 2;
            tlpPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tlpPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tlpPanel.Controls.Add(lstScenarios, 0, 0);
            tlpPanel.Controls.Add(tabControl, 1, 0);
            tlpPanel.Dock = DockStyle.Fill;
            tlpPanel.Location = new Point(0, 25);
            tlpPanel.Name = "tlpPanel";
            tlpPanel.RowCount = 1;
            tlpPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpPanel.Size = new Size(1099, 443);
            tlpPanel.TabIndex = 64;
            // 
            // tabControl
            // 
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(222, 3);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(874, 437);
            tabControl.TabIndex = 1;
            // 
            // tolAbout
            // 
            tolAbout.Image = (Image)resources.GetObject("tolAbout.Image");
            tolAbout.ImageTransparentColor = Color.Magenta;
            tolAbout.Name = "tolAbout";
            tolAbout.Size = new Size(60, 22);
            tolAbout.Text = "About";
            tolAbout.Click += tolAbout_Click;
            // 
            // FrmConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1099, 468);
            Controls.Add(tlpPanel);
            Controls.Add(tolToolbar);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1115, 268);
            Name = "FrmConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FTP JP - Device {0} Version {1}";
            WindowState = FormWindowState.Maximized;
            Load += FrmConfig_Load;
            cmnuMenu.ResumeLayout(false);
            tolToolbar.ResumeLayout(false);
            tolToolbar.PerformLayout();
            tlpPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lstScenarios;
        private ColumnHeader clmName;
        private ContextMenuStrip cmnuMenu;
        private ToolStripMenuItem cmnuListAdd;
        private ToolStripSeparator cmnuSeparator01;
        private ToolStripMenuItem cmnuListChange;
        private ToolStripSeparator cmnuSeparator02;
        private ToolStripMenuItem cmnuListDelete;
        private Button btnClose;
        private Button btnSave;
        private ColumnHeader clmEnabled;
        private Button btnAbout;
        private Button btnSettings;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cmnuListUp;
        private ToolStripMenuItem cmnuListDown;
        private Label lblUser;
        private TextBox txtUser;
        private Label lblHost;
        private TextBox txtHost;
        private Label lblPassword;
        private TextBox txtPassword;
        private ComboBox cmbEncryptionMode;
        private Label lblEncryptionMode;
        private TextBox txtPort;
        private Label lblPort;
        private Button btnConnectTest;
        private ToolStrip tolToolbar;
        private ToolStripButton tolServerManager;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripDropDownButton Toolbar_Favorites;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolFullScreen;
        private ToolStripButton toolSettings;
        private ToolStripMenuItem managerToolStripMenuItem;
        private TableLayoutPanel tlpPanel;
        private ToolStripDropDownButton tolManager;
        private ToolStripMenuItem tolConnect;
        private ToolStripMenuItem tolDisconnect;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem tolSave;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem tolClose;
        private ToolStripMenuItem tolProperties;
        private ToolStripSeparator toolStripSeparator7;
        private TabControl tabControl;
        private ToolStripButton tolSettings;
        private ToolStripButton tolAbout;
    }
}