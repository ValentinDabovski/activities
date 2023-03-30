using Newtonsoft.Json.Linq;
using SyncWorker.UserSync.Events;

namespace SyncWorker.UserSync.EventHandlers;

public class UserRegisteredEventHandler : IHandleEvents
{
    public string EventType => "UserRegistered";

    public  Task Handle(JObject @event)
    {
        var data = @event.ToObject<UserRegistered>();

        //TODO: handle event
        
        return Task.CompletedTask;
    }
}