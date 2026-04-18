using Scada.Forms;
using Scada.Lang;
using System;
using System.IO;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvDDEJP.View.Forms
{
    /// <summary>
    /// Represents a tag properties form.
    /// <para>Представляет форму свойств тега.</para>
    /// </summary>
    public partial class FrmTag : Form
    {
        #region Variable

        private readonly ProjectTag tag;                            // project tag to edit
        private readonly bool isAdd;                                // indicates whether to add a new tag

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        /// <param name="tag">The project tag.</param>
        /// <param name="isAdd">The value indicating whether to add a new tag.</param>
        public FrmTag(ProjectTag tag, bool isAdd = false)
        {
            InitializeComponent();

            this.tag = tag ?? throw new ArgumentNullException(nameof(tag));
            this.isAdd = isAdd;
        }

        /// <summary>
        /// Loads the tag properties into the controls.
        /// <para>Загружает свойства тега в элементы управления.</para>
        /// </summary>
        private void FrmTag_Load(object sender, EventArgs e)
        {
            Translate();

            cbTagFormat.DataSource = Enum.GetValues(typeof(TagDataFormat));

            txtTagName.Text = tag.Name;
            txtTopic.Text = tag.Topic;
            txtItemName.Text = tag.ItemName;
            nudDataLength.Value = Math.Max(nudDataLength.Minimum, Math.Min(nudDataLength.Maximum, tag.DataLength));
            nudChannel.Value = Math.Max(nudChannel.Minimum, Math.Min(nudChannel.Maximum, tag.Channel));
            ckbTagEnabled.Checked = tag.Enabled;
            cbTagFormat.SelectedItem = tag.DataFormat;

            btnAdd.Visible = isAdd;
            btnSave.Visible = !isAdd;

            FormTranslator.Translate(this, GetType().FullName);
        }

        #endregion Basic

        #region Control

        #region Button

        /// <summary>
        /// Handles the Add button click.
        /// <para>Обрабатывает нажатие кнопки Добавить.</para>
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            SaveTag();
        }

        /// <summary>
        /// Handles the Save button click.
        /// <para>Обрабатывает нажатие кнопки Сохранить.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTag();
        }

        /// <summary>
        /// Handles the Close button click.
        /// <para>Обрабатывает нажатие кнопки Закрыть.</para>
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion Button

        #endregion Control

        #region Private Methods

        /// <summary>
        /// Translates the form and context menu.
        /// <para>Переводит форму и контекстное меню.</para>
        /// </summary>
        private void Translate()
        {
            FormTranslator.Translate(this, GetType().FullName);
        }

        /// <summary>
        /// Saves the tag properties.
        /// <para>Сохраняет свойства тега.</para>
        /// </summary>
        private void SaveTag()
        {
            if (string.IsNullOrWhiteSpace(txtTagName.Text))
            {
                ScadaUiUtils.ShowError(Locale.IsRussian ? "Имя тега обязательно." : "Tag name is required.");
                return;
            }

            tag.Name = txtTagName.Text.Trim();
            tag.Topic = txtTopic.Text.Trim();
            tag.ItemName = txtItemName.Text.Trim();
            tag.DataLength = Decimal.ToInt32(nudDataLength.Value);
            tag.Channel = Decimal.ToInt32(nudChannel.Value);
            tag.Enabled = ckbTagEnabled.Checked;
            tag.DataFormat = cbTagFormat.SelectedItem is TagDataFormat format ? format : tag.DataFormat;

            tag.Normalize(tag.Order);

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Loads the language dictionaries.
        /// <para>Загружает словари языка.</para>
        /// </summary>
        private void LoadLanguage(string languageDir, bool isRussian)
        {
            string culture = isRussian ? "ru-RU" : "en-GB";
            string fileName = $"{DriverUtils.DriverCode}.{culture}.xml";
            string languageFile = Path.Combine(languageDir, fileName);

            if (!File.Exists(languageFile))
            {
                languageFile = Path.Combine(languageDir, "Lang", fileName);
            }

            Locale.LoadDictionaries(languageFile, out _);
        }

        #endregion Private Methods
    }
}
