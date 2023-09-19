using Microsoft.AspNetCore.Mvc;
using RunnerWebApp.Data;
using RunnerWebApp.Models;

namespace RunnerWebApp.Controllers
{
    public class RacesController : Controller
    {
        private readonly DataContext _context;

        public RacesController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Races> races = _context.Races.ToList();
            return View(races);
        }
    }
}
