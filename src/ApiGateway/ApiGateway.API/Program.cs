using ApiGateway.Application.ExtensionMethods.LayerRegistration;
using ApiGateway.Domain.SettingsOptions.Authentication;
using ApiGateway.Infrastructure.ExtensionMethods.LayerRegistration;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.AppsettingsKey));

builder.Services.AddApplicationDI();
builder.Services.AddInfrastructureDI();

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ApiGateway"));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapReverseProxy();

app.Run();