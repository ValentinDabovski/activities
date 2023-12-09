using System.Diagnostics;
using Application.Common;
using AutoMapper;
using MediatR;
using Persistence;
using Persistence.Models;

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
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Id, cancellationToken);

            if (activity == null) return Result<Activity>.Failure(new List<string> { "Activity not found." });

            return Result<Activity>
                .SuccessWith(_mapper.Map<ActivityEntity, Activity>(activity));
        }
    }
}