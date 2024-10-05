using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Scada.Comm.Drivers.DrvTelnetJP
{
    /// <summary>
    /// The class provides helper methods for the driver.
    /// <para>Класс, предоставляющий вспомогательные методы для драйвера.</para>
    /// </summary>
    public static class DriverUtils
    {
        /// <summary>
        /// The driver code.
        /// </summary>
        public const string DriverCode = "DrvTelnetJP";

        /// <summary>
        /// The driver version.
        /// </summary>
        public const string Version = "6.3.0.0";

        /// <summary>
        /// The default filename of the configuration.
        /// </summary>
        public const string DefaultConfigFileName = DriverCode + ".xml";

        /// <summary>
        /// Writes an configuration file depending on operating system.
        /// </summary>
        public static void WriteConfigFile(string fileName, bool windows)
        {
            string suffix = windows ? "Win" : "Linux";
            string resourceName = $"Scada.Comm.Drivers.DrvTelnetJP.{suffix}.xml";
            string fileContents;

            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    fileContents = reader.ReadToEnd();
                }
            }

            File.WriteAllText(fileName, fileContents, Encoding.UTF8);
        }

        /// <summary>
        /// Converting string Guid to Guid.
        /// </summary>
        /// <param name="id">string id</param>
        /// <returns>Guid id</returns>
        public static Guid StringToGuid(string id)
        {
            if (id == null || id.Length != 36)
            {
                return Guid.Empty;
            }
            if (reGuid.IsMatch(id))
            {
                return new Guid(id);
            }
            else
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// Regex Guid.
        /// </summary>
        private static Regex reGuid = new Regex(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$", RegexOptions.Compiled);

        /// <summary>
        /// Сonverting an object to a string, if the object is empty, it returns an empty string.
        /// </summary>
        /// <param name="Value">object Value</param>
        /// <returns>string Value</returns>
        public static string NullToString(object Value)
        {
            // Value.ToString() allows for Value being DBNull, but will also convert int, double, etc.
            return Value == null ? "" : Value.ToString();

            // If this is not what you want then this form may suit you better, handles 'Null' and DBNull otherwise tries a straight cast
            // which will throw if Value isn't actually a string object.
            //return Value == null || Value == DBNull.Value ? "" : (string)Value;
        }

        /// <summary>
        /// Movement of elements in the listview to the up and down.
        /// </summary>
        public enum MoveDirection { Up = -1, Down = 1 };


        /// <summary>
        /// Checking the validity of the IP address
        /// </summary>
        /// <param name="Address"></param>
        /// <returns>True/False</returns>
        public static bool IsIpAddress(string Address)
        {
            // initializing a new instance of the System.Text.RegularExpressions.Regex class
            // for the specified regular expression.
            Regex IpMatch = new Regex(@"^([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])\.([01]?\d\d?|2[0-4]\d|25[0-5])$");
            // checking whether the specified input string matches the regular
            // the expression specified in the constructor System.Text.RegularExpressions.Regex.
            // if yes, we return true, if not, false
            return IpMatch.IsMatch(Address);
        }

        public static string IPAddressNoPort(string address)
        {
            // if the IP passes the validity test
            if (IsIpAddress(address) == true)
            {
                return address;
            }
            else if (IsIpAddress(address) == false) // if the IP did not pass the validity test, then let's try to remove the port
            {
                // parse IP
                String[] IP = address.Split(new String[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                // we sort through what we got (Port or IP)
                foreach (string addressTrue in IP)
                {
                    // if you come across an IP that is true
                    if (IsIpAddress(addressTrue) == true)
                    {
                        return addressTrue;
                    }
                }
            }
            return null;
        }

        public static string PortNoIPAddress(string address)
        {
            // if the IP passes the validity test
            if (IsIpAddress(address) == true)
            {
                // we do nothing
            }
            else if (IsIpAddress(address) == false) //Если IP не прошёл на валидатность, то попробуем удалить порт
            {
                // parse IP
                String[] IP = address.Split(new String[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

                // we sort through what we got (Port or IP)
                foreach (string portTrue in IP)
                {
                    // if the IP passes the validity test
                    if (IsIpAddress(portTrue) == true)
                    {
                        // we do nothing
                    }
                    else
                    {
                        return portTrue;
                    }
                }
            }
            return null;
        }
    }
}