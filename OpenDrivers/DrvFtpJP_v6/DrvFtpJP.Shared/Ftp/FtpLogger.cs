using DebugerLog;
using FluentFTP;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Writes FluentFTP log entries to the driver log.
    /// <para>Записывает сообщения FluentFTP в журнал драйвера.</para>
    /// </summary>
    public class FtpLogger : IFtpLogger
    {
        #region Property
        /// <summary>
        /// Gets or sets the log level.
        /// <para>Возвращает или задает уровень журнала.</para>
        /// </summary>
        public LogLevel Level { get; set; }
        #endregion Property

        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public FtpLogger()
        {
            Level = LogLevel.None;
        }

        /// <summary>
        /// Writes a FluentFTP log entry.
        /// <para>Записывает сообщение журнала FluentFTP.</para>
        /// </summary>
        /// <param name="entry">FTP log entry.</param>
        public void Log(FtpLogEntry entry)
        {
            string message = string.Empty;
            switch (entry.Severity)
            {
                case FtpTraceLevel.Info:
                    message = $@"{entry.Message} {entry.Exception}";
                    Log(LogLevel.Info, message.Trim());
                    break;
                case FtpTraceLevel.Warn:
                    message = $@"{entry.Message} {entry.Exception}";
                    Log(LogLevel.Warning, message.Trim());
                    break;
                case FtpTraceLevel.Error:
                    message = $@"{entry.Message} {entry.Exception}";
                    Log(LogLevel.Info, message.Trim());
                    break;
                case FtpTraceLevel.Verbose:
                    message = $@"{entry.Message} {entry.Exception}";
                    Log(LogLevel.Info, message.Trim());
                    break;
            }
        }

        /// <summary>
        /// Writes a log message according to the specified level.
        /// <para>Записывает сообщение журнала с указанным уровнем.</para>
        /// </summary>
        /// <param name="level">Log level.</param>
        /// <param name="message">Log message.</param>
        public void Log(LogLevel level, string message)
        {
            switch (level)
            {
                case LogLevel.None:
                    break;
                case LogLevel.Info:
                    Debuger.Log(message);
                    break;
                case LogLevel.Warning:
                    Debuger.Log(message);
                    break;
                case LogLevel.Error:
                    Debuger.Log(message);
                    break;
                case LogLevel.Verbose:
                    Debuger.Log(message);
                    break;
                case LogLevel.Detailed:
                    Debuger.Log(message);
                    break;
            }
        }
        #endregion Basic

        #region Support class
        /// <summary>
        /// Defines driver log levels.
        /// <para>Определяет уровни журнала драйвера.</para>
        /// </summary>
        public enum LogLevel : int
        {
            /// <summary>
            /// Logging is disabled.
            /// <para>Запись журнала отключена.</para>
            /// </summary>
            None = 0,

            /// <summary>
            /// Error messages.
            /// <para>Сообщения об ошибках.</para>
            /// </summary>
            Error = 1,

            /// <summary>
            /// Warning messages.
            /// <para>Предупреждения.</para>
            /// </summary>
            Warning = 2,

            /// <summary>
            /// Informational messages.
            /// <para>Информационные сообщения.</para>
            /// </summary>
            Info = 3,

            /// <summary>
            /// Verbose messages.
            /// <para>Подробные сообщения.</para>
            /// </summary>
            Verbose = 4,

            /// <summary>
            /// Detailed messages.
            /// <para>Детальные сообщения.</para>
            /// </summary>
            Detailed = 5,
        }
        #endregion Support class
    }
}