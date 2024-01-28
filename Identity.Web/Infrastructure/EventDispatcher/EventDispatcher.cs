using System.Text;
using Identity.Web.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Identity.Web.Infrastructure.EventDispatcher;

public class EventDispatcher : IEventSink
{
    private readonly ConnectionFactory _factory = new() { HostName = "rabbitmq" };

    public void Send<T>(IEvent<T> @event)
    {
        var eventType = @event.GetType().Name;
        var eventData = new Event(eventType, @event);
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventData));

        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();


        channel.QueueDeclare("SSO",
            false,
            false,
            false,
            null);

        channel.BasicPublish(string.Empty,
            "SSO",
            null,
            body);
    }
}