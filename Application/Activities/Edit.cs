using Application.Common;
using Application.Models;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest<Result>
        {
            public ActivityDto ActivityDto { get; set; }

            public Guid AcivityId { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.ActivityDto).SetValidator(new ActivityValidator());
            }
        }

        private class Handler : IRequestHandler<Command, Result>
        {
            private readonly DataContext dataContext;

            private readonly IMapper mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }


            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await this.dataContext.Activities.FindAsync(request.AcivityId, cancellationToken);

                if (activity == null) return Result.Failure("Activity not found.");

                activity.UpdateTitle(request.ActivityDto.Title);
                activity.UpdateDescription(request.ActivityDto.Description);
                activity.UpdateDate(request.ActivityDto.Date);
                activity.UpdateCategory(
                        new Category(
                            name: request.ActivityDto.Category.Name,
                            description: request.ActivityDto.Description));
                activity.UpdateAddress(
                        new Address(
                            street: request.ActivityDto.Address.Street,
                            city: request.ActivityDto.Address.City,
                            state: request.ActivityDto.Address.State,
                            country: request.ActivityDto.Address.Country,
                            zipcode: request.ActivityDto.Address.ZipCode,
                            venue: request.ActivityDto.Address.Venue
                    ));

                dataContext.Activities.Update(activity);

                await dataContext.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}