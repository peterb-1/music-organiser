using System.Text.Json.Serialization;

namespace Api.Contracts;

internal record SpotifyProfile(
    [property: JsonPropertyName("id")] string Id
);
