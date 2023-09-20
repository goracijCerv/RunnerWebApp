using Microsoft.EntityFrameworkCore;
using RunnerWebApp.Data;
using RunnerWebApp.Interfaces;
using RunnerWebApp.Models;

namespace RunnerWebApp.Repository
{
    public class RacesRepository : IRacesRepository
    {
        private readonly DataContext _context;

        public RacesRepository(DataContext context)
        {
            _context = context;
        }

        public bool Add(Races races)
        {
            _context.Add(races);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(Races race)
        {
            _context.Remove(race);
            return _context.SaveChanges() > 0;
        }

        public async Task<IEnumerable<Races>> GetAllAsync()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Races> GetByIdAsync(int id)
        {
            return await _context.Races.Include(r => r.Address).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Races>> GetRacesByCityAsync(string city)
        {
            return await _context.Races.Where(r => r.Address.City == city).ToListAsync();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool Update(Races race)
        {
            _context.Update(race);
            return _context.SaveChanges() > 0;
        }
    }
}
