using Football.App.Models;
using Football.Data.Models;
using Football.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers;

public class MatchController(IMatchService matchService, IPlayerService playerService) : Controller
{
    private readonly IMatchService _matchService = matchService;
    private readonly IPlayerService _playerService = playerService;

    public async Task<IActionResult> Index()
    {
        var matches = await _matchService.GetLastMatches();
        var vm = matches.Select(x => new MatchViewModel
        {
            Id = x.Id,
            Player1 = new PlayerViewModel { Id = x.Team1.Player1.Id, Name = x.Team1.Player1.Name, Registred = x.Team1.Player1.Registred },
            Player2 = new PlayerViewModel { Id = x.Team1.Player2.Id, Name = x.Team1.Player2.Name, Registred = x.Team1.Player2.Registred },
            Player3 = new PlayerViewModel { Id = x.Team2.Player1.Id, Name = x.Team2.Player1.Name, Registred = x.Team2.Player1.Registred },
            Player4 = new PlayerViewModel { Id = x.Team2.Player2.Id, Name = x.Team2.Player2.Name, Registred = x.Team2.Player2.Registred },
            Score1 = x.Team1.Score,
            Score2 = x.Team2.Score,
            Date = x.Date
        });
        return View(vm);
    }

    public async Task<IActionResult> AddMatch()
    {
        var players = await _playerService.GetAllAsync();
        var playerViewModels = players.Select(p => new PlayerViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Registred = p.Registred
        }).ToList();
        var model = new MatchViewModel { AllPlayers = playerViewModels };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddMatch(MatchViewModel model)
    {
        if (ModelState.IsValid)
        {
            var matchModel = new MatchModel(
                string.Empty,
                new MatchTeam(new PlayerModel(model.Player1Id, string.Empty, DateTime.MinValue), new PlayerModel(model.Player2Id, string.Empty, DateTime.MinValue), model.Score1),
                new MatchTeam(new PlayerModel(model.Player3Id, string.Empty, DateTime.MinValue), new PlayerModel(model.Player4Id, string.Empty, DateTime.MinValue), model.Score2),
                model.Date
            );

            await _matchService.CreateMatchAsync(matchModel);
            return RedirectToAction("Index");
        }

        var players = await _playerService.GetAllAsync();
        model.AllPlayers = players.Select(p => new PlayerViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Registred = p.Registred
        }).ToList();

        return View(model);
    }
}
