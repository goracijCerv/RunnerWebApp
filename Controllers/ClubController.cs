using Microsoft.AspNetCore.Mvc;
using RunnerWebApp.Dtos;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;
using System.Security.Claims;

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

        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null)
                return BadRequest("the club doesnt exits");

            var clubForm = new ClubEditDto
            {
                Title = club.Title,
                Description = club.Description,
                Url = club.ImageUrl,
                Category = club.Category,
                Address = club.Address,
                AddressId = (int)club.AddressId
            };
            return View(clubForm);
        }

        [HttpPost]
        public async Task<IActionResult>Edit(int id, ClubEditDto clubForm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "There is something wrong");
                return View(clubForm);
            }

            var club = await _clubRepository.GetByIdAsync(id);
            if (club == null)
                return BadRequest("Something went wrong");

            if(clubForm.Image == null)
            {
                club.Title = clubForm.Title;
                club.Description = clubForm.Description;
                club.Category = clubForm.Category;
                club.ImageUrl = clubForm.Url;
                club.Address.City = clubForm.Address.City;
                club.Address.Street = clubForm.Address.Street;
                club.Address.State = clubForm.Address.State;
                club.Address.PostalCode = clubForm.Address.PostalCode;
                club.Address.Country = clubForm.Address.Country;

                _clubRepository.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DelateImageAsync(club.ImageUrl);
                }
                catch(Exception e)
                {
                    return BadRequest(e);
                }

                var photoResult = await _photoService.AddPhotoAsync(clubForm.Image);
                club.Title = clubForm.Title;
                club.Description = clubForm.Description;
                club.Category = clubForm.Category;
                club.ImageUrl = photoResult.Url.ToString();
                club.Address.City = clubForm.Address.City;
                club.Address.Street = clubForm.Address.Street;
                club.Address.State = clubForm.Address.State;
                club.Address.PostalCode = clubForm.Address.PostalCode;
                club.Address.Country = clubForm.Address.Country;

                _clubRepository.Update(club);
                return RedirectToAction("Index");
            }
        }
    }
}