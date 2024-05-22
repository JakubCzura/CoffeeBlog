using ArticleManager.API.Controllers.Basics;
using MediatR;

namespace ArticleManager.API.Controllers;

public class ArticleController(IMediator _mediator) : ApiControllerBase(_mediator)
{
}