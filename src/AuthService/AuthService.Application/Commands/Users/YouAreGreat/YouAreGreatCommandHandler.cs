using AuthService.Domain.ViewModels.Basics;
using MediatR;
using System.Globalization;
using Translations = AuthService.Domain.Resources.Messages;

namespace AuthService.Application.Commands.Users.YouAreGreat;

public class YouAreGreatCommandHandler : IRequestHandler<YouAreGreatCommand, ViewModelBase>
{
    public async Task<ViewModelBase> Handle(YouAreGreatCommand request, CancellationToken cancellationToken)
    {
        Console.WriteLine(CultureInfo.CurrentUICulture.Name);
        Console.WriteLine(CultureInfo.CurrentCulture.Name);
        return await Task.FromResult(new ViewModelBase(Translations.YouAreGreat));
    }
}