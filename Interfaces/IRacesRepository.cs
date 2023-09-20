using RunnerWebApp.Models;

namespace RunnerWebApp.Interfaces
{
    public interface IRacesRepository
    {
        Task<IEnumerable<Races>> GetAllAsync();
        Task<Races> GetByIdAsync(int id);
        Task<IEnumerable<Races>> GetRacesByCityAsync(string city);
        bool Add(Races races);
        bool Update(Races race);
        bool Delete(Races race);
        bool Save();
    }
}
