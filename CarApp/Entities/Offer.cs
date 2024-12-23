using CarApp.Enums;
using CarApp.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarApp.Entities;

public class Offer : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public required Guid DemandId { get; set; }

    public required List<OperationTypeValueObject> OperationTypes { get; set; } = [];
    public required decimal Price { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public OfferStatuses OfferStatus { get; set; } = OfferStatuses.Pending;

    public List<OfferStatusLogValueObject> StatusLogs { get; set; } = [];
}