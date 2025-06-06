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

        public async Task<RepairRequest?> GetRepairRequestByIdAsync(int repairRequestId)
        {
            return await _context.RepairRequests
                .Include(r => r.RepairObject)
                    .ThenInclude(ro => ro.RepairObjectType)
                .FirstOrDefaultAsync(r => r.RepairRequestId == repairRequestId);
        }

        public async Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsAsync()
        {
            return await _context.RepairRequests
                .Include(r => r.RepairObject)
                    .ThenInclude(ro => ro.RepairObjectType)
                .ToListAsync();
        }

        public async Task<bool> AddRepairRequestAsync(RepairRequest repairRequest)
        {
            _context.RepairRequests.Add(repairRequest);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRepairRequestAsync(RepairRequest repairRequest)
        {
            if (repairRequest != null)
            {
                _context.RepairRequests.Update(repairRequest);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<bool> DeleteRepairRequestAsync(int repairRequestId)
        {
            var repairRequest = await GetRepairRequestByIdAsync(repairRequestId);
            if (repairRequest != null)
            {
                _context.RepairRequests.Remove(repairRequest);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsFromCustomerAsync(int customerId)
        {
            return await _context.RepairRequests
                .Include(r => r.RepairObject)
                    .ThenInclude(ro => ro.RepairObjectType)
                .Where(r => r.RepairObject.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RepairRequest?>?> GetUnassignedRepairRequestsAsync()
        {
            return await _context.RepairRequests
                .Include(r => r.RepairObject)
                    .ThenInclude(ro => ro.RepairObjectType)
                .Where(r => r.ManagerId == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<RepairRequest?>?> GetActiveRepairRequestsAsync()
        {
            return await _context.RepairRequests
                .Include(r => r.RepairObject)
                    .ThenInclude(ro => ro.RepairObjectType)
                .Where(r => r.ManagerId != null && r.Status == "Active")
                .ToListAsync();
        }
    }
}
