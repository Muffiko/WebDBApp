using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class RepairActivityTypeRepository : IRepairActivityTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public RepairActivityTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RepairActivityType?> GetRepairActivityTypeByIdAsync(string repairActivityTypeId)
        {
            return await _context.RepairActivityTypes.FindAsync(repairActivityTypeId);
        }

        public async Task<IEnumerable<RepairActivityType?>?> GetAllRepairActivityTypesAsync()
        {
            return await _context.RepairActivityTypes.ToListAsync();
        }

        public async Task<bool> AddRepairActivityTypeAsync(RepairActivityType repairActivityType)
        {
            _context.RepairActivityTypes.Add(repairActivityType);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRepairActivityTypeAsync(RepairActivityType repairActivityType)
        {
            if (repairActivityType != null)
            {
                _context.RepairActivityTypes.Update(repairActivityType);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteRepairActivityTypeAsync(string repairActivityTypeId)
        {
            var repairActivityType = await GetRepairActivityTypeByIdAsync(repairActivityTypeId);

            if (repairActivityType != null)
            {
                _context.RepairActivityTypes.Remove(repairActivityType);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
