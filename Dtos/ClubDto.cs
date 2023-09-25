using RunnerWebApp.Data.Enum;
using RunnerWebApp.Models;

namespace RunnerWebApp.Dtos
{
    public class ClubDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IFormFile Image { get; set; } 
        public Address Address { get; set; }
        public ClubCategory Category { get; set; }
    }
}
