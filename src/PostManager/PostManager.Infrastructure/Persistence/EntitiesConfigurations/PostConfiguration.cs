using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PostManager.Domain.Constants;
using PostManager.Domain.Entities;

namespace PostManager.Infrastructure.Persistence.EntitiesConfigurations;

/// <summary>
/// Configuration for <see cref="Post"/> in database.
/// </summary>
internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Post");

        builder.Property(x => x.Title).IsRequired()
                                      .HasMaxLength(100);

        builder.Property(x => x.Subtitle).IsRequired(false)
                                         .HasMaxLength(100);

        builder.Property(x => x.Content).IsRequired()
                                        .HasMaxLength(5000);

        builder.Property(x => x.CreatedAt).IsRequired()
                                          .HasDefaultValueSql(SqlConstants.GetUtcDate);

        builder.Property(x => x.UpdatedAt).IsRequired(false);

        builder.Property(x => x.UserId).IsRequired();

        builder.HasMany(x => x.PostComments)
               .WithOne()
               .HasForeignKey(x => x.PostId)
               .IsRequired();
    }
}