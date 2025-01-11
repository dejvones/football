namespace Football.Data.Models;

public record MatchModel(string Id, string LeagueId, MatchTeam Team1, MatchTeam Team2, DateTime Date);
public record MatchTeam(string Player1Id, string Player1Name, string? Player2Id, string? Player2Name, int Score);