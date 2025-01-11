using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Football.Data.Entity;

public class League
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool Active { get; set; }
    public List<string>? Matches { get; set; }
    public List<string>? PlayerStats { get; set; }
}
