using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IManagerRepository
    {
        Task<Manager?> GetManagerByIdAsync(int managerId);
        Task<IEnumerable<Manager?>?> GetAllManagersAsync();
        Task<bool> AddManagerAsync(Manager manager);
        Task<bool> UpdateManagerAsync(Manager manager);
        Task<bool> DeleteManagerAsync(int managerId);
    }
}
