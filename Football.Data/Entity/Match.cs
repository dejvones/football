using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Football.Data.Entity;

public class Match
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public DateTime Date { get; set; }
    public required string LeagueId { get; set; }
    public required Team Team1 { get; set; }
    public required Team Team2 { get; set; }
}

public class Team
{
    public required string Player1Id { get; set; }
    public required string Player1Name { get; set; }
    public string? Player2Id { get; set; }
    public string? Player2Name { get; set; }
    public int Score { get; set; }
}
