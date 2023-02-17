using Scada.Comm.Lang;
using Scada.Forms;
using Scada.Lang;
using System.Data;
using System.Reflection;

namespace Scada.Comm.Drivers.DrvPing.View.Forms
{
    /// <summary>
    /// Device configuration form.
    /// <para>Форма настройки конфигурации КП.</para>
    /// </summary>
    public partial class FrmConfig : Form
    {

        #region Variables
        private readonly AppDirs appDirs;               // the application directories
        private readonly string driverCode;             // the driver code
        private readonly int deviceNum;                 // the device number
        private readonly DrvPingConfig config;          // the device configuration
        private string configFileName;                  // the configuration file name
        private bool modified;                          // the configuration was modified

        private List<Tag> deviceTags;                   // tags
        private ListViewItem selected;                  // selected record tag
        private int indexSelectTag = 0;                 // index number tag

        DateTime tmrEndTime = new DateTime();           // timer
        private bool tmrStatus = true;
        private double TimeRefresh = 1000d;

        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
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
            config = new DrvPingConfig();
            configFileName = Path.Combine(appDirs.ConfigDir, DrvPingConfig.GetFileName(deviceNum));
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

        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                try
                {
                    Thread.Sleep(100);
                    // after a time delay, we cancel the task
                    cancelTokenSource.Cancel();

                    // we are waiting for the completion of the task
                    Thread.Sleep(100);
                }
                finally
                {
                    cancelTokenSource.Dispose();
                }

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
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.Message.ToString());
            }
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
                    }).Tag = tmpTag.TagID; // in tag we pass the tag id... so that we can find
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
        /// Saving the settings by first getting the parameters from the controls, and then displaying
        /// </summary>
        private void Save()
        {
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
        }

        #endregion Basic

        #region Tab Settings

        #region Tag Refresh
        /// <summary>
        /// Tag Refresh
        /// </summary>
        private void cmnuTagRefresh_Click(object sender, EventArgs e)
        {
            ConfigToControls();
        }
        #endregion Tag Refresh

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
                Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                Tag tmpTag = deviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
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
                Tag newTag = new Tag();
                newTag.TagID = Guid.NewGuid();
                newTag.TagEnabled = true;

                if (DialogResult.OK == new FrmTag(1, ref newTag).ShowDialog())
                {
                    deviceTags.Add(newTag);
                }

                Save();
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
                        newTag.TagEnabled = true;

                        if (!deviceTags.Contains(newTag))
                        {
                            deviceTags.Add(newTag);
                        }
                    }

                    Save();
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
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    Tag tmpTag = deviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));

                    FrmTag InputBox = new FrmTag(2, ref tmpTag);
                    InputBox.ShowDialog();

                    if (InputBox.DialogResult == DialogResult.OK)
                    {
                        Save();
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
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    Tag tmpTag = deviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
                    indexSelectTag = deviceTags.IndexOf(deviceTags.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    try
                    {
                        if (tmplstTags.Items.Count > 0)
                        {
                            deviceTags.Remove(tmpTag);
                            tmplstTags.Items.Remove(this.selected);
                        }


                        if (indexSelectTag >= 1)
                        {
                            tmplstTags.EnsureVisible(indexSelectTag - 1);
                            tmplstTags.TopItem = tmplstTags.Items[indexSelectTag - 1];

                            tmplstTags.Focus();

                            tmplstTags.Items[indexSelectTag - 1].Selected = true;
                            tmplstTags.Select();
                        }
                    }
                    catch { }
                }

                Save();
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
            ListTagDelete();
        }

        /// <summary>
        /// Tag list delete
        /// </summary>
        private void ListTagDelete()
        {
            try
            {
                deviceTags.Clear();
                Save();
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

                    selected = tmplstTags.SelectedItems[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    Tag tmpTag = deviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
                    indexSelectTag = deviceTags.IndexOf(deviceTags.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    if (indexSelectTag == 0)
                    {
                        return;
                    }
                    else
                    {
                        deviceTags.Reverse(indexSelectTag - 1, 2);
                    }
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

                    selected = tmplstTags.SelectedItems[0];
                    Guid SelectTagID = DriverUtils.StringToGuid(selected.Tag.ToString());

                    Tag tmpTag = deviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
                    indexSelectTag = deviceTags.IndexOf(deviceTags.Where(n => n.TagID == SelectTagID).FirstOrDefault());

                    if (indexSelectTag == deviceTags.Count - 1)
                    {
                        return;
                    }
                    else
                    {
                        deviceTags.Reverse(indexSelectTag, 2);
                    }
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
                Refresh();
                tmrTimer.Stop();

                // updating information
                tmrTimer_InfoDeviceTagRefresh();

                if (tmrStatus == true)
                {
                    tmrEndTime = DateTime.Now.AddMilliseconds(TimeRefresh);
                    tmrTimer.Enabled = true;
                }
            }
            else
            {
                Refresh();
            }
        }

        /// <summary>
        /// Info Device Tag Refresh
        /// </summary>
        private void tmrTimer_InfoDeviceTagRefresh()
        {
            try
            {
                #region Ping and coloring with color
                System.Windows.Forms.ListView lstTags = this.lstTags;

                // update without flickering
                Type type = this.lstTags.GetType();
                PropertyInfo propertyInfo = type.GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
                propertyInfo.SetValue(this.lstTags, true, (object[])null);

                for (int index = 0; index < lstTags.Items.Count; ++index)
                {
                    Application.DoEvents();
                    ListViewItem TagItem = lstTags.Items[index];
                    Guid SelectTagID = (Guid)TagItem.Tag;
                    Tag tmpTag = deviceTags.Find((Predicate<Tag>)(r => r.TagID == SelectTagID));
                    if (tmpTag == null)
                    {
                        continue;
                    }

                    if (tmpTag.TagEnabled == false)
                    {
                        TagItem.ForeColor = Color.White;
                        TagItem.BackColor = Color.Gray;
                    }

                    #region Ping
                    if (tmpTag.TagEnabled == true)
                    {
                        try
                        {
                            CancellationToken token = cancelTokenSource.Token;

                            Task task = new Task(() =>
                            {
                                bool statusIP = NetworkInformationExtensions.Pinger(tmpTag.TagIPAddress);

                                    if (statusIP == true)
                                    {
                                        try
                                        {
                                            TagItem.ForeColor = Color.Black;
                                            TagItem.BackColor = Color.FromArgb(0x79, 0xDA, 0x7C);
                                        }
                                        catch { }
                                    }
                                    else if (statusIP == false)
                                    {
                                        try
                                        {
                                            TagItem.ForeColor = Color.White;
                                            TagItem.BackColor = Color.FromArgb(0xCD, 0x22, 0x30);
                                        }
                                        catch { }
                                    }

                                if (token.IsCancellationRequested)
                                {
                                    token.ThrowIfCancellationRequested(); // генерируем исключение
                                }

                            }, token);

                            try
                            {
                                task.Start();
                            }
                            catch { }

                        }
                        catch { }
                    }
                    #endregion Ping

                    // updated the information
                    TagItem.Text = tmpTag.TagName;
                    TagItem.Tag = tmpTag.TagID;

                    TagItem.SubItems[0].Text = DriverUtils.NullToString(tmpTag.TagName);
                    TagItem.SubItems[1].Text = DriverUtils.NullToString(tmpTag.TagCode);
                    TagItem.SubItems[2].Text = DriverUtils.NullToString(tmpTag.TagIPAddress);
                    TagItem.SubItems[3].Text = DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tmpTag.TagEnabled));

                    Application.DoEvents();
                }
                #endregion Ping and coloring with color
            }
            catch
            { }
        }

        #endregion Timer

        #endregion Tab Settings

    }
}
