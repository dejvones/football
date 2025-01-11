using Microsoft.AspNetCore.Mvc;

namespace Football.App.Controllers
{
    public class LeagueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
