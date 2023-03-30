namespace Identity.Web.Events;

public class UserRegistered : IEvent<UserRegistered>
{
    public UserRegistered(Guid id, string email, DateTime dateTimeUtc)
    {
        Id = id;
        Email = email;
        this.TimeStampUtc = dateTimeUtc;
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; }
    
    public  DateTime TimeStampUtc { get; private set; }
}