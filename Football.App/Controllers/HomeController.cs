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
        var rankingVm = ranking.Select(PlayerConvertor.ConvertPlayerToViewModel).OrderByDescending(x => x.CurrentPoints).ThenByDescending(x => x.CurrentPointsPerMatch).ToList();
        var vm = new HomeViewModel { 
            Ranking = new RankingViewModel { Ranking = rankingVm.Take(10).ToList(), LeagueName = league.Name, LeagueStart = league.Start, LeagueEnd = league.End}, 
            LatestMatches = matches.Select(MatchConvertor.ConvertMatchToViewModel).Take(5).GroupBy(x => x.Date).ToList(),
            ShowAllMatches = matches.Count() > 5, ShowAllRanking = rankingVm.Count > 10};

        return View(vm);
    }
}
