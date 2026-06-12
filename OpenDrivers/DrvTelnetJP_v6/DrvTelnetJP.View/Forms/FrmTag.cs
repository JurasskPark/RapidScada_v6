using Scada.Forms;

namespace Scada.Comm.Drivers.DrvTelnetJP.View.Forms
{
    /// <summary>
    /// Edits a TCP check tag.
    /// <para>Редактирует тег проверки TCP-порта.</para>
    /// </summary>
    public partial class FrmTag : Form
    {
        #region Variable

        public int ModeWork;                                      // form work mode
        public new Tag Tag;                                       // edited tag

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmTag()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the form.
        /// <para>Загружает форму.</para>
        /// </summary>
        private void FrmTag_Load(object sender, EventArgs e)
        {
            FormTranslator.Translate(this, GetType().FullName);

            if (Tag == null)
            {
                Tag = new Tag();
            }

            TagsToControls();
            SetModeControls();
        }

        /// <summary>
        /// Displays tag data in controls.
        /// <para>Отображает данные тега в элементах управления.</para>
        /// </summary>
        private void TagsToControls()
        {
            txtTagCode.Text = Tag.TagCode;
            txtTagname.Text = Tag.TagName;
            txtIPAddress.Text = Tag.TagIPAddress;
            nudPort.Value = Convert.ToDecimal(Tag.TagPort == 0 ? 1 : Tag.TagPort);
            nudTimeout.Value = Convert.ToDecimal(Tag.TagTimeout);
            ckbTagEnabled.Checked = Tag.TagEnabled;
        }

        /// <summary>
        /// Saves control values to the tag.
        /// <para>Сохраняет значения элементов управления в тег.</para>
        /// </summary>
        private void ControlsToTag(bool generateID)
        {
            if (generateID)
            {
                Tag.TagID = Guid.NewGuid();
            }

            Tag.TagName = txtTagname.Text;
            Tag.TagCode = txtTagCode.Text;
            Tag.TagIPAddress = txtIPAddress.Text;
            Tag.TagPort = Convert.ToInt32(nudPort.Value);
            Tag.TagTimeout = Convert.ToInt32(nudTimeout.Value);
            Tag.TagEnabled = ckbTagEnabled.Checked;
        }

        /// <summary>
        /// Sets controls according to the form mode.
        /// <para>Настраивает элементы управления по режиму формы.</para>
        /// </summary>
        private void SetModeControls()
        {
            btnAdd.Visible = ModeWork == 1;
            btnSave.Visible = ModeWork == 2;
        }

        #endregion Basic

        #region Control

        #region Button

        /// <summary>
        /// Adds a tag.
        /// <para>Добавляет тег.</para>
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ControlsToTag(true);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Saves a tag.
        /// <para>Сохраняет тег.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToTag(false);
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Closes the form.
        /// <para>Закрывает форму.</para>
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion Button

        #endregion Control
    }
}
