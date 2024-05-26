using ArticleManager.Domain.Constants;
using ArticleManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleManager.Infrastructure.Persistence.EntitiesConfigurations;

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