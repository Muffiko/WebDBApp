using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserAsync(string email, string password);
    }
}
