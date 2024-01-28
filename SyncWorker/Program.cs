using Microsoft.EntityFrameworkCore;
using Persistence;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using SyncWorker;
using SyncWorker.UserSync;
using SyncWorker.UserSync.EventHandlers;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        services.AddHostedService<Worker>();

        services.AddSingleton<IConnection>(sp =>
        {
            var policy = Policy.Handle<BrokerUnreachableException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            
            var rabbitMqHost = new ConnectionFactory { HostName = "rabbitmq" };

            return policy.Execute(() => rabbitMqHost.CreateConnection());
            
        });

        services
            .AddDbContext<DataContext>
            (
                opt =>
                    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
        services.AddSingleton<ISync, SyncUsers>();
        services.AddTransient<IHandleEvents, UserRegisteredEventHandler>();
        services.AddTransient<IHandleEvents, UserDeletedEventHandler>();
    })
    .Build();
host.Run();