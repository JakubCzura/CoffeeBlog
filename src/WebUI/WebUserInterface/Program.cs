using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using WebUserInterface.Components;
using WebUserInterface.Handlers;
using Microsoft.AspNetCore.Components.Authorization;
using WebUserInterface.AuthenticationStateSyncer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuth0WebAppAuthentication(options => 
{
    options.Domain = builder.Configuration["Auth0:Domain"]!;
    options.ClientId = builder.Configuration["Auth0:ClientId"]!;
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"]!;
}).WithAccessToken(options =>
{
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.UseRefreshTokens = true;
});

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthTokenHandler>();

builder.Services.AddHttpClient("ApiGateway", client => client.BaseAddress = new Uri(builder.Configuration["ApiGatewayBaseUrl"]!))
                .AddHttpMessageHandler<AuthTokenHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
  .CreateClient("ApiGateway"));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapGet("/Account/Login", async (HttpContext httpContext, string returnUrl = "/") =>
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

    await httpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
});

app.MapGet("/Account/Logout", async (HttpContext httpContext) =>
{
    var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri("/")
            .Build();

    await httpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
});

app.MapRazorComponents<App>()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(WebUserInterface.Client._Imports).Assembly,
                            typeof(WebUserInterface.Client.Components._Imports).Assembly);

app.Run();
