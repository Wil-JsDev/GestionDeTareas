using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionDeTareas.Infrastructure.Identity.Seeds
{
    public static class DefaultAdmin
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Add user default
            User defaultUser = new()
            {
                UserName = "superAdminUser",
                Email = "wilmerjose12@gmail.com",
                FirstName = "Wilmer",
                LastName = "Jose",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser,"123Pa$$word");
                    await userManager.AddToRoleAsync(defaultUser,Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser,Roles.Student.ToString());
                }
            }
        }
    }
}
