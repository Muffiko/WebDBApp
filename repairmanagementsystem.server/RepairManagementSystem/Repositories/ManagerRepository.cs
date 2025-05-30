using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Manager> GetManagerByIdAsync(int managerId)
        {
            var manager = await _context.Managers.FindAsync(managerId);

            if (manager == null)
            {
                throw new KeyNotFoundException($"Manager with ID {managerId} not found.");
            }
            else
            {
                return manager;
            }
        }

        public async Task<IEnumerable<Manager>> GetAllManagersAsync()
        {
            return await _context.Managers.ToListAsync();
        }

        public async Task AddManagerAsync(Manager manager)
        {
            _context.Managers.Add(manager);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateManagerAsync(Manager manager)
        {
            _context.Managers.Update(manager);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteManagerAsync(int managerId)
        {
            var manager = await GetManagerByIdAsync(managerId);

            if (manager != null)
            {
                _context.Managers.Remove(manager);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Manager with ID {managerId} not found.");
            }
        }
    }
}
