using CarApp.Enums;

namespace CarApp.ValueObjects;

public class OfferStatusLogValueObject
{
    public required OfferStatuses Status { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}