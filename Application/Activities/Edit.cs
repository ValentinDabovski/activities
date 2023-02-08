using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }

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

                activity.UpdateTitle(request.Activity.Title);
                activity.UpdateDescription(request.Activity.Description);
                activity.UpdateDate(request.Activity.Date);
                activity.UpdateCategory(request.Activity.Category);
                activity.UpdateAddress(request.Activity.Address);

                await dataContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}