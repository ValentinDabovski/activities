using Domain.Factories.Users;
using Newtonsoft.Json.Linq;
using Persistence;
using SyncWorker.UserSync.Events;

namespace SyncWorker.UserSync.EventHandlers;

public class UserRegisteredEventHandler : IHandleEvents
{
    private readonly IServiceProvider _serviceProvider;
    private readonly UserFactory _userFactory;

    public UserRegisteredEventHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _userFactory = new UserFactory();
    }

    public string EventType => "UserRegistered";

    public async Task Handle(JObject @event)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        var data = @event.ToObject<UserRegistered>();

        if (data != null)
        {
            var user = _userFactory
                .WithEmail(data.Email)
                .WithId(data.Id)
                .Build();

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
    }
}