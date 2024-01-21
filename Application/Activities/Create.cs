using Application.Common;
using Application.Models;
using Domain.Factories.Activities;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities;

public abstract class Create
{
    public class Command : IRequest<Result>
    {
        public ActivityDto Activity { get; init; }
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
                .WithCategory(request.Activity.Category.Name, request.Activity.Category.Description)
                .WithAddress(
                    request.Activity.Address.Street,
                    request.Activity.Address.City,
                    request.Activity.Address.State,
                    request.Activity.Address.Country,
                    request.Activity.Address.ZipCode,
                    request.Activity.Address.Venue)
                .Build();

            await _dataContext.Activities.AddAsync(activity, cancellationToken);

            await _dataContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}