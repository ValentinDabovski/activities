using Application.Common;
using Application.Models;
using AutoMapper;
using Domain.Factories.Activities;
using Domain.Models;
using FluentValidation;
using MediatR;
using Persistence;
using Persistence.Models;

namespace Application.Activities;

public abstract class Edit
{
    public class Command : IRequest<Result>
    {
        public ActivityDto ActivityDto { get; init; }

        public Guid AcivityId { get; init; }
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
        private readonly DataContext _dataContext;

        private readonly IMapper _mapper;

        public Handler(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }


        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var activityEntity = await _dataContext.Activities.FindAsync(request.AcivityId, cancellationToken);

            if (activityEntity == null) return Result.Failure(new List<string> { "Activity not found." });

            var activity = new ActivityFactory()
                .WithTitle(activityEntity.Title)
                .WithDescription(activityEntity.Description)
                .WithDate(activityEntity.Date)
                .WithCategory(new Category(activityEntity.Category.Name, activityEntity.Category.Description))
                .WithAddress(new Address(activityEntity.Address.Street, activityEntity.Address.City,
                    activityEntity.Address.State, activityEntity.Address.Country,
                    activityEntity.Address.ZipCode,
                    activityEntity.Address.Venue))
                .Build();

            activity.UpdateTitle(request.ActivityDto.Title);
            activity.UpdateDescription(request.ActivityDto.Description);
            activity.UpdateDate(request.ActivityDto.Date);
            activity.UpdateCategory(
                new Category(
                    request.ActivityDto.Category.Name,
                    request.ActivityDto.Description));
            activity.UpdateAddress(
                new Address(
                    request.ActivityDto.Address.Street,
                    request.ActivityDto.Address.City,
                    request.ActivityDto.Address.State,
                    request.ActivityDto.Address.Country,
                    request.ActivityDto.Address.ZipCode,
                    request.ActivityDto.Address.Venue
                ));

            activityEntity.Title = activity.Title;
            activityEntity.Description = activity.Description;
            activityEntity.Date = activity.Date;
            activityEntity.Category = new CategoryEntity
                { Name = activity.Category.Name, Description = activity.Description };
            activityEntity.Address = new AddressEntity
            {
                City = activity.Address.City, Country = activity.Address.Country, State = activity.Address.State,
                Street = activity.Address.Street, Venue = activity.Address.Venue,
                ZipCode = activity.Address.ZipCode
            };
            activityEntity.IsAvailable = activityEntity.IsAvailable;


            _dataContext.Activities.Update(activityEntity);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}