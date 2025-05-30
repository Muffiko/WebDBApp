using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;

namespace RepairManagementSystem.Repositories
{
    public class RepairRequestRepository : IRepairRequestRepository
    {
        private readonly ApplicationDbContext _context;

        public RepairRequestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RepairRequest> GetRepairRequestByIdAsync(int repairRequestId)
        {
            var repairRequest = await _context.RepairRequests.FindAsync(repairRequestId);

            if (repairRequest == null)
            {
                throw new KeyNotFoundException($"Repair request with ID {repairRequestId} not found.");
            }
            else
            {
                return repairRequest;
            }
        }

        public async Task<IEnumerable<RepairRequest>> GetAllRepairRequestsAsync()
        {
            return await _context.RepairRequests.ToListAsync();
        }

        public async Task AddRepairRequestAsync(RepairRequest repairRequest)
        {
            _context.RepairRequests.Add(repairRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRepairRequestAsync(RepairRequest repairRequest)
        {
            _context.RepairRequests.Update(repairRequest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRepairRequestAsync(int repairRequestId)
        {
            var repairRequest = await GetRepairRequestByIdAsync(repairRequestId);

            if (repairRequest != null)
            {
                _context.RepairRequests.Remove(repairRequest);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Repair request with ID {repairRequestId} not found.");
            }
        }
    }
}
