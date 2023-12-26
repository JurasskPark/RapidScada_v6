using Scada.Comm.Lang;
using Scada.Forms;
using Scada.Lang;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    /// <summary>
    /// Device configuration form.
    /// <para>Форма настройки конфигурации КП.</para>
    /// </summary>
    public partial class FrmConfig : Form
    {

        #region Variables
        private readonly AppDirs appDirs;                       // the application directories
        private readonly string driverCode;                     // the driver code
        private readonly int deviceNum;                         // the device number
        private readonly NetworkInformation networkInformation; // network (ping)
        private readonly DrvPingJPConfig config;                // the device configuration
        private string configFileName;                          // the configuration file name
        private bool modified;                                  // the configuration was modified

        private List<Tag> deviceTags;                           // tags
        private ListViewItem selected;                          // selected record tag
        private int indexSelectTag = 0;                         // index number tag

        DateTime tmrEndTime = new DateTime();                   // timer
        private bool tmrStatus = true;
        private double TimeRefresh = 1000d;

        private int PingMode = 0;                               // type ping
        Stopwatch stopWatch = new Stopwatch();
        TimeSpan ts;
        #endregion Variables

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        private FrmConfig()
        {
            InitializeComponent();
        }

        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmConfig(AppDirs appDirs, int deviceNum)
            : this()
        {
            this.appDirs = appDirs ?? throw new ArgumentNullException(nameof(appDirs));
            this.deviceNum = deviceNum;
            this.driverCode = DriverUtils.DriverCode;

            this.networkInformation = new NetworkInformation();
            this.networkInformation.OnDebug = new NetworkInformation.DebugData(DebugerLog);
            this.networkInformation.OnDebugTag = new NetworkInformation.DebugTag(DebugerTag);
            this.networkInformation.OnDebugTags = new NetworkInformation.DebugTags(DebugerTags);

            config = new DrvPingJPConfig();
            configFileName = Path.Combine(appDirs.ConfigDir, DrvPingJPConfig.GetFileName(deviceNum));
            modified = false;
            deviceTags = new List<Tag>();
        }

        public FrmConfig(string configFileName)
            : this()
        {
            this.configFileName = configFileName;
            this.driverCode = DriverUtils.DriverCode;

            this.networkInformation = new NetworkInformation();
            this.networkInformation.OnDebug = new NetworkInformation.DebugData(DebugerLog);
            this.networkInformation.OnDebugTag = new NetworkInformation.DebugTag(DebugerTag);
            this.networkInformation.OnDebugTags = new NetworkInformation.DebugTags(DebugerTags);

            config = new DrvPingJPConfig();
            modified = false;
            deviceTags = new List<Tag>();
        }

        /// <summary>
        /// Loading the form
        /// </summary>
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
            FormTranslator.Translate(cmnuLstTags, GetType().FullName);

            Text = string.Format(Text, deviceNum);

            // load a configuration
            if (File.Exists(configFileName) && !config.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            // display the configuration
            ConfigToControls();

            // translate the listview
            FormTranslator.Translate(lstTags, GetType().FullName);

            Modified = false;
        }

        /// <summary>
        /// Shown the form
        /// </summary>
        private void FrmConfig_Shown(object sender, EventArgs e)
        {
            tmrTimer.Enabled = true;
            tmrTimer.Start();
        }

        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                try
                {
                    networkInformation.StopPingSynchronous();
                }
                catch { }

                if (Modified)
                {
                    DialogResult result = MessageBox.Show(CommPhrases.SaveDeviceConfigConfirm,
                        CommonPhrases.QuestionCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Yes:
                            if (!config.Save(configFileName, out string errMsg))
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

        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {
            // log
            cbLog.Checked = config.Log;

            // mode
            switch (config.Mode)
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
            deviceTags = config.DeviceTags;

            if (deviceTags != null)
            {
                // update without flicker
                Type type = lstTags.GetType();
                PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(lstTags, true, null);

                this.lstTags.BeginUpdate();
                this.lstTags.Items.Clear();

                #region Data display
                foreach (var tmpTag in deviceTags)
                {
                    // inserted information
                    this.lstTags.Items.Add(new ListViewItem()
                    {
                        // tag name
                        Text = tmpTag.TagName,
                        SubItems =
                            {
                                // adding tag parameters
                                DriverUtils.NullToString(tmpTag.TagCode),
                                DriverUtils.NullToString(tmpTag.TagIPAddress),
                                DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tmpTag.TagEnabled))
                            }
                    }).Tag = tmpTag; // in tag we pass the tag id... so that we can find
                }
                #endregion Data display

                this.lstTags.EndUpdate();
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


        /// <summary>
        /// Sets the configuration parameters according to the controls.
        /// </summary>
        private void ControlsToConfig()
        {
            config.Log = cbLog.Checked;
            config.Mode = SelectPingType();

            deviceTags.Clear();
            foreach (ListViewItem itemRow in this.lstTags.Items)
            {
                deviceTags.Add((Tag)itemRow.Tag);
            }
        
            config.DeviceTags = deviceTags;
        }

        /// <summary>
        /// Saving settings
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Close Form
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saving the settings by first getting the parameters from the controls, and then displaying
        /// </summary>
        private void Save()
        {
            tmrTimer.Stop();
            // retrieve the configuration
            ControlsToConfig();

            // save the configuration
            if (config.Save(configFileName, out string errMsg))
            {
                Modified = false;
            }
            else
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            // set the control values
            ConfigToControls();
            tmrTimer.Start();
        }

        #region Mode
        private void rdbPingSync_CheckedChanged(object sender, EventArgs e)
        {
            SelectPingType();
        }

        private void rdbPingAsync_CheckedChanged(object sender, EventArgs e)
        {
            SelectPingType();
        }

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
                selected = tmplstTags.SelectedItems[0];
                indexSelectTag = tmplstTags.SelectedIndices[0];
                Tag tmpTag = (Tag)selected.Tag;
                Guid SelectTagID = DriverUtils.StringToGuid(tmpTag.TagID.ToString());
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
                Tag newTag = new Tag();
                newTag.TagID = Guid.NewGuid();
                newTag.TagTimeout = 1000;
                newTag.TagEnabled = true;
                // create form
                FrmTag frmTag = new FrmTag();
                frmTag.ModeWork = 1;
                frmTag.Tag = newTag;
                // showing the form
                DialogResult dialog = frmTag.ShowDialog();
                // if you have closed the form, click OK
                if (DialogResult.OK == dialog)
                {
                    #region Data display
                    // update without flicker
                    Type type = lstTags.GetType();
                    PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                    propertyInfo.SetValue(lstTags, true, null);

                    this.lstTags.BeginUpdate();

                    // inserted information
                    this.lstTags.Items.Add(new ListViewItem()
                    {
                        // tag name
                        Text = frmTag.Tag.TagName,
                        SubItems =
                            {
                                // adding tag parameters
                                DriverUtils.NullToString(frmTag.Tag.TagCode),
                                DriverUtils.NullToString(frmTag.Tag.TagIPAddress),
                                DriverUtils.NullToString(ListViewAsDisplayStringBoolean(frmTag.Tag.TagEnabled))
                            }
                    }).Tag = frmTag.Tag; // in tag we pass the tag id... so that we can find

                    this.lstTags.EndUpdate();
                    #endregion Data display

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

                    foreach (String tagName in tagsName)
                    {
                        Tag newTag = new Tag();
                        newTag.TagID = Guid.NewGuid();
                        newTag.TagCode = tagName;
                        newTag.TagName = tagName;
                        newTag.TagIPAddress = tagName;
                        newTag.TagTimeout = 1000;
                        newTag.TagEnabled = true;

                        if (!deviceTags.Contains(newTag))
                        {
                            #region Data display
                            // update without flicker
                            Type type = lstTags.GetType();
                            PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                            propertyInfo.SetValue(lstTags, true, null);

                            this.lstTags.BeginUpdate();

                            // inserted information
                            this.lstTags.Items.Add(new ListViewItem()
                            {
                                // tag name
                                Text = newTag.TagName,
                                SubItems =
                            {
                                // adding tag parameters
                                DriverUtils.NullToString(newTag.TagCode),
                                DriverUtils.NullToString(newTag.TagIPAddress),
                                DriverUtils.NullToString(ListViewAsDisplayStringBoolean(newTag.TagEnabled))
                            }
                            }).Tag = newTag; // in tag we pass the tag id... so that we can find

                            this.lstTags.EndUpdate();
                            #endregion Data display

                            Modified = true;
                        }
                    }                   
                }
            }
            catch { }
        }

        #endregion Tag list add

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
                    selected = tmplstTags.SelectedItems[0];
                    indexSelectTag = tmplstTags.SelectedIndices[0];
                    Tag changeTag = (Tag)selected.Tag;

                    FrmTag frmTag = new FrmTag();
                    frmTag.ModeWork = 2;
                    frmTag.Tag = changeTag;
                    // showing the form
                    DialogResult dialog = frmTag.ShowDialog();
                    // if you have closed the form, click OK
                    if (DialogResult.OK == dialog)
                    {
                        #region Data display
                        // update without flicker
                        Type type = lstTags.GetType();
                        PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                        propertyInfo.SetValue(lstTags, true, null);

                        this.lstTags.BeginUpdate();

                        // update information
                        selected.Text = frmTag.Tag.TagName;
                        selected.SubItems[0].Text = frmTag.Tag.TagName;
                        selected.SubItems[1].Text = DriverUtils.NullToString(frmTag.Tag.TagCode);
                        selected.SubItems[2].Text = DriverUtils.NullToString(frmTag.Tag.TagIPAddress);
                        selected.SubItems[3].Text = DriverUtils.NullToString(ListViewAsDisplayStringBoolean(frmTag.Tag.TagEnabled));
                        selected.Tag = frmTag.Tag;

                        this.lstTags.EndUpdate();
                        #endregion Data display
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
        private void lstTags_KeyDown(object sender, KeyEventArgs e)
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
                    selected = tmplstTags.SelectedItems[0];

                    try
                    {
                        if (tmplstTags.Items.Count > 0)
                        {
                            tmplstTags.Items.Remove(this.selected);
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
                lstTags.Items.Clear();
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
                    ListViewExtensions.MoveListViewItems(lstTags, MoveDirection.Up);
                }

                Modified = true;
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
                    ListViewExtensions.MoveListViewItems(lstTags, MoveDirection.Down);
                }

                Modified = true;
            }
        }

        #endregion Tag Down

        #region Timer
        /// <summary>
        /// Timer
        /// </summary>
        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan leftTime = tmrEndTime.Subtract(DateTime.Now);
            if (leftTime.TotalSeconds < 0)
            {
                tmrTimer.Stop();

                // updating information
                tmrTimer_InfoDeviceTagRefresh();

                if (tmrStatus == true)
                {
                    tmrEndTime = DateTime.Now.AddMilliseconds(TimeRefresh);
                    tmrTimer.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Info Device Tag Refresh
        /// </summary>
        private void tmrTimer_InfoDeviceTagRefresh()
        {
            try
            {
                stopWatch.Start();

                #region Ping
                if (PingMode == 0)
                {
                    #region Synchronous
                    try
                    {
                        networkInformation.RunPingSynchronous(deviceTags);
                    }
                    catch { }
                    #endregion Synchronous
                }
                else if (PingMode == 1)
                {
                    #region Asynchronous
                    try
                    {
                        networkInformation.RunPingAsynchronous(deviceTags);
                    }
                    catch { }
                    #endregion Asynchronous
                }
                #endregion Ping         
            }
            catch { }
        }

        public void DebugerLog(string text)
        {

        }

        public void DebugerTag(Tag tag)
        {
            if (lstTags.InvokeRequired)
            {
                lstTags.Invoke((MethodInvoker)delegate ()
                {
                    ListViewItem tagItem = lstTags.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text == tag.TagName);
                    if (tagItem != null)
                    {
                        if (tag.TagEnabled == false)
                        {
                            tagItem.ForeColor = Color.White;
                            tagItem.BackColor = Color.Gray;
                        }

                        if (tag.TagEnabled == true) // enabled
                        {
                            if (tag.TagVal == 1)
                            {
                                try
                                {
                                    tagItem.ForeColor = Color.Black;
                                    tagItem.BackColor = Color.FromArgb(0x79, 0xDA, 0x7C);
                                }
                                catch { }
                            }
                            else if (tag.TagVal == 0)
                            {
                                try
                                {
                                    tagItem.ForeColor = Color.White;
                                    tagItem.BackColor = Color.FromArgb(0xCD, 0x22, 0x30);
                                }
                                catch { }
                            }

                            // updated the information
                            tagItem.Text = tag.TagName;
                            tagItem.Tag = tag;

                            tagItem.SubItems[0].Text = DriverUtils.NullToString(tag.TagName);
                            tagItem.SubItems[1].Text = DriverUtils.NullToString(tag.TagCode);
                            tagItem.SubItems[2].Text = DriverUtils.NullToString(tag.TagIPAddress);
                            tagItem.SubItems[3].Text = DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tag.TagEnabled));
                        }
                    }
                });
            }
            else
            {
                ListViewItem tagItem = lstTags.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text == tag.TagName);
                if (tagItem != null)
                {
                    if (tag.TagEnabled == false)
                    {
                        tagItem.ForeColor = Color.White;
                        tagItem.BackColor = Color.Gray;
                    }

                    if (tag.TagEnabled == true) // enabled
                    {
                        if (tag.TagVal == 1)
                        {
                            try
                            {
                                tagItem.ForeColor = Color.Black;
                                tagItem.BackColor = Color.FromArgb(0x79, 0xDA, 0x7C);
                            }
                            catch { }
                        }
                        else if (tag.TagVal == 0)
                        {
                            try
                            {
                                tagItem.ForeColor = Color.White;
                                tagItem.BackColor = Color.FromArgb(0xCD, 0x22, 0x30);
                            }
                            catch { }
                        }

                        // updated the information
                        tagItem.Text = tag.TagName;
                        tagItem.Tag = tag;

                        tagItem.SubItems[0].Text = DriverUtils.NullToString(tag.TagName);
                        tagItem.SubItems[1].Text = DriverUtils.NullToString(tag.TagCode);
                        tagItem.SubItems[2].Text = DriverUtils.NullToString(tag.TagIPAddress);
                        tagItem.SubItems[3].Text = DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tag.TagEnabled));
                    }
                }
            }
        }

        public void DebugerTags(List<Tag> tags)
        {
            if (tags == null || tags.Count == 0)
            {
                return;
            }

            if (lstTags.InvokeRequired)
            {
                lstTags.Invoke((MethodInvoker)delegate ()
                {
                    #region Update Info
                    System.Windows.Forms.ListView lstTags = this.lstTags;

                    // update without flickering
                    Type type = this.lstTags.GetType();
                    PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                    propertyInfo.SetValue(this.lstTags, true, (object[])null);

                    for (int index = 0; index < tags.Count; index++)
                    {
                        ListViewItem TagItem = lstTags.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text == tags[index].TagName);
                        Tag tmpTag = (Tag)TagItem.Tag;
                       
                        if (tmpTag == null || tmpTag.TagID != tags[index].TagID)
                        {
                            continue;
                        }

                        #region Coloring with color

                        if (tmpTag.TagEnabled == false)
                        {
                            TagItem.ForeColor = Color.White;
                            TagItem.BackColor = Color.Gray;
                        }

                        if (tmpTag.TagEnabled == true) // enabled
                        {
                            if (tmpTag.TagVal == 1)
                            {
                                try
                                {
                                    TagItem.ForeColor = Color.Black;
                                    TagItem.BackColor = Color.FromArgb(0x79, 0xDA, 0x7C);
                                }
                                catch { }
                            }
                            else if (tmpTag.TagVal == 0)
                            {
                                try
                                {
                                    TagItem.ForeColor = Color.White;
                                    TagItem.BackColor = Color.FromArgb(0xCD, 0x22, 0x30);
                                }
                                catch { }
                            }

                            // updated the information
                            TagItem.Text = tmpTag.TagName;
                            TagItem.Tag = tmpTag;

                            TagItem.SubItems[0].Text = DriverUtils.NullToString(tmpTag.TagName);
                            TagItem.SubItems[1].Text = DriverUtils.NullToString(tmpTag.TagCode);
                            TagItem.SubItems[2].Text = DriverUtils.NullToString(tmpTag.TagIPAddress);
                            TagItem.SubItems[3].Text = DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tmpTag.TagEnabled));
                        }

                        #endregion Coloring with color

                    }

                    #endregion Update Info
                });
            }
            else
            {
                #region Update Info
                System.Windows.Forms.ListView lstTags = this.lstTags;

                // update without flickering
                Type type = this.lstTags.GetType();
                PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(this.lstTags, true, (object[])null);

                for (int index = 0; index < tags.Count; index++)
                {
                    ListViewItem TagItem = lstTags.Items.Cast<ListViewItem>().FirstOrDefault(item => item.Text == tags[index].TagName);
                    Tag tmpTag = (Tag)TagItem.Tag;

                    if (tmpTag == null || tmpTag.TagID != tags[index].TagID)
                    {
                        continue;
                    }

                    #region Coloring with color

                    if (tmpTag.TagEnabled == false)
                    {
                        TagItem.ForeColor = Color.White;
                        TagItem.BackColor = Color.Gray;
                    }

                    if (tmpTag.TagEnabled == true) // enabled
                    {
                        if (tmpTag.TagVal == 1)
                        {
                            try
                            {
                                TagItem.ForeColor = Color.Black;
                                TagItem.BackColor = Color.FromArgb(0x79, 0xDA, 0x7C);
                            }
                            catch { }
                        }
                        else if (tmpTag.TagVal == 0)
                        {
                            try
                            {
                                TagItem.ForeColor = Color.White;
                                TagItem.BackColor = Color.FromArgb(0xCD, 0x22, 0x30);
                            }
                            catch { }
                        }

                        // updated the information
                        TagItem.Text = tmpTag.TagName;
                        TagItem.Tag = tmpTag;

                        TagItem.SubItems[0].Text = DriverUtils.NullToString(tmpTag.TagName);
                        TagItem.SubItems[1].Text = DriverUtils.NullToString(tmpTag.TagCode);
                        TagItem.SubItems[2].Text = DriverUtils.NullToString(tmpTag.TagIPAddress);
                        TagItem.SubItems[3].Text = DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tmpTag.TagEnabled));
                    }

                    #endregion Coloring with color

                }

                #endregion Update Info
            }
        }

        #endregion Timer

        #endregion Tab Settings

    }
}
