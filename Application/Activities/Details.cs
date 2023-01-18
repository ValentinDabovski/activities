using MediatR;
using Domain;
using Persistence;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Activity>
        {
            public Guid Id { get; set; }
        }

        private class Handler : IRequestHandler<Query, Activity>
        {
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext) => this.dataContext = dataContext;

            public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
            {
                return await this.dataContext.Activities.FindAsync(request.Id, cancellationToken);
            }
        }
    }
}