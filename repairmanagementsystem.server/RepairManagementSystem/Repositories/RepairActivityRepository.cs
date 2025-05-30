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

        public async Task<RepairActivity> GetRepairActivityByIdAsync(int repairActivityId)
        {
            var repairActivity = await _context.RepairActivities.FindAsync(repairActivityId);

            if (repairActivity == null)
            {
                throw new KeyNotFoundException($"Repair activity with ID {repairActivityId} not found.");
            }
            else
            {
                return repairActivity;
            }
        }

        public async Task<IEnumerable<RepairActivity>> GetAllRepairActivitiesAsync()
        {
            return await _context.RepairActivities.ToListAsync();
        }

        public async Task AddRepairActivityAsync(RepairActivity repairActivity)
        {
            _context.RepairActivities.Add(repairActivity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRepairActivityAsync(RepairActivity repairActivity)
        {
            _context.RepairActivities.Update(repairActivity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRepairActivityAsync(int repairActivityId)
        {
            var repairActivity = await GetRepairActivityByIdAsync(repairActivityId);

            if (repairActivity != null)
            {
                _context.RepairActivities.Remove(repairActivity);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Repair activity with ID {repairActivityId} not found.");
            }
        }
    }
}
