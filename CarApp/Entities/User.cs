using CarApp.ValueObjects;

namespace CarApp.Entities;

public class User : BaseEntity
{
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