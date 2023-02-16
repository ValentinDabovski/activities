using Domain.Models;

namespace Domain.Factories.Activities
{
    public class ActivityFactory : IActivityFactory
    {
        private string title = default;
        private string description = default;
        private bool isAvailable = false;
        private DateTime date = DateTime.Now;
        private Address address = default;
        private Category category = default;


        public Activity Build()
        {
            return new Activity(
                title: title,
                description: description,
                isAvailable: isAvailable,
                date: date,
                address: address,
                category: category
            );
        }

        public IActivityFactory WithAddress(Address address)
        {
            this.address = address;
            return this;
        }

        public IActivityFactory WithCategory(Category category)
        {
            this.category = category;
            return this;
        }
        
        public IActivityFactory WithDate(DateTime date)
        {
            this.date = date;
            return this;
        }

        public IActivityFactory WithDescription(string description)
        {
            this.description = description;
            return this;
        }

        public IActivityFactory WithTitle(string title)
        {
            this.title = title;
            return this;
        }
    }
}