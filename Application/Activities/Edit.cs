using Application.Models;
using Domain.Models;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public ActivityDto ActivityDto { get; set; }

            public Guid AcivityId { get; set; }
        }

        private class Handler : IRequestHandler<Command>
        {
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext)
            {
                this.dataContext = dataContext;
            }


            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.AcivityId, cancellationToken);

                activity.UpdateTitle(request.ActivityDto.Title);
                activity.UpdateDescription(request.ActivityDto.Description);
                activity.UpdateDate(request.ActivityDto.Date);
                activity.UpdateCategory(
                        new Category(
                            name: request.ActivityDto.Category.Name,
                            description: request.ActivityDto.Description));
                activity.UpdateAddress(
                        new Address(
                            street: request.ActivityDto.Address.Street,
                            city: request.ActivityDto.Address.City,
                            state: request.ActivityDto.Address.State,
                            country: request.ActivityDto.Address.Country,
                            zipcode: request.ActivityDto.Address.ZipCode,
                            venue: request.ActivityDto.Address.Venue
                    ));

                dataContext.Activities.Update(activity);

                await dataContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}