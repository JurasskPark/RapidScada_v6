using Scada.Comm.Lang;
using Scada.Comm.Drivers.DrvTelnetJP.View;
using Scada.Forms;
using Scada.Lang;
using System.Diagnostics;
using System.Reflection;

namespace Scada.Comm.Drivers.DrvTelnetJP.View.Forms
{
    /// <summary>
    /// Device configuration form.
    /// <para>Форма настройки конфигурации КП.</para>
    /// </summary>
    public partial class FrmConfig : Form
    {
        #region Variable

        private readonly NetworkInformation networkInformation;     // network information
        private readonly Project config;                  // device configuration
        private readonly int deviceNum;                             // device number
        private string configFileName = string.Empty;               // configuration file name
        private bool modified;                                      // configuration was modified
        private bool polling;                                       // polling is running
        private List<Tag> deviceTags = new List<Tag>();             // device tags
        private ListViewItem selected;                              // selected tag item
        private readonly Stopwatch stopWatch = new Stopwatch();     // refresh stopwatch
        private double refreshInterval = 1000d;                     // refresh interval

        #endregion Variable

        #region Property

        private bool Modified
        {
            get => modified;
            set
            {
                modified = value;
                btnSave.Enabled = modified;
            }
        }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        private FrmConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmConfig(AppDirs appDirs, int deviceNum)
            : this()
        {
            if (appDirs == null)
            {
                throw new ArgumentNullException(nameof(appDirs));
            }

            this.deviceNum = deviceNum;
            configFileName = Path.Combine(appDirs.ConfigDir, Project.GetFileName(deviceNum));
            config = new Project();
            networkInformation = CreateNetworkInformation();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmConfig(string configFileName)
            : this()
        {
            this.configFileName = configFileName ?? string.Empty;
            config = new Project();
            networkInformation = CreateNetworkInformation();
        }

        /// <summary>
        /// Loads the form.
        /// <para>Загружает форму.</para>
        /// </summary>
        private void FrmConfig_Load(object sender, EventArgs e)
        {
            FormTranslator.Translate(this, GetType().FullName);
            FormTranslator.Translate(cmnuLstTags, GetType().FullName);
            FormTranslator.Translate(lstTags, GetType().FullName);

            Text = string.Format(Text, deviceNum, DriverUtils.Version);
            LoadConfig();
            ConfigToControls();
            Modified = false;
        }

        /// <summary>
        /// Handles the form shown event.
        /// <para>Обрабатывает отображение формы.</para>
        /// </summary>
        private void FrmConfig_Shown(object sender, EventArgs e)
        {
            tmrTimer.Enabled = true;
            stopWatch.Restart();
        }

        /// <summary>
        /// Handles the form closing event.
        /// <para>Обрабатывает закрытие формы.</para>
        /// </summary>
        private void FrmConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Modified)
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                CommPhrases.SaveDeviceConfigConfirm,
                CommonPhrases.QuestionCaption,
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Save();
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Creates a network information object.
        /// <para>Создает объект сетевой информации.</para>
        /// </summary>
        private NetworkInformation CreateNetworkInformation()
        {
            return new NetworkInformation
            {
                OnDebug = DebugerLog,
                OnDebugTag = DebugerTag,
                OnDebugTags = DebugerTags
            };
        }

        /// <summary>
        /// Loads the configuration file.
        /// <para>Загружает файл конфигурации.</para>
        /// </summary>
        private void LoadConfig()
        {
            if (!string.IsNullOrEmpty(configFileName) && File.Exists(configFileName) && !config.Load(configFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }
        }

        /// <summary>
        /// Displays the configuration in controls.
        /// <para>Отображает конфигурацию в элементах управления.</para>
        /// </summary>
        private void ConfigToControls()
        {
            cbLog.Checked = config.Log;
            deviceTags = config.DeviceTags ?? new List<Tag>();
            SetListViewColumnNames();
            RefreshTagsList();
        }

        /// <summary>
        /// Saves controls to the configuration.
        /// <para>Сохраняет значения элементов управления в конфигурацию.</para>
        /// </summary>
        private void ControlsToConfig()
        {
            config.Log = cbLog.Checked;
            config.DeviceTags = deviceTags;
        }

        /// <summary>
        /// Saves the configuration.
        /// <para>Сохраняет конфигурацию.</para>
        /// </summary>
        private void Save()
        {
            ControlsToConfig();
            if (config.Save(configFileName, out string errMsg))
            {
                Modified = false;
            }
            else
            {
                ScadaUiUtils.ShowError(errMsg);
            }
        }

        /// <summary>
        /// Sets list view column names.
        /// <para>Устанавливает имена столбцов списка.</para>
        /// </summary>
        private void SetListViewColumnNames()
        {
            clmTagname.Text = Locale.IsRussian ? "Имя" : "Name";
            clmTagCode.Text = Locale.IsRussian ? "Код тега" : "Tag code";
            clmTagIPAddressPort.Text = Locale.IsRussian ? "IP-адрес : Порт" : "IP Address : Port";
            clmTagEnabled.Text = Locale.IsRussian ? "Включен" : "Enabled";
        }

        /// <summary>
        /// Converts a boolean value to display text.
        /// <para>Преобразует логическое значение в отображаемый текст.</para>
        /// </summary>
        public string ListViewAsDisplayStringBoolean(bool boolTag)
        {
            return boolTag
                ? Locale.IsRussian ? "Да" : "Yes"
                : Locale.IsRussian ? "Нет" : "No";
        }

        #endregion Basic

        #region Control

        #region Button

        /// <summary>
        /// Saves the configuration.
        /// <para>Сохраняет конфигурацию.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        /// <summary>
        /// Closes the form.
        /// <para>Закрывает форму.</para>
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion Button

        #region Common

        /// <summary>
        /// Marks the configuration as modified.
        /// <para>Помечает конфигурацию как измененную.</para>
        /// </summary>
        private void control_Changed(object sender, EventArgs e)
        {
            Modified = true;
        }

        #endregion Common

        #region ListView

        /// <summary>
        /// Handles tag selection by mouse click.
        /// <para>Обрабатывает выбор тега щелчком мыши.</para>
        /// </summary>
        private void lstTags_MouseClick(object sender, MouseEventArgs e)
        {
            SelectTag();
        }

        /// <summary>
        /// Handles tag editing by double click.
        /// <para>Обрабатывает редактирование тега двойным щелчком.</para>
        /// </summary>
        private void lstTags_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangeTag();
        }

        /// <summary>
        /// Handles tag deletion by keyboard.
        /// <para>Обрабатывает удаление тега с клавиатуры.</para>
        /// </summary>
        private void lstTags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DeleteTag();
            }
        }

        /// <summary>
        /// Selects the current tag.
        /// <para>Выбирает текущий тег.</para>
        /// </summary>
        private void SelectTag()
        {
            selected = lstTags.SelectedItems.Count > 0 ? lstTags.SelectedItems[0] : null;
        }

        /// <summary>
        /// Refreshes the tag list.
        /// <para>Обновляет список тегов.</para>
        /// </summary>
        private void RefreshTagsList()
        {
            EnableDoubleBuffering(lstTags);
            lstTags.BeginUpdate();
            try
            {
                lstTags.Items.Clear();
                foreach (Tag tag in deviceTags)
                {
                    AddTagItem(tag);
                }
            }
            finally
            {
                lstTags.EndUpdate();
            }
        }

        /// <summary>
        /// Adds a tag item to the list.
        /// <para>Добавляет элемент тега в список.</para>
        /// </summary>
        private void AddTagItem(Tag tag)
        {
            ListViewItem item = new ListViewItem(tag.TagName)
            {
                Tag = tag
            };

            item.SubItems.Add(DriverUtils.NullToString(tag.TagCode));
            item.SubItems.Add(GetAddressPortText(tag));
            item.SubItems.Add(ListViewAsDisplayStringBoolean(tag.TagEnabled));
            SetTagItemColor(item, tag);
            lstTags.Items.Add(item);
        }

        /// <summary>
        /// Updates a tag item.
        /// <para>Обновляет элемент тега.</para>
        /// </summary>
        private void UpdateTagItem(ListViewItem item, Tag tag)
        {
            item.Text = tag.TagName;
            item.Tag = tag;
            EnsureSubItemCount(item, 4);
            item.SubItems[1].Text = DriverUtils.NullToString(tag.TagCode);
            item.SubItems[2].Text = GetAddressPortText(tag);
            item.SubItems[3].Text = ListViewAsDisplayStringBoolean(tag.TagEnabled);
            SetTagItemColor(item, tag);
        }

        /// <summary>
        /// Ensures the subitem count.
        /// <para>Проверяет количество подэлементов.</para>
        /// </summary>
        private static void EnsureSubItemCount(ListViewItem item, int count)
        {
            while (item.SubItems.Count < count)
            {
                item.SubItems.Add(string.Empty);
            }
        }

        /// <summary>
        /// Gets address and port display text.
        /// <para>Возвращает отображаемый адрес и порт.</para>
        /// </summary>
        private static string GetAddressPortText(Tag tag)
        {
            return $"{tag.TagIPAddress}:{tag.TagPort}";
        }

        /// <summary>
        /// Enables double buffering for the list view.
        /// <para>Включает двойную буферизацию списка.</para>
        /// </summary>
        private static void EnableDoubleBuffering(ListView listView)
        {
            PropertyInfo propertyInfo = typeof(ListView).GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            propertyInfo?.SetValue(listView, true, null);
        }

        #endregion ListView

        #region Menu

        /// <summary>
        /// Adds a tag.
        /// <para>Добавляет тег.</para>
        /// </summary>
        private void cmnuTagAdd_Click(object sender, EventArgs e)
        {
            AddTag();
        }

        /// <summary>
        /// Adds tags from a list.
        /// <para>Добавляет теги из списка.</para>
        /// </summary>
        private void cmnuListTagAdd_Click(object sender, EventArgs e)
        {
            AddTagsFromList();
        }

        /// <summary>
        /// Changes a tag.
        /// <para>Изменяет тег.</para>
        /// </summary>
        private void cmnuTagChange_Click(object sender, EventArgs e)
        {
            ChangeTag();
        }

        /// <summary>
        /// Deletes a tag.
        /// <para>Удаляет тег.</para>
        /// </summary>
        private void cmnuTagDelete_Click(object sender, EventArgs e)
        {
            DeleteTag();
        }

        /// <summary>
        /// Deletes all tags.
        /// <para>Удаляет все теги.</para>
        /// </summary>
        private void cmnuTagAllDelete_Click(object sender, EventArgs e)
        {
            DeleteAllTags();
        }

        /// <summary>
        /// Moves selected tags up.
        /// <para>Перемещает выбранные теги вверх.</para>
        /// </summary>
        private void cmnuUp_Click(object sender, EventArgs e)
        {
            lstTags.MoveListViewItems(MoveDirection.Up);
            SyncTagsFromListView();
            Modified = true;
        }

        /// <summary>
        /// Moves selected tags down.
        /// <para>Перемещает выбранные теги вниз.</para>
        /// </summary>
        private void cmnuDown_Click(object sender, EventArgs e)
        {
            lstTags.MoveListViewItems(MoveDirection.Down);
            SyncTagsFromListView();
            Modified = true;
        }

        #endregion Menu

        #region Timer

        /// <summary>
        /// Handles timer ticks.
        /// <para>Обрабатывает тики таймера.</para>
        /// </summary>
        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            if (stopWatch.Elapsed.TotalMilliseconds < refreshInterval || polling)
            {
                return;
            }

            stopWatch.Restart();
            RunTelnetCheck();
        }

        /// <summary>
        /// Refreshes tag information.
        /// <para>Обновляет информацию тегов.</para>
        /// </summary>
        private void tmrTimer_InfoDeviceTagRefresh()
        {
            RunTelnetCheck();
        }

        #endregion Timer

        #endregion Control

        #region Tag Operations

        /// <summary>
        /// Adds a tag.
        /// <para>Добавляет тег.</para>
        /// </summary>
        private void AddTag()
        {
            FrmTag frmTag = new FrmTag
            {
                ModeWork = 1,
                Tag = new Tag { TagTimeout = 1000, TagEnabled = true }
            };

            if (frmTag.ShowDialog() == DialogResult.OK && frmTag.Tag != null)
            {
                deviceTags.Add(frmTag.Tag);
                AddTagItem(frmTag.Tag);
                Modified = true;
            }
        }

        /// <summary>
        /// Adds tags from a list.
        /// <para>Добавляет теги из списка.</para>
        /// </summary>
        private void AddTagsFromList()
        {
            FrmInputBox inputBox = new FrmInputBox();
            if (inputBox.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string[] tagNames = inputBox.Values.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (string tagName in tagNames)
            {
                if (!TryCreateTagFromAddress(tagName, out Tag newTag))
                {
                    continue;
                }

                deviceTags.Add(newTag);
                AddTagItem(newTag);
                Modified = true;
            }
        }

        /// <summary>
        /// Tries to create a tag from an address string.
        /// <para>Пытается создать тег из строки адреса.</para>
        /// </summary>
        private static bool TryCreateTagFromAddress(string address, out Tag tag)
        {
            tag = null;
            string host = DriverUtils.IPAddressNoPort(address);
            string portText = DriverUtils.PortNoIPAddress(address);

            if (string.IsNullOrWhiteSpace(host) || !int.TryParse(portText, out int port))
            {
                return false;
            }

            tag = new Tag
            {
                TagID = Guid.NewGuid(),
                TagCode = address,
                TagName = address,
                TagIPAddress = host,
                TagPort = port,
                TagTimeout = 1000,
                TagEnabled = true
            };

            return true;
        }

        /// <summary>
        /// Changes a tag.
        /// <para>Изменяет тег.</para>
        /// </summary>
        private void ChangeTag()
        {
            SelectTag();
            if (selected?.Tag is not Tag tag)
            {
                return;
            }

            FrmTag frmTag = new FrmTag
            {
                ModeWork = 2,
                Tag = tag
            };

            if (frmTag.ShowDialog() == DialogResult.OK)
            {
                UpdateTagItem(selected, frmTag.Tag);
                Modified = true;
            }
        }

        /// <summary>
        /// Deletes a tag.
        /// <para>Удаляет тег.</para>
        /// </summary>
        private void DeleteTag()
        {
            SelectTag();
            if (selected?.Tag is not Tag tag)
            {
                return;
            }

            deviceTags.Remove(tag);
            lstTags.Items.Remove(selected);
            selected = null;
            Modified = true;
        }

        /// <summary>
        /// Deletes all tags.
        /// <para>Удаляет все теги.</para>
        /// </summary>
        private void DeleteAllTags()
        {
            deviceTags.Clear();
            lstTags.Items.Clear();
            selected = null;
            Modified = true;
        }

        /// <summary>
        /// Synchronizes tags from the list view order.
        /// <para>Синхронизирует теги с порядком списка.</para>
        /// </summary>
        private void SyncTagsFromListView()
        {
            deviceTags = lstTags.Items
                .Cast<ListViewItem>()
                .Select(item => item.Tag as Tag)
                .Where(tag => tag != null)
                .ToList();
        }

        #endregion Tag Operations

        #region Telnet

        /// <summary>
        /// Runs TCP checks in the background.
        /// <para>Выполняет TCP-проверки в фоне.</para>
        /// </summary>
        private void RunTelnetCheck()
        {
            polling = true;
            List<Tag> tagsSnapshot = deviceTags.ToList();

            Task.Run(() => networkInformation.RunTelnet(tagsSnapshot))
                .ContinueWith(_ =>
                {
                    if (!IsDisposed)
                    {
                        BeginInvoke(new System.Windows.Forms.MethodInvoker(() => polling = false));
                    }
                });
        }

        /// <summary>
        /// Writes a debug message.
        /// <para>Записывает отладочное сообщение.</para>
        /// </summary>
        public void DebugerLog(string text)
        {
            // the configuration form does not display a text log.
        }

        /// <summary>
        /// Updates one tag after a TCP check.
        /// <para>Обновляет один тег после TCP-проверки.</para>
        /// </summary>
        public void DebugerTag(Tag tag)
        {
            if (tag == null || IsDisposed)
            {
                return;
            }

            if (InvokeRequired)
            {
                BeginInvoke(new System.Windows.Forms.MethodInvoker(() => DebugerTag(tag)));
                return;
            }

            ListViewItem tagItem = lstTags.Items
                .Cast<ListViewItem>()
                .FirstOrDefault(item => item.Tag is Tag itemTag && itemTag.TagID == tag.TagID);

            if (tagItem == null || tagItem.Tag is not Tag existingTag)
            {
                return;
            }

            existingTag.TagVal = tag.TagVal;
            existingTag.TagStat = tag.TagStat;
            existingTag.TagIPAddress = tag.TagIPAddress;
            UpdateTagItem(tagItem, existingTag);
        }

        /// <summary>
        /// Updates tags after TCP checks.
        /// <para>Обновляет теги после TCP-проверок.</para>
        /// </summary>
        public void DebugerTags(List<Tag> tags)
        {
            if (tags == null)
            {
                return;
            }

            foreach (Tag tag in tags)
            {
                DebugerTag(tag);
            }
        }

        /// <summary>
        /// Sets item color according to tag state.
        /// <para>Устанавливает цвет элемента по состоянию тега.</para>
        /// </summary>
        private static void SetTagItemColor(ListViewItem item, Tag tag)
        {
            if (!tag.TagEnabled)
            {
                item.ForeColor = Color.White;
                item.BackColor = Color.Gray;
            }
            else if (tag.TagVal == 1)
            {
                item.ForeColor = Color.Black;
                item.BackColor = Color.FromArgb(0x79, 0xDA, 0x7C);
            }
            else if (tag.TagVal == 0)
            {
                item.ForeColor = Color.White;
                item.BackColor = Color.FromArgb(0xCD, 0x22, 0x30);
            }
        }

        #endregion Telnet
    }
}
