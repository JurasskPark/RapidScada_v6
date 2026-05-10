using Scada.Forms;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    /// <summary>
    /// Tag editing form.
    /// <para>Форма редактирования тега.</para>
    /// </summary>
    public partial class FrmTag : Form
    {
        /// <summary>
        /// Gets or sets the form operating mode.
        /// </summary>
        public int ModeWork;

        /// <summary>
        /// Gets or sets the tag being edited.
        /// </summary>
        public DriverTag CurrentTag;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmTag()
        {
            InitializeComponent();

            if (CurrentTag == null)
            {
                return;
            }
        }

        /// <summary>
        /// Loads the form.
        /// </summary>
        private void FrmTag_Load(object sender, EventArgs e)
        {
            txtTagCode.Text = CurrentTag.Code;
            txtTagname.Text = CurrentTag.Name;
            txtIPAddress.Text = CurrentTag.IpAddress;
            nudTimeout.Value = Convert.ToDecimal(CurrentTag.Timeout);
            ckbTagEnabled.Checked = CurrentTag.Enabled;

            // translate the form
            FormTranslator.Translate(this, GetType().FullName);

            if (ModeWork == 1)
            {
                btnAdd.Visible = true;
                btnSave.Visible = false;
            }

            if (ModeWork == 2)
            {
                btnAdd.Visible = false;
                btnSave.Visible = true;
            }
        }

        /// <summary>
        /// Adds a tag.
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            CurrentTag.ID = Guid.NewGuid();
            CurrentTag.Name = txtTagname.Text;
            CurrentTag.Code = txtTagCode.Text;
            CurrentTag.IpAddress = txtIPAddress.Text;
            CurrentTag.Timeout = Convert.ToInt32(nudTimeout.Value);
            CurrentTag.Enabled = ckbTagEnabled.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Saves the tag.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            CurrentTag.Name = txtTagname.Text;
            CurrentTag.Code = txtTagCode.Text;
            CurrentTag.IpAddress = txtIPAddress.Text;
            CurrentTag.Timeout = Convert.ToInt32(nudTimeout.Value);
            CurrentTag.Enabled = ckbTagEnabled.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Closes the form.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
