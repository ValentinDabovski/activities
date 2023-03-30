using RabbitMQ.Client;
using SyncWorker;
using SyncWorker.UserSync;
using SyncWorker.UserSync.EventHandlers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        services.AddSingleton<IConnection>(sp => {
            var factory = new ConnectionFactory { HostName = "localhost" };
            return factory.CreateConnection();
        });
        
        services.AddSingleton<ISync, SyncUsers>();
        services.AddTransient<IHandleEvents, UserRegisteredEventHandler>();
        services.AddTransient<IHandleEvents, UserDeletedEventHandler>();

    })
    .Build();

host.Run();