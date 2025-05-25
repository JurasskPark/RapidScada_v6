// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Scada.Comm.Drivers.DrvFreeDiskSpaceJP
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
        /// </summary>
        public const string DriverCode = "DrvFreeDiskSpaceJP";

        /// <summary>
        /// The driver version.
        /// </summary>
        public const string Version = "6.4.0.0";

        /// <summary>
        /// The default filename of the configuration.
        /// </summary>
        public const string DefaultConfigFileName = DriverCode + ".xml";

        /// <summary>
        /// Gets the short name of the device configuration file.
        /// </summary>
        public static string GetFileName(int deviceNum = 0)
        {
            return $"{DriverCode}_{deviceNum:D3}.xml";
        }

        /// <summary>
        /// Gets the short name of the license file activation.
        /// </summary>
        public static string GetFileActivation(int deviceNum = 0)
        {
            return $"{DriverCode}_{deviceNum:D3}.bin";
        }

        /// <summary>
        /// Gets the short name of the device configuration file.
        /// </summary>
        public static string GetFileName()
        {
            return $"{DriverCode}.xml";
        }

        /// <summary>
        /// Application name.
        /// </summary>
        public static string Name(bool isRussian = false)
        {
            string text = isRussian ? "Анализатор текста" : "Parser text";
            return text;
        }

        /// <summary>
        /// Description of the application.
        /// </summary>
        public static string Description(bool isRussian = false)
        {
            string text = isRussian ?
                    "Преобразование текстовых файлов в данные SCADA." :
                    "Parsing text files to SCADA data.";
            return text;
        }

        /// <summary>
        /// Writes an configuration file depending on operating system.
        /// </summary>
        public static void WriteConfigFile(string fileName, bool windows)
        {
            string suffix = windows ? "Win" : "Linux";
            string resourceName = $"Scada.Comm.Drivers.DrvFreeDiskSpaceJP.Config.DrvFreeDiskSpaceJP.{suffix}.xml";
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
        #endregion Basic

        #region Guid
        /// <summary>
        /// Converting string Guid to Guid.
        /// </summary>
        /// <param name="id">String id</param>
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
        /// Create Guid
        /// </summary>
        public static string CreateGuid()
        {
            Guid guid = Guid.NewGuid();
            byte[] bytes = guid.ToByteArray();

            var s = new StringBuilder();
            foreach (byte b in bytes)
            {
                s.Append(b.ToString("x2"));
            }
            return s.ToString();
        }

        /// <summary>
        /// Regex Guid.
        /// </summary>
        private static Regex reGuid = new Regex(@"^[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}$", RegexOptions.Compiled);
        #endregion Guid

        #region IsNullString
        /// <summary>
        /// Сonverting an object to a string, if the object is empty, it returns an empty string.
        /// </summary>
        /// <param name="Value">object Value</param>
        /// <returns>string Value</returns>
        public static string NullToString(object Value)
        {
            try
            {
                // check for null
                if (Value == null) return "";

                var type = Value.GetType();

                // check for invalid values in date times.
                if (type == typeof(DateTime))
                {
                    if (((DateTime)Value) == DateTime.MinValue)
                    {
                        return string.Empty;
                    }

                    var date = (DateTime)Value;

                    if (date.Millisecond > 0)
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss.fff");
                    }
                    else
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                // use only the local name for qualified names.
                if (type == typeof(XmlQualifiedName))
                {
                    return ((XmlQualifiedName)Value).Name;
                }

                // use only the name for system types.
                if (type.FullName == "System.RuntimeType")
                {
                    return ((Type)Value).FullName;
                }

                // treat byte arrays as a special case.
                if (type == typeof(byte[]))
                {
                    var bytes = (byte[])Value;

                    var buffer = new StringBuilder(bytes.Length * 3);

                    foreach (var character in bytes)
                    {
                        buffer.Append(character.ToString("X2"));
                        buffer.Append(".");
                    }

                    return buffer.ToString();
                }

                // show the element type and length for arrays.
                if (type.IsArray)
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)Value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"{type.GetElementType()?.Name}[{((Array)Value).Length}]{result}";
                }

                // instances of array are always treated as arrays of objects.
                if (type == typeof(Array))
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)Value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"Object[{((Array)Value).Length}]{result}";
                }

                // default behavior.
                return Value.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        #endregion IsNullString

        #region Schedule
        /// <summary>
        /// ScheduleMode
        /// </summary>
        public enum ScheduleMode { None = 0, Secondly = 1, Minutly = 2, Hourly = 3, Daily = 4 };

        /// <summary>
        /// Calculate Trigger Time
        /// </summary>
        /// <param name="currentTime">Current Time</param>
        /// <param name="processTime"></param>
        /// <param name="scheduleMode">Schedule Mode</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static DateTime CalculateTriggerTime(DateTime currentTime, TimeSpan processTime, ScheduleMode scheduleMode)
        {
            DateTime nextTime = new DateTime();
            nextTime = currentTime;

            switch (scheduleMode)
            {
                case ScheduleMode.Secondly:
                    {
                        nextTime = nextTime.Add(new TimeSpan(0, 0, 0, processTime.Seconds, nextTime.Millisecond));
                    }
                    break;
                case ScheduleMode.Minutly:
                    {
                        nextTime = nextTime.Subtract(new TimeSpan(0, 0, 0, nextTime.Second, nextTime.Millisecond));
                        nextTime = nextTime.Add(new TimeSpan(0, 0, processTime.Minutes, processTime.Seconds, 0));
                    }
                    break;

                case ScheduleMode.Hourly:
                    {
                        nextTime = nextTime.Subtract(new TimeSpan(0, 0, nextTime.Minute, nextTime.Second, nextTime.Millisecond));
                        nextTime = nextTime.Add(new TimeSpan(0, processTime.Hours, processTime.Minutes, processTime.Seconds, 0));
                    }
                    break;

                case ScheduleMode.Daily:
                    {
                        nextTime = nextTime.Subtract(new TimeSpan(0, nextTime.Hour, nextTime.Minute, nextTime.Second, nextTime.Millisecond));
                        nextTime = nextTime.Add(new TimeSpan(processTime.Days, processTime.Hours, processTime.Minutes, processTime.Seconds, 0));
                    }
                    break;
            }
            return nextTime;
        }
        #endregion Schedule

        #region UTC

        /// <summary>
        /// Converts the specified date and time to the local time.
        /// </summary>
        public static DateTime UtcToLocalTime(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime();
        }

        /// <summary>
        /// Converts local date and time to the univeral time.
        /// </summary>
        public static DateTime LocalToUtcTime(DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local).ToUniversalTime();
        }

        /// <summary>
        /// Converts double to the time.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns>DateTime</returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        /// <summary>
        /// Converts double to the time.
        /// </summary>
        /// <param name="unixTimeStamp"></param>
        /// <returns></returns>
        public static DateTime FromOADate(double unixTimeStamp)
        {
            return new DateTime(DoubleDateToTicks(unixTimeStamp), DateTimeKind.Unspecified);
        }

        /// <summary>
        /// Converts double to the long.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>Long</returns>
        /// <exception cref="ArgumentException"></exception>
        static internal long DoubleDateToTicks(double value)
        {
            if (value >= 2958466.0 || value <= -657435.0)
                throw new ArgumentException("Not a valid value");
            long num1 = (long)(value * 86400000.0 + (value >= 0.0 ? 0.5 : -0.5));
            if (num1 < 0L)
                num1 -= num1 % 86400000L * 2L;
            long num2 = num1 + 59926435200000L;
            if (num2 < 0L || num2 >= 315537897600000L)
                throw new ArgumentException("Not a valid value");
            return num2 * 10000L;
        }

        #endregion UTC

        #region SQL

        /// <summary>
        /// The date format used for naming partitions.
        /// </summary>
        public const string PartitionDateFormat = "yyyy-MM-ddTHH:mm:ss";
        public const string PartitionDateTime = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Converts the specified date to a name string.
        /// </summary>
        public static bool ParsePartitionString(DateTime dateTime, out string result)
        {
            try
            {
                result = dateTime.ToString(PartitionDateFormat);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        /// <summary>
        /// Converts the specified substring of a partition name to a date.
        /// </summary>
        public static bool ParsePartitionDate(string s, out DateTime result)
        {
            return DateTime.TryParseExact(s, PartitionDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        #endregion SQL

        #region Convert String to Object

        public static object ConvertStringToObject(string typeData, string valueData)
        {
            object obj = null;

            switch (typeData)
            {
                case "Single":
                    obj = Convert.ToSingle(valueData);
                    break;
                case "Double":
                    obj = Convert.ToDouble(valueData);
                    break;
                case "Boolean":
                    obj = Convert.ToBoolean(valueData);
                    break;
                case "SByte":
                    obj = Convert.ToSByte(valueData);
                    break;
                case "Byte":
                    obj = Convert.ToByte(valueData);
                    break;
                case "Int16":
                    obj = Convert.ToInt16(valueData);
                    break;
                case "UInt16":
                    obj = Convert.ToUInt16(valueData);
                    break;
                case "Int32":
                    obj = Convert.ToInt32(valueData);
                    break;
                case "UInt32":
                    obj = Convert.ToUInt32(valueData);
                    break;
                case "Int64":
                    obj = Convert.ToInt64(valueData);
                    break;
                case "UInt64":
                    obj = Convert.ToUInt64(valueData);
                    break;
                case "String":
                    obj = Convert.ToString(valueData);
                    break;
                case "DateTime":
                    obj = Convert.ToDateTime(valueData);
                    break;
                case "ArrayList":
                    obj = valueData.ToArray();
                    break;
            }

            return obj;
        }

        #endregion Convert String to Object

        #region IpAddress
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
        #endregion IpAddress

        #region Float String
        /// <summary>
        /// Checking the string for the float value.
        /// </summary>
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

        /// <summary>
        /// Converting a string to a float value.
        /// </summary>
        public static Decimal FloatAsFloat(string s)
        {
            return Decimal.Parse(FloatPuttingInOrder(s), CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converting a string to a int value.
        /// </summary>
        public static int FloatToInteger(string s)
        {
            string[] parts = FloatPuttingInOrder(s).Split('.');
            return Convert.ToInt32(parts[0]);
        }

        /// <summary>
        /// Returns the int value from the string float up to the separating sign of the value.
        /// </summary>
        public static int FloatToFractionalNumber(string s)
        {
            string[] parts = FloatPuttingInOrder(s).Split('.');
            if (parts.Length == 1)
            {
                return 0;
            }
            return Convert.ToInt32(parts[1]);
        }

        /// <summary>
        /// Returns the value of the required number of bits from the string float.
        /// </summary>
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

        /// <summary>
        /// Replacing the comma in the float string with a dot.
        /// </summary>
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

        /// <summary>
        /// Checking the float for NaN.
        /// </summary>
        public static bool IsNaN(float f)
        {
            FloatUnion union = new FloatUnion();
            union.value = f;

            return ((union.binary & 0x7F800000) == 0x7F800000) && ((union.binary & 0x007FFFFF) != 0);
        }

        #endregion Float String

        #region Double String
        /// <summary>
        /// Converting a string to a double value.
        /// </summary>
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

        /// <summary>
        /// Converting a string to a double value according to the language standards.
        /// </summary>
        public static string StringDoubleAsString(string s)
        {
            double result;
            if (!double.TryParse(s, out result))
            {
                s = s.Replace(",", ".").Trim();
                if (!double.TryParse(s, out result))
                {
                    s = s.Replace(".", ",").Trim();
                    if (double.TryParse(s, out result))
                    {
                        return s;
                    }
                    else
                    {
                        return "NaN";
                    }
                }
                else
                {
                    return s;
                }
            }
            else
            {
                return s;
            }
        }

        /// <summary>
        /// Replacing the comma in the float string with a dot.
        /// </summary>
        public static string DoublePuttingInOrder(string s)
        {
            s = s.Replace(",", ".").Trim();
            return s;
        }

        /// <summary>
        /// Converting a string to a double value.
        /// </summary>
        public static double StringToDouble(string s) 
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