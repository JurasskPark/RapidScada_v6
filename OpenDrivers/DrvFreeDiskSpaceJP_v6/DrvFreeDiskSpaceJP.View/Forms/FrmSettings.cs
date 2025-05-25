using Scada.Forms;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP.View.Forms
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
        }

        #region Variables
        public FrmConfig formParent;                       // parent form
        public Project project;                   // the project configuration
        private bool isRussian;                                 // language

        private bool modified;                                  // the configuration was modified
        #endregion Variables


        private void FrmSettings_Load(object sender, EventArgs e)
        {
            ConfigToControls();
            Translate();
        }


        #region Config 

        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {
            if (formParent.isDll)
            {
                lblLogDays.Visible = false;
                nudLogDays.Visible = false;

                gpbLanguage.Visible = false;
            }

            ckbWriteDriverLog.Checked = project.DebugerSettings.LogWrite;
            nudLogDays.Value = Convert.ToDecimal(project.DebugerSettings.LogDays);

            isRussian = project.LanguageIsRussian;
            cmbLanguage.SelectedIndex = Convert.ToInt32(isRussian);

            Modified = false;
        }

        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ControlsToConfig()
        {
            project.DebugerSettings.LogWrite = ckbWriteDriverLog.Checked;
            project.DebugerSettings.LogDays = Convert.ToInt32(nudLogDays.Value);

            int index = cmbLanguage.SelectedIndex;
            project.LanguageIsRussian = Convert.ToBoolean(index);
        }

        #endregion Config 

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

        #region Modified
        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// </summary>
        private bool Modified
        {
            get
            {
                return modified;
            }
            set
            {
                modified = value;
                btnSave.Enabled = modified;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the configuration was modified.
        /// </summary>
        private void control_Changed(object sender, EventArgs e)
        {
            Modified = true;
        }

        #endregion Modified

        #region Control
        /// <summary>
        /// Specify where the license file is located.
        /// </summary>
        private void btnLicense_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.Title = DriverPhrases.TitleLicense;
            OFD.Filter = DriverPhrases.FilterLicense;
            OFD.InitialDirectory = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath);

            if (OFD.ShowDialog() == DialogResult.OK && OFD.FileName != "")
            {
                txtLicenseFile.Text = OFD.FileName;
                Modified = true;
            }
        }

        /// <summary>
        /// Close the form and save the settings.
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToConfig();
            formParent.SaveData();
            Close();
        }

        /// <summary>
        /// Closing the form without saving settings.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        #endregion Control



    }
}
