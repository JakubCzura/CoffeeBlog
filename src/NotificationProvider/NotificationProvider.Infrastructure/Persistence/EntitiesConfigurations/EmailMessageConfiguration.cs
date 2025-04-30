using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="EmailMessage"/> in database.
/// </summary>
internal class EmailMessageConfiguration : IEntityTypeConfiguration<EmailMessage>
{
    public void Configure(EntityTypeBuilder<EmailMessage> builder)
    {
        builder.ToCollection("EmailMessage");

        builder.Property(x => x.SenderName).IsRequired();

        builder.Property(x => x.SenderEmail).IsRequired();

        builder.Property(x => x.RecipientEmail).IsRequired();

        builder.Property(x => x.Subject).IsRequired(false);

        builder.Property(x => x.Body).IsRequired(false);

        builder.Property(x => x.SentAt).IsRequired();
    }
}