﻿using FluentResults;
using MediatR;
using Shared.Application.AuthService.Responses.Users;
using System.ComponentModel.DataAnnotations;

namespace Shared.Application.AuthService.Queries.Users.SignInUser;

/// <summary>
/// Request query to sign in a user. It's handled using Mediatr and CQRS pattern.
/// </summary>
public class SignInUserQuery : IRequest<Result<SignInUserResponse>>
{
    /// <summary>
    /// User can log in using email.
    /// </summary>
    [Required(ErrorMessage = "E-mail is required.")]
    [MaxLength(320)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password.
    /// </summary>
    [Required(ErrorMessage = "Password is required.")]
    [Length(5, 50, ErrorMessage = "Password must be between 5 and 50 characters long")]
    public string Password { get; set; } = string.Empty;
}