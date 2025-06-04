using AutoMapper;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.AutoMapper
{
    public class RepairRequestProfile : Profile
    {
        public RepairRequestProfile()
        {
            CreateMap<RepairRequest, RepairRequestDTO>();
            CreateMap<RepairRequestDTO, RepairRequest>();
            CreateMap<RepairRequest, RepairRequestAddDTO>();
            CreateMap<RepairRequestAddDTO, RepairRequest>();
        }
    }
}
