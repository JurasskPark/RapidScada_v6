using Scada.Comm.Config;
using Scada.Comm.Drivers.DrvDDEJP;
using Scada.Forms;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvDDEJP.View.Forms
{
    /// <summary>
    /// Represents a project properties form.
    /// <para>Представляет форму свойств проекта.</para>
    /// </summary>
    public partial class FrmProject : Form
    {
        #region Variable

        private readonly int deviceNum;                             // device number
        private readonly string projectFileName;                    // project file path
        private readonly Project project;                           // project configuration
        private string languageDir;                                 // language directory
        private IDriverClient driverClient;                         // DDE driver client

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmProject()
        {
            InitializeComponent();

            project = new Project();
            driverClient = null;
            languageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Lang");
            projectFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DriverUtils.GetFileName(0));
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        /// <param name="appDirs">The application directories.</param>
        /// <param name="lineConfig">The line configuration.</param>
        /// <param name="deviceConfig">The device configuration.</param>
        /// <param name="deviceNum">The device number.</param>
        public FrmProject(AppDirs appDirs, LineConfig lineConfig, DeviceConfig deviceConfig, int deviceNum)
            : this()
        {
            this.deviceNum = deviceNum;
            languageDir = appDirs.LangDir;
            projectFileName = Path.Combine(appDirs.ConfigDir, DriverUtils.GetFileName(deviceNum));
        }

        /// <summary>
        /// Handles the form load event.
        /// <para>Обрабатывает событие загрузки формы.</para>
        /// </summary>
        private void FrmProject_Load(object sender, EventArgs e)
        {
            if (!project.Load(projectFileName, out string errMsg) && !string.IsNullOrWhiteSpace(errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            LoadLanguage(languageDir, Locale.IsRussian);

            SetListViewColumnNames();
            Translate();

            Text = string.Format(Locale.IsRussian ? "Параметры DrvDDEJP [{0}]" : "DrvDDEJP Parameters [{0}]", deviceNum);
            ControlsFromProject();
            RefreshTagsList();
            btnSave.Enabled = false;
        }

        #endregion Basic

        #region Control

        #region Button

        /// <summary>
        /// Handles the Save button click.
        /// <para>Обрабатывает нажатие кнопки Сохранить.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToProject();
            ReorderTags();

            if (!project.Save(projectFileName, out string errMsg))
            {
                ScadaUiUtils.ShowError(errMsg);
                return;
            }

            btnSave.Enabled = false;
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// Handles the Close button click.
        /// <para>Обрабатывает нажатие кнопки Закрыть.</para>
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            tmrPoll.Stop();
            driverClient?.Dispose();
            Close();
        }

        /// <summary>
        /// Handles the Connect button click.
        /// <para>Обрабатывает нажатие кнопки Подключить.</para>
        /// </summary>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                ControlsToProject();
                driverClient?.Dispose();
                driverClient = DriverClientFactory.Create(project);
                driverClient.Connect();
                RefreshLiveData();
                tmrPoll.Start();
                btnConnect.Enabled = false;
                btnDisconnect.Enabled = true;
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Handles the Disconnect button click.
        /// <para>Обрабатывает нажатие кнопки Отключить.</para>
        /// </summary>
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            tmrPoll.Stop();
            driverClient?.Dispose();
            driverClient = null;
            ClearTagValues();
            btnConnect.Enabled = true;
            btnDisconnect.Enabled = false;
        }

        #endregion Button

        #region Menu

        /// <summary>
        /// Handles the Add Tag menu item click.
        /// <para>Обрабатывает нажатие пункта меню Добавить тег.</para>
        /// </summary>
        private void cmnuTagAdd_Click(object sender, EventArgs e)
        {
            TagAdd();
        }

        /// <summary>
        /// Handles the Change Tag menu item click.
        /// <para>Обрабатывает нажатие пункта меню Изменить тег.</para>
        /// </summary>
        private void cmnuTagChange_Click(object sender, EventArgs e)
        {
            TagChange();
        }

        /// <summary>
        /// Handles the Delete Tag menu item click.
        /// <para>Обрабатывает нажатие пункта меню Удалить тег.</para>
        /// </summary>
        private void cmnuTagDelete_Click(object sender, EventArgs e)
        {
            TagDelete();
        }

        #endregion Menu

        #region ListView

        /// <summary>
        /// Handles the tags ListView double click.
        /// <para>Обрабатывает двойной щелчок по списку тегов.</para>
        /// </summary>
        private void lvTags_DoubleClick(object sender, EventArgs e)
        {
            if (lvTags.SelectedItems.Count > 0)
            {
                TagChange();
            }
        }

        /// <summary>
        /// Handles the key down event in the tags ListView.
        /// <para>Обрабатывает нажатие клавиши в списке тегов.</para>
        /// </summary>
        private void lstTags_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                TagDelete();
            }
        }

        #endregion ListView

        #region Timer

        /// <summary>
        /// Handles the poll timer tick.
        /// <para>Обрабатывает событие таймера опроса.</para>
        /// </summary>
        private void tmrPoll_Tick(object sender, EventArgs e)
        {
            try
            {
                RefreshLiveData();
            }
            catch (Exception ex)
            {
                tmrPoll.Stop();
                btnConnect.Enabled = true;
                btnDisconnect.Enabled = false;
                ScadaUiUtils.ShowError(ex.Message);
            }
        }

        #endregion Timer

        #region Standard Controls

        /// <summary>
        /// Handles the control change event.
        /// <para>Обрабатывает событие изменения элемента управления.</para>
        /// </summary>
        private void controls_Changed(object sender, EventArgs e)
        {
            SetModified();
        }

        #endregion Standard Controls

        #endregion Control

        #region Private Methods

        /// <summary>
        /// Translates the form and context menu.
        /// <para>Переводит форму и контекстное меню.</para>
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
            FormTranslator.Translate(cmnuMenuListTags, GetType().FullName);
        }

        /// <summary>
        /// Sets the names for the ListView columns.
        /// <para>Устанавливает имена столбцов ListView.</para>
        /// </summary>
        private void SetListViewColumnNames()
        {
            colTagName.Name = nameof(colTagName);
            colTagChannel.Name = nameof(colTagChannel);
            colTagTopic.Name = nameof(colTagTopic);
            colTagItem.Name = nameof(colTagItem);
            colTagFormat.Name = nameof(colTagFormat);
            colTagValue.Name = nameof(colTagValue);
        }

        /// <summary>
        /// Copies values from controls to the project object.
        /// <para>Копирует значения из элементов управления в объект проекта.</para>
        /// </summary>
        private void ControlsToProject()
        {
            project.ServiceName = txtServiceName.Text.Trim();
            project.DefaultTopic = txtDefaultTopic.Text.Trim();
            project.RequestTimeout = Decimal.ToInt32(nudRequestTimeout.Value);
            project.ReconnectDelay = Decimal.ToInt32(nudReconnectDelay.Value);
        }

        /// <summary>
        /// Fills controls with values from the project object.
        /// <para>Заполняет элементы управления значениями из объекта проекта.</para>
        /// </summary>
        private void ControlsFromProject()
        {
            txtServiceName.Text = project.ServiceName;
            txtDefaultTopic.Text = project.DefaultTopic;
            nudRequestTimeout.Value = Math.Max(nudRequestTimeout.Minimum, Math.Min(nudRequestTimeout.Maximum, project.RequestTimeout));
            nudReconnectDelay.Value = Math.Max(nudReconnectDelay.Minimum, Math.Min(nudReconnectDelay.Maximum, project.ReconnectDelay));
        }

        /// <summary>
        /// Refreshes the tags list in the ListView.
        /// <para>Обновляет список тегов в ListView.</para>
        /// </summary>
        private void RefreshTagsList()
        {
            lvTags.BeginUpdate();
            lvTags.Items.Clear();

            foreach (ProjectTag tag in project.Tags.OrderBy(t => t.Order))
            {
                string topic = string.IsNullOrWhiteSpace(tag.Topic) ? project.DefaultTopic : tag.Topic;
                string item = tag.ItemName;

                ListViewItem itemView = new ListViewItem(tag.Name);
                itemView.SubItems.Add(tag.Channel.ToString());
                itemView.SubItems.Add(topic);
                itemView.SubItems.Add(item);
                itemView.SubItems.Add(tag.DataFormat.ToString());
                itemView.SubItems.Add(string.Empty);
                itemView.Tag = tag;
                lvTags.Items.Add(itemView);
            }

            lvTags.EndUpdate();
        }

        /// <summary>
        /// Refreshes live data using the driver client.
        /// <para>Обновляет живые данные с помощью клиента драйвера.</para>
        /// </summary>
        private void RefreshLiveData()
        {
            if (driverClient == null)
            {
                return;
            }

            Dictionary<Guid, string> values = driverClient.ReadTags(project.Tags);
            foreach (ListViewItem item in lvTags.Items)
            {
                if (item.Tag is ProjectTag tag)
                {
                    item.SubItems[5].Text = values.TryGetValue(tag.Id, out string value)
                        ? value
                        : string.Empty;
                }
            }
        }

        /// <summary>
        /// Clears the displayed tag values.
        /// <para>Очищает отображаемые значения тегов.</para>
        /// </summary>
        private void ClearTagValues()
        {
            foreach (ListViewItem item in lvTags.Items)
            {
                item.SubItems[5].Text = string.Empty;
            }
        }

        /// <summary>
        /// Reorders tags according to their position in the list.
        /// <para>Переупорядочивает теги в соответствии с их позицией в списке.</para>
        /// </summary>
        private void ReorderTags()
        {
            for (int i = 0; i < project.Tags.Count; i++)
            {
                project.Tags[i].Order = i;
            }
        }

        /// <summary>
        /// Marks the project as modified.
        /// <para>Помечает проект как изменённый.</para>
        /// </summary>
        private void SetModified()
        {
            btnSave.Enabled = true;
        }

        /// <summary>
        /// Loads regional language dictionaries.
        /// <para>Загружает региональные словари языка.</para>
        /// </summary>
        private void LoadLanguage(string languageDir, bool isRussian)
        {
            isRussian = Locale.IsRussian;
            string culture = isRussian ? "ru-RU" : "en-GB";
            string fileName = $"{DriverUtils.DriverCode}.{culture}.xml";
            string languageFile = Path.Combine(languageDir, fileName);

            if (!File.Exists(languageFile))
            {
                languageFile = Path.Combine(languageDir, "Lang", fileName);
            }

            if (!Locale.LoadDictionaries(languageFile, out string errMsg) && errMsg != string.Empty)
            {
                ScadaUiUtils.ShowError(errMsg);
            }

            // load translate the form
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDDEJP.View.Forms.FrmProject");
            Locale.GetDictionary("Scada.Comm.Drivers.DrvDDEJP.View.Forms.Tag.FrmTag");

            if (errMsg != string.Empty)
            {
                MessageBox.Show(errMsg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// Adds a new tag to the project.
        /// <para>Добавляет новый тег в проект.</para>
        /// </summary>
        private void TagAdd()
        {
            try
            {
                ProjectTag newTag = new ProjectTag
                {
                    Order = project.Tags.Count,
                    Name = $"Tag_{project.Tags.Count + 1}",
                    Channel = project.Tags.Count + 1,
                    Topic = project.DefaultTopic,
                    ItemName = "item"
                };

                using FrmTag dialog = new FrmTag(newTag, true);
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    project.Tags.Add(newTag);
                    ReorderTags();
                    RefreshTagsList();
                    SetModified();
                }
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Changes the selected tag.
        /// <para>Изменяет текущий выбранный тег.</para>
        /// </summary>
        private void TagChange()
        {
            try
            {
                if (lvTags.SelectedItems.Count > 0 && lvTags.SelectedItems[0].Tag is ProjectTag tag)
                {
                    using FrmTag dialog = new FrmTag(tag, false);
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        RefreshTagsList();
                        SetModified();
                    }
                }
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the selected tag.
        /// <para>Удаляет текущий выбранный тег.</para>
        /// </summary>
        private void TagDelete()
        {
            try
            {
                if (lvTags.SelectedItems.Count > 0 && lvTags.SelectedItems[0].Tag is ProjectTag tag)
                {
                    project.Tags.Remove(tag);
                    ReorderTags();
                    RefreshTagsList();
                    SetModified();
                }
            }
            catch (Exception ex)
            {
                ScadaUiUtils.ShowError(ex.Message);
            }
        }

        #endregion Private Methods
    }
}
