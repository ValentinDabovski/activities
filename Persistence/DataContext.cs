using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Properties;

namespace Persistence;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Activity> Activities { get; init; }

    public DbSet<User> Users { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ActivityEntityConfiguration).Assembly);
    }
}