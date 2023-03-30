namespace Identity.Web.Events;

public class Event
{
    public Event(string eventType, object data)
    {
        this.Type = eventType;
        this.Data = data;
    }
    public string Type { get; protected set; }
    public object Data { get; private set; }
}