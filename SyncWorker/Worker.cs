namespace SyncWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ISync _syncWorker;

    public Worker(ILogger<Worker> logger, ISync syncWorker)
    {
        _logger = logger;
        _syncWorker = syncWorker;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {Time}", DateTimeOffset.Now);
            await _syncWorker.Sync(stoppingToken);
        }
    }
}