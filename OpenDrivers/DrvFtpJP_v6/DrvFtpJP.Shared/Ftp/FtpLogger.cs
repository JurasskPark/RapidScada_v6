using DebugerLog;
using FluentFTP;
    using System;
    using System.IO;
    using System.Text;

public class FtpLogger : IFtpLogger
{
    private readonly StringBuilder logBuffer = new StringBuilder();

    public FtpLogger()
    {
        Level = LogLevel.None;
    }

    public LogLevel Level { get; set; }

    public void Log(FtpLogEntry entry)
    {
        FtpLogEntry ftpLogEntry = entry;
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

    public enum LogLevel : int
    {
        None = 0,
        Error = 1,
        Warning = 2,
        Info = 3,
        Verbose = 4,
        Detailed = 5,
    }
}

