namespace CarApp.DTOs.User;

public class CreateUserDto
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? ProfileImage { get; set; }
    public string? Role { get; set; }
}