using Microsoft.AspNetCore.Mvc;
using RunnerWebApp.Dtos;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;

namespace RunnerWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;

        public ClubController(IClubRepository clubRepository, IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
        }

        public  async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAllAsync();
            return  View(clubs);
        }

        public async Task<IActionResult> Details(int id)
        {
            Club? club = await _clubRepository.GetByIdAsync(id);
            if(club != null)
                return View(club);
            return BadRequest("No es posible mostrar dicho item");
        }
        
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClubDto clubForm)
        {
            if(ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubForm.Image);
                var club = new Club
                {
                    Title = clubForm.Title,
                    Description = clubForm.Description,
                    ImageUrl = result.Url.ToString(),
                    Category = clubForm.Category,
                    Address = new Address
                    {
                        City = clubForm.Address.City,
                        Street = clubForm.Address.Street,
                        State = clubForm.Address.State,
                        PostalCode = clubForm.Address.PostalCode,
                        Country = clubForm.Address.Country
                    }
                };

                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo update error");
                return View(clubForm);
            }
        }

    }
}
