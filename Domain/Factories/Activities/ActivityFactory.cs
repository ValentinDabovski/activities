using Domain.Models;

namespace Domain.Factories.Activities;

public class ActivityFactory : IActivityFactory
{
    private readonly bool _isAvailable = false;
    private Address _address;
    private Category _category;
    private DateTime _date = DateTime.Now;
    private string _description;
    private string _title;


    public Activity Build()
    {
        return new Activity(
            _title,
            _description,
            _isAvailable,
            _date,
            _address,
            _category
        );
    }

    public IActivityFactory WithAddress(Address address)
    {
        _address = address;
        return this;
    }

    public IActivityFactory WithCategory(Category category)
    {
        _category = category;
        return this;
    }

    public IActivityFactory WithDate(DateTime date)
    {
        _date = date;
        return this;
    }

    public IActivityFactory WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public IActivityFactory WithTitle(string title)
    {
        _title = title;
        return this;
    }
}