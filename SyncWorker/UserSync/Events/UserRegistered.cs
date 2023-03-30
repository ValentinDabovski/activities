namespace SyncWorker.UserSync.Events;

public class UserRegistered
{
    public Guid Id { get;  set; }

    public string? Email { get;  set; }
    
    public  DateTime TimeStampUtc { get;  set; }
}