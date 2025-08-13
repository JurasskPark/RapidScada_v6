using FluentFTP;
using Scada.Forms;
using System.Data;
using static Scada.Comm.Drivers.DrvFtpJP.DriverUtils;
using static Scada.Comm.Drivers.DrvFtpJP.OperationAction;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmActionLocalUploadDirectory : Form
    {
        public FrmActionLocalUploadDirectory()
        {
            InitializeComponent();

            InitializeTypeOperationsActions();
            InitializeTypeMode();
            InitializeTypeRemoteExists();
            InitializeTypeFtpVerify();
            InitializeTypeSize();
        }

        #region Variables
        public FrmConfig formParent;                // parent form
        public Project project;                     // the project configuration

        private bool modified;                      // the configuration was modified

        public OperationAction operationAction { get; set; }
        #endregion Variables

        private void InitializeTypeOperationsActions()
        {
            // Получение отсортированного списка строк
            List<string> sortedStrings = Enum.GetValues(typeof(OperationsActions))
                .Cast<OperationsActions>()
                .OrderBy(x => (int)(object)x)
                .Select(x => x.ToString())
                .ToList();

            cmbAction.Items.Clear();
            cmbAction.Items.AddRange(sortedStrings.ToArray());
            cmbAction.SelectedIndex = 0;
        }

        private void InitializeTypeMode()
        {
            // Получение отсортированного списка строк
            List<string> sortedStrings = Enum.GetValues(typeof(FtpFolderSyncMode))
                .Cast<FtpFolderSyncMode>()
                .OrderBy(x => (int)(object)x)
                .Select(x => x.ToString())
                .ToList();

            cmbMode.Items.Clear();
            cmbMode.Items.AddRange(sortedStrings.ToArray());
            cmbMode.SelectedIndex = 0;
        }

        private void InitializeTypeRemoteExists()
        {
            // Получение отсортированного списка строк
            List<string> sortedStrings = Enum.GetValues(typeof(FtpRemoteExists))
                .Cast<FtpRemoteExists>()
                .OrderBy(x => (int)(object)x)
                .Select(x => x.ToString())
                .ToList();

            cmbRemoteExists.Items.Clear();
            cmbRemoteExists.Items.AddRange(sortedStrings.ToArray());
            cmbRemoteExists.SelectedIndex = 0;
        }

        private void InitializeTypeSize()
        {
            // Получение отсортированного списка строк
            List<string> sortedStrings = Enum.GetValues(typeof(TypeSize))
                .Cast<TypeSize>()
                .OrderBy(x => (int)(object)x)
                .Select(x => x.ToString())
                .ToList();

            cmbTypeSize.Items.Clear();
            cmbTypeSize.Items.AddRange(sortedStrings.ToArray());
            cmbTypeSize.SelectedIndex = 0;
        }

        private void InitializeTypeFtpVerify()
        {
            // Получение отсортированного списка строк
            List<string> sortedStrings = Enum.GetValues(typeof(FtpVerify))
                .Cast<FtpVerify>()
                .OrderBy(x => (int)(object)x)
                .Select(x => x.ToString())
                .ToList();

            cmbFtpVerify.Items.Clear();
            cmbFtpVerify.Items.AddRange(sortedStrings.ToArray());
            cmbFtpVerify.SelectedIndex = 0;
        }


        #region Form Load
        private void FrmAction_Load(object sender, EventArgs e)
        {
            ConfigToControls();
        }
        #endregion Form Load

        #region Config
        /// <summary>
        /// Transferring data from configuration (parameters) to form controls.
        /// <para>Передача данных из конфигурации (параметров) на контролы формы.</para>
        /// </summary>
        private void ConfigToControls()
        {
            ckbEnabled.Checked = operationAction.Enabled;
            cmbAction.SelectedIndex = (int)operationAction.Operation;
            txtLocalPath.Text = operationAction.LocalPath;
            txtRemotePath.Text = operationAction.RemotePath;

            cmbMode.SelectedIndex = (int)operationAction.Mode;
            cmbRemoteExists.SelectedIndex = (int)operationAction.RemoteExistsMode;
            cmbFtpVerify.SelectedIndex = (int)operationAction.FtpOptions;
            txtFormats.Text = String.Join(", ", operationAction.Formats);

            TypeSize typeSize = DriverUtils.DiskTypeSize(operationAction.MaxSizeFile);
            cmbTypeSize.SelectedIndex = (int)typeSize;
            nudSize.Value = Convert.ToDecimal(DriverUtils.DiskSizeNoUnit(operationAction.MaxSizeFile));
            if (operationAction.MaxSizeFile == 0)
            {
                nudSize.Enabled = false;
                cmbTypeSize.Enabled = false;
                ckbSize.Checked = false;
            }
            else
            {
                nudSize.Enabled = true;
                cmbTypeSize.Enabled = true;
                ckbSize.Checked = true;
            }

            Translate();
        }

        /// <summary>
        /// Transferring data from form controls to configuration (parameters).
        /// <para>Передача данных из контролов формы в конфигурацию (параметры).</para>
        /// </summary>
        private void ControlsToConfig()
        {
            operationAction.Enabled = ckbEnabled.Checked;
            operationAction.Operation = (OperationsActions)cmbAction.SelectedIndex;
            operationAction.LocalPath = txtLocalPath.Text;
            operationAction.RemotePath = txtRemotePath.Text;

            operationAction.Mode = (FtpFolderSyncMode)cmbMode.SelectedIndex;
            operationAction.RemoteExistsMode = (FtpRemoteExists)cmbRemoteExists.SelectedIndex;
            operationAction.FtpOptions = (FtpVerify)cmbFtpVerify.SelectedIndex;
            operationAction.Formats = txtFormats.Text.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();

            TypeSize typeSize = (TypeSize)cmbTypeSize.SelectedIndex;
            operationAction.MaxSizeFile = DriverUtils.ConvertToBytes(Convert.ToDouble(nudSize.Value), typeSize);

        }

        #endregion Config

        #region Translate
        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            // translate the form
            FormTranslator.Translate(this, "Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmAction");
        }
        #endregion Translate

        #region Control
        private void ckbSize_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbSize.Checked == true)
            {
                nudSize.Enabled = true;
                cmbTypeSize.Enabled = true;
            }
            else
            {
                nudSize.Value = 0;
                nudSize.Enabled = false;
                cmbTypeSize.Enabled = false;

            }
        }
        #endregion Control

        #region Button

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();

            DialogResult = DialogResult.OK;

            Close();
        }

        public void SaveData()
        {
            ControlsToConfig();
        }
        #endregion Save

        #region  Cancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
        #endregion  Cancel

        #endregion Button
    }
}
