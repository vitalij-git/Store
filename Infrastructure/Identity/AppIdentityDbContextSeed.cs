using Core.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any()) 
            {
                var user = new AppUser
                {
                    DisplayName = "Keinas",
                    Email = "Keinas@test.com",
                    UserName = "keinas1",
                    UserInfo = new UserInfo
                    {
                        FirstName = "Keinas",
                        LastName = "Kelsas",
                        City = "Vilnius",
                        Street = "Savanoriu 12",
                    }
                };
                await userManager.CreateAsync(user, "Password1$");
            }

        }
    }
}
