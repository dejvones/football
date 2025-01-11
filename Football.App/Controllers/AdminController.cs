using Football.Data.Models;
using Football.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers;

public class AdminController(ILeagueRepository leagueRepository) : Controller
{
    private readonly ILeagueRepository _leagueRepository = leagueRepository;

    public async Task<IActionResult> AddLeague()
    {
        var league = new LeagueModel(string.Empty, "Liga 1", DateTime.Now, DateTime.Now.AddMonths(3));
        await _leagueRepository.AddLeagueAsync(league);
        return RedirectToAction("Index", "Home");
    }
}