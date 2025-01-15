using Football.App.Convertors;
using Football.App.ViewModels;
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
        var matches = await _matchService.GetAllMatches();
        var vm = matches.Select(MatchConvertor.ConvertMatchToViewModel).GroupBy(x => x.Date);
        return View(vm);
    }

    public async Task<IActionResult> AddMatch()
    {
        var vm = new MatchViewModel { AllPlayers = await GetAllPlayers(), Date = DateTime.Now };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddMatch(MatchViewModel model)
    {
        model.AllPlayers.AddRange(await GetAllPlayers());
        if (ModelState.IsValid)
        {
            if (model.Team1Player1Id is null || model.Team2Player1Id is null)
            {
                TempData["error"] = "Players are not selected.";
                return View(model);
            }
            if ((model.Team1Player2Id is null && model.Team2Player2Id is not null) || (model.Team1Player2Id is not null && model.Team2Player2Id is null))
            {
                TempData["error"] = "Each team must have the same number of players.";
                return View(model);
            }
            if (model.Score1 < 0 || model.Score1 > 2 || model.Score2 < 0 || model.Score2 > 2 || (model.Score1 + model.Score2) < 2 || (model.Score1 + model.Score2) > 3)
            {
                TempData["error"] = "Invalid score.";
                return View(model);
            }

            var players = (await _playerService.GetAllAsync()).ToList();
            var matchModel = new MatchModel(
                string.Empty, string.Empty,
                new MatchTeam(model.Team1Player1Id!, GetPlayerName(model.Team1Player1Id!, players)!, 
                    model.Team1Player2Id, GetPlayerName(model.Team1Player2Id, players), model.Score1),
                new MatchTeam(model.Team2Player1Id!, GetPlayerName(model.Team2Player1Id!, players)!, 
                    model.Team2Player2Id, GetPlayerName(model.Team2Player2Id, players), model.Score2),
                model.Date
            );

            await _matchService.CreateMatchAsync(matchModel);
            TempData["success"] = "Match added successfully.";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Invalid values.";
        return View(model);
    }

    private static string? GetPlayerName(string? id, List<PlayerModel> players)
    {
        return players.FirstOrDefault(x => x.Id == id)?.Name;
    }

    private async Task<List<PlayerViewModel>> GetAllPlayers()
    {
        var players = await _playerService.GetAllAsync();
        var playerViewModels = players.Select(PlayerConvertor.ConvertPlayerToViewModel).ToList();
        playerViewModels.Insert(0, new PlayerViewModel { Id = null, Name = "Select player" });
        return playerViewModels;
    }
}
