using ProjectDriver;
using Scada.Forms;
using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvFSTJP.View.Forms
{
    /// <summary>
    /// Represents the FST-03x XML project editor.
    /// <para>Представляет редактор XML-проекта ФСТ-03х.</para>
    /// </summary>
    internal partial class FrmProject : Form
    {
        private readonly AppDirs appDirs;
        private readonly int deviceNum;
        private readonly string projectFileName;
        private readonly Project project;

        private BindingList<FstChannelConfig> channels;
        private BindingList<FstRelayDeviceConfig> relayDevices;

        public FrmProject(AppDirs appDirs, int deviceNum)
        {
            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.deviceNum = deviceNum;
            projectFileName = Path.Combine(appDirs.ConfigDir, Project.GetFileName(deviceNum));
            project = new Project();

            InitializeComponent();
            lblConfigFile.Text = projectFileName;
            LoadProject();
        }

        private void LoadProject()
        {
            if (!project.Load(projectFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            project.Normalize();
            numMasterAddress.Value = project.MasterAddress;
            numDeviceAddress.Value = project.DeviceAddress;
            chkPollLinkCheck.Checked = project.PollLinkCheck;
            chkPollStatus.Checked = project.PollStatus;
            chkDetailedPacketLog.Checked = project.DetailedPacketLog;
            numRequestDelayMs.Value = project.RequestDelayMs;

            channels = new BindingList<FstChannelConfig>(project.Channels.Select(CloneChannel).ToList());
            relayDevices = new BindingList<FstRelayDeviceConfig>(project.RelayDevices.Select(CloneRelayDevice).ToList());
            dgvChannels.DataSource = channels;
            dgvRelayDevices.DataSource = relayDevices;
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                dgvChannels.EndEdit();
                dgvRelayDevices.EndEdit();

                project.MasterAddress = (int)numMasterAddress.Value;
                project.DeviceAddress = (int)numDeviceAddress.Value;
                project.PollLinkCheck = chkPollLinkCheck.Checked;
                project.PollStatus = chkPollStatus.Checked;
                project.DetailedPacketLog = chkDetailedPacketLog.Checked;
                project.RequestDelayMs = (int)numRequestDelayMs.Value;
                project.Channels = channels.Where(channel => channel != null).Select(CloneChannel).ToList();
                project.RelayDevices = relayDevices.Where(relayDevice => relayDevice != null).Select(CloneRelayDevice).ToList();
                project.Normalize();

                if (!project.Save(projectFileName, out string errMsg))
                {
                    ScadaUiUtils.ShowError(errMsg);
                    DialogResult = DialogResult.None;
                }
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.Message);
                DialogResult = DialogResult.None;
            }
        }

        private static FstChannelConfig CloneChannel(FstChannelConfig source)
        {
            if (source == null)
            {
                return new FstChannelConfig();
            }

            return new FstChannelConfig
            {
                Order = source.Order,
                Enabled = source.Enabled,
                ChannelNo = source.ChannelNo,
                Name = source.Name,
                CodePrefix = source.CodePrefix,
                Coefficient = source.Coefficient,
                Offset = source.Offset,
                Precision = source.Precision,
                Units = source.Units
            };
        }

        private static FstRelayDeviceConfig CloneRelayDevice(FstRelayDeviceConfig source)
        {
            if (source == null)
            {
                return new FstRelayDeviceConfig();
            }

            return new FstRelayDeviceConfig
            {
                Order = source.Order,
                Enabled = source.Enabled,
                Address = source.Address,
                Name = source.Name,
                CodePrefix = source.CodePrefix
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ConfigureGridColumns(dgvChannels);
            ConfigureGridColumns(dgvRelayDevices);
        }

        private static void ConfigureGridColumns(DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
                column.DefaultCellStyle.FormatProvider = CultureInfo.InvariantCulture;
            }
        }
    }
}
