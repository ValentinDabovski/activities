using Newtonsoft.Json.Linq;
using Persistence;
using Persistence.Models;
using SyncWorker.UserSync.Events;

namespace SyncWorker.UserSync.EventHandlers;

public class UserRegisteredEventHandler : IHandleEvents
{
    private readonly IServiceProvider _serviceProvider;

    public UserRegisteredEventHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public string EventType => "UserRegistered";

    public async Task Handle(JObject @event)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        var data = @event.ToObject<UserRegistered>();

        if (data != null)
        {
            var user = dbContext.Users.Add(new UserEntity
            {
                Email = data.Email,
                Id = data.Id
            });

            await dbContext.SaveChangesAsync();
        }
    }
}