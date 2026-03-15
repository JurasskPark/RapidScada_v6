// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace Scada.Comm.Drivers.DrvDebug
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
        public const string DriverCode = "DrvDebug";

        /// <summary>
        /// The driver version.
        /// </summary>
        public const string Version = "6.0.0.1";

        /// <summary>
        /// The default filename of the configuration.
        /// </summary>
        public const string DefaultConfigFileName = DriverCode + ".xml";

        /// <summary>
        /// Gets the short name of the device configuration file.
        /// </summary>
        public static string GetFileName(int deviceNum)
        {
            if (deviceNum == 0)
            {
                return $"{DriverCode}.xml";
            }
            else
            {
                return $"{DriverCode}_{deviceNum:D3}.xml";
            }
        }

        /// <summary>
        /// Gets the short name of the device configuration file.
        /// </summary>
        public static string GetFileName()
        {
            return $"{DriverCode}.xml";
        }

        /// <summary>
        /// Gets the short name of the device log file.
        /// </summary>
        public static string GetFileLogName(int deviceNum)
        {
            if (deviceNum == 0)
            {
                return $"line.log";
            }
            else
            {
                return $"line{deviceNum:D3}.log";
            }
        }

        /// <summary>
        /// Application name.
        /// </summary>
        public static string Name(bool isRussian = false)
        {
            string text = isRussian ? "DrvDebug" : "DrvDebug";
            return text;
        }

        /// <summary>
        /// Description of the application.
        /// </summary>
        public static string Description(bool isRussian = false)
        {
            string text = isRussian ?
                    "Протокол DrvDebug." :
                    "Protocol DrvDebug.";
            return text;
        }

        #endregion Basic

        #region IsNullString
        /// <summary>
        /// Converts an object to a string and returns an empty string for empty values.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <returns>The string representation of the object.</returns>
        public static string NullToString(object value)
        {
            try
            {
                // Check for null.
                if (value == null)
                {
                    return "";
                }

                Type type = value.GetType();

                // Check for invalid DateTime values.
                if (type == typeof(DateTime))
                {
                    if ((DateTime)value == DateTime.MinValue)
                    {
                        return string.Empty;
                    }

                    DateTime date = (DateTime)value;

                    if (date.Millisecond > 0)
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss.fffff");
                    }
                    else
                    {
                        return date.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }

                // Use only the local name for qualified names.
                if (type == typeof(XmlQualifiedName))
                {
                    return ((XmlQualifiedName)value).Name;
                }

                // Use only the name for system types.
                if (type.FullName == "System.RuntimeType")
                {
                    return ((Type)value).FullName ?? string.Empty;
                }

                // Treat byte arrays as a special case.
                if (type == typeof(byte[]))
                {
                    byte[] bytes = (byte[])value;

                    StringBuilder buffer = new StringBuilder(bytes.Length * 3);

                    foreach (byte character in bytes)
                    {
                        buffer.Append(character.ToString("X2"));
                        buffer.Append(".");
                    }

                    return buffer.ToString();
                }

                // Show the element type and length for arrays.
                if (type.IsArray)
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"{type.GetElementType()?.Name}[{((Array)value).Length}]{result}";
                }

                // Instances of Array are always treated as arrays of objects.
                if (type == typeof(Array))
                {
                    string result = string.Empty;
                    int index = 0;
                    foreach (object element in (Array)value)
                    {
                        result += String.Format("[{0}]", index++) + element.ToString() + Environment.NewLine;
                    }
                    return $"Object[{((Array)value).Length}]{result}";
                }

                // Return an empty string for a plain object instance.
                if (type.FullName == "System.Object")
                {
                    return string.Empty;
                }

                // Use the default conversion.
                return value.ToString() ?? string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion IsNullString
    }
}
