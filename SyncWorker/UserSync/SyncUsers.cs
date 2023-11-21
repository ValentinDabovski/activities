using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SyncWorker.UserSync;

public class SyncUsers : ISync
{
    private readonly IModel? _channel;
    private readonly Dictionary<string, IHandleEvents> _eventHandlers;
    private readonly ILogger<SyncUsers> _logger;

    public SyncUsers(ILogger<SyncUsers> logger, IEnumerable<IHandleEvents> eventHandlers, IConnection connection)
    {
        _eventHandlers = eventHandlers.ToDictionary(handler => handler.EventType);
        _logger = logger;
        _channel = connection.CreateModel();
    }

    public async Task Sync(CancellationToken cancellationToken = default)
    {
        _channel!.QueueDeclare("SSO",
            false,
            false,
            false,
            null);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            _logger.LogInformation("event received");
            _logger.LogInformation("processing...");

            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var @event = JsonConvert.DeserializeObject<Event>(message);

            _logger.LogInformation("{EventType}", @event.Type);

            await HandleEvent(@event);
        };

        var consumerTag = _channel.BasicConsume("SSO",
            true,
            consumer);

        var completionSource = new TaskCompletionSource<bool>();
        cancellationToken.Register(() =>
        {
            _channel.BasicCancel(consumerTag);
            completionSource.TrySetResult(true);
        });

        await completionSource.Task;
    }

    private async Task HandleEvent(Event @event)
    {
        if (_eventHandlers.TryGetValue(@event.Type, out var handler))
            await handler.Handle(JObject.FromObject(@event.Data));
        else
            _logger.LogInformation("Handler not found");
    }
}