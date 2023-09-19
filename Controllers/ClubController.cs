using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RunnerWebApp.Data;
using RunnerWebApp.Models;

namespace RunnerWebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly DataContext _context;

        public ClubController(DataContext context)
        {
            _context = context;
        }

        public  IActionResult Index()
        {
            List<Club> clubs = _context.Clubs.ToList();
            return  View(clubs);
        }

        public IActionResult Details(int id)
        {
            Club? club = _context.Clubs.Include(a => a.Address).FirstOrDefault(c => c.Id == id);
            if(club != null)
                return View(club);
            return BadRequest("No es posible mostrar dicho item");
        }
    }
}
