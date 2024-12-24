using CarApp.DTOs.Common;

namespace CarApp.DTOs.User;

public class SearchUserDto : SearchDto
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
}