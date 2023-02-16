namespace Application.Models
{
    public class ActivityDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool IsAvailable { get; set; }
        public string Description { get; set; }
        public CategoryDto Category { get; set; }
        public AddressDto Address { get; set; }
    }
}