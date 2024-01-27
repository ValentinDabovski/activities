using Application.Common;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities;

public class List
{
    public class Query : IRequest<Result<List<Activity>>>
    {
    }

    private class Handler(DataContext dataContext) : IRequestHandler<Query, Result<List<Activity>>>
    {
        public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activities = await dataContext.Activities.ToListAsync(cancellationToken);

            return Result<List<Activity>>
                .SuccessWith(activities);
        }
    }
}