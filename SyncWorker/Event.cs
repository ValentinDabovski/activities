namespace SyncWorker;
public class Event
{
    public Event(string eventType, object data)
    {
        this.Type = eventType;
        this.Data = data;
    }
    public string Type { get;  set; }
    public object Data { get;  set; }
}