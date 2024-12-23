using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarApp.ValueObjects;

public class UserCommentValueObject
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } = Guid.NewGuid();
    [BsonRepresentation(BsonType.String)]
    public required Guid UserId { get; set; }
    public required string UserFullName { get; set; }
    public required string Description { get; set; }
    public List<FileValueObject>? Files { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}