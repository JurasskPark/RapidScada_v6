using Scada.Comm.Config;
using Scada.Comm.Devices;
using Scada.Comm.Drivers.DrvDDEJP;
using Scada.Data.Const;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Scada.Comm.Drivers.DrvDDEJP.Logic
{
    /// <summary>
    /// Provides device logic for the DrvDDEJP driver.
    /// <para>Реализует логику устройства для драйвера DrvDDEJP.</para>
    /// </summary>
    internal class DevDDEJPLogic : DeviceLogic
    {
        #region Variable

        private readonly Project project;                           // the driver project configuration
        private readonly string projectFileName;                    // the project file path
        private readonly List<ProjectTag> tags;                     // enabled project tags ordered by order
        private readonly Dictionary<string, DateTime> topicErrors;  // topic error timestamps to throttle messages
        private readonly Dictionary<Guid, DeviceTag> deviceTagsById;// mapping from project tag Id to device tag
        private readonly IDriverClient driverClient;                // driver client for DDE requests

        #endregion Variable

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        /// <param name="commContext">The communication context.</param>
        /// <param name="lineContext">The line context.</param>
        /// <param name="deviceConfig">The device configuration.</param>
        public DevDDEJPLogic(ICommContext commContext, ILineContext lineContext, DeviceConfig deviceConfig)
            : base(commContext, lineContext, deviceConfig)
        {
            CanSendCommands = false;
            ConnectionRequired = false;

            projectFileName = Path.Combine(commContext.AppDirs.ConfigDir, Project.GetFileName(DeviceNum));
            project = new Project();

            if (!project.Load(projectFileName, out string errMsg))
            {
                LogDriver(errMsg);
            }

            tags = project.Tags?.Where(t => t != null && t.Enabled).OrderBy(t => t.Order).ToList() ?? new List<ProjectTag>();
            topicErrors = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);
            deviceTagsById = new Dictionary<Guid, DeviceTag>();
            driverClient = DriverClientFactory.Create(project, LogDriver);
        }

        /// <summary>
        /// Called when the communication line starts.
        /// <para>Вызывается при запуске линии связи.</para>
        /// </summary>
        public override void OnCommLineStart()
        {
            LogDriver(Locale.IsRussian ? "Запуск DDE клиента" : "Starting DDE client");
            LogDriver($"[Driver {DriverUtils.DriverCode}]");
            LogDriver($"[Service {project.ServiceName}]");
            LogDriver($"[Default topic {project.DefaultTopic}]");
            LogDriver($"[Request timeout {project.RequestTimeout}]");
        }

        /// <summary>
        /// Called when the communication line terminates.
        /// <para>Вызывается при завершении работы линии связи.</para>
        /// </summary>
        public override void OnCommLineTerminate()
        {
            driverClient.Dispose();
            topicErrors.Clear();
            LogDriver(Locale.IsRussian ? "Останов DDE клиента" : "Stopping DDE client");
        }

        /// <summary>
        /// Initializes device tags from project tags.
        /// <para>Инициализирует теги устройства на основе тегов проекта.</para>
        /// </summary>
        public override void InitDeviceTags()
        {
            DeviceTags.FlattenGroups = true;
            deviceTagsById.Clear();

            TagGroup numericGroup = new TagGroup(Locale.IsRussian ? "Теги" : "Tags");
            TagGroup stringGroup = new TagGroup(Locale.IsRussian ? "Строковые теги" : "String tags");

            int index = 0;
            foreach (ProjectTag tag in tags)
            {
                TagGroup targetGroup = IsStringFormat(tag.DataFormat) ? stringGroup : numericGroup;
                DeviceTag deviceTag = targetGroup.AddTag(GetTagCode(tag), tag.Name);
                deviceTag.Index = index++;
                deviceTag.DataType = GetTagDataType(tag);
                deviceTag.DataLen = GetTagDataLen(tag);
                deviceTag.Format = GetTagFormat(tag);
                deviceTag.Aux = tag;
                deviceTagsById[tag.Id] = deviceTag;
            }

            DeviceTags.AddGroup(numericGroup);
            if (stringGroup.DeviceTags.Count > 0)
            {
                DeviceTags.AddGroup(stringGroup);
            }

            DeviceData.Init();
        }

        /// <summary>
        /// Main session logic: polls DDE tags and processes values.
        /// <para>Основная логика сессии: опрашивает DDE теги и обрабатывает значения.</para>
        /// </summary>
        public override void Session()
        {
            base.Session();

            try
            {
                LogDriver($"[SESSION] Device={DeviceNum}, Tags={tags.Count}, Time={DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                PollDdeTags();
                LastRequestOK = true;
                LogDriver("[SESSION] Completed successfully.");
            }
            catch (Exception ex)
            {
                LastRequestOK = false;
                LogDriver(string.Format(Locale.IsRussian ? "Ошибка в сессии: {0}" : "Session error: {0}", ex.Message));
                Log.WriteError(ex, Locale.IsRussian ? "Ошибка при выполнении сессии драйвера DrvDDEJP" : "Error executing DrvDDEJP driver session");
            }

            FinishRequest();
            SleepPollingDelay();
            FinishSession();
        }

        #endregion Basic

        #region Private Methods

        /// <summary>
        /// Polls configured DDE tags and sets decoded values to device data.
        /// <para>Опрос настроенных DDE тегов и запись декодированных значений в данные устройства.</para>
        /// </summary>
        private void PollDdeTags()
        {
            foreach (ProjectTag tag in tags)
            {
                string topic = ResolveTopic(tag);
                string itemName = ResolveItemName(tag);
                LogDriver($"[POLL] Tag='{tag.Name}', Channel={tag.Channel}, Topic='{topic}', Item='{itemName}'.");

                if (string.IsNullOrWhiteSpace(topic) || string.IsNullOrWhiteSpace(itemName))
                {
                    LogDriver(Locale.IsRussian
                        ? $"Тег {tag.Name}: не заданы topic/item"
                        : $"Tag {tag.Name}: topic/item are not configured");
                    continue;
                }

                if (TryRequestValue(topic, itemName, out string valueText))
                {
                    LogDriver($"[POLL] Value received for tag '{tag.Name}': {valueText}");
                    SetDecodedTagData(tag, valueText);
                }
            }
        }

        /// <summary>
        /// Tries to request a value for the specified topic and item.
        /// <para>Пытается запросить значение для указанного topic и item.</para>
        /// </summary>
        private bool TryRequestValue(string topic, string itemName, out string valueText)
        {
            valueText = string.Empty;

            try
            {
                valueText = driverClient.ReadTag(new ProjectTag
                {
                    Topic = topic,
                    ItemName = itemName
                });

                LogDriver($"[DDE] {project.ServiceName}|{topic}!{itemName} = {valueText}");
                return true;
            }
            catch (Exception ex)
            {
                if (!topicErrors.TryGetValue(topic, out DateTime lastErrorTime) ||
                    (DateTime.UtcNow - lastErrorTime).TotalMilliseconds >= project.ReconnectDelay)
                {
                    LogDriver(Locale.IsRussian
                        ? $"Ошибка DDE {project.ServiceName}|{topic}!{itemName}: {ex.Message}"
                        : $"DDE error {project.ServiceName}|{topic}!{itemName}: {ex.Message}");

                    topicErrors[topic] = DateTime.UtcNow;
                }

                driverClient.Disconnect();
                LogDriver($"[POLL] DriverClient disconnected after error for topic '{topic}'.");
                return false;
            }
        }

        /// <summary>
        /// Validates and sets decoded tag data into device data storage.
        /// <para>Проверяет и записывает декодированные данные тега в данные устройства.</para>
        /// </summary>
        private bool SetDecodedTagData(ProjectTag tag, string valueText)
        {
            string normalizedText = NormalizeIncomingValue(valueText);

            if (string.IsNullOrWhiteSpace(normalizedText))
            {
                LogDriver($"[DECODE] Empty value for tag '{tag.Name}'.");
                return false;
            }

            if (!TryGetDeviceTag(tag, out DeviceTag deviceTag))
            {
                LogDriver($"[DECODE] Device tag not found for '{tag.Name}'.");
                return false;
            }

            return SetTagData(deviceTag, tag, normalizedText);
        }

        /// <summary>
        /// Resolves the topic for a project tag or falls back to project's DefaultTopic.
        /// <para>Определяет topic для тега или использует DefaultTopic из проекта.</para>
        /// </summary>
        private string ResolveTopic(ProjectTag tag)
        {
            return string.IsNullOrWhiteSpace(tag.Topic)
                ? project.DefaultTopic
                : tag.Topic.Trim();
        }

        /// <summary>
        /// Resolves the item name for a project tag.
        /// <para>Возвращает имя item для тега.</para>
        /// </summary>
        private static string ResolveItemName(ProjectTag tag)
        {
            return tag.ItemName?.Trim() ?? string.Empty;
        }

        /// <summary>
        /// Normalizes the incoming value text by trimming and removing trailing NULs.
        /// <para>Нормализует входное значение: обрезает и удаляет завершающие NUL-символы.</para>
        /// </summary>
        private static string NormalizeIncomingValue(string valueText)
        {
            return (valueText ?? string.Empty).Trim().TrimEnd('\0');
        }

        /// <summary>
        /// Tries to parse a boolean text representation.
        /// <para>Пытается распарсить строковое представление логического значения.</para>
        /// </summary>
        private static bool TryParseBoolean(string text, out bool value)
        {
            switch (text.Trim().ToUpperInvariant())
            {
                case "1":
                case "TRUE":
                case "ON":
                case "YES":
                    value = true;
                    return true;

                case "0":
                case "FALSE":
                case "OFF":
                case "NO":
                    value = false;
                    return true;

                default:
                    value = false;
                    return false;
            }
        }

        /// <summary>
        /// Tries to parse a floating point number from text with culture fallbacks.
        /// <para>Пытается распарсить число с учётом разных локалей и форматов.</para>
        /// </summary>
        private static bool TryParseDouble(string text, out double value)
        {
            string normalized = text.Trim().Replace(" ", string.Empty);
            
            // try invariant culture
            if (double.TryParse(normalized, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value))
                return true;

            // try current culture
            if (double.TryParse(normalized, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out value))
                return true;

            // try common fallbacks
            string fallback = normalized.Replace(',', '.');
            if (double.TryParse(fallback, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, out value))
                return true;

            fallback = normalized.Replace('.', ',');
            return double.TryParse(fallback, NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.GetCultureInfo("ru-RU"), out value);
        }

        /// <summary>
        /// Builds a tag code for device tag registration.
        /// <para>Формирует код тега для регистрации в устройстве.</para>
        /// </summary>
        private static string GetTagCode(ProjectTag tag)
        {
            return string.IsNullOrWhiteSpace(tag.Name)
                ? $"tag_{tag.Channel}_{tag.Id:N}"
                : tag.Name.Trim().Replace(" ", "_");
        }

        /// <summary>
        /// Maps project tag data format to device tag data type.
        /// <para>Преобразует формат данных тега проекта в тип данных устройства.</para>
        /// </summary>
        private static TagDataType GetTagDataType(ProjectTag tag)
        {
            return tag.DataFormat switch
            {
                TagDataFormat.Ascii => TagDataType.ASCII,
                TagDataFormat.Unicode => TagDataType.Unicode,
                TagDataFormat.Int64 => TagDataType.Int64,
                TagDataFormat.UInt64 => TagDataType.Int64,
                _ => TagDataType.Double
            };
        }

        /// <summary>
        /// Calculates the device data length for a tag.
        /// <para>Вычисляет длину данных для тега на устройстве.</para>
        /// </summary>
        private static int GetTagDataLen(ProjectTag tag)
        {
            int length = Math.Max(1, tag.DataLength);
            if (IsStringFormat(tag.DataFormat))
                return Math.Max(1, (int)Math.Ceiling(length / 4.0));

            return 1;
        }

        /// <summary>
        /// Determines whether the specified data format is a string type.
        /// <para>Определяет, является ли формат строковым.</para>
        /// </summary>
        private static bool IsStringFormat(TagDataFormat format)
        {
            return format == TagDataFormat.Ascii ||
                   format == TagDataFormat.Unicode ||
                   format == TagDataFormat.HexString;
        }

        /// <summary>
        /// Maps project tag format to UI tag format.
        /// <para>Преобразует формат тега проекта в формат отображения.</para>
        /// </summary>
        private static TagFormat GetTagFormat(ProjectTag tag)
        {
            return tag.DataFormat switch
            {
                TagDataFormat.Ascii => new TagFormat(TagFormatType.String),
                TagDataFormat.Unicode => new TagFormat(TagFormatType.String),
                TagDataFormat.HexString => new TagFormat(TagFormatType.String),
                TagDataFormat.Bool => TagFormat.OffOn,
                TagDataFormat.Int16 => TagFormat.IntNumber,
                TagDataFormat.UInt16 => TagFormat.IntNumber,
                TagDataFormat.Int32 => TagFormat.IntNumber,
                TagDataFormat.UInt32 => TagFormat.IntNumber,
                TagDataFormat.Int64 => TagFormat.IntNumber,
                TagDataFormat.UInt64 => TagFormat.IntNumber,
                TagDataFormat.Float => TagFormat.FloatNumber,
                TagDataFormat.Double => TagFormat.FloatNumber,
                _ => new TagFormat(TagFormatType.Number)
            };
        }

        /// <summary>
        /// Tries to get a device tag by project tag.
        /// <para>Пытается получить тег устройства по тегу проекта.</para>
        /// </summary>
        private bool TryGetDeviceTag(ProjectTag projectTag, out DeviceTag deviceTag)
        {
            return deviceTagsById.TryGetValue(projectTag.Id, out deviceTag);
        }

        /// <summary>
        /// Decodes and sets tag data into device structures according to tag format.
        /// <para>Декодирует и записывает данные тега в структуру устройства в соответствии с форматом.</para>
        /// </summary>
        private bool SetTagData(DeviceTag deviceTag, ProjectTag projectTag, string valueText)
        {
            try
            {
                int status = CnlStatusID.Defined;
                switch (projectTag.DataFormat)
                {
                    case TagDataFormat.Ascii:
                    case TagDataFormat.HexString:
                        DeviceData.SetAscii(deviceTag.Code, valueText, status);
                        return true;

                    case TagDataFormat.Unicode:
                        DeviceData.SetUnicode(deviceTag.Code, valueText, status);
                        return true;

                    case TagDataFormat.Bool:
                        if (TryParseBoolean(valueText, out bool boolValue))
                        {
                            DeviceData.Set(deviceTag.Index, boolValue ? 1 : 0, status);
                            return true;
                        }
                        break;

                    default:
                        if (TryParseDouble(valueText, out double numericValue))
                        {
                            DeviceData.Set(deviceTag.Index, numericValue, status);
                            return true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogDriver($"[DECODE] Error setting tag '{deviceTag.Code}': {ex.Message}");
            }

            LogDriver($"[DECODE] Failed to decode tag '{projectTag.Name}' value '{valueText}'.");
            return false;
        }

        /// <summary>
        /// Logs driver messages to the main Scada Communicator log.
        /// <para>Записывает сообщения драйвера в основной лог Scada Communicator.</para>
        /// </summary>
        private void LogDriver(string text)
        {
            if (!string.IsNullOrWhiteSpace(text) && project.WriteLogDriver)
            {
                Log.WriteMessage(text, project.MessageTypeLogDriver);
            }
        }

        #endregion Private Methods
    }
}
