using FluentFTP;
using Scada;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Represents FTP client settings.
    /// <para>Представляет настройки FTP-клиента.</para>
    /// </summary>
    public class FtpClientSettings
    {
        #region Property

        /// <summary>
        /// Gets or sets the connection name.
        /// <para>Возвращает или задает имя подключения.</para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the FTP host.
        /// <para>Возвращает или задает FTP-хост.</para>
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// <para>Возвращает или задает имя пользователя.</para>
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// <para>Возвращает или задает пароль.</para>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the FTP port.
        /// <para>Возвращает или задает FTP-порт.</para>
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the FTP data connection type.
        /// <para>Возвращает или задает тип FTP-соединения данных.</para>
        /// </summary>
        public FtpDataConnectionType FtpDataType { get; set; }

        /// <summary>
        /// Gets or sets the FTP encryption mode.
        /// <para>Возвращает или задает режим шифрования FTP.</para>
        /// </summary>
        public FtpEncryptionMode EncryptionMode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the connection is favorite.
        /// <para>Возвращает или задает признак избранного подключения.</para>
        /// </summary>
        public bool IsFavorite { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether encryption is enabled in the UI.
        /// <para>Возвращает или задает признак включения шифрования в интерфейсе.</para>
        /// </summary>
        public bool Encryption { get; set; }

        /// <summary>
        /// Gets or sets the SSH key text.
        /// <para>Возвращает или задает текст SSH-ключа.</para>
        /// </summary>
        public string SshKey { get; set; }

        /// <summary>
        /// Gets or sets FluentFTP configuration.
        /// <para>Возвращает или задает конфигурацию FluentFTP.</para>
        /// </summary>
        public FtpConfig Config { get; set; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FtpClientSettings()
        {
            Name = string.Empty;
            Host = "127.0.0.1";
            Username = "ftptest";
            Password = "ftppassword";
            Port = 21;
            FtpDataType = FtpDataConnectionType.AutoPassive;
            EncryptionMode = FtpEncryptionMode.None;
            IsFavorite = false;
            Encryption = false;
            SshKey = string.Empty;
            Config = new FtpConfig();
        }

        /// <summary>
        /// Loads the settings from the XML node.
        /// <para>Загружает настройки из XML-узла.</para>
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            Name = xmlNode.GetChildAsString("Name");
            Host = xmlNode.GetChildAsString("Host");
            Username = xmlNode.GetChildAsString("Username");
            Password = DecryptPassword(xmlNode.GetChildAsString("Password"));
            Port = xmlNode.GetChildAsInt("Port");
            FtpDataType = (FtpDataConnectionType)Enum.Parse(typeof(FtpDataConnectionType), xmlNode.GetChildAsString("FtpDataType"));
            EncryptionMode = (FtpEncryptionMode)Enum.Parse(typeof(FtpEncryptionMode), xmlNode.GetChildAsString("EncryptionMode"));
            IsFavorite = xmlNode.GetChildAsBool("IsFavorite");
            Encryption = xmlNode.GetChildAsBool("Encryption");
            SshKey = xmlNode.GetChildAsString("SshKey");
            Config = new FtpConfig();
        }

        /// <summary>
        /// Saves the settings into the XML node.
        /// <para>Сохраняет настройки в XML-узел.</para>
        /// </summary>
        public void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException(nameof(xmlElem));
            }

            xmlElem.AppendElem("Name", Name);
            xmlElem.AppendElem("Host", Host);
            xmlElem.AppendElem("Username", Username);
            xmlElem.AppendElem("Password", EncryptPassword(Password));
            xmlElem.AppendElem("Port", Port);
            xmlElem.AppendElem("FtpDataType", Enum.GetName(typeof(FtpDataConnectionType), FtpDataType));
            xmlElem.AppendElem("EncryptionMode", Enum.GetName(typeof(FtpEncryptionMode), EncryptionMode));
            xmlElem.AppendElem("IsFavorite", IsFavorite);
            xmlElem.AppendElem("Encryption", Encryption);
            xmlElem.AppendElem("SshKey", SshKey);
        }

        /// <summary>
        /// Encrypts a password before saving.
        /// <para>Шифрует пароль перед сохранением.</para>
        /// </summary>
        private static string EncryptPassword(string password)
        {
            return string.IsNullOrEmpty(password) ? string.Empty : ScadaUtils.Encrypt(password);
        }

        /// <summary>
        /// Decrypts a password after loading.
        /// <para>Расшифровывает пароль после загрузки.</para>
        /// </summary>
        private static string DecryptPassword(string password)
        {
            return string.IsNullOrEmpty(password) ? string.Empty : ScadaUtils.Decrypt(password);
        }

        #endregion Basic
    }
}
