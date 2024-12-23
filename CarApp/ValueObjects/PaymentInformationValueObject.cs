namespace CarApp.ValueObjects;

public class PaymentInformationValueObject
{
    public string? Iban { get; set; }
    public string? CartNumber { get; set; }
    public string? ExpirationDate { get; set; }
    public string? Cvc { get; set; }
}