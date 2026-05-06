using FitnessApp.Modules.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace FitnessApp.Modules.Identity.Infrastructure;

public static class IdentitySeeder
{
    public static async Task SeedAsync(RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        string[] roleNames = { "Admin", "User" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new Role { Name = roleName });
            }
        }

        var adminEmail = "admin@gmail.com";
        var user = await userManager.FindByEmailAsync(adminEmail);
        if (user is null)
        {
            var admin = new User { UserName = "Admin", Email = adminEmail };
            var result = await userManager.CreateAsync(admin, "AdminPassword1");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
        
        
    }
}