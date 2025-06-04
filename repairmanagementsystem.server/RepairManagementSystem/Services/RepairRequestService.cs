using AutoMapper;
using RepairManagementSystem.Data;
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

        public RepairRequestService(ApplicationDbContext context, IMapper mapper, IRepairRequestRepository repairRequestRepository)
        {
            _context = context;
            _mapper = mapper;
            _repairRequestRepository = repairRequestRepository;
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

        public async Task<RepairRequest?> AddRepairRequestAsync(RepairRequestDTO repairRequestDTO)
        {
            if (repairRequestDTO == null)
                return null;
            var repairRequest = _mapper.Map<RepairRequest>(repairRequestDTO);
            await _repairRequestRepository.AddRepairRequestAsync(repairRequest);
            return repairRequest;
        }

        public async Task<bool> UpdateRepairRequestAsync(int repairRequestId, RepairRequestDTO repairRequestDTO)
        {
            var updated = _mapper.Map<RepairRequest>(repairRequestDTO);
            updated.RepairRequestId = repairRequestId;
            return await _repairRequestRepository.UpdateRepairRequestAsync(updated);
        }

        public async Task<bool> DeleteRepairRequestAsync(int repairRequestId)
        {
            return await _repairRequestRepository.DeleteRepairRequestAsync(repairRequestId);
        }

        public async Task<RepairRequest?> AddRepairRequestAsync(RepairRequestAdd repairRequestAddDTO)
        {
            if (repairRequestAddDTO == null)
                return null;
            var repairRequest = _mapper.Map<RepairRequest>(repairRequestAddDTO);
            await _repairRequestRepository.AddRepairRequestAsync(repairRequest);
            return repairRequest;
        }

        public async Task<IEnumerable<RepairRequest?>?> GetAllRepairRequestsFromCustomerAsync(int customerId)
        {
            return await _repairRequestRepository.GetAllRepairRequestsFromCustomerAsync(customerId);
        }
    }
}
