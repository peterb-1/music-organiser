using Client.Utils;
using Shared.DTOs;

namespace Client.Services;

public class MathService(ApiClient apiClient)
{
    private ApiClient ApiClient { get; } = apiClient;

    public async Task<Maybe<MathResult>> AddAsync(MathRequest request)
    {
        return await ApiClient.Post<MathRequest, MathResult>("math/add", request);
    }
}
