using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Scada.Comm.Drivers.DrvDDEJP
{
    /// <summary>
    /// Creates platform-specific driver clients.
    /// <para>Создаёт платформозависимые клиенты драйвера.</para>
    /// </summary>
    public static class DriverClientFactory
    {
        #region Variable

        private const string DdeAssemblyName = "DrvDDEJP.DDE";                          // name of the platform-specific DDE assembly
        private const string DdeTypeName = "Scada.Comm.Drivers.DrvDDEJP.DriverClient";  // fully qualified type name of the DDE client

        #endregion Variable

        #region Basic

        /// <summary>
        /// Creates a driver client for the current platform.
        /// <para>Создаёт клиент драйвера для текущей платформы.</para>
        /// </summary>
        /// <param name="project">Project configuration used to construct the client.</param>
        /// <param name="logAction">Optional logging delegate.</param>
        /// <returns>An <see cref="IDriverClient"/> implementation appropriate for the platform.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="project"/> is null.</exception>
        public static IDriverClient Create(Project project, Action<string> logAction = null)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new UnsupportedDriverClient("DDE is not supported on this platform.", logAction);
            }

            string assemblyPath = ResolveAssemblyPath();
            if (string.IsNullOrWhiteSpace(assemblyPath))
            {
                return new UnsupportedDriverClient(
                    $"DDE client assembly is not found. Probed: {string.Join("; ", EnumerateProbePaths())}",
                    logAction);
            }

            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                Type clientType = assembly.GetType(DdeTypeName, throwOnError: true);
                
                if (Activator.CreateInstance(clientType, project, logAction) is IDriverClient client)
                {
                    return client;
                }

                return new UnsupportedDriverClient("DDE client does not implement IDriverClient interface.", logAction);
            }
            catch (Exception ex)
            {
                return new UnsupportedDriverClient($"Error creating DDE client: {ex.Message}", logAction);
            }
        }

        #endregion Basic

        #region Private Methods

        /// <summary>
        /// Resolves the path to the platform-specific assembly by probing likely locations.
        /// <para>Разрешает путь к платформозависимой сборке, перебирая возможные пути.</para>
        /// </summary>
        /// <returns>Full path to the assembly if found; otherwise an empty string.</returns>
        private static string ResolveAssemblyPath()
        {
            foreach (string path in EnumerateProbePaths())
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Enumerates probe paths where the DDE assembly may be located.
        /// <para>Перечисляет пути, в которых может находиться сборка DDE.</para>
        /// </summary>
        /// <returns>An enumerable of full file paths to probe.</returns>
        private static IEnumerable<string> EnumerateProbePaths()
        {
            HashSet<string> paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string fileName = DdeAssemblyName + ".dll";

            string logicAssemblyDir = Path.GetDirectoryName(typeof(DriverClientFactory).Assembly.Location);
            if (!string.IsNullOrWhiteSpace(logicAssemblyDir))
            {
                paths.Add(Path.Combine(logicAssemblyDir, fileName));
            }

            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrWhiteSpace(baseDirectory))
            {
                paths.Add(Path.Combine(baseDirectory, fileName));
                paths.Add(Path.Combine(baseDirectory, "Drv", fileName));
            }

            return paths;
        }

        #endregion Private Methods

        #region Private Class

        /// <summary>
        /// Fallback client used on unsupported platforms or when the DDE assembly is missing.
        /// <para>Запасной клиент, используемый на неподдерживаемых платформах или при отсутствии сборки DDE.</para>
        /// </summary>
        private sealed class UnsupportedDriverClient : IDriverClient
        {
            private readonly string message;
            private readonly Action<string> logAction;

            public UnsupportedDriverClient(string message, Action<string> logAction)
            {
                this.message = message;
                this.logAction = logAction;
                this.logAction?.Invoke("[DriverClientFactory] " + message);
            }

            public bool IsConnected => false;

            public void Connect() => throw new PlatformNotSupportedException(message);

            public void Disconnect() { }

            public string ReadTag(ProjectTag tag) => throw new PlatformNotSupportedException(message);

            public Dictionary<Guid, string> ReadTags(IEnumerable<ProjectTag> tags) => throw new PlatformNotSupportedException(message);

            public string ResolveTopic(ProjectTag tag) => string.Empty;

            public string ResolveItemName(ProjectTag tag) => string.Empty;

            public void Dispose() { }
        }

        #endregion Private Class
    }
}
