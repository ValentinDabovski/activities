using Application.Common;
using Application.Models;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities;

public abstract class Edit
{
    public class Command : IRequest<Result>
    {
        public ActivityDto Activity { get; init; }

        public Guid ActivityId { get; init; }
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
            var activity = await dataContext.Activities.FindAsync(request.ActivityId, cancellationToken);

            if (activity == null) return Result.Failure(new List<string> { "Activity not found." });

            activity.UpdateTitle(request.Activity.Title);
            activity.UpdateDescription(request.Activity.Description);
            activity.UpdateDate(request.Activity.Date);
            activity.UpdateCategory(
                request.Activity.Category.Name,
                request.Activity.Description);
            activity.UpdateAddress(
                request.Activity.Address.Street,
                request.Activity.Address.City,
                request.Activity.Address.State,
                request.Activity.Address.Country,
                request.Activity.Address.ZipCode,
                request.Activity.Address.Venue
            );

            dataContext.Activities.Update(activity);

            await dataContext.SaveChangesAsync(cancellationToken);

            return Result.Success;
        }
    }
}