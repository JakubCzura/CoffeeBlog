using AuthService.Domain.Enums;
using AuthService.Domain.ViewModels.Basics;
using FluentResults;
using MediatR;

namespace AuthService.Application.Commands.Accounts.BanAccountByUserId;

/// <summary>
/// Request command to ban user's account. It's handled using Mediatr and CQRS pattern.
/// </summary>
/// <param name="UserId"> User's Id whose account will be banned. </param>
/// <param name="BanReason">Why user's account is banned. </param>
/// <param name="BanNote"> Note with details about ban. </param>
/// <param name="BanEndsAt"> Date and time of ban expiration. </param>
public record BanAccountByUserIdCommand(int UserId,
                                        AccountBanReason BanReason,
                                        string BanNote,
                                        DateTime BanEndsAt) 
    : IRequest<Result<ViewModelBase>>;