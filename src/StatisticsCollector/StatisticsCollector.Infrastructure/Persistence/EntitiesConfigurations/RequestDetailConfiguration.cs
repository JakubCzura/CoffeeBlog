﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using StatisticsCollector.Domain.Entities;

namespace StatisticsCollector.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="RequestDetail"/> in database.
/// </summary>
internal class RequestDetailConfiguration : IEntityTypeConfiguration<RequestDetail>
{
    public void Configure(EntityTypeBuilder<RequestDetail> builder)
    {
        builder.ToCollection("AuthServiceRequestDetail");

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