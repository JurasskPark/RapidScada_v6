using DrvFtpJP.Shared.FilesDirectorys;
using FluentFTP;
using FluentFTP.Rules;
using FluentFTP.Monitors;
using System.Runtime.InteropServices;
using static DrvFtpJP.Shared.FilesDirectorys.FilesDirectoriesInformation;
using System.IO;


namespace Scada.Comm.Drivers.DrvFtpJP
{
    internal class DriverClient
    {
        public DriverClient()
        {
            this.project = new Project();
            this.client = new FtpClient();
        }

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
        }

        public DriverClient(FtpClientSettings client)
        {
            this.client = new FtpClient();
            this.logger.Level = FtpLogger.LogLevel.Verbose;
            this.host = client.Host;
            this.port = client.Port;
            this.username = client.Username;
            this.password = client.Password;
            this.encryptionMode = client.EncryptionMode;
        }

        #region Variables
        private readonly Project project;                                   // project
        private FtpClient client = new FtpClient();                         // ftp client
        private FtpConfig clientConfig = new FtpConfig();                   // ftp config
        private FtpLogger logger = new FtpLogger();                         // ftp logger
        private List<Scenario> listScenarios = new List<Scenario>();        // list scenario
        private CancellationToken token = new CancellationToken();          // token
        private FtpEncryptionMode encryptionMode = FtpEncryptionMode.None;  // encryption mode
        private string host = string.Empty;                                 // host
        private int port = 21;                                              // port
        private string username = string.Empty;                             // username
        private string password = string.Empty;                             // password
        #endregion Variables

        #region Dispose
        private IntPtr _bufferPtr;
        public int BUFFER_SIZE = 1024 * 1024 * 50; // 50 MB
        private bool _disposed = false;

        ~DriverClient()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // free any other managed objects here.
            }

            // free any unmanaged objects here.
            Marshal.FreeHGlobal(_bufferPtr);
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Dispose

        #region Process

        public void Process()
        {
            foreach(Scenario scenario in listScenarios)
            {
                if(scenario.Enabled == true)
                {
                    foreach(OperationAction operationAction in scenario.Actions)
                    {
                        if(operationAction.Enabled)
                        {
                            switch(operationAction.Operation)
                            {
                                case OperationAction.OperationsActions.None:

                                    break;
                                case OperationAction.OperationsActions.LocalCreateDirectory:
                                    LocalCreateDirectory(operationAction.LocalPath);
                                    break;
                                case OperationAction.OperationsActions.RemoteCreateDirectory:
                                    LocalCreateDirectory(operationAction.RemotePath);
                                    break;
                                case OperationAction.OperationsActions.LocalRename:
                                    if(File.Exists(operationAction.LocalPath))
                                    {
                                        LocalRename(operationAction.LocalPath, operationAction.RemotePath, true);
                                    }
                                    
                                    if(Directory.Exists(operationAction.LocalPath))
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
        public bool Connect()
        {    
            return Connect(out string errMsg);    
        }

        public bool Connect(out string errMsg)
        {
            errMsg = string.Empty;

            try
            { 
                token = new CancellationToken();
                client = new FtpClient(host, username, password, port, clientConfig, logger);
                client.Config.EncryptionMode = encryptionMode;
                client.Config.ValidateAnyCertificate = false;
                client.Logger = logger;
                client.Connect(true);
                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message.ToString();
                return false;
            }
        }
        #endregion Connect

        #region Disconnect
        public bool Disconnect()
        {
            return Disconnect(out string errMsg);
        }

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
                errMsg = ex.Message.ToString();
                return false;
            }
        }
        #endregion Disconnect

        #region Get Listing
        public List<FilesDirectoriesInformation> GetLocalListing(string path = "")
        {
            List<FilesDirectoriesInformation> list = new List<FilesDirectoriesInformation>();
            list = FilesDirectoriesInformation.GetDirectoriesAndFiles(path);
            return list;
        }

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
                    catch { }
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
                    catch { }
                }
            }

            listFiles = listFiles.OrderBy(p => p.Name).ToList();
            foreach (FilesDirectoriesInformation folder in listFiles)
            {
                list.Add(folder);
            }

            return list;
        }

        public List<FtpListItem> GetListing(string path = "")
        {
            List<FtpListItem> paths = new List<FtpListItem>();
            
            if(path == "")
            {
                path = "/";
            }

            // get a list of files and directories in the "/" folder
            paths = client.GetListing(path).ToList();

            return paths;
        }
        #endregion Get Listing

        #region Create Directory
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


        public bool RemoteCreateDirectory(string path, bool force = false)
        {
            return client.CreateDirectory(path, force);
        }

        #endregion Create Directory

        #region Delete Directory
        public bool LocalDeleteDirectory(string path)
        {
            Directory.Delete(path, true);
            return !Directory.Exists(path);
        }


        public bool RemoteDeleteDirectory(string path)
        {
            client.DeleteDirectory(path);
            return !client.DirectoryExists(path);
        }
        #endregion Delete Directory

        #region Delete File
        public bool LocalDeleteFile(string path)
        {
            File.Delete(path);
            return !File.Exists(path);
        }

        public bool RemoteDeleteFile(string path)
        {
            client.DeleteFile(path);
            return !client.FileExists(path);       
        }
        #endregion Delete File

        #region Remote Rename
        public void LocalRename(string pathOld, string pathNew, bool file = false)
        {
            if(file == false)
            {
                LocalRenameDirectory(pathOld, pathNew);
            }
            else
            {
                LocalRenameFile(pathOld, pathNew);
            }
        }

        public void LocalRenameDirectory(string pathOld, string pathNew)
        {
            try
            {
                Directory.Move(pathOld, pathNew);
            }
            catch { }
        }

        public void LocalRenameFile(string pathOld, string pathNew)
        {
            try
            {
                File.Move(pathOld, pathNew);
            }
            catch { }
        }

        public void RemoteRename(string pathOld, string pathNew)
        {
            client.Rename(pathOld, pathNew);
        }

        #endregion Remote Rename

        #region Local Upload
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
            if(maxSizeFile != 0)
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
        /// Getting logs
        /// </summary>
        public static DebugData OnDebug;
        public delegate void DebugData(string msg);
        // transfer to the form and to the file in the Log folder
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
