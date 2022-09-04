// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using System.Text;

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
    }
}