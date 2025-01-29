using Football.App.ViewModels;
using Football.Data.Models;

namespace Football.App.Convertors;

public static class PlayerConvertor
{
    public static PlayerViewModel ConvertPlayerToViewModel(PlayerModel player)
    {
        var form = player.Stats.Form.ToList();
        form.Insert(0, Result.Future);

        return new PlayerViewModel
        {
            Id = player.Id,
            Name = player.Name,
            Registred = player.Registred,
            CurrentPoints = player.Stats.CurrentPoints,
            CurrentMatches = player.Stats.CurrentMatches,
            AllPoints = player.Stats.AllPoints,
            AllMatches = player.Stats.AllMatches,
            CurrentPointsPerMatch = player.Stats.CurrentMatches > 0
            ? ((double)player.Stats.CurrentPoints - 100) / player.Stats.CurrentMatches
            : 0,
            RateWin = player.Stats.CurrentMatches > 0 ? (double)player.Stats.Wins / player.Stats.CurrentMatches : 0,
            Form = [.. form]
        };
    }
}
