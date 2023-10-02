using RunnerWebApp.Data.Enum;
using RunnerWebApp.Models;

namespace RunnerWebApp.Dtos
{
    public class RacesEditDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
        public Address Address { get; set; }
        public int? AddressId { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
