using DrvFtpJP.Shared.FilesDirectorys;
using FluentFTP;
using FluentFTP.Monitors;
using FluentFTP.Rules;
using System.Runtime.InteropServices;
using static DrvFtpJP.Shared.FilesDirectorys.FilesDirectoriesInformation;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Implements FTP client operations for driver scenarios.
    /// <para>Выполняет операции FTP-клиента для сценариев драйвера.</para>
    /// </summary>
    internal class DriverClient : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DriverClient()
        {
            this.project = new Project();
            this.client = new FtpClient();
        }

        /// <summary>
        /// Initializes a new instance of the class using project settings.
        /// <para>Инициализирует новый экземпляр класса с настройками проекта.</para>
        /// </summary>
        /// <param name="project">Project settings.</param>
        public DriverClient(Project project)
        {
            this.project = new Project();
            this.client = new FtpClient();
            this.logger.Level = FtpLogger.LogLevel.Info;
            this.project = project;
            this.listScenarios = project.Scenarios;
            this.host = project.FtpClientSettings.Host;
            this.port = project.FtpClientSettings.Port;
            this.username = project.FtpClientSettings.Username;
            this.password = project.FtpClientSettings.Password;
            this.encryptionMode = project.FtpClientSettings.EncryptionMode;
            this.dataConnectionType = project.FtpClientSettings.FtpDataType;
        }

        /// <summary>
        /// Initializes a new instance of the class using FTP client settings.
        /// <para>Инициализирует новый экземпляр класса с настройками FTP-клиента.</para>
        /// </summary>
        /// <param name="client">FTP client settings.</param>
        public DriverClient(FtpClientSettings client)
        {
            this.client = new FtpClient();
            this.logger.Level = FtpLogger.LogLevel.Verbose;
            this.host = client.Host;
            this.port = client.Port;
            this.username = client.Username;
            this.password = client.Password;
            this.encryptionMode = client.EncryptionMode;
            this.dataConnectionType = client.FtpDataType;
        }

        #region Variable
        private readonly Project project;                                   // project
        private FtpClient client = new FtpClient();                         // ftp client
        private FtpConfig clientConfig = new FtpConfig();                   // ftp config
        private FtpLogger logger = new FtpLogger();                         // ftp logger
        private List<Scenario> listScenarios = new List<Scenario>();        // list scenario
        private CancellationToken token = new CancellationToken();          // token
        private FtpEncryptionMode encryptionMode = FtpEncryptionMode.None;  // encryption mode
        private FtpDataConnectionType dataConnectionType = FtpDataConnectionType.AutoPassive; // data connection type
        private string host = string.Empty;                                 // host
        private int port = 21;                                              // port
        private string username = string.Empty;                             // username
        private string password = string.Empty;                             // password
        #endregion Variable

        #region Dispose
        private IntPtr bufferPtr;                                           // buffer pointer
        /// <summary>
        /// Gets or sets the transfer buffer size.
        /// <para>Возвращает или задает размер буфера передачи.</para>
        /// </summary>
        public int BufferSize = 1024 * 1024 * 50;                           // buffer size
        private bool disposed = false;                                      // disposed flag

        ~DriverClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases managed and unmanaged resources.
        /// <para>Освобождает управляемые и неуправляемые ресурсы.</para>
        /// </summary>
        /// <param name="disposing">Indicates whether managed resources should be released.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                // free any other managed objects here.
            }

            // free any unmanaged objects here.
            Marshal.FreeHGlobal(bufferPtr);
            disposed = true;
        }

        /// <summary>
        /// Releases resources used by the client.
        /// <para>Освобождает ресурсы, используемые клиентом.</para>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose

        #region Process

        /// <summary>
        /// Executes enabled scenarios and actions.
        /// <para>Выполняет включенные сценарии и действия.</para>
        /// </summary>
        public void Process()
        {
            foreach (Scenario scenario in listScenarios)
            {
                if (scenario.Enabled)
                {
                    foreach (OperationAction operationAction in scenario.Actions)
                    {
                        if (operationAction.Enabled)
                        {
                            switch (operationAction.Operation)
                            {
                                case OperationAction.OperationsActions.None:

                                    break;
                                case OperationAction.OperationsActions.LocalCreateDirectory:
                                    LocalCreateDirectory(operationAction.LocalPath);
                                    break;
                                case OperationAction.OperationsActions.RemoteCreateDirectory:
                                    RemoteCreateDirectory(operationAction.RemotePath, true);
                                    break;
                                case OperationAction.OperationsActions.LocalRename:
                                    if (File.Exists(operationAction.LocalPath))
                                    {
                                        LocalRename(operationAction.LocalPath, operationAction.RemotePath, true);
                                    }
                                    
                                    if (Directory.Exists(operationAction.LocalPath))
                                    {
                                        LocalRename(operationAction.LocalPath, operationAction.RemotePath, false);
                                    }
                                    break;
                                case OperationAction.OperationsActions.RemoteRename:
                                    RemoteRename(operationAction.LocalPath, operationAction.RemotePath);
                                    break;
                                case OperationAction.OperationsActions.LocalDeleteFile:
                                    if (File.Exists(operationAction.LocalPath))
                                    {
                                        LocalDeleteFile(operationAction.LocalPath);
                                    }
                                    break;
                                case OperationAction.OperationsActions.LocalDeleteDirectory:
                                    if (Directory.Exists(operationAction.LocalPath))
                                    {
                                        LocalDeleteDirectory(operationAction.LocalPath);
                                    }
                                    break;
                                case OperationAction.OperationsActions.RemoteDeleteFile:
                                    RemoteDeleteFile(operationAction.RemotePath);
                                    break;
                                case OperationAction.OperationsActions.RemoteDeleteDirectory:
                                    RemoteDeleteDirectory(operationAction.RemotePath);
                                    break;
                                case OperationAction.OperationsActions.LocalUploadFile:
                                    LocalUploadFile(operationAction.LocalPath, operationAction.RemotePath, operationAction.RemoteExistsMode, operationAction.FtpOptions);
                                    break;
                                case OperationAction.OperationsActions.LocalUploadDirectory:
                                    LocalUploadDirectory(operationAction.LocalPath, operationAction.RemotePath, operationAction.Mode, operationAction.RemoteExistsMode, operationAction.FtpOptions, operationAction.Formats, operationAction.MaxSizeFile);
                                    break;
                                case OperationAction.OperationsActions.RemoteDownloadFile:
                                    RemoteDownloadFile(operationAction.LocalPath, operationAction.RemotePath, operationAction.LocalExistsMode, operationAction.FtpOptions);
                                    break;
                                case OperationAction.OperationsActions.RemoteDownloadDirectory:
                                    RemoteDownloadDirectory(operationAction.LocalPath, operationAction.RemotePath, operationAction.Mode, operationAction.LocalExistsMode, operationAction.FtpOptions, operationAction.Formats, operationAction.MaxSizeFile);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        #endregion Process

        #region Connect
        /// <summary>
        /// Connects to the FTP server.
        /// <para>Подключается к FTP-серверу.</para>
        /// </summary>
        /// <returns>True if the connection is successful.</returns>
        public bool Connect()
        {    
            return Connect(out string errMsg);    
        }

        /// <summary>
        /// Connects to the FTP server and returns an error message on failure.
        /// <para>Подключается к FTP-серверу и возвращает сообщение об ошибке при сбое.</para>
        /// </summary>
        /// <param name="errMsg">Error message.</param>
        /// <returns>True if the connection is successful.</returns>
        public bool Connect(out string errMsg)
        {
            errMsg = string.Empty;

            try
            { 
                token = new CancellationToken();
                client = new FtpClient(host, username, password, port, clientConfig, logger);
                client.Config.EncryptionMode = encryptionMode;
                client.Config.DataConnectionType = dataConnectionType;
                client.Config.ValidateAnyCertificate = false;
                client.Logger = logger;
                client.Connect(true);
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
        #endregion Connect

        #region Disconnect
        /// <summary>
        /// Disconnects from the FTP server.
        /// <para>Отключается от FTP-сервера.</para>
        /// </summary>
        /// <returns>True if disconnection is successful.</returns>
        public bool Disconnect()
        {
            return Disconnect(out string errMsg);
        }

        /// <summary>
        /// Disconnects from the FTP server and returns an error message on failure.
        /// <para>Отключается от FTP-сервера и возвращает сообщение об ошибке при сбое.</para>
        /// </summary>
        /// <param name="errMsg">Error message.</param>
        /// <returns>True if disconnection is successful.</returns>
        public bool Disconnect(out string errMsg)
        {
            errMsg = string.Empty;

            try
            {
                client.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                return false;
            }
        }
        #endregion Disconnect

        #region Get Listing
        /// <summary>
        /// Gets local directory and file listing.
        /// <para>Получает список локальных каталогов и файлов.</para>
        /// </summary>
        /// <param name="path">Local path.</param>
        /// <returns>Local directory and file list.</returns>
        public List<FilesDirectoriesInformation> GetLocalListing(string path = "")
        {
            List<FilesDirectoriesInformation> list = new List<FilesDirectoriesInformation>();
            list = FilesDirectoriesInformation.GetDirectoriesAndFiles(path);
            return list;
        }

        /// <summary>
        /// Gets remote FTP directory and file listing.
        /// <para>Получает список удаленных FTP-каталогов и файлов.</para>
        /// </summary>
        /// <param name="path">Remote path.</param>
        /// <returns>Remote directory and file list.</returns>
        public List<FilesDirectoriesInformation> GetRemoteListing(string path = "")
        {
            List<FilesDirectoriesInformation> list = new List<FilesDirectoriesInformation>();
            List<FilesDirectoriesInformation> listFolder = new List<FilesDirectoriesInformation>();
            List<FilesDirectoriesInformation> listFiles = new List<FilesDirectoriesInformation>();
            List<FtpListItem> itemsFtp = GetListing(path);

            for (int i = 0; i < itemsFtp.Count; i++)
            {
                if (itemsFtp[i].Type == FtpObjectType.Link || itemsFtp[i].Type == FtpObjectType.Directory)
                {
                    try
                    {
                        FilesDirectoriesInformation item = new FilesDirectoriesInformation();
                        item.Name = itemsFtp[i].Name;
                        item.FullName = itemsFtp[i].FullName;
                        item.Type = ConverterObjectType(itemsFtp[i].Type);
                        item.Date = itemsFtp[i].Modified;
                        item.Size = itemsFtp[i].Size;
                        if (item.Type == FilesDirectoriesType.Directory)
                        {
                            item.SizeString = string.Empty;
                        }
                        item.Format = Path.GetExtension(item.Name).TrimStart('.');
                        listFolder.Add(item);
                    }
                    catch
                    {
                    }
                }
            }

            listFolder = listFolder.OrderBy(p => p.Name).ToList();
            foreach (FilesDirectoriesInformation folder in listFolder)
            {
                list.Add(folder);
            }

            for (int i = 0; i < itemsFtp.Count; i++)
            {
                if (itemsFtp[i].Type == FtpObjectType.File)
                {
                    try
                    {
                        FilesDirectoriesInformation item = new FilesDirectoriesInformation();
                        item.Name = itemsFtp[i].Name;
                        item.FullName = itemsFtp[i].FullName;
                        item.Type = ConverterObjectType(itemsFtp[i].Type);
                        item.Date = itemsFtp[i].Modified;
                        item.Size = itemsFtp[i].Size;
                        if (item.Type == FilesDirectoriesType.Directory)
                        {
                            item.SizeString = string.Empty;
                        }
                        item.Format = Path.GetExtension(item.Name).TrimStart('.');
                        listFiles.Add(item);
                    }
                    catch
                    {
                    }
                }
            }

            listFiles = listFiles.OrderBy(p => p.Name).ToList();
            foreach (FilesDirectoriesInformation folder in listFiles)
            {
                list.Add(folder);
            }

            return list;
        }

        /// <summary>
        /// Gets raw FTP listing items.
        /// <para>Получает исходные элементы списка FTP.</para>
        /// </summary>
        /// <param name="path">Remote path.</param>
        /// <returns>FTP listing items.</returns>
        public List<FtpListItem> GetListing(string path = "")
        {
            List<FtpListItem> paths = new List<FtpListItem>();
            
            if (path == string.Empty)
            {
                path = "/";
            }

            // get a list of files and directories in the "/" folder
            paths = client.GetListing(path).ToList();

            return paths;
        }
        #endregion Get Listing

        #region Create Directory
        /// <summary>
        /// Creates a local directory.
        /// <para>Создает локальный каталог.</para>
        /// </summary>
        /// <param name="path">Local directory path.</param>
        /// <returns>True if the directory is created.</returns>
        public bool LocalCreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Creates a remote FTP directory.
        /// <para>Создает удаленный FTP-каталог.</para>
        /// </summary>
        /// <param name="path">Remote directory path.</param>
        /// <param name="force">Create parent directories if required.</param>
        /// <returns>True if the directory is created.</returns>
        public bool RemoteCreateDirectory(string path, bool force = false)
        {
            return client.CreateDirectory(path, force);
        }

        #endregion Create Directory

        #region Delete Directory
        /// <summary>
        /// Deletes a local directory recursively.
        /// <para>Рекурсивно удаляет локальный каталог.</para>
        /// </summary>
        /// <param name="path">Local directory path.</param>
        /// <returns>True if the directory no longer exists.</returns>
        public bool LocalDeleteDirectory(string path)
        {
            Directory.Delete(path, true);
            return !Directory.Exists(path);
        }


        /// <summary>
        /// Deletes a remote FTP directory.
        /// <para>Удаляет удаленный FTP-каталог.</para>
        /// </summary>
        /// <param name="path">Remote directory path.</param>
        /// <returns>True if the directory no longer exists.</returns>
        public bool RemoteDeleteDirectory(string path)
        {
            client.DeleteDirectory(path);
            return !client.DirectoryExists(path);
        }
        #endregion Delete Directory

        #region Delete File
        /// <summary>
        /// Deletes a local file.
        /// <para>Удаляет локальный файл.</para>
        /// </summary>
        /// <param name="path">Local file path.</param>
        /// <returns>True if the file no longer exists.</returns>
        public bool LocalDeleteFile(string path)
        {
            File.Delete(path);
            return !File.Exists(path);
        }

        /// <summary>
        /// Deletes a remote FTP file.
        /// <para>Удаляет удаленный FTP-файл.</para>
        /// </summary>
        /// <param name="path">Remote file path.</param>
        /// <returns>True if the file no longer exists.</returns>
        public bool RemoteDeleteFile(string path)
        {
            client.DeleteFile(path);
            return !client.FileExists(path);       
        }
        #endregion Delete File

        #region Remote Rename
        /// <summary>
        /// Renames a local file or directory.
        /// <para>Переименовывает локальный файл или каталог.</para>
        /// </summary>
        /// <param name="pathOld">Current path.</param>
        /// <param name="pathNew">New path.</param>
        /// <param name="file">Indicates that the path is a file.</param>
        public void LocalRename(string pathOld, string pathNew, bool file = false)
        {
            if (file == false)
            {
                LocalRenameDirectory(pathOld, pathNew);
            }
            else
            {
                LocalRenameFile(pathOld, pathNew);
            }
        }

        /// <summary>
        /// Renames a local directory.
        /// <para>Переименовывает локальный каталог.</para>
        /// </summary>
        /// <param name="pathOld">Current directory path.</param>
        /// <param name="pathNew">New directory path.</param>
        public void LocalRenameDirectory(string pathOld, string pathNew)
        {
            try
            {
                Directory.Move(pathOld, pathNew);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Renames a local file.
        /// <para>Переименовывает локальный файл.</para>
        /// </summary>
        /// <param name="pathOld">Current file path.</param>
        /// <param name="pathNew">New file path.</param>
        public void LocalRenameFile(string pathOld, string pathNew)
        {
            try
            {
                File.Move(pathOld, pathNew);
            }
            catch
            {
            }
        }

        /// <summary>
        /// Renames a remote FTP object.
        /// <para>Переименовывает удаленный FTP-объект.</para>
        /// </summary>
        /// <param name="pathOld">Current remote path.</param>
        /// <param name="pathNew">New remote path.</param>
        public void RemoteRename(string pathOld, string pathNew)
        {
            client.Rename(pathOld, pathNew);
        }

        #endregion Remote Rename

        #region Local Upload
        /// <summary>
        /// Uploads a local directory to a remote FTP directory.
        /// <para>Загружает локальный каталог в удаленный FTP-каталог.</para>
        /// </summary>
        /// <param name="pathLocal">Local directory path.</param>
        /// <param name="pathRemote">Remote target directory path.</param>
        /// <param name="mode">Folder synchronization mode.</param>
        /// <param name="existsMode">Remote file existence mode.</param>
        /// <param name="ftpOptions">FTP verification options.</param>
        /// <param name="formats">Allowed file extensions.</param>
        /// <param name="maxSizeFile">Maximum file size.</param>
        /// <returns>Upload results.</returns>
        public List<FtpResult> LocalUploadDirectory(string pathLocal, string pathRemote,
            FtpFolderSyncMode mode = FtpFolderSyncMode.Update,
            FtpRemoteExists existsMode = FtpRemoteExists.Overwrite,
            FtpVerify ftpOptions = FtpVerify.None,
            List<string> formats = null, long maxSizeFile = 0
            )
        {
            List<FtpResult> results = new List<FtpResult>();
            DebugerFilesReturn debugerFiles = new DebugerFilesReturn();

            List<FtpRule> rules = new List<FtpRule>();
            if (formats != null && formats.Count > 0)
            {
                FtpRule rule = new FtpFileExtensionRule(true, formats);
                rules.Add(rule);
            }
            if (maxSizeFile != 0)
            {
                FtpRule rule = new FtpSizeRule(FtpOperator.LessThan, maxSizeFile);
                rules.Add(rule);
            }

            // define the progress tracking callback
            Action<FtpProgress> progress = delegate (FtpProgress p)
            {
                debugerFiles.Log(p, "->");
            };

            string[] parts = pathLocal.Split(Path.DirectorySeparatorChar);
            string lastDirectoryName = parts[parts.Length - 1];
            string remoteNewDirectory = Path.Combine(pathRemote, lastDirectoryName).Replace("\\", "/");
            RemoteCreateDirectory(remoteNewDirectory, true);

            if (formats == null || maxSizeFile == 0)
            {
                results = client.UploadDirectory(pathLocal, remoteNewDirectory, mode, existsMode, ftpOptions, null, progress);
            }
            else
            {
                results = client.UploadDirectory(pathLocal, remoteNewDirectory, mode, existsMode, ftpOptions, rules, progress);
            }

            return results;
        }

        /// <summary>
        /// Uploads a local file to a remote FTP directory.
        /// <para>Загружает локальный файл в удаленный FTP-каталог.</para>
        /// </summary>
        /// <param name="pathLocal">Local file path.</param>
        /// <param name="pathRemote">Remote target directory path.</param>
        /// <param name="existsMode">Remote file existence mode.</param>
        /// <param name="ftpOptions">FTP verification options.</param>
        /// <returns>Upload status.</returns>
        public FtpStatus LocalUploadFile(string pathLocal, string pathRemote,
            FtpRemoteExists existsMode = FtpRemoteExists.Overwrite,
            FtpVerify ftpOptions = FtpVerify.None
            )
        {
            FtpStatus result = new FtpStatus();
            DebugerFilesReturn debugerFiles = new DebugerFilesReturn();

            // define the progress tracking callback
            Action<FtpProgress> progress = delegate (FtpProgress p)
            {
                debugerFiles.Log(p, "->");
            };

            string[] parts = pathLocal.Split(Path.DirectorySeparatorChar);
            string lastDirectoryName = parts[parts.Length - 1];
            string RemoteNewFile = Path.Combine(pathRemote, lastDirectoryName).Replace("\\", "/");

            // download a file with progress tracking
            result = client.UploadFile(pathLocal, RemoteNewFile, existsMode, false, ftpOptions, progress);
            return result;
        }

        #endregion Local Upload

        #region Remote Download
        /// <summary>
        /// Downloads a remote FTP directory to a local directory.
        /// <para>Скачивает удаленный FTP-каталог в локальный каталог.</para>
        /// </summary>
        /// <param name="pathLocal">Local target directory path.</param>
        /// <param name="pathRemote">Remote directory path.</param>
        /// <param name="mode">Folder synchronization mode.</param>
        /// <param name="existsMode">Local file existence mode.</param>
        /// <param name="ftpOptions">FTP verification options.</param>
        /// <param name="formats">Allowed file extensions.</param>
        /// <param name="maxSizeFile">Maximum file size.</param>
        /// <returns>Download results.</returns>
        public List<FtpResult> RemoteDownloadDirectory(string pathLocal, string pathRemote,
            FtpFolderSyncMode mode = FtpFolderSyncMode.Update, 
            FtpLocalExists existsMode = FtpLocalExists.Overwrite,
            FtpVerify ftpOptions = FtpVerify.None,
            List<string> formats = null, long maxSizeFile = 0
            )
        {
            List<FtpResult> results = new List<FtpResult>();
            DebugerFilesReturn debugerFiles = new DebugerFilesReturn();

            List<FtpRule> rules = new List<FtpRule>();
            if (formats != null && formats.Count > 0)
            {
                FtpRule rule = new FtpFileExtensionRule(true, formats);
                rules.Add(rule);
            }
            if (maxSizeFile != 0)
            {
                FtpRule rule = new FtpSizeRule(FtpOperator.LessThan, maxSizeFile);
                rules.Add(rule);
            }

            // define the progress tracking callback
            Action<FtpProgress> progress = delegate (FtpProgress p) 
            {
                debugerFiles.Log(p, "<-");
            };

            string[] parts = pathRemote.Split(Path.AltDirectorySeparatorChar);
            string lastDirectoryName = parts[parts.Length - 1];
            string localNewDirectory = Path.Combine(pathLocal, lastDirectoryName);
            LocalCreateDirectory(localNewDirectory);

            if (formats == null || maxSizeFile == 0)
            {
                results = client.DownloadDirectory(localNewDirectory, pathRemote, mode, existsMode, ftpOptions, null, progress);
            }
            else
            {
                results = client.DownloadDirectory(localNewDirectory, pathRemote, mode, existsMode, ftpOptions, rules, progress);
            }

            return results;
        }

        /// <summary>
        /// Downloads a remote FTP file to a local directory.
        /// <para>Скачивает удаленный FTP-файл в локальный каталог.</para>
        /// </summary>
        /// <param name="pathLocal">Local target directory path.</param>
        /// <param name="pathRemote">Remote file path.</param>
        /// <param name="existsMode">Local file existence mode.</param>
        /// <param name="ftpOptions">FTP verification options.</param>
        /// <returns>Download status.</returns>
        public FtpStatus RemoteDownloadFile(string pathLocal, string pathRemote,
            FtpLocalExists existsMode = FtpLocalExists.Overwrite,
            FtpVerify ftpOptions = FtpVerify.None
            )
        {
            FtpStatus result = new FtpStatus();
            DebugerFilesReturn debugerFiles = new DebugerFilesReturn();

            // define the progress tracking callback
            Action<FtpProgress> progress = delegate (FtpProgress p)
            {
                debugerFiles.Log(p, "<-");
            };

            string[] parts = pathRemote.Split(Path.AltDirectorySeparatorChar);
            string lastDirectoryName = parts[parts.Length - 1];
            string LocalNewFile = Path.Combine(pathLocal, lastDirectoryName);

            // download a file with progress tracking
            result = client.DownloadFile(LocalNewFile, pathRemote, existsMode, ftpOptions, progress);
            return result;
        }

        #endregion Remote Download

        #region UnixParentPath

        /// <summary>
        /// Gets the parent path in Unix format.
        /// <para>Получает родительский путь в Unix-формате.</para>
        /// </summary>
        /// <param name="path">Source path.</param>
        /// <returns>Parent path.</returns>
        public static string GetUnixParentPath(string path)
        {
            // Проверяем, что путь не пустой
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("The path cannot be empty!");
            }

            // Преобразуем путь в Unix-формат
            path = path.Replace("\\", "/");

            // Удаляем завершающую косую черту, если есть
            path = path.TrimEnd('/');

            // Если это корневой путь
            if (path == "/")
            {
                return "/";
            }

            // Находим последнюю косую черту
            int lastSlashIndex = path.LastIndexOf('/');

            // Если путь состоит только из корня, возвращаем его
            if (lastSlashIndex == -1)
            {
                return "/";
            }

            // Если путь начинается с корня
            if (lastSlashIndex == 0)
            {
                return "/";
            }

            // Возвращаем родительский путь
            path = path.Substring(0, lastSlashIndex);
            return path;
        }

        #endregion UnixParentPath

        #region Converter Type

        /// <summary>
        /// Converts FTP object type to driver file system object type.
        /// <para>Преобразует тип FTP-объекта в тип объекта файловой системы драйвера.</para>
        /// </summary>
        /// <param name="ftpObjectType">FTP object type.</param>
        /// <returns>Driver file system object type.</returns>
        public static FilesDirectoriesType ConverterObjectType(FtpObjectType ftpObjectType)
        {
            switch (ftpObjectType)
            {
                case FtpObjectType.File:
                    return FilesDirectoriesInformation.FilesDirectoriesType.File;
                case FtpObjectType.Directory:
                    return FilesDirectoriesInformation.FilesDirectoriesType.Directory;
                case FtpObjectType.Link:
                    return FilesDirectoriesInformation.FilesDirectoriesType.Link;
            }

            return FilesDirectoriesType.None;
        }

        #endregion Converter Type

        #region Log
        /// <summary>
        /// Occurs when debug data is available.
        /// <para>Возникает при появлении отладочных данных.</para>
        /// </summary>
        public static DebugData OnDebug;
        /// <summary>
        /// Represents a debug data callback.
        /// <para>Представляет callback отладочных данных.</para>
        /// </summary>
        /// <param name="msg">Debug message.</param>
        public delegate void DebugData(string msg);
        // transfer to the form and to the file in the Log folder
        /// <summary>
        /// Sends a debug message to subscribers.
        /// <para>Передает отладочное сообщение подписчикам.</para>
        /// </summary>
        /// <param name="text">Debug message.</param>
        internal void DebugerLog(string text)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(text);
        }

        #endregion Log

    }
}
