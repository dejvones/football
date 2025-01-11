using Football.App.Convertors;
using Football.App.ViewModels;
using Football.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers;

public class HomeController(IPlayerService playerService, IMatchService matchService) : Controller
{
    private readonly IPlayerService _playerService = playerService;
    private readonly IMatchService _matchService = matchService;

    public async Task<IActionResult> Index()
    {
        var ranking = await _playerService.GetCurrentRanking();
        var matches = await _matchService.GetLastMatches();
        var vm = new HomeViewModel { 
            Ranking = ranking.Select(PlayerConvertor.ConvertPlayerToViewModel).ToList(), 
            LatestMatches = matches.Select(MatchConvertor.ConvertMatchToViewModel).ToList() };

        return View(vm);
    }
}
