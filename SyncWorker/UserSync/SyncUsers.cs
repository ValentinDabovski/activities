using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SyncWorker.UserSync;
public class SyncUsers :ISync
{
    private readonly IModel? channel;
    private readonly ILogger<SyncUsers> logger;
    private readonly Dictionary<string, IHandleEvents> eventHandlers;

    public SyncUsers(ILogger<SyncUsers> logger, IEnumerable<IHandleEvents> eventHandlers, IConnection connection)
    {
        this.eventHandlers = eventHandlers.ToDictionary(handler => handler.EventType);
        this.logger = logger;
        this.channel = connection.CreateModel();
    }
    
    public async Task Sync(CancellationToken cancellationToken = default)
    {
        channel!.QueueDeclare(queue: "SSO",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var consumer = new AsyncEventingBasicConsumer(channel);
        
        consumer.Received += async (model, ea)  =>
        {
            logger.LogInformation("event received");
            logger.LogInformation("processing...");
                
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var @event = JsonConvert.DeserializeObject<Event>(message);
            
            logger.LogInformation(@event.Type);

            await this.HandleEvent(@event);
        };
        
        var consumerTag = channel.BasicConsume(queue: "SSO",
            autoAck: true,
            consumer: consumer);
        
        var completionSource = new TaskCompletionSource<bool>();
        cancellationToken.Register(() =>
        {
            channel.BasicCancel(consumerTag);
            completionSource.TrySetResult(true);
        });
        
        await completionSource.Task;
    }

    private async Task HandleEvent(Event @event)
    {
        if (eventHandlers.TryGetValue(@event.Type, out var handler))
        {
            await handler.Handle(JObject.FromObject(@event.Data));
        }
        else
        {
            logger.LogInformation("Handler not found.");
        }
    }
}