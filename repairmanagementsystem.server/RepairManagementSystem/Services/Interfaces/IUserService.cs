using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDTO?> GetUserAsync(string email, string password);

        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
    }
}
