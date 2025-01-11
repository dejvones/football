namespace Football.Data.Models;

public record PlayerModel(string Id, string Name, DateTime Registred, Stats Stats);
public record Stats(int AllPoints, int CurrentPoints, int AllMatches, int CurrentMatches);
