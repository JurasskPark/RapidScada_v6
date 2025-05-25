using Database;
using Lang;
using MES.Shared;
using Parser.ParserText;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MES.Service
{
    public class FileWatcherLite : IDisposable
    {
        #region Variables
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

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FileWatcherLite()
        {

        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FileWatcherLite(string name, string pathFolder, bool useSubDir, string filter, string templateFileName, 
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
        ~FileWatcherLite()
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
            Parsing();
            // dispose
            Dispose(true);
        }
        #endregion Process

        #region Synchronization
        /// <summary>
        /// Files synchronization.
        /// <para>Синхронизация файлов.</para>
        /// </summary>
        public void Synchronization()
        {
            // 06.12.2024 Сказали не удалять файлы из БД
            FileWithChanges fileWithChanges = new FileWithChanges();
            fileWithChanges.Synchronization(PathToWatchFolder, UsePathSubDir, Name, TemplatefileName, ScriptSynchronization, ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename, true, false);
        }
        #endregion Synchronization

        #region Parsing
        /// <summary>
        /// Files parsing.
        /// <para>Парсинг файлов.</para>
        /// </summary>
        public void Parsing()
        {
            FileWithChanges fileWithChanges = new FileWithChanges();
            fileWithChanges.Parsing(ScriptParserSelect, ScriptParserInsert, ParserSettings, ParserDictonary);
        }
        #endregion Parsing
    }
}
