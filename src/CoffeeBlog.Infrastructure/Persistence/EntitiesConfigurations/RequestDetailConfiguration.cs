using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

public class RequestDetailConfiguration : IEntityTypeConfiguration<RequestDetailEntity>
{
    public void Configure(EntityTypeBuilder<RequestDetailEntity> builder)
    {
        throw new NotImplementedException();
    }
}