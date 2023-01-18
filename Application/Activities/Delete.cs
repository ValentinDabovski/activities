using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
        }

        private class Handler : IRequestHandler<Command>
        {
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext) => this.dataContext = dataContext;

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.Id, cancellationToken);

                if (activity != null)
                {
                    this.dataContext.Activities.Remove(activity);

                    await this.dataContext.SaveChangesAsync(cancellationToken);
                }

                return Unit.Value;
            }
        }
    }
}