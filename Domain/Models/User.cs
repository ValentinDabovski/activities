using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models;

public class User : Entity
{
    internal User(string firstName, string lastName, string email, Guid? id) : base(id)
    {
        ValidateAgainstEmptyString(firstName, nameof(firstName));
        ValidateAgainstEmptyString(lastName, nameof(lastName));
        ValidateAgainstEmptyString(email, nameof(email));
        Guard.ForValidEmail<InvalidUserException>(email, nameof(email));

        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }


    private User()
    {
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    public IEnumerable<Activity> Activities { get; private set; } = new List<Activity>();

    private void ValidateAgainstEmptyString(string stringForValidation, string paramName)
    {
        Guard.AgainstEmptyString<InvalidUserException>(stringForValidation, paramName);
    }
}