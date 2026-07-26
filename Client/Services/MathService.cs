using Client.Utils;
using Shared.DTOs;

namespace Client.Services;

public class MathService
{
    private ApiClient ApiClient { get; }

    public MathService(ApiClient apiClient)
    {
        ApiClient = apiClient;
    }

    public async Task<Maybe<MathResult>> AddAsync(MathRequest request)
    {
        return await ApiClient.Post<MathRequest, MathResult>("math/add", request);
    }
}
