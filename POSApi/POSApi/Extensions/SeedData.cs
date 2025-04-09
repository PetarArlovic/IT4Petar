using Microsoft.AspNetCore.Identity;
using POSApi.Domain.Models;

namespace POSApi.Extensions
{
    public static class SeedDataExtensions
    {
        public static async Task SeedData(this IHost webHost) 
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    var role = new IdentityRole("Admin");
                    await roleManager.CreateAsync(role);
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
}
