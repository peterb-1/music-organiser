using System.Net.Http.Json;

namespace Client.Utils;

public class ApiClient
{
    private HttpClient HttpClient { get; }

    public ApiClient(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<Maybe<TResult>> Get<TResult>(string requestUri)
    {
        try
        {
            var result = await HttpClient.GetFromJsonAsync<TResult>(requestUri);
            return result != null
                ? Maybe.Success(result)
                : Maybe.Failure<TResult>("Empty response");
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
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TResult>();
            return result != null
                ? Maybe.Success(result)
                : Maybe.Failure<TResult>("Empty response");
        }
        catch (HttpRequestException e)
        {
            return Maybe.Failure<TResult>(e.Message);
        }
    }
}
