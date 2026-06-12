using FluentFTP;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Sends file transfer progress messages to subscribers.
    /// <para>Передает подписчикам сообщения о прогрессе передачи файлов.</para>
    /// </summary>
    internal class DebugerFilesReturn
    {
        #region Basic
        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public DebugerFilesReturn()
        {
        }

        /// <summary>
        /// Sends file transfer progress to subscribers.
        /// <para>Передает подписчикам прогресс передачи файла.</para>
        /// </summary>
        /// <param name="progress">FTP transfer progress.</param>
        /// <param name="direction">Transfer direction.</param>
        public void Log(FtpProgress progress, string direction)
        {
            DebugerFilesLog(progress, direction);
        }

        /// <summary>
        /// Invokes the file transfer progress callback.
        /// <para>Вызывает callback прогресса передачи файла.</para>
        /// </summary>
        /// <param name="progress">FTP transfer progress.</param>
        /// <param name="direction">Transfer direction.</param>
        internal void DebugerFilesLog(FtpProgress progress, string direction)
        {
            if (OnDebug == null)
            {
                return;
            }

            OnDebug(progress, direction);
        }
        #endregion Basic

        #region Support class
        /// <summary>
        /// Occurs when file transfer progress is available.
        /// <para>Возникает при появлении прогресса передачи файла.</para>
        /// </summary>
        public static DebugData OnDebug;

        /// <summary>
        /// Represents a file transfer progress callback.
        /// <para>Представляет callback прогресса передачи файла.</para>
        /// </summary>
        /// <param name="progress">FTP transfer progress.</param>
        /// <param name="direction">Transfer direction.</param>
        public delegate void DebugData(FtpProgress progress, string direction);
        #endregion Support class
    }
}