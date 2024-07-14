using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="UserToRole"/> in database.
/// </summary>
internal class UserToRoleConfiguration : IEntityTypeConfiguration<UserToRole>
{
    public void Configure(EntityTypeBuilder<UserToRole> builder)
    {
        builder.ToTable("UserToRole");
    }
}