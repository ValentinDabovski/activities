using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models;

public class Category : ValueObject
{
    internal Category(string name, string description)
    {
        ValidateAgainstEmptyString(name, nameof(name));

        Name = name;
        Description = description;
    }

    private Category()
    {
    }

    public string Name { get; }
    public string Description { get; }

    private void ValidateAgainstEmptyString(string stringForValidation, string paramName)
    {
        Guard.AgainstEmptyString<InvalidCategoryException>(stringForValidation, paramName);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Description;
    }
}