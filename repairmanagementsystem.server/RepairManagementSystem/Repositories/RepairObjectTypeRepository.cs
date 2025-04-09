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

        public async Task<RepairObjectType> GetRepairObjectTypeByIdAsync(int repairObjectTypeId)
        {
            var repairObjectType = await _context.RepairObjectTypes.FindAsync(repairObjectTypeId);

            if (repairObjectType == null)
            {
                throw new KeyNotFoundException($"Repair object type with ID {repairObjectTypeId} not found.");
            }
            else
            {
                return repairObjectType;
            }
        }

        public async Task<IEnumerable<RepairObjectType>> GetAllRepairObjectTypesAsync()
        {
            return await _context.RepairObjectTypes.ToListAsync();
        }

        public async Task AddRepairObjectTypeAsync(RepairObjectType repairObjectType)
        {
            _context.RepairObjectTypes.Add(repairObjectType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRepairObjectTypeAsync(RepairObjectType repairObjectType)
        {
            _context.RepairObjectTypes.Update(repairObjectType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRepairObjectTypeAsync(int repairObjectTypeId)
        {
            var repairObjectType = await GetRepairObjectTypeByIdAsync(repairObjectTypeId);

            if (repairObjectType != null)
            {
                _context.RepairObjectTypes.Remove(repairObjectType);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Repair object type with ID {repairObjectTypeId} not found.");
            }
        }
    }
}
