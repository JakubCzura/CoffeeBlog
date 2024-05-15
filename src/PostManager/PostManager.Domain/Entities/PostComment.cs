namespace PostManager.Domain.Entities;

/// <summary>
/// Class representing a post comment.
/// See <see cref="Post"/>
/// </summary>
public class PostComment
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }
}