using Domain.Common;

namespace Domain
{
    public class Address : ValueObject
    {
        public Address(string street, string city, string state, string country, string zipcode, string venue)
        {
            this.Id = Guid.NewGuid();
            this.Street = street;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.ZipCode = zipcode;
            this.Venue = venue;
        }

        private Address() { }
        public Guid Id { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string Venue { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Using a yield return statement to return each element one at a time
            yield return this.Id;
            yield return this.Street;
            yield return this.City;
            yield return this.State;
            yield return this.Country;
            yield return this.ZipCode;
        }
    }
}