using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.Json;

internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            if (args.Length != 3)
            {
                throw new ArgumentException("Pass an extracted module DLL, metadata JSON and host-library directory.");
            }

            AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", false);
            AppContext.SetSwitch("System.Resources.Extensions.UseBinaryFormatter", false);
            string path = Path.GetFullPath(args[0]);
            string directory = Path.GetDirectoryName(path)!;
            string privateDirectory = Path.Combine(directory, Path.GetFileNameWithoutExtension(path));
            if (!Directory.Exists(privateDirectory))
            {
                privateDirectory = directory;
            }
            string hostDirectory = Path.GetFullPath(args[2]);
            AppDomain.CurrentDomain.AssemblyResolve += (_, request) =>
            {
                string name = new AssemblyName(request.Name).Name + ".dll";
                string? requestingDirectory = Path.GetDirectoryName(request.RequestingAssembly?.Location);
                string[] directories = [requestingDirectory ?? directory, privateDirectory, directory, hostDirectory];
                string? match = directories.Select(d => Path.Combine(d, name)).FirstOrDefault(File.Exists);
                return match == null ? null : Assembly.LoadFrom(match);
            };

            Assembly assembly = Assembly.LoadFrom(path);
            Type[] types = assembly.GetTypes();
            foreach (AssemblyName reference in assembly.GetReferencedAssemblies())
            {
                Assembly dependency = Assembly.Load(reference);
                if (reference.Name!.StartsWith("Drv", StringComparison.Ordinal))
                {
                    foreach (AssemblyName nested in dependency.GetReferencedAssemblies())
                    {
                        _ = Assembly.Load(nested);
                    }
                }
            }
            Dictionary<string, string> metadata = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(args[1]))!;
            Type? utils = types.SingleOrDefault(t => t.Name == "DriverUtils");
            if (utils != null)
            {
                Require((string)utils.GetField("DriverCode")!.GetValue(null)! == metadata["Id"], "Driver code differs from the package ID.");
                Require((string)utils.GetProperty("Version")!.GetValue(null)! == assembly.GetName().Version!.ToString(), "DriverUtils.Version differs from the DLL version.");
                foreach (bool russian in new[] { true, false })
                {
                    string suffix = russian ? "Ru" : "En";
                    Require((string)utils.GetMethod("Name")!.Invoke(null, [russian])! == metadata["Name" + suffix], "UI name differs from README.");
                    Require((string)utils.GetMethod("Description")!.Invoke(null, [russian])! == metadata["Description" + suffix], "UI description differs from README.");
                }
            }

            string sqlPath = Path.Combine(privateDirectory, "Microsoft.Data.SqlClient.dll");
            if (File.Exists(sqlPath))
            {
                Assembly sql = Assembly.LoadFrom(sqlPath);
                using var connection = (IDisposable)Activator.CreateInstance(sql.GetType("Microsoft.Data.SqlClient.SqlConnection", true)!)!;
                string nativePath = Path.Combine(privateDirectory, "Microsoft.Data.SqlClient.SNI.dll");
                if (File.Exists(nativePath))
                {
                    nint handle = NativeLibrary.Load(nativePath);
                    NativeLibrary.Free(handle);
                }
                Console.WriteLine("PASS SQL client constructor and native library (no connection opened)");
            }

            string editorPath = Path.Combine(privateDirectory, "FastColoredTextBox.dll");
            if (File.Exists(editorPath))
            {
                Assembly editor = Assembly.LoadFrom(editorPath);
                using var control = (Control)Activator.CreateInstance(editor.GetType("FastColoredTextBoxNS.FastColoredTextBox", true)!)!;
                control.Text = "SELECT 1;";
                Require(control.Text == "SELECT 1;", "Editor did not accept text.");
                Require(editor.GetName().FullName == "FastColoredTextBox, Version=2.16.26.0, Culture=neutral, PublicKeyToken=fb8aa12b994ef61b", "Editor assembly identity changed.");
                Console.WriteLine("PASS packaged FastColoredTextBox constructor and text");
            }

            if (assembly.GetName().Name == "DrvDDEJP.Logic")
            {
                string ddePath = Path.Combine(directory, "DrvDDEJP.DDE.dll");
                Require(File.Exists(ddePath), "DDE helper is missing next to the logic DLL.");
                Require(Assembly.LoadFrom(ddePath).GetTypes().Length > 0, "DDE helper has no loadable types.");
            }
            Console.WriteLine($"PASS {assembly.GetName().Name}: {types.Length} types, metadata and dependencies [{RuntimeInformation.ProcessArchitecture}]");
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception);
            if (exception is ReflectionTypeLoadException typeError)
            {
                foreach (Exception? loaderError in typeError.LoaderExceptions)
                {
                    Console.Error.WriteLine(loaderError);
                }
            }
            return 1;
        }
    }

    private static void Require(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }
}
