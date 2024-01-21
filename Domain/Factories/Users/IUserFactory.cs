using Domain.Models;

namespace Domain.Factories.Users;

public interface IUserFactory : IFactory<User>
{
    IUserFactory WithFirstName(string firstName);
    IUserFactory WithLastName(string lastName);
    IUserFactory WithEmail(string email);

    IUserFactory WithId(Guid id);
}