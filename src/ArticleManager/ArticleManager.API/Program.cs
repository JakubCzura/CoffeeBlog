using ArticleManager.API.ExtensionMethods.Swagger;
using ArticleManager.API.ExtensionMethods.Versioning;
using ArticleManager.API.Middlewares;
using ArticleManager.Application.ExtensionMethods.ActionContext;
using ArticleManager.Application.ExtensionMethods.LayerRegistration;
using ArticleManager.Domain.ViewModels.Errors;
using ArticleManager.Infrastructure.ExtensionMethods.LayerRegistration;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
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

builder.Services.Configure<ApiBehaviorOptions>(config =>
{
    config.SuppressModelStateInvalidFilter = false;  //False to perform auto validation. Added for readability and code clear behaviour despite False is default value.
    config.InvalidModelStateResponseFactory = context
        => new BadRequestObjectResult(new ErrorDetailsViewModel(StatusCodes.Status400BadRequest, context.GetJoinedErrorsMessages()));
});

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