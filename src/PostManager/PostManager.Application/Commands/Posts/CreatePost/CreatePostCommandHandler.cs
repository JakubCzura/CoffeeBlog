using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostManager.Application.Commands.Posts.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, int>
{
    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}