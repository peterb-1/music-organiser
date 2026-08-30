using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client;
using Client.Handlers;
using Client.Services;
using Client.Utils;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalStorageServices();
builder.Services.AddScoped<ApiClient>();
builder.Services.AddScoped<AuthenticationHandler>();
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped(sp => 
{
    var handler = sp.GetRequiredService<AuthenticationHandler>();
    handler.InnerHandler = new HttpClientHandler();
    return new HttpClient(handler) 
    { 
        BaseAddress = new Uri("https://localhost:7065") 
    };
});

await builder.Build().RunAsync();
