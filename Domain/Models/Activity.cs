using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models
{
    public class Activity : IAggregateRoot
    {
        internal Activity(string title, string description, bool isAvailable, DateTime date, Address address, Category category)
        {
            this.ValidateAgainstEmptyString(title);
            this.ValidateAgainstEmptyString(description);
            this.ValidateAgainstEmptyString(address.City);
            this.ValidateAgainstEmptyString(category.Name);

            this.Id = Guid.NewGuid();
            this.Title = title;
            this.Description = description;
            this.IsAvailable = isAvailable;
            this.Date = date;
            this.Category = category;
            this.Address = address;
        }
        private Activity() { }
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public DateTime Date { get; private set; }
        public bool IsAvailable { get; private set; }
        public string Description { get; private set; }
        public Category Category { get; private set; }
        public Address Address { get; private set; }

        public Activity UpdateDate(DateTime newdDte)
        {
            this.Date = newdDte;

            return this;
        }

        public Activity UpdateTitle(string newTitle)
        {
            this.Title = newTitle;

            return this;
        }

        public Activity UpdateDescription(string newDescription)
        {
            this.Description = newDescription;

            return this;
        }

        public Activity UpdateCategory(Category newCategory)
        {
            this.Category = newCategory;

            return this;
        }

        public Activity UpdateAddress(Address newAddress)
        {
            this.Address = newAddress;

            return this;
        }

        public Activity ChangeAvailability()
        {
            this.IsAvailable = !this.IsAvailable;

            return this;
        }

        private void ValidateAgainstEmptyString(string stringForValidation)
        {
            Guard.AgainstEmptyString<InvalidActivityException>(value: stringForValidation);
        }
    }
}