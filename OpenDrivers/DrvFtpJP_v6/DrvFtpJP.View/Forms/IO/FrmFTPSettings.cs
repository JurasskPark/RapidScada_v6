using FluentFTP;
using Scada.Forms;
using Scada.Lang;

namespace Scada.Comm.Drivers.DrvFtpJP.View.Forms
{
    public partial class FrmFTPSettings : Form
    {
        /// <summary>
        /// Initializes a new instance of the form.
        /// <para>Инициализирует новый экземпляр формы.</para>
        /// </summary>
        public FrmFTPSettings(FtpClientSettings client)
        {
            InitializeComponent();
            this.client = client;         
        }

        #region Basic
        /// <summary>
        /// Loading the form
        /// </summary>
        private void FrmFTPSettings_Load(object sender, EventArgs e)
        {
            ConfigToControls();
            Translate();
        }
        #endregion Basic

        #region Variable
        public FrmConfig formParent;                     // parent form
        public FtpClientSettings client = null;          // ftp client settings
        private DriverClient driverClient;               // driver client
        #endregion Variable

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
            client.EncryptionMode = ckbUseTLS.Checked ? FtpEncryptionMode.Explicit : FtpEncryptionMode.None;

        }

        /// <summary>
        /// Loads FTP data connection types.
        /// <para>Загружает типы соединения данных FTP.</para>
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

        /// <summary>
        /// Handles the ckbDefaultPort_CheckedChanged event.
        /// <para>Обрабатывает событие ckbDefaultPort_CheckedChanged.</para>
        /// </summary>
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

        /// <summary>
        /// Handles the txtPassword_KeyDown event.
        /// <para>Обрабатывает событие txtPassword_KeyDown.</para>
        /// </summary>
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
            }
        }

        /// <summary>
        /// Handles the btnShowPassword_Click event.
        /// <para>Обрабатывает событие btnShowPassword_Click.</para>
        /// </summary>
        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !txtPassword.UseSystemPasswordChar;
        }

        /// <summary>
        /// Handles the btnOpenSshKey_Click event.
        /// <para>Обрабатывает событие btnOpenSshKey_Click.</para>
        /// </summary>
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
        /// <summary>
        /// Handles the btnSave_Click event.
        /// <para>Обрабатывает событие btnSave_Click.</para>
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ControlsToConfig();

            DialogResult = DialogResult.OK;

            Close();
        }
        #endregion Save

        #region Close
        /// <summary>
        /// Handles the btnCancel_Click event.
        /// <para>Обрабатывает событие btnCancel_Click.</para>
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion Close

        #region Connection Test
        /// <summary>
        /// Handles the btnConnectionTest_Click event.
        /// <para>Обрабатывает событие btnConnectionTest_Click.</para>
        /// </summary>
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
