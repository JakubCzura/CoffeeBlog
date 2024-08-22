using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Domain.Entities;
using AuthService.Domain.SettingsOptions.UserCredential;
using EntityFrameworkCore.Triggered;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AuthService.Infrastructure.Persistence.Triggers;

/// <summary>
/// Database trigger to adjust user's last password count.
/// When a new password is added, this trigger will adjust the user's last password count.
/// We have limit on how many last passwords a user can have.
/// </summary>
/// <param name="logger">Logger to log exceptions.</param>
/// <param name="_userCredentialOptions">Settings to perform adjusting user's last password count.</param>
/// <param name="currentUserContext">Interface to get information about current signed in user.</param>
/// <param name="userLastPasswordRepository">Interface to perform user's last passwords operations in database.</param>
internal sealed class AdjustUserLastPasswordCountTrigger(ILogger<AdjustUserLastPasswordCountTrigger> logger,
                                                         IOptions<UserCredentialOptions> _userCredentialOptions,
                                                         ICurrentUserContext currentUserContext,
                                                         IUserLastPasswordRepository userLastPasswordRepository) : IAfterSaveTrigger<UserLastPassword>
{
    private readonly UserCredentialOptions _userCredentialOptions = _userCredentialOptions.Value;

    public async Task AfterSave(ITriggerContext<UserLastPassword> context,
                                CancellationToken cancellationToken)
    {
        try
        {
            if (context.ChangeType == ChangeType.Added)
            {
                await userLastPasswordRepository.AdjustUserLastPasswordCountByUserIdAsync(currentUserContext.GetCurrentAuthorizedUser().Id,
                                                                                          _userCredentialOptions.LastPasswordCount,
                                                                                          cancellationToken);
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error in database trigger while adjusting user's last password count.");
        }
    }
}