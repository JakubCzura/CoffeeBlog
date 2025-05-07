using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using WebUserInterface;
using WebUserInterface.Constants.Communication;
using WebUserInterface.Services.Communication.NotificationProvider;
using WebUserInterface.Services.Communication.NotificationProvider.Interfaces;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient(HttpClientConstants.ApiGateway, client => client.BaseAddress = new Uri(builder.Configuration["ApiGateway:Address"]!));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient(HttpClientConstants.ApiGateway));

builder.Services.AddScoped<INewsletterSubscriptionCommunicationService, NewsletterSubscriptionCommunicationService>();

await builder.Build().RunAsync();