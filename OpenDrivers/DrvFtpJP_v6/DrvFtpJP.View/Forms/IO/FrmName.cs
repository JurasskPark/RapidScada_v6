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

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmName : Form
    {
        #region Variables
        public bool isEditName;
        public string FileName { get; set; }
        #endregion Variables

        public FrmName()
        {
            InitializeComponent();
            this.Load += NameForm_Load;
            this.FormClosing += NameForm_FormClosing;
        }

        
        public FrmName(string n) : this()
        {
            FileName = n;
            isEditName = true;
        }
        
        #region Form Load
        private void NameForm_Load(object sender, EventArgs e)
        {
            Translate();

            if (isEditName) 
            { 
                txtName.Text = FileName; 
            }
        }
        #endregion Form Load

        #region Form Closing
        private void NameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (txtName.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Введите имя", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                this.FileName = this.txtName.Text.Trim();
            }
        }
        #endregion Form Closing

        #region Translate
        /// <summary>
        /// Translation of the form.
        /// <para>Перевод формы.</para>
        /// </summary>
        private void Translate()
        {
            // translate the form
            FormTranslator.Translate(this, GetType().FullName);
        }
        #endregion Translate
    }
}