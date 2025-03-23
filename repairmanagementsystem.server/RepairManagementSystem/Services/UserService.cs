using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            return await _context.Users
            .Select(u => new UserDTO
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role
            })
            .ToListAsync();
        }

        public async Task<UserDTO?> GetUserAsync(string email, string password)
        {
            /*TODO: implement this method*/
            /*if user exists return it */
            /*else return null*/
            /* mock functionality for now*/
            if (email == "email@tab.com" && password == "password")
            {
                return new UserDTO
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
