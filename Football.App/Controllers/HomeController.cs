using Football.App.Convertors;
using Football.App.ViewModels;
using Football.Data.Repository;
using Football.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers;

public class HomeController(IPlayerService playerService, IMatchService matchService, ILeagueRepository leagueRepository) : Controller
{
    private readonly IPlayerService _playerService = playerService;
    private readonly IMatchService _matchService = matchService;
    private readonly ILeagueRepository _leagueRepository = leagueRepository;

    public async Task<IActionResult> Index()
    {
        var ranking = await _playerService.GetCurrentRanking();
        var matches = await _matchService.GetLastMatches();
        var league = await _leagueRepository.GetActiveAsync();
        var rankingVm = ranking.Select(PlayerConvertor.ConvertPlayerToViewModel).ToList();
        var vm = new HomeViewModel { 
            Ranking = new RankingViewModel { Ranking = rankingVm, LeagueName = league.Name, LeagueStart = league.Start, LeagueEnd = league.End}, 
            LatestMatches = matches.Select(MatchConvertor.ConvertMatchToViewModel).ToList() };

        return View(vm);
    }
}
