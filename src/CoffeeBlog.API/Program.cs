using CoffeeBlog.API.Middlewares;
using CoffeeBlog.Application.ExtensionMethods;
using CoffeeBlog.Infrastructure.ExtensionMethods;
using Serilog;

namespace CoffeeBlog.API;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationDI(builder.Configuration);
        builder.Services.AddInfrastructureDI(builder.Configuration);

        // Add services to the container.

        builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<ExceptionMiddleware>();
        builder.Services.AddSingleton<RequestDetailsMiddleware>();

        WebApplication app = builder.Build();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseMiddleware<RequestDetailsMiddleware>();

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
    }
}