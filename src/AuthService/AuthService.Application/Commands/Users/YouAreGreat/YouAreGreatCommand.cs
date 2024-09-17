using AuthService.Domain.ViewModels.Basics;
using MediatR;

namespace AuthService.Application.Commands.Users.YouAreGreat;

public class YouAreGreatCommand : IRequest<ViewModelBase>
{
    public string Name { get; set; }
}