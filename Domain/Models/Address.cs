using Domain.Common;
using Domain.Exceptions;

namespace Domain.Models;

public class Address : ValueObject
{
    internal Address(string street, string city, string state, string country, string zipcode, string venue)
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

    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; private set; }
    public string ZipCode { get; }
    public string Venue { get; }


    private void ValidateAgainstEmptyString(string stringForValidation, string paramName)
    {
        Guard.AgainstEmptyString<InvalidAddressException>(stringForValidation, paramName);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return State;
        yield return ZipCode;
        yield return Venue;
    }
}