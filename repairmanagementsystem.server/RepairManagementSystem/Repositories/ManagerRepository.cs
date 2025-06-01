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
            return await _context.Managers.FindAsync(managerId);
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
            if (manager == null)
            {
                return;
            }
            _context.Managers.Update(manager);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteManagerAsync(int managerId)
        {
            var manager = await GetManagerByIdAsync(managerId);
            if (manager == null)
            {
                return;
            }
            _context.Managers.Remove(manager);
            await _context.SaveChangesAsync();
        }
    }
}
