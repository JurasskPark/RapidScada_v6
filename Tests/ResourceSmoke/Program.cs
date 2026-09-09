using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.Loader;
using System.Security.Cryptography;
using System.Text.Json;
using System.Xml.Linq;

internal static class Program
{
    [STAThread]
    private static int Main(string[] args)
    {
        try
        {
            AppContext.SetSwitch("System.Runtime.Serialization.EnableUnsafeBinaryFormatterSerialization", false);
            AppContext.SetSwitch("System.Resources.Extensions.UseBinaryFormatter", false);
            if (args.Length == 2 && args[0] == "--check-binary-resources")
            {
                CheckBinaryResources(args[1]);
                return 0;
            }

            if (args.Length != 1)
            {
                throw new ArgumentException("Pass the path to a built View DLL. Run each DLL in a separate process.");
            }

            string assemblyPath = Path.GetFullPath(args[0]);
            string directory = Path.GetDirectoryName(assemblyPath)!;
            string dependencyDirectory = Path.Combine(directory, Path.GetFileNameWithoutExtension(assemblyPath));
            var resolver = new AssemblyDependencyResolver(assemblyPath);
            AssemblyLoadContext.Default.Resolving += (context, name) =>
            {
                string? resolved = resolver.ResolveAssemblyToPath(name);
                string[] candidates = [resolved ?? "", Path.Combine(directory, name.Name + ".dll"), Path.Combine(dependencyDirectory, name.Name + ".dll")];
                string? match = candidates.FirstOrDefault(File.Exists);
                return match == null ? null : context.LoadFromAssemblyPath(match);
            };

            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            Application.EnableVisualStyles();
            Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(assemblyPath);
            int resources = 0;
            int pictures = 0;
            foreach (string resource in assembly.GetManifestResourceNames().Where(n => n.EndsWith(".resources")))
            {
                var manager = new ResourceManager(resource[..^10], assembly);
                ResourceSet set = manager.GetResourceSet(CultureInfo.InvariantCulture, true, false)!;
                foreach (DictionaryEntry item in set)
                {
                    resources++;
                    if (item.Value is Image image)
                    {
                        using var rendered = new Bitmap(image.Width, image.Height);
                        using var graphics = Graphics.FromImage(rendered);
                        graphics.DrawImageUnscaled(image, 0, 0);
                        pictures++;
                    }
                    else if (item.Value is Icon icon)
                    {
                        using var bitmap = icon.ToBitmap();
                        pictures++;
                    }
                }
                manager.ReleaseAllResources();
            }

            var baseline = JsonSerializer.Deserialize<List<ImageBaseline>>(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "ImageBaseline.json")))!;
            foreach (ImageBaseline item in baseline.Where(b => b.Assembly == assembly.GetName().Name))
            {
                Type type = assembly.GetType(item.Form, true)!;
                object?[] parameters = [];
                if (type.Name == "FrmFilesManager")
                {
                    Type settings = assembly.GetType("Scada.Comm.Drivers.DrvFtpJP.FtpClientSettings", true)!;
                    parameters = [Activator.CreateInstance(settings)];
                }

                // Construct the form without showing it or firing its Load handler (FTP connects there).
                using var form = (Form)Activator.CreateInstance(type, parameters)!;
                var images = (ImageList)type.GetField(item.ListName, BindingFlags.Instance | BindingFlags.NonPublic)!.GetValue(form)!;
                Require(images.Images.Count == item.Keys.Length, item.Form + ": image count changed");
                Require(images.ImageSize == new Size(item.Width, item.Height), item.Form + ": image dimensions changed");
                foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    if (field.GetValue(form) is TabControl tabs && tabs.ImageList == images)
                    {
                        foreach (TabPage page in tabs.TabPages)
                        {
                            Require(page.ImageIndex >= 0 && page.ImageIndex < images.Images.Count, item.Form + ": missing tab image");
                        }
                    }
                }
                for (int i = 0; i < item.Keys.Length; i++)
                {
                    Require(images.Images.Keys[i] == item.Keys[i], item.Form + ": image key changed at " + i);
                    using var bitmap = RenderIcon(images, i);
                    Require(HashPixels(bitmap) == item.Hashes[i], item.Form + ": pixels changed for " + item.Keys[i]);
                }

                // Render actual TreeView nodes with the migrated image list in normal and selected states.
                using var tree = new TreeView { ImageList = images, Size = new Size(320, 180) };
                for (int i = 0; i < item.Keys.Length; i++)
                {
                    tree.Nodes.Add(new TreeNode(item.Keys[i]) { ImageKey = item.Keys[i], SelectedImageKey = item.Keys[i] });
                }
                tree.SelectedNode = tree.Nodes[0];
                using var preview = new Bitmap(tree.Width, tree.Height);
                tree.DrawToBitmap(preview, tree.ClientRectangle);
                Console.WriteLine($"PASS {item.Form}: {item.Keys.Length} image keys and pixel hashes, TreeView rendering");
            }

            // These forms previously read custom ServiceColors through binary deserialization.
            foreach (Type type in assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Form)) && t.Name is "FrmImportCmd" or "FrmExportCmd"))
            {
                if (type.GetConstructor(Type.EmptyTypes) == null)
                {
                    continue;
                }
                using var form = (Form)Activator.CreateInstance(type)!;
                foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
                {
                    if (field.FieldType.FullName != "FastColoredTextBoxNS.FastColoredTextBox")
                    {
                        continue;
                    }
                    object editor = field.GetValue(form)!;
                    object colors = field.FieldType.GetProperty("ServiceColors")!.GetValue(editor)!;
                    Require(colors != null, type.Name + ": missing service colors");
                    Require((Color)colors!.GetType().GetProperty("ExpandMarkerForeColor")!.GetValue(colors)! == Color.Red, type.Name + ": service colors changed");
                    var visibility = (DesignerSerializationVisibilityAttribute)TypeDescriptor.GetProperties(editor)["ServiceColors"]!.Attributes[typeof(DesignerSerializationVisibilityAttribute)]!;
                    Require(visibility.Visibility == DesignerSerializationVisibility.Content, type.Name + ": service colors require content serialization");
                }
                Console.WriteLine($"PASS {type.Name}: constructor and editor colors");
            }

            Console.WriteLine($"PASS {assembly.GetName().Name}: {resources} resources, {pictures} images/icons decoded");
            return 0;
        }
        catch (Exception exception)
        {
            Console.Error.WriteLine(exception);
            return 1;
        }
    }

    private static void CheckBinaryResources(string path)
    {
        // A designer ImageList uses the binary resource format, but .NET 10 reads it without BinaryFormatter.
        var document = XDocument.Load(path);
        var binaryNames = document.Root!.Elements("data")
            .Where(data => (string?)data.Attribute("mimetype") == "application/x-microsoft.net.object.binary.base64")
            .Select(data => (string)data.Attribute("name")!)
            .ToHashSet();
        using var reader = new ResXResourceReader(path) { UseResXDataNodes = true };
        foreach (DictionaryEntry entry in reader)
        {
            if (!binaryNames.Contains((string)entry.Key))
            {
                continue;
            }

            var node = (ResXDataNode)entry.Value!;
            object value = node.GetValue((System.ComponentModel.Design.ITypeResolutionService?)null)!;
            Require(value is ImageListStreamer, path + ": unsupported binary resource " + entry.Key);
            using var streamer = (ImageListStreamer)value;
            using var images = new ImageList { ImageStream = streamer };
            for (int i = 0; i < images.Images.Count; i++)
            {
                using var rendered = RenderIcon(images, i);
            }
            Console.WriteLine($"PASS {Path.GetFileName(path)}: {entry.Key}, {images.Images.Count} images without BinaryFormatter");
        }
    }

    private static Bitmap RenderIcon(ImageList images, int index)
    {
        var bitmap = new Bitmap(images.ImageSize.Width, images.ImageSize.Height * 2);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(Color.White);
        graphics.FillRectangle(Brushes.Black, 0, images.ImageSize.Height, bitmap.Width, images.ImageSize.Height);
        images.Draw(graphics, 0, 0, index);
        images.Draw(graphics, 0, images.ImageSize.Height, index);
        return bitmap;
    }

    private static string HashPixels(Bitmap bitmap)
    {
        using var pixels = new MemoryStream();
        using var writer = new BinaryWriter(pixels);
        for (int y = 0; y < bitmap.Height; y++)
        {
            for (int x = 0; x < bitmap.Width; x++)
            {
                Color color = bitmap.GetPixel(x, y);
                writer.Write(color.A == 0 ? 0 : color.ToArgb());
            }
        }
        return Convert.ToHexString(SHA256.HashData(pixels.ToArray()));
    }

    private static void Require(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    private sealed record ImageBaseline(string Assembly, string Form, string ListName, int Width, int Height, string[] Keys, string[] Hashes);
}
