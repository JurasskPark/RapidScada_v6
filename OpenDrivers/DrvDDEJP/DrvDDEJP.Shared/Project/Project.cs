using Scada.Comm.Lang;
using Scada.Lang;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// Represents a driver project configuration.
    /// <para>Представляет конфигурацию проекта драйвера.</para>
    /// </summary>
    [XmlRoot("DrvDDEJPConfig")]
    public class Project
    {
        #region Variable

        private string serviceName = string.Empty;                  // the DDE service name
        private string defaultTopic = string.Empty;                 // the default DDE topic

        #endregion Variable

        #region Property

        /// <summary>
        /// Gets or sets the driver log message type.
        /// <para>Получает или задает тип сообщений лога драйвера.</para>
        /// </summary>
        public Scada.Log.LogMessageType MessageTypeLogDriver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to write the driver log.
        /// <para>Получает или задает значение, указывающее, записывать ли лог драйвера.</para>
        /// </summary>
        public bool WriteLogDriver { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the language is Russian.
        /// <para>Получает или задает значение, указывающее, является ли язык русским.</para>
        /// </summary>
        public bool LanguageIsRussian { get; set; }

        /// <summary>
        /// Gets or sets the DDE service name.
        /// <para>Получает или задает имя DDE сервиса.</para>
        /// </summary>
        public string ServiceName
        {
            get => serviceName;
            set => serviceName = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the default DDE topic.
        /// <para>Получает или задает DDE топик по умолчанию.</para>
        /// </summary>
        public string DefaultTopic
        {
            get => defaultTopic;
            set => defaultTopic = value ?? string.Empty;
        }

        /// <summary>
        /// Gets or sets the request timeout in milliseconds.
        /// <para>Получает или задает таймаут запроса в миллисекундах.</para>
        /// </summary>
        public int RequestTimeout { get; set; }

        /// <summary>
        /// Gets or sets the reconnect delay in milliseconds.
        /// <para>Получает или задает задержку переподключения в миллисекундах.</para>
        /// </summary>
        public int ReconnectDelay { get; set; }

        /// <summary>
        /// Gets or sets the project tags.
        /// <para>Получает или задает теги проекта.</para>
        /// </summary>
        [XmlArray("Tags")]
        [XmlArrayItem("Tag")]
        public List<ProjectTag> Tags { get; set; }

        #endregion Property

        #region Basic

        /// <summary>
        /// Initializes a new instance of the class.
        /// <para>Инициализирует новый экземпляр класса.</para>
        /// </summary>
        public Project()
        {
            Tags = new List<ProjectTag>();
            MessageTypeLogDriver = Scada.Log.LogMessageType.Action;
            WriteLogDriver = true;
            ServiceName = "ServiceName ";
            DefaultTopic = "DefaultTopic";
            RequestTimeout = 5000;
            ReconnectDelay = 2000;
        }

        /// <summary>
        /// Loads the configuration from the specified file.
        /// <para>Загружает конфигурацию из указанного файла.</para>
        /// </summary>
        public bool Load(string fileName, out string errMsg)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    if (!Save(fileName, out errMsg))
                    {
                        return false;
                    }

                    errMsg = string.Empty;
                    return true;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using FileStream stream = File.OpenRead(fileName);
                if (serializer.Deserialize(stream) is Project loadedProject)
                {
                    CopyFrom(loadedProject);
                }

                Normalize();
                if (!Validate(out errMsg))
                {
                    return false;
                }

                errMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.LoadDriverConfigError + ": " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Saves the configuration to the specified file.
        /// <para>Сохраняет конфигурацию в указанный файл.</para>
        /// </summary>
        public bool Save(string fileName, out string errMsg)
        {
            try
            {
                Normalize();

                string directory = Path.GetDirectoryName(fileName);
                if (!string.IsNullOrEmpty(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using FileStream stream = File.Create(fileName);
                serializer.Serialize(stream, this);

                errMsg = string.Empty;
                return true;
            }
            catch (Exception ex)
            {
                errMsg = CommPhrases.SaveDriverConfigError + ": " + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// Gets the configuration file name for the specified device.
        /// <para>Получает имя файла конфигурации для указанного устройства.</para>
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            return DriverUtils.GetFileName(deviceNum);
        }

        /// <summary>
        /// Copies the properties from the source project.
        /// <para>Копирует свойства из исходного проекта.</para>
        /// </summary>
        private void CopyFrom(Project source)
        {
            MessageTypeLogDriver = source.MessageTypeLogDriver;
            WriteLogDriver = source.WriteLogDriver;
            LanguageIsRussian = source.LanguageIsRussian;
            ServiceName = source.ServiceName;
            DefaultTopic = source.DefaultTopic;
            RequestTimeout = Math.Max(100, source.RequestTimeout);
            ReconnectDelay = Math.Max(0, source.ReconnectDelay);
            Tags = source.Tags ?? new List<ProjectTag>();
        }

        /// <summary>
        /// Normalizes the project configuration.
        /// <para>Нормализует конфигурацию проекта.</para>
        /// </summary>
        private void Normalize()
        {
            Tags ??= new List<ProjectTag>();
            ServiceName = ServiceName.Trim();
            if (string.IsNullOrEmpty(ServiceName))
                ServiceName = "ServiceName ";

            DefaultTopic = DefaultTopic.Trim();
            if (string.IsNullOrEmpty(DefaultTopic))
                DefaultTopic = "DefaultTopic";

            RequestTimeout = Math.Max(100, RequestTimeout);
            ReconnectDelay = Math.Max(0, ReconnectDelay);

            int tagOrder = 0;
            foreach (ProjectTag tag in Tags)
            {
                if (tag != null)
                {
                    tag.Normalize(tagOrder++);
                }
            }
        }

        /// <summary>
        /// Validates the project configuration.
        /// <para>Проверяет конфигурацию проекта.</para>
        /// </summary>
        private bool Validate(out string errMsg)
        {
            if (string.IsNullOrWhiteSpace(ServiceName))
            {
                errMsg = Locale.IsRussian ? "Имя сервиса обязательно." : "ServiceName is required.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(DefaultTopic))
            {
                errMsg = Locale.IsRussian ? "Топик по умолчанию обязателен." : "DefaultTopic is required.";
                return false;
            }

            errMsg = string.Empty;
            return true;
        }

        #endregion Basic
    }
}
