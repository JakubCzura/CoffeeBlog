using MediatR;

namespace PostManager.Application.Commands.Posts.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}