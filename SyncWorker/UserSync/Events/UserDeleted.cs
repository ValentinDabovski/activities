namespace SyncWorker.UserSync.Events;

public class UserDeleted
{
    public Guid Id { get;  set; }
    
    public DateTime TimeStampUtc { get; set; }
}