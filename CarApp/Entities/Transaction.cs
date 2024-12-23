using System.Transactions;
using CarApp.Enums;
using CarApp.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarApp.Entities;

public class Transaction : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public required Guid DemandId { get; set; }
    [BsonRepresentation(BsonType.String)]
    public required Guid OfferId { get; set; }

    public required decimal Price { get; set; }

    public TransactionStatuses Status { get; set; } = TransactionStatuses.Pending;

    public List<TransactionStatusLogValueObject> StatusLogs { get; set; } = [];

    public required UserValueObject Customer { get; set; }
    public List<string>? CustomerFiles { get; set; }
    public string? CustomerNote { get; set; }
    public required UserValueObject Provider { get; set; }
    public List<string>? ProviderFiles { get; set; }
    public string? ProviderNote { get; set; }
}