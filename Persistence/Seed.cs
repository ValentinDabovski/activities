using Domain.Factories.Activities;
using Domain.Models;
using Persistence.Models;

namespace Persistence;

public abstract class Seed
{
    public static async Task SeedData(DataContext context)
    {
        if (context.Activities.Any()) return;

        var activity1 = new ActivityFactory()
            .WithTitle("Drinks")
            .WithDescription("Drink like a dork and live on to see the day light...")
            .WithDate(DateTime.Now.AddMonths(+2))
            .WithCategory(new Category("Drinks", "God like drinking"))
            .WithAddress(new Address("Street lizare", "Swinovo", "", "Drinkonovia", "2334", "Krachmata"))
            .Build();

        var activity2 = new ActivityFactory()
            .WithTitle("Dance all night")
            .WithDescription("Dance all night like a teenage girl in her period.")
            .WithDate(DateTime.Now.AddMonths(+1))
            .WithCategory(new Category("Music", "Have a blast and dance like a cow"))
            .WithAddress(new Address("Nomer chetiri minus edno", "Pashkutevo", "Torim-Town", "Kosim", "2334",
                "Diskotekata na ploshtada"))
            .Build();

        var activities = new List<ActivityEntity>
        {
            new()
            {
                Title = activity1.Title,
                Description = activity1.Description,
                Date = activity1.Date,
                Category = new CategoryEntity { Name = activity1.Category.Name, Description = activity1.Description },
                Address = new AddressEntity
                {
                    City = activity1.Address.City, Country = activity1.Address.Country, State = activity1.Address.State,
                    Street = activity1.Address.Street, Venue = activity1.Address.Venue,
                    ZipCode = activity1.Address.ZipCode
                }
            },
            new()
            {
                Title = activity2.Title,
                Description = activity2.Description,
                Date = activity2.Date,
                Category = new CategoryEntity { Name = activity2.Category.Name, Description = activity2.Description },
                Address = new AddressEntity
                {
                    City = activity2.Address.City, Country = activity2.Address.Country, State = activity2.Address.State,
                    Street = activity2.Address.Street, Venue = activity2.Address.Venue,
                    ZipCode = activity2.Address.ZipCode
                }
            }
        };

        await context.Activities.AddRangeAsync(activities);
        await context.SaveChangesAsync();
    }
}