using MediatR;
using Persistence;
using Application.Models;
using Application.Common;
using AutoMapper;
using Domain.Models;

namespace Application.Activities
{
    public class Details
    {
        public class Query : IRequest<Result<ActivityDto>>
        {
            public Guid Id { get; set; }
        }

        private class Handler : IRequestHandler<Query, Result<ActivityDto>>
        {
            private readonly DataContext dataContext;
            private readonly IMapper mapper;
            
            public Handler(DataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }
            public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.Id, cancellationToken);

                if (activity == null) return Result<ActivityDto>.Failure(new List<string> { "Activity not found." });

                return Result<ActivityDto>
                    .SuccessWith(
                        this.mapper.Map<Activity,ActivityDto>(activity));
            }
        }
    }
}