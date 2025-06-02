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
    }
}

