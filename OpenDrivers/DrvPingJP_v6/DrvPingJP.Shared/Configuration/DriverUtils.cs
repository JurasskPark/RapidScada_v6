using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Scada.Comm.Drivers.DrvPingJP
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
        public const string DriverCode = "DrvPingJP";

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
            string resourceName = $"Scada.Comm.Drivers.DrvPingJP.{suffix}.xml";
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

        #region Float String

        public static bool FloatAsTrue(string s)
        {
            s = FloatPuttingInOrder(s);
            if (s.LastIndexOf(".") != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static float FloatAsFloat(string s)
        {
            return float.Parse(FloatPuttingInOrder(s), CultureInfo.InvariantCulture);
        }

        public static int FloatToInteger(string s)
        {
            string[] parts = FloatPuttingInOrder(s).Split('.');
            return Convert.ToInt32(parts[0]);
        }

        public static int FloatToFractionalNumber(string s)
        {
            string[] parts = FloatPuttingInOrder(s).Split('.');
            if (parts.Length == 1)
            {
                return 0;
            }
            return Convert.ToInt32(parts[1]);
        }

        public static int FloatToFractionalNumber(string s, out int startBit, out int endBit, out int countBit)
        {
            startBit = 0;
            endBit = 0;
            countBit = 0;

            string[] parts = FloatPuttingInOrder(s).Split('.');
            if (parts.Length == 1)
            {
                return Convert.ToInt32(parts[0]);
            }
            string[] parts2 = parts[1].Split("-");
            if (parts2.Length == 1)
            {
                startBit = Convert.ToInt32(parts[1]);
                countBit = 1;
                return Convert.ToInt32(parts[0]);
            }
            startBit = Convert.ToInt32(parts2[0]);
            endBit = Convert.ToInt32(parts2[1]);
            countBit = endBit - startBit;
            return Convert.ToInt32(parts[0]);
        }

        private static string FloatPuttingInOrder(string s)
        {
            s = s.Replace(",", ".").Trim();
            return s;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct FloatUnion
        {
            [FieldOffset(0)]
            public float value;

            [FieldOffset(0)]
            public int binary;
        }

        public static bool IsNaN(float f)
        {
            FloatUnion union = new FloatUnion();
            union.value = f;

            return ((union.binary & 0x7F800000) == 0x7F800000) && ((union.binary & 0x007FFFFF) != 0);
        }

        #endregion Float String

        #region Double String

        public static double DoubleAsDouble(string s)
        {
            try
            {
                return double.Parse(DoublePuttingInOrder(s), CultureInfo.InvariantCulture);
            }
            catch
            {
                return double.NaN;
            }
        }

        private static string DoublePuttingInOrder(string s)
        {
            s = s.Replace(",", ".").Trim();
            return s;
        }

        public static double StringToDouble(string s) // Позволяет в строке вводить дробные числа с ,(запятой) или .(точкой)
        {
            double result = 1;
            if (!double.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("ru-RU"), out result))
            {
                if (!double.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.GetCultureInfo("en-US"), out result))
                {
                    return 1;
                }
            }
            return result;
        }

        #endregion Double String
    }
}