using Application.Common;
using MediatR;
using Persistence;

namespace Application.Activities;

public abstract class Delete
{
    public class Command : IRequest<Result>
    {
        public Guid Id { get; init; }
    }

    private class Handler(DataContext dataContext) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await dataContext.Activities.FindAsync(request.Id, cancellationToken);

            if (activity == null) return Result.Failure(new List<string> { "Activity not found." });

            dataContext.Activities.Remove(activity);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}