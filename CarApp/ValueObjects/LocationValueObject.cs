namespace CarApp.ValueObjects;

public class LocationValueObject
{
    public required string Address { get; set; }
    public required double Latitude { get; set; }
    public required double Longitude { get; set; }
    public required string City { get; set; }
    public required string County { get; set; }
    public required string District { get; set; }
}