using ApiGateway.Domain.SettingsOptions.Authentication;
using ApiGateway.Infrastructure.ExtensionMethods.LayerRegistration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.AppsettingsKey));

builder.Services.AddInfrastructureDI();

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ApiGateway"));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.Run();