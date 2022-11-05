using Scada.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Scada.Comm.Drivers.DrvDbImportPlus.View.Forms
{
    public partial class FrmInputBox : Form
    {
        public FrmInputBox()
        {
            InitializeComponent();
        }

        public string Values;

        private void btnOk_Click(object sender, EventArgs e)
        {
            Values = txtInputbox.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void FrmInputBox_Load(object sender, EventArgs e)
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }
    }
}
