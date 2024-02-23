using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

public class UserDetailConfiguration : IEntityTypeConfiguration<UserDetailEntity>
{
    public void Configure(EntityTypeBuilder<UserDetailEntity> builder)
    {
        throw new NotImplementedException();
    }
}