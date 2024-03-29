using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models;

public class Activity : Entity, IAggregateRoot
{
    internal Activity(string title, string description, bool isAvailable, DateTime date, Address address,
        Category category)
    {
        ValidateAgainstEmptyString(title, nameof(title));
        ValidateAgainstEmptyString(description, nameof(description));
        ValidateAgainstEmptyString(address.City, nameof(address.City));
        ValidateAgainstEmptyString(category.Name, nameof(category.Name));

        Title = title;
        Description = description;
        IsAvailable = isAvailable;
        Date = date;
        Category = category;
        Address = address;
    }

    private Activity()
    {
    }

    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public bool IsAvailable { get; private set; }
    public string Description { get; private set; }
    public Category Category { get; private set; }
    public Address Address { get; private set; }
    public Guid? UserId { get; private set; } = null;

    public Activity UpdateDate(DateTime newDate)
    {
        Date = newDate;

        return this;
    }

    public Activity UpdateTitle(string newTitle)
    {
        Title = newTitle;

        return this;
    }

    public Activity UpdateDescription(string newDescription)
    {
        Description = newDescription;

        return this;
    }

    public Activity UpdateCategory(string name, string description)
    {
        Category = new Category(name, description);

        return this;
    }

    public Activity UpdateAddress(string street, string city, string state, string country, string zipcode,
        string venue)
    {
        Address = new Address(street, city, state, country, zipcode, venue);

        return this;
    }

    public Activity ChangeAvailability()
    {
        IsAvailable = !IsAvailable;
        return this;
    }

    private void ValidateAgainstEmptyString(string stringForValidation, string paramName)
    {
        Guard.AgainstEmptyString<InvalidActivityException>(stringForValidation, paramName);
    }
}