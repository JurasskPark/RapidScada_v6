using System.Text;

namespace SampleServer;

internal static class FileLog
{
    private static readonly object SyncRoot = new object();
    private static string _logPath;

    public static void Initialize(string baseDirectory)
    {
        string logDir = Path.Combine(baseDirectory, "logs");
        Directory.CreateDirectory(logDir);
        _logPath = Path.Combine(logDir, "SampleServer.log");
        Write("Log initialized.");
    }

    public static void Write(string message)
    {
        try
        {
            lock (SyncRoot)
            {
                if (string.IsNullOrWhiteSpace(_logPath))
                {
                    return;
                }

                string line = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} {message}{Environment.NewLine}";
                File.AppendAllText(_logPath, line, Encoding.UTF8);
            }
        }
        catch
        {
            // Logging must not break service runtime.
        }
    }
}
