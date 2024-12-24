using CarApp.DTOs.User;

namespace CarApp.DTOs.Auth;

public class LoginRequestDto
{
    public required string Phone { get; set; }
    public required string Password { get; set; }
}