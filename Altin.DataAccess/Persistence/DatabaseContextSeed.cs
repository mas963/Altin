using Microsoft.AspNetCore.Identity;

namespace Altin.DataAccess;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser
            {
                UserName = "admin",
                Email = "admin@localhost",
                EmailConfirmed = true
            };
            
            await userManager.CreateAsync(user, "123456789Yasor.");
        }

        await context.SaveChangesAsync();
    }
}
