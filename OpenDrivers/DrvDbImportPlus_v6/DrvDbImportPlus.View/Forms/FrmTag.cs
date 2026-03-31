using Scada.Forms;
using static Scada.Comm.Drivers.DrvDbImportPlus.DriverTag;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    public partial class FrmTag : Form
    {

        private int modeWork;
        private DriverTag tmpTag;

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
        public FrmTag(int Mode, ref DriverTag Tag)
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
            txtTagCode.Text = tmpTag.Code;
            txtTagname.Text = tmpTag.Name;

            try
            {
                cbTagFormat.Items.AddRange(Enum.GetNames(typeof(DriverTag.FormatTag)));
                if (tmpTag.Format != null)
                {
                    cbTagFormat.SelectedIndex = cbTagFormat.FindString(tmpTag.Format.ToString());
                }
            }
            catch { }

            nudNumberOfDecimalPlaces.Value = Convert.ToDecimal(tmpTag.NumberDecimalPlaces);
            ckbTagEnabled.Checked = tmpTag.Enabled;

            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }

        /// <summary>
        /// The ability to specify the number of decimal places
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbTagFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormatTag value = (FormatTag)cbTagFormat.SelectedIndex;
            lblNumberOfDecimalPlaces.Visible = true;
            lblMaxNumberCharactersInWord.Visible = false;
            nudNumberOfDecimalPlaces.Maximum = 16;
            switch (value) 
            {
                case FormatTag.Float:
                    nudNumberOfDecimalPlaces.Enabled = true;
                    break;
                case FormatTag.DateTime:
                    nudNumberOfDecimalPlaces.Enabled = false;
                    nudNumberOfDecimalPlaces.Value = 0;
                    break;
                case FormatTag.Integer:
                    nudNumberOfDecimalPlaces.Enabled = false;
                    nudNumberOfDecimalPlaces.Value = 0;
                    break;
                case FormatTag.Boolean:
                    nudNumberOfDecimalPlaces.Enabled = false;
                    nudNumberOfDecimalPlaces.Value = 0;
                    break;
                case FormatTag.String:
                    lblNumberOfDecimalPlaces.Visible = false;
                    lblMaxNumberCharactersInWord.Visible = true;
                    nudNumberOfDecimalPlaces.Enabled = true;
                    nudNumberOfDecimalPlaces.Value = 0;
                    nudNumberOfDecimalPlaces.Maximum = 300;
                    break;
            }
        }

        /// <summary>
        /// Tag Add
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            tmpTag.Id = Guid.NewGuid();
            tmpTag.Name = txtTagname.Text;
            tmpTag.Code = txtTagCode.Text;
            tmpTag.Format = (FormatTag)cbTagFormat.SelectedIndex;
            tmpTag.NumberDecimalPlaces = Convert.ToInt32(nudNumberOfDecimalPlaces.Value);
            tmpTag.Enabled = ckbTagEnabled.Checked;

            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Tag Save
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            tmpTag.Name = txtTagname.Text;
            tmpTag.Code = txtTagCode.Text;
            tmpTag.Format = (FormatTag)cbTagFormat.SelectedIndex;
            tmpTag.NumberDecimalPlaces = Convert.ToInt32(nudNumberOfDecimalPlaces.Value);
            tmpTag.Enabled = ckbTagEnabled.Checked;

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
