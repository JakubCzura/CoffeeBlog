using AuthService.Domain.Constants;
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

        builder.Property(x => x.AccountBanReason).IsRequired()
                                                 .HasConversion<string>();

        builder.Property(x => x.BanNote).IsRequired()
                                        .HasMaxLength(50);

        builder.Property(x => x.BannedAt).IsRequired()
                                         .HasDefaultValueSql(SqlConstants.GetUtcDate);

        builder.Property(x => x.BanEndsAt).IsRequired()
                                          .HasDefaultValueSql(SqlConstants.GetUtcDate);

        builder.Property(x => x.UserId).IsRequired();
    }
}