using Scada.Forms;
using Scada.Lang;
using System.Net;
using System.Reflection;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    /// <summary>
    /// Search form for active devices on the network.
    /// <para>Форма поиска активных устройств в сети.</para>
    /// </summary>
    public partial class FrmHostSearch : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmHostSearch()
        {
            InitializeComponent();

            this.deviceTags = new List<DriverTag>();
            this.project = new Project();
            this.project.Mode = 1;
            this.project.DeviceTags = this.deviceTags;
            this.driverClient = new DriverClient(project);
            
            DriverTagReturn.OnDebug = new DriverTagReturn.DebugData(DebugerTags);
        }

        #region Variables
        private DriverClient driverClient;                                  // thie driver client
        private Project project;                                            // the device configuration
        public List<DriverTag> deviceTags = new List<DriverTag>();          // tags
        private Dictionary<string, ListViewItem> itemMap = new Dictionary<string, ListViewItem>(); // dictionary for quick access to list items
        #endregion Variables

        #region Form Load
        /// <summary>
        /// Loading the form.
        /// </summary>
        private void FrmHostSearch_Load(object sender, EventArgs e)
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);

            // display the configuration
            ConfigToControls();

            // translate the listview
            FormTranslator.Translate(lstTags, GetType().FullName);
        }
        #endregion Form Load

        #region Config

        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {
            // set the control values   
            SetListViewColumnNames();

            // ip address
            txtRangeStart.Text = "192.168.1.1";
            txtRangeEnd.Text = "192.168.1.255";
        }

        /// <summary>
        /// Sets the configuration parameters according to the controls.
        /// </summary>
        private void ControlsToConfig()
        {
            deviceTags.Clear();
            foreach (ListViewItem itemRow in this.lstTags.Items)
            {
                deviceTags.Add((DriverTag)itemRow.Tag);
            }
        }

        #endregion Config

        #region Language
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
        /// Saving settings.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

            if(rdbAddHostActive.Checked)
            {
                List<DriverTag> filteredTags = deviceTags.FindAll(tag => tag.Val == 1);
                deviceTags = filteredTags;
            }
            else if (rdbAddHostAll.Checked)
            {

            }

            project.DeviceTags = deviceTags;

            DialogResult = DialogResult.OK;

            this.Close();
        }

        /// <summary>
        /// Saving the settings by first getting the parameters from the controls, and then displaying.
        /// </summary>
        private void Save()
        {
            // retrieve the configuration
            ControlsToConfig();
        }
        #endregion Save

        #region Close
        /// <summary>
        /// Close form.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion Close

        #region Textbox

        /// <summary>
        /// Handles changes of the start address.
        /// </summary>
        private void txtRangeStart_TextChanged(object sender, EventArgs e)
        {
            Generate(txtRangeStart.Text.Trim(), txtRangeEnd.Text.Trim());
        }

        /// <summary>
        /// Handles changes of the end address.
        /// </summary>
        private void txtRangeEnd_TextChanged(object sender, EventArgs e)
        {
            Generate(txtRangeStart.Text.Trim(), txtRangeEnd.Text.Trim());
        }

        /// <summary>
        /// Generates a list of addresses for the selected range.
        /// </summary>
        private void Generate(string start, string end)
        {
            List<IPAddress> listIpAddress = new List<IPAddress>();
            deviceTags.Clear();
            lstTags.Items.Clear();

            if (DriverUtils.IsIpAddress(start) && DriverUtils.IsIpAddress(end))
            {
                listIpAddress = IPAddressGenerator.GenerateIPAddresses(start, end);
            }
            else
            {
                return;
            }

            foreach (IPAddress ip in listIpAddress)
            {
                DriverTag newTag = new DriverTag();
                newTag.ID = Guid.NewGuid();
                newTag.Code = ip.ToString();
                newTag.Name = ip.ToString();
                newTag.IpAddress = ip.ToString();
                newTag.Timeout = 1000;
                newTag.Enabled = true;

                deviceTags.Add(newTag);
            }

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
                    tagItem.Tag = tag;

                    if (!itemMap.TryGetValue(tag.Name, out var TagItem))
                    {
                        itemMap.Add(tag.Name, tagItem);
                        this.lstTags.Items.Add(tagItem);
                    }


                    TagInformation(tag);
                }
            }
        }

        #endregion Textbox

        #region Find
        /// <summary>
        /// Start find device.
        /// </summary>
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnClose.Enabled = false;

            driverClient = new DriverClient(project);

            try
            {
                driverClient.Ping();
            }
            catch { }

            btnSave.Enabled = true;
            btnClose.Enabled = true;

        }

        /// <summary>
        /// Recording the tags result.
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
        /// Updates information about the tag in the list.
        /// </summary>
        public void TagInformation(DriverTag tag)
        {

            if (tag == null)
            {
                return;
            }

            // collect changes.
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

                        // update data.
                        TagItem.Text = tagItem.Name;
                        TagItem.SubItems[0].Text = DriverUtils.NullToString(tagItem.Name);
                        TagItem.SubItems[1].Text = DriverUtils.NullToString(tagItem.Code);
                        TagItem.SubItems[2].Text = DriverUtils.NullToString(tagItem.IpAddress);
                        TagItem.SubItems[3].Text = DriverUtils.NullToString(ListViewAsDisplayStringBoolean(tagItem.Enabled));
                        // ... other updates
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

        #endregion Find

        #endregion Control
    }
}
