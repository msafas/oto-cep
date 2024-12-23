using CarApp.Enums;
using CarApp.ValueObjects;

namespace CarApp.Entities;

public class Demand : BaseEntity
{
    public required string Description { get; set; }
    public required LocationValueObject Location { get; set; }
    public required DemandTypes DemandType { get; set; }
    public required List<OperationTypeValueObject> OperationTypes { get; set; } = [];

    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public required decimal Price { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }



}