using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models;

public class Activity : IAggregateRoot
{
    internal Activity(string title, string description, bool isAvailable, DateTime date, Address address,
        Category category)
    {
        ValidateAgainstEmptyString(title, nameof(title));
        ValidateAgainstEmptyString(description, nameof(description));
        ValidateAgainstEmptyString(address.City, nameof(address.City));
        ValidateAgainstEmptyString(category.Name, nameof(category.Name));

        Id = Guid.NewGuid();
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

    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public DateTime Date { get; private set; }
    public bool IsAvailable { get; private set; }
    public string Description { get; private set; }
    public Category Category { get; private set; }
    public Address Address { get; private set; }

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

    public Activity UpdateCategory(Category newCategory)
    {
        Category = newCategory;

        return this;
    }

    public Activity UpdateAddress(Address newAddress)
    {
        Address = newAddress;

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