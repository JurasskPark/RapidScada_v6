using Scada.Forms;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    /// <summary>
    /// Input box form.
    /// <para>Форма поля ввода.</para>
    /// </summary>
    public partial class FrmInputBox : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmInputBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the entered value.
        /// </summary>
        public string Values;

        /// <summary>
        /// Confirms the entered value.
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            Values = txtInputbox.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Cancels entering the value.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Translates the form.
        /// </summary>
        private void FrmInputBox_Load(object sender, EventArgs e)
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }
    }
}
