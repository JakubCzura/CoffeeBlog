using CoffeeBlog.Application.Interfaces.Persistence.Repositories;
using CoffeeBlog.Application.Interfaces.Security.CurrentUsers;
using CoffeeBlog.Domain.Entities;
using EntityFrameworkCore.Triggered;

namespace CoffeeBlog.Infrastructure.Persistence.Triggers;

internal class AdjustUserLastPasswordCount(ICurrentUserContext _currentUserContext,
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