namespace Identity.Web.Events;

public class UserDeleted : IEvent<UserDeleted>
{
    public UserDeleted(Guid id, DateTime dateTimeUtc)
    {
        this.Id = id;
        this.TimeStampUtc = dateTimeUtc;
    }
    public Guid Id { get; set; }
    public DateTime TimeStampUtc { get; set; }
}