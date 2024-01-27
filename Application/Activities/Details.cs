using Application.Common;
using Domain.Models;
using MediatR;
using Persistence;

namespace Application.Activities;

public abstract class Details
{
    public class Query : IRequest<Result<Activity>>
    {
        public Guid Id { get; init; }
    }

    private class Handler(DataContext dataContext) : IRequestHandler<Query, Result<Activity>>
    {
        public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await dataContext.Activities.FindAsync(request.Id, cancellationToken);

            if (activity == null) return Result<Activity>.Failure(new List<string> { "Activity not found." });

            return Result<Activity>
                .SuccessWith(activity);
        }
    }
}