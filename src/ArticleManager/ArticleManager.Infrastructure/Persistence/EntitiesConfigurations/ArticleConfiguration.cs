using ArticleManager.Domain.Constants;
using ArticleManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleManager.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="Article"/> in database.
/// </summary>
internal class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("Article");

        builder.Property(x => x.Title).IsRequired()
                                      .HasMaxLength(100);

        builder.Property(x => x.Subtitle).IsRequired(false)
                                         .HasMaxLength(100);

        builder.Property(x => x.Content).IsRequired()
                                        .HasMaxLength(10000);

        builder.Property(x => x.CreatedAt).IsRequired()
                                          .HasDefaultValueSql(SqlConstants.GetUtcDate);

        builder.Property(x => x.UpdatedAt).IsRequired(false);

        builder.Property(x => x.UserId).IsRequired();

        builder.HasMany(x => x.ArticleComments)
               .WithOne()
               .HasForeignKey(x => x.ArticleId)
               .IsRequired();
    }
}