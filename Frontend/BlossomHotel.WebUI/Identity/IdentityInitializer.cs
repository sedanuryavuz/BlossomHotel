using BlossomHotel.EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;

namespace BlossomHotel.WebUI.Identity
{
    public class IdentityInitializer
    {
        public static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            string adminEmail = "admin@blossom.com";
            string adminPassword = "Admin123!";

            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                AppUser user = new AppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, adminPassword);
            }
        }
    }
}