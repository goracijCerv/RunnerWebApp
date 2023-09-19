using RunnerWebApp.Data.Enum;

namespace RunnerWebApp.Models
{
    public class Club
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl {  get; set; } = string.Empty;
        public Address? Address { get; set; }
        public int? AddressId { get; set; }
        public ClubCategory Category { get; set; }
        public AppUser? AppUser { get; set; }
        public int? AppUserId {  get; set; }
    }
}
