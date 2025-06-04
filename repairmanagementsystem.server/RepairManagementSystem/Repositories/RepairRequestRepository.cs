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
            return await _context.RepairRequests.FindAsync(repairRequestId);
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
            if (repairRequest != null)
            {
                _context.RepairRequests.Update(repairRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRepairRequestAsync(int repairRequestId)
        {
            var repairRequest = await GetRepairRequestByIdAsync(repairRequestId);
            if (repairRequest != null)
            {
                _context.RepairRequests.Remove(repairRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<RepairRequest>> GetAllRepairObjectsFromCustomerAsync(int customerId)
        {
            return await _context.RepairRequests
                .Include(rr => rr.RepairObject)
                .Where(rr => rr.RepairObject.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
