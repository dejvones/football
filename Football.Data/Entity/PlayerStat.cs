using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Football.Data.Entity;

public class PlayerStat
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; set; }

    public required string Player { get; set; }
    public required string League { get; set; }
    public required List<string> Matches { get; set; }
    public int Points { get; set; }
    public int MatchesPlayed { get; set; }
}
