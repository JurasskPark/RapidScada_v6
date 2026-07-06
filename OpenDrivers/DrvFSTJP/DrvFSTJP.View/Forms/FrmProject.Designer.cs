using System.Drawing;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvFSTJP.View.Forms
{
    partial class FrmProject
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
            rootLayout = new TableLayoutPanel();
            grpGeneral = new GroupBox();
            generalLayout = new TableLayoutPanel();
            lblMasterAddress = new Label();
            numMasterAddress = new NumericUpDown();
            lblDeviceAddress = new Label();
            numDeviceAddress = new NumericUpDown();
            lblRequestDelay = new Label();
            numRequestDelayMs = new NumericUpDown();
            chkPollLinkCheck = new CheckBox();
            chkPollStatus = new CheckBox();
            chkDetailedPacketLog = new CheckBox();
            lblConfigFile = new Label();
            grpChannels = new GroupBox();
            dgvChannels = new DataGridView();
            grpRelayDevices = new GroupBox();
            dgvRelayDevices = new DataGridView();
            buttonsPanel = new FlowLayoutPanel();
            btnCancel = new Button();
            btnOk = new Button();
            rootLayout.SuspendLayout();
            grpGeneral.SuspendLayout();
            generalLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numMasterAddress).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numDeviceAddress).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numRequestDelayMs).BeginInit();
            grpChannels.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChannels).BeginInit();
            grpRelayDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRelayDevices).BeginInit();
            buttonsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // rootLayout
            // 
            rootLayout.ColumnCount = 1;
            rootLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rootLayout.Controls.Add(grpGeneral, 0, 0);
            rootLayout.Controls.Add(grpChannels, 0, 1);
            rootLayout.Controls.Add(grpRelayDevices, 0, 2);
            rootLayout.Controls.Add(buttonsPanel, 0, 3);
            rootLayout.Dock = DockStyle.Fill;
            rootLayout.Location = new Point(0, 0);
            rootLayout.Name = "rootLayout";
            rootLayout.Padding = new Padding(8);
            rootLayout.RowCount = 4;
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            rootLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            rootLayout.RowStyles.Add(new RowStyle());
            rootLayout.Size = new Size(980, 680);
            rootLayout.TabIndex = 0;
            // 
            // grpGeneral
            // 
            grpGeneral.AutoSize = true;
            grpGeneral.Controls.Add(generalLayout);
            grpGeneral.Dock = DockStyle.Fill;
            grpGeneral.Location = new Point(11, 11);
            grpGeneral.Name = "grpGeneral";
            grpGeneral.Size = new Size(958, 122);
            grpGeneral.TabIndex = 0;
            grpGeneral.TabStop = false;
            grpGeneral.Text = "General";
            // 
            // generalLayout
            // 
            generalLayout.AutoSize = true;
            generalLayout.ColumnCount = 4;
            generalLayout.ColumnStyles.Add(new ColumnStyle());
            generalLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            generalLayout.ColumnStyles.Add(new ColumnStyle());
            generalLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            generalLayout.Controls.Add(lblMasterAddress, 0, 0);
            generalLayout.Controls.Add(numMasterAddress, 1, 0);
            generalLayout.Controls.Add(lblDeviceAddress, 2, 0);
            generalLayout.Controls.Add(numDeviceAddress, 3, 0);
            generalLayout.Controls.Add(lblRequestDelay, 0, 1);
            generalLayout.Controls.Add(numRequestDelayMs, 1, 1);
            generalLayout.Controls.Add(chkPollLinkCheck, 0, 2);
            generalLayout.Controls.Add(chkPollStatus, 1, 2);
            generalLayout.Controls.Add(chkDetailedPacketLog, 2, 2);
            generalLayout.Controls.Add(lblConfigFile, 3, 2);
            generalLayout.Dock = DockStyle.Fill;
            generalLayout.Location = new Point(3, 19);
            generalLayout.Name = "generalLayout";
            generalLayout.Padding = new Padding(8);
            generalLayout.RowCount = 3;
            generalLayout.RowStyles.Add(new RowStyle());
            generalLayout.RowStyles.Add(new RowStyle());
            generalLayout.RowStyles.Add(new RowStyle());
            generalLayout.Size = new Size(952, 100);
            generalLayout.TabIndex = 0;
            // 
            // lblMasterAddress
            // 
            lblMasterAddress.Anchor = AnchorStyles.Left;
            lblMasterAddress.AutoSize = true;
            lblMasterAddress.Location = new Point(11, 15);
            lblMasterAddress.Name = "lblMasterAddress";
            lblMasterAddress.Size = new Size(86, 15);
            lblMasterAddress.TabIndex = 0;
            lblMasterAddress.Text = "Master address";
            // 
            // numMasterAddress
            // 
            numMasterAddress.Dock = DockStyle.Left;
            numMasterAddress.Location = new Point(119, 11);
            numMasterAddress.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numMasterAddress.Name = "numMasterAddress";
            numMasterAddress.Size = new Size(120, 23);
            numMasterAddress.TabIndex = 1;
            // 
            // lblDeviceAddress
            // 
            lblDeviceAddress.Anchor = AnchorStyles.Left;
            lblDeviceAddress.AutoSize = true;
            lblDeviceAddress.Location = new Point(466, 15);
            lblDeviceAddress.Name = "lblDeviceAddress";
            lblDeviceAddress.Size = new Size(85, 15);
            lblDeviceAddress.TabIndex = 2;
            lblDeviceAddress.Text = "Device address";
            // 
            // numDeviceAddress
            // 
            numDeviceAddress.Dock = DockStyle.Left;
            numDeviceAddress.Location = new Point(599, 11);
            numDeviceAddress.Maximum = new decimal(new int[] { 15, 0, 0, 0 });
            numDeviceAddress.Name = "numDeviceAddress";
            numDeviceAddress.Size = new Size(80, 23);
            numDeviceAddress.TabIndex = 3;
            // 
            // lblRequestDelay
            // 
            lblRequestDelay.Anchor = AnchorStyles.Left;
            lblRequestDelay.AutoSize = true;
            lblRequestDelay.Location = new Point(11, 44);
            lblRequestDelay.Name = "lblRequestDelay";
            lblRequestDelay.Size = new Size(102, 15);
            lblRequestDelay.TabIndex = 4;
            lblRequestDelay.Text = "Request delay, ms";
            // 
            // numRequestDelayMs
            // 
            numRequestDelayMs.Dock = DockStyle.Left;
            numRequestDelayMs.Increment = new decimal(new int[] { 10, 0, 0, 0 });
            numRequestDelayMs.Location = new Point(119, 40);
            numRequestDelayMs.Maximum = new decimal(new int[] { 60000, 0, 0, 0 });
            numRequestDelayMs.Name = "numRequestDelayMs";
            numRequestDelayMs.Size = new Size(120, 23);
            numRequestDelayMs.TabIndex = 5;
            // 
            // chkPollLinkCheck
            // 
            chkPollLinkCheck.Anchor = AnchorStyles.Left;
            chkPollLinkCheck.AutoSize = true;
            chkPollLinkCheck.Location = new Point(11, 69);
            chkPollLinkCheck.Name = "chkPollLinkCheck";
            chkPollLinkCheck.Size = new Size(102, 19);
            chkPollLinkCheck.TabIndex = 6;
            chkPollLinkCheck.Text = "Poll link check";
            chkPollLinkCheck.UseVisualStyleBackColor = true;
            // 
            // chkPollStatus
            // 
            chkPollStatus.Anchor = AnchorStyles.Left;
            chkPollStatus.AutoSize = true;
            chkPollStatus.Location = new Point(119, 69);
            chkPollStatus.Name = "chkPollStatus";
            chkPollStatus.Size = new Size(80, 19);
            chkPollStatus.TabIndex = 7;
            chkPollStatus.Text = "Poll status";
            chkPollStatus.UseVisualStyleBackColor = true;
            // 
            // chkDetailedPacketLog
            // 
            chkDetailedPacketLog.Anchor = AnchorStyles.Left;
            chkDetailedPacketLog.AutoSize = true;
            chkDetailedPacketLog.Location = new Point(466, 69);
            chkDetailedPacketLog.Name = "chkDetailedPacketLog";
            chkDetailedPacketLog.Size = new Size(127, 19);
            chkDetailedPacketLog.TabIndex = 8;
            chkDetailedPacketLog.Text = "Detailed packet log";
            chkDetailedPacketLog.UseVisualStyleBackColor = true;
            // 
            // lblConfigFile
            // 
            lblConfigFile.AutoEllipsis = true;
            lblConfigFile.Dock = DockStyle.Fill;
            lblConfigFile.Location = new Point(599, 66);
            lblConfigFile.Name = "lblConfigFile";
            lblConfigFile.Size = new Size(342, 26);
            lblConfigFile.TabIndex = 9;
            lblConfigFile.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // grpChannels
            // 
            grpChannels.Controls.Add(dgvChannels);
            grpChannels.Dock = DockStyle.Fill;
            grpChannels.Location = new Point(11, 139);
            grpChannels.Name = "grpChannels";
            grpChannels.Size = new Size(958, 265);
            grpChannels.TabIndex = 1;
            grpChannels.TabStop = false;
            grpChannels.Text = "Channels";
            // 
            // dgvChannels
            // 
            dgvChannels.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChannels.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChannels.Dock = DockStyle.Fill;
            dgvChannels.Location = new Point(3, 19);
            dgvChannels.MultiSelect = false;
            dgvChannels.Name = "dgvChannels";
            dgvChannels.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChannels.Size = new Size(952, 243);
            dgvChannels.TabIndex = 0;
            // 
            // grpRelayDevices
            // 
            grpRelayDevices.Controls.Add(dgvRelayDevices);
            grpRelayDevices.Dock = DockStyle.Fill;
            grpRelayDevices.Location = new Point(11, 410);
            grpRelayDevices.Name = "grpRelayDevices";
            grpRelayDevices.Size = new Size(958, 215);
            grpRelayDevices.TabIndex = 2;
            grpRelayDevices.TabStop = false;
            grpRelayDevices.Text = "Relay expansion blocks";
            // 
            // dgvRelayDevices
            // 
            dgvRelayDevices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRelayDevices.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRelayDevices.Dock = DockStyle.Fill;
            dgvRelayDevices.Location = new Point(3, 19);
            dgvRelayDevices.MultiSelect = false;
            dgvRelayDevices.Name = "dgvRelayDevices";
            dgvRelayDevices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRelayDevices.Size = new Size(952, 193);
            dgvRelayDevices.TabIndex = 0;
            // 
            // buttonsPanel
            // 
            buttonsPanel.AutoSize = true;
            buttonsPanel.Controls.Add(btnCancel);
            buttonsPanel.Controls.Add(btnOk);
            buttonsPanel.Dock = DockStyle.Fill;
            buttonsPanel.FlowDirection = FlowDirection.RightToLeft;
            buttonsPanel.Location = new Point(11, 631);
            buttonsPanel.Name = "buttonsPanel";
            buttonsPanel.Padding = new Padding(0, 8, 0, 0);
            buttonsPanel.Size = new Size(958, 38);
            buttonsPanel.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(865, 11);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(90, 23);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOk
            // 
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(769, 11);
            btnOk.Name = "btnOk";
            btnOk.Size = new Size(90, 23);
            btnOk.TabIndex = 0;
            btnOk.Text = "OK";
            btnOk.UseVisualStyleBackColor = true;
            btnOk.Click += BtnOk_Click;
            // 
            // FrmProject
            // 
            AcceptButton = btnOk;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(980, 680);
            Controls.Add(rootLayout);
            MinimumSize = new Size(820, 560);
            Name = "FrmProject";
            StartPosition = FormStartPosition.CenterParent;
            Text = "DrvFSTJP";
            rootLayout.ResumeLayout(false);
            rootLayout.PerformLayout();
            grpGeneral.ResumeLayout(false);
            grpGeneral.PerformLayout();
            generalLayout.ResumeLayout(false);
            generalLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numMasterAddress).EndInit();
            ((System.ComponentModel.ISupportInitialize)numDeviceAddress).EndInit();
            ((System.ComponentModel.ISupportInitialize)numRequestDelayMs).EndInit();
            grpChannels.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvChannels).EndInit();
            grpRelayDevices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvRelayDevices).EndInit();
            buttonsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TableLayoutPanel rootLayout;
        private GroupBox grpGeneral;
        private TableLayoutPanel generalLayout;
        private Label lblMasterAddress;
        private NumericUpDown numMasterAddress;
        private Label lblDeviceAddress;
        private NumericUpDown numDeviceAddress;
        private Label lblRequestDelay;
        private NumericUpDown numRequestDelayMs;
        private CheckBox chkPollLinkCheck;
        private CheckBox chkPollStatus;
        private CheckBox chkDetailedPacketLog;
        private Label lblConfigFile;
        private GroupBox grpChannels;
        private DataGridView dgvChannels;
        private GroupBox grpRelayDevices;
        private DataGridView dgvRelayDevices;
        private FlowLayoutPanel buttonsPanel;
        private Button btnOk;
        private Button btnCancel;
    }
}
