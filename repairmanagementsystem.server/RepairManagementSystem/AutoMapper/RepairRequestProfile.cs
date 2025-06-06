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
            CreateMap<RepairRequest, RepairRequestAdd>();
            CreateMap<RepairRequestAdd, RepairRequest>();
            CreateMap<RepairRequest, RepairRequestResponse>()
                .ForMember(dest => dest.RepairObjectName, opt => opt.MapFrom(src => src.RepairObject.Name))
                .ForMember(dest => dest.RepairObjectType, opt => opt.MapFrom(src => src.RepairObject.RepairObjectType));
            CreateMap<RepairRequestResponse, RepairRequest>();
        }
    }
}
