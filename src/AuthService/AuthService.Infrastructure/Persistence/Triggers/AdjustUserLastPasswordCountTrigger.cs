using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Domain.Entities;
using EntityFrameworkCore.Triggered;
using Microsoft.Extensions.Logging;

namespace AuthService.Infrastructure.Persistence.Triggers;

internal sealed class AdjustUserLastPasswordCountTrigger(ILogger<AdjustUserLastPasswordCountTrigger> _logger,
                                                         ICurrentUserContext _currentUserContext,
                                                         IUserLastPasswordRepository _userLastPasswordRepository) : IAfterSaveTrigger<UserLastPassword>
{
    private readonly ILogger<AdjustUserLastPasswordCountTrigger> _logger = _logger;
    private readonly ICurrentUserContext _currentUserContext = _currentUserContext;
    private readonly IUserLastPasswordRepository _userLastPasswordRepository = _userLastPasswordRepository;

    public async Task AfterSave(ITriggerContext<UserLastPassword> context,
                                CancellationToken cancellationToken)
    {
        try
        {
            if (context.ChangeType == ChangeType.Added)
            {
                await _userLastPasswordRepository.AdjustUserLastPasswordCountByUserIdAsync(_currentUserContext.GetCurrentAuthorizedUser().Id, cancellationToken);
            }
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Error in database trigger while adjusting user's last password count.");
        }
    }
}