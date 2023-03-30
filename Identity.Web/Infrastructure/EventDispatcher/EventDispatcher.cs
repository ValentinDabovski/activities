
using System.Text;
using Identity.Web.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Identity.Web.Infrastructure.EventDispatcher;
public class EventDispatcher : IEventSink
{
    private readonly ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
    public void Send<T>(IEvent<T> @event)
    {
        var eventType = @event.GetType().Name;
        var eventData = new Event(eventType, @event);
        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventData));
        
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: "SSO",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        channel.BasicPublish(exchange: string.Empty, 
             routingKey: "SSO",
             basicProperties: null,
             body: body);
    }
}