using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

public class ApiErrorConfiguration : IEntityTypeConfiguration<ApiErrorEntity>
{
    public void Configure(EntityTypeBuilder<ApiErrorEntity> builder)
    {
        builder.ToTable("ApiErrors");

        builder.Property(x => x.Exception).IsRequired();

        builder.Property(x => x.Message).IsRequired();

        builder.Property(x => x.Description).IsRequired();

        builder.Property(x => x.Exception).IsRequired();
    }
}