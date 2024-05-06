using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.EntitiesConfigurations;

internal class ApiErrorConfiguration : IEntityTypeConfiguration<ApiError>
{
    public void Configure(EntityTypeBuilder<ApiError> builder)
    {
        builder.ToCollection("ApiError");

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.Exception).IsRequired();

        builder.Property(x => x.Message).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.ThrownAt).IsRequired();
    }
}