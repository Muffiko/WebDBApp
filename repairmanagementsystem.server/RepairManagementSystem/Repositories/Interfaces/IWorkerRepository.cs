using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IWorkerRepository
    {
        Task<Worker?> GetWorkerByIdAsync(int workerId);
        Task<IEnumerable<Worker?>?> GetAllWorkersAsync();
        Task<bool> AddWorkerAsync(Worker worker);
        Task<bool> UpdateWorkerAsync(Worker worker);
        Task<bool> DeleteWorkerAsync(int workerId);
    }
}
