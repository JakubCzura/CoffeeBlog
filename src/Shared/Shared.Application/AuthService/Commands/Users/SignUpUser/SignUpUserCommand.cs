using FluentResults;
using MediatR;
using Shared.Application.AuthService.Responses.Users;

namespace Shared.Application.AuthService.Commands.Users.SignUpUser;

/// <summary>
/// Request command to sign up a new user and save this user to database. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="Username"> User's username. It's unique in database. </param>
/// <param name="Email"> User's e-mail. It's unique in database. </param>
/// <param name="Password"> User's password that will be hashed. </param>
/// <param name="ConfirmPassword"> Confirmation of user's password. It must match <see cref="Password"/>. </param>
public record SignUpUserCommand(string Username,
                                string Email,
                                string Password,
                                string ConfirmPassword) : IRequest<Result<SignUpUserResponse>>;