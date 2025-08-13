using Scada.Lang;
using static DrvFtpJP.Shared.FilesDirectorys.FilesDirectoriesInformation;
using static Scada.Comm.Drivers.DrvFtpJP.OperationAction;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    public static class DriverDictonary
    {
        public static string StartDriver = Locale.IsRussian? "Запуск драйвера" : "Launching the driver";
        public static string Timeout = Locale.IsRussian ? "Таймаут" : "Timeout";
        public static string Delay = Locale.IsRussian ? "Пауза" : "Delay";
        public static string Period = Locale.IsRussian? "Период" : "Period";

        public static string ProjectName = Locale.IsRussian ? "Проект" : "Project";
        public static string ProjectNo = Locale.IsRussian ?
            "Количество тегов не было получено т.к. конфигурационный файл не был загружен" :
            "The number of tags was not received because the configuration file was not loaded";

        public static string TagsIsNull = Locale.IsRussian ? "Список тегов пустой" : "The tag list is empty";
        public static string TagsCount = Locale.IsRussian ? "Количество тегов" : "Number of tags";

        public static string Error = Locale.IsRussian ? "Ошибка" : "Error";
        public static string ErrorMessage = Locale.IsRussian ? "Ошибка при выполнении: {0}" : "Error executing: {0}";
        public static string ErrorCount = Locale.IsRussian ? "Количество ошибок" : "Count Error";
        public static string ErrorSetData = Locale.IsRussian ? "Ошибка при установке данных тега" : "Error setting tag data";

        public static string Status = Locale.IsRussian ? "Статус" : "Status";

        public static string FileNoFound = Locale.IsRussian ? "Файл проекта не был найден!" : "The project file was not found!";
        public static string FileLenghtZero = Locale.IsRussian ? "Файл проекта пустой!" : "The project file is empty!";

        public static string RestartLine = Locale.IsRussian ? "Перезапуск линии" : "Restart Line";

        public static string QuestDelete = Locale.IsRussian ? "Вы действительно хотите удалить '{0}'?" : "Are you sure you want to delete '{0}'?";
        public static string ConfirmDelete = Locale.IsRussian ? "Подтверждение удаления" : "Confirm deletion";

        public static string DirectoryDoesNotExist = Locale.IsRussian ? "Указанный каталог '{0}' не существует." : "The specified directory '{0}' does not exist.";
        public static string DirectoryDelete = Locale.IsRussian ? "Каталог '{0}' удален." : "Directory '{0}' has been removed.";
        public static string DirectoryZip = Locale.IsRussian ? "Каталог '{folder.PathFile}' был успешно сжат в архив '{zipFileName}'." : "The directory '{0}' was successfully compressed into archive '{1}'.";
        public static string MoveZip = Locale.IsRussian ? "Архив '{0}' перенесён в каталог '{1}'." : "Archive '{0}' moved to directory '{1}'.";
        public static string DiskInfo = Locale.IsRussian ? "{0} ({1}) [{2} / {3}]" : "{0} ({1}) [{2} / {3}]";
        public static string DiskError = Locale.IsRussian ? "Ошибка при получении информации о дисках: {0}." : "Error retrieving disk information: {0}.";
        public static string DiskErrorMessage = Locale.IsRussian ? "Ошибка при получении информации о дисках!" : "Error retrieving disk information!";

        public static string Computer = Locale.IsRussian ? "Компьютер" : "Computer";
        public static string Dir = Locale.IsRussian ? "Папка" : "Directory";

        public static string ColumnName = Locale.IsRussian ? "Название" : "Name";
        public static string ColumnType = Locale.IsRussian ? "Тип" : "Type";
        public static string ColumnSize = Locale.IsRussian ? "Размер" : "Size";
        public static string ColumnChange = Locale.IsRussian ? "Изменён" : "Change";
        public static string ColumnTotalSize = Locale.IsRussian ? "Общий размер" : "Total size";
        public static string ColumnFreeSpace = Locale.IsRussian ? "Свободное место" : "Free space";
        public static string ColumnOccupiedSpace = Locale.IsRussian ? "Занятое место" : "Occupied space";

        public static string ScenarioName = Locale.IsRussian ? "Сценарий" : "Scenario";

        public static string MenuStatus(string serverName, string serverHost, string Username)
        {
            string result = string.Empty;
            return result = Locale.IsRussian ? @$"Подключено к {serverName} ({serverHost}) с пользователем {Username}" : @$"Connected to {serverName} ({serverHost}) with user {Username}";
        }

        public static string FilesDirectoriesTypeString(FilesDirectoriesType type)
        {
            string result = string.Empty;

            switch(type)
            {
                case FilesDirectoriesType.None:
                    return result = Locale.IsRussian ? "" : "";
                case FilesDirectoriesType.File:
                    return result = Locale.IsRussian ? "Файл" : "File";
                case FilesDirectoriesType.Link:
                    return result = Locale.IsRussian ? "Ссылка" : "Link";
                case FilesDirectoriesType.Directory:
                    return result = Locale.IsRussian ? "Каталог" : "Directory";
            }

            return result;
        }

        public static string OperationsActionsTypeString(OperationsActions type)
        {
            string result = string.Empty;

            switch (type)
            {
                case OperationsActions.None:
                    return result = Locale.IsRussian ? "Ничего" : "None";
                case OperationsActions.LocalCreateDirectory:
                    return result = Locale.IsRussian ? "Создать локально каталог" : "Create local directory";
                case OperationsActions.RemoteCreateDirectory:
                    return result = Locale.IsRussian ? "Создать удалённо каталог" : "Create remote directory ";
                case OperationsActions.LocalRename:
                    return result = Locale.IsRussian ? "Переименовать локально" : "Rename local";
                case OperationsActions.RemoteRename:
                    return result = Locale.IsRussian ? "Переименовать удалённо" : "Rename remote";
                case OperationsActions.LocalDeleteFile:
                    return result = Locale.IsRussian ? "Удалить локально файл" : "Delete local file";
                case OperationsActions.LocalDeleteDirectory:
                    return result = Locale.IsRussian ? "Удалить локально каталог" : "Delete local directory";
                case OperationsActions.RemoteDeleteFile:
                    return result = Locale.IsRussian ? "Удалить удалённо файл" : "Delete remote file";
                case OperationsActions.RemoteDeleteDirectory:
                    return result = Locale.IsRussian ? "Удалить удалённо каталог" : "Delete remote directory";
                case OperationsActions.LocalUploadFile:
                    return result = Locale.IsRussian ? "Загрузить локальный файл" : "Upload local file";
                case OperationsActions.LocalUploadDirectory:
                    return result = Locale.IsRussian ? "Загрузить локальный каталог" : "Load local directory";
                case OperationsActions.RemoteDownloadFile:
                    return result = Locale.IsRussian ? "Скачать удалённый файл" : "Download remote file";
                case OperationsActions.RemoteDownloadDirectory:
                    return result = Locale.IsRussian ? "Скачать удалёный каталог" : "Download remote directory";
            }

            return result;
        }
    }
}