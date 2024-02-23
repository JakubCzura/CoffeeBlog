using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

public class ApiErrorConfiguration : IEntityTypeConfiguration<ApiErrorEntity>
{
    public void Configure(EntityTypeBuilder<ApiErrorEntity> builder)
    {
        throw new NotImplementedException();
    }
}