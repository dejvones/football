using Football.App.Convertors;
using Football.App.ViewModels;
using Football.Data.Repository;
using Football.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers;

public class LeagueController(IPlayerService playerService, ILeagueRepository leagueRepository) : Controller
{
    private readonly IPlayerService _playerService = playerService;
    private readonly ILeagueRepository _leagueRepository = leagueRepository;

    public async Task<IActionResult> Index()
    {
        var ranking = await _playerService.GetCurrentRanking();
        var leagues = await _leagueRepository.GetActiveAsync();
        var rankingVm = ranking.Select(PlayerConvertor.ConvertPlayerToViewModel).ToList();
        var vm = new RankingViewModel { LeagueEnd = leagues.End, LeagueName = leagues.Name, LeagueStart = leagues.Start, Ranking = rankingVm };
        return View(vm);
    }
}
