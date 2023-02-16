using MediatR;
using Persistence;
using Application.Models;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<ActivityDto>
        {
            public Guid Id { get; set; }
        }

        private class Handler : IRequestHandler<Query, ActivityDto>
        {
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext) => this.dataContext = dataContext;

            public async Task<ActivityDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.Id, cancellationToken);
                return new ActivityDto
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
                };
            }
        }
    }
}