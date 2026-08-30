using System.Net.Http.Headers;
using Client.Services;

namespace Client.Handlers;

public class AuthenticationHandler(IServiceProvider serviceProvider) : DelegatingHandler
{
    private IServiceProvider ServiceProvider { get; } = serviceProvider;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authService = ServiceProvider.GetRequiredService<AuthenticationService>();
        
        if (authService.TryGetToken(out var token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
