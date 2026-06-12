namespace Scada.Comm.Drivers.DrvFtpJP
{
    /// <summary>
    /// Defines FTP action types.
    /// <para>Определяет типы FTP-действий.</para>
    /// </summary>
    public enum FtpAction : int
    {
        /// <summary>
        /// Create a directory.
        /// <para>Создание каталога.</para>
        /// </summary>
        CreateDirectory = 0,

        /// <summary>
        /// Delete a directory.
        /// <para>Удаление каталога.</para>
        /// </summary>
        DeleteDirectory = 1,

        /// <summary>
        /// Delete a file.
        /// <para>Удаление файла.</para>
        /// </summary>
        DeleteFile = 3,

        /// <summary>
        /// Download a directory.
        /// <para>Скачивание каталога.</para>
        /// </summary>
        DownloadDirectory = 3,

        /// <summary>
        /// Download a file.
        /// <para>Скачивание файла.</para>
        /// </summary>
        DownloadFile = 4,

        /// <summary>
        /// Upload a directory.
        /// <para>Загрузка каталога.</para>
        /// </summary>
        UploadDirectory = 2,

        /// <summary>
        /// Upload a file.
        /// <para>Загрузка файла.</para>
        /// </summary>
        UploadFile = 8,

        /// <summary>
        /// Execute an action.
        /// <para>Выполнение действия.</para>
        /// </summary>
        Execute = 5,

        /// <summary>
        /// Rename an object.
        /// <para>Переименование объекта.</para>
        /// </summary>
        Rename = 6,
    }
}