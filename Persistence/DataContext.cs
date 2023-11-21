using Microsoft.EntityFrameworkCore;
using Persistence.Models;
using Persistence.Properties;

namespace Persistence;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<ActivityEntity> Activities { get; set; }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ActivityEntityConfiguration).Assembly);
    }
}