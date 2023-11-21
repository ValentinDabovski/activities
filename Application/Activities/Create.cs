using Application.Common;
using Application.Models;
using Domain.Factories.Activities;
using Domain.Models;
using FluentValidation;
using MediatR;
using Persistence;
using Persistence.Models;

namespace Application.Activities;

public abstract class Create
{
    public class Command : IRequest<Result>
    {
        public ActivityDto ActivityDto { get; init; }
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

        public Handler(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = new ActivityFactory()
                .WithTitle(request.ActivityDto.Title)
                .WithDescription(request.ActivityDto.Description)
                .WithDate(request.ActivityDto.Date)
                .WithCategory(
                    new Category(
                        request.ActivityDto.Category.Name,
                        request.ActivityDto.Category.Description))
                .WithAddress(
                    new Address(
                        request.ActivityDto.Address.Street,
                        request.ActivityDto.Address.City,
                        request.ActivityDto.Address.State,
                        request.ActivityDto.Address.Country,
                        request.ActivityDto.Address.ZipCode,
                        request.ActivityDto.Address.Venue))
                .Build();

            await _dataContext.Activities.AddAsync(new ActivityEntity
            {
                Title = activity.Title,
                Address = new AddressEntity
                {
                    City = activity.Address.City, Country = activity.Address.Country, State = activity.Address.State,
                    Street = activity.Address.Street, Venue = activity.Address.Venue,
                    ZipCode = activity.Address.ZipCode
                },
                Category = new CategoryEntity
                    { Name = activity.Category.Name, Description = activity.Category.Description },
                Date = activity.Date,
                Description = activity.Description,
                IsAvailable = activity.IsAvailable,
                Id = activity.Id
            }, cancellationToken);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}