using Scada.Forms;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
{
    public partial class FrmTag : Form
    {

        public int ModeWork;
        public Tag Tag;

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
            txtTagCode.Text = Tag.TagCode;
            txtTagname.Text = Tag.TagName;
            txtIPAddress.Text = Tag.TagIPAddress;
            nudTimeout.Value = Convert.ToDecimal(Tag.TagTimeout);
            ckbTagEnabled.Checked = Tag.TagEnabled;

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
            Tag.TagID = Guid.NewGuid();
            Tag.TagName = txtTagname.Text;
            Tag.TagCode = txtTagCode.Text;
            Tag.TagIPAddress = txtIPAddress.Text;
            Tag.TagTimeout = Convert.ToInt32(nudTimeout.Value);
            Tag.TagEnabled = ckbTagEnabled.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Tag Save
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            Tag.TagName = txtTagname.Text;
            Tag.TagCode = txtTagCode.Text;
            Tag.TagIPAddress = txtIPAddress.Text;
            Tag.TagTimeout = Convert.ToInt32(nudTimeout.Value);
            Tag.TagEnabled = ckbTagEnabled.Checked;

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
