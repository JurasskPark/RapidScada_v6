using DebugerLog;
using Scada.Lang;
using System.Runtime.InteropServices;


namespace  Scada.Comm.Drivers.DrvFtpJP
{
    public class FileWithChanges : IDisposable
    {
        #region Variables
        object Obj = new object();
        #endregion Variables

        #region Dispose
        private IntPtr _bufferPtr;
        public int BUFFER_SIZE = 1024 * 1024 * 50; // 50 MB
        private bool _disposed = false;

        /// <summary>
        /// Dispose an instance of a class.
        /// <para>Уничтожает экземпляр класса.</para>
        /// </summary>
        ~FileWithChanges()
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

        #region Synchronization
        ///// <summary>
        ///// File synchronization.
        ///// <para>Синхронизация файлов.</para>
        ///// </summary>
        //public void Synchronization(string PathToWatchFolder, bool UsePathSubDir, string Name, string TemplatefileName, string ScriptSynchronization, string ScriptSelect, string ScriptInsert, string ScriptUpdate, string ScriptDelete, string ScriptRename, bool AddFiles = true, bool DeleteFiles = true, bool UseReadFromLastLine = true, bool UseReadJustOneLastLine = true, ParserTextDictonary ParserDictonary = null)
        //{
        //    // список файлов с информации с диска
        //    List<FilesDatabase> lstFiles = SearchFiles(PathToWatchFolder, UsePathSubDir);
        //    List<string> lstFullPath = new List<string>();

        //    // заполняем список путей файлов c диска
        //    for (int i = 0; i < lstFiles.Count; i++)
        //    {
        //        FilesDatabase row = lstFiles[i];
        //        lstFullPath.Add(row.PathFile);
        //    }

        //    // список файлов из базы данных
        //    List<string> lstFullPathDatabase = new List<string>();
            
        //    // скрипт синхронизации
        //    string scriptSynchronization = ScriptSynchronization;
        //    ParserDictonary = DictonaryUpdate(ParserDictonary, "@Name", Name);
        //    scriptSynchronization = DictonaryReplace(ParserDictonary, scriptSynchronization);

        //    // название столбцов
        //    string ColumnNameId = DictonaryFindName(ParserDictonary, "@ColumnId");         
        //    string ColumnNameDate = DictonaryFindName(ParserDictonary, "@ColumnDate");
        //    string ColumnNameName = DictonaryFindName(ParserDictonary, "@ColumnName");
        //    string ColumnNameFileName = DictonaryFindName(ParserDictonary, "@ColumnFileName");
        //    string ColumnNameFullPath = DictonaryFindName(ParserDictonary, "@ColumnFullPath");
        //    string ColumnNameContent = DictonaryFindName(ParserDictonary, "@ColumnContent");
        //    string ColumnNameOwner = DictonaryFindName(ParserDictonary, "@ColumnOwner");
        //    string ColumnNameIsNeedToRead = DictonaryFindName(ParserDictonary, "@ColumnIsNeedToRead");
        //    string ColumnNameStatus = DictonaryFindName(ParserDictonary, "@ColumnStatus");
        //    string ColumnNameSizeFile = DictonaryFindName(ParserDictonary, "@ColumnSizeFile");
        //    string ColumnNameNumberLines = DictonaryFindName(ParserDictonary, "@ColumnNumberLines");

        //    // получение списка путей в базе данных через скрипт синхронизации
        //    DataTable dtData = new DataTable();
        //    DatabaseCommand databaseCommand = new DatabaseCommand();
        //    dtData = databaseCommand.Execution(scriptSynchronization);

        //    // заполняем список путей файлов из базы данных
        //    for (int i = 0; i < dtData.Rows.Count; i++)
        //    {
        //        DataRow row = dtData.Rows[i];
        //        lstFullPathDatabase.Add(row[ColumnNameFullPath].ToString());
        //    }

        //    // add
        //    if (AddFiles)
        //    {
        //        List<string> lstDifference = lstFullPath.Except(lstFullPathDatabase).ToList();
        //        foreach (string fullPath in lstDifference)
        //        {
        //            string fullPathToUpper = fullPath.ToUpper();

        //            if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName.ToUpper()))
        //            {
        //                const string fileEvent = "CREATED";
        //                FilesDatabase file = lstFiles.Where(x => x.PathFile.ToUpper() == fullPathToUpper).FirstOrDefault();

        //                RecordEntry(Name, fileEvent, fullPath, file.LastTimeChanged.ToString("s"), "", ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename, ParserDictonary);
        //            }
        //        }
        //    }

        //    // delete
        //    if (DeleteFiles)
        //    {
        //        List<string> lstDifference2 = lstFullPathDatabase.Except(lstFullPath).ToList();
        //        foreach (string fullPath in lstDifference2)
        //        { 
        //            string fullPathToUpper = fullPath.ToUpper();

        //            if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName.ToUpper()))
        //            {
        //                const string fileEvent = "DELETED";

        //                RecordEntry(Name, fileEvent, fullPath, "", "", ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename, ParserDictonary);
        //            }
        //        }
        //    }

        //    // change
        //    if (UseReadFromLastLine || UseReadJustOneLastLine)
        //    {
        //        List<FilesDatabase> lstFilesDatabaseChange = new List<FilesDatabase>();
        //        for (int i = 0; i < dtData.Rows.Count; i++)
        //        {
        //            DataRow row = dtData.Rows[i];

        //            FilesDatabase filesDatabase = new FilesDatabase();

        //            try { filesDatabase.PathFile = row[ColumnNameFullPath].ToString(); } catch { filesDatabase.PathFile = string.Empty; }
        //            try { filesDatabase.LastTimeChanged = DateTime.Parse(row[ColumnNameDate].ToString()); } catch { filesDatabase.LastTimeChanged = DateTime.MinValue; }
        //            try { filesDatabase.SizeFile = Convert.ToInt32(row[ColumnNameSizeFile]); } catch { filesDatabase.SizeFile = 0; }
        //            try { filesDatabase.Parsed = Convert.ToBoolean(row[ColumnNameIsNeedToRead]); } catch { filesDatabase.Parsed = false; }
        //            try { filesDatabase.StatusString = row[ColumnNameStatus].ToString(); } catch { filesDatabase.StatusString = string.Empty; }

        //            FilesDatabase file = lstFiles.Where(x => x.PathFile.ToUpper() == filesDatabase.PathFile.ToUpper()).FirstOrDefault();

        //            if (
        //                (filesDatabase.LastTimeChanged != file.LastTimeChanged) ||
        //                (filesDatabase.SizeFile != file.SizeFile)
        //                )
        //            {      
        //                string fullPathToUpper = filesDatabase.PathFile.ToUpper();

        //                if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName.ToUpper()))
        //                {
        //                    const string fileEvent = "CHANGED";

        //                    RecordEntry(Name, fileEvent, filesDatabase.PathFile, file.LastTimeChanged.ToString("s"), "", ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename, ParserDictonary);
        //                }
        //            }

        //        }
        //    }
        //}
        #endregion Synchronization

        #region SearchFiles
        /// <summary>
        /// Getting a list of files from a directory.
        /// <para>Получение списка файлов из каталога.</para>
        /// </summary>
        private static List<FilesDatabase> lstFiles = new List<FilesDatabase>();
        private static List<string> lstFolders = new List<string>();
        private static string dir = string.Empty;
        private static string subdir = string.Empty;

        public static List<FilesDatabase> SearchFiles(string path, bool useSubDir = false)
        {
            try
            {
                lstFiles = new List<FilesDatabase>();
                if (useSubDir == false)
                {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    FileInfo[] files = dir.GetFiles();          // get a list of directory files // получаем список файлов каталога

                    foreach (FileInfo i in files)
                    {
                        FilesDatabase file = new FilesDatabase();
                        file.PathFile = i.FullName;
                        file.SizeFile = i.Length;
                        file.LastTimeChanged = i.LastWriteTime;
                        lstFiles.Add(file);
                        //Debuger.Log("Имя файла {0}, Размер файла {1}, Дата создания {2}, Дата изменения {3}", i.Name, i.Length, i.CreationTime, i.LastWriteTime); 
                    }

                    return lstFiles;
                }
                else
                {
                    return IterateSortFoldersFiles(path);
                }
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return new List<FilesDatabase>();
            }
        }

        private static List<FilesDatabase> IterateSortFoldersFiles(string dir)
        {
            try
            {
                lstFiles = new List<FilesDatabase>();
                lstFolders = new List<string>();
                lstFolders.Add(dir);

                return IterateSortFiles(IterateSortFolders(dir));
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return lstFiles = new List<FilesDatabase>();
            }
        }

        private static List<string> IterateSortFolders(string dir)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(dir);
                foreach (DirectoryInfo tmpdir in dirInfo.GetDirectories())
                {
                    lstFolders.Add(tmpdir.FullName);
                    IterateSortFolders(tmpdir.FullName);
                }

                return lstFolders;
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return lstFolders = new List<string>();
            }
        }

        private static List<FilesDatabase> IterateSortFiles(List<string> lstFolders)
        {
            try
            {
                foreach (string dir in lstFolders)
                {
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(dir);
                        FileInfo[] files = dirInfo.GetFiles();
                        foreach (FileInfo i in files)
                        {
                            FilesDatabase file = new FilesDatabase();
                            file.PathFile = i.FullName;
                            file.SizeFile = i.Length;
                            file.LastTimeChanged = i.LastWriteTime;
                            lstFiles.Add(file);
                            //Debuger.Log("Имя файла {0}, Размер файла {1}, Дата создания {2}, Дата изменения {3}", i.Name, i.Length, i.CreationTime, i.LastWriteTime); 
                        }
                    }
                    catch (Exception ex)
                    {
                        string errMsg = @$"{ex.Message}";
                        Debuger.Log(Locale.IsRussian ?
                        @$"[Ошибка] {errMsg}" :
                        @$"[Error] {errMsg}");
                    }
                }

                return lstFiles;
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return lstFiles;
            }
        }
        #endregion SearchFiles

        #region RecordEntry
        /// <summary>
        /// Recording information about a file from a directory and its event.
        /// <para>Запись информации о файле из каталога и его событии.</para>
        /// </summary>
        //public void RecordEntry(string Name, string fileEvent, string fullPath, string lastTimeChanged = "", string oldFilePath = "", string ScriptSelect = "", string ScriptInsert = "", string ScriptUpdate = "", string ScriptDelete = "", string ScriptRename = "", ParserTextDictonary ParserDictonary = null)
        //{
        //    try
        //    {
        //        string msg = $"[{fileEvent}][{lastTimeChanged}][{fullPath}]";
        //        Debuger.Log(msg);
        //        string content = string.Empty;
        //        string fileName = Path.GetFileName(fullPath);
        //        string owner = string.Empty;
        //        long sizeFile = 0;
        //        int numberLines = 0;
        //        string line;

        //        if (fileEvent != "DELETED")
        //        {
        //            lock (Obj)
        //            {
        //                sizeFile = new System.IO.FileInfo(fullPath).Length;
        //                lastTimeChanged = File.GetLastWriteTime(fullPath).ToString();

        //                // read file
        //                //using (StreamReader reader = new StreamReader(fullPath))
        //                //{
        //                //    content = reader.ReadToEnd();
        //                //}
        //                using (StreamReader reader = new StreamReader(fullPath))
        //                {
        //                    while ((line = reader.ReadLine()) != null)
        //                    {
        //                        content += line + Environment.NewLine;
        //                        numberLines++;
        //                    }
        //                }

        //                #region Owner
        //                // owner file
        //                try
        //                {
        //                    IdentityReference sid = null;
        //                    try
        //                    {
        //                        FileInfo oneFileInfo = new FileInfo(fullPath);
        //                        FileSecurity fileSecurity = oneFileInfo.GetAccessControl();
        //                        sid = fileSecurity.GetOwner(typeof(SecurityIdentifier));
        //                        try
        //                        {
        //                            NTAccount ntAccount = sid.Translate(typeof(System.Security.Principal.NTAccount)) as NTAccount;
        //                            owner = ntAccount.Value;
        //                        }
        //                        catch
        //                        {
        //                            SecurityIdentifier scAccount = sid.Translate(typeof(System.Security.Principal.SecurityIdentifier)) as SecurityIdentifier;
        //                            owner = scAccount.Value;
        //                        }
        //                    }
        //                    catch (IdentityNotMappedException ex)
        //                    {
        //                        if (sid != null)
        //                        {
        //                            owner = sid.ToString();
        //                        }
        //                    }
        //                }
        //                catch { }
        //                #endregion Owner
        //            }
        //        }

        //        Process(fileEvent, Name, fileName, fullPath, lastTimeChanged, content, owner, sizeFile, numberLines, ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename, oldFilePath, ParserDictonary);

        //    }
        //    catch (Exception ex)
        //    {
        //        string errMsg = @$"{ex.Message}";
        //        Debuger.Log(Locale.IsRussian ?
        //        @$"[Ошибка] {errMsg}" :
        //        @$"[Error] {errMsg}");
        //    }
        //}
        #endregion RecordEntry

        #region Process
        ///// <summary>
        ///// Process monitoring the directory.
        ///// <para>Процесс слежения за каталогом.</para>
        ///// </summary>
        //public void Process(string fileEvent, string name, string fileName, string fullPath, string lastTimeChanged = "", string content = "", string owner = "", long sizeFile = 0, int numberLines = 0, string scriptSelect = "", string scriptInsert = "", string scriptUpdate = "", string scriptDelete = "", string scriptRename = "", string oldFilePath = "", ParserTextDictonary parserDictonary = null)
        //{
        //    switch (fileEvent)
        //    {
        //        case "CREATED":
        //            FileCreated(name, fileName, fullPath, lastTimeChanged, content, owner, sizeFile, numberLines, scriptSelect, scriptInsert, parserDictonary);
        //            break;
        //        case "CHANGED":
        //            FileChanged(name, fileName, fullPath, lastTimeChanged, content, owner, sizeFile, numberLines, scriptUpdate, parserDictonary);
        //            break;
        //        case "DELETED":
        //            FileDelete(name, fileName, fullPath, scriptDelete, parserDictonary);
        //            break;
        //        case "RENAMED":
        //            FileRenamed(name, fileName, fullPath, lastTimeChanged, content, owner, scriptRename, oldFilePath);
        //            break;
        //    }
        //}
        #endregion Process

        #region FileCreated
        ///// <summary>
        ///// Recording an event about file creation.
        ///// <para>Запись события о создание файла.</para>
        ///// </summary>
        //private void FileCreated(string name, string fileName, string fullPath, string lastTimeChanged, string content, string owner, long sizeFile, int numberLines, string scriptSelect, string scriptInsert, ParserTextDictonary parserDictonary = null)
        //{
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Name", SQLValidationValue(name));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@FileName", SQLValidationValue(fileName));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@FullPath", SQLValidationValue(fullPath));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Date", SQLValidationValue(lastTimeChanged));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Content", SQLValidationValue(content));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Owner", SQLValidationValue(owner));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@SizeFile", SQLValidationValue(sizeFile.ToString()));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@NumberLines", SQLValidationValue(numberLines.ToString()));

        //    string sqlSelect = DictonaryReplace(parserDictonary, scriptSelect);

        //    DataTable dtData = new DataTable();
        //    int rowCount = 0;
        //    string errMsg = string.Empty;


        //    DatabaseCommand databaseCommandSelect = new DatabaseCommand();
        //    databaseCommandSelect.Reguest(sqlSelect, out dtData, out rowCount, out errMsg);

        //    if (rowCount == 0)
        //    {
        //        content = SQLValidationValue(content);
        //        string sqlInsert = DictonaryReplace(parserDictonary, scriptInsert);

        //        DatabaseCommand databaseCommandInsert = new DatabaseCommand();
        //        databaseCommandInsert.ExecuteScalar(sqlInsert, out dtData, out rowCount, out errMsg);
                
        //        Debuger.Log(Locale.IsRussian ?
        //            @$"Вставлено количество записей {rowCount}" :
        //            @$"Number of records inserted {rowCount}");

        //        if (errMsg != string.Empty)
        //        {
        //            Debuger.Log(Locale.IsRussian ?
        //               @$"[Ошибка] {errMsg}" :
        //               @$"[Error] {errMsg}");
        //        }
        //    }
        //}
        #endregion FileCreated

        #region FileChanged
        ///// <summary>
        ///// Recording a file change event.
        ///// <para>Запись события о изменении файла.</para>
        ///// </summary>
        //private void FileChanged(string name, string fileName, string fullPath, string lastTimeChanged, string content, string owner, long sizeFile, int numberLines, string scriptUpdate, ParserTextDictonary parserDictonary = null)
        //{
        //    content = SQLValidationValue(content);
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Name", SQLValidationValue(name));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@FileName", SQLValidationValue(fileName));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@FullPath", SQLValidationValue(fullPath));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Content", SQLValidationValue(content));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Date", SQLValidationValue(lastTimeChanged));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Owner", SQLValidationValue(owner));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@SizeFile", SQLValidationValue(sizeFile.ToString()));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@NumberLines", SQLValidationValue(numberLines.ToString()));

        //    string sqlUpdate = DictonaryReplace(parserDictonary, scriptUpdate);

        //    DataTable dtData = new DataTable();
        //    int rowCount = 0;
        //    string errMsg = string.Empty;

        //    DatabaseCommand databaseCommandUpdate = new DatabaseCommand();
        //    databaseCommandUpdate.ExecuteScalar(sqlUpdate, out dtData, out rowCount, out errMsg);
            
        //    Debuger.Log(Locale.IsRussian ?
        //            @$"Изменено количество записей {rowCount}" :
        //            @$"Number of entries changed {rowCount}");

        //    if (errMsg != string.Empty)
        //    {
        //        Debuger.Log(Locale.IsRussian ?
        //           @$"[Ошибка] {errMsg}" :
        //           @$"[Error] {errMsg}");
        //    }
        //}
        #endregion FileChanged

        #region FileRenamed
        ///// <summary>
        ///// File rename event record.
        ///// <para>Запись события о переименовывании файла.</para>
        ///// </summary>
        //private void FileRenamed(string name, string fileName, string fullPath, string lastTimeChanged, string content, string owner, string scriptRename, string oldFilePath)
        //{
        //    content = SQLValidationValue(content);
        //    scriptRename = scriptRename
        //        .Replace("@Name", SQLValidationValue(name))
        //        .Replace("@FileName", SQLValidationValue(fileName))
        //        .Replace("@FullPath", SQLValidationValue(fullPath))
        //        .Replace("@Date", SQLValidationValue(lastTimeChanged))
        //        .Replace("@Content", SQLValidationValue(content))
        //        .Replace("@OldFullPath", SQLValidationValue(oldFilePath));

        //    DataTable dtData = new DataTable();
        //    int rowCount = 0;
        //    string errMsg = string.Empty;

        //    DatabaseCommand databaseCommandUpdate = new DatabaseCommand();
        //    databaseCommandUpdate.ExecuteScalar(scriptRename, out dtData, out rowCount, out errMsg);
        //    Debuger.Log(Locale.IsRussian ?
        //            @$"Изменено количество записей {rowCount}" :
        //            @$"Number of entries changed {rowCount}");

        //    if (errMsg != string.Empty)
        //    {
        //        Debuger.Log(Locale.IsRussian ?
        //           @$"[Ошибка] {errMsg}" :
        //           @$"[Error] {errMsg}");
        //    }
        //}
        #endregion FileRenamed

        #region FileDeleted
        ///// <summary>
        ///// Recording an event about file deletion.
        ///// <para>Запись события о удалении файла.</para>
        ///// </summary>
        //private void FileDelete(string name, string fileName, string fullPath, string scriptDelete, ParserTextDictonary parserDictonary = null)
        //{
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@Name", SQLValidationValue(name));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@FileName", SQLValidationValue(fileName));
        //    parserDictonary = DictonaryUpdate(parserDictonary, "@FullPath", SQLValidationValue(fullPath));

        //    string sqlDelete= DictonaryReplace(parserDictonary, scriptDelete);

        //    DataTable dtData = new DataTable();
        //    int rowCount = 0;
        //    string errMsg = string.Empty;

        //    DatabaseCommand databaseCommandDelete = new DatabaseCommand();
        //    databaseCommandDelete.ExecuteScalar(sqlDelete, out dtData, out rowCount, out errMsg);
        //    Debuger.Log(Locale.IsRussian ?
        //            @$"Удалено количество записей {rowCount}" :
        //            @$"Number of records deleted {rowCount}");

        //    if (errMsg != string.Empty)
        //    {
        //        Debuger.Log(Locale.IsRussian ?
        //           @$"[Ошибка] {errMsg}" :
        //           @$"[Error] {errMsg}");
        //    }
        //}
        #endregion FileDeleted

        #region Parsing
        ///// <summary>
        ///// Parsing a file.
        ///// <para>Парсинг файла.</para>
        ///// </summary>
        //public void Parsing(string scriptParsingSelect, string scriptParsingInsert, bool useReadFromLastLine, bool useReadJustOneLastLine, ParserTextSettings settings, ParserTextDictonary parserDictonary)
        //{
        //    string scriptParsingInsertOriginal = scriptParsingInsert;

        //    List<DriverTag> ListTag = settings.GroupTag.Group;
        //    List<ParserTextVariable> ListDictonary = parserDictonary.Dictonary;

        //    DatabaseCommand databaseCommandParsing = new DatabaseCommand();
        //    DataTable dtData = databaseCommandParsing.Execution(scriptParsingSelect);
            
        //    if (dtData.Rows.Count > 0)
        //    {
        //        Debuger.Log(Locale.IsRussian ?
        //            @$"Парсинг документа" :
        //            @$"Parsing document");
        //    }

        //    for (int i = 0; i < dtData.Rows.Count; i++)
        //    {
        //        DataRow row = dtData.Rows[i];
        //        // добавляем информацию в словарь
        //        for (int r = 0; r < row.Table.Columns.Count; r++)
        //        {
        //            string columnName = row.Table.Columns[r].ColumnName;
        //            string value = row[columnName].ToString();
        //            ParserTextVariable parserTextVariable = ListDictonary.Find(t => t.VariableName == columnName);
        //            if (parserTextVariable == null)
        //            {
        //                continue;
        //            }
        //            parserTextVariable.VariableValue = value;
        //        }

        //        // название столбца
        //        string columnNameContent = DictonaryFindName(parserDictonary, "@ColumnContent");
        //        // получаем контент
        //        string content = row[columnNameContent].ToString();

        //        // получаем последнее
        //        if (useReadJustOneLastLine)
        //        {
        //            string[] lines = content.Split(new[] { '\r', '\n' });
        //            string line = lines[lines.Length - 1];
        //            content = line;
        //        }
        //        else if (useReadFromLastLine)
        //        {
        //            string columnNameNumberLines = DictonaryFindName(parserDictonary, "@ColumnNumberLines");
        //            int numberLines = Convert.ToInt32(row[columnNameNumberLines].ToString());

        //            string[] lines = content.Split(new[] { '\r', '\n' });
        //            string contentTmp = string.Empty;
        //            if (numberLines > 1)
        //            {
        //                numberLines = numberLines - 1;
        //            }

        //            for (int u = numberLines; u < lines.Length; u++)
        //            {
        //                contentTmp += lines.ElementAt(u);
        //            }

        //            content = contentTmp;
        //        }

        //        // обработка текста до парсинга
        //        for(int l = 0; l < ListDictonary.Count; l++)
        //        {
        //            // сортируем список, чтобы сначала шли длинные названия
        //            ListDictonary = ListDictonary.OrderByDescending(x => x.variable.Length).ToList();

        //            ParserTextVariable variable = ListDictonary[l];
        //            if (variable.VariableValue == string.Empty)
        //            {
        //                continue;
        //            }
        //            else if (variable.VariableValue == "{DELETE}")
        //            {
        //                string[] lines = content.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

        //                // Удаляем строку, содержащую определенный текст
        //                lines = lines.Where(line => !line.Contains(variable.Variable)).ToArray();

        //                // Собираем текст обратно
        //                content = string.Join(Environment.NewLine, lines);
        //            }
        //            else
        //            {
        //                content = content.Replace(variable.Variable, variable.VariableValue);
        //            }
        //        }

        //        scriptParsingInsert = scriptParsingInsertOriginal;

        //        #region Parsing
        //        ParserText parserText = new ParserText();
        //        ParserListBlocks blocks = parserText.Parsing(content, settings);

        //        for (int t = 0; t < ListTag.Count; t++)
        //        {
        //            DriverTag parserTag = ListTag[t];
        //            DriverTag.GetValue(blocks, ref parserTag);

        //            string parserTagValue = DriverTag.TagToString(parserTag.TagDataValue, parserTag.TagFormatData);
        //            ParserTextVariable parserTextVariable = ListDictonary.Find(t => t.VariableName == parserTag.TagName);
        //            parserTextVariable.VariableValue = parserTagValue;
        //            if (parserTextVariable == null)
        //            {
        //                continue;
        //            }
        //            else if (parserTextVariable.VariableValue == "{DELETE}")
        //            {

        //            }
        //            else
        //            {
        //                scriptParsingInsert = scriptParsingInsert.Replace(parserTextVariable.Variable, parserTagValue);
        //            }               
        //        }

        //        #endregion Parsing

        //        #region Dictonary

        //        for (int d = 0; d < ListDictonary.Count; d++)
        //        {
        //            ParserTextVariable parserTextVariable = ListDictonary[d];
        //            if (parserTextVariable.VariableValue == string.Empty)
        //            {
        //                continue;
        //            }
        //            else
        //            {
        //                scriptParsingInsert = scriptParsingInsert.Replace(parserTextVariable.Variable, parserTextVariable.VariableValue);
        //            }
        //        }

        //        #endregion Dictonary

        //        #region Tags
        //        DriverTagReturn parserTextTagReturn = new DriverTagReturn();
        //        parserTextTagReturn.Return(ListTag);
        //        #endregion Tags

        //        #region Insert Data
        //        DatabaseCommand databaseCommandInsert = new DatabaseCommand();
        //        int rowCount = 0;
        //        string errMsg = string.Empty;
        //        databaseCommandInsert.Reguest(scriptParsingInsert, out rowCount, out errMsg);

        //        if (rowCount > 0)
        //        {
        //            Debuger.Log(Locale.IsRussian ?
        //                @$"Обработано запиcей {rowCount}" :
        //                @$"Processed records {rowCount}");
        //        }

        //        if (errMsg != string.Empty)
        //        {
        //            Debuger.Log("=============================================================================");
        //            Debuger.Log(scriptParsingInsert);
        //            Debuger.Log(Locale.IsRussian ?
        //                @$"[Ошибка]" + errMsg :
        //                @$"[Error]" + errMsg);
        //            Debuger.Log("=============================================================================");
        //        }
        //        #endregion Insert data
        //    }

        //    bool debug = true;
        //}
        #endregion Parsing

        #region DictonaryProccess
        //public string DictonaryFindName(ParserTextDictonary parserDictonary, string Variable)
        //{
        //    string VariableValue = string.Empty;

        //    ParserTextDictonary result = new ParserTextDictonary();
        //    result = parserDictonary;
        //    List<ParserTextVariable> ListDictonary = result.Dictonary;

        //    for (int d = 0; d < ListDictonary.Count; d++)
        //    {
        //        ParserTextVariable parserTextVariable = ListDictonary[d];
        //        if (parserTextVariable.Variable != Variable)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            return parserTextVariable.VariableName;
        //        }
        //    }

        //    return VariableValue;
        //}

        //public string DictonaryFindValue(ParserTextDictonary parserDictonary, string Variable)
        //{
        //    string VariableValue = string.Empty;

        //    ParserTextDictonary result = new ParserTextDictonary();
        //    result = parserDictonary;
        //    List<ParserTextVariable> ListDictonary = result.Dictonary;

        //    for (int d = 0; d < ListDictonary.Count; d++)
        //    {
        //        ParserTextVariable parserTextVariable = ListDictonary[d];
        //        if (parserTextVariable.Variable != Variable)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //           return parserTextVariable.VariableValue;
        //        }
        //    }

        //    return VariableValue;
        //}


        //public ParserTextDictonary DictonaryUpdate(ParserTextDictonary parserDictonary, string Variable, string VariableValue)
        //{
        //    ParserTextDictonary result = new ParserTextDictonary();
        //    result = parserDictonary;
        //    List<ParserTextVariable> ListDictonary = result.Dictonary;

        //    for (int d = 0; d < ListDictonary.Count; d++)
        //    {
        //        ParserTextVariable parserTextVariable = ListDictonary[d];
        //        if (parserTextVariable.Variable != Variable)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            parserTextVariable.VariableValue = VariableValue;
        //        }
        //    }
        
        //    return result;
        //}

        //public string DictonaryReplace(ParserTextDictonary parserDictonary, string text)
        //{
        //    List<ParserTextVariable> ListDictonary = parserDictonary.Dictonary;

        //    for (int d = 0; d < ListDictonary.Count; d++)
        //    {
        //        ParserTextVariable parserTextVariable = ListDictonary[d];
        //        if (parserTextVariable.VariableValue == string.Empty)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            text = text.Replace(parserTextVariable.Variable, parserTextVariable.VariableValue);
        //        }
        //    }

        //    return text;
        //}

        #endregion DictonaryProccess

        #region SQLValidation
        /// <summary>
        /// <para>Экранирование одинарных ковычек</para>>
        /// </summary>
        public static string SQLValidationValue(string text)
        {
            return text = text.Replace("''", "'").Replace("'", "''");
        }

        #endregion SQLValidation
    }
}
