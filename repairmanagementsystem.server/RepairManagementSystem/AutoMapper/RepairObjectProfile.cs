using AutoMapper;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

namespace RepairManagementSystem.AutoMapper
{
    public class RepairObjectProfile : Profile
    {
        public RepairObjectProfile()
        {
            CreateMap<RepairObject, RepairObjectRequest>();
            CreateMap<RepairObjectRequest, RepairObject>();
        }
    }
}
