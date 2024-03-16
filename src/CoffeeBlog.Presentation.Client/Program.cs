using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped(sp =>
{
    return new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
});

await builder.Build().RunAsync();