// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Scada.Lang;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Scada.Comm.Drivers.DrvDbImportPlus
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
        public const string DriverCode = "DrvDbImportPlus";

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
            string resourceName = $"Scada.Comm.Drivers.DrvDbImportPlus.Config.DrvDbImportPlus.{suffix}.xml";
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
    }
}