using Scada.Lang;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
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

        public static string DirectoryDoesNotExist = Locale.IsRussian ? "Указанный каталог '{0}' не существует." : "The specified directory '{0}' does not exist.";
        public static string DirectoryDelete = Locale.IsRussian ? "Каталог '{0}' удален." : "Directory '{0}' has been removed.";
        public static string DirectoryZip = Locale.IsRussian ? "Каталог '{folder.PathFile}' был успешно сжат в архив '{zipFileName}'." : "The directory '{0}' was successfully compressed into archive '{1}'.";
        public static string MoveZip = Locale.IsRussian ? "Архив '{0}' перенесён в каталог '{1}'." : "Archive '{0}' moved to directory '{1}'.";
        public static string DiskInfo = Locale.IsRussian ? "{0} ({1}) [{2} / {3}]" : "{0} ({1}) [{2} / {3}]";
        public static string DiskError = Locale.IsRussian ? "Ошибка при получении информации о дисках: {0}." : "Error retrieving disk information: {0}.";
    }
}