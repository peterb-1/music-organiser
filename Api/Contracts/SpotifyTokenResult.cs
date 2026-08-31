namespace Api.Contracts;

public record SpotifyTokenResult(
    string SpotifyUserId,
    string AccessToken,
    string RefreshToken,
    DateTimeOffset ExpiresAt
);
