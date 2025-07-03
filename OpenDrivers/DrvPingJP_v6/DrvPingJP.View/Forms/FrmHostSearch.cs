using Scada.Comm.Devices;
using Scada.Config;
using Scada.Forms;
using Scada.Lang;
using System.Collections;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using Timer = System.Windows.Forms.Timer;

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
        }

        #region Variables
        private readonly NetworkInformation networkInformation; // network (ping)
        public List<Tag> deviceTags;                           // tags
        private ListViewItem selected;                          // selected record tag
        private int indexSelectTag = 0;                         // index number tag

        DateTime tmrEndTime = new DateTime();                   // timer
        private bool tmrStatus = true;                          // timer status
        private double TimeRefresh = 1000d;                     // period

        private int PingMode = 0;                               // type ping
        Stopwatch stopWatch = new Stopwatch();                  // stop watch
        TimeSpan ts;                                            // timespan
        #endregion Variables

        #region Form Load
        /// <summary>
        /// Loading the form
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
            cmbRangeType.SelectedIndex = 0;

            // set the control values   
            SetListViewColumnNames();

            // tags
            deviceTags = new List<Tag>();

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
        /// Sets the configuration parameters according to the controls.
        /// </summary>
        private void ControlsToConfig()
        {
            deviceTags.Clear();
            foreach (ListViewItem itemRow in this.lstTags.Items)
            {
                deviceTags.Add((Tag)itemRow.Tag);
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

        #region Range Type
        /// <summary>
        /// Range Type
        /// </summary>
        private void cmbRangeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbRangeType.SelectedIndex == 0)
            {
                lblRangeSep.Text = "-";
                lblRangeEnd.Text = "Конец диапазона:";
                txtRangeEnd.Size = new Size(130, txtRangeEnd.Size.Height);
            }
            else if (cmbRangeType.SelectedIndex == 1)
            {
                lblRangeSep.Text = "/";
                lblRangeEnd.Text = "Маска подсети:";
                txtRangeEnd.Size = new Size(32, txtRangeEnd.Size.Height);
            }
        }

        #endregion Range Type

        #region 
        /// <summary>
        /// Start find device
        /// </summary>
        private void btnStartStop_Click(object sender, EventArgs e)
        {

        }
        #endregion 

        #endregion Control








    }
}
