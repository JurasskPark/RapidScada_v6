using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// The class provides helper methods for the driver.
    /// <para>Класс, предоставляющий вспомогательные методы для драйвера.</para>
    /// </summary>
    public static class DriverUtils
    {
        #region Basic

        /// <summary>
        /// The driver code.
        /// <para>Код драйвера.</para>
        /// </summary>
        public const string DriverCode = "DrvDDEJP";

        /// <summary>
        /// The driver version.
        /// <para>Версия драйвера.</para>
        /// </summary>
        public const string Version = "6.0.0.1";

        /// <summary>
        /// The default filename of the configuration.
        /// <para>Имя файла конфигурации по умолчанию.</para>
        /// </summary>
        public const string DefaultConfigFileName = DriverCode + ".xml";

        /// <summary>
        /// Gets the short name of the device configuration file.
        /// <para>Получает короткое имя файла конфигурации устройства.</para>
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            return deviceNum == 0
                ? $"{DriverCode}.xml"
                : $"{DriverCode}_{deviceNum:D3}.xml";
        }

        /// <summary>
        /// Gets the short name of the device log file.
        /// <para>Получает короткое имя файла лога устройства.</para>
        /// </summary>
        public static string GetFileLogName(int deviceNum)
        {
            return deviceNum == 0
                ? "line.log"
                : $"line{deviceNum:D3}.log";
        }

        /// <summary>
        /// Gets the driver name.
        /// <para>Возвращает имя драйвера.</para>
        /// </summary>
        public static string Name(bool isRussian = false)
        {
            return isRussian ? "Драйвер DDE" : "DDE Driver";
        }

        /// <summary>
        /// Gets the driver description.
        /// <para>Возвращает описание драйвера.</para>
        /// </summary>
        public static string Description(bool isRussian = false)
        {
            return isRussian
                ? "Взаимодействие с внешними приложениями через протокол DDE."
                : "Interacts with external applications via DDE protocol.";
        }

        #endregion Basic

        #region Helper Methods

        /// <summary>
        /// Converts an object to a string and returns an empty string for null values.
        /// <para>Преобразует объект в строку и возвращает пустую строку для null значений.</para>
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The string representation of the object.</returns>
        public static string NullToString(object value)
        {
            try
            {
                if (value == null)
                {
                    return string.Empty;
                }

                Type type = value.GetType();

                if (type == typeof(DateTime))
                {
                    DateTime date = (DateTime)value;
                    if (date == DateTime.MinValue)
                    {
                        return string.Empty;
                    }

                    return date.Millisecond > 0
                        ? date.ToString("yyyy-MM-dd HH:mm:ss.fffff")
                        : date.ToString("yyyy-MM-dd HH:mm:ss");
                }

                if (type == typeof(XmlQualifiedName))
                {
                    return ((XmlQualifiedName)value).Name;
                }

                if (type.FullName == "System.RuntimeType")
                {
                    return ((Type)value).FullName ?? string.Empty;
                }

                if (type == typeof(byte[]))
                {
                    byte[] bytes = (byte[])value;
                    StringBuilder buffer = new StringBuilder(bytes.Length * 3);
                    foreach (byte b in bytes)
                    {
                        buffer.Append(b.ToString("X2")).Append(".");
                    }
                    return buffer.ToString();
                }

                if (type.IsArray)
                {
                    StringBuilder result = new StringBuilder();
                    int index = 0;
                    foreach (object element in (Array)value)
                    {
                        result.AppendFormat("[{0}] {1}{2}", index++, element, Environment.NewLine);
                    }
                    return $"{type.GetElementType()?.Name}[{((Array)value).Length}]{Environment.NewLine}{result}";
                }

                if (type.FullName == "System.Object")
                {
                    return string.Empty;
                }

                return value.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion Helper Methods
    }
}
