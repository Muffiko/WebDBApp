using AutoMapper;
using RepairManagementSystem.Models;
using RepairManagementSystem.Models.DTOs;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
        CreateMap<Address, AddressDTO>();
        CreateMap<AddressDTO, Address>();
        CreateMap<User, UserMyInfoResponse>()
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Number))
            .ForMember(dest => dest.LastLoginAt, opt => opt.MapFrom(src => src.LastLoginAt))
            .ForMember(dest => dest.AccountCreated, opt => opt.MapFrom(src => src.CreatedAt));
        CreateMap<Worker, WorkerDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.WorkerId, opt => opt.MapFrom(src => src.UserId));
        CreateMap<Manager, ManagerDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(src => src.UserId));
        CreateMap<Customer, CustomerDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId));
    }
}

