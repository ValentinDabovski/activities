using AutoMapper;
using Domain.Factories.Activities;
using Domain.Models;
using Persistence.Models;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Activity, ActivityEntity>();

        CreateMap<ActivityEntity, Activity>()
            .ConstructUsing(entity =>
                new ActivityFactory()
                    .WithTitle(entity.Title)
                    .WithDescription(entity.Description)
                    .WithDate(entity.Date)
                    .WithCategory(
                        new Category(
                            entity.Category.Name,
                            entity.Category.Description))
                    .WithAddress(
                        new Address(
                            entity.Address.Street,
                            entity.Address.City,
                            entity.Address.State,
                            entity.Address.Country,
                            entity.Address.ZipCode,
                            entity.Address.Venue))
                    .Build());

        CreateMap<AddressEntity, Address>();
        CreateMap<Address, AddressEntity>();

        CreateMap<CategoryEntity, Category>();
        CreateMap<Category, CategoryEntity>();
    }
}