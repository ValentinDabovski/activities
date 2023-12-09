using AutoMapper;
using Domain.Models;
using Persistence.Models;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Activity, ActivityEntity>();
        CreateMap<ActivityEntity, Activity>();

        CreateMap<AddressEntity, Address>();
        CreateMap<Address, AddressEntity>();

        CreateMap<CategoryEntity, Category>();
        CreateMap<Category, CategoryEntity>();
    }
}