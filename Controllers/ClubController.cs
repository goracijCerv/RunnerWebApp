using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunnerWebApp.Data;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;

namespace RunnerWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository)
        {
           _clubRepository = clubRepository;
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
    }
}
