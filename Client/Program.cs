using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Services;
using Client.Utils;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri("https://localhost:7065") });
builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<MathService>();

await builder.Build().RunAsync();
