// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Scada.Comm.Drivers.DrvDbDataTransferJP.Converter
{
    /// <summary>
    /// Defines constants used by the converter.
    /// <para>Определяет константы, используемые конвертером.</para>
    /// </summary>
    internal static class ConversionConstants
    {
        /// <summary>
        /// The old configuration root element.
        /// </summary>
        public const string OldRootName = "DrvDbDataTransferJPConfig";

        /// <summary>
        /// The new configuration root element.
        /// </summary>
        public const string NewRootName = "DrvDbDataTransferJPProject";

        /// <summary>
        /// The driver configuration file search pattern.
        /// </summary>
        public const string FileSearchPattern = "DrvDbDataTransferJP_*.xml";

        /// <summary>
        /// The default output directory name.
        /// </summary>
        public const string OutputDirectoryName = "output";
    }
}
