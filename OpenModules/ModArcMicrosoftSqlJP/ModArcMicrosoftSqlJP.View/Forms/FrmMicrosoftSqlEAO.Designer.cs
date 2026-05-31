
namespace Scada.Server.Modules.ModArcMicrosoftSqlJP.View.Forms
{
    partial class FrmMicrosoftSqlEAO
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
            ctrlEventArchiveOptions = new Scada.Server.Forms.Controls.CtrlEventArchiveOptions();
            btnManageConn = new Button();
            btnCancel = new Button();
            btnOK = new Button();
            ctrlDatabaseOptions = new Scada.Server.Modules.ModArcMicrosoftSqlJP.View.Controls.CtrlDatabaseOptions();
            tabControl = new TabControl();
            pageGeneral = new TabPage();
            pageDatabase = new TabPage();
            pnlBottom = new Panel();
            tabControl.SuspendLayout();
            pageGeneral.SuspendLayout();
            pageDatabase.SuspendLayout();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // ctrlEventArchiveOptions
            // 
            ctrlEventArchiveOptions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            ctrlEventArchiveOptions.ArchiveOptions = null;
            ctrlEventArchiveOptions.Location = new Point(11, 13);
            ctrlEventArchiveOptions.Margin = new Padding(6, 8, 6, 8);
            ctrlEventArchiveOptions.Name = "ctrlEventArchiveOptions";
            ctrlEventArchiveOptions.Size = new Size(596, 193);
            ctrlEventArchiveOptions.TabIndex = 0;
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
            ctrlDatabaseOptions.Size = new Size(596, 292);
            ctrlDatabaseOptions.TabIndex = 1;
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
            tabControl.Size = new Size(628, 366);
            tabControl.TabIndex = 0;
            // 
            // pageGeneral
            // 
            pageGeneral.Controls.Add(ctrlEventArchiveOptions);
            pageGeneral.Location = new Point(4, 34);
            pageGeneral.Margin = new Padding(4, 5, 4, 5);
            pageGeneral.Name = "pageGeneral";
            pageGeneral.Padding = new Padding(7, 8, 7, 8);
            pageGeneral.Size = new Size(620, 328);
            pageGeneral.TabIndex = 0;
            pageGeneral.Text = "General";
            pageGeneral.UseVisualStyleBackColor = true;
            // 
            // pageDatabase
            // 
            pageDatabase.Controls.Add(ctrlDatabaseOptions);
            pageDatabase.Location = new Point(4, 34);
            pageDatabase.Margin = new Padding(4, 5, 4, 5);
            pageDatabase.Name = "pageDatabase";
            pageDatabase.Padding = new Padding(7, 8, 7, 8);
            pageDatabase.Size = new Size(620, 328);
            pageDatabase.TabIndex = 1;
            pageDatabase.Text = "Database";
            pageDatabase.UseVisualStyleBackColor = true;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(btnManageConn);
            pnlBottom.Controls.Add(btnOK);
            pnlBottom.Controls.Add(btnCancel);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 366);
            pnlBottom.Margin = new Padding(4, 5, 4, 5);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(628, 68);
            pnlBottom.TabIndex = 1;
            // 
            // FrmMicrosoftSqlEAO
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(628, 434);
            Controls.Add(tabControl);
            Controls.Add(pnlBottom);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmMicrosoftSqlEAO";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Event Archive Options";
            Load += FrmMicrosoftSqlHAO_Load;
            tabControl.ResumeLayout(false);
            pageGeneral.ResumeLayout(false);
            pageDatabase.ResumeLayout(false);
            pnlBottom.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Button btnManageConn;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private Server.Forms.Controls.CtrlEventArchiveOptions ctrlEventArchiveOptions;
        private Controls.CtrlDatabaseOptions ctrlDatabaseOptions;
        private TabControl tabControl;
        private TabPage pageGeneral;
        private TabPage pageDatabase;
        private Panel pnlBottom;
    }
}