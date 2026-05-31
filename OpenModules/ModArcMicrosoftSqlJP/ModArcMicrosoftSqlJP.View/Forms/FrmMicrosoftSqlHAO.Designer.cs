
namespace Scada.Server.Modules.ModArcMicrosoftSqlJP.View.Forms
{
    partial class FrmMicrosoftSqlHAO
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
            ctrlHistoricalArchiveOptions = new Scada.Server.Forms.Controls.CtrlHistoricalArchiveOptions();
            btnManageConn = new Button();
            btnCancel = new Button();
            btnOK = new Button();
            ctrlDatabaseOptions = new Scada.Server.Modules.ModArcMicrosoftSqlJP.View.Controls.CtrlDatabaseOptions();
            tabControl = new TabControl();
            pageGeneral = new TabPage();
            pageDatabase = new TabPage();
            numCacheSizeRatio = new NumericUpDown();
            lblCacheSizeRatio = new Label();
            chkUseMemoryCache = new CheckBox();
            lblUseMemoryCache = new Label();
            panel1 = new Panel();
            tabControl.SuspendLayout();
            pageGeneral.SuspendLayout();
            pageDatabase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numCacheSizeRatio).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ctrlHistoricalArchiveOptions
            // 
            ctrlHistoricalArchiveOptions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ctrlHistoricalArchiveOptions.ArchiveOptions = null;
            ctrlHistoricalArchiveOptions.Location = new Point(11, 13);
            ctrlHistoricalArchiveOptions.Margin = new Padding(6, 8, 6, 8);
            ctrlHistoricalArchiveOptions.Name = "ctrlHistoricalArchiveOptions";
            ctrlHistoricalArchiveOptions.Size = new Size(596, 515);
            ctrlHistoricalArchiveOptions.TabIndex = 0;
            // 
            // btnManageConn
            // 
            btnManageConn.Location = new Point(17, 10);
            btnManageConn.Margin = new Padding(4, 5, 4, 5);
            btnManageConn.Name = "btnManageConn";
            btnManageConn.Size = new Size(200, 38);
            btnManageConn.TabIndex = 0;
            btnManageConn.Text = "Manage Connections";
            btnManageConn.UseVisualStyleBackColor = true;
            btnManageConn.Click += btnManageConn_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCancel.Location = new Point(508, 10);
            btnCancel.Margin = new Padding(4, 5, 4, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(107, 38);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOK.Location = new Point(393, 10);
            btnOK.Margin = new Padding(4, 5, 4, 5);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(107, 38);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // ctrlDatabaseOptions
            // 
            ctrlDatabaseOptions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ctrlDatabaseOptions.Location = new Point(11, 13);
            ctrlDatabaseOptions.Margin = new Padding(6, 8, 6, 8);
            ctrlDatabaseOptions.Name = "ctrlDatabaseOptions";
            ctrlDatabaseOptions.Size = new Size(596, 227);
            ctrlDatabaseOptions.TabIndex = 0;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(pageGeneral);
            tabControl.Controls.Add(pageDatabase);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(4, 5, 4, 5);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(628, 589);
            tabControl.TabIndex = 0;
            // 
            // pageGeneral
            // 
            pageGeneral.Controls.Add(ctrlHistoricalArchiveOptions);
            pageGeneral.Location = new Point(4, 34);
            pageGeneral.Margin = new Padding(4, 5, 4, 5);
            pageGeneral.Name = "pageGeneral";
            pageGeneral.Padding = new Padding(7, 8, 7, 8);
            pageGeneral.Size = new Size(620, 551);
            pageGeneral.TabIndex = 0;
            pageGeneral.Text = "General";
            pageGeneral.UseVisualStyleBackColor = true;
            // 
            // pageDatabase
            // 
            pageDatabase.Controls.Add(numCacheSizeRatio);
            pageDatabase.Controls.Add(lblCacheSizeRatio);
            pageDatabase.Controls.Add(chkUseMemoryCache);
            pageDatabase.Controls.Add(lblUseMemoryCache);
            pageDatabase.Controls.Add(ctrlDatabaseOptions);
            pageDatabase.Location = new Point(4, 34);
            pageDatabase.Margin = new Padding(4, 5, 4, 5);
            pageDatabase.Name = "pageDatabase";
            pageDatabase.Padding = new Padding(7, 8, 7, 8);
            pageDatabase.Size = new Size(620, 551);
            pageDatabase.TabIndex = 1;
            pageDatabase.Text = "Database";
            pageDatabase.UseVisualStyleBackColor = true;
            // 
            // numCacheSizeRatio
            // 
            numCacheSizeRatio.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            numCacheSizeRatio.DecimalPlaces = 2;
            numCacheSizeRatio.Location = new Point(391, 299);
            numCacheSizeRatio.Margin = new Padding(4, 5, 4, 5);
            numCacheSizeRatio.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numCacheSizeRatio.Name = "numCacheSizeRatio";
            numCacheSizeRatio.Size = new Size(216, 31);
            numCacheSizeRatio.TabIndex = 4;
            numCacheSizeRatio.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblCacheSizeRatio
            // 
            lblCacheSizeRatio.AutoSize = true;
            lblCacheSizeRatio.Location = new Point(7, 305);
            lblCacheSizeRatio.Margin = new Padding(4, 0, 4, 0);
            lblCacheSizeRatio.Name = "lblCacheSizeRatio";
            lblCacheSizeRatio.Size = new Size(134, 25);
            lblCacheSizeRatio.TabIndex = 3;
            lblCacheSizeRatio.Text = "Cache size ratio";
            // 
            // chkUseMemoryCache
            // 
            chkUseMemoryCache.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            chkUseMemoryCache.AutoSize = true;
            chkUseMemoryCache.Location = new Point(585, 257);
            chkUseMemoryCache.Margin = new Padding(4, 5, 4, 5);
            chkUseMemoryCache.Name = "chkUseMemoryCache";
            chkUseMemoryCache.Size = new Size(22, 21);
            chkUseMemoryCache.TabIndex = 2;
            chkUseMemoryCache.UseVisualStyleBackColor = true;
            // 
            // lblUseMemoryCache
            // 
            lblUseMemoryCache.AutoSize = true;
            lblUseMemoryCache.Location = new Point(7, 257);
            lblUseMemoryCache.Margin = new Padding(4, 0, 4, 0);
            lblUseMemoryCache.Name = "lblUseMemoryCache";
            lblUseMemoryCache.Size = new Size(183, 25);
            lblUseMemoryCache.TabIndex = 1;
            lblUseMemoryCache.Text = "Use in-memory cache";
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnManageConn);
            panel1.Controls.Add(btnOK);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 589);
            panel1.Margin = new Padding(4, 5, 4, 5);
            panel1.Name = "panel1";
            panel1.Size = new Size(628, 68);
            panel1.TabIndex = 1;
            // 
            // FrmMicrosoftSqlHAO
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(628, 657);
            Controls.Add(tabControl);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMicrosoftSqlHAO";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Historical Archive Options";
            Load += FrmMicrosoftSqlHAO_Load;
            tabControl.ResumeLayout(false);
            pageGeneral.ResumeLayout(false);
            pageDatabase.ResumeLayout(false);
            pageDatabase.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numCacheSizeRatio).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button btnManageConn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Scada.Server.Forms.Controls.CtrlHistoricalArchiveOptions ctrlHistoricalArchiveOptions;
        private Controls.CtrlDatabaseOptions ctrlDatabaseOptions;
        private TabControl tabControl;
        private TabPage pageGeneral;
        private TabPage pageDatabase;
        private Panel panel1;
        private CheckBox chkUseMemoryCache;
        private Label lblUseMemoryCache;
        private Label lblCacheSizeRatio;
        private NumericUpDown numCacheSizeRatio;
    }
}