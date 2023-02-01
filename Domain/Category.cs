using Domain.Common;

namespace Domain
{
    public record Category
    {
        public Category(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        private Category() { }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}