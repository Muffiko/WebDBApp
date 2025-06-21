using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class RepairActivityRepository : IRepairActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public RepairActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RepairActivity?> GetRepairActivityByIdAsync(int repairActivityId)
        {
            return await _context.RepairActivities.FindAsync(repairActivityId);
        }

        public async Task<IEnumerable<RepairActivity?>?> GetAllRepairActivitiesAsync()
        {
            return await _context.RepairActivities
                .Include(ra => ra.RepairActivityType)
                .ToListAsync();
        }

        public async Task<bool> AddRepairActivityAsync(RepairActivity repairActivity)
        {
            _context.RepairActivities.Add(repairActivity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRepairActivityAsync(RepairActivity repairActivity)
        {
            var existing = await GetRepairActivityByIdAsync(repairActivity.RepairActivityId);
            if (existing == null)
                return false;
            _context.Entry(existing).CurrentValues.SetValues(repairActivity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteRepairActivityAsync(int repairActivityId)
        {
            var repairActivity = await GetRepairActivityByIdAsync(repairActivityId);
            if (repairActivity != null)
            {
                _context.RepairActivities.Remove(repairActivity);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
