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

    private class Handler : IRequestHandler<Command, Result>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Id, cancellationToken);

            if (activity == null) return Result.Failure(new List<string> { "Activity not found." });

            _dataContext.Activities.Remove(activity);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}