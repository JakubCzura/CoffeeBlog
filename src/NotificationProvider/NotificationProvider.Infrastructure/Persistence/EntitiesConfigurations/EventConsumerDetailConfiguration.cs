using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.EntitiesConfigurations;

internal class EventConsumerDetailConfiguration : IEntityTypeConfiguration<EventConsumerDetail>
{
    public void Configure(EntityTypeBuilder<EventConsumerDetail> builder)
    {
        builder.ToCollection("EventConsumerDetail");

        builder.Property(x => x.EventId).IsRequired();

        builder.Property(x => x.EventPublishedAt).IsRequired();

        builder.Property(x => x.EventName).IsRequired();

        builder.Property(x => x.EventPublisherName).IsRequired();

        builder.Property(x => x.EventConsumerName).IsRequired();

        builder.Property(x => x.EventMessage).IsRequired();
    }
}