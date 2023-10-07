using Microsoft.EntityFrameworkCore;
using RunnerWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace RunnerWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Races> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
