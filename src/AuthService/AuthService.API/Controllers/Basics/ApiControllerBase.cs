using AuthService.Application.ExtensionMethods.Collections;
using AuthService.Domain.Constants;
using AuthService.Domain.Resources;
using AuthService.Domain.ViewModels.Errors;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.API.Controllers.Basics;

/// <summary>
/// Base controller for all API controllers in the application.
/// </summary>
/// <param name="_mediator">Mediator to handle commands and queries using CQRS pattern.</param>
[ApiController]
[Route(RouteConstants.ApiController)]
[Produces(ContentTypeConstants.ApplicationJson)]
[Authorize]
public class ApiControllerBase(IMediator _mediator) 
    : ControllerBase
{
    /// <summary>
    /// Mediator to handle commands and queries using CQRS pattern.
    /// </summary>
    protected readonly IMediator Mediator = _mediator;

    /// <summary>
    /// Creates bad request object result based on errors that should be returned and status code 400.
    /// </summary>
    /// <param name="errors">Errors that were returned by command / query handler.</param>
    /// <returns>Instance of <see cref="BadRequestObjectResult"/></returns>
    protected BadRequestObjectResult CreateBadRequestObjectResult(IList<IError> errors)
        => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, errors.GetJoinedMessages()));

    /// <summary>
    /// Creates bad request object result based on default response and status code 400.
    /// </summary>
    /// <returns>Instance of <see cref="BadRequestObjectResult"/></returns>
    protected BadRequestObjectResult CreateBadRequestObjectResult()
        => BadRequest(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, ResponseMessages.UndefinedError));

    /// <summary>
    /// Creates not found object result based on errors that should be returned and status code 404.
    /// </summary>
    /// <param name="error">Error that was returned by command / query handler.</param>
    /// <returns>Instance of <see cref="NotFoundObjectResult"/></returns>
    protected NotFoundObjectResult CreateNotFoundObjectResult(IError error)
        => NotFound(new ErrorDetailsViewModel(StatusCodes.Status404NotFound, error.Message));

    /// <summary>
    /// Creates conflict object result based on errors that should be returned and status code 409.
    /// </summary>
    /// <param name="error">Error that was returned by command / query handler.</param>
    /// <returns>Instance of <see cref="ConflictObjectResult"/></returns>
    protected ConflictObjectResult CreateConflictObjectResult(IError error)
        => Conflict(new ErrorDetailsViewModel(StatusCodes.Status409Conflict, error.Message));

    /// <summary>
    /// Creates unauthorized object result based on errors that should be returned and status code 401.
    /// </summary>
    /// <param name="error">Error that was returned by command / query handler.</param>
    /// <returns>Instance of <see cref="UnauthorizedObjectResult"/></returns>
    protected UnauthorizedObjectResult CreateUnauthorizedObjectResult(IError error)
        => Unauthorized(new ErrorDetailsViewModel(StatusCodes.Status401Unauthorized, error.Message));
}