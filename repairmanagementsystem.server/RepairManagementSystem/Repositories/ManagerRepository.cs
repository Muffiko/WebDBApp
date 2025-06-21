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

        public async Task<Manager?> GetManagerByIdAsync(int managerId)
        {
            return await _context.Managers.FindAsync(managerId);
        }

        public async Task<IEnumerable<Manager?>?> GetAllManagersAsync()
        {
            return await _context.Managers
                .Include(m => m.User)
                .ToListAsync();
        }

        public async Task<bool> AddManagerAsync(Manager manager)
        {
            _context.Managers.Add(manager);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateManagerAsync(Manager manager)
        {
            if (manager != null)
            {
                _context.Managers.Update(manager);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteManagerAsync(int managerId)
        {
            var manager = await GetManagerByIdAsync(managerId);
            if (manager != null)
            {
                _context.Managers.Remove(manager);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
