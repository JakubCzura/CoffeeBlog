using CoffeeBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeBlog.Infrastructure.Persistence.EntitiesConfigurations;

public class RequestDetailConfiguration : IEntityTypeConfiguration<RequestDetail>
{
    public void Configure(EntityTypeBuilder<RequestDetail> builder)
    {
        builder.ToTable("RequestDetails");

        builder.Property(x => x.ControllerName).IsRequired()
                                               .HasMaxLength(100);

        builder.Property(x => x.Path).IsRequired();

        builder.Property(x => x.HttpMethod).IsRequired()
                                           .HasMaxLength(20);

        builder.Property(x => x.StatusCode).IsRequired();

        builder.Property(x => x.RequestBody).IsRequired(false);

        builder.Property(x => x.RequestContentType).IsRequired(false)
                                                   .HasMaxLength(50);

        builder.Property(x => x.ResponseBody).IsRequired(false);

        builder.Property(x => x.ResponseContentType).IsRequired(false);

        builder.Property(x => x.RequestTimeInMiliseconds).IsRequired();

        builder.Property(x => x.SentAt).IsRequired();

        builder.Property(x => x.UserId).IsRequired(false);
    }
}