using BlossomHotel.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;

namespace BlossomHotel.WebUI.Identity
{
    public class IdentityInitializer
    {
        public static async Task SeedAdminAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            string adminEmail = "admin@blossom.com";
            string adminPassword = "Admin123!";

            string[] roles = { "Admin", "Musteri", "Calisan" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole { Name = role });
                }
            }

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                AppUser user = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }


    }
}
