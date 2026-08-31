using Microsoft.AspNetCore.Identity;

namespace Api.Models;

public class ApplicationUser : IdentityUser
{
    // TODO: encrypt tokens
    public string SpotifyUserId { get; set; } = string.Empty;
    public string SpotifyAccessToken { get; set; } = string.Empty;
    public string SpotifyRefreshToken { get; set; } = string.Empty;
    public DateTimeOffset SpotifyTokenExpiresAt { get; set; }
}
