using Domain.Models;

namespace Domain.Factories.Users;

public class UserFactory : IUserFactory
{
    private string _email;
    private string _firstName;
    private Guid? _id;
    private string _lastName;

    public User Build()
    {
        return new User(_firstName, _lastName, _email, _id);
    }

    public IUserFactory WithFirstName(string firstName)
    {
        _firstName = firstName;
        return this;
    }

    public IUserFactory WithLastName(string lastName)
    {
        _lastName = lastName;
        return this;
    }

    public IUserFactory WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public IUserFactory WithId(Guid id)
    {
        _id = id;
        return this;
    }
}