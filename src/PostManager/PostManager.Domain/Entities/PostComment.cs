using PostManager.Domain.Entities.Basics;

namespace PostManager.Domain.Entities;

/// <summary>
/// Class representing post's comment.
/// See <see cref="Post"/>
/// </summary>
public class PostComment : DbEntityBase
{
    /// <summary>
    /// Content of the comment, everything that user wants to tell.
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
    /// Id of post to which comment is related.
    /// </summary>
    public int PostId { get; set; }

    /// <summary>
    /// Id of user who wrote comment.
    /// </summary>
    public int UserId { get; set; }
}