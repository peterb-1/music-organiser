using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Client.Utils;
using Microsoft.JSInterop;
using Shared.DTOs;

namespace Client.Services;

public class AuthenticationService
{
    private const string TOKEN_KEY = "token";

    private ApiClient ApiClient { get; }
    private ILocalStorageService LocalStorageService { get; }
    private Task InitialisationTask { get; }
    private string Token { get; set; } = string.Empty;

    public AuthenticationService(ApiClient apiClient, ILocalStorageService localStorageService)
    {
        ApiClient = apiClient;
        LocalStorageService = localStorageService;
        InitialisationTask = LoadStoredToken();
    }

    public Task EnsureInitializedAsync()
    {
        return InitialisationTask;
    }

    private async Task LoadStoredToken()
    {
        var typeInfo = JsonTypeInfo.CreateJsonTypeInfo<string>(JsonSerializerOptions.Web);
        Token = await LocalStorageService.GetItemAsync(TOKEN_KEY, typeInfo) ?? string.Empty;
    }

    public bool TryGetToken(out string token)
    {
        token = Token;
        return !string.IsNullOrWhiteSpace(token);
    }

    public async Task<Maybe<TokenResponse>> LoginAsync(LoginRequest request)
    {
        return await ApiClient.Post<LoginRequest, TokenResponse>("authentication/login", request)
            .Then(ProcessResponse);
    }

    public async Task<Maybe<TokenResponse>> RegisterAsync(RegisterRequest request)
    {
        return await ApiClient.Post<RegisterRequest, TokenResponse>("authentication/register", request)
            .Then(ProcessResponse);
    }

    private async Task ProcessResponse(TokenResponse response)
    {
        await LocalStorageService.SetItemAsync(TOKEN_KEY, response.Token);
        Token = response.Token;
    }
}
