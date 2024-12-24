using CarApp.ValueObjects;

namespace CarApp.DTOs;

public class RedisSession
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? ProfileImage { get; set; }
    public string? Role { get; set; }
    public bool IsActive { get; set; } = true;

    public PaymentInformationValueObject? PaymentInformation { get; set; }
}