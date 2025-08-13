using FluentFTP;
using Scada.Forms;
using System.Data;
using static Scada.Comm.Drivers.DrvFtpJP.DriverUtils;
using static Scada.Comm.Drivers.DrvFtpJP.OperationAction;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmRemoteCreateDirectoty : Form
    {
        public FrmRemoteCreateDirectoty()
        {
            InitializeComponent();

            InitializeTypeOperationsActions();
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
            txtRemotePath.Text = operationAction.RemotePath;

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
            operationAction.RemotePath = txtRemotePath.Text;
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
