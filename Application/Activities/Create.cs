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
                await this.dataContext.Activities.AddAsync(request.Activity, cancellationToken);

                await this.dataContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
