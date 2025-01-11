using Football.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers;

public class HomeController(ILeagueRepository leagueRepository) : Controller
{
    private readonly ILeagueRepository _leagueRepository = leagueRepository;

    public IActionResult Index()
    {
        var leagues = _leagueRepository.GetAllAsync().Result;
        return View(leagues);
    }
}
