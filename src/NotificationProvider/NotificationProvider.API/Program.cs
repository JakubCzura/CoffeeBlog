using NotificationProvider.Application.ExtensionMethods.LayerRegistration;
using NotificationProvider.Domain.SettingsOptions.Database;
using NotificationProvider.Domain.SettingsOptions.Email;
using NotificationProvider.Infrastructure.ExtensionMethods.LayerRegistration;
using Serilog;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.AppsettingsKey));
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection(EmailOptions.AppsettingsKey));

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