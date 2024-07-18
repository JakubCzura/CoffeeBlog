using AutoMapper;
using EventBus.Domain.Events.CommonEvents;
using PostManager.Application.Commands.Posts.CreatePost;
using PostManager.Domain.Entities;

namespace PostManager.Application.ExtensionMethods.Automapper.Posts;

/// <summary>
/// Extension methods for <see cref="IMapper"/>.
/// </summary>
public static class AutoMapperForPostExtensions
{
    /// <summary>
    /// Maps <see cref="CreatePostCommand"/> to <see cref="Post"/>.
    /// </summary>
    /// <typeparam name="T"><see cref="RequestDetailCreatedEvent"/></typeparam>
    /// <param name="mapper"><see cref="IMapper"/></param>
    /// <param name="createPostCommand">Command with details to create a new post.</param>
    /// <param name="UserId">Id of user who creates the post.</param>
    /// <returns>Instance of <see cref="RequestDetailCreatedEvent"/></returns>
    public static Post Map<T>(this IMapper mapper,
                              CreatePostCommand createPostCommand,
                              int UserId) where T : Post
        => mapper.Map<Post>(createPostCommand, opt =>
        {
            opt.Items[nameof(Post.UserId)] = UserId;
        });
}