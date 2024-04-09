using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.EntitiesConfigurations;

internal class UserToRoleConfiguration : IEntityTypeConfiguration<UserToRole>
{
    public void Configure(EntityTypeBuilder<UserToRole> builder)
    {
        builder.ToTable("UserToRole");
    }
}