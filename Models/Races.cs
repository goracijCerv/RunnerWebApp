using RunnerWebApp.Data.Enum;
using System.Globalization;

namespace RunnerWebApp.Models
{
    public class Races
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty; 
        public Address? Address { get; set; }
        public int? AddressId { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public AppUser? AppUser { get; set; }
        public int? AppUserId { get; set; }
    }
}
