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
    public int AllPoints { get; set; }
    public int CurrentPoints { get; set; }
    public int AllMatches { get; set; }
    public int CurrentMatches { get; set; }
    public int Wins { get; set; }
    public int[] Form { get; set; } = [0, 0, 0, 0, 0];
}
