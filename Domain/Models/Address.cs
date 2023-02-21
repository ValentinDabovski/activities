using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models
{
    public record Address
    {
        public Address(string street, string city, string state, string country, string zipcode, string venue)
        {
            this.ValidateAgainstEmptyString(city, nameof(city));
            this.ValidateAgainstEmptyString(street, nameof(street));
            this.ValidateAgainstEmptyString(venue, nameof(venue));

            this.Street = street;
            this.City = city;
            this.State = state;
            this.Country = country;
            this.ZipCode = zipcode;
            this.Venue = venue;
        }
        private Address() { }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }
        public string Venue { get; private set; }


        private void ValidateAgainstEmptyString(string stringForValidation, string paramName)
        {
            Guard.AgainstEmptyString<InvalidAddressException>(value: stringForValidation, name: paramName);
        }
    }
}