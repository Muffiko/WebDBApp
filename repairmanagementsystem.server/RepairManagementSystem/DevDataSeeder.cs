using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem
{
    public static class DevDataSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider services)
        {
            var userRepo = services.GetRequiredService<IUserRepository>();
            var crypto = services.GetRequiredService<ICryptoService>();
            var context = services.GetRequiredService<ApplicationDbContext>();

            var (hash, salt) = crypto.HashPassword("admin12345");
            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@tab.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Number = "123456789",
                Address = new Address
                {
                    Country = "PL",
                    City = "TabCity",
                    PostalCode = "00-001",
                    Street = "Tab Street",
                    HouseNumber = "1",
                    ApartNumber = "1A"
                },
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };
            await userRepo.AddUserAsync(adminUser);
        }
    }
}
