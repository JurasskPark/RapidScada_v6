using FastColoredTextBoxNS;
using Scada.Forms;
using Scada.Lang;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    /// <summary>
    /// Form for setting up commands for data export.
    /// <para>Форма настройки команд для экспорта данных.</para>
    /// </summary>
    public partial class FrmExportCmd : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmExportCmd()
        {
            InitializeComponent();
            SetupHotkeys(fctCmdQuery);
            SetupHotkeys(fctResult);
        }

        #region Variables
        private bool modified;                                  // the configuration was modified
        public FrmProject formParent;                           // parent form
        public DrvDbImportPlusProject project;                  // the configuration
        public ExportCmd cmd = new ExportCmd();                 // the export command
        public List<DriverTag> tags = new List<DriverTag>();    // the tags

        private ListViewItem selected;                          // selected row
        private int indexSelectRow = 0;                         // index select row
        public Guid idRow;                                      // id row
        public string nameRow;                                  // name row
        #endregion Variables

        #region Form
        /// <summary>
        /// Loading the form.
        /// </summary>
        private void FrmExportCmd_Load(object sender, EventArgs e)
        {
            Translate();
            ProjectToControls();
            SetupHotkeys();
        }

        /// <summary>
        /// Intercepting button presses on the form.
        /// </summary>
        private void FrmExportCmd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                btnExecuteSQLQuery.PerformClick();
            }
        }
        #endregion Form

        #region Modified
        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// </summary>
        private bool Modified
        {
            get => modified;
            set
            {
                modified = value;
                btnSave.Enabled = modified;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// </summary>
        private void control_Changed(object sender, EventArgs e)
        {
            Modified = true;
        }
        #endregion Modified

        #region Project
        private void ProjectToControls()
        {
            if (cmd != null)
            {
                ckbEnabled.Checked = cmd.Enabled;
                nudCmdNum.Value = Convert.ToDecimal(cmd.CmdNum);
                txtCmdCode.Text = cmd.CmdCode;
                txtCmdName.Text = cmd.Name;
                txtCmdDescription.Text = cmd.Description;
                nudCmdStringLenght.Value = Convert.ToDecimal(cmd.Length <= 0 ? 80 : cmd.Length);
                fctCmdQuery.Text = cmd.Query;
            }

            Modified = false;
        }

        private void ControlsToProject()
        {
            cmd.Enabled = ckbEnabled.Checked;
            cmd.CmdNum = Convert.ToInt32(nudCmdNum.Value);
            cmd.CmdCode = txtCmdCode.Text;
            cmd.Name = txtCmdName.Text;
            cmd.Description = txtCmdDescription.Text;
            cmd.Length = Convert.ToInt32(nudCmdStringLenght.Value);
            cmd.Query = fctCmdQuery.Text;
        }
        #endregion Project

        #region Translate
        /// <summary>
        /// Translate form.
        /// </summary>
        private void Translate()
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            // tranlaste the menu
            FormTranslator.Translate(cmnuMenuScriptQuery, GetType().FullName);
        }

        #endregion Translate

        #region Control

        #region DataGridView
        /// <summary>
        /// Hiding errors in data processing.
        /// </summary>
        private void dgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
            e.Cancel = false;
        }
        #endregion DataGridView

        #region FastColorTextBox

        /// <summary>
        /// Hotkeys Mapping.
        /// </summary>
        private void SetupHotkeys()
        {
            fctCmdQuery.HotkeysMapping.Clear();

            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.C] = FCTBAction.Copy;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.X] = FCTBAction.Cut;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.V] = FCTBAction.Paste;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.A] = FCTBAction.SelectAll;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.Z] = FCTBAction.Undo;
            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.Y] = FCTBAction.Redo;

            fctCmdQuery.HotkeysMapping[Keys.Control | Keys.Insert] = FCTBAction.Copy;
            fctCmdQuery.HotkeysMapping[Keys.Shift | Keys.Insert] = FCTBAction.Paste;
            fctCmdQuery.HotkeysMapping[Keys.Shift | Keys.Delete] = FCTBAction.Cut;

            fctCmdQuery.HotkeysMapping[Keys.Up] = FCTBAction.GoUp;
            fctCmdQuery.HotkeysMapping[Keys.Down] = FCTBAction.GoDown;
            fctCmdQuery.HotkeysMapping[Keys.Left] = FCTBAction.GoLeft;
            fctCmdQuery.HotkeysMapping[Keys.Right] = FCTBAction.GoRight;

            fctResult.HotkeysMapping.Clear();

            fctResult.HotkeysMapping[Keys.Control | Keys.C] = FCTBAction.Copy;
            fctResult.HotkeysMapping[Keys.Control | Keys.X] = FCTBAction.Cut;
            fctResult.HotkeysMapping[Keys.Control | Keys.V] = FCTBAction.Paste;
            fctResult.HotkeysMapping[Keys.Control | Keys.A] = FCTBAction.SelectAll;
            fctResult.HotkeysMapping[Keys.Control | Keys.Z] = FCTBAction.Undo;
            fctResult.HotkeysMapping[Keys.Control | Keys.Y] = FCTBAction.Redo;

            fctResult.HotkeysMapping[Keys.Control | Keys.Insert] = FCTBAction.Copy;
            fctResult.HotkeysMapping[Keys.Shift | Keys.Insert] = FCTBAction.Paste;
            fctResult.HotkeysMapping[Keys.Shift | Keys.Delete] = FCTBAction.Cut;

            fctResult.HotkeysMapping[Keys.Up] = FCTBAction.GoUp;
            fctResult.HotkeysMapping[Keys.Down] = FCTBAction.GoDown;
            fctResult.HotkeysMapping[Keys.Left] = FCTBAction.GoLeft;
            fctResult.HotkeysMapping[Keys.Right] = FCTBAction.GoRight;
        }
        #endregion FastColorTextBox

        #region Button

        #region ExecuteSQLQuery
        /// <summary>
        /// Executing an SQL script.
        /// </summary>

        private async void btnExecuteSQLQuery_Click(object sender, EventArgs e)
        {
            try
            {
                ControlsToProject();
                btnExecuteSQLQuery.Enabled = false;
                await Task.Run(() => ExecuteSQLQuery());
            }
            catch (Exception ex)
            {
                ShowExceptionMessage(ex);
            }
            finally
            {
                btnExecuteSQLQuery.Enabled = true;
            }
        }

        #endregion ExecuteSQLQuery

        #region Save
        /// <summary>
        /// Saving settings.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToProject();
            DialogResult = DialogResult.OK;
            Close();
        }

        #endregion Save

        #region Close
        /// <summary>
        /// Close form.
        /// </summary>

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion Close

        #endregion Button

        #endregion Control

        #region Execute
        /// <summary>
        /// Executing an SQL script.
        /// </summary>
        private void ExecuteSQLQuery()
        {
            dgvData.Invoke(new Action(() => dgvData.DataSource = null));
            fctResult.Invoke(new Action(() => fctResult.Text = string.Empty));

            DebugerReturn.OnDebug = new DebugerReturn.DebugData(PollLogGet);
            DriverClient driverClient = new DriverClient(formParent.pathProject, formParent.project, formParent.deviceNum, formParent.pathProject, false);
            ImportCmd importCmd = new ImportCmd
            {
                Query = cmd.Query,
                IsColumnBased = true,
                DeviceTags = new List<DriverTag>()
            };
            var result = driverClient.Process(importCmd);

            dgvData.Invoke(new Action(() => dgvData.DataSource = result));
            driverClient.Dispose();
        }

        /// <summary>
        /// Poll Log Get.
        /// </summary>
        private void PollLogGet(string text, bool writeDateTime = true)
        {
            try
            {
                if (!IsHandleCreated)
                    CreateControl();

                if (IsHandleCreated)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        fctResult.AppendText(text + Environment.NewLine);
                    });
                }
                else
                {
                    fctResult.AppendText(text + Environment.NewLine);
                }
            }
            catch
            {
            }
        }
        #endregion Execute

        #region Menu Select

        /// <summary>
        /// Cut the text.
        /// </summary>
        private void cmnuScriptQueryCut_Click(object sender, EventArgs e)
        {
            fctCmdQuery.Cut();
        }

        /// <summary>
        /// Copy the text.
        /// </summary>
        private void cmnuScriptQueryCopy_Click(object sender, EventArgs e)
        {
            fctCmdQuery.Copy();
        }

        /// <summary>
        /// Paste the text.
        /// </summary>
        private void cmnuScriptQueryPaste_Click(object sender, EventArgs e)
        {
            fctCmdQuery.Paste();
        }

        /// <summary>
        /// Select the all text.
        /// </summary>
        private void cmnuScriptQuerySelectAll_Click(object sender, EventArgs e)
        {
            fctCmdQuery.Selection.SelectAll();
        }

        /// <summary>
        /// Undo the previous action.
        /// </summary>
        private void cmnuScriptQueryUndo_Click(object sender, EventArgs e)
        {
            if (fctCmdQuery.UndoEnabled)
            {
                fctCmdQuery.Undo();
            }
        }

        /// <summary>
        /// Redo the previous action.
        /// </summary>
        private void cmnuScriptQueryRedo_Click(object sender, EventArgs e)
        {
            if (fctCmdQuery.RedoEnabled)
            {
                fctCmdQuery.Redo();
            }
        }

        #endregion Menu Select

        #region Show Error
        /// <summary>
        /// Displaying an error message
        /// </summary>
        private void ShowExceptionMessage(Exception ex)
        {
            MessageBox.Show(this, ex.InnerException != null ? ex.InnerException.Message : ex.Message,
                Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion Show Error

        #region Help
        /// <summary>
        /// Enabling a combination of hotkeys in FastColorTextbox.
        /// <para>Включение комбинации горячих клавиш в FastColorTextbox.</para>
        /// </summary>
        private void SetupHotkeys(FastColoredTextBox fastColoredTextBox)
        {
            fastColoredTextBox.HotkeysMapping[Keys.Control | Keys.X] = FCTBAction.Cut;
            fastColoredTextBox.HotkeysMapping[Keys.Control | Keys.V] = FCTBAction.Paste;
            fastColoredTextBox.HotkeysMapping[Keys.Control | Keys.C] = FCTBAction.Copy;
            fastColoredTextBox.HotkeysMapping[Keys.Control | Keys.X] = FCTBAction.Cut;
            fastColoredTextBox.HotkeysMapping[Keys.Control | Keys.V] = FCTBAction.Paste;
            fastColoredTextBox.HotkeysMapping[Keys.Control | Keys.A] = FCTBAction.SelectAll;
            fastColoredTextBox.HotkeysMapping[Keys.Control | Keys.Z] = FCTBAction.Undo;
            fastColoredTextBox.HotkeysMapping[Keys.Control | Keys.Y] = FCTBAction.Redo;
        }
        #endregion Help
    }
}
