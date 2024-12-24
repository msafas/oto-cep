using CarApp.DTOs.User;

namespace CarApp.DTOs.Auth;

public class MeResponse
{
    public required UserDto User { get; set; }
}