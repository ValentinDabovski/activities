using Application.Common;
using Application.Models;
using AutoMapper;
using MediatR;
using Persistence;
using Persistence.Models;

namespace Application.Activities;

public abstract class Details
{
    public class Query : IRequest<Result<ActivityDto>>
    {
        public Guid Id { get; init; }
    }

    private class Handler : IRequestHandler<Query, Result<ActivityDto>>
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activity = await _dataContext.Activities.FindAsync(request.Id, cancellationToken);

            if (activity == null) return Result<ActivityDto>.Failure(new List<string> { "Activity not found." });

            return Result<ActivityDto>
                .SuccessWith(_mapper.Map<ActivityEntity, ActivityDto>(activity));
        }
    }
}