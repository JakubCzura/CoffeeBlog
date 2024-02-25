using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");

        builder.Property(x => x.Username).IsRequired()
                                         .HasMaxLength(100);

        builder.Property(x => x.Email).IsRequired()
                                      .HasMaxLength(320);

        builder.Property(x => x.Password).IsRequired();

        builder.HasIndex(x => new { x.Username, x.Email }).IsUnique();
    }
};