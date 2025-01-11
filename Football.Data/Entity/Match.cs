using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Football.Data.Entity;

public class Match
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public required string Team1Player1Stat { get; set; }
    public required string Team1Player2Stat { get; set; }
    public required string Team2Player1Stat { get; set; }
    public required string Team2Player2Stat { get; set; }
    public required string League { get; set; }
    public int Team1Result { get; set; }
    public int Team2Result { get; set; }
    public DateTime Date { get; set; }
}
