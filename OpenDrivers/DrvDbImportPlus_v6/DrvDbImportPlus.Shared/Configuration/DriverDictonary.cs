using Scada.Lang;

namespace Scada.Comm.Drivers.DrvDbImportPlus
{
    public static class DriverDictonary
    {
        public static string StartDriver = Locale.IsRussian? "Запуск драйвера" : "Launching the driver";
        public static string Driver = Locale.IsRussian ? "Драйвер" : "Driver";
        public static string Version = Locale.IsRussian ? "Версия" : "Version";
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
    }
}