using Microsoft.AspNetCore.Mvc;

namespace RunnerWebApp.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
