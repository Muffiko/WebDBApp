using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IManagerRepository
    {
        Task<Manager?> GetManagerByIdAsync(int managerId);
        Task<IEnumerable<Manager>> GetAllManagersAsync();
        Task AddManagerAsync(Manager manager);
        Task UpdateManagerAsync(Manager manager);
        Task DeleteManagerAsync(int managerId);
    }
}
