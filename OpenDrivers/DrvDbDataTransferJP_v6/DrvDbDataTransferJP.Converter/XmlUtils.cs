// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Xml;

namespace Scada.Comm.Drivers.DrvDbDataTransferJP.Converter
{
    /// <summary>
    /// Provides XML helper methods.
    /// <para>Предоставляет вспомогательные методы XML.</para>
    /// </summary>
    internal static class XmlUtils
    {
        /// <summary>
        /// Gets the child text.
        /// </summary>
        public static string GetChildText(XmlNode xmlNode, string childName)
        {
            return xmlNode.SelectSingleNode(childName)?.InnerText ?? "";
        }

        /// <summary>
        /// Gets the child integer value.
        /// </summary>
        public static int GetChildInt(XmlNode xmlNode, string childName, int defaultValue)
        {
            string value = GetChildText(xmlNode, childName);
            return int.TryParse(value, out int result) ? result : defaultValue;
        }

        /// <summary>
        /// Gets the child Boolean value.
        /// </summary>
        public static bool GetChildBool(XmlNode xmlNode, string childName, bool defaultValue)
        {
            string value = GetChildText(xmlNode, childName);
            return bool.TryParse(value, out bool result) ? result : defaultValue;
        }

        /// <summary>
        /// Gets the child GUID value.
        /// </summary>
        public static Guid GetChildGuid(XmlNode xmlNode, string childName, Guid defaultValue)
        {
            string value = GetChildText(xmlNode, childName);
            return Guid.TryParse(value, out Guid result) ? result : defaultValue;
        }

        /// <summary>
        /// Gets the child enumeration value.
        /// </summary>
        public static T GetChildEnum<T>(XmlNode xmlNode, string childName, T defaultValue)
            where T : struct
        {
            string value = GetChildText(xmlNode, childName);
            return Enum.TryParse(value, true, out T result) ? result : defaultValue;
        }
    }
}
