using Identity.Web.Events;

namespace Identity.Web.Infrastructure.EventDispatcher;

public interface IEventSink
{
    public void Send<T>(IEvent<T> @event);
}