using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Activity> Activities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Activity>()
            .OwnsOne(activity => activity.Address,
                address =>
                {
                    address.WithOwner();
                    address.Property(a => a.City).IsRequired();
                }
            );

            base.OnModelCreating(builder);
        }
    }
}