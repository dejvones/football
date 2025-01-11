using Football.App.Convertors;
using Football.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers;

public class LeagueController(IPlayerService playerService) : Controller
{
    private readonly IPlayerService _playerService = playerService;

    public async Task<IActionResult> Index()
    {
        var ranking = await _playerService.GetCurrentRanking();
        var vm = ranking.Select(PlayerConvertor.ConvertPlayerToViewModel).ToList();
        return View(vm);
    }
}
