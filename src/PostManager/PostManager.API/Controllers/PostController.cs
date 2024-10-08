﻿using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostManager.API.Controllers.Basics;
using PostManager.API.ExtensionMethods.Versioning;
using PostManager.Application.Commands.Posts.CreatePost;
using PostManager.Domain.ViewModels.Errors;

namespace PostManager.API.Controllers;

/// <summary>
/// Controller to manage post entity.
/// </summary>
/// <param name="mediator">Mediator to handle commands and queries using CQRS pattern.</param>
[ApiVersion(ApiVersioningInfo.Version_1_0)]
public class PostController(IMediator mediator) : ApiControllerBase
{
    /// <summary>
    /// Endpoint to create post on website.
    /// </summary>
    /// <param name="createPostCommand">Details to create user's post on website.</param>
    /// <param name="cancellationToken">Token to cancel asynchronous operation.</param>
    /// <returns>Id of created post.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(UnauthorizedResult), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDetailsViewModel), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> Create([FromBody] CreatePostCommand createPostCommand,
                                                CancellationToken cancellationToken)
        => await mediator.Send(createPostCommand, cancellationToken);
}