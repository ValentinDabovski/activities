namespace Application.Models;

public class ActivityDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string Description { get; set; }
    public required DateTime Date { get; set; }
    public required CategoryDto Category { get; set; }
    public required AddressDto Address { get; set; }

    public class CategoryDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
    }

    public class AddressDto
    {
        public required string Street { get; set; }
        public required string City { get; set; }
        public required string State { get; set; }
        public required string Country { get; set; }
        public required string ZipCode { get; set; }
        public required string Venue { get; set; }
    }
}