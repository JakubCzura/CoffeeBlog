using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

//Address of ApiGateway.
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7231") });

await builder.Build().RunAsync();
