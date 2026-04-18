using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleServer;

internal static class Program
{
    static void Main(string[] args)
    {
        FileLog.Initialize(AppContext.BaseDirectory);
        FileLog.Write("Application starting.");

        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddWindowsService(options =>
        {
            options.ServiceName = "SampleDdeServer";
        });

        builder.Logging.ClearProviders();
        builder.Logging.AddSimpleConsole(options =>
        {
            options.TimestampFormat = "yyyy-MM-dd HH:mm:ss ";
            options.SingleLine = true;
        });

        builder.Services.AddHostedService<DdeServerWorker>();

        using IHost host = builder.Build();
        host.Run();
    }
}
