using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Models;
using Application.Common;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<List<ActivityDto>>> { }

        private class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
        {
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext) => this.dataContext = dataContext;

            public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await this.dataContext.Activities.ToListAsync(cancellationToken);

                var activityDtos = activities.Select(activity => new ActivityDto
                {
                    Id = activity.Id,
                    Title = activity.Title,
                    Description = activity.Description,
                    Address = new AddressDto
                    {
                        Street = activity.Address.Street,
                        City = activity.Address.City,
                        State = activity.Address.State,
                        Country = activity.Address.Country,
                        ZipCode = activity.Address.ZipCode,
                        Venue = activity.Address.Venue
                    },
                    Category = new CategoryDto
                    {
                        Name = activity.Category.Name,
                        Description = activity.Category.Description
                    }
                }).ToList();

                return Result<List<ActivityDto>>.SuccessWith(activityDtos);
            }
        }
    }
}