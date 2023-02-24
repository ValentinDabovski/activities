using Application.Common;
using Application.Models;
using Domain.Factories.Activities;
using Domain.Models;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest<Result>
        {
            public ActivityDto ActivityDto { get; set; }
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
            public Handler(DataContext dataContext) => this.dataContext = dataContext;
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = new ActivityFactory()
                    .WithTitle(request.ActivityDto.Title)
                    .WithDescription(request.ActivityDto.Description)
                    .WithDate(request.ActivityDto.Date)
                    .WithCategory(
                            new Category(
                                name: request.ActivityDto.Category.Name,
                                description: request.ActivityDto.Category.Description))
                    .WithAddress(
                            new Address(
                                street: request.ActivityDto.Address.Street,
                                city: request.ActivityDto.Address.City,
                                state: request.ActivityDto.Address.State,
                                country: request.ActivityDto.Address.Country,
                                zipcode: request.ActivityDto.Address.ZipCode,
                                venue: request.ActivityDto.Address.Venue))
                    .Build();

                await this.dataContext.Activities.AddAsync(activity, cancellationToken);

                await this.dataContext.SaveChangesAsync(cancellationToken);

                return Result.Success();
            }
        }
    }
}
