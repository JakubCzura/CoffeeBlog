using AutoMapper;
using MediatR;
using PostManager.Application.ExtensionMethods.Automapper.Posts;
using PostManager.Application.Interfaces.Persistence.Repositories;
using PostManager.Application.Interfaces.Security.CurrentUsers;
using PostManager.Domain.Entities;
using PostManager.Domain.Models.Users;

namespace PostManager.Application.Commands.Posts.CreatePost;

/// <summary>
/// Command handler to create new post on blog website by a user. It's related to <see cref="CreatePostCommand"/>.
/// </summary>
/// <param name="postRepository">Interface to perform post operations in database.</param>
/// <param name="currentUserContext">Interface to get information about current signed in user.</param>
/// <param name="mapper">Automapper to map classes.</param>
public class CreatePostCommandHandler(IPostRepository postRepository,
                                      ICurrentUserContext currentUserContext,
                                      IMapper mapper)
    : IRequestHandler<CreatePostCommand, int>
{
    /// <summary>
    /// Handles request to create new post on blog website.
    /// </summary>
    /// <param name="request">Request command with details to create post by a user.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Id of created post.</returns>
    public async Task<int> Handle(CreatePostCommand request,
                                  CancellationToken cancellationToken)
    {
        CurrentAuthorizedUser currentAuthorizedUser = currentUserContext.GetCurrentAuthorizedUser();

        Post post = mapper.Map<Post>(request, currentAuthorizedUser.Id);
        return await postRepository.CreateAsync(post, cancellationToken);
    }
}