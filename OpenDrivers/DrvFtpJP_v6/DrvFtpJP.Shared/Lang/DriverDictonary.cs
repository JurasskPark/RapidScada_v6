using Scada.Lang;
using static DrvFtpJP.Shared.FilesDirectorys.FilesDirectoriesInformation;
using static Scada.Comm.Drivers.DrvFtpJP.OperationAction;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Contains localized driver phrases.
    /// <para>Содержит локализованные фразы драйвера.</para>
    /// </summary>
    public static class DriverDictonary
    {
        #region Property
        public static string StartDriver = Locale.IsRussian ? "Запуск драйвера" : "Launching the driver"; // start driver phrase
        public static string Timeout = Locale.IsRussian ? "Таймаут" : "Timeout"; // timeout phrase
        public static string Delay = Locale.IsRussian ? "Пауза" : "Delay"; // delay phrase
        public static string Period = Locale.IsRussian ? "Период" : "Period"; // period phrase
        public static string ProjectName = Locale.IsRussian ? "Проект" : "Project"; // project name phrase
        public static string ProjectNo = Locale.IsRussian ?
            "Количество тегов не было получено т.к. конфигурационный файл не был загружен" :
            "The number of tags was not received because the configuration file was not loaded"; // project not loaded phrase
        public static string TagsIsNull = Locale.IsRussian ? "Список тегов пустой" : "The tag list is empty"; // empty tag list phrase
        public static string TagsCount = Locale.IsRussian ? "Количество тегов" : "Number of tags"; // tag count phrase
        public static string Error = Locale.IsRussian ? "Ошибка" : "Error"; // error phrase
        public static string ErrorMessage = Locale.IsRussian ? "Ошибка при выполнении: {0}" : "Error executing: {0}"; // error message format
        public static string ErrorCount = Locale.IsRussian ? "Количество ошибок" : "Count Error"; // error count phrase
        public static string ErrorSetData = Locale.IsRussian ? "Ошибка при установке данных тега" : "Error setting tag data"; // set data error phrase
        public static string Status = Locale.IsRussian ? "Статус" : "Status"; // status phrase
        public static string FileNoFound = Locale.IsRussian ? "Файл проекта не был найден!" : "The project file was not found!"; // file not found phrase
        public static string FileLenghtZero = Locale.IsRussian ? "Файл проекта пустой!" : "The project file is empty!"; // empty file phrase
        public static string RestartLine = Locale.IsRussian ? "Перезапуск линии" : "Restart Line"; // restart line phrase
        public static string QuestDelete = Locale.IsRussian ? "Вы действительно хотите удалить '{0}'?" : "Are you sure you want to delete '{0}'?"; // delete question format
        public static string ConfirmDelete = Locale.IsRussian ? "Подтверждение удаления" : "Confirm deletion"; // delete confirmation phrase
        public static string DirectoryDoesNotExist = Locale.IsRussian ? "Указанный каталог '{0}' не существует." : "The specified directory '{0}' does not exist."; // directory not found format
        public static string DirectoryDelete = Locale.IsRussian ? "Каталог '{0}' удален." : "Directory '{0}' has been removed."; // directory deleted format
        public static string DirectoryZip = Locale.IsRussian ? "Каталог '{folder.PathFile}' был успешно сжат в архив '{zipFileName}'." : "The directory '{0}' was successfully compressed into archive '{1}'."; // directory zipped format
        public static string MoveZip = Locale.IsRussian ? "Архив '{0}' перенесён в каталог '{1}'." : "Archive '{0}' moved to directory '{1}'."; // archive moved format
        public static string DiskInfo = Locale.IsRussian ? "{0} ({1}) [{2} / {3}]" : "{0} ({1}) [{2} / {3}]"; // disk info format
        public static string DiskError = Locale.IsRussian ? "Ошибка при получении информации о дисках: {0}." : "Error retrieving disk information: {0}."; // disk error format
        public static string DiskErrorMessage = Locale.IsRussian ? "Ошибка при получении информации о дисках!" : "Error retrieving disk information!"; // disk error phrase
        public static string Computer = Locale.IsRussian ? "Компьютер" : "Computer"; // computer phrase
        public static string Dir = Locale.IsRussian ? "Папка" : "Directory"; // directory phrase
        public static string ColumnName = Locale.IsRussian ? "Название" : "Name"; // name column phrase
        public static string ColumnType = Locale.IsRussian ? "Тип" : "Type"; // type column phrase
        public static string ColumnSize = Locale.IsRussian ? "Размер" : "Size"; // size column phrase
        public static string ColumnChange = Locale.IsRussian ? "Изменён" : "Change"; // change column phrase
        public static string ColumnTotalSize = Locale.IsRussian ? "Общий размер" : "Total size"; // total size column phrase
        public static string ColumnFreeSpace = Locale.IsRussian ? "Свободное место" : "Free space"; // free space column phrase
        public static string ColumnOccupiedSpace = Locale.IsRussian ? "Занятое место" : "Occupied space"; // occupied space column phrase
        public static string ScenarioName = Locale.IsRussian ? "Сценарий" : "Scenario"; // scenario phrase
        #endregion Property

        #region Basic
        /// <summary>
        /// Gets connection status text for the menu.
        /// <para>Получает текст статуса подключения для меню.</para>
        /// </summary>
        /// <param name="serverName">Server name.</param>
        /// <param name="serverHost">Server host.</param>
        /// <param name="username">User name.</param>
        /// <returns>Connection status text.</returns>
        public static string MenuStatus(string serverName, string serverHost, string username)
        {
            return Locale.IsRussian ?
                @$"Подключено к {serverName} ({serverHost}) с пользователем {username}" :
                @$"Connected to {serverName} ({serverHost}) with user {username}";
        }

        /// <summary>
        /// Gets localized file system object type text.
        /// <para>Получает локализованный текст типа объекта файловой системы.</para>
        /// </summary>
        /// <param name="type">File system object type.</param>
        /// <returns>Localized object type text.</returns>
        public static string FilesDirectoriesTypeString(FilesDirectoriesType type)
        {
            switch (type)
            {
                case FilesDirectoriesType.None:
                    return Locale.IsRussian ? string.Empty : string.Empty;
                case FilesDirectoriesType.File:
                    return Locale.IsRussian ? "Файл" : "File";
                case FilesDirectoriesType.Link:
                    return Locale.IsRussian ? "Ссылка" : "Link";
                case FilesDirectoriesType.Directory:
                    return Locale.IsRussian ? "Каталог" : "Directory";
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets localized operation action type text.
        /// <para>Получает локализованный текст типа действия операции.</para>
        /// </summary>
        /// <param name="type">Operation action type.</param>
        /// <returns>Localized action type text.</returns>
        public static string OperationsActionsTypeString(OperationsActions type)
        {
            switch (type)
            {
                case OperationsActions.None:
                    return Locale.IsRussian ? "Ничего" : "None";
                case OperationsActions.LocalCreateDirectory:
                    return Locale.IsRussian ? "Создать локально каталог" : "Create local directory";
                case OperationsActions.RemoteCreateDirectory:
                    return Locale.IsRussian ? "Создать удалённо каталог" : "Create remote directory ";
                case OperationsActions.LocalRename:
                    return Locale.IsRussian ? "Переименовать локально" : "Rename local";
                case OperationsActions.RemoteRename:
                    return Locale.IsRussian ? "Переименовать удалённо" : "Rename remote";
                case OperationsActions.LocalDeleteFile:
                    return Locale.IsRussian ? "Удалить локально файл" : "Delete local file";
                case OperationsActions.LocalDeleteDirectory:
                    return Locale.IsRussian ? "Удалить локально каталог" : "Delete local directory";
                case OperationsActions.RemoteDeleteFile:
                    return Locale.IsRussian ? "Удалить удалённо файл" : "Delete remote file";
                case OperationsActions.RemoteDeleteDirectory:
                    return Locale.IsRussian ? "Удалить удалённо каталог" : "Delete remote directory";
                case OperationsActions.LocalUploadFile:
                    return Locale.IsRussian ? "Загрузить локальный файл" : "Upload local file";
                case OperationsActions.LocalUploadDirectory:
                    return Locale.IsRussian ? "Загрузить локальный каталог" : "Load local directory";
                case OperationsActions.RemoteDownloadFile:
                    return Locale.IsRussian ? "Скачать удалённый файл" : "Download remote file";
                case OperationsActions.RemoteDownloadDirectory:
                    return Locale.IsRussian ? "Скачать удалёный каталог" : "Download remote directory";
            }

            return string.Empty;
        }
        #endregion Basic
    }
}