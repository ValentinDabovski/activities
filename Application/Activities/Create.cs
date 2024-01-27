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

    private class Handler(DataContext dataContext) : IRequestHandler<Command, Result>
    {
        public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = new ActivityFactory()
                .WithTitle(request.Activity.Title)
                .WithDescription(request.Activity.Description)
                .WithDate(request.Activity.Date)
                .WithCategory(
                    request.Activity.Category.Name,
                    request.Activity.Category.Description)
                .WithAddress(
                    request.Activity.Address.Street,
                    request.Activity.Address.City,
                    request.Activity.Address.State,
                    request.Activity.Address.Country,
                    request.Activity.Address.ZipCode,
                    request.Activity.Address.Venue)
                .Build();

            await dataContext.Activities.AddAsync(activity, cancellationToken);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}