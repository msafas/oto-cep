using CarApp.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CarApp.Entities;

public class UserComment : BaseEntity
{
    [BsonRepresentation(BsonType.String)]
    public required Guid DemandId { get; set; }
    [BsonRepresentation(BsonType.String)]
    public required Guid UserId { get; set; }
    public required string UserFullName { get; set; }
    public required string Comment { get; set; }
    public required short Star { get; set; }
    public List<FileValueObject>? Files { get; set; }

    public List<UserCommentValueObject>? Answers { get; set; }

    public bool IsLocked { get; set; } = false;
    public bool IsHidden { get; set; } = false;
    public bool IsReported { get; set; } = false;
    public string? ReportDescription { get; set; }
}