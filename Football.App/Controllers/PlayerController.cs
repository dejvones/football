using Football.App.Models;
using Football.Data.Models;
using Football.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers;

public class PlayerController(IPlayerService playerService) : Controller
{
    private readonly IPlayerService _playerService = playerService;

    public async Task<IActionResult> Index()
    {
        var players = await _playerService.GetAllAsync();
        return View(players.Select(x => new PlayerViewModel { Id = x.Id, Name = x.Name, Registred = x.Registred }));
    }

    public IActionResult AddPlayer()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddPlayer(PlayerViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _playerService.CreateAsync(model.Name);
            return RedirectToAction("Index");
        }
        return View(model);
    }
}
