using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserAsync(string email, string password);
    }
}
