using Application.Mapping;
using AutoMapper;
using Domain.Models;

namespace Application.Models;

public sealed class CategoryDto : IMapFrom<Category>
{
    public string Name { get; set; }

    public string Description { get; set; }

    public void Mapping(Profile mapper)
    {
        mapper.CreateMap<Category, CategoryDto>();
    }
}