using Football.App.ViewModels;
using Football.Data.Models;

namespace Football.App.Convertors;

public static class PlayerConvertor
{
    public static PlayerViewModel ConvertPlayerToViewModel(PlayerModel player)
    {
        return new PlayerViewModel
        {
            Id = player.Id,
            Name = player.Name,
            Registred = player.Registred,
            CurrentPoints = player.Stats.CurrentPoints,
            CurrentMatches = player.Stats.CurrentMatches,
            AllPoints = player.Stats.AllPoints,
            AllMatches = player.Stats.AllMatches
        };
    }
}
