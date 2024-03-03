using CoffeeBlog.Presentation.Components;
using CoffeeBlog.Application.ExtensionMethods;
using CoffeeBlog.Infrastructure.ExtensionMethods;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using CoffeeBlog.Presentation.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddApplicationDI(builder.Configuration);
builder.Services.AddInfrastructureDI(builder.Configuration);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
builder.Services.AddControllers();
builder.Services.Configure<ApiBehaviorOptions>(config =>
{
    config.InvalidModelStateResponseFactory = context =>
    {
        string result = string.Join(";", context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
        return new BadRequestObjectResult(result);
    };
    //config.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<ExceptionMiddleware>();
builder.Services.AddSingleton<RequestDetailsMiddleware>();


WebApplication app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestDetailsMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(CoffeeBlog.Presentation.Client._Imports).Assembly);

app.UseHttpsRedirection();

app.Run();
