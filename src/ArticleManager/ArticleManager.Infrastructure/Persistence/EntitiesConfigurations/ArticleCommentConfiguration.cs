using ArticleManager.Domain.Constants;
using ArticleManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ArticleManager.Infrastructure.Persistence.EntitiesConfigurations;

internal class ArticleCommentConfiguration : IEntityTypeConfiguration<ArticleComment>
{
    public void Configure(EntityTypeBuilder<ArticleComment> builder)
    {
        builder.ToTable("ArticleComment");

        builder.Property(x => x.Content).IsRequired()
                                        .HasMaxLength(2000);

        builder.Property(x => x.CreatedAt).IsRequired()
                                          .HasDefaultValueSql(SqlConstants.GetUtcDate);

        builder.Property(x => x.UpdatedAt).IsRequired(false);

        builder.Property(x => x.ArticleId).IsRequired();

        builder.Property(x => x.UserId).IsRequired();
    }
}