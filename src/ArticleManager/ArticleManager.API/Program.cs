using ArticleManager.API.ExtensionMethods.Swagger;
using ArticleManager.API.ExtensionMethods.Versioning;
using ArticleManager.API.Middlewares;
using ArticleManager.Application.ExtensionMethods.LayerRegistration;
using ArticleManager.Domain.SettingsOptions.Authentication;
using ArticleManager.Infrastructure.ExtensionMethods.LayerRegistration;
using Serilog;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.AppsettingsKey));

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

app.UseMiddleware<RequestDetailsMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerInterface();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();