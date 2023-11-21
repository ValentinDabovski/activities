namespace Persistence.Models;

public class UserEntity
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public required string Email { get; set; }
    public IEnumerable<ActivityEntity> Activities { get; set; }
}