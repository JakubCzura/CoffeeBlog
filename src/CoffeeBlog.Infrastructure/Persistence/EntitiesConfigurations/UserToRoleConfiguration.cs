using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

internal class UserToRoleConfiguration : IEntityTypeConfiguration<UserToRole>
{
    public void Configure(EntityTypeBuilder<UserToRole> builder)
    {
        builder.ToTable("UserToRole");
    }
}