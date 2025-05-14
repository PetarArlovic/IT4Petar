using Microsoft.AspNetCore.Identity;
using POSApi.Domain.Models;

namespace POSApi.Infrastructure.Extensions
{
    public static class SeedDataExtensions
    {
        public static async Task SeedData(IServiceProvider service) 
        {
           
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var adminUser = await userManager.FindByEmailAsync("admin@gmail.com");

            if (adminUser == null)
            {
                adminUser = new User
                {

                    UserName = "admin",
                    Email = "admin@gmail.com",
                    Ime = "Admin",
                    Prezime = "Admirovic"

                };

                var result = await userManager.CreateAsync(adminUser, "Adminadmin123!");

                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(adminUser, "Admin");

                }
            }
        }
    }
}
