using AutoMapper;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.AutoMapper
{
    public class RepairObjectTypeProfile : Profile
    {
        public RepairObjectTypeProfile()
        {
            CreateMap<RepairObjectType, RepairObjectTypeDTO>();
            CreateMap<RepairObjectTypeDTO, RepairObjectType>();
        }
    }
}
