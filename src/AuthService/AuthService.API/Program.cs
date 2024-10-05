using AuthService.API.ExtensionMethods.Swagger;
using AuthService.API.ExtensionMethods.Versioning;
using AuthService.API.Middlewares;
using AuthService.Application.ExtensionMethods.LayerRegistration;
using AuthService.Domain.Entities;
using AuthService.Domain.SettingsOptions.Authentication;
using AuthService.Domain.SettingsOptions.BanRemovalService;
using AuthService.Domain.SettingsOptions.PasswordHasher;
using AuthService.Domain.SettingsOptions.SecurityToken;
using AuthService.Domain.SettingsOptions.UserCredential;
using AuthService.Infrastructure.ExtensionMethods.LayerRegistration;
using Serilog;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AuthenticationOptions>(builder.Configuration.GetSection(AuthenticationOptions.AppsettingsKey));
builder.Services.Configure<PasswordHasherOptions>(builder.Configuration.GetSection(PasswordHasherOptions.AppsettingsKey));
builder.Services.Configure<UserCredentialOptions>(builder.Configuration.GetSection(UserCredentialOptions.AppsettingsKey));
builder.Services.Configure<SecurityTokenOptions>(builder.Configuration.GetSection(SecurityTokenOptions.AppsettingsKey));
builder.Services.Configure<BanRemovalServiceOptions>(builder.Configuration.GetSection(BanRemovalServiceOptions.AppsettingsKey));

builder.Services.AddApplicationDI(builder.Configuration);
builder.Services.AddInfrastructureDI(builder.Configuration);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddApiVersion();
//builder.Services.AddSwagger();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "AuthService.API", Version = "v1" });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Id = "Bearer",
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme
                }
            },
            new string[] { }
        }
    });
});
builder.Services.AddTransient<ExceptionMiddleware>();
builder.Services.AddTransient<RequestDetailsMiddleware>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwaggerInterface();
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerInterface();
}

app.UseMiddleware<RequestDetailsMiddleware>();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();
app.MapGroup("api/Account").MapIdentityApi<User>();


app.Run();