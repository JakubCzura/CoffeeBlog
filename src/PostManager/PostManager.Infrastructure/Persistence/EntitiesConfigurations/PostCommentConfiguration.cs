using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostManager.Domain.Constants;
using PostManager.Domain.Entities;

namespace PostManager.Infrastructure.Persistence.EntitiesConfigurations;

internal class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
{
    public void Configure(EntityTypeBuilder<PostComment> builder)
    {
        builder.ToTable("PostComment");

        builder.Property(x => x.Content).IsRequired()
                                        .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt).IsRequired()
                                          .HasDefaultValueSql(SqlConstants.GetUtcDate);

        builder.Property(x => x.UpdatedAt).IsRequired(false);

        builder.Property(x => x.PostId).IsRequired();

        builder.Property(x => x.UserId).IsRequired();
    }
}