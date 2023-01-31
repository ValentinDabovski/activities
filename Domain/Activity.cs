using Domain.Common;

namespace Domain
{
    public class Activity : Entity<Guid>
    {
        public Activity(string title, string description, DateTime date, Address address, Category category)
        {
            this.Id = Guid.NewGuid();
            this.Title = title;
            this.Description = description;
            this.Date = date;
            this.Category = category;
            this.Address = address;
        }
        private Activity() {}
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public string Description { get; private set; }
        public Category Category { get; private set; }
        public Address Address { get; private set; }

        public void UpdateDate(DateTime newdDte)
        {
            this.Date = newdDte;
        }

        public void UpdateTitle(string newTitle)
        {
            this.Title = newTitle;
        }

        public void UpdateDescription(string newDescription)
        {
            this.Description = newDescription;
        }

        public void UpdateCategory(Category newCategory)
        {
            this.Category = newCategory;
        }

        public void UpdateAddress(Address newAddress)
        {
            this.Address = newAddress;
        }
    }
}