using ArticleManager.Domain.Constants;
using ArticleManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleManager.Infrastructure.Persistence.EntitiesConfigurations;

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
                                          .HasDefaultValue(SqlConstants.GetUtcDate);

        builder.Property(x => x.UpdatedAt).IsRequired(false);

        builder.Property(x => x.UserId).IsRequired();

        builder.HasMany(x => x.ArticleComments)
               .WithOne()
               .HasForeignKey(x => x.ArticleId)
               .IsRequired();
    }
}