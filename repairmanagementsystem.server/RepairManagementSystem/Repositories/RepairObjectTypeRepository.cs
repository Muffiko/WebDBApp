using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class RepairObjectTypeRepository : IRepairObjectTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public RepairObjectTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RepairObjectType?> GetRepairObjectTypeByIdAsync(string repairObjectTypeId)
        {
            return await _context.RepairObjectTypes.FindAsync(repairObjectTypeId);
        }

        public async Task<IEnumerable<RepairObjectType?>?> GetAllRepairObjectTypesAsync()
        {
            return await _context.RepairObjectTypes.ToListAsync();
        }

        public async Task<bool> AddRepairObjectTypeAsync(RepairObjectType repairObjectType)
        {
            var existingType = await _context.RepairObjectTypes.FindAsync(repairObjectType.RepairObjectTypeId);
            if (existingType != null)
                return false;
            _context.RepairObjectTypes.Add(repairObjectType);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRepairObjectTypeAsync(RepairObjectType repairObjectType)
        {
            if (repairObjectType != null)
            {
                _context.RepairObjectTypes.Update(repairObjectType);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteRepairObjectTypeAsync(string repairObjectTypeId)
        {
            var repairObjectType = await GetRepairObjectTypeByIdAsync(repairObjectTypeId);

            if (repairObjectType != null)
            {
                _context.RepairObjectTypes.Remove(repairObjectType);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
