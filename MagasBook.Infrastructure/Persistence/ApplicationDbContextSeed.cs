using System.Linq;
using System.Threading.Tasks;
using MagasBook.Application.Common.Constants;
using MagasBook.Domain.Entities.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace MagasBook.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SetDefaultDataAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            if (!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!userManager.Users.Any())
            {
                var user = new ApplicationUser
                {
                    UserName = "Admin",
                    Email = "admin@yandex.ru"
                };

                await userManager.CreateAsync(user, "admin123");
                await userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
        } 
    }
}