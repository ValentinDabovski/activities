using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }
        }

        private class Handler : IRequestHandler<Command>
        {
            private readonly DataContext dataContext;
            public Handler(DataContext dataContext) => this.dataContext = dataContext;
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = new Activity(
                    title: request.Activity.Title,
                    description: request.Activity.Description,
                    date: request.Activity.Date,
                    address: request.Activity.Address,
                    category: request.Activity.Category);

                await this.dataContext.Activities.AddAsync(activity, cancellationToken);

                await this.dataContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
