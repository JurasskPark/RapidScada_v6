using Scada.Forms;

namespace Scada.Comm.Drivers.DrvTelnetJP.View.Forms
{
    public partial class FrmInputBox : Form
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmInputBox()
        {
            InitializeComponent();
        }

        public string Values;

        /// <summary>
        /// Сonfirmation of the entered value
        /// </summary>
        private void btnOk_Click(object sender, EventArgs e)
        {
            Values = txtInputbox.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Cancel of the entered value
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Translate the form
        /// </summary>
        private void FrmInputBox_Load(object sender, EventArgs e)
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }
    }
}
