using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RunnerWebApp.Data;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;
//Los repositorios son para las bases de datos, los servicios son para lo demas
namespace RunnerWebApp.Repository
{
    public class ClubRepository : IClubRepository
    {
        private readonly ApplicationDbContext _context;

        public ClubRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public bool Add(Club club)
        {
            _context.Add(club);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return _context.SaveChanges() > 0;
        }

        public async Task<IEnumerable<Club>> GetAllAsync()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int id)
        {
           return await _context.Clubs.Include(c => c.Address).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Club>> GetClubByCityAsync(string city)
        {
            return await _context.Clubs.Where(c => c.Address.City == city).ToListAsync();
        }

        public  bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return _context.SaveChanges() > 0;
        }

        
    }
}
