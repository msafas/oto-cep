using CarApp.Enums;

namespace CarApp.ValueObjects;

public class TransactionStatusLogValueObject
{
    public required TransactionStatuses Status { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}