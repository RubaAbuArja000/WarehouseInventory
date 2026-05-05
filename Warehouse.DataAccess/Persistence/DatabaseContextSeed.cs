using Warehouse.Core.Entities;

namespace Warehouse.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    public static async Task SeedAsync(DatabaseContext context)
    {
        if (!context.Users.Any(u => u.Email == "admin@happywarehouse.com"))
        {
            context.Users.Add(new User
            {
                Email = "admin@happywarehouse.com",
                FullName = "System Admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd"),
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            });

            await context.SaveChangesAsync();
        }
    }
}
