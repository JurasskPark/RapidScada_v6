using FluentFTP;
using Scada;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Represents a scenario operation action.
    /// <para>Представляет действие сценария.</para>
    /// </summary>
    public class OperationAction
    {
        #region Property

        /// <summary>
        /// Specifies supported operation actions.
        /// <para>Задает поддерживаемые действия операций.</para>
        /// </summary>
        public enum OperationsActions : int
        {
            None = 0,
            LocalCreateDirectory = 1,
            RemoteCreateDirectory = 2,
            LocalRename = 3,
            RemoteRename = 4,
            LocalDeleteFile = 5,
            LocalDeleteDirectory = 6,
            RemoteDeleteFile = 7,
            RemoteDeleteDirectory = 8,
            LocalUploadFile = 9,
            LocalUploadDirectory = 10,
            RemoteDownloadFile = 11,
            RemoteDownloadDirectory = 12,
        }

        /// <summary>
        /// Gets or sets the action identifier.
        /// <para>Возвращает или задает идентификатор действия.</para>
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the action is enabled.
        /// <para>Возвращает или задает признак включения действия.</para>
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the operation type.
        /// <para>Возвращает или задает тип операции.</para>
        /// </summary>
        public OperationsActions Operation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the action works with a file.
        /// <para>Возвращает или задает признак работы действия с файлом.</para>
        /// </summary>
        public bool IsFile { get; set; }

        /// <summary>
        /// Gets or sets the local path.
        /// <para>Возвращает или задает локальный путь.</para>
        /// </summary>
        public string LocalPath { get; set; }

        /// <summary>
        /// Gets or sets the remote path.
        /// <para>Возвращает или задает удаленный путь.</para>
        /// </summary>
        public string RemotePath { get; set; }

        /// <summary>
        /// Gets or sets the folder synchronization mode.
        /// <para>Возвращает или задает режим синхронизации каталогов.</para>
        /// </summary>
        public FtpFolderSyncMode Mode { get; set; }

        /// <summary>
        /// Gets or sets behavior when a remote object exists.
        /// <para>Возвращает или задает поведение при существующем удаленном объекте.</para>
        /// </summary>
        public FtpRemoteExists RemoteExistsMode { get; set; }

        /// <summary>
        /// Gets or sets behavior when a local object exists.
        /// <para>Возвращает или задает поведение при существующем локальном объекте.</para>
        /// </summary>
        public FtpLocalExists LocalExistsMode { get; set; }

        /// <summary>
        /// Gets or sets FTP verification options.
        /// <para>Возвращает или задает параметры проверки FTP.</para>
        /// </summary>
        public FtpVerify FtpOptions { get; set; }

        /// <summary>
        /// Gets or sets file extension filters.
        /// <para>Возвращает или задает фильтры расширений файлов.</para>
        /// </summary>
        public List<string> Formats { get; set; }

        /// <summary>
        /// Gets or sets the maximum file size.
        /// <para>Возвращает или задает максимальный размер файла.</para>
        /// </summary>
        public long MaxSizeFile { get; set; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public OperationAction()
        {
            ID = Guid.NewGuid();
            Operation = OperationsActions.None;
            IsFile = false;
            Enabled = true;
            LocalPath = string.Empty;
            RemotePath = string.Empty;
            Mode = FtpFolderSyncMode.Update;
            RemoteExistsMode = FtpRemoteExists.Skip;
            LocalExistsMode = FtpLocalExists.Skip;
            FtpOptions = FtpVerify.None;
            Formats = new List<string>();
            MaxSizeFile = 0;
        }

        /// <summary>
        /// Loads the action from the XML node.
        /// <para>Загружает действие из XML-узла.</para>
        /// </summary>
        public void LoadFromXml(XmlNode xmlNode)
        {
            if (xmlNode == null)
            {
                throw new ArgumentNullException(nameof(xmlNode));
            }

            ID = DriverUtils.StringToGuid(xmlNode.GetChildAsString("ID"));
            Enabled = xmlNode.GetChildAsBool("Enabled");
            Operation = (OperationsActions)Enum.Parse(typeof(OperationsActions), xmlNode.GetChildAsString("Operation"));
            LocalPath = xmlNode.GetChildAsString("LocalPath");
            RemotePath = xmlNode.GetChildAsString("RemotePath");
            Mode = (FtpFolderSyncMode)Enum.Parse(typeof(FtpFolderSyncMode), xmlNode.GetChildAsString("Mode"));
            RemoteExistsMode = (FtpRemoteExists)Enum.Parse(typeof(FtpRemoteExists), xmlNode.GetChildAsString("RemoteExistsMode"));
            LocalExistsMode = (FtpLocalExists)Enum.Parse(typeof(FtpLocalExists), xmlNode.GetChildAsString("LocalExistsMode"));
            FtpOptions = (FtpVerify)Enum.Parse(typeof(FtpVerify), xmlNode.GetChildAsString("FtpOptions"));
            Formats = xmlNode.GetChildAsString("Formats").Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
            MaxSizeFile = Convert.ToInt64(xmlNode.GetChildAsString("MaxSizeFile"));
        }

        /// <summary>
        /// Saves the action into the XML node.
        /// <para>Сохраняет действие в XML-узел.</para>
        /// </summary>
        public virtual void SaveToXml(XmlElement xmlElem)
        {
            if (xmlElem == null)
            {
                throw new ArgumentNullException(nameof(xmlElem));
            }

            xmlElem.AppendElem("ID", ID);
            xmlElem.AppendElem("Enabled", Enabled);
            xmlElem.AppendElem("Operation", Enum.GetName(typeof(OperationsActions), Operation));
            xmlElem.AppendElem("LocalPath", LocalPath);
            xmlElem.AppendElem("RemotePath", RemotePath);
            xmlElem.AppendElem("Mode", Enum.GetName(typeof(FtpFolderSyncMode), Mode));
            xmlElem.AppendElem("RemoteExistsMode", Enum.GetName(typeof(FtpRemoteExists), RemoteExistsMode));
            xmlElem.AppendElem("LocalExistsMode", Enum.GetName(typeof(FtpLocalExists), LocalExistsMode));
            xmlElem.AppendElem("FtpOptions", Enum.GetName(typeof(FtpVerify), FtpOptions));
            xmlElem.AppendElem("Formats", string.Join(", ", Formats));
            xmlElem.AppendElem("MaxSizeFile", MaxSizeFile);
        }

        #endregion Basic
    }
}
