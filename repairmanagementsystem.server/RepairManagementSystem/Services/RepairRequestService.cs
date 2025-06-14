using AutoMapper;
using RepairManagementSystem.Data;
using RepairManagementSystem.Helpers;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Repositories;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class RepairRequestService : IRepairRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRepairRequestRepository _repairRequestRepository;
        private readonly IRepairObjectRepository _repairObjectRepository;

        public RepairRequestService(ApplicationDbContext context, IMapper mapper, IRepairRequestRepository repairRequestRepository, IRepairObjectRepository repairObjectRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairRequestRepository = repairRequestRepository;
            _repairObjectRepository = repairObjectRepository;
        }

        public async Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsAsync()
        {
            var repairRequests = await _repairRequestRepository.GetAllRepairRequestsAsync();
            return _mapper.Map<IEnumerable<RepairRequest?>?>(repairRequests);
        }

        public async Task<RepairRequest?> GetRepairRequestByIdAsync(int repairRequestId)
        {
            var repairRequest = await _repairRequestRepository.GetRepairRequestByIdAsync(repairRequestId);
            return _mapper.Map<RepairRequest?>(repairRequest);
        }

        public async Task<Result> AddRepairRequestAsync(RepairRequestAdd request)
        {
            var repairRequest = _mapper.Map<RepairRequest>(request);
            repairRequest.CreatedAt = DateTime.UtcNow;
            repairRequest.Status = "Open";
            repairRequest.IsPaid = false;
            repairRequest.RepairObjectId = request.RepairObjectId;
            var repairObject = await _repairObjectRepository.GetRepairObjectByIdAsync(request.RepairObjectId);
            if (repairObject == null)
            {
                return Result.Fail(404, "Repair object not found.");
            }
            repairRequest.RepairObject = repairObject;
            await _repairRequestRepository.AddRepairRequestAsync(repairRequest);
            return Result.Ok("Repair request added successfully.");
        }

        public async Task<Result> UpdateRepairRequestAsync(int repairRequestId, RepairRequestDTO repairRequestDTO)
        {
            var updated = _mapper.Map<RepairRequest>(repairRequestDTO);
            updated.RepairRequestId = repairRequestId;
            var success = await _repairRequestRepository.UpdateRepairRequestAsync(updated);
            if (!success)
            {
                return Result.Fail(404, $"Repair request with ID {repairRequestId} not found.");
            }
            return Result.Ok($"Repair request with ID {repairRequestId} updated successfully.");
        }

        public async Task<Result> DeleteRepairRequestAsync(int repairRequestId)
        {
            var success = await _repairRequestRepository.DeleteRepairRequestAsync(repairRequestId);
            if (!success)
            {
                return Result.Fail(404, $"Repair request with ID {repairRequestId} not found.");
            }
            return Result.Ok($"Repair request with ID {repairRequestId} deleted successfully.");
        }

        public async Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsFromCustomerAsync(int customerId)
        {
            return await _repairRequestRepository.GetAllRepairRequestsFromCustomerAsync(customerId);
        }

        public async Task<IEnumerable<RepairRequestResponse?>?> GetUnassignedRepairRequestsAsync()
        {
            var repairRequest = await _repairRequestRepository.GetUnassignedRepairRequestsAsync();
            return _mapper.Map<IEnumerable<RepairRequestResponse?>?>(repairRequest);
        }

        public async Task<IEnumerable<RepairRequestResponse?>?> GetActiveRepairRequestsAsync()
        {
            var repairRequest = await _repairRequestRepository.GetActiveRepairRequestsAsync();
            return _mapper.Map<IEnumerable<RepairRequestResponse?>?>(repairRequest);
        }

        public async Task<IEnumerable<RepairRequestCustomerResponse?>?> GetAllRepairRequestsForCustomerAsync(int customerId)
        {
            var repairRequests = await _repairRequestRepository.GetAllRepairRequestsFromCustomerAsync(customerId);
            return _mapper.Map<IEnumerable<RepairRequestCustomerResponse?>?>(repairRequests);
        }
    }
}
