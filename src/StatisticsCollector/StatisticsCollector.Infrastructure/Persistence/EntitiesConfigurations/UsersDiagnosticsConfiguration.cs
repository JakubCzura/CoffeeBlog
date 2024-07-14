using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="UsersDiagnostics"/> in database.
/// </summary>
internal class UsersDiagnosticsConfiguration : IEntityTypeConfiguration<UsersDiagnostics>
{
    public void Configure(EntityTypeBuilder<UsersDiagnostics> builder)
    {
        builder.ToCollection("UsersDiagnostics");

        builder.Property(x => x.NewUserCount).IsRequired();

        builder.Property(x => x.ActiveAccountCount).IsRequired();

        builder.Property(x => x.BannedAccountCount).IsRequired();

        builder.Property(x => x.MostCommonBanReason).IsRequired();

        builder.Property(x => x.UserWhoLoggedInCount).IsRequired();

        builder.Property(x => x.UserWhoFailedToLogInCount).IsRequired();

        builder.Property(x => x.UserWhoChangedUsernameCount).IsRequired();

        builder.Property(x => x.UserWhoChangedEmailCount).IsRequired();

        builder.Property(x => x.UserWhoChangedPasswordCount).IsRequired();

        builder.Property(x => x.DataCollectedAt).IsRequired();
    }
}