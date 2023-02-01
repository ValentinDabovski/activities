using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.Activities.Any()) return;

            var activities = new List<Activity>
            {
                new Activity(   title: "Piene",
                                description: "Qko piene",
                                date: DateTime.UtcNow.AddMonths(-2),
                                address: new Address(
                                                    street: "Ulitsa 2",
                                                    city: "Varna",
                                                    state: "",
                                                    country: "Bulgaria",
                                                    zipcode: "9282",
                                                    venue: "Chlagoteka 34"),
                                category: new Category(name: "Drinks", description: ""))

            };

            await context.Activities.AddRangeAsync(activities);
            await context.SaveChangesAsync();
        }
    }
}