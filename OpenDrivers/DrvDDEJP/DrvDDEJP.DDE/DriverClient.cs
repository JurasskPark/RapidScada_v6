using DdeNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// Provides DDE client access.
    /// <para>Предоставляет доступ к DDE клиенту.</para>
    /// </summary>
    public sealed class DriverClient : IDriverClient
#if !NETFRAMEWORK
        , IDisposable
#endif
    {
        #region Variable

        private readonly Project project;                           // driver project configuration
        private readonly Dictionary<string, DdeClient> clients;     // active DDE clients indexed by topic
        private readonly Action<string> logAction;                  // action to log messages
        private bool disposed;                                      // indicates whether the object is disposed

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets a value indicating whether there are active client connections.
        /// <para>Возвращает значение, указывающее, есть ли активные подключения клиентов.</para>
        /// </summary>
        public bool IsConnected => clients.Count > 0;

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        /// <param name="project">The project configuration.</param>
        /// <param name="logAction">The logging action.</param>
        public DriverClient(Project project, Action<string> logAction = null)
        {
            this.project = project ?? throw new ArgumentNullException(nameof(project));
            this.logAction = logAction;
            this.clients = new Dictionary<string, DdeClient>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Connects all DDE clients listed in the project configuration.
        /// <para>Подключает всех DDE клиентов, перечисленных в конфигурации проекта.</para>
        /// </summary>
        public void Connect()
        {
            ThrowIfDisposed();
            Log("[DriverClient] Connect requested.");

            foreach (string topic in EnumerateTopics())
            {
                Log($"[DriverClient] Connecting topic '{topic}'.");
                GetOrCreateClient(topic);
            }
        }

        /// <summary>
        /// Disconnects and releases all DDE clients.
        /// <para>Отключает и освобождает все DDE клиенты.</para>
        /// </summary>
        public void Disconnect()
        {
            Log("[DriverClient] Disconnect requested.");

            foreach (DdeClient client in clients.Values)
            {
                try
                {
                    client.Dispose();
                }
                catch
                {
                    // ignore disposal errors
                }
            }

            clients.Clear();
        }

        /// <summary>
        /// Reads the value for the specified tag.
        /// <para>Выполняет запрос значения по тегу.</para>
        /// </summary>
        /// <param name="tag">The project tag.</param>
        /// <returns>The string value returned by the DDE service.</returns>
        public string ReadTag(ProjectTag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            string topic = ResolveTopic(tag);
            string itemName = ResolveItemName(tag);
            string serviceName = project.ServiceName;
            int requestTimeout = project.RequestTimeout;

            if (string.IsNullOrWhiteSpace(topic))
            {
                throw new InvalidOperationException("DDE topic is not configured.");
            }

            if (string.IsNullOrWhiteSpace(itemName))
            {
                throw new InvalidOperationException("DDE item is not configured.");
            }

            Log($"[DriverClient] Request {serviceName}|{topic}!{itemName}");
            DdeClient client = GetOrCreateClient(topic);
            string value = (client.Request(itemName, requestTimeout) ?? string.Empty).TrimEnd('\0', '\r', '\n');
            Log($"[DriverClient] Response {serviceName}|{topic}!{itemName} = {value}");
            return value;
        }

        /// <summary>
        /// Reads values for the specified tags.
        /// <para>Считывает значения для набора тегов.</para>
        /// </summary>
        /// <param name="tags">The collection of tags.</param>
        /// <returns>A dictionary of tag IDs and their values.</returns>
        public Dictionary<Guid, string> ReadTags(IEnumerable<ProjectTag> tags)
        {
            Dictionary<Guid, string> result = new Dictionary<Guid, string>();
            if (tags == null)
            {
                return result;
            }

            foreach (ProjectTag tag in tags)
            {
                if (tag == null || !tag.Enabled)
                {
                    continue;
                }

                try
                {
                    result[tag.Id] = ReadTag(tag);
                }
                catch (Exception ex)
                {
                    Log($"[DriverClient] Error reading tag '{tag.Name}': {ex.Message}");
                    result[tag.Id] = string.Empty;
                }
            }

            return result;
        }

        /// <summary>
        /// Resolves the topic for the specified tag.
        /// <para>Определяет топик для указанного тега.</para>
        /// </summary>
        public string ResolveTopic(ProjectTag tag)
        {
            if (tag == null)
            {
                return string.Empty;
            }

            return !string.IsNullOrWhiteSpace(tag.Topic)
                ? tag.Topic.Trim()
                : project.DefaultTopic.Trim();
        }

        /// <summary>
        /// Resolves the item name for the specified tag.
        /// <para>Возвращает имя элемента для тега.</para>
        /// </summary>
        public string ResolveItemName(ProjectTag tag)
        {
            return tag?.ItemName?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Releases the resources used by the class.
        /// <para>Освобождает ресурсы, используемые классом.</para>
        /// </summary>
        public void Dispose()
        {
            if (disposed)
            {
                return;
            }

            Disconnect();
            disposed = true;
        }

        #endregion Basic

        #region Private Methods

        /// <summary>
        /// Gets an existing client for the topic or creates a new one.
        /// <para>Получает существующий клиент для топика или создает новый.</para>
        /// </summary>
        private DdeClient GetOrCreateClient(string topic)
        {
            if (!clients.TryGetValue(topic, out DdeClient client))
            {
                string serviceName = project.ServiceName;
                Log($"[DriverClient] Creating client for service '{serviceName}', topic '{topic}'.");
                client = new DdeClient(serviceName, topic);
                client.Connect();
                clients.Add(topic, client);
                Log($"[DriverClient] Connected service '{serviceName}', topic '{topic}'.");
                return client;
            }

            if (!client.IsConnected)
            {
                string serviceName = project.ServiceName;
                Log($"[DriverClient] Reconnecting service '{serviceName}', topic '{topic}'.");
                client.Connect();
                Log($"[DriverClient] Reconnected service '{serviceName}', topic '{topic}'.");
            }

            return client;
        }

        /// <summary>
        /// Enumerates unique topics in the project tags.
        /// <para>Перечисляет уникальные топики в тегах проекта.</para>
        /// </summary>
        private IEnumerable<string> EnumerateTopics()
        {
            HashSet<string> topics = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            if (project.Tags != null)
            {
                foreach (ProjectTag tag in project.Tags)
                {
                    string topic = ResolveTopic(tag);
                    if (!string.IsNullOrWhiteSpace(topic))
                    {
                        topics.Add(topic);
                    }
                }
            }

            if (topics.Count == 0 && !string.IsNullOrWhiteSpace(project.DefaultTopic))
            {
                topics.Add(project.DefaultTopic.Trim());
            }

            return topics;
        }

        /// <summary>
        /// Throws an exception if the object is disposed.
        /// <para>Генерирует исключение, если объект освобожден.</para>
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(DriverClient));
            }
        }

        /// <summary>
        /// Logs the specified text.
        /// <para>Записывает указанный текст в лог.</para>
        /// </summary>
        private void Log(string text)
        {
            logAction?.Invoke(text);
        }

        #endregion Private Methods
    }
}
