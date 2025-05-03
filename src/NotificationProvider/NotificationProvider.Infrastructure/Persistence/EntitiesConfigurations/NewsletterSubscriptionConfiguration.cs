using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="NewsletterSubscription"/> in database.
/// </summary>
internal class NewsletterSubscriptionConfiguration : IEntityTypeConfiguration<NewsletterSubscription>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscription> builder)
    {
        builder.ToCollection("NewsletterSubscription");

        builder.Property(x => x.Email).IsRequired();

        builder.Property(x => x.AgreeToTerms);

        builder.Property(x => x.IsConfirmed);
    }
}