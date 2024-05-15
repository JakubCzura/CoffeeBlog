using PostManager.Domain.Entities.Basics;

namespace PostManager.Domain.Entities;

/// <summary>
/// Class representing post's comment.
/// See <see cref="Post"/>
/// </summary>
public class PostComment : DbEntityBase
{
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }
}