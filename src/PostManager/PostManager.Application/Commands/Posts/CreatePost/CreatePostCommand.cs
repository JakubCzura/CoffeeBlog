using MediatR;

namespace PostManager.Application.Commands.Posts.CreatePost;

/// <summary>
/// Request command to create new post on blog website. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="Title"> Title of the post. </param>
/// <param name="Subtitle"> Subtitle of the post, additional information about the post. </param>
/// <param name="Content"> Content of the post, everything that user wants to share with other people. </param>
/// <param name="UserId"> Id of user who created post. </param>
public record CreatePostCommand(string Title,
                                string? Subtitle,
                                string Content,
                                int UserId) : IRequest<int>;