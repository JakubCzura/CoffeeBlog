using ArticleManager.Domain.Entities.Basics;

namespace ArticleManager.Domain.Entities;

/// <summary>
/// Class representing article's comment.
/// See <see cref="Article"/>
/// </summary>
public class ArticleComment : DbEntityBase
{
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int ArticleId { get; set; }

    public int UserId { get; set; }
}