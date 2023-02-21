using Application.Common;
using Application.Models;
using Domain.Exceptions;
using Domain.Factories.Activities;
using Domain.Models;
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

        private class Handler : IRequestHandler<Command, Result>
        {
            private readonly DataContext dataContext;
            public Handler(DataContext dataContext) => this.dataContext = dataContext;
            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                try
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

                    return Result.Success;

                }
                catch (InvalidActivityException e)
                {
                    return Result.Failure(new List<string> { e.Message, e.Error });
                }
                catch (InvalidCategoryException e)
                {
                    return Result.Failure(new List<string> { e.Message, e.Error });
                }
                 catch (InvalidAddressException e)
                {
                    return Result.Failure(new List<string> { e.Message, e.Error });
                }
            }
        }
    }
}
