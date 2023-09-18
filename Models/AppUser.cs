namespace RunnerWebApp.Models
{
    public class AppUser
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public Address? Address { get; set; }
        public ICollection<Club>? Clubs { get; set; }
        public ICollection<Races>? Races { get; set; }

    }
}
