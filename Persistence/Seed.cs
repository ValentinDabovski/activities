using Domain.Factories.Activities;

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
            .WithCategory("Drinks", "God like drinking")
            .WithAddress("Street lizare", "Swinovo", "", "Drinkonovia", "2334", "Krachmata")
            .Build();

        var activity2 = new ActivityFactory()
            .WithTitle("Dance all night")
            .WithDescription("Dance all night like a teenage girl in her period.")
            .WithDate(DateTime.Now.AddMonths(+1))
            .WithCategory("Music", "Have a blast and dance like a cow")
            .WithAddress("Nomer chetiri minus edno", "Pashkutevo", "Torim-Town", "Kosim", "2334",
                "Diskotekata na ploshtada")
            .Build();

        await context.Activities.AddRangeAsync(activity1, activity2);
        await context.SaveChangesAsync();
    }
}