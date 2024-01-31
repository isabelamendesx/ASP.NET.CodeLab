using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsersAPI.Data.Dtos;
using UsersAPI.Models;

namespace UsersAPI.Services;

public class RegisterService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;

    public RegisterService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task Register(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded) throw new ApplicationException("Failed to register user :(");

    }
}
