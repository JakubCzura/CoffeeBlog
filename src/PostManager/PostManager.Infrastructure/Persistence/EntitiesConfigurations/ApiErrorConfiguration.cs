using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostManager.Domain.Constants;
using PostManager.Domain.Entities;

namespace PostManager.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="ApiError"/> in database.
/// </summary>
internal class ApiErrorConfiguration : IEntityTypeConfiguration<ApiError>
{
    public void Configure(EntityTypeBuilder<ApiError> builder)
    {
        builder.ToTable("ApiError");

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Exception).IsRequired();

        builder.Property(x => x.Message).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.ThrownAt).IsRequired()
                                         .HasDefaultValueSql(SqlConstants.GetUtcDate);
    }
}