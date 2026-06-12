using Scada.Forms;

namespace Scada.Comm.Drivers.DrvTelnetJP.View.Forms
{
    /// <summary>
    /// Shows a multi-line input box.
    /// <para>Показывает многострочное окно ввода.</para>
    /// </summary>
    public partial class FrmInputBox : Form
    {
        #region Variable

        public string Values = string.Empty;                       // entered values

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FrmInputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the form.
        /// <para>Загружает форму.</para>
        /// </summary>
        private void FrmInputBox_Load(object sender, EventArgs e)
        {
            FormTranslator.Translate(this, GetType().FullName);
        }

        #endregion Basic

        #region Control

        #region Button

        /// <summary>
        /// Confirms the entered value.
        /// <para>Подтверждает введенное значение.</para>
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            Values = txtInputbox.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancels the entered value.
        /// <para>Отменяет введенное значение.</para>
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion Button

        #endregion Control
    }
}
