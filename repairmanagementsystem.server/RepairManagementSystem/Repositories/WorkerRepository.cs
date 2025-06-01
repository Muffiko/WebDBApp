using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Worker> GetWorkerByIdAsync(int workerId)
        {
            return await _context.Workers.FindAsync(workerId);
        }

        public async Task<IEnumerable<Worker>> GetAllWorkersAsync()
        {
            return await _context.Workers.ToListAsync();
        }

        public async Task AddWorkerAsync(Worker worker)
        {
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWorkerAsync(Worker worker)
        {
            if (worker == null)
            {
                return;
            }
            _context.Workers.Update(worker);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkerAsync(int workerId)
        {
            var worker = await GetWorkerByIdAsync(workerId);
            if (worker == null)
            {
                return;
            }
            _context.Workers.Remove(worker);
            await _context.SaveChangesAsync();
        }
    }
}
