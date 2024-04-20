using AuthService.Application.Interfaces.Persistence.Repositories;
using AuthService.Application.Interfaces.Security.CurrentUsers;
using AuthService.Domain.Entities;
using EntityFrameworkCore.Triggered;

namespace AuthService.Infrastructure.Persistence.Triggers;

internal sealed class AdjustUserLastPasswordCountTrigger(ICurrentUserContext _currentUserContext,
                                                         IUserLastPasswordRepository _userLastPasswordRepository) : IAfterSaveTrigger<UserLastPassword>
{
    private readonly ICurrentUserContext _currentUserContext = _currentUserContext;
    private readonly IUserLastPasswordRepository _userLastPasswordRepository = _userLastPasswordRepository;

    public async Task AfterSave(ITriggerContext<UserLastPassword> context,
                                CancellationToken cancellationToken)
    {
        if (context.ChangeType == ChangeType.Added)
        {
            await _userLastPasswordRepository.AdjustUserLastPasswordCountByUserIdAsync(_currentUserContext.GetCurrentAuthorizedUser().Id, cancellationToken);
        }
    }
}