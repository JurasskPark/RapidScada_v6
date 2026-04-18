using System;
using System.Collections.Generic;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// Defines shared driver client operations.
    /// <para>Определяет общие операции клиента драйвера.</para>
    /// </summary>
    public interface IDriverClient : IDisposable
    {
        #region Property

        /// <summary>
        /// Gets a value indicating whether the client has any active connections.
        /// <para>Возвращает значение, указывающее, есть ли у клиента активные подключения.</para>
        /// </summary>
        bool IsConnected { get; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Connects the client to the underlying DDE service.
        /// <para>Подключает клиент к базовому DDE-сервису.</para>
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnects the client from the underlying DDE service.
        /// <para>Отключает клиент от базового DDE-сервиса.</para>
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Reads a value for the specified project tag.
        /// <para>Считывает значение для указанного тега проекта.</para>
        /// </summary>
        /// <param name="tag">The project tag to read.</param>
        /// <returns>The string value returned by the DDE service.</returns>
        string ReadTag(ProjectTag tag);

        /// <summary>
        /// Reads values for a collection of project tags.
        /// <para>Считывает значения для коллекции тегов проекта.</para>
        /// </summary>
        /// <param name="tags">Enumerable of project tags to read.</param>
        /// <returns>A dictionary mapping tag Ids (<see cref="Guid"/>) to their string values.</returns>
        Dictionary<Guid, string> ReadTags(IEnumerable<ProjectTag> tags);

        /// <summary>
        /// Resolves the DDE topic for the given project tag.
        /// <para>Определяет topic для указанного тега проекта.</para>
        /// </summary>
        /// <param name="tag">The project tag.</param>
        /// <returns>The resolved topic string or empty string if not set.</returns>
        string ResolveTopic(ProjectTag tag);

        /// <summary>
        /// Resolves the DDE item name for the given project tag.
        /// <para>Возвращает имя item для указанного тега проекта.</para>
        /// </summary>
        /// <param name="tag">The project tag.</param>
        /// <returns>The resolved item name or empty string if not set.</returns>
        string ResolveItemName(ProjectTag tag);

        #endregion Basic
    }
}
