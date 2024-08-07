﻿using AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthService.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="EventConsumerDetail"/> in database.
/// </summary>
internal class EventConsumerDetailConfiguration : IEntityTypeConfiguration<EventConsumerDetail>
{
    public void Configure(EntityTypeBuilder<EventConsumerDetail> builder)
    {
        builder.ToTable("EventConsumerDetail");

        builder.Property(x => x.EventId).IsRequired();

        builder.Property(x => x.EventPublishedAt).IsRequired();

        builder.Property(x => x.EventName).IsRequired();

        builder.Property(x => x.EventPublisherName).IsRequired();

        builder.Property(x => x.EventPublisherMicroserviceName).IsRequired();

        builder.Property(x => x.EventConsumerName).IsRequired();

        builder.Property(x => x.EventMessage).IsRequired();
    }
}