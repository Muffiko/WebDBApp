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
            CreateMap<RepairActivity, RepairActivityResponse>();
            CreateMap<RepairActivityResponse, RepairActivity>();
            CreateMap<RepairActivity, RepairActivityRequest>();
            CreateMap<RepairActivityRequest, RepairActivity>();
        }
    }
}
