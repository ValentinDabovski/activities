using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models;

public record Address
{
    public Address(string street, string city, string state, string country, string zipcode, string venue)
    {
        ValidateAgainstEmptyString(city, nameof(city));
        ValidateAgainstEmptyString(street, nameof(street));
        ValidateAgainstEmptyString(venue, nameof(venue));

        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipcode;
        Venue = venue;
    }

    private Address()
    {
    }

    public string Street { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }
    public string Venue { get; private set; }


    private void ValidateAgainstEmptyString(string stringForValidation, string paramName)
    {
        Guard.AgainstEmptyString<InvalidAddressException>(stringForValidation, paramName);
    }
}