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

    private class Handler : IRequestHandler<Query, Result<List<Activity>>>
    {
        private readonly DataContext _dataContext;

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activities = await _dataContext.Activities.ToListAsync(cancellationToken);

            return Result<List<Activity>>
                .SuccessWith(activities);
        }
    }
}