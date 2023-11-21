using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.Properties;

public class ActivityEntityConfiguration : IEntityTypeConfiguration<ActivityEntity>
{
    public void Configure(EntityTypeBuilder<ActivityEntity> builder)
    {
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
    }
}