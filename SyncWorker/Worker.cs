using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SyncWorker;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> logger;
    private readonly ISync syncWorker;

    public Worker(ILogger<Worker> logger, ISync syncWorker)
    {
        this.logger = logger;
        this.syncWorker = syncWorker;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await Task.Delay(6000, stoppingToken);

           await this.syncWorker.Sync(stoppingToken);
        }
    }
}