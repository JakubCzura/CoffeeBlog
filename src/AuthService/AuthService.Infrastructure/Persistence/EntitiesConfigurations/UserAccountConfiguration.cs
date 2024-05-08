using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.EntitiesConfigurations;

internal class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
{
    public void Configure(EntityTypeBuilder<UserAccount> builder)
    {
        builder.ToTable("UserAccount");

        builder.Property(x => x.IsBanned).IsRequired();

        builder.Property(x => x.BanReason).IsRequired(false)
                                          .HasConversion<string>();

        builder.Property(x => x.BanNote).IsRequired(false)
                                        .HasMaxLength(50);

        builder.Property(x => x.BannedAt).IsRequired(false);

        builder.Property(x => x.BanEndsAt).IsRequired(false);

        builder.Property(x => x.UserId).IsRequired();
    }
}