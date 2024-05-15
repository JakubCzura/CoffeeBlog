using ArticleManager.Domain.Entities.Basics;

namespace ArticleManager.Domain.Entities;

public class Article : DbEntityBase
{
    public string Title { get; set; } = string.Empty;

    public string? Subtitle { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }

    public virtual List<ArticleComment> ArticleComments { get; set; } = [];
}