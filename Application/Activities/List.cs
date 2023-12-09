using Application.Common;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Models;

namespace Application.Activities;

public class List
{
    public class Query : IRequest<Result<List<Activity>>>
    {
    }

    private class Handler : IRequestHandler<Query, Result<List<Activity>>>
    {
        private readonly DataContext _dataContext;

        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<Result<List<Activity>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var activities = await _dataContext.Activities.ToListAsync(cancellationToken);

            return Result<List<Activity>>
                .SuccessWith(_mapper.Map<List<ActivityEntity>, List<Activity>>(activities));
        }
    }
}