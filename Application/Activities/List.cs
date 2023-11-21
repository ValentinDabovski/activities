using Application.Common;
using Application.Models;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;

namespace Application.Activities;

public class List
{
    public class Query : IRequest<Result<List<ActivityDto>>>
    {
    }

    private class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
    {
        private readonly DataContext _dataContext;

        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activities = await _dataContext.Activities.ToListAsync(cancellationToken);

            return Result<List<ActivityDto>>
                .SuccessWith(_mapper.Map<List<ActivityEntity>, List<ActivityDto>>(activities));
        }
    }
}