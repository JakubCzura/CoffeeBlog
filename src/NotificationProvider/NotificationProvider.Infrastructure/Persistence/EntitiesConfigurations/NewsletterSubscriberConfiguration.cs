using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="NewsletterSubscriber"/> in database.
/// </summary>
internal class NewsletterSubscriberConfiguration : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> builder)
    {
        builder.ToCollection("NewsletterSubscriber");

        builder.Property(x => x.Email).IsRequired();

        builder.Property(x => x.AgreeToTerms).IsRequired();

        builder.Property(x => x.IsConfirmed).IsRequired(false);
    }
}