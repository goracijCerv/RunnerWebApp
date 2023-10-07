using Microsoft.AspNetCore.Identity;
using RunnerWebApp.Data.Enum;
using RunnerWebApp.Models;

namespace RunnerWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Running Club 1",
                            Description = "This is the description of the first Running Club",
                            ImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Category = ClubCategory.City,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                PostalCode = 231,
                                City = "Charlotte",
                                State = "NC",
                                Country = "EEUU"
                            }
                         },
                        new Club()
                        {
                            Title = "Running Club 2",
                            Description = "This is the description of the second Running Club",
                            ImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Category = ClubCategory.Endurance,
                            Address = new Address()
                            {
                                Street = "1231 Santa Elena",
                                PostalCode = 321,
                                City = "Monterrey",
                                State = "Nuevo Leon",
                                Country="MEX"
                            }
                        },
                        new Club()
                        {
                            Title = "Running Club 3",
                            Description = "This is the description of the third  Running club",
                            ImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Category = ClubCategory.Trail,
                            Address = new Address()
                            {
                                Street = "2321 White St",
                                City = "Los Angeles",
                                State = "California",
                                Country="EEUU"
                            }
                        },
                        new Club()
                        {
                            Title = "Running Club 4",
                            Description = "This is the description of the fourth Running club",
                            ImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Category = ClubCategory.City,
                            Address = new Address()
                            {
                                Street = "Baja California 123",
                                PostalCode = 1831,
                                City = "Cosio",
                                State = "Aguascalientes",
                                Country = "MEX"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Races
                if (!context.Races.Any())
                {
                    context.Races.AddRange(new List<Races>()
                    {
                        new Races()
                        {
                            Title = "Running Race 1",
                            ImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the first race",
                            RaceCategory = RaceCategory.Marthon,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                PostalCode= 213,
                                City = "Charlotte",
                                State = "NC",
                                Country ="EEUU"
                            }
                        },
                        new Races()
                        {
                            Title = "Running Race 2",
                            ImageUrl = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "This is the description of the second race",
                            RaceCategory = RaceCategory.IronMan,
                            Address = new Address()
                            {
                                Street = "1231 Santa Elena",
                                PostalCode = 321,
                                City = "Monterrey",
                                State = "Nuevo Leon",
                                Country="MEX"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "goracijpcervantilia@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "horasdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "123 Main St",
                            City = "Charlotte",
                            State = "NC"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
