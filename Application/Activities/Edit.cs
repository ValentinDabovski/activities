using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }

            public Guid AcivityId { get; set; }
        }

        private class Handler : IRequestHandler<Command>
        {
            private readonly DataContext dataContext;
            private readonly IMapper mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                this.mapper = mapper;
                this.dataContext = dataContext;
            }


            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.AcivityId, cancellationToken);

                this.mapper.Map(request.Activity, activity);

                await dataContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}