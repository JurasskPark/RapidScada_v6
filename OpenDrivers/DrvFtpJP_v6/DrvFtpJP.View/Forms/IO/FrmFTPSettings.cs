using FluentFTP;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmFTPSettings : Form
    {
        public FrmFTPSettings(FtpClientSettings client)
        {
            InitializeComponent();
            this.client = client;         
        }

        #region Form Load
        /// <summary>
        /// Loading the form
        /// </summary>
        private void FrmFTPSettings_Load(object sender, EventArgs e)
        {
            ConfigToControls();
            Translate();
        }
        #endregion Form Load

        #region Variables
        public FrmConfig formParent;                     // parent form
        public FtpClientSettings client = null;
        private DriverClient driverClient;
        #endregion Variables

        #region Config
        /// <summary>
        /// Sets the controls according to the configuration.
        /// </summary>
        private void ConfigToControls()
        {
            // ftp client
            txtName.Text = client.Name;
            txtHost.Text = client.Host;
            txtUsername.Text = client.Username;
            txtPassword.Text = client.Password;
            txtSshKey.Text = client.SshKey;
            ckbUseTLS.Checked = client.Encryption;

            if (client.Port == 21 || client.Port == 0)
            {
                ckbDefaultPort.Checked = true;
                nudPort.Enabled = false;
                nudPort.Value = 21;
            }
            else
            {
                ckbDefaultPort.Checked = false;
                nudPort.Enabled = true;
                nudPort.Value = client.Port;
            }
            LoadFtpDataType(client.FtpDataType);
        }

        /// <summary>
        /// Sets the configuration parameters according to the controls.
        /// </summary>
        private void ControlsToConfig()
        {
            // ftp client
            client.Name = txtName.Text;
            client.Host = txtHost.Text;
            client.Username = txtUsername.Text;
            client.Password = txtPassword.Text;
            client.Port = Convert.ToInt32(nudPort.Value);

            int index = cmbFtpDataType.SelectedIndex;
            switch (index)
            {
                case 0:

                    client.FtpDataType = FtpDataConnectionType.PASV;
                    break;
                case 1:
                    client.FtpDataType = FtpDataConnectionType.PASVEX;
                    break;
                case 2:
                    client.FtpDataType = FtpDataConnectionType.AutoActive;
                    break;

            }

            client.SshKey = txtSshKey.Text;
            client.Encryption = ckbUseTLS.Checked;

        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadFtpDataType(FtpDataConnectionType type)
        {
            cmbFtpDataType.Items.Clear();
            cmbFtpDataType.Items.Add(Locale.IsRussian ? "Пассивный - Автоматический - Рекомендуется" : "Passive - Auto - Recommended");
            cmbFtpDataType.Items.Add(Locale.IsRussian ? "Пассивный — Игнорировать маршрутизацию" : "Passive - Ignore routing info");
            cmbFtpDataType.Items.Add(Locale.IsRussian ? "Активный" : "Active");

            switch ((FtpDataConnectionType)type)
            {
                case FtpDataConnectionType.AutoPassive:
                case FtpDataConnectionType.PASV:
                    cmbFtpDataType.SelectedIndex = 0;
                    break;
                case FtpDataConnectionType.PASVEX:
                    cmbFtpDataType.SelectedIndex = 1;
                    break;
                case FtpDataConnectionType.AutoActive:
                    cmbFtpDataType.SelectedIndex = 2;
                    break;
            }
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

        #region Control

        private void ckbDefaultPort_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbDefaultPort.Checked)
            {
                nudPort.Enabled = false;
                nudPort.Value = 21;
            }
            else
            {
                nudPort.Enabled = true;
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            }
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        private void btnOpenSshKey_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                txtSshKey.Text = File.ReadAllText(open.FileName);
            }
        }
        #endregion Control

        #region Button

        #region Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToConfig();

            DialogResult = DialogResult.OK;

            Close();
        }
        #endregion Save

        #region Close
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion Close

        #region Connection Test
        private void btnConnectionTest_Click(object sender, EventArgs e)
        {
            ControlsToConfig();

            driverClient = new DriverClient(client);
            if (driverClient.Connect(out string errMsg))
            {
                MessageBox.Show(Locale.IsRussian ?
                                "Соединение успешно прошло!" :
                                "The connection was successful!");
            }
            else
            {
                if (errMsg != string.Empty)
                {
                    MessageBox.Show(errMsg);
                }
            }
        }
        #endregion Connection Test

        #endregion Button

    }
}
