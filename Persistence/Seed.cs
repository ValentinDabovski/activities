using Domain.Factories.Activities;
using Domain.Models;

namespace Persistence
{
    public abstract class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Activities.Any()) return;

            var activity1 = new ActivityFactory()
                   .WithTitle("Drinks")
                   .WithDescription("Drink like a dork and live on to see the day light...")
                   .WithDate(DateTime.Now.AddMonths(+2))
                   .WithCategory(new Category(name: "Drinks", description: "God like drinking"))
                   .WithAddress(new Address(street: "Street lizare", city: "Swinovo", state: "", country: "Drinkonovia", zipcode: "2334", venue: "Krachmata"))
                   .Build();

               var activity2 = new ActivityFactory()
                   .WithTitle("Dance all night")
                   .WithDescription("Dance all night like a teenage girl in her period.")
                   .WithDate(DateTime.Now.AddMonths(+1))
                   .WithCategory(new Category(name: "Music", description: "Have a blast and dance like a cow"))
                   .WithAddress(new Address(street: "Nomer chetiri minus edno", city: "Pashkutevo", state: "Torim-Town", country: "Kosim", zipcode: "2334", venue: "Diskotekata na ploshtada"))
                   .Build();

            var activities = new List<Activity> { activity1, activity2 };

            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}