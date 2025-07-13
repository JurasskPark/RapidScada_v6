using Scada.Forms;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    public partial class FrmTag : Form
    {

        public int ModeWork;
        public DriverTag Tag;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmTag()
        {
            InitializeComponent();



            if (Tag == null)
            {
                return;
            }
        }

        /// <summary>
        /// Loading the form
        /// </summary>
        private void FrmTag_Load(object sender, EventArgs e)
        {
            txtTagCode.Text = Tag.Code;
            txtTagname.Text = Tag.Name;
            txtIPAddress.Text = Tag.IpAddress;
            nudTimeout.Value = Convert.ToDecimal(Tag.Timeout);
            ckbTagEnabled.Checked = Tag.Enabled;

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
        /// Tag Add
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Tag.ID = Guid.NewGuid();
            Tag.Name = txtTagname.Text;
            Tag.Code = txtTagCode.Text;
            Tag.IpAddress = txtIPAddress.Text;
            Tag.Timeout = Convert.ToInt32(nudTimeout.Value);
            Tag.Enabled = ckbTagEnabled.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Tag Save
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Tag.Name = txtTagname.Text;
            Tag.Code = txtTagCode.Text;
            Tag.IpAddress = txtIPAddress.Text;
            Tag.Timeout = Convert.ToInt32(nudTimeout.Value);
            Tag.Enabled = ckbTagEnabled.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Form Close
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
