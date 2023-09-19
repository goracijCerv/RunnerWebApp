using Microsoft.AspNetCore.Mvc;

namespace RunnerWebApp.Controllers
{
    public class RacesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
