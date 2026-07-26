using System.Net.Http.Json;
using Shared.DTOs;

namespace Client.Services;

public class MathService
{
    private HttpClient HttpClient { get; }

    public MathService(HttpClient httpClient)
    {
        HttpClient = httpClient;
    }

    public async Task<MathResult?> AddAsync(MathRequest request)
    {
        var response = await HttpClient.PostAsJsonAsync("math/add", request);
        return await response.Content.ReadFromJsonAsync<MathResult>();
    }
}
