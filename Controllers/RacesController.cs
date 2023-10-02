using Microsoft.AspNetCore.Mvc;
using RunnerWebApp.Dtos;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;
using RunnerWebApp.Repository;

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

        public async Task<IActionResult> Edit(int id)
        {
            var race = await _racesRepository.GetByIdAsync(id);
            if (race == null)
                return BadRequest("Not found");

            var raceForm = new RacesEditDto
            {
                Title = race.Title,
                Description = race.Description,
                Url = race.ImageUrl,
                Address = race.Address,
                AddressId = race.AddressId
            };

            return View(raceForm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RacesEditDto racesEdit)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something is wrong");
                return BadRequest();
            }

            var race = await _racesRepository.GetByIdAsync(id);
            if (race == null)
                return NotFound();

            if(racesEdit.Image == null)
            {
                race.Title = racesEdit.Title;
                race.Description = racesEdit.Description;
                race.RaceCategory = racesEdit.RaceCategory;
                race.ImageUrl = racesEdit.Url;
                race.Address.City = racesEdit.Address.City;
                race.Address.Street = racesEdit.Address.Street;
                race.Address.State = racesEdit.Address.State;
                race.Address.PostalCode = racesEdit.Address.PostalCode;
                race.Address.Country = racesEdit.Address.Country;

                _racesRepository.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DelateImageAsync(race.ImageUrl);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }

                var photoResult = await _photoService.AddPhotoAsync(racesEdit.Image);
                race.Title = racesEdit.Title;
                race.Description = racesEdit.Description;
                race.RaceCategory = racesEdit.RaceCategory;
                race.ImageUrl = photoResult.Url.ToString();
                race.Address.City = racesEdit.Address.City;
                race.Address.Street = racesEdit.Address.Street;
                race.Address.State = racesEdit.Address.State;
                race.Address.PostalCode = racesEdit.Address.PostalCode;
                race.Address.Country = racesEdit.Address.Country;

                _racesRepository.Update(race);
                return RedirectToAction("Index");
            }
        }
    }
}
