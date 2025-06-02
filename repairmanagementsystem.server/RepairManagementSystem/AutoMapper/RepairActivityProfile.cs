using AutoMapper;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.AutoMapper
{
    public class RepairActivityProfile : Profile
    {
        public RepairActivityProfile()
        {
            CreateMap<RepairActivity, RepairActivityDTO>();
            CreateMap<RepairActivityDTO, RepairActivity>();
        }
    }
}
