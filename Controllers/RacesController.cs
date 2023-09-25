using Microsoft.AspNetCore.Mvc;
using RunnerWebApp.Dtos;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;

namespace RunnerWebApp.Controllers
{
    public class RacesController : Controller
    {
        private readonly IRacesRepository _racesRepository;
        private readonly IPhotoService _photoService;

        public RacesController(IRacesRepository racesRepository, IPhotoService photoService)
        {
            _racesRepository = racesRepository;
            _photoService = photoService;
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
        public async  Task<IActionResult> Create(RacesCreateDto raceForm)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceForm.Image);

                var race = new Races
                {
                    Title = raceForm.Title,
                    Description = raceForm.Description,
                    ImageUrl = result.Url.ToString(),
                    Address = new Address
                    {
                        City = raceForm.Address.City,
                        State = raceForm.Address.State,
                        PostalCode = raceForm.Address.PostalCode,
                        Street = raceForm.Address.Street,
                        Country = raceForm.Address.Country
                    }
                };

                _racesRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "faild upload image");
                return View(raceForm);
            }
        }
    }
}
