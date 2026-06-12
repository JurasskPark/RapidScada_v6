using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    /// <summary>
    /// Provides helper methods for the driver.
    /// <para>Предоставляет вспомогательные методы драйвера.</para>
    /// </summary>
    public static class DriverUtils
    {
        #region Variable

        private static readonly Regex guidRegex = new Regex(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$", RegexOptions.Compiled); // GUID regex

        #endregion Variable

        #region Const

        /// <summary>
        /// The driver code.
        /// <para>Код драйвера.</para>
        /// </summary>
        public const string DriverCode = "DrvTelnetJP";

        /// <summary>
        /// The driver version.
        /// <para>Версия драйвера.</para>
        /// </summary>
        public const string Version = "6.4.0.0";

        /// <summary>
        /// The default configuration file name.
        /// <para>Имя файла конфигурации по умолчанию.</para>
        /// </summary>
        public const string DefaultConfigFileName = DriverCode + ".xml";

        #endregion Const

        #region Basic

        /// <summary>
        /// Writes a configuration file from an embedded resource.
        /// <para>Записывает файл конфигурации из встроенного ресурса.</para>
        /// </summary>
        public static void WriteConfigFile(string fileName, bool windows)
        {
            string suffix = windows ? "Win" : "Linux";
            string resourceName = $"Scada.Comm.Drivers.DrvTelnetJP.{suffix}.xml";

            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                return;
            }

            using StreamReader reader = new StreamReader(stream);
            File.WriteAllText(fileName, reader.ReadToEnd(), Encoding.UTF8);
        }

        /// <summary>
        /// Converts a string to a GUID.
        /// <para>Преобразует строку в GUID.</para>
        /// </summary>
        public static Guid StringToGuid(string id)
        {
            return !string.IsNullOrEmpty(id) && guidRegex.IsMatch(id)
                ? new Guid(id)
                : Guid.Empty;
        }

        /// <summary>
        /// Converts an object to a string or returns an empty string.
        /// <para>Преобразует объект в строку или возвращает пустую строку.</para>
        /// </summary>
        public static string NullToString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Checks whether the value is an IPv4 address.
        /// <para>Проверяет, является ли значение IPv4-адресом.</para>
        /// </summary>
        public static bool IsIpAddress(string address)
        {
            return IPAddress.TryParse(address, out IPAddress ipAddress) &&
                ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
        }

        /// <summary>
        /// Extracts an address from an address:port string.
        /// <para>Извлекает адрес из строки адрес:порт.</para>
        /// </summary>
        public static string IPAddressNoPort(string address)
        {
            SplitAddressAndPort(address, out string host, out _);
            return host;
        }

        /// <summary>
        /// Extracts a port from an address:port string.
        /// <para>Извлекает порт из строки адрес:порт.</para>
        /// </summary>
        public static string PortNoIPAddress(string address)
        {
            SplitAddressAndPort(address, out _, out string port);
            return port;
        }

        /// <summary>
        /// Splits an address and port.
        /// <para>Разделяет адрес и порт.</para>
        /// </summary>
        private static void SplitAddressAndPort(string address, out string host, out string port)
        {
            host = string.Empty;
            port = string.Empty;

            if (string.IsNullOrWhiteSpace(address))
            {
                return;
            }

            string[] parts = address.Trim().Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (parts.Length == 1)
            {
                host = parts[0];
                return;
            }

            host = parts[0];
            port = parts[^1];
        }

        #endregion Basic
    }
}
