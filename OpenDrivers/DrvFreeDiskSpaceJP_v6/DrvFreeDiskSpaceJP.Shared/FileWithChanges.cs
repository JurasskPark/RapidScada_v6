using Database;
using Lang;
using MES.Shared;
using Parser.ParserText;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Xml.Linq;


namespace MES.Service
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
        /// <summary>
        /// File synchronization.
        /// <para>Синхронизация файлов.</para>
        /// </summary>
        public void Synchronization(string PathToWatchFolder, bool UsePathSubDir, string Name, string TemplatefileName, string ScriptSynchronization, string ScriptSelect, string ScriptInsert, string ScriptUpdate, string ScriptDelete, string ScriptRename, bool AddFiles = true, bool DeleteFales = true)
        {
            List<string> lstFiles = SearchFiles(PathToWatchFolder, UsePathSubDir);

            string scriptSynchronization = ScriptSynchronization;
            scriptSynchronization = scriptSynchronization.Replace("@Name", Name);

            DataTable dtData = new DataTable();

            DatabaseCommand databaseCommand = new DatabaseCommand();
            dtData = databaseCommand.Execution(scriptSynchronization);
            List<string> lstFilesDatabase = new List<string>();
            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow row = dtData.Rows[i];
                lstFilesDatabase.Add(row.ItemArray[0].ToString());
            }

            // add
            if (AddFiles)
            {
                List<string> lstDifference = lstFiles.Except(lstFilesDatabase).ToList();
                foreach (string fullPath in lstDifference)
                {
                    string fileEvent = "CREATED";
                    string fullPathToUpper = fullPath.ToUpper();

                    if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName.ToUpper()))
                    {
                        DateTime lastWriteTime = File.GetLastWriteTime(fullPath);
                        RecordEntry(Name, fileEvent, fullPath, lastWriteTime.ToString("s"), "", ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename);
                    }
                }
            }

            // delete
            if (DeleteFales)
            {
                List<string> lstDifference2 = lstFilesDatabase.Except(lstFiles).ToList();
                foreach (string fullPath in lstDifference2)
                {
                    const string fileEvent = "DELETED";
                    string fullPathToUpper = fullPath.ToUpper();

                    if (TemplatefileName != string.Empty && fullPathToUpper.Contains(TemplatefileName.ToUpper()))
                    {
                        RecordEntry(Name, fileEvent, fullPath, "", "", ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename);
                    }
                }
            }
        }
        #endregion Synchronization

        #region SearchFiles
        /// <summary>
        /// Getting a list of files from a directory.
        /// <para>Получение списка файлов из каталога.</para>
        /// </summary>
        private static List<string> lstFullPath = new List<string>();
        private static List<string> lstFolders = new List<string>();
        private static string dir = string.Empty;
        private static string subdir = string.Empty;

        public static List<string> SearchFiles(string path, bool useSubDir = false)
        {
            try
            {
                lstFullPath = new List<string>();
                if (useSubDir == false)
                {
                    DirectoryInfo dir = new DirectoryInfo(path);    // we receive a catalog // получаем каталог
                    DirectoryInfo[] dirs = dir.GetDirectories();    // получаем список каталогов
                    FileInfo[] files = dir.GetFiles(path);          // get a list of directory files // получаем список файлов каталога

                    foreach (FileInfo i in files)
                    {
                        lstFullPath.Add(i.FullName);
                        //Debuger.Log("Имя файла {0}, Размер файла {1}, Дата создания {2}, Дата изменения {3}", i.Name, i.Length, i.CreationTime, i.LastWriteTime); 
                    }

                    return lstFullPath;
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

                return new List<string>();
            }
        }

        private static List<string> IterateSortFoldersFiles(string dir)
        {
            try
            {
                lstFullPath = new List<string>();
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

                return lstFullPath = new List<string>();
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

        private static List<string> IterateSortFiles(List<string> lstFolders)
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
                            lstFullPath.Add(i.FullName);
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

                return lstFullPath;
            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");

                return lstFullPath;
            }
        }
        #endregion SearchFiles

        #region RecordEntry
        /// <summary>
        /// Recording information about a file from a directory and its event.
        /// <para>Запись информации о файле из каталога и его событии.</para>
        /// </summary>
        public void RecordEntry(string Name, string fileEvent, string fullPath, string lastTimeChanged = "", string oldFilePath = "", string ScriptSelect = "", string ScriptInsert = "", string ScriptUpdate = "", string ScriptDelete = "", string ScriptRename = "")
        {
            try
            {
                string msg = $"[{fileEvent}][{lastTimeChanged}][{fullPath}]";
                Debuger.Log(msg);
                string content = string.Empty;
                string fileName = Path.GetFileName(fullPath);

                if (fileEvent != "DELETED")
                {
                    lock (Obj)
                    {
                        using (StreamReader reader = new StreamReader(fullPath))
                        {
                            content = reader.ReadToEnd();
                        }
                    }
                }

                Process(fileEvent, Name, fileName, fullPath, lastTimeChanged, content, ScriptSelect, ScriptInsert, ScriptUpdate, ScriptDelete, ScriptRename, oldFilePath);

            }
            catch (Exception ex)
            {
                string errMsg = @$"{ex.Message}";
                Debuger.Log(Locale.IsRussian ?
                @$"[Ошибка] {errMsg}" :
                @$"[Error] {errMsg}");
            }
        }
        #endregion RecordEntry

        #region Process
        /// <summary>
        /// Process monitoring the directory.
        /// <para>Процесс слежения за каталогом.</para>
        /// </summary>
        public void Process(string fileEvent, string name, string fileName, string fullPath, string lastTimeChanged = "", string content = "", string scriptSelect = "", string scriptInsert = "", string scriptUpdate = "", string scriptDelete = "", string scriptRename = "", string oldFilePath = "")
        {
            switch (fileEvent)
            {
               case "CREATED":
                    FileCreated(name, fileName, fullPath, lastTimeChanged, content, scriptSelect, scriptInsert);
                break;
                case "CHANGED":
                    FileChanged(name, fileName, fullPath, lastTimeChanged, content, scriptUpdate);
                    break;
                case "DELETED":
                    FileDelete(name, fileName, fullPath, scriptDelete);
                    break;
                case "RENAMED":
                    FileRenamed(name, fileName, fullPath, lastTimeChanged, content, scriptRename, oldFilePath);
                    break;
            }
        }
        #endregion Process

        #region FileCreated
        /// <summary>
        /// Recording an event about file creation.
        /// <para>Запись события о создание файла.</para>
        /// </summary>
        private void FileCreated(string name, string fileName, string fullPath, string lastTimeChanged, string content, string scriptSelect, string scriptInsert)
        {
            string sqlSelect = scriptSelect.Replace("@Name", name).Replace("@FileName", fileName).Replace("@FullPath", fullPath);

            DataTable dtData = new DataTable();
            int rowCount = 0;
            string errMsg = string.Empty;


            DatabaseCommand databaseCommandSelect = new DatabaseCommand();
            databaseCommandSelect.Reguest(sqlSelect, out dtData, out rowCount, out errMsg);

            if (rowCount == 0)
            {
                content = content.Replace("''", "'").Replace("'", "''");
                string sqlInsert = scriptInsert.Replace("@Name", name).Replace("@FileName", fileName).Replace("@FullPath", fullPath).Replace("@Date", lastTimeChanged).Replace("@Content", content);

                DatabaseCommand databaseCommandInsert = new DatabaseCommand();
                databaseCommandInsert.ExecuteScalar(sqlInsert, out dtData, out rowCount, out errMsg);
                Debuger.Log(Locale.IsRussian ?
                    @$"Вставлено количество записей {rowCount}" :
                    @$"Number of records inserted {rowCount}");

                if (errMsg != string.Empty)
                {
                    Debuger.Log(Locale.IsRussian ?
                       @$"[Ошибка] {errMsg}" :
                       @$"[Error] {errMsg}");
                }
            }
        }
        #endregion FileCreated

        #region FileChanged
        /// <summary>
        /// Recording a file change event.
        /// <para>Запись события о изменении файла.</para>
        /// </summary>
        private void FileChanged(string name, string fileName, string fullPath, string lastTimeChanged, string content, string scriptUpdate)
        {
            content = content.Replace("''", "'").Replace("'", "''");
            scriptUpdate = scriptUpdate.Replace("@Name", name).Replace("@FileName", fileName).Replace("@FullPath", fullPath).Replace("@Date", lastTimeChanged).Replace("@Content", content);

            DataTable dtData = new DataTable();
            int rowCount = 0;
            string errMsg = string.Empty;

            DatabaseCommand databaseCommandUpdate = new DatabaseCommand();
            databaseCommandUpdate.ExecuteScalar(scriptUpdate, out dtData, out rowCount, out errMsg);
            Debuger.Log(Locale.IsRussian ?
                    @$"Изменено количество записей {rowCount}" :
                    @$"Number of entries changed {rowCount}");

            if (errMsg != string.Empty)
            {
                Debuger.Log(Locale.IsRussian ?
                   @$"[Ошибка] {errMsg}" :
                   @$"[Error] {errMsg}");
            }
        }
        #endregion FileChanged

        #region FileRenamed
        /// <summary>
        /// File rename event record.
        /// <para>Запись события о переименовывании файла.</para>
        /// </summary>
        private void FileRenamed(string name, string fileName, string fullPath, string lastTimeChanged, string content, string scriptRename, string oldFilePath)
        {
            content = content.Replace("''", "'").Replace("'", "''");
            scriptRename = scriptRename.Replace("@Name", name).Replace("@FileName", fileName).Replace("@FullPath", fullPath).Replace("@Date", lastTimeChanged).Replace("@Content", content).Replace("@OldFullPath", oldFilePath);

            DataTable dtData = new DataTable();
            int rowCount = 0;
            string errMsg = string.Empty;

            DatabaseCommand databaseCommandUpdate = new DatabaseCommand();
            databaseCommandUpdate.ExecuteScalar(scriptRename, out dtData, out rowCount, out errMsg);
            Debuger.Log(Locale.IsRussian ?
                    @$"Изменено количество записей {rowCount}" :
                    @$"Number of entries changed {rowCount}");

            if (errMsg != string.Empty)
            {
                Debuger.Log(Locale.IsRussian ?
                   @$"[Ошибка] {errMsg}" :
                   @$"[Error] {errMsg}");
            }
        }
        #endregion FileRenamed

        #region FileDeleted
        /// <summary>
        /// Recording an event about file deletion.
        /// <para>Запись события о удалении файла.</para>
        /// </summary>
        private void FileDelete(string name, string fileName, string fullPath, string scriptDelete)
        {
            string sqlDelete = scriptDelete.Replace("@Name", name).Replace("@FileName", fileName).Replace("@FullPath", fullPath);

            DataTable dtData = new DataTable();
            int rowCount = 0;
            string errMsg = string.Empty;

            DatabaseCommand databaseCommandDelete = new DatabaseCommand();
            databaseCommandDelete.ExecuteScalar(sqlDelete, out dtData, out rowCount, out errMsg);
            Debuger.Log(Locale.IsRussian ?
                    @$"Удалено количество записей {rowCount}" :
                    @$"Number of records deleted {rowCount}");

            if (errMsg != string.Empty)
            {
                Debuger.Log(Locale.IsRussian ?
                   @$"[Ошибка] {errMsg}" :
                   @$"[Error] {errMsg}");
            }
        }
        #endregion FileDeleted

        #region Parsing
        /// <summary>
        /// Parsing a file.
        /// <para>Парсинг файла.</para>
        /// </summary>
        public void Parsing(string scriptParsingSelect, string scriptParsingInsert, ParserTextSettings settings, ParserTextDictonary parserDictonary)
        {
            string scriptParsingInsertOriginal = scriptParsingInsert;
            List<ParserTextTag> ListTag = settings.GroupTag.Group;
            List<ParserTextVariable> ListDictonary = parserDictonary.Dictonary;

            DatabaseCommand databaseCommandParsing = new DatabaseCommand();
            DataTable dtData = databaseCommandParsing.Execution(scriptParsingSelect);
            if (dtData.Rows.Count > 0)
            {
                Debuger.Log(Locale.IsRussian ?
                    @$"Парсинг документа" :
                    @$"Parsing document");
            }


            for (int i = 0; i < dtData.Rows.Count; i++)
            {
                DataRow row = dtData.Rows[i];

                for (int r = 0; r < row.Table.Columns.Count; r++)
                {
                    string columnName = row.Table.Columns[r].ColumnName;
                    string value = row[columnName].ToString();
                    ParserTextVariable parserTextVariable = ListDictonary.Find(t => t.VariableName == columnName);
                    if (parserTextVariable == null)
                    {
                        continue;
                    }
                    parserTextVariable.VariableValue = value;
                }

                //string id = row["AU_Id"].ToString();
                //string date = row["AU_Date"].ToString();
                string content = row["AU_Content"].ToString();
                //string fileName = row["AU_FileName"].ToString();
                //string fullPath = row["AU_FilePath"].ToString();

                scriptParsingInsert = scriptParsingInsertOriginal;

                #region Parsing
                ParserText parserText = new ParserText();
                ParserListBlocks blocks = parserText.Parsing(content, settings);

                for (int t = 0; t < ListTag.Count; t++)
                {
                    ParserTextTag parserTag = ListTag[t];
                    ParserTextTag.GetValue(blocks, ref parserTag);

                    string parserTagValue = ParserTextTag.TagToString(parserTag.TagDataValue, parserTag.TagFormatData);
                    ParserTextVariable parserTextVariable = ListDictonary.Find(t => t.VariableName == parserTag.TagName);
                    parserTextVariable.VariableValue = parserTagValue;
                    if (parserTextVariable == null)
                    {
                        continue;
                    }

                    scriptParsingInsert = scriptParsingInsert.Replace(parserTextVariable.Variable, parserTagValue);
                }

                #endregion Parsing

                #region Dictonary

                for (int d = 0; d < ListDictonary.Count; d++)
                {
                    ParserTextVariable parserTextVariable = ListDictonary[d];
                    if (parserTextVariable.VariableValue == string.Empty)
                    {
                        continue;
                    }
                    else
                    {
                        scriptParsingInsert = scriptParsingInsert.Replace(parserTextVariable.Variable, parserTextVariable.VariableValue);
                    }
                }

                #endregion Dictonary

                #region Insert Data
                DatabaseCommand databaseCommandInsert = new DatabaseCommand();
                int rowCount = 0;
                string errMsg = string.Empty;
                databaseCommandInsert.Reguest(scriptParsingInsert, out rowCount, out errMsg);

                if (errMsg != string.Empty)
                {
                    Debuger.Log("=============================================================================");
                    Debuger.Log(scriptParsingInsert);
                    Debuger.Log(Locale.IsRussian ?
                        @$"[Ошибка]" + errMsg :
                        @$"[Error]" + errMsg);
                    Debuger.Log("=============================================================================");
                }
                #endregion Insert data
            }
        }
        #endregion Parsing
    }
}
