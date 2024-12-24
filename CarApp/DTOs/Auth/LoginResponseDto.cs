using CarApp.DTOs.User;

namespace CarApp.DTOs.Auth;

public class LoginResponseDto
{
    public required UserDto User { get; set; }
    public required string Token { get; set; }
    public required DateTime Expiration { get; set; }
}