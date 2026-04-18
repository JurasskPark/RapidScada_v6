using DdeNet.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SampleServer;

public sealed class DdeServerWorker : BackgroundService
{
    private readonly ILogger<DdeServerWorker> _logger;
    private DdeServer _server;

    public DdeServerWorker(ILogger<DdeServerWorker> logger)
    {
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting DDE server service 'myapp'.");
        FileLog.Write("Worker start requested.");

        _server = new MyServer("myapp");
        _server.Register();

        _logger.LogInformation("DDE server registered successfully.");
        FileLog.Write("DDE server registered: service='myapp'.");
        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping DDE server.");
        FileLog.Write("Worker stop requested.");

        if (_server != null)
        {
            _server.Unregister();
            _server.Dispose();
            _server = null;
        }

        _logger.LogInformation("DDE server stopped.");
        FileLog.Write("DDE server stopped.");
        return base.StopAsync(cancellationToken);
    }
}
