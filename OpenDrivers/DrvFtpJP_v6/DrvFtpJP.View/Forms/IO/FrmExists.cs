namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmExists : Form
    {
        //public XwFileAction DoToFile = XwFileAction.Ask;
        //public XwFileAction DoToAllFiles = XwFileAction.Ask;

        
        /// <summary>
        /// Initializes a new instance of the form.
        /// <para>Инициализирует новый экземпляр формы.</para>
        /// </summary>
        public FrmExists()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// Handles the Cancel_Click event.
        /// <para>Обрабатывает событие Cancel_Click.</para>
        /// </summary>
        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        
        //private void overwriteFile_Click(object sender, EventArgs e)
        //{
        //    DoToFile = XwFileAction.Overwrite;
        //    DialogResult = DialogResult.OK;
        //    Close();
        //}

        
        //private void resumeFile_Click(object sender, EventArgs e)
        //{
        //    DoToFile = XwFileAction.Resume;
        //    DialogResult = DialogResult.OK;
        //    Close();
        //}

        
        //private void skipFile_Click(object sender, EventArgs e)
        //{
        //    DoToFile = XwFileAction.Skip;
        //    DialogResult = DialogResult.OK;
        //    Close();
        //}

        
        //private void overwriteAll_Click(object sender, EventArgs e)
        //{
        //    DoToAllFiles = XwFileAction.Overwrite;
        //    DialogResult = DialogResult.OK;
        //    Close();
        //}

        
        //private void resumeAll_Click(object sender, EventArgs e)
        //{
        //    DoToAllFiles = XwFileAction.Resume;
        //    DialogResult = DialogResult.OK;
        //    Close();
        //}

        
        //private void skipAll_Click(object sender, EventArgs e)
        //{
        //    DoToAllFiles = XwFileAction.Skip;
        //    DialogResult = DialogResult.OK;
        //    Close();
        //}
    }
}
