using MediatR;
using PostManager.Application.Interfaces.Persistence.Repositories;
using PostManager.Domain.Entities;

namespace PostManager.Application.Commands.Posts.CreatePost;

public class CreatePostCommandHandler(IPostRepository _postRepository) 
    : IRequestHandler<CreatePostCommand, int>
{
    private readonly IPostRepository _postRepository = _postRepository;

    public async Task<int> Handle(CreatePostCommand request, 
                                  CancellationToken cancellationToken)
    {
        //TODO: use automapper
        Post post = new()
        {
            Title = request.Title,
            Subtitle = request.Subtitle,
            Content = request.Content,
            UserId = request.UserId,
        };

        return await _postRepository.CreateAsync(post, cancellationToken);
        throw new NotImplementedException();
    }
}