// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Scada.Comm.Drivers.DrvDbDataTransfer.Converter
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
        public const string OldRootName = "DrvDbDataTransferConfig";

        /// <summary>
        /// The new configuration root element.
        /// </summary>
        public const string NewRootName = "DrvDbDataTransferProject";

        /// <summary>
        /// The driver configuration file search pattern.
        /// </summary>
        public const string FileSearchPattern = "DrvDbDataTransfer_*.xml";

        /// <summary>
        /// The default output directory name.
        /// </summary>
        public const string OutputDirectoryName = "output";
    }
}
