using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class RepairObjectRepository : IRepairObjectRepository
    {
        private readonly ApplicationDbContext _context;

        public RepairObjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RepairObject?> GetRepairObjectByIdAsync(int repairObjectId)
        {
            return await _context.RepairObjects.FindAsync(repairObjectId);
        }

        public async Task<IEnumerable<RepairObject?>> GetAllRepairObjectsAsync()
        {
            return await _context.RepairObjects
                .Include(ro => ro.RepairObjectType)
                .ToListAsync();
        }

        public async Task<bool> AddRepairObjectAsync(RepairObject repairObject)
        {
            _context.RepairObjects.Add(repairObject);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRepairObjectAsync(RepairObject repairObject)
        {
            var existing = await GetRepairObjectByIdAsync(repairObject.RepairObjectId);
            if (existing == null) return false;
            _context.Entry(existing).CurrentValues.SetValues(repairObject);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRepairObjectAsync(int repairObjectId)
        {
            var repairObject = await GetRepairObjectByIdAsync(repairObjectId);
            if (repairObject != null)
            {
                _context.RepairObjects.Remove(repairObject);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<RepairObject?>> GetAllRepairObjectsFromCustomerAsync(int customerId)
        {
            return await _context.RepairObjects
                .Where(r => r.CustomerId == customerId)
                .Include(r => r.RepairObjectType)
                .ToListAsync();
        }
    }
}
