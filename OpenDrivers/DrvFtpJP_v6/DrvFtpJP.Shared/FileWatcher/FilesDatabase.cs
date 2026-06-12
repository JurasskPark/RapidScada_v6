namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Represents a row of the file watcher database.
    /// <para>Представляет строку базы данных наблюдателя файлов.</para>
    /// </summary>
    public class FilesDatabase
    {
        #region Variable
        private string pathFile;                 // file path
        private DateTime lastTimeChanged;        // last change time
        private long sizeFile;                   // file size
        private int numberLines;                 // number of lines
        private bool parsed;                     // parsed flag
        private int status;                      // parse status
        private string statusString;             // parse status text
        #endregion Variable

        #region Property
        /// <summary>
        /// Gets or sets the file path.
        /// <para>Возвращает или задает путь к файлу.</para>
        /// </summary>
        public string PathFile
        {
            get { return pathFile; }
            set { pathFile = value; }
        }

        /// <summary>
        /// Gets or sets the last change time.
        /// <para>Возвращает или задает время последнего изменения.</para>
        /// </summary>
        public DateTime LastTimeChanged
        {
            get { return lastTimeChanged; }
            set { lastTimeChanged = value; }
        }

        /// <summary>
        /// Gets or sets the file size.
        /// <para>Возвращает или задает размер файла.</para>
        /// </summary>
        public long SizeFile
        {
            get { return sizeFile; }
            set { sizeFile = value; }
        }

        /// <summary>
        /// Gets or sets the number of lines.
        /// <para>Возвращает или задает количество строк.</para>
        /// </summary>
        public int NumberLines
        {
            get { return numberLines; }
            set { numberLines = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the file is parsed.
        /// <para>Возвращает или задает признак разбора файла.</para>
        /// </summary>
        public bool Parsed
        {
            get { return parsed; }
            set { parsed = value; }
        }

        /// <summary>
        /// Gets or sets the parse status.
        /// <para>Возвращает или задает статус разбора.</para>
        /// </summary>
        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        /// <summary>
        /// Gets or sets the parse status text.
        /// <para>Возвращает или задает текст статуса разбора.</para>
        /// </summary>
        public string StatusString
        {
            get { return statusString; }
            set { statusString = value; }
        }
        #endregion Property

        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FilesDatabase()
        {
            PathFile = string.Empty;
            LastTimeChanged = DateTime.MinValue;
            SizeFile = 0;
            NumberLines = 0;
            Parsed = false;
            Status = 0;
            StatusString = string.Empty;
        }

        /// <summary>
        /// Loads database rows from the specified file.
        /// <para>Загружает строки базы данных из указанного файла.</para>
        /// </summary>
        /// <param name="path">Database file path.</param>
        /// <returns>Database row list.</returns>
        public static List<FilesDatabase> LoadDB(string path)
        {
            List<FilesDatabase> rows = new List<FilesDatabase>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    if (TryParseRow(s, out FilesDatabase line))
                    {
                        rows.Add(line);
                    }
                }
            }

            return rows;
        }

        /// <summary>
        /// Gets file paths from database rows.
        /// <para>Получает пути файлов из строк базы данных.</para>
        /// </summary>
        /// <param name="list">Database row list.</param>
        /// <returns>File path list.</returns>
        public static List<string> ListPathFiles(List<FilesDatabase> list)
        {
            List<string> result = new List<string>();
            foreach (FilesDatabase file in list)
            {
                result.Add(file.PathFile);
            }

            return result;
        }

        /// <summary>
        /// Adds a row to the database file.
        /// <para>Добавляет строку в файл базы данных.</para>
        /// </summary>
        /// <param name="path">Database file path.</param>
        /// <param name="row">Database row.</param>
        public static void AddRow(string path, FilesDatabase row)
        {
            using (StreamWriter streamWriter = File.AppendText(Path.Combine(path)))
            {
                streamWriter.WriteLine(GetRowText(row));
            }
        }

        /// <summary>
        /// Deletes a row from the database file by file path.
        /// <para>Удаляет строку из файла базы данных по пути файла.</para>
        /// </summary>
        /// <param name="path">Database file path.</param>
        /// <param name="pathFile">File path.</param>
        public static void DeleteRow(string path, string pathFile)
        {
            string tempFile = path.Replace(".db", ".tmp");
            using (StreamReader sr = new StreamReader(path))
            {
                using (StreamWriter sw = new StreamWriter(tempFile))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (TryParseRow(s, out FilesDatabase row) && row.PathFile != pathFile)
                        {
                            sw.WriteLine(GetRowText(row));
                        }
                    }
                }
            }

            File.Delete(path);
            File.Move(tempFile, path);
        }

        /// <summary>
        /// Selects a row from the database file by file path.
        /// <para>Выбирает строку из файла базы данных по пути файла.</para>
        /// </summary>
        /// <param name="path">Database file path.</param>
        /// <param name="pathFile">File path.</param>
        /// <returns>Found database row, or null if the row is not found.</returns>
        public static FilesDatabase SelectRow(string path, string pathFile)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    if (TryParseRow(s, out FilesDatabase line) && line.PathFile == pathFile)
                    {
                        return line;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Updates a row in the database file.
        /// <para>Обновляет строку в файле базы данных.</para>
        /// </summary>
        /// <param name="path">Database file path.</param>
        /// <param name="rowUpdate">Updated database row.</param>
        public static void UpdateRow(string path, FilesDatabase rowUpdate)
        {
            string tempFile = path.Replace(".db", ".tmp");
            using (StreamReader sr = new StreamReader(path))
            {
                using (StreamWriter sw = new StreamWriter(tempFile))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (TryParseRow(s, out FilesDatabase row))
                        {
                            if (row.PathFile != rowUpdate.PathFile)
                            {
                                sw.WriteLine(GetRowText(row));
                            }
                            else
                            {
                                sw.WriteLine(GetRowText(rowUpdate));
                            }
                        }
                    }
                }
            }

            File.Delete(path);
            File.Move(tempFile, path);
        }

        /// <summary>
        /// Tries to parse a database row.
        /// <para>Пытается разобрать строку базы данных.</para>
        /// </summary>
        /// <param name="text">Database row text.</param>
        /// <param name="row">Parsed database row.</param>
        /// <returns>True if the row is parsed.</returns>
        private static bool TryParseRow(string text, out FilesDatabase row)
        {
            row = new FilesDatabase();

            if (text == null)
            {
                return false;
            }

            string[] parameters = text.Split("|", StringSplitOptions.None);
            if (parameters.Length < 6)
            {
                return false;
            }

            try { row.PathFile = parameters[0]; } catch { row.PathFile = string.Empty; }
            try { row.LastTimeChanged = DateTime.Parse(parameters[1]); } catch { row.LastTimeChanged = DateTime.MinValue; }
            try { row.SizeFile = Convert.ToInt32(parameters[2]); } catch { row.SizeFile = 0; }
            try { row.NumberLines = Convert.ToInt32(parameters[3]); } catch { row.NumberLines = 0; }
            try { row.Parsed = Convert.ToBoolean(parameters[4]); } catch { row.Parsed = false; }
            try { row.Status = Convert.ToInt32(parameters[5]); } catch { row.Status = 3; }

            return true;
        }

        /// <summary>
        /// Converts a database row to text.
        /// <para>Преобразует строку базы данных в текст.</para>
        /// </summary>
        /// <param name="row">Database row.</param>
        /// <returns>Database row text.</returns>
        private static string GetRowText(FilesDatabase row)
        {
            return $@"{row.PathFile}|{row.LastTimeChanged.ToString()}|{row.SizeFile}|{row.NumberLines}|{row.Parsed.ToString()}|{row.Status}";
        }
        #endregion Basic

        #region Support class
        /// <summary>
        /// Defines file parsing statuses.
        /// <para>Определяет статусы разбора файла.</para>
        /// </summary>
        public enum FileParsedStatus
        {
            /// <summary>
            /// File is not parsed.
            /// <para>Файл не разобран.</para>
            /// </summary>
            NoParsed = 0,

            /// <summary>
            /// File is parsed.
            /// <para>Файл разобран.</para>
            /// </summary>
            Parsed = 1,

            /// <summary>
            /// File parsing failed.
            /// <para>Ошибка разбора файла.</para>
            /// </summary>
            Error = 2,
        }
        #endregion Support class
    }
}