using Serilog;
using StatisticsCollector.Application.ExtensionMethods.LayerRegistration;
using StatisticsCollector.Domain.SettingsOptions.Authentication;
using StatisticsCollector.Domain.SettingsOptions.Database;
using StatisticsCollector.Domain.SettingsOptions.UsersDiagnosticsCollector;
using StatisticsCollector.Infrastructure.ExtensionMethods.LayerRegistration;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.AppsettingsKey));
builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.AppsettingsKey));
builder.Services.Configure<UsersDiagnosticsCollectorOptions>(builder.Configuration.GetSection(UsersDiagnosticsCollectorOptions.AppsettingsKey));

builder.Services.AddInfrastructureDI(builder.Configuration);
builder.Services.AddApplicationDI(builder.Configuration);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapControllers();

app.Run();