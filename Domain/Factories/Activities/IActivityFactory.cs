using Domain.Models;

namespace Domain.Factories.Activities
{
    public interface IActivityFactory : IFactory<Activity>
    {
        IActivityFactory WithTitle(string title);
        IActivityFactory WithDescription(string title);
        IActivityFactory WithDate(DateTime date);
        IActivityFactory WithAddress(Address address);
        IActivityFactory WithCategory(Category category);
    }
}