using MediatR;
using Persistence;
using Application.Models;
using Application.Common;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Result<ActivityDto>>
        {
            public Guid Id { get; set; }
        }

        private class Handler : IRequestHandler<Query, Result<ActivityDto>>
        {
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext) => this.dataContext = dataContext;

            public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.Id, cancellationToken);

                if (activity == null) return Result<ActivityDto>.Failure(new List<string> { "Activity not found." });

                var activityDto = new ActivityDto
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

                return Result<ActivityDto>.SuccessWith(activityDto);
            }
        }
    }
}