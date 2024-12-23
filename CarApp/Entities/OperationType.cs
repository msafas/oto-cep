using CarApp.ValueObjects;

namespace CarApp.Entities;

public class OperationType : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public bool IsRequired { get; set; } = false;

    public List<OperationValueObject> Operations { get; set; } = [];
}