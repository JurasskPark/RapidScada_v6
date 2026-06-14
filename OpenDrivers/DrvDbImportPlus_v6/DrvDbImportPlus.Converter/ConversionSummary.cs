// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Scada.Comm.Drivers.DrvDbImportPlus.Converter
{
    /// <summary>
    /// Represents a conversion summary.
    /// <para>Представляет результат конвертации.</para>
    /// </summary>
    internal sealed class ConversionSummary
    {
        /// <summary>
        /// Gets or sets the number of found files.
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the number of converted files.
        /// </summary>
        public int ConvertedCount { get; set; }

        /// <summary>
        /// Gets or sets the number of failed files.
        /// </summary>
        public int FailedCount { get; set; }

        /// <summary>
        /// Gets the conversion errors.
        /// </summary>
        public List<string> Errors { get; } = new List<string>();
    }
}
