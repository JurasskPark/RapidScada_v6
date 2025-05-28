using Scada.Forms;
using Scada.Lang;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static Scada.Comm.Drivers.DrvFreeDiskSpaceJP.DriverTag;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ListView = System.Windows.Forms.ListView;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    public partial class FrmTask : Form
    {
        public FrmTask()
        {
            InitializeComponent();
        }

        #region Variables
        public FrmConfig formParent;                            // parent form
        public Project project;                                 // the project configuration

        private bool modified;                                  // the configuration was modified

        public Task task = new Task();

        List<DriverTag> listTag = new List<DriverTag>();
        public List<DriverTag> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }

        private ListViewItem selectedItem;              // selected item
        public int indexSelect = 0;                     // num index
        public int sortingMethod = 0;                   // sorting method

        public int deviceNum;
        public int DeviceNum                            // device num
        {
            get { return deviceNum; }
            set { deviceNum = value; }
        }
        #endregion Variables

        #region Form
        private void FrmTask_Load(object sender, EventArgs e)
        {
            LoadData();
            Translate();
        }

        private void LoadData()
        {
            ConfigToControls();
        }

        private void ConfigToControls()
        {
            if (task != null)
            {
                ckbEnabled.Checked = task.Enabled;
                txtName.Text = task.Name.ToString();
                txtDescription.Text = task.Description.ToString();
                txtDiskName.Text = task.DiskName;
                nudPercentageOfFreeSpace.Value = task.ProceentFreeSpace;
                txtPath.Text = task.Path.ToString();
                txtCompressMove.Text = task.PathTo.ToString();

                switch (task.Action)
                {
                    case ActionTask.None:
                        rdbActionNone.Checked = true;
                        break;
                    case ActionTask.Delete:
                        rdbActionDelete.Checked = true;
                        break;
                    case ActionTask.CompressMove:
                        rdbActionCompressMove.Checked = true;
                        break;
                }

                cmbDiskName.Items.AddRange(DriverClient.GetPhysicalDrivesNames().ToArray());

                //DictonatyToolTipHelp();
            }
        }

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

            DialogResult = DialogResult.OK;

            Close();
        }

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void Save()
        {
            ControlsToConfig();
        }

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            Close();
        }

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void ControlsToConfig()
        {
            task.Enabled = ckbEnabled.Checked;
            task.Name = txtName.Text.Trim();
            task.Description = txtDescription.Text.Trim();
            task.DiskName = txtDiskName.Text.Trim();
            task.ProceentFreeSpace = nudPercentageOfFreeSpace.Value;
            task.Path = txtPath.Text.Trim();
            task.PathTo = txtCompressMove.Text.Trim();
            
            if (rdbActionNone.Checked)
            {
                task.Action = ActionTask.None;
            }
            else if (rdbActionDelete.Checked)
            {
                task.Action = ActionTask.Delete;
            }
            else if (rdbActionCompressMove.Checked)
            {
                task.Action = ActionTask.CompressMove;
            }
        }

        #endregion Form

        #region Lang
        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }
        #endregion Lang

        #region Control

        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// <para></para>
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
        /// <para></para>
        /// </summary>
        private void control_Changed(object sender, EventArgs e)
        {
            Modified = true;
        }

        #endregion Control

        #region Disk Name
        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void cmbDiskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDiskName.Text = cmbDiskName.Text;
        }

        #endregion Disk Name

        #region Validate
        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void btnValidate_Click(object sender, EventArgs e)
        {
            Validate();
        }
        

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void Validate()
        {
            // retrieve the configuration
            ControlsToConfig();

            // save the configuration
            formParent.SaveData();

            // load a configuration
            formParent.LoadData();

            // set the control values
            ConfigToControls();

            DebugerReturn.OnDebug = new DebugerReturn.DebugData(PollLogGet);
            DriverTagReturn.OnDebug = new DriverTagReturn.DebugData(PollTagGet);

            DriverClient driverClient = new DriverClient(this.formParent.pathProject, this.formParent.project); 
            driverClient.Validate(task);
        }

        private void PollTagGet(List<DriverTag> tags)
        {
            for (int t = 0; t < tags.Count; t++)
            {
                string parserTagValue = DriverTag.TagToString(tags[t].TagDataValue, tags[t].TagFormatData);
                txtValidate.Text += @$"[{tags[t].TagName}][{tags[t].TagCode}]=[{parserTagValue}]" + Environment.NewLine;
            }
        }

        private void PollLogGet(string text)
        {
            try
            {
                if (!IsHandleCreated)
                {
                    this.CreateControl();
                }

                if (IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        txtValidate.AppendText(text + Environment.NewLine);
                    });
                }
                else
                {
                    txtValidate.AppendText(text + Environment.NewLine);
                }
            }
            catch { }
        }

        #endregion Validate

        #region CSV
        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        public static DataTable Default()
        {
            DataTable dtData = new DataTable();
            dtData.Clear();
            dtData.TableName = "dtData";

            for (int h = 0; h < mibHeader.Length; h++)
            {
                dtData.Columns.Add(mibHeader[h], typeof(string));
            }

            return dtData;
        }

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        public static string[] mibHeader = new string[]
        {
            "TagID",
            "TagName",
            "TagCode",
            "TagAddress",
            "TagDescription",
            "TagEnabled",
            "TagNumberDecimalPlaces",
            "TagValueFormat",
        };

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void tolDataLoadFromCSV_Click(object sender, EventArgs e)
        {
            DataLoadCSV();
        }

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void DataLoadCSV()
        {
            bool header = true;
            bool quotes = true;
            char separater = char.Parse(";");
            string encoding = "UTF8";

            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = DriverPhrases.TitleLoadCSV;
            OFD.Filter = DriverPhrases.FilterCSV;
            OFD.InitialDirectory = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath);

            if (OFD.ShowDialog() == DialogResult.OK && OFD.FileName != "")
            {
                string FileName = OFD.FileName;

                try
                {
                    var listMIB = FastCSV.ReadFile<DriverTag>
                        (
                            FileName,           // filename
                            header,             // has header
                            separater,          // delimiter
                            encoding,
                            quotes,
                            (o, c) =>           // to object function o : cars object, c : columns array read
                            {
                                o.TagID = DriverUtils.StringToGuid(c[0]);
                                o.TagName = c[1];
                                o.TagCode = c[2];
                                o.TagAddress = c[3];
                                o.TagDescription = c[4];
                                o.TagEnabled = Convert.ToBoolean(c[5]);
                                o.TagNumberDecimalPlaces = Convert.ToInt32(c[6]);
                                o.TagValueFormat = (FormatData)Enum.Parse(typeof(FormatData), c[7]); ;

                                // add to list
                                ListTag.Add(o);
                                return true;
                            }
                        );

                    LoadData();
                    Modified = true;
                }
                catch { }
            }
        }

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void tolDataSaveInCSV_Click(object sender, EventArgs e)
        {
            DataSaveCSV();
        }

        /// <summary>
        /// 
        /// <para></para>>
        /// </summary>
        private void DataSaveCSV()
        {
            bool header = true;
            bool quotes = true;
            char separater = char.Parse(";");
            string encoding = "UTF8";

            SaveFileDialog SFD = new SaveFileDialog();
            SFD.Title = DriverPhrases.TitleSaveCSV;
            SFD.Filter = DriverPhrases.FilterCSV;
            SFD.InitialDirectory = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath);

            if (SFD.ShowDialog() == DialogResult.OK && SFD.FileName != "")
            {
                string FileName = SFD.FileName;

                FastCSV.WriteFile<DriverTag>(
                    FileName,
                    mibHeader,          // has header
                    separater,          // delimiter
                    encoding,
                    quotes,
                    ListTag,
                    (o, c) =>
                    {
                        c.Add(o.TagID);
                        c.Add(o.TagName);
                        c.Add(o.TagCode);
                        c.Add(o.TagAddress);
                        c.Add(o.TagDescription);
                        c.Add(o.TagEnabled);
                        c.Add(o.TagNumberDecimalPlaces);
                        c.Add(o.TagValueFormat);
                    });
            }
        }

        #endregion CSV


    }
}
