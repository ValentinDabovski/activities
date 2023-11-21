using Application.Mapping;
using AutoMapper;
using Domain.Models;

namespace Application.Models;

public sealed class ActivityDto : IMapFrom<Activity>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public DateTime Date { get; set; }
    public bool IsAvailable { get; set; }
    public string Description { get; set; }
    public CategoryDto Category { get; set; }
    public AddressDto Address { get; set; }

    public void Mapping(Profile mapper)
    {
        mapper.CreateMap<Activity, ActivityDto>();
    }
}