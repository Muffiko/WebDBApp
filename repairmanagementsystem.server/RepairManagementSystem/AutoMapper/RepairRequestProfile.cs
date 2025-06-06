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
            CreateMap<RepairRequest, RepairRequestCustomerResponse>()
                .ForMember(dest => dest.Request_Id, opt => opt.MapFrom(src => src.RepairRequestId))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.ManagerId.HasValue
                    ? src.Manager.User.FirstName + " " + src.Manager.User.LastName
                    : null));
        }
    }
}
