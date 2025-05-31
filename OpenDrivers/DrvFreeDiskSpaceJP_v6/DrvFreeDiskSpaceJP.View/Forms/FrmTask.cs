using Scada.Forms;
using MethodInvoker = System.Windows.Forms.MethodInvoker;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    /// <summary>
    /// A form with task settings.
    /// <para>Форма с настройками задачи.</para>
    /// </summary>
    public partial class FrmTask : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmTask()
        {
            InitializeComponent();
        }

        #region Variables
        public FrmConfig formParent;                            // parent form
        public Project project;                                 // the project configuration

        private bool modified;                                  // the configuration was modified

        public Task task = new Task();                          // the task

        List<DriverTag> listTag = new List<DriverTag>();        // the list task
        public List<DriverTag> ListTag
        {
            get { return listTag; }
            set { listTag = value; }
        }

        private ListViewItem selectedItem;              // selected item
        public int indexSelect = 0;                     // num index
        public int sortingMethod = 0;                   // sorting method

        public int deviceNum;                           // the device num
        public int DeviceNum                            
        {
            get { return deviceNum; }
            set { deviceNum = value; }
        }
        #endregion Variables

        #region Form Load
        /// <summary>
        /// Load the form.
        /// <para>Загрузка формы.</para>
        /// </summary>
        private void FrmTask_Load(object sender, EventArgs e)
        {
            LoadData();
            Translate();
        }
        #endregion Form Load

        #region Load Data
        /// <summary>
        /// Load data.
        /// <para>Загрузка данных.</para>
        /// </summary>
        private void LoadData()
        {
            ConfigToControls();
        }
        #endregion Load Data

        #region Control
        /// <summary>
        /// Close the form and save the settings.
        /// <para>Закрытие формы и сохранение настроек.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();

            DialogResult = DialogResult.OK;

            Close();
        }

        /// <summary>
        /// Saving the settings.
        /// <para>Сохранение настроек.</para>>
        /// </summary>
        private void Save()
        {
            ControlsToConfig();
        }

        /// <summary>
        /// Closing the form without saving settings.
        /// <para>Закрытие формы без сохранения настроек.</para>>
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

            Close();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// <para>Возвращает или задает значение, указывающее, была ли изменена конфигурация.</para>
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
        /// <para>Возвращает или задает значение, указывающее, была ли изменена конфигурация.</para>
        /// </summary>
        private void control_Changed(object sender, EventArgs e)
        {
            Modified = true;
        }
        #endregion Control

        #region Config 
        /// <summary>
        /// Sets the controls according to the configuration.
        /// <para>Установить элементы управления в соответствии с конфигурацией.</para>
        /// </summary>
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
            }
        }

        /// <summary>
        /// Sets the controls according to the configuration.
        /// <para>Устанавливает элементы управления в соответствии с конфигурацией.</para>>
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
        #endregion Config 
  
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

        #region Disk Name
        /// <summary>
        /// Select the disk name.
        /// <para>Выбор название диска.</para>>
        /// </summary>
        private void cmbDiskName_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDiskName.Text = cmbDiskName.Text;
        }

        #endregion Disk Name

        #region Validate
        /// <summary>
        /// Checking the task.
        /// <para>Проверка задачи.</para>>
        /// </summary>
        private void btnValidate_Click(object sender, EventArgs e)
        {
            ValidateTask();
        }

        /// <summary>
        /// Checking the task.
        /// <para>Проверка задачи.</para>>
        /// </summary>
        private void ValidateTask()
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

        /// <summary>
        /// Getting information from tags from the driver client.
        /// <para>Получение информацию с тегов из клиента драйвера.</para>
        /// </summary>
        private void PollTagGet(List<DriverTag> tags)
        {
            for (int t = 0; t < tags.Count; t++)
            {
                string driverTagValue = DriverTag.TagToString(tags[t].TagDataValue, tags[t].TagValueFormat);
                txtValidate.Text += @$"[{tags[t].TagName}][{tags[t].TagCode}]=[{driverTagValue}]" + Environment.NewLine;
            }
        }

        /// <summary>
        /// Getting the log information from the driver client.
        /// <para>Получение информацию лога из клиента драйвера.</para>
        /// </summary>
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

    }
}
