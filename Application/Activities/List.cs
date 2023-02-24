using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Application.Models;
using Application.Common;
using AutoMapper;
using Domain.Models;

namespace Application.Activities
{
    public class List
    {
        public class Query : IRequest<Result<List<ActivityDto>>> { }

        private class Handler : IRequestHandler<Query, Result<List<ActivityDto>>>
        {
            private readonly DataContext dataContext;

            private readonly IMapper mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<ActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activities = await this.dataContext.Activities.ToListAsync(cancellationToken);

                return Result<List<ActivityDto>>
                    .Success(this.mapper.Map<List<Activity>, List<ActivityDto>>(activities));
            }
        }
    }
}