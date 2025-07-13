namespace Scada.Comm.Drivers.DrvPingJP
{
    /// <summary>
    /// The structure of storing information about files and directories of the operating system.
    /// <para>Структура хранения информации о файлах и каталогах операционной системы.</para>
    /// </summary>
    public class FilesDatabase
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FilesDatabase()
        {
            this.PathFile = string.Empty;
            this.LastTimeChanged = DateTime.MinValue;
            this.SizeFile = 0;
            this.NumberLines = 0;
            this.Parsed = false;
            this.Status = 0;
        }

        #region Variables
        /// <summary>
        /// The path to the file.
        /// <para>Путь до файла.</para>
        /// </summary>
        private string pathFile;
        public string PathFile
        {
            get { return pathFile; }
            set { pathFile = value; }
        }

        /// <summary>
        /// The date the file was modified.
        /// <para>Дата изменения файла.</para>
        /// </summary>
        private DateTime lastTimeChanged;
        public DateTime LastTimeChanged
        {
            get { return lastTimeChanged; }
            set { lastTimeChanged = value; }

        }

        /// <summary>
        /// The file size.
        /// <para>Размер файла.</para>
        /// </summary>
        private long sizeFile;
        public long SizeFile
        {
            get { return sizeFile; }
            set { sizeFile = value; }
        }

        /// <summary>
        /// The number of lines in the file.
        /// <para>Количество строк в файле.</para>
        /// </summary>
        private int numberLines;
        public int NumberLines
        { 
            get { return numberLines; } 
            set { numberLines = value; } 
        }

        /// <summary>
        /// An indication of the analysis.
        /// <para>Признак проведения анализа.</para>
        /// </summary>
        private bool parsed;
        public bool Parsed 
        { 
            get { return parsed; } 
            set { parsed = value; } 
        }

        /// <summary>
        /// Status (numeric).
        /// <para>Статус (числовой).</para>
        /// </summary>
        private int status;
        public int Status 
        {
            get { return status; } 
            set { status = value; }
        }

        /// <summary>
        /// Status (string).
        /// <para>Статус (строковый).</para>
        /// </summary>
        private string statusString;
        public string StatusString
        {
            get { return statusString; }
            set { statusString = value; }
        }
        #endregion Variables
    }
}
