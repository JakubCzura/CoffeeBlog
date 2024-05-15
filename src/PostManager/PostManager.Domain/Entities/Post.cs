using PostManager.Domain.Entities.Basics;

namespace PostManager.Domain.Entities;

public class Post : DbEntityBase
{
    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int UserId { get; set; }

    public virtual List<PostComment> PostComments { get; set; } = [];
}