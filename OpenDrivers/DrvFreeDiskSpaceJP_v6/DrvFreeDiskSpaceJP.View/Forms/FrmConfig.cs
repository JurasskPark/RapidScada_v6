﻿using Scada.Forms;
using Scada.Lang;
using System.Reflection;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    /// <summary>
    /// A form with a list of tasks.
    /// <para>Форма со списком задач.</para>
    /// </summary>
    public partial class FrmConfig : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmConfig()
        {
            InitializeComponent();

            this.isDll = false;

            this.driverCode = DriverUtils.DriverCode;

            this.appDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.languageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Lang");
            this.pathLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Log");
            this.project = new Project();
            this.pathProject = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DriverUtils.GetFileName(deviceNum));

            LoadData();

            // manager
            Manager.IsDll = this.isDll;
            Manager.DeviceNum = this.deviceNum;
            Manager.PathProject = this.pathProject;
            Manager.Project = this.project;
            Manager.PathLog = this.pathLog;


            this.modified = false;
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmConfig(AppDirs appDirs, int deviceNum)
            : this()
        {
            this.isDll = true;

            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.deviceNum = deviceNum;
            this.driverCode = DriverUtils.DriverCode;

            this.appDirectory = appDirs.ConfigDir;
            this.languageDir = appDirs.LangDir;
            this.project = new Project();
            this.pathProject = Path.Combine(appDirs.ConfigDir, DriverUtils.GetFileName(deviceNum));

            LoadData();

            // manager
            Manager.IsDll = this.isDll;
            Manager.DeviceNum = this.deviceNum;
            Manager.PathProject = this.pathProject;
            Manager.Project = this.project;
            Manager.PathLog = this.pathLog;
           
            this.isRussian = Locale.IsRussian;

            this.modified = false;
        }

        #region Variables
        public readonly bool isDll;                     // application or dll

        private readonly string pathLog;                // the path log

        private readonly AppDirs appDirs;               // the application directories
        private readonly string driverCode;             // the driver code
        private readonly int deviceNum;                 // the device number

        public Project project;                         // the project configuration
        public string pathProject;                      // path project
        private List<Task> ListTask = new List<Task>(); // list task

        private bool modified;                          // the configuration was modified
       
        private bool cmdSelecting;                      // a command is selecting

        private string appDirectory;                    // the applications directory
        private string languageDir;                     // the language directory
        private string culture = "en-GB";               // the culture
        private bool isRussian;							// the language

        private int indexSelectRow = 0;                 // index select row
        private ListViewItem selected;                  // selected row

        public Guid idRow;                              // id row
        public string nameRow;                          // name row
        #endregion Variables

        #region Form Load
        /// <summary>
        /// Load the form.
        /// <para>Загрузка формы.</para>
        /// </summary>
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            LoadLanguage(languageDir, isRussian);
            Translate();

            Text = string.Format(Text, deviceNum, DriverUtils.Version);
        }
        #endregion Form Load

        #region Load Data
        /// <summary>
        /// Load data.
        /// <para>Загрузка данных.</para>
        /// </summary>
        public void LoadData()
        {
            try
            {
                project.Load(this.pathProject, out string errMsg);
                if (errMsg != string.Empty)
                {
                    MessageBox.Show(errMsg);
                }

                // language 
                this.isRussian = project.LanguageIsRussian;

                // list task
                this.ListTask = project.ListTask;

                // select data
                if (ListTask != null && ListTask.Count > 0)
                {
                    // update without flicker
                    Type type = lstParsers.GetType();
                    PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                    propertyInfo.SetValue(lstParsers, true, null);

                    this.lstParsers.BeginUpdate();
                    this.lstParsers.Items.Clear();

                    #region Data display

                    foreach (Task task in ListTask)
                    {
                        // inserted information
                        this.lstParsers.Items.Add(new ListViewItem()
                        {
                            Text = task.Name,
                            SubItems =
                                {
                                    task.Description,
                                    task.DiskName,
                                    task.Path,
                                    ListViewAsDisplayStringBoolean(task.Enabled),
                                }
                        }).Tag = task.ID;
                    }
                    #endregion Data display

                    this.lstParsers.EndUpdate();
                }
                else
                {
                    this.lstParsers.Items.Clear();
                }
            }
            catch { }

            try
            {
                if (indexSelectRow != 0 && indexSelectRow < lstParsers.Items.Count)
                {
                    // scroll through
                    lstParsers.EnsureVisible(indexSelectRow);
                    lstParsers.TopItem = lstParsers.Items[indexSelectRow];
                    // Making the area active
                    lstParsers.Focus();
                    // making the desired element selected
                    lstParsers.Items[indexSelectRow].Selected = true;
                    lstParsers.Select();
                }
            }
            catch { }
        }

        #endregion Load Data

        #region Lang
        /// <summary>
        /// Loading from the translation catalog by language.
        /// <para>Загрузка из каталога перевода по признаку языка.</para>
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
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFreeDiskSpaceJP.Application");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms.FrmConfig");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms.FrmTask");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms.Combobox");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms.DialogBox");
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
            FormTranslator.Translate(lstParsers, GetType().FullName);
        }

        /// <summary>
        /// Sets the column names as needed for translation.
        /// <para>Задает имена столбцов, необходимые для перевода.</para>
        /// </summary>
        private void SetListViewColumnNames()
        {
            clmName.Name = nameof(clmName);
            clmDescription.Name = nameof(clmDescription);
            clmPath.Name = nameof(clmPath);
            clmDiskName.Name = nameof(clmDiskName);
            clmEnabled.Name = nameof(clmEnabled);
        }

        /// <summary>
        /// Translating values to listview.
        /// <para>Перевод значений в список.</para>
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

        #region ListView

        #region Select
        /// <summary>
        /// Mouse click on ListView.
        /// <para>Щелчок мыши по списку.</para>
        /// </summary>
        private void lstDevice_MouseClick(object sender, MouseEventArgs e)
        {
            ListSelect();
        }

        /// <summary>
        /// List select.
        /// Выбор из списка.
        /// </summary>
        private void ListSelect()
        {
            System.Windows.Forms.ListView tmplstParsers = this.lstParsers;
            if (tmplstParsers.SelectedItems.Count <= 0)
            {
                return;
            }

            try
            {
                selected = tmplstParsers.SelectedItems[0];
                indexSelectRow = tmplstParsers.SelectedIndices[0];
                idRow = DriverUtils.StringToGuid(tmplstParsers.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstParsers.SelectedItems[0].SubItems[1].Text.Trim());
            }
            catch { }

        }
        #endregion Select

        #region Add

        /// <summary>
        /// Select from the menu add a row.
        /// <para>Выберите в меню пункт добавить запись.</para>
        /// </summary>
        private void cmnuListAdd_Click(object sender, EventArgs e)
        {
            ListAdd();
        }

        /// <summary>
        /// List add.
        /// <para>Добавление в список.</para>
        /// </summary>
        private void ListAdd()
        {
            try
            {
                Task task = new Task();
                task.ID = Guid.NewGuid();

                // create form
                FrmTask frmTask= new FrmTask();
                frmTask.formParent = this;
                frmTask.task = task;
                // showing the form
                DialogResult dialog = frmTask.ShowDialog();
                // if you have closed the form, click Save
                if (DialogResult.OK == dialog)
                {
                    ListTask.Add(frmTask.task);
                    project.ListTask = ListTask;

                    SaveData();
                    LoadData();
                }
            }
            catch { }
        }

        #endregion Add

        #region Change
        /// <summary>
        /// Select from the menu change a row.
        /// <para>>Выберите в меню пункт изменить запись.</para>
        /// </summary>
        private void cmnuListChange_Click(object sender, EventArgs e)
        {
            ListChange();
        }

        /// <summary>
        /// List change.
        /// <para>Изменение списка.</para>
        /// </summary>
        private void ListChange()
        {
            try
            {
                System.Windows.Forms.ListView tmplstParsers = this.lstParsers;
                if (tmplstParsers.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstParsers.SelectedItems[0].Tag.ToString());

                Task task = ListTask.Find(s => s.ID == idRow);
                int index = ListTask.IndexOf(task);

                // create form
                FrmTask frmTask = new FrmTask();
                frmTask.formParent = this;
                frmTask.task = task;
                // showing the form
                DialogResult dialog = frmTask.ShowDialog();
                // if you have closed the form, click Save
                if (DialogResult.OK == dialog)
                {
                    if (index != -1)
                    {
                        ListTask[index] = frmTask.task;
                        SaveData();
                        LoadData();
                    }
                }
            }
            catch { }
        }

        #endregion Change

        #region Delete
        /// <summary>
        /// Select from the menu delete a row.
        /// <para>>Выберите в меню пункт удалить запись.</para>
        /// </summary>
        private void cmnuListDelete_Click(object sender, EventArgs e)
        {
            ListDelete();
        }

        /// <summary>
        /// Delete from the list.
        /// <para>Удаление из списка.</para>
        /// </summary>
        private void ListDelete()
        {
            try
            {
                System.Windows.Forms.ListView tmplstParsers = this.lstParsers;
                if (tmplstParsers.SelectedItems.Count <= 0)
                {
                    return;
                }

                idRow = DriverUtils.StringToGuid(tmplstParsers.SelectedItems[0].Tag.ToString());
                nameRow = Convert.ToString(tmplstParsers.SelectedItems[0].SubItems[1].Text.Trim());

                // create dialog
                DialogResult dialog = MessageBox.Show(Locale.IsRussian ?
                    "Вы действительно хотите удалить запись?" :
                    "Are you sure you want to delete this entry?",
                    "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (dialog == DialogResult.OK)
                {
                    Task task = ListTask.Find(s => s.ID == idRow);
                    int index = ListTask.IndexOf(task);
                    if (index != -1)
                    {
                        ListTask.RemoveAt(index);
                        SaveData();
                        LoadData();
                    }
                }
            }
            catch { }
        }
        #endregion Delete

        #region Task Up
        /// <summary>
        /// Raises the entry higher in the list.
        /// <para>Поднимает запись выше в списке.</para>
        /// </summary>
        private void cmnuListUp_Click(object sender, EventArgs e)
        {
            TaskUp();
        }

        /// <summary>
        /// Raises the task higher.
        /// <para>Поднимает задачу выше.</para>
        /// </summary>
        private void TaskUp()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstParsers = this.lstParsers;
            if (tmplstParsers.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstParsers.SelectedItems.Count > 0)
            {
                if (ListTask != null)
                {
                    ListViewItem selectedIndex = tmplstParsers.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstParsers.SelectedItems[0].Tag.ToString());

                    Task task = ListTask.Find(s => s.ID == idRow);
                    int index = ListTask.IndexOf(task);

                    ListViewExtensions.MoveListViewItems(lstParsers, MoveDirection.Up);

                    if (index == 0)
                    {
                        return;
                    }
                    else
                    {
                        ListTask.Reverse(index - 1, 2);
                    }

                    SaveData();
                }
            }
        }
        #endregion Task Up

        #region Task Down
        /// <summary>
        /// Omits the entry below in the list.
        /// <para>Опускает запись ниже в списке.</para>
        /// </summary>
        private void cmnuListDown_Click(object sender, EventArgs e)
        {
            TaskDown();
        }

        /// <summary>
        /// Drops the task below.
        /// <para>Опускает задачу ниже.</para>
        /// </summary>
        private void TaskDown()
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstParsers = this.lstParsers;
            if (tmplstParsers.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstParsers.SelectedItems.Count > 0)
            {
                if (ListTask != null)
                {
                    ListViewItem selectedIndex = tmplstParsers.SelectedItems[0];
                    idRow = DriverUtils.StringToGuid(tmplstParsers.SelectedItems[0].Tag.ToString());

                    Task task = ListTask.Find(s => s.ID == idRow);
                    int index = ListTask.IndexOf(task);

                    ListViewExtensions.MoveListViewItems(lstParsers, MoveDirection.Down);

                    if (index == ListTask.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        ListTask.Reverse(index, 2);
                    }

                    SaveData();
                }
            }
        }
        #endregion Task Down

        #endregion ListView

        #region Save
        /// <summary>
        /// Saving the settings.
        /// <para>Сохранение настроек.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

            SaveData();
        }

        /// <summary>
        /// Saving the settings.
        /// <para>Сохранение настроек.</para>
        /// </summary>
        public void SaveData()
        {
            project.Save(pathProject, out string errMsg);
            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg);
            }
        }
        #endregion Save

        #region Close
        /// <summary>
        /// Closing the form without saving the settings.
        /// <para>Закрытие формы без сохранения настроек.</para>
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;

            Close();
        }
        #endregion Close

        #region Settings
        /// <summary>
        /// Opening a form with settings.
        /// <para>Открытие формы с настройками.</para>
        /// </summary>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            FrmSettings frmSettings = new FrmSettings();
            frmSettings.formParent = this;
            frmSettings.project = project;
            frmSettings.ShowDialog();
        }
        #endregion Settings

        #region About
        /// <summary>
        /// About.
        /// <para>О программе.</para>
        /// </summary>
        private void btnAbout_Click(object sender, EventArgs e)
        {
            try
            {
                About.FrmAbout frmAbout = new About.FrmAbout();
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
            catch (Exception ex) { UtilsJP.Utils.InfoError(ex, true); }
        }

        /// <summary>
        /// Get copyright.
        /// <para>Получить авторское право</para>
        /// </summary>
        public static string GetAutoCopyright()
        {
            try
            {
                object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Get description.
        /// <para>Получить описание.</para>
        /// </summary>
        public static string GetAutoDescription()
        {
            try
            {
                object[] customAttributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return ((AssemblyDescriptionAttribute)customAttributes[0]).Description.ToString();
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Get title.
        /// <para>Получить заголовок.</para>
        /// </summary>
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

        /// <summary>
        /// Get the version.
        /// <para>Получить версию.</para>
        /// </summary>
        public static string GetAutoVersion()
        {
            try
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Get the company.
        /// <para>Получить компанию.</para>
        /// </summary>
        public static string GetAutoCompany()
        {
            try
            {
                return ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), false)).Company;
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// Get the compilation time.
        /// <para>Получить время компиляции.</para>
        /// </summary>
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

    }
}
