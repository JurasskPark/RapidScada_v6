using Scada.Comm.Lang;
using Scada.Forms;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    /// <summary>
    /// Device configuration form.
    /// <para>Форма настройки конфигурации.</para>
    /// </summary>
    public partial class FrmConfig : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
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

            // load a configuration
            if (!project.Load(pathProject, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            DriverTagReturn.OnDebug = new DriverTagReturn.DebugData(DebugerTags);

            this.driverClient = new DriverClient(project);
            this.deviceTags = new List<DriverTag>();

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

            // load a configuration
            if (!project.Load(pathProject, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            DriverClient.OnDebugTags = new DriverClient.DebugTags(DebugerTags);

            this.driverClient = new DriverClient(project);
            this.deviceTags = new List<DriverTag>();

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
        public readonly bool isDll;                             // application or dll
        private readonly string pathLog;                        // the path log
        private readonly AppDirs appDirs;                       // the application directories
        private readonly string driverCode;                     // the driver code
        private readonly int deviceNum;                         // the device number
        private Project project;                                // the device configuration
        private string pathProject;                             // the configuration file name
        private DriverClient driverClient;                      // thie driver client
        private bool modified;                                  // the configuration was modified

        private string appDirectory;                    // the applications directory
        private string languageDir;                     // the language directory
        private string culture = "en-GB";               // the culture
        private bool isRussian;							// the language

        private Dictionary<string, ListViewItem> itemMap = new Dictionary<string, ListViewItem>();
        private List<DriverTag> deviceTags;                     // tags
        private ListViewItem selected;                          // selected record tag
        private int indexSelectTag = 0;                         // index number tag
        private Guid selectTagID = Guid.Empty;                  // id selected driver tag

        private int PingMode = 0;                               // type ping
        #endregion Variables

        #region Basic

        #region Form Load
        /// <summary>
        /// Loading the form
        /// </summary>
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            // translate
            Translate();

            // name form
            Text = string.Format(Text, deviceNum, DriverUtils.Version);

            // display the configuration
            ConfigToControls();

            Modified = false;


        }

        #endregion Form Load


        private void FrmConfig_Shown(object sender, EventArgs e)
        {
            driverClient.Start();
        }

        #region Form Close
        /// <summary>
        /// Closing the form
        /// </summary>
        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.driverClient.Stop();
                this.driverClient.Dispose();

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
            catch { }
        }
        #endregion Form Close

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

        #region Config
        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {
            // log
            cbLog.Checked = project.DebugerSettings.LogWrite;

            // mode
            switch (project.Mode)
            {
                case 0:
                    rdbPingSync.Checked = true;
                    break;
                case 1:
                    rdbPingAsync.Checked = true;
                    break;
            }

            // set the control values   
            SetListViewColumnNames();

            // tags
            deviceTags = project.DeviceTags;

            UpdateInformationTags();
        }

        private void UpdateInformationTags()
        {
            itemMap = new Dictionary<string, ListViewItem>();
            itemMap.Clear();

            lstTags.Items.Clear();

            if (deviceTags != null && deviceTags.Count > 0)
            {
                foreach (DriverTag tag in deviceTags)
                {
                    // filling in the dictionary
                    ListViewItem tagItem = new ListViewItem()
                    {
                        // tag name
                        Text = tag.Name,
                        SubItems =
                            {
                                // adding tag parameters
                                DriverUtils.NullToString(tag.Code),
                                DriverUtils.NullToString(tag.IpAddress),
                                DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tag.Enabled))
                            }
                    };
                    tagItem.Tag = tag.ID;

                    if (!itemMap.TryGetValue(tag.Name, out var TagItem))
                    {
                        itemMap.Add(tag.Name, tagItem);
                        this.lstTags.Items.Add(tagItem);
                    }

                    TagInformation(tag);
                }
            }

            try
            {
                if (indexSelectTag != 0 && indexSelectTag < lstTags.Items.Count)
                {
                    // scroll through
                    lstTags.EnsureVisible(indexSelectTag);
                    lstTags.TopItem = lstTags.Items[indexSelectTag];
                    // Making the area active
                    lstTags.Focus();
                    // making the desired element selected
                    lstTags.Items[indexSelectTag].Selected = true;
                    lstTags.Select();
                }
            }
            catch { }
        }

        /// <summary>
        /// Sets the configuration parameters according to the controls.
        /// </summary>
        private void ControlsToConfig()
        {
            this.driverClient = new DriverClient(project);
            this.driverClient.Stop();

            project.DebugerSettings.LogWrite = cbLog.Checked;
            project.Mode = SelectPingType();
            project.DeviceTags = deviceTags;
        }
        #endregion Config

        #region Language
        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            SetListViewColumnNames();


            CommPhrases.Init();

            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            // tranlaste the menu
            FormTranslator.Translate(cmnuLstTags, GetType().FullName);
            // translate the listview
            FormTranslator.Translate(lstTags, GetType().FullName);
        }


        /// <summary>
        /// Sets the column names as needed for translation.
        /// </summary>
        private void SetListViewColumnNames()
        {
            clmTagname.Name = nameof(clmTagname);
            clmTagCode.Name = nameof(clmTagCode);
            clmTagIPAddress.Name = nameof(clmTagIPAddress);
            clmTagEnabled.Name = nameof(clmTagEnabled);
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



        #endregion Language

        #region Control

        #region Save
        /// <summary>
        /// Saving settings
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Saving the settings by first getting the parameters from the controls, and then displaying
        /// </summary>
        private void Save()
        {
            // retrieve the configuration
            ControlsToConfig();

            // save the configuration
            if (project.Save(pathProject, out string errMsg))
            {
                Modified = false;
            }
            else
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            // set the control values
            ConfigToControls();
        }
        #endregion Save

        #region Close
        /// <summary>
        /// Close Form
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Close

        #region Mode
        /// <summary>
        /// Selecting the device polling mode
        /// </summary>
        private void rdbPingSync_CheckedChanged(object sender, EventArgs e)
        {
            SelectPingType();
        }

        /// <summary>
        /// Selecting the device polling mode
        /// </summary>
        private void rdbPingAsync_CheckedChanged(object sender, EventArgs e)
        {
            SelectPingType();
        }

        /// <summary>
        /// Selecting the device polling mode
        /// </summary>
        private int SelectPingType()
        {
            Modified = true;
            if (rdbPingSync.Checked == true)
            {
                return PingMode = 0;
            }
            else if (rdbPingAsync.Checked == true)
            {
                return PingMode = 1;
            }
            return PingMode = 0;
        }

        #endregion Mode

        #endregion Control

        #endregion Basic

        #region Tab Settings

        #region Tag selection
        /// <summary>
        /// Tag selection
        /// </summary>
        private void lstTags_MouseClick(object sender, MouseEventArgs e)
        {
            TagSelect();
        }

        /// <summary>
        /// Tag selection
        /// </summary>
        private void TagSelect()
        {
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (deviceTags != null)
            {
                indexSelectTag = tmplstTags.SelectedIndices[0];
                selected = tmplstTags.SelectedItems[0];                
                selectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());
            }
        }

        #endregion Tag selection

        #region Tag add
        /// <summary>
        /// Tag add
        /// </summary>
        private void cmnuTagAdd_Click(object sender, EventArgs e)
        {
            TagAdd();
        }

        /// <summary>
        /// Tag add
        /// </summary>
        private void TagAdd()
        {
            try
            {
                // create tag
                DriverTag newTag = new DriverTag();
                newTag.ID = Guid.NewGuid();
                newTag.Timeout = 1000;
                newTag.Enabled = true;
                // create form
                FrmTag frmTag = new FrmTag();
                frmTag.ModeWork = 1;
                frmTag.Tag = newTag;
                // showing the form
                DialogResult dialog = frmTag.ShowDialog();
                // if you have closed the form, click OK
                if (DialogResult.OK == dialog)
                {
                    deviceTags.Add(newTag);
                    UpdateInformationTags();

                    Modified = true;
                }
            }
            catch { }
        }

        #endregion Tag add

        #region Tag list add
        /// <summary>
        /// Tag list add
        /// </summary>
        private void cmnuListTagAdd_Click(object sender, EventArgs e)
        {
            ListTagAdd();
        }

        /// <summary>
        /// Tag list add
        /// </summary>
        private void ListTagAdd()
        {
            try
            {
                FrmInputBox InputBox = new FrmInputBox();
                InputBox.Values = string.Empty;
                InputBox.ShowDialog();

                if (InputBox.DialogResult == DialogResult.OK)
                {
                    String[] tagsName = InputBox.Values.Split(new String[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (String name in tagsName)
                    {
                        DriverTag newTag = new DriverTag();
                        newTag.ID = Guid.NewGuid();
                        newTag.Code = name;
                        newTag.Name = name;
                        newTag.IpAddress = name;
                        newTag.Timeout = 1000;
                        newTag.Enabled = true;

                        if (!deviceTags.Contains(newTag))
                        {
                            deviceTags.Add(newTag);
                            UpdateInformationTags();

                            Modified = true;
                        }
                    }
                }
            }
            catch { }
        }

        #endregion Tag list add

        #region Tag list add Ip addresses
        /// <summary>
        /// Tag list add Ip addresses
        /// </summary>
        private void cmnuFoundIpAddresses_Click(object sender, EventArgs e)
        {
            ListTagAddIpAddresses();
        }

        /// <summary>
        /// Tag list add Ip addresses
        /// </summary>
        private void ListTagAddIpAddresses()
        {
            try
            {
                FrmHostSearch frmHostSearch = new FrmHostSearch();
                frmHostSearch.ShowDialog();

                if (frmHostSearch.DialogResult == DialogResult.OK)
                {
                    List<DriverTag> foundTags = frmHostSearch.deviceTags;

                    foreach (DriverTag newTag in foundTags)
                    {
                        if (!deviceTags.Contains(newTag))
                        {
                            deviceTags.Add(newTag);
                        }
                    }

                    UpdateInformationTags();
                    Modified = true;
                }
            }
            catch { }
        }

        #endregion

        #region Tag change
        /// <summary>
        /// Tag change
        /// </summary>
        private void lstTags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TagChange();
        }

        /// <summary>
        /// Tag change
        /// </summary>
        private void cmnuTagChange_Click(object sender, EventArgs e)
        {
            TagChange();
        }

        /// <summary>
        /// Tag change
        /// </summary>
        private void TagChange()
        {
            try
            {
                System.Windows.Forms.ListView tmplstTags = this.lstTags;
                if (tmplstTags.SelectedItems.Count <= 0)
                {
                    return;
                }

                if (deviceTags != null)
                {
                    DriverTag changeTag = deviceTags.Find(s => s.ID == selectTagID);
                    int index = deviceTags.IndexOf(changeTag);

                    FrmTag frmTag = new FrmTag();
                    frmTag.ModeWork = 2;
                    frmTag.Tag = changeTag;
                    // showing the form
                    DialogResult dialog = frmTag.ShowDialog();
                    // if you have closed the form, click OK
                    if (DialogResult.OK == dialog)
                    {
                        if (index != -1)
                        {
                            deviceTags[index] = frmTag.Tag;
                        }

                        UpdateInformationTags();
                        Modified = true;

                        // scroll through
                        tmplstTags.EnsureVisible(indexSelectTag);
                        tmplstTags.TopItem = tmplstTags.Items[indexSelectTag];
                        // making the area active
                        tmplstTags.Focus();
                        // making the desired element selected
                        tmplstTags.Items[indexSelectTag].Selected = true;
                        tmplstTags.Select();
                    }
                }
            }
            catch { }
        }

        #endregion Tag change

        #region Tag delete

        /// <summary>
        /// Tag delete
        /// </summary>
        private void cmnuTagDelete_Click(object sender, EventArgs e)
        {
            TagDelete();
        }

        /// <summary>
        /// Tag delete
        /// </summary>
        private void TagDelete()
        {
            try
            {
                System.Windows.Forms.ListView tmplstTags = this.lstTags;
                if (tmplstTags.SelectedItems.Count <= 0)
                {
                    return;
                }

                if (deviceTags != null)
                {
                    DriverTag changeTag = deviceTags.Find(s => s.ID == selectTagID);
                    int index = deviceTags.IndexOf(changeTag);

                    if(changeTag == null && index == -1)
                    {
                        tmplstTags = this.lstTags;
                        if (tmplstTags.SelectedItems.Count <= 0)
                        {
                            return;
                        }

                        foreach (object item in tmplstTags.SelectedItems)
                        {
                            ListViewItem rowItem = item as ListViewItem;
                            if (!String.IsNullOrEmpty(rowItem.Tag.ToString()))
                            {
                                changeTag = deviceTags.Where(s => s.ID == DriverUtils.StringToGuid(rowItem.Tag.ToString())).FirstOrDefault();
                                index = deviceTags.IndexOf(changeTag);
                                if (index != -1)
                                {
                                    deviceTags.RemoveAt(index);
                                    UpdateInformationTags();
                                }
                            }
                        }               
                    }

                    try
                    {
                        if (index != -1)
                        {
                            deviceTags.RemoveAt(index);
                            UpdateInformationTags();
                        }

                        if (indexSelectTag >= 1)
                        {
                            // scroll through
                            tmplstTags.EnsureVisible(indexSelectTag - 1);
                            tmplstTags.TopItem = tmplstTags.Items[indexSelectTag - 1];
                            // making the area active
                            tmplstTags.Focus();
                            // making the desired element selected
                            tmplstTags.Items[indexSelectTag - 1].Selected = true;
                            tmplstTags.Select();
                        }
                    }
                    catch { }
                }

                Modified = true;
            }
            catch { }
        }

        #endregion Tag delete

        #region Tag list delete
        /// <summary>
        /// Tag list delete
        /// </summary>
        private void cmnuTagAllDelete_Click(object sender, EventArgs e)
        {
            TagAllDelete();
        }

        /// <summary>
        /// Tag list delete
        /// </summary>
        private void TagAllDelete()
        {
            try
            {
                deviceTags.Clear();
                UpdateInformationTags();
                Modified = true;
            }
            catch { }
        }

        #endregion Tag list delete

        #region Tag Up
        /// <summary>
        /// Tag Up
        /// </summary>
        private void cmnuUp_Click(object sender, EventArgs e)
        {
            // an item must be selected
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            if (lstTags.SelectedItems.Count > 0)
            {
                if (deviceTags != null)
                {
                    DriverTag changeTag = deviceTags.Find(s => s.ID == selectTagID);
                    int index = deviceTags.IndexOf(changeTag);

                    MoveUp(deviceTags, index);
                    UpdateInformationTags();
                }

                Modified = true;
            }
        }

        /// <summary>
        /// Move row up dependent on direction parameter
        /// </summary>
        public static void MoveUp(List<DriverTag> list, int index)
        {
            // Проверяем границы
            if (index > 0 && index < list.Count)
            {
                // Меняем местами текущий элемент и предыдущий
                DriverTag temp = list[index];
                list[index] = list[index - 1];
                list[index - 1] = temp;
            }
        }

        #endregion Tag Up

        #region Tag Down
        /// <summary>
        ///  Tag Down
        /// </summary>
        private void cmnuDown_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ListView tmplstTags = this.lstTags;
            if (tmplstTags.SelectedItems.Count <= 0)
            {
                return;
            }

            // an item must be selected
            if (lstTags.SelectedItems.Count > 0)
            {
                if (deviceTags != null)
                {
                    DriverTag changeTag = deviceTags.Find(s => s.ID == selectTagID);
                    int index = deviceTags.IndexOf(changeTag);

                    MoveDown(deviceTags, index);
                    UpdateInformationTags();
                }

                Modified = true;
            }
        }

        /// <summary>
        /// Move row down dependent on direction parameter
        /// </summary>
        public static void MoveDown(List<DriverTag> list, int index)
        {
            // Проверяем границы
            if (index >= 0 && index < list.Count - 1)
            {
                // Меняем местами текущий элемент и следующий
                DriverTag temp = list[index];
                list[index] = list[index + 1];
                list[index + 1] = temp;
            }
        }

        #endregion Tag Down

        #region Information
        /// <summary>
        /// Recording the tags result
        /// </summary>
        public void DebugerTags(List<DriverTag> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                return;
            }

            foreach (var tag in tags.ToList())
            {
                TagInformation(tag);
            }
        }

        /// <summary>
        /// Information tag
        /// </summary>
        public void TagInformation(DriverTag tag)
        {

            if (tag == null)
            {
                return;
            }

            // Собираем изменения
            var updates = new List<(string name, DriverTag tag)>();
            updates.Add((tag.Name, tag));

            // update without flicker
            Type type = lstTags.GetType();
            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo.SetValue(lstTags, true, null);

            lstTags.BeginInvoke((MethodInvoker)delegate
            {
                lstTags.BeginUpdate();
                try
                {
                    foreach (var (name, tagItem) in updates)
                    {
                        if (!itemMap.TryGetValue(name, out var TagItem))
                        {
                            continue;
                        }

                        // Обновление данных
                        TagItem.Text = tagItem.Name;
                        TagItem.SubItems[0].Text = DriverUtils.NullToString(tagItem.Name);
                        TagItem.SubItems[1].Text = DriverUtils.NullToString(tagItem.Code);
                        TagItem.SubItems[2].Text = DriverUtils.NullToString(tagItem.IpAddress);
                        TagItem.SubItems[3].Text = DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tagItem.Enabled));
                        // ... остальные обновления
                        if (tagItem.Enabled == false) // disables
                        {
                            TagItem.ForeColor = Color.White;
                            TagItem.BackColor = Color.Gray;
                        }
                        else if (tagItem.Enabled == true) // enabled
                        {
                            if (tagItem.Val == 1)
                            {
                                try
                                {
                                    TagItem.ForeColor = Color.Black;
                                    TagItem.BackColor = Color.FromArgb(0x79, 0xDA, 0x7C);
                                }
                                catch { }
                            }
                            else if (tagItem.Val == 0)
                            {
                                try
                                {
                                    TagItem.ForeColor = Color.White;
                                    TagItem.BackColor = Color.FromArgb(0xCD, 0x22, 0x30);
                                }
                                catch { }
                            }
                        }
                    }
                }
                catch { }
                finally
                {
                    lstTags.EndUpdate();
                }
            });
        }
        #endregion Information

        #endregion Tab Settings

    }
}
