using Domain.Models;

namespace Domain.Factories.Activities;

public interface IActivityFactory : IFactory<Activity>
{
    IActivityFactory WithTitle(string title);
    IActivityFactory WithDescription(string title);
    IActivityFactory WithDate(DateTime date);

    IActivityFactory WithAddress(string street, string city, string state, string country, string zipcode,
        string venue);

    IActivityFactory WithCategory(string name, string description);
}