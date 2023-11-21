using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Models;

namespace Persistence.Properties;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.Property(p => p.Id)
            .IsRequired();

        builder
            .HasMany(u => u.Activities)
            .WithOne()
            .HasForeignKey(a => a.UserId);
    }
}