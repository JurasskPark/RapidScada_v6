using ManagerAssistant;
using Scada.Comm.Config;
using Scada.Forms;
using Scada.Lang;
using System.Data;
using System.Reflection;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmConfig : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmConfig()
        {
            InitializeComponent();
            InitializeFtpEncryptionMode();

            this.isDll = false;

            this.deviceNum = 0;

            this.driverCode = DriverUtils.DriverCode;

            this.appDirectory = ApplicationPath.StartPath;
            this.pathProject = ApplicationPath.StartPath;
            this.languageDir = ApplicationPath.LangDir;
            this.pathLog = ApplicationPath.LogDir;

            this.project = new Project();
            this.projectFileName = Path.Combine(this.pathProject, DriverUtils.GetFileName(deviceNum));
            this.project.Load(this.projectFileName, out string errMsg);
            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg);
            }

            ConfigToControls();

            // manager
            Manager.Project = this.project;
            Manager.ProjectPath = this.pathProject;

            Manager.IsDll = this.isDll;
            Manager.LogPath = this.project.DebugerSettings.LogPath;
            Manager.LogDays = this.project.DebugerSettings.LogDays;
            Manager.LogWrite = this.project.DebugerSettings.LogWrite;

            Manager.DeviceNum = 0;

            // language 
            this.isRussian = project.LanguageIsRussian;

            this.modified = false;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmConfig(AppDirs appDirs, LineConfig lineConfig, DeviceConfig deviceConfig, int deviceNum)
            : this()
        {
            InitializeFtpEncryptionMode();

            this.isDll = true;

            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.lineConfig = lineConfig ?? throw new ArgumentNullException(nameof(lineConfig));
            this.deviceConfig = deviceConfig ?? throw new ArgumentNullException(nameof(deviceConfig));

            this.deviceNum = deviceNum;
            this.deviceName = deviceConfig.Name;

            this.driverCode = DriverUtils.DriverCode;

            this.appDirectory = appDirs.ConfigDir;
            this.pathProject = appDirs.ConfigDir;
            this.languageDir = appDirs.LangDir;
            this.pathLog = appDirs.LogDir;

            this.project = new Project();
            this.projectFileName = Path.Combine(appDirs.ConfigDir, DriverUtils.GetFileName(deviceNum));
            this.project.Load(this.projectFileName, out string errMsg);
            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg);
            }

            ConfigToControls();
            ConfigToControls(lineConfig);
            ConfigToControls(deviceConfig);

            // manager
            Manager.Project = this.project;
            Manager.ProjectPath = this.pathProject;

            Manager.IsDll = this.isDll;
            Manager.LogPath = this.project.DebugerSettings.LogPath;
            Manager.LogDays = this.project.DebugerSettings.LogDays;
            Manager.LogWrite = this.project.DebugerSettings.LogWrite;

            Manager.DeviceNum = this.deviceNum;

            // language
            this.isRussian = Locale.IsRussian;

            this.modified = false;
        }

        #region Variables
        public readonly bool isDll;                     // application or dll
        private readonly string pathLog;                // the path log
        private readonly AppDirs appDirs;               // the application directories
        private readonly LineConfig lineConfig;         // the communication line configuration
        private readonly DeviceConfig deviceConfig;     // the device configuration
        private readonly string driverCode;             // the driver code
        private readonly int deviceNum;                 // the device number
        private readonly string deviceName;             // the device name

        public Project project;                         // the project configuration
        public string pathProject;                      // path project
        private string projectFileName;                 // the configuration file name

        private DriverClient driverClient;              // driver client

        private bool modified;                          // the configuration was modified
        private bool connChanging;                      // connection settings are changing
        private bool cmdSelecting;                      // a command is selecting

        private string appDirectory;                    // the applications directory
        private string languageDir;                     // the language directory
        private string culture = "en-GB";               // the culture
        private bool isRussian;							// the language

        private int indexSelectRow = 0;                 // index select row
        private ListViewItem selected;                  // selected row

        public Guid idRow;                              // id row
        public string nameRow;                          // name row

        public FrmConfig formParent;                    // parent form
        private Form frmPropertyForm = new Form();      // child form properties
        private List<Scenario> ListScenarios = new List<Scenario>(); // scenarios
        #endregion Variables

        #region Initialize

        private void InitializeFtpEncryptionMode()
        {
            //// Получение отсортированного списка строк
            //List<string> sortedStrings = Enum.GetValues(typeof(FtpEncryptionMode))
            //    .Cast<FtpEncryptionMode>()
            //    .OrderBy(x => (int)(object)x)
            //    .Select(x => x.ToString())
            //    .ToList();

            //cmbEncryptionMode.Items.AddRange(sortedStrings.ToArray());
            //cmbEncryptionMode.SelectedIndex = 0;
        }

        #endregion Initialize

        #region Form Load
        /// <summary>
        /// Loading the form
        /// </summary>
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            RefreshData();

            LoadLanguage(languageDir, isRussian);
            Translate();

            Text = string.Format(Text, deviceNum, DriverUtils.Version);
        }
        #endregion Form Load

        #region Config 

        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        public void ConfigToControls(LineConfig lineConfig)
        {
            ArgumentNullException.ThrowIfNull(lineConfig, nameof(lineConfig));
            Modified = true;

            if (lineConfig.Channel.Driver == "DrvCnlBasic" && lineConfig.Channel.TypeCode == "SerialPort")
            {

            }
            else if (lineConfig.Channel.Driver == "DrvCnlBasic" && lineConfig.Channel.TypeCode == "TcpClient")
            {

            }
            else if (lineConfig.Channel.Driver == "DrvCnlBasic" && lineConfig.Channel.TypeCode == "TcpServer")
            {

            }

            Modified = false;
        }

        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        public void ConfigToControls(DeviceConfig deviceConfig)
        {
            ArgumentNullException.ThrowIfNull(deviceConfig, nameof(deviceConfig));
            Modified = true;

            Modified = false;
        }

        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {
            Modified = true;

            ListScenarios = project.Scenarios;



            Modified = false;
        }

        /// <summary>
        /// Sets the configuration parameters according to the controls.
        /// </summary>
        private void ControlsToConfig()
        {
            project.Scenarios = ListScenarios;


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
                if (ListScenarios != null && ListScenarios.Count > 0)
                {
                    // update without flicker
                    Type type = lstScenarios.GetType();
                    PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                    propertyInfo.SetValue(lstScenarios, true, null);

                    this.lstScenarios.BeginUpdate();
                    this.lstScenarios.Items.Clear();

                    #region Data display

                    foreach (Scenario scenario in ListScenarios)
                    {
                        // inserted information
                        this.lstScenarios.Items.Add(new ListViewItem()
                        {
                            // tag name
                            Text = scenario.Name,
                            SubItems =
                                {
                                    // adding tag parameters
                                    ListViewAsDisplayStringBoolean(scenario.Enabled),
                                }
                        }).Tag = scenario.ID; // in tag we pass the tag id... so that we can find
                    }
                    #endregion Data display

                    this.lstScenarios.EndUpdate();
                }
                else
                {
                    this.lstScenarios.Items.Clear();
                }
            }
            catch { }

            try
            {
                if (indexSelectRow != 0 && indexSelectRow < lstScenarios.Items.Count)
                {
                    // scroll through
                    lstScenarios.EnsureVisible(indexSelectRow);
                    lstScenarios.TopItem = lstScenarios.Items[indexSelectRow];
                    // Making the area active
                    lstScenarios.Focus();
                    // making the desired element selected
                    lstScenarios.Items[indexSelectRow].Selected = true;
                    lstScenarios.Select();
                }
            }
            catch { }
        }

        #endregion Config

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
                //btnSave.Enabled = modified;
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

        #region Lang
        /// <summary>
        /// Loading from the translation catalog by language.
        /// <para>Загрузка из каталога перевода по признаку языка</para>
        /// </summary>
        public void LoadLanguage(string languageDir, bool IsRussian = false)
        {
            this.languageDir = languageDir;
            // load translate
            this.isRussian = IsRussian;

            culture = "en-GB";
            string EnglishCultureName = "en-GB";
            string RussianCultureName = "ru-RU";
            if (!IsRussian)
            {
                culture = EnglishCultureName;
            }
            else
            {
                culture = RussianCultureName;
            }

            string languageFile = Path.Combine(languageDir, DriverUtils.DriverCode + "." + culture + ".xml");

            if (!File.Exists(languageFile))
            {
                MessageBox.Show(languageFile, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Locale.LoadDictionaries(languageFile, out string errMsg);
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmConfig");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmSettings");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmScenario");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmFilesManager");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmFTPSettings");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Forms.FrmName");

            Locale.GetDictionary("Scada.Comm.Drivers.DrvTextParserJP.View.Application");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Forms.Combobox");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFtpJP.View.Forms.DialogBox");
            DriverPhrases.Init();

            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            FormTranslator.Translate(lstScenarios, GetType().FullName);
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

        #endregion Lang

        #region Toolbar

        private void tolProperties_Click(object sender, EventArgs e)
        {
            FrmFTPSettings frmFTPSettings = new FrmFTPSettings(project.FtpClientSettings);
            DialogResult dialog = frmFTPSettings.ShowDialog();
            // if you have closed the form, click Save
            if (DialogResult.OK == dialog)
            {
                project.FtpClientSettings = frmFTPSettings.client;
            }
        }

        private void tolConnect_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void Connect()
        {
            FrmFilesManager frmFilesManager = new FrmFilesManager(project.FtpClientSettings);
            frmFilesManager.formParent = formParent;

            frmPropertyForm = new Form();
            frmPropertyForm = frmFilesManager;

            TabPage tabPageNew = new TabPage(project.FtpClientSettings.Name);
            tabPageNew.Name = project.FtpClientSettings.Name;
            tabPageNew.Text = project.FtpClientSettings.Name;
            tabPageNew.Controls.Add(frmPropertyForm);

            tabControl.TabPages.Add(tabPageNew);
            tabControl.SelectedTab = tabPageNew;

            frmPropertyForm.Dock = DockStyle.Fill;
            frmPropertyForm.Show();
        }

        private void tolDisconnect_Click(object sender, EventArgs e)
        {
            Disconnect();
        }

        private void Disconnect()
        {
            if (frmPropertyForm != null)
            {
                frmPropertyForm.Dispose();
            }
        }


        private void tolSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        public void SaveData()
        {
            ControlsToConfig();

            project.Save(projectFileName, out string errMsg);
            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg);
            }

            Modified = false;
        }

        private void tolClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }

        #endregion Toolbar

        #region Button



        #region Close
        private void btnClose_Click(object sender, EventArgs e)
        {

        }
        #endregion Close

        #region Connection Test
        private void btnConnectTest_Click(object sender, EventArgs e)
        {

        }
        #endregion Connection Test

        #endregion Button

        #region Settings
        private void tolSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.formParent = this;
            frmSettings.project = project;
            frmSettings.ShowDialog();
        }
        #endregion Settings

        #region About

        private void tolAbout_Click(object sender, EventArgs e)
        {
            try
            {
                FrmAbout frmAbout = new FrmAbout();
                frmAbout.Text = DriverPhrases.TitleAbout;
                frmAbout.AppTitle = DriverUtils.DriverCode;
                frmAbout.AppDescription = DriverUtils.Description(isRussian);
                frmAbout.AppVersion = DriverUtils.Version;
                frmAbout.AppCopyright = GetAutoCopyright();
                frmAbout.AppBuildDate = GetBuildDateTime();
                frmAbout.AppInfoMore = DriverUtils.Description(isRussian);

                string[] linkInfo = new string[6];
                linkInfo[0] = "mailto:jurasskpark@yandex.ru";
                linkInfo[1] = "https://github.com/JurasskPark";
                linkInfo[2] = "https://www.youtube.com/@JurasskParkChannel";
                linkInfo[3] = "https://jurasskpark.ru";
                frmAbout.AppLinkInfo = linkInfo;
                frmAbout.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
        }

        public static string GetAutoCopyright()
        {
            try
            {
                object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
            }
            catch { return string.Empty; }
        }

        public static string GetAutoDescription()
        {
            try
            {
                object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return ((AssemblyDescriptionAttribute)customAttributes[0]).Description.ToString();
            }
            catch { return string.Empty; }
        }

        public static string GetAutoTitle()
        {
            try
            {
                object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes.Length > 0)
                {
                    AssemblyTitleAttribute attribute = (AssemblyTitleAttribute)customAttributes[0];
                    if (attribute.Title != "")
                    {
                        return attribute.Title;
                    }
                }
                return Path.GetFileNameWithoutExtension(path: Assembly.GetEntryAssembly().CodeBase);
            }
            catch (Exception) { return string.Empty; }
        }

        public static string GetAutoVersion()
        {
            try
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
            catch { return string.Empty; }
        }

        public static string GetAutoCompany()
        {
            try
            {
                return ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company;
            }
            catch { return string.Empty; }
        }

        public static string GetBuildDateTime()
        {
            try
            {
                DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                return buildDate.ToString();
            }
            catch { return string.Empty; }
        }

        #endregion About

        #region ListView

        #region Select
        /// <summary>
        /// Mouse click on ListView
        /// </summary>
        private void lstScenarios_MouseClick(object sender, MouseEventArgs e)
        {
            ListSelect();
        }

        /// <summary>
        /// Mouse double click on ListView
        /// </summary>
        private void lstScenarios_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListSelect();
            ListChange();
        }

        /// <summary>
        /// List select
        /// </summary>
        private void ListSelect()
        {
            System.Windows.Forms.ListView tmplstScenarios = this.lstScenarios;
            if (tmplstScenarios.SelectedItems.Count <= 0)
            {
                return;
            }

            try
            {
                selected = tmplstScenarios.SelectedItems[0];
                indexSelectRow = tmplstScenarios.SelectedIndices[0];
                idRow = DriverUtils.StringToGuid(tmplstScenarios.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstScenarios.SelectedItems[0].SubItems[1].Text.Trim());
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
            ListAdd();
        }

        /// <summary>
        /// List add
        /// </summary>
        private void ListAdd()
        {
            try
            {
                Scenario scenario = new Scenario();
                scenario.ID = Guid.NewGuid();
                scenario.Enabled = true;
                scenario.Name = DriverDictonary.ScenarioName;

                // create form
                FrmScenario frmScenario = new FrmScenario();
                frmScenario.formParent = this;
                frmScenario.scenario = scenario;
                // showing the form
                DialogResult dialog = frmScenario.ShowDialog();
                // if you have closed the form, click Save
                if (DialogResult.OK == dialog)
                {
                    ListScenarios.Add(frmScenario.scenario);
                    project.Scenarios = ListScenarios;
                    RefreshData();
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
                System.Windows.Forms.ListView tmplstScenarios = this.lstScenarios;
                if (tmplstScenarios.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstScenarios.SelectedItems[0].Tag.ToString());

                Scenario scenario = ListScenarios.Find(s => s.ID == idRow);
                int index = ListScenarios.IndexOf(scenario);

                // create form
                FrmScenario frmScenario = new FrmScenario();
                frmScenario.formParent = this;
                frmScenario.scenario = scenario;
                // showing the form
                DialogResult dialog = frmScenario.ShowDialog();
                // if you have closed the form, click Save
                if (DialogResult.OK == dialog)
                {
                    if (index != -1)
                    {
                        ListScenarios[index] = frmScenario.scenario;
                        SaveData();
                        RefreshData();
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
                System.Windows.Forms.ListView tmplstScenarios = this.lstScenarios;
                if (tmplstScenarios.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstScenarios.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstScenarios.SelectedItems[0].SubItems[1].Text.Trim());

                // create dialog
                DialogResult dialog = MessageBox.Show(Locale.IsRussian ?
                    "Вы действительно хотите удалить запись?" :
                    "Are you sure you want to delete this entry?",
                    "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialog == DialogResult.OK)
                {
                    Scenario scenario = ListScenarios.Find(s => s.ID == idRow);
                    int index = ListScenarios.IndexOf(scenario);
                    if (index != -1)
                    {
                        ListScenarios.RemoveAt(index);
                        SaveData();
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
            System.Windows.Forms.ListView tmplstScenarios = this.lstScenarios;
            if (tmplstScenarios.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstScenarios.SelectedItems.Count > 0)
            {
                if (ListScenarios != null)
                {
                    ListViewItem selectedIndex = tmplstScenarios.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstScenarios.SelectedItems[0].Tag.ToString());

                    Scenario scenario = ListScenarios.Find(s => s.ID == idRow);
                    int index = ListScenarios.IndexOf(scenario);

                    ListViewExtensions.MoveListViewItems(lstScenarios, MoveDirection.Up);

                    if (index == 0)
                    {
                        return;
                    }
                    else
                    {
                        ListScenarios.Reverse(index - 1, 2);
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
            System.Windows.Forms.ListView tmplstScenarios = this.lstScenarios;
            if (tmplstScenarios.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstScenarios.SelectedItems.Count > 0)
            {
                if (ListScenarios != null)
                {
                    ListViewItem selectedIndex = tmplstScenarios.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstScenarios.SelectedItems[0].Tag.ToString());

                    Scenario scenario = ListScenarios.Find(s => s.ID == idRow);
                    int index = ListScenarios.IndexOf(scenario);

                    ListViewExtensions.MoveListViewItems(lstScenarios, MoveDirection.Down);

                    if (index == ListScenarios.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        ListScenarios.Reverse(index, 2);
                    }

                    SaveData();
                }
            }
        }
        #endregion Parser Down

        #endregion ListView

    }
}
