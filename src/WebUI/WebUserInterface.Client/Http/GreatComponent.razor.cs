using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Net.Http.Json;

namespace WebUserInterface.Client.Http;

public partial class GreatComponent
{
    [Inject] HttpClient httpClient { get; set; } = null!;
    [Inject] IStringLocalizer<AuthService.Domain.Resources.Messages> StringLocalizer { get; set; } = null!;

    private string Message { get; set; } = string.Empty;
    private ViewModelBase ViewModel { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        ViewModel = await YouAreGreat();
    }

    private async Task<ViewModelBase> YouAreGreat()
    {
        Console.WriteLine(CultureInfo.CurrentUICulture.Name);
        Console.WriteLine(CultureInfo.CurrentCulture.Name);
        Message = StringLocalizer["NiceToSeeYou"];
        return await httpClient.GetFromJsonAsync<ViewModelBase>("auth-service/v1.0/user/great");
    }
}

/// <summary>
/// Base class for view models.
/// </summary>
public class ViewModelBase
{
    /// <summary>
    /// Response message which user gets from request.
    /// </summary>
    public string? ResponseMessage { get; set; } = null;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ViewModelBase()
    {
    }

    /// <summary>
    /// Parameterized constructor.
    /// </summary>
    /// <param name="responseMessage">Response from request.</param>
    public ViewModelBase(string? responseMessage) => ResponseMessage = responseMessage;
}