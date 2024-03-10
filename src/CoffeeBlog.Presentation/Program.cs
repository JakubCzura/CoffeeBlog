using CoffeeBlog.Presentation.Components;
using CoffeeBlog.Application.ExtensionMethods;
using CoffeeBlog.Infrastructure.ExtensionMethods;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using CoffeeBlog.Presentation.Middlewares;
using CoffeeBlog.Presentation.Versioning;
using CoffeeBlog.Presentation.Swagger;
using FluentValidation.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationDI(builder.Configuration);
builder.Services.AddInfrastructureDI(builder.Configuration);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddApiVersion();
builder.Services.AddSwagger();

builder.Services.Configure<ApiBehaviorOptions>(config =>
{
    config.InvalidModelStateResponseFactory = context =>
    {
        string result = string.Join(";", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        return new BadRequestObjectResult(result);
    };
    //config.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddSingleton<ExceptionMiddleware>();
builder.Services.AddSingleton<RequestDetailsMiddleware>();

WebApplication app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestDetailsMiddleware>();

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
    .AddAdditionalAssemblies(typeof(CoffeeBlog.Presentation.Client._Imports).Assembly);

app.Run();