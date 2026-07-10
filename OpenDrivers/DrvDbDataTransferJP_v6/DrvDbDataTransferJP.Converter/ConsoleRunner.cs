// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Scada.Comm.Drivers.DrvDbDataTransferJP.Converter
{
    /// <summary>
    /// Implements console interaction.
    /// <para>Реализует взаимодействие с консолью.</para>
    /// </summary>
    internal sealed class ConsoleRunner
    {
        private readonly ProjectConverter projectConverter;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public ConsoleRunner(ProjectConverter projectConverter)
        {
            this.projectConverter = projectConverter ?? throw new ArgumentNullException(nameof(projectConverter));
        }

        /// <summary>
        /// Runs the converter in command line mode.
        /// </summary>
        public int RunCommandLine(string[] args)
        {
            ConverterOptions options = ConverterOptions.Parse(args);

            if (options.ShowHelp || options.SourcePaths.Count == 0)
            {
                PrintUsage();
                return 0;
            }

            return ConvertAndPrint(options);
        }

        /// <summary>
        /// Runs the converter in interactive mode.
        /// </summary>
        public int RunInteractive()
        {
            PrintHeader();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Drag XML files or a config directory here and press Enter.");
                Console.WriteLine("Commands: help, exit");
                Console.Write("> ");

                string input = Console.ReadLine();
                if (input == null)
                {
                    return 0;
                }

                input = input.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }

                if (IsExitCommand(input))
                {
                    return 0;
                }

                if (IsHelpCommand(input))
                {
                    PrintUsage();
                    continue;
                }

                List<string> sourcePaths = ParseInputPaths(input);
                if (sourcePaths.Count == 0)
                {
                    Console.WriteLine("No source paths found.");
                    continue;
                }

                ConverterOptions options = ConverterOptions.CreateInteractive(sourcePaths);
                Console.WriteLine($"Output: {options.OutputPath}");
                ConvertAndPrint(options);
            }
        }

        private int ConvertAndPrint(ConverterOptions options)
        {
            try
            {
                ConversionSummary summary = projectConverter.Convert(options);
                PrintSummary(summary);
                return summary.FailedCount == 0 ? 0 : 1;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Conversion failed.");
                Console.Error.WriteLine(ex.Message);
                return 2;
            }
        }

        private static List<string> ParseInputPaths(string input)
        {
            List<string> paths = new List<string>();
            List<string> tokens = SplitCommandLine(input);

            foreach (string token in tokens)
            {
                string path = token.Trim().Trim('"');

                if (File.Exists(path) || Directory.Exists(path))
                {
                    paths.Add(path);
                }
            }

            return paths;
        }

        private static List<string> SplitCommandLine(string input)
        {
            List<string> tokens = new List<string>();
            bool inQuote = false;
            int tokenStart = -1;

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];

                if (ch == '"')
                {
                    inQuote = !inQuote;

                    if (tokenStart < 0)
                    {
                        tokenStart = i;
                    }
                }
                else if (char.IsWhiteSpace(ch) && !inQuote)
                {
                    AppendToken(input, tokens, tokenStart, i);
                    tokenStart = -1;
                }
                else if (tokenStart < 0)
                {
                    tokenStart = i;
                }
            }

            AppendToken(input, tokens, tokenStart, input.Length);
            return tokens;
        }

        private static void AppendToken(string input, List<string> tokens, int tokenStart, int tokenEnd)
        {
            if (tokenStart < 0 || tokenEnd <= tokenStart)
            {
                return;
            }

            string token = input.Substring(tokenStart, tokenEnd - tokenStart).Trim();
            if (!string.IsNullOrWhiteSpace(token))
            {
                tokens.Add(token);
            }
        }

        private static bool IsExitCommand(string input)
        {
            return input.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("quit", StringComparison.OrdinalIgnoreCase);
        }

        private static bool IsHelpCommand(string input)
        {
            return input.Equals("help", StringComparison.OrdinalIgnoreCase) ||
                input.Equals("?", StringComparison.OrdinalIgnoreCase);
        }

        private static void PrintHeader()
        {
            Console.WriteLine("DrvDbDataTransferJP old project converter");
            Console.WriteLine("Interactive mode");
        }

        private static void PrintUsage()
        {
            Console.WriteLine("DrvDbDataTransferJP old project converter");
            Console.WriteLine();
            Console.WriteLine("Command line:");
            Console.WriteLine("  DrvDbDataTransferJP.Converter <source-config-dir-or-xml> [output-config-dir] [--overwrite]");
            Console.WriteLine();
            Console.WriteLine("Interactive:");
            Console.WriteLine("  Run without arguments, drag XML files or a config directory into the console, then press Enter.");
            Console.WriteLine("  The converter creates an output directory near the source files.");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine(@"  DrvDbDataTransferJP.Converter C:\SCADA\ProjectSamples\HelloWorld_Database\Instances\Default\ScadaComm\Config C:\Temp\DrvDbDataTransferJPConverted");
        }

        private static void PrintSummary(ConversionSummary summary)
        {
            Console.WriteLine($"Files found: {summary.TotalCount}");
            Console.WriteLine($"Converted: {summary.ConvertedCount}");
            Console.WriteLine($"Failed: {summary.FailedCount}");

            foreach (string error in summary.Errors)
            {
                Console.WriteLine(error);
            }
        }
    }
}
