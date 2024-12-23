using CarApp.Enums;

namespace CarApp.ValueObjects;

public class FileValueObject
{
    public required string Url { get; set; }
    public string? Description { get; set; }
    public FileTypes? Type { get; set; }
}