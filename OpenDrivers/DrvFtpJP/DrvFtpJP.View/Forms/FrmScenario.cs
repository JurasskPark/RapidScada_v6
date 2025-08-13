using Scada.Forms;
using Scada.Lang;
using System.Reflection;
using static Scada.Comm.Drivers.DrvFtpJP.OperationAction;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmScenario : Form
    {
        public FrmScenario()
        {
            InitializeComponent();
        }

        #region Variables
        public FrmConfig formParent;                // parent form
        public Project project;                     // the project configuration

        private bool modified;                      // the configuration was modified

        public Scenario scenario { get; set; }      // scenario
        private List<OperationAction> Actions { get; set; }

        private int indexSelectRow = 0;                 // index select row
        private ListViewItem selected;                  // selected row

        public Guid idRow;                              // id row
        public string nameRow;                          // name row
        #endregion Variables

        #region Initialize

        #endregion Initialize

        #region Form Load
        private void FrmAction_Load(object sender, EventArgs e)
        {
            ConfigToControls();
            RefreshData();
            Translate();
        }
        #endregion Form Load

        #region Config
        private void ConfigToControls()
        {
            ckbEnabled.Checked = scenario.Enabled;
            txtName.Text = scenario.Name;
            txtDescription.Text = scenario.Description;

            Actions = scenario.Actions;
        }

        private void ControlsToConfig()
        {
            scenario.Enabled = ckbEnabled.Checked;
            scenario.Name = txtName.Text.Trim();
            scenario.Description = txtDescription.Text.Trim();

            scenario.Actions = Actions;
        }

        /// <summary>
        /// Refresh data
        /// <para>Обновление данных</para>
        /// </summary>
        private void RefreshData()
        {
            // select scenarios
            try
            {
                if (Actions != null && Actions.Count > 0)
                {
                    // update without flicker
                    Type type = lstActions.GetType();
                    PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                    propertyInfo.SetValue(lstActions, true, null);

                    this.lstActions.BeginUpdate();
                    this.lstActions.Items.Clear();

                    #region Data display

                    foreach (OperationAction action in Actions)
                    {
                        // inserted information
                        this.lstActions.Items.Add(new ListViewItem()
                        {
                            // tag name
                            Text = DriverDictonary.OperationsActionsTypeString(action.Operation),
                            SubItems =
                                {
                                    // adding tag parameters
                                    ListViewAsDisplayStringBoolean(scenario.Enabled),
                                }
                        }).Tag = action.ID; // in tag we pass the tag id... so that we can find
                    }
                    #endregion Data display

                    this.lstActions.EndUpdate();
                }
                else
                {
                    this.lstActions.Items.Clear();
                }


            }
            catch { }

            try
            {
                if (indexSelectRow != 0 && indexSelectRow < lstActions.Items.Count)
                {
                    // scroll through
                    lstActions.EnsureVisible(indexSelectRow);
                    lstActions.TopItem = lstActions.Items[indexSelectRow];
                    // Making the area active
                    lstActions.Focus();
                    // making the desired element selected
                    lstActions.Items[indexSelectRow].Selected = true;
                    lstActions.Select();
                }
            }
            catch { }
        }
        #endregion Config

        #region Translate

        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            SetListViewColumnNames();

            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            // tranlaste the menu
            FormTranslator.Translate(cmnuMenu, GetType().FullName);
            // translate the listview
            FormTranslator.Translate(lstActions, GetType().FullName);
        }

        /// <summary>
        /// Sets the column names as needed for translation.
        /// </summary>
        private void SetListViewColumnNames()
        {
            clmName.Name = nameof(clmName);
            clmEnabled.Name = nameof(clmEnabled);
        }


        /// <summary>
        /// Translating values to listview.
        /// </summary>
        public string ListViewAsDisplayStringBoolean(bool boolTag)
        {
            string result = string.Empty;
            if (boolTag == false)
            {
                result = Locale.IsRussian ?
                        "Отключен" :
                        "Disabled";
                return result;
            }

            if (boolTag == true)
            {
                result = Locale.IsRussian ?
                       "Включен" :
                       "Enabled";
                return result;
            }

            return result;
        }

        #endregion Translate

        #region Control

        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// </summary>
        private bool Modified
        {
            get
            {
                return modified;
            }
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

            Modified = false;
        }
        #endregion Save

        #region Close

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
        #endregion Close

        #endregion Button

        #region ListView

        #region Select
        /// <summary>
        /// Mouse click on ListView
        /// </summary>
        private void lstAction_MouseClick(object sender, MouseEventArgs e)
        {
            ListSelect();
        }

        /// <summary>
        /// Mouse double click on ListView
        /// </summary>
        private void lstAction_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListSelect();
            ListChange();
        }

        /// <summary>
        /// List select
        /// </summary>
        private void ListSelect()
        {
            System.Windows.Forms.ListView tmplstActions = this.lstActions;
            if (tmplstActions.SelectedItems.Count <= 0)
            {
                return;
            }

            try
            {
                selected = tmplstActions.SelectedItems[0];
                indexSelectRow = tmplstActions.SelectedIndices[0];
                idRow = DriverUtils.StringToGuid(tmplstActions.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstActions.SelectedItems[0].SubItems[1].Text.Trim());
            }
            catch { }

        }
        #endregion Select

        #region Add

        /// <summary>
        /// Select from the menu add a row
        /// </summary>
        private void cmnuListAdd_Click(object sender, EventArgs e)
        {
            switch (((ToolStripMenuItem)sender).Tag.ToString())
            {
                case "None":
                    {
                        ListAdd((int)OperationsActions.None);
                        break;
                    }
                case "LocalCreateDirectory":
                    {
                        ListAdd((int)OperationsActions.LocalCreateDirectory);
                        break;
                    }
                case "RemoteCreateDirectory":
                    {
                        ListAdd((int)OperationsActions.RemoteCreateDirectory);
                        break;
                    }
                case "LocalRename":
                    {
                        ListAdd((int)OperationsActions.LocalRename);
                        break;
                    }
                case "RemoteRename":
                    {
                        ListAdd((int)OperationsActions.RemoteRename);
                        break;
                    }
                case "LocalDeleteFile":
                    {
                        ListAdd((int)OperationsActions.LocalDeleteFile);
                        break;
                    }
                case "LocalDeleteDirectory":
                    {
                        ListAdd((int)OperationsActions.LocalDeleteDirectory);
                        break;
                    }
                case "RemoteDeleteFile":
                    {
                        ListAdd((int)OperationsActions.RemoteDeleteFile);
                        break;
                    }
                case "RemoteDeleteDirectory":
                    {
                        ListAdd((int)OperationsActions.RemoteDeleteDirectory);
                        break;
                    }
                case "LocalUploadFile":
                    {
                        ListAdd((int)OperationsActions.LocalUploadFile);
                        break;
                    }
                case "LocalUploadDirectory":
                    {
                        ListAdd((int)OperationsActions.LocalUploadDirectory);
                        break;
                    }
                case "RemoteDownloadFile":
                    {
                        ListAdd((int)OperationsActions.RemoteDownloadFile);
                        break;
                    }
                case "RemoteDownloadDirectory":
                    {
                        ListAdd((int)OperationsActions.RemoteDownloadDirectory);
                        break;
                    }
            }
        }

        /// <summary>
        /// List add
        /// </summary>
        private void ListAdd(int value = 0)
        {
            try
            {
                OperationAction operationAction = new OperationAction();
                operationAction.ID = Guid.NewGuid();
                operationAction.Enabled = true;
                operationAction.Operation = (OperationsActions)value;

          
                switch(value)
                {
                    case 1:
                        // create form
                        FrmLocalCreateDirectoty frmLocalCreateDirectoty = new FrmLocalCreateDirectoty();
                        frmLocalCreateDirectoty.formParent = formParent;
                        frmLocalCreateDirectoty.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogLocalCreateDirectoty = frmLocalCreateDirectoty.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogLocalCreateDirectoty)
                        {
                            Actions.Add(frmLocalCreateDirectoty.operationAction);
                            RefreshData();
                        }
                        break;
                    case 2:
                        // create form
                        FrmRemoteCreateDirectoty frmRemoteCreateDirectoty = new FrmRemoteCreateDirectoty();
                        frmRemoteCreateDirectoty.formParent = formParent;
                        frmRemoteCreateDirectoty.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogRemoteCreateDirectoty = frmRemoteCreateDirectoty.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogRemoteCreateDirectoty)
                        {
                            Actions.Add(frmRemoteCreateDirectoty.operationAction);
                            RefreshData();
                        }
                        break;
                    case 3:
                        // create form
                        FrmActionLocalRename frmActionLocalRename = new FrmActionLocalRename();
                        frmActionLocalRename.formParent = formParent;
                        frmActionLocalRename.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalRename = frmActionLocalRename.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalRename)
                        {
                            Actions.Add(frmActionLocalRename.operationAction);
                            RefreshData();
                        }
                        break;
                    case 4:
                        // create form
                        FrmActionRemoteRename frmActionRemoteRename = new FrmActionRemoteRename();
                        frmActionRemoteRename.formParent = formParent;
                        frmActionRemoteRename.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteRename = frmActionRemoteRename.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteRename)
                        {
                            Actions.Add(frmActionRemoteRename.operationAction);
                            RefreshData();
                        }
                        break;
                    case 5:
                        // create form
                        FrmActionLocalDelete frmActionLocalDeleteFile = new FrmActionLocalDelete();
                        frmActionLocalDeleteFile.formParent = formParent;
                        operationAction.IsFile = true;
                        frmActionLocalDeleteFile.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalDeleteFile = frmActionLocalDeleteFile.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalDeleteFile)
                        {
                            Actions.Add(frmActionLocalDeleteFile.operationAction);
                            RefreshData();
                        }
                        break;
                    case 6:
                        // create form
                        FrmActionLocalDelete frmActionLocalDeleteDirectory = new FrmActionLocalDelete();
                        frmActionLocalDeleteDirectory.formParent = formParent;
                        operationAction.IsFile = false;
                        frmActionLocalDeleteDirectory.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalDeleteDirectory = frmActionLocalDeleteDirectory.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalDeleteDirectory)
                        {
                            Actions.Add(frmActionLocalDeleteDirectory.operationAction);
                            RefreshData();
                        }
                        break;
                    case 7:
                        // create form
                        FrmActionRemoteDelete frmActionRemoteDeleteFile = new FrmActionRemoteDelete();
                        frmActionRemoteDeleteFile.formParent = formParent;
                        operationAction.IsFile = true;
                        frmActionRemoteDeleteFile.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteDeleteFile = frmActionRemoteDeleteFile.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteDeleteFile)
                        {
                            Actions.Add(frmActionRemoteDeleteFile.operationAction);
                            RefreshData();
                        }
                        break;
                    case 8:
                        // create form
                        FrmActionRemoteDelete frmActionRemoteDeleteDirectory = new FrmActionRemoteDelete();
                        frmActionRemoteDeleteDirectory.formParent = formParent;
                        operationAction.IsFile = false;
                        frmActionRemoteDeleteDirectory.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteDeleteDirectory = frmActionRemoteDeleteDirectory.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteDeleteDirectory)
                        {
                            Actions.Add(frmActionRemoteDeleteDirectory.operationAction);
                            RefreshData();
                        }
                        break;
                    case 9:
                        // create form
                        FrmActionLocalUploadFile frmActionLocalUploadFile = new FrmActionLocalUploadFile();
                        frmActionLocalUploadFile.formParent = formParent;
                        operationAction.IsFile = true;
                        frmActionLocalUploadFile.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalUploadFile = frmActionLocalUploadFile.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalUploadFile)
                        {
                            Actions.Add(frmActionLocalUploadFile.operationAction);
                            RefreshData();
                        }
                        break;
                    case 10:
                        // create form
                        FrmActionLocalUploadDirectory frmActionLocalUploadDirectory = new FrmActionLocalUploadDirectory();
                        frmActionLocalUploadDirectory.formParent = formParent;
                        operationAction.IsFile = false;
                        frmActionLocalUploadDirectory.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalUploadDirectory = frmActionLocalUploadDirectory.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalUploadDirectory)
                        {
                            Actions.Add(frmActionLocalUploadDirectory.operationAction);
                            RefreshData();
                        }
                        break;
                    case 11:
                        // create form
                        FrmActionRemoteDownloadFile frmActionRemoteDownloadFile = new FrmActionRemoteDownloadFile();
                        frmActionRemoteDownloadFile.formParent = formParent;
                        operationAction.IsFile = true;
                        frmActionRemoteDownloadFile.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteDownloadFile = frmActionRemoteDownloadFile.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteDownloadFile)
                        {
                            Actions.Add(frmActionRemoteDownloadFile.operationAction);
                            RefreshData();
                        }
                        break;
                    case 12:
                        // create form
                        FrmActionRemoteDownloadDirectory frmActionRemoteDownloadDirectory = new FrmActionRemoteDownloadDirectory();
                        frmActionRemoteDownloadDirectory.formParent = formParent;
                        operationAction.IsFile = false;
                        frmActionRemoteDownloadDirectory.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteDownloadDirectory = frmActionRemoteDownloadDirectory.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteDownloadDirectory)
                        {
                            Actions.Add(frmActionRemoteDownloadDirectory.operationAction);
                            RefreshData();
                        }
                        break;
                    default:
                        // create form
                        FrmAction frmAction = new FrmAction();
                        frmAction.formParent = formParent;
                        frmAction.operationAction = operationAction;

                        // showing the form
                        DialogResult dialog = frmAction.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialog)
                        {
                            Actions.Add(frmAction.operationAction);
                            RefreshData();
                        }
                        break;
                }             
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #endregion Add

        #region Change
        /// <summary>
        /// Select from the menu change a row
        /// </summary>
        private void cmnuListChange_Click(object sender, EventArgs e)
        {
            ListChange();
        }

        /// <summary>
        /// List change
        /// </summary>
        private void ListChange()
        {
            try
            {
                System.Windows.Forms.ListView tmplstActions = this.lstActions;
                if (tmplstActions.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstActions.SelectedItems[0].Tag.ToString());

                OperationAction operationAction = Actions.Find(s => s.ID == idRow);
                int index = Actions.IndexOf(operationAction);

                int value = (int)operationAction.Operation;

                switch (value)
                {
                    case 1:
                        // create form
                        FrmLocalCreateDirectoty frmLocalCreateDirectoty = new FrmLocalCreateDirectoty();
                        frmLocalCreateDirectoty.formParent = formParent;
                        frmLocalCreateDirectoty.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogLocalCreateDirectoty = frmLocalCreateDirectoty.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogLocalCreateDirectoty)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmLocalCreateDirectoty.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 2:
                        // create form
                        FrmRemoteCreateDirectoty frmRemoteCreateDirectoty = new FrmRemoteCreateDirectoty();
                        frmRemoteCreateDirectoty.formParent = formParent;
                        frmRemoteCreateDirectoty.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogRemoteCreateDirectoty = frmRemoteCreateDirectoty.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogRemoteCreateDirectoty)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmRemoteCreateDirectoty.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 3:
                        // create form
                        FrmActionLocalRename frmActionLocalRename = new FrmActionLocalRename();
                        frmActionLocalRename.formParent = formParent;
                        frmActionLocalRename.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalRename = frmActionLocalRename.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalRename)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionLocalRename.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 4:
                        // create form
                        FrmActionRemoteRename frmActionRemoteRename = new FrmActionRemoteRename();
                        frmActionRemoteRename.formParent = formParent;
                        frmActionRemoteRename.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteRename = frmActionRemoteRename.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteRename)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionRemoteRename.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 5:
                        // create form
                        FrmActionLocalDelete frmActionLocalDeleteFile = new FrmActionLocalDelete();
                        frmActionLocalDeleteFile.formParent = formParent;
                        operationAction.IsFile = true;
                        frmActionLocalDeleteFile.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalDeleteFile = frmActionLocalDeleteFile.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalDeleteFile)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionLocalDeleteFile.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 6:
                        // create form
                        FrmActionLocalDelete frmActionLocalDeleteDirectory = new FrmActionLocalDelete();
                        frmActionLocalDeleteDirectory.formParent = formParent;
                        operationAction.IsFile = false;
                        frmActionLocalDeleteDirectory.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalDeleteDirectory = frmActionLocalDeleteDirectory.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalDeleteDirectory)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionLocalDeleteDirectory.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 7:
                        // create form
                        FrmActionRemoteDelete frmActionRemoteDeleteFile = new FrmActionRemoteDelete();
                        frmActionRemoteDeleteFile.formParent = formParent;
                        operationAction.IsFile = true;
                        frmActionRemoteDeleteFile.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteDeleteFile = frmActionRemoteDeleteFile.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteDeleteFile)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionRemoteDeleteFile.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 8:
                        // create form
                        FrmActionRemoteDelete frmActionRemoteDeleteDirectory = new FrmActionRemoteDelete();
                        frmActionRemoteDeleteDirectory.formParent = formParent;
                        operationAction.IsFile = false;
                        frmActionRemoteDeleteDirectory.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteDeleteDirectory = frmActionRemoteDeleteDirectory.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteDeleteDirectory)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionRemoteDeleteDirectory.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 9:
                        // create form
                        FrmActionLocalUploadFile frmActionLocalUploadFile = new FrmActionLocalUploadFile();
                        frmActionLocalUploadFile.formParent = formParent;
                        operationAction.IsFile = true;
                        frmActionLocalUploadFile.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalUploadFile = frmActionLocalUploadFile.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalUploadFile)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionLocalUploadFile.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 10:
                        // create form
                        FrmActionLocalUploadDirectory frmActionLocalUploadDirectory = new FrmActionLocalUploadDirectory();
                        frmActionLocalUploadDirectory.formParent = formParent;
                        operationAction.IsFile = false;
                        frmActionLocalUploadDirectory.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionLocalUploadDirectory = frmActionLocalUploadDirectory.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionLocalUploadDirectory)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionLocalUploadDirectory.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 11:
                        // create form
                        FrmActionRemoteDownloadFile frmActionRemoteDownloadFile = new FrmActionRemoteDownloadFile();
                        frmActionRemoteDownloadFile.formParent = formParent;
                        operationAction.IsFile = true;
                        frmActionRemoteDownloadFile.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteDownloadFile = frmActionRemoteDownloadFile.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteDownloadFile)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionRemoteDownloadFile.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    case 12:
                        // create form
                        FrmActionRemoteDownloadDirectory frmActionRemoteDownloadDirectory = new FrmActionRemoteDownloadDirectory();
                        frmActionRemoteDownloadDirectory.formParent = formParent;
                        operationAction.IsFile = false;
                        frmActionRemoteDownloadDirectory.operationAction = operationAction;

                        // showing the form
                        DialogResult dialogActionRemoteDownloadDirectory = frmActionRemoteDownloadDirectory.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialogActionRemoteDownloadDirectory)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmActionRemoteDownloadDirectory.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                    default:
                        // create form
                        FrmAction frmAction = new FrmAction();
                        frmAction.formParent = formParent;
                        frmAction.operationAction = operationAction;
                        // showing the form
                        DialogResult dialog = frmAction.ShowDialog();
                        // if you have closed the form, click Save
                        if (DialogResult.OK == dialog)
                        {
                            if (index != -1)
                            {
                                Actions[index] = frmAction.operationAction;
                                RefreshData();
                            }
                        }
                        break;
                }


                
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #endregion Change

        #region Delete
        /// <summary>
        /// Select from the menu delete a row
        /// </summary>
        private void cmnuListDelete_Click(object sender, EventArgs e)
        {
            ListDelete();
        }

        /// <summary>
        /// List delete
        /// </summary>
        private void ListDelete()
        {
            try
            {
                System.Windows.Forms.ListView tmplstActions = this.lstActions;
                if (tmplstActions.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstActions.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstActions.SelectedItems[0].SubItems[1].Text.Trim());

                // create dialog
                DialogResult dialog = MessageBox.Show(Locale.IsRussian ?
                    "Вы действительно хотите удалить запись?" :
                    "Are you sure you want to delete this entry?",
                    "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialog == DialogResult.OK)
                {
                    OperationAction operationAction = Actions.Find(s => s.ID == idRow);
                    int index = Actions.IndexOf(operationAction);

                    if (index != -1)
                    {
                        Actions.RemoveAt(index);
                        RefreshData();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        #endregion Delete

        #region Parser Up
        private void cmnuListUp_Click(object sender, EventArgs e)
        {
            ParserUp();
        }

        /// <summary>
        /// Parser Up
        /// </summary>
        private void ParserUp()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstActions = this.lstActions;
            if (tmplstActions.SelectedItems.Count <= 0)
            {
                return;
            }

            if (this.lstActions.SelectedItems.Count > 0)
            {
                if (Actions != null)
                {
                    ListViewItem selectedIndex = tmplstActions.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstActions.SelectedItems[0].Tag.ToString());

                    OperationAction operationAction = Actions.Find(s => s.ID == idRow);
                    int index = Actions.IndexOf(operationAction);

                    ListViewExtensions.MoveListViewItems(this.lstActions, MoveDirection.Up);

                    if (index == 0)
                    {
                        return;
                    }
                    else
                    {
                        Actions.Reverse(index - 1, 2);
                    }

                    SaveData();
                }
            }
        }
        #endregion Parser Up

        #region Parser Down
        private void cmnuListDown_Click(object sender, EventArgs e)
        {
            ParserDown();
        }

        /// <summary>
        /// Parser Up
        /// </summary>
        private void ParserDown()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstActions = this.lstActions;
            if (tmplstActions.SelectedItems.Count <= 0)
            {
                return;
            }

            if (this.lstActions.SelectedItems.Count > 0)
            {
                if (Actions != null)
                {
                    ListViewItem selectedIndex = tmplstActions.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstActions.SelectedItems[0].Tag.ToString());

                    OperationAction operationAction = Actions.Find(s => s.ID == idRow);
                    int index = Actions.IndexOf(operationAction);

                    ListViewExtensions.MoveListViewItems(this.lstActions, MoveDirection.Down);

                    if (index == Actions.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        Actions.Reverse(index, 2);
                    }

                    SaveData();
                }
            }
        }
        #endregion Parser Down

        #endregion ListView

    }
}
