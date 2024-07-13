using ArticleManager.Domain.Entities.Basics;

namespace ArticleManager.Domain.Entities;

/// <summary>
/// Entity that represents article which is created by user.
/// </summary>
public class Article 
    : DbEntityBase
{
    /// <summary>
    /// Title of article.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Subtitle of article, for example to give additional information about article.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Content of article, everything that user wants to share with other people.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when article was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when article was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Id of user who created article.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Collection of comments that are related to this article.
    /// </summary>
    public virtual List<ArticleComment> ArticleComments { get; set; } = [];
}