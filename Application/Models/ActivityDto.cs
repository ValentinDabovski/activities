namespace Application.Models;

public class ActivityDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public CategoryDto Category { get; set; }
    public AddressDto Address { get; set; }

    public class CategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Venue { get; set; }
    }
}