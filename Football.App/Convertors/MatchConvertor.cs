using Football.App.ViewModels;
using Football.Data.Models;

namespace Football.App.Convertors;

public static class MatchConvertor
{
    public static MatchViewModel ConvertMatchToViewModel(MatchModel match)
    {
        return new MatchViewModel
        {
            Id = match.Id,
            Team1Player1Id = match.Team1.Player1Id,
            Team1Player1Name = match.Team1.Player1Name,
            Team1Player2Id = match.Team1.Player2Id,
            Team1Player2Name = match.Team1.Player2Name,
            Team2Player1Id = match.Team2.Player1Id,
            Team2Player1Name = match.Team2.Player1Name,
            Team2Player2Id = match.Team2.Player2Id,
            Team2Player2Name = match.Team2.Player2Name,
            Score1 = match.Team1.Score,
            Score2 = match.Team2.Score,
            Date = match.Date
        };
    }
}
