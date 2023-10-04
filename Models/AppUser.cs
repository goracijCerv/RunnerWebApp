using Microsoft.AspNetCore.Identity
namespace RunnerWebApp.Models
{
    public class AppUser : IdentityUser
    {
        public int? Pace {get; set;}
        public int? Mileage {get; set;}
        [ForeignKey("Address")]
        public int AddressId
        public Address? Address { get; set; }
        public ICollection<Club>? Clubs { get; set; }
        public ICollection<Races>? Races { get; set; }
    }
}
