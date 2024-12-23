namespace DocvivoCrm.Framework.DTOs;

public class RedisSession
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string? Img { get; set; }
    public string? BackgroundImg { get; set; }
    public Guid FirmId { get; set; }
    public string FirmName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? PhoneCountryCode { get; set; }
    public string[] Roles { get; set; }
    public string[] ProtectedRoutes { get; set; }
    public bool IsPatient { get; set; } = false;
    public Guid? AdminUserId { get; set; }
    public string? AdminUserFullName { get; set; }
}