using Domain.Common;

namespace Domain
{
    public class Category : ValueObject
    {
        public Category(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        private Category() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Id;
            yield return this.Name;
        }
    }
}