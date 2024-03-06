using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

internal class ApiErrorConfiguration : IEntityTypeConfiguration<ApiError>
{
    public void Configure(EntityTypeBuilder<ApiError> builder)
    {
        builder.ToTable("ApiErrors");

        builder.Property(x => x.Exception).IsRequired();

        builder.Property(x => x.Message).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.Exception).IsRequired();
    }
}