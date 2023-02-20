using Scada.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Scada.Comm.Drivers.DrvPingJP.Tag;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Scada.Comm.Drivers.DrvPingJP.View.Forms
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
            txtIPAddress.Text = tmpTag.TagIPAddress;
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
            tmpTag.TagIPAddress = txtIPAddress.Text;
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
            tmpTag.TagIPAddress = txtIPAddress.Text;
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
