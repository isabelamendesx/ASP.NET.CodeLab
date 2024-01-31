using AutoMapper;
using UsersAPI.Data.Dtos;
using UsersAPI.Models;

namespace UsersAPI.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>(); 
    }
}
