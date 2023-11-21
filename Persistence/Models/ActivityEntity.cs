namespace Persistence.Models;

public class ActivityEntity
{
    public Guid Id { get; set; }
    public Guid? UserId { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public bool IsAvailable { get; set; }
    public string Description { get; set; }
    public CategoryEntity Category { get; set; }
    public AddressEntity Address { get; set; }
}