using FluentFTP;
using Microsoft.Extensions.Logging;

namespace Examples
{
    /// <summary>
    /// Connects the bundled FluentFTP source to Microsoft.Extensions.Logging providers.
    /// </summary>
    internal sealed class FtpLogAdapter : IFtpLogger
    {
        private readonly ILogger logger;

        public FtpLogAdapter(ILogger logger)
        {
            this.logger = logger;
        }

        public void Log(FtpLogEntry entry)
        {
            LogLevel level = entry.Severity switch
            {
                FtpTraceLevel.Error => LogLevel.Error,
                FtpTraceLevel.Warn => LogLevel.Warning,
                FtpTraceLevel.Info => LogLevel.Information,
                _ => LogLevel.Debug
            };

            logger.Log(level, entry.Exception, "{Message}", entry.Message);
        }
    }
}
