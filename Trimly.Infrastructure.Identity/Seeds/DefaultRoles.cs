using GestionDeTareas.Core.Domain.Enum;
using GestionDeTareas.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace GestionDeTareas.Infrastructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Estos serian los roles por default que tendra la app
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Student.ToString()));
        }
    }
}
