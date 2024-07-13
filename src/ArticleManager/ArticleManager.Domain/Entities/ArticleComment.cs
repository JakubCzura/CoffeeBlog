using ArticleManager.Domain.Entities.Basics;

namespace ArticleManager.Domain.Entities;

/// <summary>
/// Entity representing article's comment.
/// See <see cref="Article"/>
/// </summary>
public class ArticleComment 
    : DbEntityBase
{
    /// <summary>
    /// Content of comment, everything that user wants to tell.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when comment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when comment was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Id of article to which comment is related.
    /// </summary>
    public int ArticleId { get; set; }

    /// <summary>
    /// Id of user who wrote comment.
    /// </summary>
    public int UserId { get; set; }
}