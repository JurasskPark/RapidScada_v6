// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Scada.Comm.Drivers.DrvDbImportPlus.Converter
{
    /// <summary>
    /// Represents converter options.
    /// <para>Представляет параметры конвертера.</para>
    /// </summary>
    internal sealed class ConverterOptions
    {
        /// <summary>
        /// Gets the source paths.
        /// </summary>
        public List<string> SourcePaths { get; } = new List<string>();

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        public string OutputPath { get; set; } = "";

        /// <summary>
        /// Gets or sets a value indicating whether existing files can be overwritten.
        /// </summary>
        public bool Overwrite { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether help should be displayed.
        /// </summary>
        public bool ShowHelp { get; set; }

        /// <summary>
        /// Parses command line arguments.
        /// </summary>
        public static ConverterOptions Parse(string[] args)
        {
            ConverterOptions options = new ConverterOptions();
            List<string> paths = new List<string>();

            foreach (string arg in args)
            {
                if (IsHelpArg(arg))
                {
                    options.ShowHelp = true;
                }
                else if (arg.Equals("--overwrite", StringComparison.OrdinalIgnoreCase))
                {
                    options.Overwrite = true;
                }
                else
                {
                    paths.Add(arg);
                }
            }

            if (paths.Count > 0)
            {
                options.SourcePaths.Add(paths[0]);
            }

            if (paths.Count > 1)
            {
                options.OutputPath = paths[1];
            }

            return options;
        }

        /// <summary>
        /// Creates options for interactive input.
        /// </summary>
        public static ConverterOptions CreateInteractive(List<string> sourcePaths)
        {
            ConverterOptions options = new ConverterOptions
            {
                Overwrite = true
            };

            foreach (string sourcePath in sourcePaths)
            {
                options.SourcePaths.Add(sourcePath);
            }

            options.OutputPath = GetDefaultOutputPath(sourcePaths);
            return options;
        }

        private static bool IsHelpArg(string arg)
        {
            return arg.Equals("--help", StringComparison.OrdinalIgnoreCase) ||
                arg.Equals("-h", StringComparison.OrdinalIgnoreCase) ||
                arg.Equals("/?", StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Gets the default output path.
        /// </summary>
        public static string GetDefaultOutputPath(List<string> sourcePaths)
        {
            string basePath = Directory.GetCurrentDirectory();

            if (sourcePaths.Count > 0)
            {
                string firstPath = Path.GetFullPath(sourcePaths[0]);

                if (Directory.Exists(firstPath))
                {
                    basePath = firstPath;
                }
                else if (File.Exists(firstPath))
                {
                    basePath = Path.GetDirectoryName(firstPath) ?? basePath;
                }
            }

            return Path.Combine(basePath, ConversionConstants.OutputDirectoryName);
        }
    }
}
