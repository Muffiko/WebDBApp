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

        public async Task<RepairObject> GetRepairObjectByIdAsync(int repairObjectId)
        {
            var repairObject = await _context.RepairObjects.FindAsync(repairObjectId);

            if (repairObject == null)
            {
                return null;
            }
            else
            {
                return repairObject;
            }
        }

        public async Task<IEnumerable<RepairObject>> GetAllRepairObjectsAsync()
        {
            return await _context.RepairObjects.ToListAsync();
        }

        public async Task AddRepairObjectAsync(RepairObject repairObject)
        {
            _context.RepairObjects.Add(repairObject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRepairObjectAsync(RepairObject repairObject)
        {
            if (repairObject != null)
            {
                _context.RepairObjects.Update(repairObject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRepairObjectAsync(int repairObjectId)
        {
            var repairObject = await GetRepairObjectByIdAsync(repairObjectId);
            if (repairObject != null)
            {
                _context.RepairObjects.Remove(repairObject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RepairObject>> GetAllRepairObjectsFromCustomerAsync(int customerId)
        {
            return await _context.RepairObjects
                .Where(ro => ro.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
