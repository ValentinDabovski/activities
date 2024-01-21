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

    private class Handler : IRequestHandler<Query, Result<Activity>>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Id, cancellationToken);

            if (activity == null) return Result<Activity>.Failure(new List<string> { "Activity not found." });

            return Result<Activity>
                .SuccessWith(activity);
        }
    }
}