using Database;
using Lang;
using MES.Shared;
using MES.Shared.Animation;
using Parser.ParserText;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MES.Service
{
    /// <summary>
    /// The class provides helper methods for the entire software package.
    /// <para>Класс, предоставляющий функции за контролем каталога на наличие новых, измененных, удаленных файлов.</para>
    /// </summary>
    public class FileWatcher : IDisposable
    {
        #region Variables

        FileSystemWatcher Watcher = new FileSystemWatcher();                        // watcher // наблюдатель
        readonly object Obj = new object();

        bool WatcherProcessing = false;                                             // sign of work // признак работы

        string Name = string.Empty;                                                 // device name // название устройства
        string PathToWatchFolder = string.Empty;                                    // watch directory // каталог наблюдения
        bool UsePathSubDir = false;                                                 // subdirectories are used // используются подкаталоги  
        string Filter = string.Empty;                                               // data selection filter // фильтр отбора данных        
        string TemplatefileName = string.Empty;                                     // name template // шаблон имени
        string ScriptSelect = string.Empty;                                         // data select script // скрипт выборки данных
        string ScriptInsert = string.Empty;                                         // data insert script // скрипт вставки данных
        string ScriptUpdate = string.Empty;                                         // data update script // скрипт обновления данных
        string ScriptDelete = string.Empty;                                         // data delete script // скрипт удаления данных
        string ScriptRename = string.Empty;                                         // data rename script // скрипт переименовывания данных
        string ScriptSynchronization = string.Empty;                                // data synchronization script // скрипт синхронизации данных
        string ScriptParserSelect = string.Empty;                                   // script data selection for parsing // скрипт выборка данных для парсинга
        string ScriptParserInsert = string.Empty;                                   // script for inserting parsed data // скрипт для вставки спарсенных данных
        ParserTextSettings ParserSettings = new ParserTextSettings();               // settings for parsing // настройки для парсинга
        ParserTextDictonary ParserDictonary = new ParserTextDictonary();            // dictionary for parsing (tags, field names, etc.) // словарь для парсинга (теги, названия полей ит.д.)

        #endregion Variables

        #region Void / Event
        public delegate void FswFileChanged(string str, DateTime lastWriteTime);    // file change event // событие изменения файла
        public event FswFileChanged FileChanged;

        public delegate void FswFileDeleted(string str);                            // file deletion event // событие удаления файла
        public event FswFileDeleted FileDeleted;

        public delegate void FswFileRenamed(string str);                            // file rename event // событие переименовывания файла
        public event FswFileRenamed FileRenamed;

        public delegate void FswDisconnected(string str);                           // disconnect event // событие отключения
        public event FswDisconnected Disconnected;

        #endregion Void / Event

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FileWatcher()
        {

        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FileWatcher(string name, string pathFolder, string filter, string templateFileName, bool useSubDir = false,
            string scriptSelect = "", string scriptInsert = "", string scriptUpdate = "", string scriptDelete = "", 
            string scriptRename = "", string scriptSynchronization = "", 
            string scriptParserSelect = "", string scriptParserInsert = "", ParserTextSettings parserSettings = null, ParserTextDictonary parserDictonary = null)
        {
            this.Name = name;
            this.PathToWatchFolder = pathFolder;
            this.UsePathSubDir = useSubDir;
            this.Filter = filter;
            this.TemplatefileName = templateFileName;
            this.ScriptSelect = scriptSelect;
            this.ScriptInsert = scriptInsert;
            this.ScriptUpdate = scriptUpdate;
            this.ScriptDelete = scriptDelete;
            this.ScriptRename = scriptRename;
            this.ScriptSynchronization = scriptSynchronization;
            this.ScriptParserSelect = scriptParserSelect;
            this.ScriptParserInsert = scriptParserInsert;
            this.ParserSettings = parserSettings;
            this.ParserDictonary = parserDictonary;
        }

        #region Dispose
        private IntPtr _bufferPtr;
        public int BUFFER_SIZE = 1024 * 1024 * 50; // 50 MB
        private bool _disposed = false;

        /// <summary>
        /// Dispose an instance of a class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        ~FileWatcher()
        {
            Dispose(false);
        }

        /// <summary>
        /// Dispose an instance of a class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
            }

            // Free any unmanaged objects here.
            Marshal.FreeHGlobal(_bufferPtr);
            _disposed = true;
        }

        /// <summary>
        /// Dispose an instance of a class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion Dispose

        #region Start
        /// <summary>
        /// Start directory monitoring.
        /// <para>Запуск отслеживания за каталогом.</para>
        /// </summary>
        public void Start()
        {
            while (true)
            {
                try
                {
                    Debuger.Log(Locale.IsRussian ?
                    @$"Каталог наблюдения {PathToWatchFolder}" :
                    @$"Watching directory {PathToWatchFolder}");

                    Watcher = new FileSystemWatcher(PathToWatchFolder)
                    {
                        IncludeSubdirectories = true,
                        NotifyFilter = NotifyFilters.LastAccess
                        | NotifyFilters.LastWrite
                        | NotifyFilters.FileName,

                        InternalBufferSize = 65536
                    };

                    Watcher.Filter = Filter;

                    Watcher.Created += Watcher_Created;
                    Watcher.Deleted += Watcher_Deleted;
                    
                    Watcher.Changed += Watcher_Changed;
                    Watcher.Renamed += Watcher_Renamed;

                    Watcher.Error += Watcher_Error;

                    Watcher.EnableRaisingEvents = true;

                    Task.Run(() =>
                    {
                        while (true)
                        {

                            if (Watcher.EnableRaisingEvents == false && WatcherProcessing == false)
                            {
                                try
                                {
                                    Debuger.Log(Locale.IsRussian ?
                                    @$"Следующий" :
                                    @$"Next");

                                    Watcher.EnableRaisingEvents = true;
                                }
                                catch (Exception ex)
                                {
                                    Watcher.EnableRaisingEvents = false;
                                    Disconnected?.Invoke(ex.Message);

                                    string errMsg = @$"{ex.Message} {Watcher.EnableRaisingEvents}";
                                    Debuger.Log(Locale.IsRussian ?
                                    @$"[Ошибка] {errMsg}" :
                                    @$"[Error] {errMsg}");
                                }
                            }
                            Thread.Sleep(5000);

                        }
                    });

                    Debuger.Log(Locale.IsRussian ?
                    @$"Инициировано наблюдение за каталогом {PathToWatchFolder}" :
                    @$"Directory monitoring initiated {PathToWatchFolder}");

                    while (true)
                    {                        
                        // synchronization
                        Synchronization();
                        // parsing
                        FileWithChanges fileWithChanges = new FileWithChanges();
                        fileWithChanges.Parsing(ScriptParserSelect, ScriptParserInsert, ParserSettings, ParserDictonary);
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    string errMsg = @$"{ex.Message}";
                    Debuger.Log(Locale.IsRussian ?
                    @$"[Ошибка] {errMsg}" :
                    @$"[Error] {errMsg}");

                    Thread.Sleep(5000);
                }
            }
        }
        #endregion Start

        #region Stop
        /// <summary>
        /// Stop monitoring the directory.
        /// <para>Останов слежения за каталогом.</para>
        /// </summary>
        public void Stop()
        {
            ((IDisposable)Watcher).Dispose();
        }
        #endregion Stop

        #region Process
        /// <summary>
        /// Process monitoring the directory.
        /// <para>Процесс слежения за каталогом.</para>
        /// </summary>
        public void Process()
        {
            // synchronization
            Synchronization();
            // parsing
            FileWithChanges fileWithChanges = new FileWithChanges();
            fileWithChanges.Parsing(ScriptParserSelect, ScriptParserInsert, ParserSettings, ParserDictonary);
        }
        #endregion Process

        #region Synchronization
        /// <summary>
        /// File synchronization.
        /// <para>Синхронизация файлов.</para>
        /// </summary>
        public void Synchronization()
        {
            FileWithChanges fileWithChanges = new FileWithChanges();
            fileWithChanges.Synchronization(PathToWatchFolder, UsePathSubDir, Name, TemplatefileName, ScriptSynchronization, ScriptSelect, ScriptInsert, ScriptUpdate,ScriptDelete, ScriptRename, true, true);
        }
        #endregion Synchronization

        #region Watcher_Created
        /// <summary>
        /// Creating files
        /// <para>Cоздание файлов</para>
        /// </summary>
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            WatcherProcessing = true;
            Watcher.EnableRaisingEvents = false;

            string fileEvent = "CREATED";
            string fullPath = e.FullPath;
            string fullPathToUpper = fullPath;

            if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(fullPath);
                FileChanged?.Invoke(fullPath, lastWriteTime);

                FileWithChanges fileWithChanges = new FileWithChanges();
                fileWithChanges.RecordEntry(fileEvent, fullPath, lastWriteTime.ToString("s"), "", ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename);
            }

            Watcher.EnableRaisingEvents = true;
            WatcherProcessing = false;

        }
        #endregion Watcher_Created

        #region Watcher_Deleted
        /// <summary>
        /// Deleting files
        /// <para>Удаление файлов</para>
        /// </summary>
        private void Watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            WatcherProcessing = true;
            Watcher.EnableRaisingEvents = false;

            const string fileEvent = "DELETED";
            string fullPath = e.FullPath;
            string fullPathToUpper = fullPath;

            if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName))
            {
                FileDeleted?.Invoke(fullPath);

                FileWithChanges fileWithChanges = new FileWithChanges();
                fileWithChanges.RecordEntry(fileEvent, fullPath, "", "", ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename);
            }

            Watcher.EnableRaisingEvents = true;
            WatcherProcessing = false;
        }
        #endregion Watcher_Deleted

        #region  Watcher_Changed
        /// <summary>
        /// Changing files
        /// <para>Изменение файлов</para>
        /// </summary>
        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            WatcherProcessing = true;
            Watcher.EnableRaisingEvents = false;

            const string fileEvent = "CHANGED";
            string fullPath = e.FullPath;

            string fullPathToUpper = fullPath;
            if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(fullPath);
                FileChanged?.Invoke(fullPath, lastWriteTime);

                FileWithChanges fileWithChanges = new FileWithChanges();
                fileWithChanges.RecordEntry(fileEvent, fullPath, lastWriteTime.ToString("s"), "", ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename);
            }

            Watcher.EnableRaisingEvents = true;
            WatcherProcessing = false;
        }
        #endregion  Watcher_Changed

        #region Watcher_Renamed
        /// <summary>
        /// Renaming files
        /// <para>Переименование файлов</para>
        /// </summary>
        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            WatcherProcessing = true;
            Watcher.EnableRaisingEvents = false;
            string fileEvent = "RENAMED";
            string fullPath = e.FullPath;
            string oldFilePath = e.OldFullPath;

            string fullPathToUpper = fullPath;

            if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName))
            {
                DateTime lastWriteTime = File.GetLastWriteTime(fullPath);

                FileWithChanges fileWithChanges = new FileWithChanges();
                fileWithChanges.RecordEntry(fileEvent, fullPath, lastWriteTime.ToString("s"), oldFilePath, ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename);
            }

            Watcher.EnableRaisingEvents = true;
            WatcherProcessing = false;
        }
        #endregion Watcher_Renamed

        #region Watcher_Error
        /// <summary>
        /// Error event.
        /// <para>Событие о ошибке.</para>
        /// </summary>
        private static void Watcher_Error(object sender, ErrorEventArgs e) => PrintException(e.GetException());

        /// <summary>
        /// Recording error information.
        /// <para>Запись информации о ошибке.</para>
        /// </summary>
        private static void PrintException(Exception? ex)
        {
            if (ex != null)
            {
                Debuger.Log($"Message: {ex.Message}");
                Debuger.Log("Stacktrace:");
                Debuger.Log(ex.StackTrace);
                PrintException(ex.InnerException);
            }
        }
        #endregion Watcher_Error
    }
}
