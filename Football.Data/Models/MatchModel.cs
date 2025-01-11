namespace Football.Data.Models;

public record MatchModel(string Id, MatchTeam Team1, MatchTeam Team2, DateTime Date);
public record MatchTeam(PlayerModel Player1, PlayerModel Player2, int Score);