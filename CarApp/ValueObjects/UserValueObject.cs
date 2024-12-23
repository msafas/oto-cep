using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarApp.ValueObjects;

public class UserValueObject
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
}