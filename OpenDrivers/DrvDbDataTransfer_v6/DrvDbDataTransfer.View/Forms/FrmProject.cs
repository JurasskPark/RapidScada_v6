using Engine;
using Scada.Comm.Lang;
using Scada.Forms;
using Scada.Lang;
using System.Reflection;
using File = System.IO.File;

namespace Scada.Comm.Drivers.DrvDbDataTransfer.View.Forms
{
    /// <summary>
    /// Device configuration form.
    /// <para>Форма настройки конфигурации драйвера.</para>
    /// </summary>
    public partial class FrmProject : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmProject()
        {
            InitializeComponent();

            this.isDll = false;
            this.deviceNum = 0;
            this.driverCode = DriverUtils.DriverCode;

            this.appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.languageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Lang");
            this.pathLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log");
            this.pathProject = AppDomain.CurrentDomain.BaseDirectory;
            this.project = new DrvDbDataTransferProject();
            this.pathProject = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DriverUtils.GetFileName(deviceNum));

            modified = false;
            connChanging = false;
            listImportCommands = new List<ImportCmd>();
            listExportCommands = new List<ExportCmd>();

            // manager
            Manager.IsDll = this.isDll;
            Manager.DeviceNum = this.deviceNum;
            Manager.PathLog = this.pathLog;
            Manager.PathProject = this.pathProject;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmProject(AppDirs appDirs, int deviceNum)
            : this()
        {
            this.isDll = true;

            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.deviceNum = deviceNum;
            this.driverCode = DriverUtils.DriverCode;
            this.appDirectory = appDirs.ConfigDir;
            this.languageDir = appDirs.LangDir;
            this.pathLog = appDirs.LogDir;

            project = new DrvDbDataTransferProject();
            pathProject = Path.Combine(appDirs.ConfigDir, DriverUtils.GetFileName(deviceNum));
            
            modified = false;
            connChanging = false;
            listImportCommands = new List<ImportCmd>();
            listExportCommands = new List<ExportCmd>();

            // manager
            Manager.IsDll = this.isDll;
            Manager.DeviceNum = this.deviceNum;
            Manager.PathLog = this.pathLog;
            Manager.PathProject = this.pathProject;
        }

        #region Variables
        public readonly bool isDll;                     // application or dll
       
        private readonly string appDirectory;           // the applications directory
        private readonly string languageDir;            // the language directory

        private readonly AppDirs appDirs;               // the application directories
        public readonly string driverCode;              // the driver code
        public readonly int deviceNum;                  // the device number
        public readonly DrvDbDataTransferProject project; // the device configuration
        public string pathProject;                      // the configuration file path

        private readonly string pathLog;                // the path log
        private bool modified;                          // the configuration was modified
        private bool connChanging;                      // connection settings are changing

        private bool isRussian;                         // language


        private List<ImportCmd> listImportCommands;     // import commands
        public List<ImportCmd> ListImportCommands
        {
            get { return listImportCommands; }
            set { listImportCommands = value; }
        }

        private List<ExportCmd> listExportCommands;     // export commands
        public List<ExportCmd> ListExportCommands
        {
            get { return listExportCommands; }
            set { listExportCommands = value; }
        }

        private ListViewItem selected;                  // selected row
        private int indexSelectRow = 0;                 // index select row
        public Guid idRow;                              // id row
        public string nameRow;                          // name row
        #endregion Variables

        #region Form

        /// <summary>
        /// Loading the form.
        /// </summary>
        private void FrmProject_Load(object sender, EventArgs e)
        {
            // load a configuration
            if (File.Exists(pathProject) && !project.Load(pathProject, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            LoadLanguage(languageDir, Locale.IsRussian);

            // translate
            Translate();

            // display the configuration
            ProjectToControls();

            Text = string.Format(Text, deviceNum, DriverUtils.Version);

            Modified = false;
        }

        /// <summary>
        /// Closing the form.
        /// </summary>
        private void FrmProject_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Modified)
            {
                DialogResult result = MessageBox.Show(CommPhrases.SaveDeviceConfigConfirm,
                    CommonPhrases.QuestionCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        if (!project.Save(pathProject, out string errMsg))
                        {
                            ScadaUiUtils.ShowError(errMsg);
                            e.Cancel = true;
                        }
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        e.Cancel = true;
                        break;
                }
            }
        }

        #endregion Form

        #region Translate
        /// <summary>
        /// Load the driver translation.
        /// </summary>
        public void LoadLanguage(string languageDir, bool IsRussian = false)
        {
            isRussian = Locale.IsRussian;
            string culture = isRussian ? "ru-RU" : "en-GB";
            string fileName = $"{DriverUtils.DriverCode}.{culture}.xml";
            string languageFile = Path.Combine(languageDir, fileName);

            if (!File.Exists(languageFile))
            {
                languageFile = Path.Combine(languageDir, "Lang", fileName);
            }

            if (!Locale.LoadDictionaries(languageFile, out string errMsg) && errMsg != string.Empty)
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            // load translate the form
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDbDataTransfer.View.Forms.FrmProject");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDbDataTransfer.View.Forms.FrmImportCmd");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDbDataTransfer.View.Forms.FrmExportCmd");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDbDataTransfer.View.Forms.FrmInputBox");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDbDataTransfer.View.Forms.FrmTag");

            DriverPhrases.Init();

            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Sets the column names as needed for translation.
        /// </summary>
        private void SetListViewColumnNames()
        {
            clmImportCmdNum.Name = nameof(clmImportCmdNum);
            clmImportCmdCode.Name = nameof(clmImportCmdCode);
            clmImportCmdName.Name = nameof(clmImportCmdName);
            clmImportCmdDescription.Name = nameof(clmImportCmdDescription);
            clmImportCmdEnabled.Name = nameof(clmImportCmdEnabled);

            clmExportCmdNum.Name = nameof(clmExportCmdNum);
            clmExportCmdCode.Name = nameof(clmExportCmdCode);
            clmExportCmdName.Name = nameof(clmExportCmdName);
            clmExportCmdDescription.Name = nameof(clmExportCmdDescription);
            clmExportCmdEnabled.Name = nameof(clmExportCmdEnabled);
        }

        /// <summary>
        /// Translate form.
        /// </summary>
        private void Translate()
        {
            // column names
            SetListViewColumnNames();

            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            FormTranslator.Translate(txtHelp, GetType().FullName);
            // tranlaste the menu
            FormTranslator.Translate(cmnuImportCommands, GetType().FullName);
            FormTranslator.Translate(cmnuExportCommands, GetType().FullName);
        }

        /// <summary>
        /// Doesn't show value if it's zero.
        /// </summary>
        public string ListViewNoDisplayStringZero(string zeroValue)
        {
            string result = string.Empty;
            if (zeroValue == "0")
            {
                return string.Empty;
            }
            else
            {
                return zeroValue;
            }
        }

        /// <summary>
        /// Translating values to listview.
        /// </summary>
        public string ListViewAsDisplayStringBoolean(bool boolValue)
        {
            string result = string.Empty;
            if (boolValue == false)
            {
                result = Locale.IsRussian ?
                        "Отключен" :
                        "Disabled";
                return result;
            }

            if (boolValue == true)
            {
                result = Locale.IsRussian ?
                       "Включен" :
                       "Enabled";
                return result;
            }

            return result;
        }

        #endregion Translate

        #region Modified
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

        #endregion Modified

        #region Project
        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ProjectToControls()
        {
            connChanging = true;

            // source database
            SrcSettingsToControls();

            // target database
            TgtSettingsToControls();

            // import commands
            listImportCommands = project.ImportCmds;
            ListImportCmdToListView();

            // export commands
            listExportCommands = project.ExportCmds;
            ListExportCmdToListView();

            // log
            ckbWriteDriverLog.Checked = project.DebugerSettings.LogWrite;

            connChanging = false;
            Modified = false;
        }

        /// <summary>
        /// Displaying import commands in a ListView.
        /// </summary>
        private void ListImportCmdToListView()
        {
            if (listImportCommands != null)
            {
                Type type = lstImportCommands.GetType();
                PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(lstImportCommands, true, null);

                this.lstImportCommands.BeginUpdate();
                this.lstImportCommands.Items.Clear();

                foreach (var tmpImportCmd in listImportCommands)
                {
                    this.lstImportCommands.Items.Add(new ListViewItem()
                    {
                        Text = DriverUtils.NullToString(tmpImportCmd.CmdNum),
                        SubItems =
                            {
                                DriverUtils.NullToString(tmpImportCmd.CmdCode),
                                DriverUtils.NullToString(tmpImportCmd.Name),
                                DriverUtils.NullToString(tmpImportCmd.Description),
                                ListViewAsDisplayStringBoolean(tmpImportCmd.Enabled),
                            }
                    }).Tag = tmpImportCmd.Id;
                }

                this.lstImportCommands.EndUpdate();
            }

            ListViewExtensions.ResizeColumn(lstImportCommands, "clmImportCmdNum");
            ListViewExtensions.ResizeColumn(lstImportCommands, "clmImportCmdCode");
            ListViewExtensions.ResizeColumn(lstImportCommands, "clmImportCmdName");
            ListViewExtensions.ResizeColumn(lstImportCommands, "clmImportCmdDescription");
            ListViewExtensions.ResizeColumnDefault(lstImportCommands, "clmImportCmdEnabled");

            try
            {
                if (indexSelectRow < lstImportCommands.Items.Count)
                {
                    lstImportCommands.EnsureVisible(indexSelectRow);
                    lstImportCommands.TopItem = lstImportCommands.Items[indexSelectRow];
                    lstImportCommands.Focus();
                    lstImportCommands.Items[indexSelectRow].Selected = true;
                    lstImportCommands.Select();
                }
            }
            catch { }
        }

        /// <summary>
        /// Displaying export commands in a ListView.
        /// </summary>
        private void ListExportCmdToListView()
        {
            if (listExportCommands != null)
            {
                Type type = lstExportCommands.GetType();
                PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(lstExportCommands, true, null);

                this.lstExportCommands.BeginUpdate();
                this.lstExportCommands.Items.Clear();

                foreach (var tmpExportCmd in listExportCommands)
                {
                    this.lstExportCommands.Items.Add(new ListViewItem()
                    {
                        Text = DriverUtils.NullToString(tmpExportCmd.CmdNum),
                        SubItems =
                    {
                        DriverUtils.NullToString(tmpExportCmd.CmdCode),
                        DriverUtils.NullToString(tmpExportCmd.Name),
                        DriverUtils.NullToString(tmpExportCmd.Description),
                        ListViewAsDisplayStringBoolean(tmpExportCmd.Enabled),
                    }
                    }).Tag = tmpExportCmd.Id;
                }

                this.lstExportCommands.EndUpdate();
            }

            ListViewExtensions.ResizeColumn(lstExportCommands, "clmExportCmdNum");
            ListViewExtensions.ResizeColumn(lstExportCommands, "clmExportCmdCode");
            ListViewExtensions.ResizeColumn(lstExportCommands, "clmExportCmdName");
            ListViewExtensions.ResizeColumn(lstExportCommands, "clmExportCmdDescription");
            ListViewExtensions.ResizeColumnDefault(lstExportCommands, "clmExportCmdEnabled");

            try
            {
                if (indexSelectRow < lstExportCommands.Items.Count)
                {
                    lstExportCommands.EnsureVisible(indexSelectRow);
                    lstExportCommands.TopItem = lstExportCommands.Items[indexSelectRow];
                    lstExportCommands.Focus();
                    lstExportCommands.Items[indexSelectRow].Selected = true;
                    lstExportCommands.Select();
                }
            }
            catch { }
        }

        /// <summary>
        /// Sets the configuration parameters according to the controls.
        /// </summary>
        private void ControlsToProject()
        {
            ControlsToSrcSettings();
            ControlsToTgtSettings();
            project.DebugerSettings.LogWrite = ckbWriteDriverLog.Checked;
        }

        /// <summary>
        /// Displays source database settings.
        /// <para>Отображает настройки исходной БД.</para>
        /// </summary>
        private void SrcSettingsToControls()
        {
            DbConnSettings s = project.SourceDbConnSettings;
            cbSrcDataSourceType.SelectedIndex = (int)s.DataSourceType;
            txtSrcServer.Text = s.Server;
            txtSrcPort.Text = s.Port;
            txtSrcDatabase.Text = s.Database;
            txtSrcUser.Text = s.User;
            txtSrcPassword.Text = s.Password;
            txtSrcOptionalOptions.Text = s.OptionalOptions;
            txtSrcTimeout.Text = s.Timeout;
            txtSrcConnectionString.Text = s.ConnectionString;
            ApplyConnMode(true);
        }

        private void ControlsToSrcSettings()
        {
            DbConnSettings s = project.SourceDbConnSettings;
            s.DataSourceType = (DataSourceType)cbSrcDataSourceType.SelectedIndex;
            s.Server = txtSrcServer.Text;
            s.Port = txtSrcPort.Text;
            s.Database = txtSrcDatabase.Text;
            s.User = txtSrcUser.Text;
            s.Password = txtSrcPassword.Text;
            s.OptionalOptions = txtSrcOptionalOptions.Text;
            s.Timeout = txtSrcTimeout.Text;
            s.ConnectionString = txtSrcConnectionString.Text == BuildConnString(true) ? "" : txtSrcConnectionString.Text;
        }

        /// <summary>
        /// Displays target database settings.
        /// <para>Отображает настройки целевой БД.</para>
        /// </summary>
        private void TgtSettingsToControls()
        {
            DbConnSettings t = project.TargetDbConnSettings;
            cbTgtDataSourceType.SelectedIndex = (int)t.DataSourceType;
            txtTgtServer.Text = t.Server;
            txtTgtPort.Text = t.Port;
            txtTgtDatabase.Text = t.Database;
            txtTgtUser.Text = t.User;
            txtTgtPassword.Text = t.Password;
            txtTgtOptionalOptions.Text = t.OptionalOptions;
            txtTgtTimeout.Text = t.Timeout;
            txtTgtConnectionString.Text = t.ConnectionString;
            ApplyConnMode(false);
        }

        private void ControlsToTgtSettings()
        {
            DbConnSettings t = project.TargetDbConnSettings;
            t.DataSourceType = (DataSourceType)cbTgtDataSourceType.SelectedIndex;
            t.Server = txtTgtServer.Text;
            t.Port = txtTgtPort.Text;
            t.Database = txtTgtDatabase.Text;
            t.User = txtTgtUser.Text;
            t.Password = txtTgtPassword.Text;
            t.OptionalOptions = txtTgtOptionalOptions.Text;
            t.Timeout = txtTgtTimeout.Text;
            t.ConnectionString = txtTgtConnectionString.Text == BuildConnString(false) ? "" : txtTgtConnectionString.Text;
        }

        #endregion Project

        #region Control

        #region ListView Import Commands

        #region Select
        /// <summary>
        /// Mouse click on ListView
        /// </summary>
        private void lstImportCommands_MouseClick(object sender, MouseEventArgs e)
        {
            ListSelectImportCommand();
        }

        /// <summary>
        /// Mouse double click on ListView
        /// </summary>
        private void lstImportCommands_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListSelectImportCommand();
            ListImportCmdChange();
        }

        /// <summary>
        /// List select
        /// </summary>
        private void ListSelectImportCommand()
        {
            System.Windows.Forms.ListView tmplstImportCommands = this.lstImportCommands;
            if (tmplstImportCommands.SelectedItems.Count <= 0)
            {
                return;
            }

            try
            {
                selected = tmplstImportCommands.SelectedItems[0];
                indexSelectRow = tmplstImportCommands.SelectedIndices[0];
                idRow = DriverUtils.StringToGuid(tmplstImportCommands.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstImportCommands.SelectedItems[0].SubItems[1].Text.Trim());
            }
            catch { }

        }
        #endregion Select

        #region Add
        /// <summary>
        /// Select from the menu add a row
        /// </summary>
        private void cmnuListImportCmdAdd_Click(object sender, EventArgs e)
        {
            ListImportCmdAdd();
        }

        /// <summary>
        /// List add
        /// </summary>
        private void ListImportCmdAdd()
        {
            try
            {
                ImportCmd cmd = new ImportCmd();
                cmd.Id = Guid.NewGuid();
                cmd.CmdNum = listImportCommands.Count + 1;
                cmd.CmdCode = "DBIMPORTCODE" + (listImportCommands.Count + 1).ToString();
                cmd.IsColumnBased = true;

                // create form
                FrmImportCmd frmImportCmd = new FrmImportCmd();
                frmImportCmd.formParent = this;
                frmImportCmd.cmd = cmd;
                // showing the form
                DialogResult dialog = frmImportCmd.ShowDialog();
                // if you have closed the form, click Save
                if (DialogResult.OK == dialog)
                {
                    ListImportCommands.Add(frmImportCmd.cmd);
                    project.ImportCmds = ListImportCommands;

                    ControlsToProject();
                    ProjectToControls();
                    Modified = true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #endregion Add

        #region Change
        /// <summary>
        /// Select from the menu change a row
        /// </summary>
        private void cmnuListImportCmdChange_Click(object sender, EventArgs e)
        {
            ListImportCmdChange();
        }

        /// <summary>
        /// List change
        /// </summary>
        private void ListImportCmdChange()
        {
            try
            {
                System.Windows.Forms.ListView tmplstImportCommands = this.lstImportCommands;
                if (tmplstImportCommands.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstImportCommands.SelectedItems[0].Tag.ToString());

                ImportCmd cmd = ListImportCommands.Find(s => s.Id == idRow);
                int index = ListImportCommands.IndexOf(cmd);

                // create form
                FrmImportCmd frmImportCmd = new FrmImportCmd();
                frmImportCmd.formParent = this;
                frmImportCmd.cmd = cmd;
                // showing the form
                DialogResult dialog = frmImportCmd.ShowDialog();
                // if you have closed the form, click Save
                if (DialogResult.OK == dialog)
                {
                    if (index != -1)
                    {
                        ListImportCommands[index] = frmImportCmd.cmd;

                        ControlsToProject();
                        ProjectToControls();
                        Modified = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #endregion Change

        #region Delete
        /// <summary>
        /// Select from the menu delete a row
        /// </summary>
        private void cmnuListImportCmdDelete_Click(object sender, EventArgs e)
        {
            ListImportCmdDelete();
        }

        /// <summary>
        /// List delete
        /// </summary>
        private void ListImportCmdDelete()
        {
            try
            {
                System.Windows.Forms.ListView tmplstImportCommands = this.lstImportCommands;
                if (tmplstImportCommands.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstImportCommands.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstImportCommands.SelectedItems[0].SubItems[1].Text.Trim());

                // create dialog
                DialogResult dialog = MessageBox.Show(Locale.IsRussian ?
                    "Вы действительно хотите удалить запись?" :
                    "Are you sure you want to delete this entry?",
                    "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialog == DialogResult.OK)
                {
                    ImportCmd cmd = ListImportCommands.Find(s => s.Id == idRow);
                    int index = ListImportCommands.IndexOf(cmd);
                    if (index != -1)
                    {
                        ListImportCommands.RemoveAt(index);

                        ControlsToProject();
                        ProjectToControls();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        #endregion Delete

        #region Up
        /// <summary>
        /// Select from the menu up a row.
        /// </summary>
        private void cmnuListImportCmdUp_Click(object sender, EventArgs e)
        {
            ListImportCmdUp();
        }

        /// <summary>
        /// Move an item up in a list.
        /// </summary>
        private void ListImportCmdUp()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstImportCommands = this.lstImportCommands;
            if (tmplstImportCommands.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstImportCommands.SelectedItems.Count > 0)
            {
                if (ListImportCommands != null)
                {
                    ListViewItem selectedIndex = tmplstImportCommands.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstImportCommands.SelectedItems[0].Tag.ToString());

                    ImportCmd cmd = ListImportCommands.Find(s => s.Id == idRow);
                    int index = ListImportCommands.IndexOf(cmd);

                    ListViewExtensions.MoveListViewItems(lstImportCommands, MoveDirection.Up);

                    if (index == 0)
                    {
                        return;
                    }
                    else
                    {
                        ListImportCommands.Reverse(index - 1, 2);
                    }

                    ControlsToProject();
                    ProjectToControls();
                }
            }
        }
        #endregion Up

        #region Down
        /// <summary>
        /// Select from the menu down a row.
        /// </summary>
        private void cmnuListImportCmdDown_Click(object sender, EventArgs e)
        {
            ListImportCmdDown();
        }

        /// <summary>
        /// Move an item down in a list.
        /// </summary>
        private void ListImportCmdDown()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstImportCommands = this.lstImportCommands;
            if (tmplstImportCommands.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstImportCommands.SelectedItems.Count > 0)
            {
                if (ListImportCommands != null)
                {
                    ListViewItem selectedIndex = tmplstImportCommands.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstImportCommands.SelectedItems[0].Tag.ToString());

                    ImportCmd cmd = ListImportCommands.Find(s => s.Id == idRow);
                    int index = ListImportCommands.IndexOf(cmd);

                    ListViewExtensions.MoveListViewItems(lstImportCommands, MoveDirection.Down);

                    if (index == ListImportCommands.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        ListImportCommands.Reverse(index, 2);
                    }

                    ControlsToProject();
                    ProjectToControls();
                }
            }
        }
        #endregion Down

        #endregion ListView Import Commands

        #region ListView Export Commands

        #region Select
        /// <summary>
        /// Mouse click on ListView
        /// </summary>
        private void lstExportCommands_MouseClick(object sender, MouseEventArgs e)
        {
            ListSelectExportCommand();
        }

        /// <summary>
        /// Mouse double click on ListView
        /// </summary>
        private void lstExportCommands_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListSelectExportCommand();
            ListExportCmdChange();
        }

        /// <summary>
        /// List select
        /// </summary>
        private void ListSelectExportCommand()
        {
            System.Windows.Forms.ListView tmplstExportCommands = this.lstExportCommands;
            if (tmplstExportCommands.SelectedItems.Count <= 0)
            {
                return;
            }

            try
            {
                selected = tmplstExportCommands.SelectedItems[0];
                indexSelectRow = tmplstExportCommands.SelectedIndices[0];
                idRow = DriverUtils.StringToGuid(tmplstExportCommands.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstExportCommands.SelectedItems[0].SubItems[1].Text.Trim());
            }
            catch { }

        }
        #endregion Select

        #region Add
        /// <summary>
        /// Select from the menu add a row
        /// </summary>
        private void cmnuListExportCmdAdd_Click(object sender, EventArgs e)
        {
            ListExportCmdAdd();
        }

        /// <summary>
        /// List add
        /// </summary>
        private void ListExportCmdAdd()
        {
            try
            {
                ExportCmd cmd = new ExportCmd();
                cmd.Id = Guid.NewGuid();
                cmd.CmdNum = listExportCommands.Count + 1;
                cmd.CmdCode = "DBEXPORTCODE" + (listExportCommands.Count + 1).ToString();
                cmd.IsColumnBased = true;

                // create form
                FrmExportCmd frmExportCmd = new FrmExportCmd();
                frmExportCmd.formParent = this;
                frmExportCmd.cmd = cmd;
                // showing the form
                DialogResult dialog = frmExportCmd.ShowDialog();
                // if you have closed the form, click Save
                if (DialogResult.OK == dialog)
                {
                    ListExportCommands.Add(frmExportCmd.cmd);
                    project.ExportCmds = ListExportCommands;

                    ControlsToProject();
                    ProjectToControls();
                    Modified = true;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #endregion Add

        #region Change
        /// <summary>
        /// Select from the menu change a row
        /// </summary>
        private void cmnuListExportCmdChange_Click(object sender, EventArgs e)
        {
            ListExportCmdChange();
        }

        /// <summary>
        /// List change
        /// </summary>
        private void ListExportCmdChange()
        {
            try
            {
                System.Windows.Forms.ListView tmplstExportCommands = this.lstExportCommands;
                if (tmplstExportCommands.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstExportCommands.SelectedItems[0].Tag.ToString());

                ExportCmd cmd = ListExportCommands.Find(s => s.Id == idRow);
                int index = ListExportCommands.IndexOf(cmd);

                // create form
                FrmExportCmd frmExportCmd = new FrmExportCmd();
                frmExportCmd.formParent = this;
                frmExportCmd.cmd = cmd;
                // showing the form
                DialogResult dialog = frmExportCmd.ShowDialog();
                // if you have closed the form, click Save
                if (DialogResult.OK == dialog)
                {
                    if (index != -1)
                    {
                        ListExportCommands[index] = frmExportCmd.cmd;

                        ControlsToProject();
                        ProjectToControls();
                        Modified = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        #endregion Change

        #region Delete
        /// <summary>
        /// Select from the menu delete a row
        /// </summary>
        private void cmnuListExportCmdDelete_Click(object sender, EventArgs e)
        {
            ListExportCmdDelete();
        }

        /// <summary>
        /// List delete
        /// </summary>
        private void ListExportCmdDelete()
        {
            try
            {
                System.Windows.Forms.ListView tmplstExportCommands = this.lstExportCommands;
                if (tmplstExportCommands.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstExportCommands.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstExportCommands.SelectedItems[0].SubItems[1].Text.Trim());

                // create dialog
                DialogResult dialog = MessageBox.Show(Locale.IsRussian ?
                    "Вы действительно хотите удалить запись?" :
                    "Are you sure you want to delete this entry?",
                    "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialog == DialogResult.OK)
                {
                    ExportCmd cmd = ListExportCommands.Find(s => s.Id == idRow);
                    int index = ListExportCommands.IndexOf(cmd);
                    if (index != -1)
                    {
                        ListExportCommands.RemoveAt(index);

                        ControlsToProject();
                        ProjectToControls();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
        #endregion Delete

        #region Up
        /// <summary>
        /// Select from the menu up a row.
        /// </summary>
        private void cmnuListExportCmdUp_Click(object sender, EventArgs e)
        {
            ListExportCmdUp();
        }

        /// <summary>
        /// Move an item up in a list.
        /// </summary>
        private void ListExportCmdUp()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstExportCommands = this.lstExportCommands;
            if (tmplstExportCommands.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstExportCommands.SelectedItems.Count > 0)
            {
                if (ListExportCommands != null)
                {
                    ListViewItem selectedIndex = tmplstExportCommands.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstExportCommands.SelectedItems[0].Tag.ToString());

                    ExportCmd cmd = ListExportCommands.Find(s => s.Id == idRow);
                    int index = ListExportCommands.IndexOf(cmd);

                    ListViewExtensions.MoveListViewItems(lstExportCommands, MoveDirection.Up);

                    if (index == 0)
                    {
                        return;
                    }
                    else
                    {
                        ListExportCommands.Reverse(index - 1, 2);
                    }

                    ControlsToProject();
                    ProjectToControls();
                }
            }
        }
        #endregion Up

        #region Down
        /// <summary>
        /// Select from the menu down a row.
        /// </summary>
        private void cmnuListExportCmdDown_Click(object sender, EventArgs e)
        {
            ListExportCmdDown();
        }

        /// <summary>
        /// Move an item down in a list.
        /// </summary>
        private void ListExportCmdDown()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstExportCommands = this.lstExportCommands;
            if (tmplstExportCommands.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstExportCommands.SelectedItems.Count > 0)
            {
                if (ListExportCommands != null)
                {
                    ListViewItem selectedIndex = tmplstExportCommands.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstExportCommands.SelectedItems[0].Tag.ToString());

                    ExportCmd cmd = ListExportCommands.Find(s => s.Id == idRow);
                    int index = ListExportCommands.IndexOf(cmd);

                    ListViewExtensions.MoveListViewItems(lstExportCommands, MoveDirection.Down);

                    if (index == ListExportCommands.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        ListExportCommands.Reverse(index, 2);
                    }

                    ControlsToProject();
                    ProjectToControls();
                }
            }
        }
        #endregion Down

        #endregion ListView Export Commands

        #region Tab Database

        /// <summary>
        /// Source DBMS type changed.
        /// </summary>
        private void cbDataSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                connChanging = true;
                ApplyConnMode(true);
                Modified = true;
                connChanging = false;
            }
        }

        /// <summary>
        /// Target DBMS type changed.
        /// </summary>
        private void cbTgtDataSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                connChanging = true;
                ApplyConnMode(false);
                Modified = true;
                connChanging = false;
            }
        }

        /// <summary>
        /// Applies connection properties or connection string display mode.
        /// <para>Применяет режим отображения свойств подключения или строки подключения.</para>
        /// </summary>
        private void ApplyConnMode(bool isSrc)
        {
            ComboBox cb = isSrc ? cbSrcDataSourceType : cbTgtDataSourceType;
            TextBox txtServer = isSrc ? txtSrcServer : txtTgtServer;
            TextBox txtDatabase = isSrc ? txtSrcDatabase : txtTgtDatabase;
            TextBox txtUser = isSrc ? txtSrcUser : txtTgtUser;
            TextBox txtPassword = isSrc ? txtSrcPassword : txtTgtPassword;
            TextBox txtPort = isSrc ? txtSrcPort : txtTgtPort;
            TextBox txtOpts = isSrc ? txtSrcOptionalOptions : txtTgtOptionalOptions;
            TextBox txtConnStr = isSrc ? txtSrcConnectionString : txtTgtConnectionString;

            if (cb.SelectedIndex == 0)
            {
                txtConnStr.Text = "";
                SetConnStringMode(txtServer, txtDatabase, txtUser, txtPassword, txtPort, txtOpts, txtConnStr, false);
            }
            else
            {
                string connStr = BuildConnString(isSrc);
                if (string.IsNullOrEmpty(connStr) || !string.IsNullOrEmpty(txtConnStr.Text))
                {
                    SetConnStringMode(txtServer, txtDatabase, txtUser, txtPassword, txtPort, txtOpts, txtConnStr, true);
                }
                else
                {
                    SetConnStringMode(txtServer, txtDatabase, txtUser, txtPassword, txtPort, txtOpts, txtConnStr, false);
                }
            }
        }

        private static void SetConnStringMode(TextBox txtServer, TextBox txtDatabase, TextBox txtUser,
            TextBox txtPassword, TextBox txtPort, TextBox txtOpts, TextBox txtConnStr, bool connStrMode)
        {
            Color winColor = Color.FromKnownColor(KnownColor.Window);
            Color ctrlColor = Color.FromKnownColor(KnownColor.Control);

            txtServer.BackColor = txtDatabase.BackColor = txtUser.BackColor = txtPassword.BackColor =
                txtPort.BackColor = txtOpts.BackColor = connStrMode ? ctrlColor : winColor;
            txtServer.Enabled = txtDatabase.Enabled = txtUser.Enabled = txtPassword.Enabled =
                txtPort.Enabled = txtOpts.Enabled = !connStrMode;
            txtConnStr.BackColor = connStrMode ? winColor : ctrlColor;
            txtConnStr.Enabled = connStrMode;
        }

        /// <summary>
        /// Builds a connection string from currently displayed source or target controls.
        /// <para>Формирует строку подключения из текущих полей источника или приемника.</para>
        /// </summary>
        private string BuildConnString(bool isSrc)
        {
            DataSourceType dsType = (DataSourceType)(isSrc ? cbSrcDataSourceType : cbTgtDataSourceType).SelectedIndex;

            DbConnSettings cs = new DbConnSettings
            {
                Server = isSrc ? txtSrcServer.Text : txtTgtServer.Text,
                Database = isSrc ? txtSrcDatabase.Text : txtTgtDatabase.Text,
                Port = isSrc ? txtSrcPort.Text : txtTgtPort.Text,
                User = isSrc ? txtSrcUser.Text : txtTgtUser.Text,
                Password = isSrc ? txtSrcPassword.Text : txtTgtPassword.Text,
                Timeout = isSrc ? txtSrcTimeout.Text : txtTgtTimeout.Text,
                OptionalOptions = isSrc ? txtSrcOptionalOptions.Text : txtTgtOptionalOptions.Text
            };

            switch (dsType)
            {
                case DataSourceType.MSSQL: return SqlDataSource.BuildSqlConnectionString(cs);
                case DataSourceType.Oracle: return OraDataSource.BuildOraConnectionString(cs);
                case DataSourceType.PostgreSQL: return PgSqlDataSource.BuildPgSqlConnectionString(cs);
                case DataSourceType.MySQL: return MySqlDataSource.BuildMySqlConnectionString(cs);
                case DataSourceType.Firebird: return FirebirdDataSource.BuildFbConnectionString(cs);
                case DataSourceType.InfluxDBv2: return InfluxDBHttpDataSource.BuildInfluxConnectionString(cs);
                case DataSourceType.InfluxDBv3: return InfluxDBHttpDataSource.BuildInfluxConnectionString(cs);
                default: return "";
            }
        }

        /// <summary>
        /// Handles connection property text changes.
        /// <para>Обрабатывает изменение свойств подключения.</para>
        /// </summary>
        private void txtSrcConnProp_TextChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                string connStr = BuildConnString(true);
                if (!string.IsNullOrEmpty(connStr))
                {
                    connChanging = true;
                    ApplyConnMode(true);
                    connChanging = false;
                }
                Modified = true;
            }
        }

        private void txtTgtConnProp_TextChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                string connStr = BuildConnString(false);
                if (!string.IsNullOrEmpty(connStr))
                {
                    connChanging = true;
                    ApplyConnMode(false);
                    connChanging = false;
                }
                Modified = true;
            }
        }

        /// <summary>
        /// Handles source connection string text changes.
        /// <para>Обрабатывает изменение строки подключения источника.</para>
        /// </summary>
        private void txtSrcConnectionString_TextChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                ApplyConnMode(true);
                Modified = true;
            }
        }

        private void txtTgtConnectionString_TextChanged(object sender, EventArgs e)
        {
            if (!connChanging)
            {
                ApplyConnMode(false);
                Modified = true;
            }
        }

        /// <summary>
        /// Tests source connection.
        /// <para>Проверяет подключение к источнику.</para>
        /// </summary>
        private void btnSrcConnectionTest_Click(object sender, EventArgs e)
        {
            string roleName = Locale.IsRussian ? "Источник" : "Source";
            TestConnection(project.SourceDbConnSettings, roleName);
        }

        /// <summary>
        /// Tests target connection.
        /// <para>Проверяет подключение к приемнику.</para>
        /// </summary>
        private void btnTgtConnectionTest_Click(object sender, EventArgs e)
        {
            string roleName = Locale.IsRussian ? "Приемник" : "Target";
            TestConnection(project.TargetDbConnSettings, roleName);
        }

        private void TestConnection(DbConnSettings connSettings, string roleName)
        {
            string errMsg;
            if (ValidateConnection(connSettings, out errMsg))
            {
                MessageBox.Show(string.Format(Locale.IsRussian ?
                    "Соединение с {0} успешно прошло!" :
                    "Connection to {0} was successful!", roleName));
            }
            else
            {
                if (!string.IsNullOrEmpty(errMsg))
                {
                    MessageBox.Show(errMsg);
                }
            }
        }

        /// <summary>
        /// Validates database connection settings by opening a real connection.
        /// <para>Проверяет настройки подключения к БД реальным соединением.</para>
        /// </summary>
        private bool ValidateConnection(DbConnSettings connSettings, out string errMsg)
        {
            errMsg = string.Empty;
            ControlsToProject();

            if (!project.Save(pathProject, out errMsg))
            {
                return false;
            }

            if (File.Exists(pathProject) && !project.Load(pathProject, out errMsg))
            {
                return false;
            }

            ProjectToControls();

            DataSource ds = DataSource.GetDataSourceType(connSettings);
            if (ds == null)
            {
                errMsg = Locale.IsRussian ?
                    "Тип источника данных не задан или не поддерживается" :
                    "Data source type is not set or not supported";
                return false;
            }

            string connStr = string.IsNullOrEmpty(connSettings.ConnectionString) ?
                ds.BuildConnectionString(connSettings) : connSettings.ConnectionString;

            if (string.IsNullOrEmpty(connStr))
            {
                errMsg = Locale.IsRussian ?
                    "Соединение не определено" : "Connection is undefined";
                return false;
            }

            try
            {
                Application.DoEvents();
                ds.Init(connSettings);
                ds.Connect();
                ds.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                errMsg = string.Format(Locale.IsRussian ?
                    "Ошибка при соединении с БД: {0}" :
                    "Error connecting to DB: {0}", ex.Message);
                return false;
            }
        }

        #endregion Tab Settings

        #region CheckBox
        /// <summary>
        /// Write driver log.
        /// </summary>
        private void ckbWriteDriverLog_CheckedChanged(object sender, EventArgs e)
        {
            Modified = true;
        }
        #endregion CheckBox

        #region Button

        /// <summary>
        /// Saving settings.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

            // set the control values
            ProjectToControls();
        }

        /// <summary>
        /// Saving the settings by first getting the parameters from the controls, and then displaying.
        /// </summary>
        private void Save()
        {
            // retrieve the configuration
            ControlsToProject();

            // save the configuration
            if (project.Save(pathProject, out string errMsg))
            {
                Modified = false;
            }
            else
            {
                ScadaUiUtils.ShowError(errMsg);
            }
        }

        /// <summary>
        /// Close form
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion Button

        #endregion Control
    }
}
