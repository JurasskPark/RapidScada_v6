using FluentFTP;
using Scada.Forms;
using System.Data;
using static Scada.Comm.Drivers.DrvFtpJP.DriverUtils;
using static Scada.Comm.Drivers.DrvFtpJP.OperationAction;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmActionRemoteDownloadFile : Form
    {
        /// <summary>
        /// Initializes a new instance of the form.
        /// <para>Инициализирует новый экземпляр формы.</para>
        /// </summary>
        public FrmActionRemoteDownloadFile()
        {
            InitializeComponent();

            InitializeTypeOperationsActions();
            InitializeTypeLocalExists();
            InitializeTypeFtpVerify();
        }

        #region Variable
        public FrmConfig formParent;                // parent form
        public Project project;                     // the project configuration

        private bool modified;                      // the configuration was modified

        public OperationAction operationAction { get; set; }
        #endregion Variable

        /// <summary>
        /// Executes InitializeTypeOperationsActions.
        /// <para>Выполняет InitializeTypeOperationsActions.</para>
        /// </summary>
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

        /// <summary>
        /// Executes InitializeTypeLocalExists.
        /// <para>Выполняет InitializeTypeLocalExists.</para>
        /// </summary>
        private void InitializeTypeLocalExists()
        {
            // Получение отсортированного списка строк
            List<string> sortedStrings = Enum.GetValues(typeof(FtpLocalExists))
                .Cast<FtpLocalExists>()
                .OrderBy(x => (int)(object)x)
                .Select(x => x.ToString())
                .ToList();

            cmbLocalExists.Items.Clear();
            cmbLocalExists.Items.AddRange(sortedStrings.ToArray());
            cmbLocalExists.SelectedIndex = 0;
        }

        /// <summary>
        /// Executes InitializeTypeFtpVerify.
        /// <para>Выполняет InitializeTypeFtpVerify.</para>
        /// </summary>
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
        /// <summary>
        /// Handles the FrmAction_Load event.
        /// <para>Обрабатывает событие FrmAction_Load.</para>
        /// </summary>
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
            cmbLocalExists.SelectedIndex = (int)operationAction.LocalExistsMode;
            cmbFtpVerify.SelectedIndex = (int)operationAction.FtpOptions;

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
            operationAction.LocalExistsMode = (FtpLocalExists)cmbLocalExists.SelectedIndex;
            operationAction.FtpOptions = (FtpVerify)cmbFtpVerify.SelectedIndex;
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

        #endregion Control

        #region Button

        #region Save
        /// <summary>
        /// Handles the btnSave_Click event.
        /// <para>Обрабатывает событие btnSave_Click.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();

            DialogResult = DialogResult.OK;

            Close();
        }

        /// <summary>
        /// Executes SaveData.
        /// <para>Выполняет SaveData.</para>
        /// </summary>
        public void SaveData()
        {
            ControlsToConfig();
        }
        #endregion Save

        #region  Cancel
        /// <summary>
        /// Handles the btnCancel_Click event.
        /// <para>Обрабатывает событие btnCancel_Click.</para>
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
        #endregion  Cancel

        #endregion Button
    }
}
