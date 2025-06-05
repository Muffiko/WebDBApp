using AutoMapper;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.AutoMapper
{
    public class RepairActivityTypeProfile : Profile
    {
        public RepairActivityTypeProfile()
        {
            CreateMap<RepairActivityType, RepairActivityTypeDTO>();
            CreateMap<RepairActivityTypeDTO, RepairActivityType>();
        }
    }
}
