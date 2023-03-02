using Application.Common;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest<Result>
        {
            public Guid Id { get; set; }
        }

        private class Handler : IRequestHandler<Command, Result>
        {
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext) => this.dataContext = dataContext;

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.Id, cancellationToken);

                if (activity == null) return Result.Failure(new List<string> { "Activity not found." });

                this.dataContext.Activities.Remove(activity);

                await this.dataContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}