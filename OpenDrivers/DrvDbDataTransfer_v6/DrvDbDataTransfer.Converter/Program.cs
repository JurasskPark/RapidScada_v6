// Copyright (c) Rapid Software LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Scada.Comm.Drivers.DrvDbDataTransfer.Converter
{
    /// <summary>
    /// The application entry point.
    /// <para>Точка входа приложения.</para>
    /// </summary>
    internal static class Program
    {
        private static int Main(string[] args)
        {
            ConsoleRunner runner = new ConsoleRunner(new ProjectConverter());

            if (args.Length == 0)
            {
                return runner.RunInteractive();
            }
            else
            {
                return runner.RunCommandLine(args);
            }
        }
    }
}
