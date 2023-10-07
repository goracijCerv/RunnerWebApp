using RunnerWebApp.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RunnerWebApp.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl {  get; set; } = string.Empty;
        public Address? Address { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public ClubCategory Category { get; set; }
        public AppUser? AppUser { get; set; }
        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
    }
}
