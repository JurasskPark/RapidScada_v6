using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
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
            lstParsers = new ListView();
            clmName = new ColumnHeader();
            clmDescription = new ColumnHeader();
            clmPath = new ColumnHeader();
            clmFilter = new ColumnHeader();
            clmTemplateFileName = new ColumnHeader();
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
            btnClose = new Button();
            btnSave = new Button();
            btnAbout = new Button();
            btnSettings = new Button();
            cmnuMenu.SuspendLayout();
            SuspendLayout();
            // 
            // lstParsers
            // 
            lstParsers.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstParsers.Columns.AddRange(new ColumnHeader[] { clmName, clmDescription, clmPath, clmFilter, clmTemplateFileName, clmEnabled });
            lstParsers.ContextMenuStrip = cmnuMenu;
            lstParsers.FullRowSelect = true;
            lstParsers.GridLines = true;
            lstParsers.Location = new Point(12, 12);
            lstParsers.Name = "lstParsers";
            lstParsers.Size = new Size(1077, 168);
            lstParsers.TabIndex = 0;
            lstParsers.UseCompatibleStateImageBehavior = false;
            lstParsers.View = System.Windows.Forms.View.Details;
            lstParsers.MouseClick += lstDevice_MouseClick;
            // 
            // clmName
            // 
            clmName.Text = "Name";
            clmName.Width = 200;
            // 
            // clmDescription
            // 
            clmDescription.Text = "Description";
            clmDescription.Width = 350;
            // 
            // clmPath
            // 
            clmPath.Text = "Path";
            clmPath.Width = 400;
            // 
            // clmFilter
            // 
            clmFilter.Text = "Filter";
            clmFilter.Width = 150;
            // 
            // clmTemplateFileName
            // 
            clmTemplateFileName.Text = "Template file name";
            clmTemplateFileName.Width = 180;
            // 
            // clmEnabled
            // 
            clmEnabled.Text = "Enabled";
            clmEnabled.Width = 100;
            // 
            // cmnuMenu
            // 
            cmnuMenu.ImageScalingSize = new Size(24, 24);
            cmnuMenu.Items.AddRange(new ToolStripItem[] { cmnuListAdd, cmnuSeparator01, cmnuListChange, cmnuSeparator02, cmnuListDelete, toolStripSeparator1, cmnuListUp, cmnuListDown });
            cmnuMenu.Name = "cmnuSelectQuery";
            cmnuMenu.Size = new Size(189, 194);
            // 
            // cmnuListAdd
            // 
            cmnuListAdd.Image = (Image)resources.GetObject("cmnuListAdd.Image");
            cmnuListAdd.Name = "cmnuListAdd";
            cmnuListAdd.Size = new Size(188, 30);
            cmnuListAdd.Text = "Add Task";
            cmnuListAdd.Click += cmnuListAdd_Click;
            // 
            // cmnuSeparator01
            // 
            cmnuSeparator01.Name = "cmnuSeparator01";
            cmnuSeparator01.Size = new Size(185, 6);
            // 
            // cmnuListChange
            // 
            cmnuListChange.Image = (Image)resources.GetObject("cmnuListChange.Image");
            cmnuListChange.Name = "cmnuListChange";
            cmnuListChange.Size = new Size(188, 30);
            cmnuListChange.Text = "Change Task";
            cmnuListChange.Click += cmnuListChange_Click;
            // 
            // cmnuSeparator02
            // 
            cmnuSeparator02.Name = "cmnuSeparator02";
            cmnuSeparator02.Size = new Size(185, 6);
            // 
            // cmnuListDelete
            // 
            cmnuListDelete.Image = (Image)resources.GetObject("cmnuListDelete.Image");
            cmnuListDelete.Name = "cmnuListDelete";
            cmnuListDelete.Size = new Size(188, 30);
            cmnuListDelete.Text = "Delete Task";
            cmnuListDelete.Click += cmnuListDelete_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(185, 6);
            // 
            // cmnuListUp
            // 
            cmnuListUp.Image = (Image)resources.GetObject("cmnuListUp.Image");
            cmnuListUp.Name = "cmnuListUp";
            cmnuListUp.ShortcutKeys = Keys.Control | Keys.Up;
            cmnuListUp.Size = new Size(188, 30);
            cmnuListUp.Text = "Up";
            cmnuListUp.Click += cmnuListUp_Click;
            // 
            // cmnuListDown
            // 
            cmnuListDown.Image = (Image)resources.GetObject("cmnuListDown.Image");
            cmnuListDown.Name = "cmnuListDown";
            cmnuListDown.ShortcutKeys = Keys.Control | Keys.Down;
            cmnuListDown.Size = new Size(188, 30);
            cmnuListDown.Text = "Down";
            cmnuListDown.Click += cmnuListDown_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(982, 184);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(107, 27);
            btnClose.TabIndex = 48;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(869, 184);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(107, 27);
            btnSave.TabIndex = 47;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAbout.Location = new Point(12, 184);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(107, 27);
            btnAbout.TabIndex = 49;
            btnAbout.Text = "About";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;
            // 
            // btnSettings
            // 
            btnSettings.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSettings.Location = new Point(125, 184);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(107, 27);
            btnSettings.TabIndex = 51;
            btnSettings.Text = "Settings";
            btnSettings.UseVisualStyleBackColor = true;
            btnSettings.Click += btnSettings_Click;
            // 
            // FrmConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1099, 229);
            Controls.Add(btnSettings);
            Controls.Add(btnAbout);
            Controls.Add(btnClose);
            Controls.Add(btnSave);
            Controls.Add(lstParsers);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1115, 268);
            Name = "FrmConfig";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Free Disk Space JP - Device {0} Version {1}";
            WindowState = FormWindowState.Maximized;
            Load += FrmListParsers_Load;
            cmnuMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ListView lstParsers;
        private ColumnHeader clmFilter;
        private ColumnHeader clmName;
        private ColumnHeader clmPath;
        private ContextMenuStrip cmnuMenu;
        private ToolStripMenuItem cmnuListAdd;
        private ToolStripSeparator cmnuSeparator01;
        private ToolStripMenuItem cmnuListChange;
        private ToolStripSeparator cmnuSeparator02;
        private ToolStripMenuItem cmnuListDelete;
        private Button btnClose;
        private Button btnSave;
        private ColumnHeader clmTemplateFileName;
        private ColumnHeader clmEnabled;
        private Button btnAbout;
        private Button btnSettings;
        private ColumnHeader clmDescription;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem cmnuListUp;
        private ToolStripMenuItem cmnuListDown;
    }
}