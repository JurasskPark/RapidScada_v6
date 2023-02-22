using Scada.Forms;
using static Scada.Comm.Drivers.DrvDbImportPlus.Tag;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    public partial class FrmTag : Form
    {

        private int modeWork;
        private Tag tmpTag;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmTag()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public FrmTag(int Mode, ref Tag Tag)
        {
            modeWork = Mode;
            tmpTag = Tag;

            InitializeComponent();

            if (modeWork == 1)
            {
                btnAdd.Visible = true;
                btnSave.Visible = false;
            }

            if (modeWork == 2)
            {
                btnAdd.Visible = false;
                btnSave.Visible = true;
            }

            if (tmpTag == null)
            {
                return;
            }
        }

        /// <summary>
        /// Loading the form
        /// </summary>
        private void FrmTag_Load(object sender, EventArgs e)
        {
            txtTagCode.Text = tmpTag.TagCode;
            txtTagname.Text = tmpTag.TagName;

            try
            {
                cbTagFormat.Items.AddRange(Enum.GetNames(typeof(Tag.FormatTag)));
                if (tmpTag.TagFormat != null)
                {
                    cbTagFormat.SelectedIndex = cbTagFormat.FindString(tmpTag.TagFormat.ToString());
                }
            }
            catch { }

            ckbTagEnabled.Checked = tmpTag.TagEnabled;

            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }

        /// <summary>
        /// Tag Add
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            tmpTag.TagID = Guid.NewGuid();
            tmpTag.TagName = txtTagname.Text;
            tmpTag.TagCode = txtTagCode.Text;
            tmpTag.TagFormat = (FormatTag)cbTagFormat.SelectedIndex;
            tmpTag.TagEnabled = ckbTagEnabled.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Tag Save
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            tmpTag.TagName = txtTagname.Text;
            tmpTag.TagCode = txtTagCode.Text;
            tmpTag.TagFormat = (FormatTag)cbTagFormat.SelectedIndex;
            tmpTag.TagEnabled = ckbTagEnabled.Checked;

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
