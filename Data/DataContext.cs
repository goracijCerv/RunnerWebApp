using Microsoft.EntityFrameworkCore;
using RunnerWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace RunnerWebApp.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Races> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }

    }
}
