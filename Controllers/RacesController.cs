using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult Details(int id)
        {
            Races? race = _context.Races.Include(a => a.Address ).FirstOrDefault(r => r.Id == id);
            if (race != null)
                return View(race);
            return BadRequest("Esta carrera no puede ser encontrada");

        }
    }
}
