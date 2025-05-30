using RepairManagementSystem.Models;

namespace RepairManagementSystem.Repositories.Interfaces
{
    public interface IWorkerRepository
    {
        Task<Worker> GetWorkerByIdAsync(int workerId);
        Task<IEnumerable<Worker>> GetAllWorkersAsync();
        Task AddWorkerAsync(Worker worker);
        Task UpdateWorkerAsync(Worker worker);
        Task DeleteWorkerAsync(int workerId);
    }
}
