using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunnerWebApp.Data;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;

namespace RunnerWebApp.Controllers
{
    public class RacesController : Controller
    {
        private readonly IRacesRepository _racesRepository;

        public RacesController(IRacesRepository racesRepository)
        {
            _racesRepository = racesRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Races> races = await _racesRepository.GetAllAsync();
            return View(races);
        }

        public async Task<IActionResult> Details(int id)
        {
            Races? race = await _racesRepository.GetByIdAsync(id);
            if (race != null)
                return View(race);
            return BadRequest("Esta carrera no puede ser encontrada");

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Races race)
        {
            if (!ModelState.IsValid)
                return View(race);

            _racesRepository.Add(race);
            return RedirectToAction("Index");
        }
    }
}
