using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models
{
    public record Category
    {
        public Category(string name, string description)
        {

            this.ValidateAgainstEmptyString(name);

            this.Name = name;
            this.Description = description;
        }

        private Category() { }

        public string Name { get; private set; }

        public string Description { get; private set; }


        private void ValidateAgainstEmptyString(string stringForValidation)
        {
            Guard.AgainstEmptyString<InvalidCategoryException>(value: stringForValidation);
        }
    }
}