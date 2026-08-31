using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Shared.DTOs;

namespace Client.Utils;

public class ApiClient(HttpClient httpClient, NavigationManager navigationManager)
{
    private HttpClient HttpClient { get; } = httpClient;
    private NavigationManager NavigationManager { get; } = navigationManager;

    public async Task<Maybe<TResult>> Get<TResult>(string requestUri)
    {
        try
        {
            var response = await HttpClient.GetAsync(requestUri);
            return await HandleResponseAsync<TResult>(response);
        }
        catch (HttpRequestException e)
        {
            return Maybe.Failure<TResult>(e.Message);
        }
    }

    public async Task<Maybe<TResult>> Post<TRequest, TResult>(string requestUri, TRequest request)
    {
        try
        {
            var response = await HttpClient.PostAsJsonAsync(requestUri, request);
            return await HandleResponseAsync<TResult>(response);
        }
        catch (HttpRequestException e)
        {
            return Maybe.Failure<TResult>(e.Message);
        }
    }

    private async Task<Maybe<TResult>> HandleResponseAsync<TResult>(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            var problem = await response.Content.ReadFromJsonAsync<ApiProblemDetails>();
            return Maybe.Failure<TResult>(problem?.Detail ?? "An unknown error occurred.");
        }

        var result = await response.Content.ReadFromJsonAsync<TResult>();
        return result != null
            ? Maybe.Success(result)
            : Maybe.Failure<TResult>("Empty response.");
    }

    public void NavigateToApi(string relativePath)
    {
        var baseUrl = HttpClient.BaseAddress!.ToString().TrimEnd('/');
        NavigationManager.NavigateTo($"{baseUrl}/{relativePath.TrimStart('/')}", forceLoad: true);
    }
}
