using FluentValidation.AspNetCore;
using PostManager.API.ExtensionMethods.Swagger;
using PostManager.API.ExtensionMethods.Versioning;
using PostManager.API.Middlewares;
using PostManager.Application.ExtensionMethods.LayerRegistration;
using PostManager.Infrastructure.ExtensionMethods.LayerRegistration;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationDI(builder.Configuration);
builder.Services.AddInfrastructureDI(builder.Configuration);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
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