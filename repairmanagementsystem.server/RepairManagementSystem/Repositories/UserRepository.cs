using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserAsync(string email, string password)
        {
            /*TODO: implement this method*/
            /*if user exists return it */
            /*else return null*/
            /* mock functionality for now*/
            if (email == "email@tab.com" && password == "password")
            {
                return new User
                {
                    Email = email,
                    FirstName = "username",
                    Role = "role"
                };
            }
            else
            {
                return null;
            }
        }
    }
}
