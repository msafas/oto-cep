using CarApp.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarApp.Entities;

public class BaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [BsonRepresentation(BsonType.String)]
    public Guid? CreatedById { get; set; }
    public UserValueObject? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid? UpdatedById { get; set; }
    public UserValueObject? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime? DeletedAt { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Guid? DeletedById { get; set; }
    public UserValueObject? DeletedBy { get; set; }
}