using CoffeeBlog.Domain.Constants;
using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

internal class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");

        builder.Property(x => x.Name).IsRequired()
                                     .HasMaxLength(100);

        builder.Property(x => x.Description).IsRequired()
                                            .HasMaxLength(200);

        builder.Property(x => x.CreatedAt).IsRequired()
                                          .HasDefaultValueSql(SqlConstants.GetUtcDate);
    }
}