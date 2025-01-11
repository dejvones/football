using Football.App.ViewModels;
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
            if (await _playerService.PlayerExists(model.Name))
            {
                TempData["error"] = "Player with this name already exists.";
                return View(model);
            }

            await _playerService.CreateAsync(model.Name);
            TempData["success"] = "Player added successfully.";
            return RedirectToAction("Index");
        }
        TempData["error"] = "Invalid values.";
        return View(model);
    }
}
