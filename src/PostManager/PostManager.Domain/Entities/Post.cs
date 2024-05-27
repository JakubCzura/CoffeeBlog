using PostManager.Domain.Entities.Basics;

namespace PostManager.Domain.Entities;

/// <summary>
/// Entity that represents a post published at blog.
/// </summary>
public class Post : DbEntityBase
{
    /// <summary>
    /// Title of the post.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Subtitle of the post, additional information about the post.
    /// </summary>
    public string? Subtitle { get; set; }

    /// <summary>
    /// Content of the post, everything that user wants to share with other people.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when post was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date and time when post was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Id of user who created post.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Collection of comments that are related to this post.
    /// </summary>
    public virtual List<PostComment> PostComments { get; set; } = [];
}