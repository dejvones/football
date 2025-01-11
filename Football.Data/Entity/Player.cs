using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Football.Data.Entity;

public class Player
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string Name { get; set; }
    public DateTime Registred { get; set; }
    public required List<string> PlayerStats { get; set; }
}
