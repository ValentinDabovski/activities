using Newtonsoft.Json.Linq;

namespace SyncWorker;

public interface IHandleEvents
{
    string EventType { get; }
    Task Handle(JObject @event);
}