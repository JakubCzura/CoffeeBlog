using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");

        builder.Property(x => x.Username).IsRequired()
                                         .HasMaxLength(100);

        builder.Property(x => x.Email).IsRequired()
                                      .HasMaxLength(320);

        builder.Property(x => x.Password).IsRequired();

        builder.HasIndex(x => new { x.Username, x.Email }).IsUnique();

        builder.HasMany(x => x.LastPasswords)
               .WithOne()
               .HasForeignKey(x => x.UserId);

        builder.HasMany(x => x.Roles)
               .WithMany(x => x.Users)
               .UsingEntity<UserToRole>();

        builder.HasMany(x => x.RequestDetails)
               .WithOne()
               .HasForeignKey(x => x.UserId)
               .IsRequired(false);
    }
};