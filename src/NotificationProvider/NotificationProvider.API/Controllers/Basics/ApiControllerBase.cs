﻿using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotificationProvider.Application.ExtensionMethods.Collections;
using NotificationProvider.Domain.Constants;
using Shared.Application.Common.Responses.Errors;
using Shared.Domain.Common.Resources.Translations;

namespace NotificationProvider.API.Controllers.Basics;

/// <summary>
/// Base controller for all API controllers in the application.
/// </summary>
[ApiController]
[Route(RouteConstants.ApiController)]
[Produces(ContentTypeConstants.ApplicationJson)]
[Authorize]
public class ApiControllerBase : ControllerBase
{
    /// <summary>
    /// Creates bad request object result based on errors that should be returned and status code 400.
    /// </summary>
    /// <param name="errors">Errors that were returned by command / query handler.</param>
    /// <returns>Instance of <see cref="BadRequestObjectResult"/></returns>
    protected BadRequestObjectResult CreateBadRequestObjectResult(IList<IError> errors)
        => BadRequest(new ErrorDetailsResponse(StatusCodes.Status400BadRequest, errors.GetJoinedMessages()));

    /// <summary>
    /// Creates bad request object result based on default response and status code 400.
    /// </summary>
    /// <returns>Instance of <see cref="BadRequestObjectResult"/></returns>
    protected BadRequestObjectResult CreateBadRequestObjectResult()
        => BadRequest(new ErrorDetailsResponse(StatusCodes.Status400BadRequest, ResponseMessages.UndefinedError));

    /// <summary>
    /// Creates not found object result based on errors that should be returned and status code 404.
    /// </summary>
    /// <param name="error">Error that was returned by command / query handler.</param>
    /// <returns>Instance of <see cref="NotFoundObjectResult"/></returns>
    protected NotFoundObjectResult CreateNotFoundObjectResult(IError error)
        => NotFound(new ErrorDetailsResponse(StatusCodes.Status404NotFound, error.Message));

    /// <summary>
    /// Creates conflict object result based on errors that should be returned and status code 409.
    /// </summary>
    /// <param name="error">Error that was returned by command / query handler.</param>
    /// <returns>Instance of <see cref="ConflictObjectResult"/></returns>
    protected ConflictObjectResult CreateConflictObjectResult(IError error)
        => Conflict(new ErrorDetailsResponse(StatusCodes.Status409Conflict, error.Message));

    /// <summary>
    /// Creates unauthorized object result based on errors that should be returned and status code 401.
    /// </summary>
    /// <param name="error">Error that was returned by command / query handler.</param>
    /// <returns>Instance of <see cref="UnauthorizedObjectResult"/></returns>
    protected UnauthorizedObjectResult CreateUnauthorizedObjectResult(IError error)
        => Unauthorized(new ErrorDetailsResponse(StatusCodes.Status401Unauthorized, error.Message));
}