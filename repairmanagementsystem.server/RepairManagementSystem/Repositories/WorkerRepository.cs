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

        public async Task<Worker?> GetWorkerByIdAsync(int workerId)
        {
            return await _context.Workers.FindAsync(workerId);
        }

        public async Task<IEnumerable<Worker?>?> GetAllWorkersAsync()
        {
            return await _context.Workers
                .Include(w => w.User)
                .ToListAsync();
        }

        public async Task<bool> AddWorkerAsync(Worker worker)
        {
            _context.Workers.Add(worker);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateWorkerAsync(Worker worker)
        {
            if (worker != null)
            {
                _context.Workers.Update(worker);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteWorkerAsync(int workerId)
        {
            var worker = await GetWorkerByIdAsync(workerId);
            if (worker != null)
            {
                _context.Workers.Remove(worker);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
