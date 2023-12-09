using Application.Common;
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
        public Activity Activity { get; init; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Activity).SetValidator(new ActivityValidator());
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
                .WithTitle(request.Activity.Title)
                .WithDescription(request.Activity.Description)
                .WithDate(request.Activity.Date)
                .WithCategory(
                    new Category(
                        request.Activity.Category.Name,
                        request.Activity.Category.Description))
                .WithAddress(
                    new Address(
                        request.Activity.Address.Street,
                        request.Activity.Address.City,
                        request.Activity.Address.State,
                        request.Activity.Address.Country,
                        request.Activity.Address.ZipCode,
                        request.Activity.Address.Venue))
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