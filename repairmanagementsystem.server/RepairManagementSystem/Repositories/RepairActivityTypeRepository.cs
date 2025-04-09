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

        public async Task<RepairActivityType> GetRepairActivityTypeByIdAsync(int repairActivityTypeId)
        {
            var repairActivityType = await _context.RepairActivityTypes.FindAsync(repairActivityTypeId);

            if (repairActivityType == null)
            {
                throw new KeyNotFoundException($"Repair activity type with ID {repairActivityTypeId} not found.");
            }
            else
            {
                return repairActivityType;
            }
        }

        public async Task<IEnumerable<RepairActivityType>> GetAllRepairActivityTypesAsync()
        {
            return await _context.RepairActivityTypes.ToListAsync();
        }

        public async Task AddRepairActivityTypeAsync(RepairActivityType repairActivityType)
        {
            _context.RepairActivityTypes.Add(repairActivityType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRepairActivityTypeAsync(RepairActivityType repairActivityType)
        {
            _context.RepairActivityTypes.Update(repairActivityType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRepairActivityTypeAsync(int repairActivityTypeId)
        {
            var repairActivityType = await GetRepairActivityTypeByIdAsync(repairActivityTypeId);

            if (repairActivityType != null)
            {
                _context.RepairActivityTypes.Remove(repairActivityType);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Repair activity type with ID {repairActivityTypeId} not found.");
            }
        }
    }
}
