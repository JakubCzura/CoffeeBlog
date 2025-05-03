using NotificationProvider.API.ExtensionMethods.Swagger;
using NotificationProvider.API.ExtensionMethods.Versioning;
using NotificationProvider.API.Middlewares;
using NotificationProvider.Application.ExtensionMethods.LayerRegistration;
using NotificationProvider.Domain.SettingsOptions.Authentication;
using NotificationProvider.Domain.SettingsOptions.Database;
using NotificationProvider.Domain.SettingsOptions.Email;
using NotificationProvider.Infrastructure.ExtensionMethods.LayerRegistration;
using Serilog;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.AppsettingsKey));
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.AppsettingsKey));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.AppsettingsKey));

builder.Services.AddApplicationDI(builder.Configuration);
builder.Services.AddInfrastructureDI(builder.Configuration);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApiVersion();
builder.Services.AddSwagger();

builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<RequestDetailsMiddleware>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerInterface();
}

app.UseMiddleware<RequestDetailsMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();