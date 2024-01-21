using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Properties;

public class ActivityEntityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.HasKey(a => a.Id);

        builder
            .OwnsOne(activity => activity.Address,
                address =>
                {
                    address.WithOwner();
                    address.Property(a => a.City).IsRequired();
                }
            );

        builder
            .OwnsOne(activity => activity.Category,
                category =>
                {
                    category.WithOwner();
                    category.Property(c => c.Name).IsRequired();
                }
            );

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .IsRequired(false);
    }
}