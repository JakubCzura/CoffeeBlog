using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.EntityFrameworkCore.Extensions;
using NotificationProvider.Domain.Entities;

namespace NotificationProvider.Infrastructure.Persistence.EntitiesConfigurations;

internal class EmailMessageDetailConfiguration : IEntityTypeConfiguration<EmailMessageDetail>
{
    public void Configure(EntityTypeBuilder<EmailMessageDetail> builder)
    {
        builder.ToCollection("EmailMessageDetail");

        builder.Property(x => x.SenderName).IsRequired();

        builder.Property(x => x.SenderEmail).IsRequired();

        builder.Property(x => x.RecipientName).IsRequired();

        builder.Property(x => x.RecipientEmail).IsRequired();

        builder.Property(x => x.Subject).IsRequired(false);

        builder.Property(x => x.Body).IsRequired(false);

        builder.Property(x => x.SentAt).IsRequired();
    }
}