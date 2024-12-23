namespace CarApp.ValueObjects;

public class OperationTypeValueObject
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public List<OperationValueObject> Operations { get; set; } = [];
}