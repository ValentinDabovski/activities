using Application.Mapping;
using AutoMapper;
using Domain.Models;

namespace Application.Models
{
    public class AddressDto : IMapFrom<Address>
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Venue { get; set; }

        public virtual void Mapping(Profile mapper)
        {
            mapper.CreateMap<Address, AddressDto>();
        }
    }
}