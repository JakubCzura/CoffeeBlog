using AuthService.API.Components;
using AuthService.API.ExtensionMethods.Swagger;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.API.Middlewares;
using AuthService.Application.ExtensionMethods.LayerRegistration;
using AuthService.Domain.SettingsOptions.Authentication;
using AuthService.Domain.SettingsOptions.PasswordHasher;
using AuthService.Domain.SettingsOptions.SecurityToken;
using AuthService.Domain.SettingsOptions.UserCredential;
using AuthService.Infrastructure.ExtensionMethods.LayerRegistration;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.AppsettingsKey));
builder.Services.Configure<PasswordHasherOptions>(builder.Configuration.GetSection(PasswordHasherOptions.AppsettingsKey));
builder.Services.Configure<UserCredentialOptions>(builder.Configuration.GetSection(UserCredentialOptions.AppsettingsKey));
builder.Services.Configure<SecurityTokenOptions>(builder.Configuration.GetSection(SecurityTokenOptions.AppsettingsKey));

builder.Services.AddApplicationDI(builder.Configuration);
builder.Services.AddInfrastructureDI(builder.Configuration);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersion();
builder.Services.AddSwagger();

builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<RequestDetailsMiddleware>();

WebApplication app = builder.Build();

app.UseMiddleware<RequestDetailsMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseSwaggerInterface();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(WebClient.UI._Imports).Assembly);

app.Run();