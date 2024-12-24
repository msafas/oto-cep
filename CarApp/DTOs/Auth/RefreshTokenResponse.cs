namespace CarApp.DTOs.Auth;

public class RefreshTokenResponse
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
}