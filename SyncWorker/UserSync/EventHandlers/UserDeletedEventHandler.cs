using Newtonsoft.Json.Linq;
using SyncWorker.UserSync.Events;

namespace SyncWorker.UserSync.EventHandlers;

public class UserDeletedEventHandler : IHandleEvents
{
    public string EventType => "UserDeleted";
    public Task Handle(JObject @event)
    {
        var data = @event.ToObject<UserDeleted>();

        //TODO: handle event
        
        return Task.CompletedTask;
    }
}