using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsersAPI.Data.Dtos;
using UsersAPI.Models;

namespace UsersAPI.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signInManager;
    private TokenService _tokenService;

    public UserService(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, TokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }
    public async Task Register(CreateUserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (!result.Succeeded) throw new ApplicationException("Failed to register user :(");

    }

    public async Task<string> Login(LoginUserDto userDto)
    {
        var result = await _signInManager.PasswordSignInAsync(userDto.Username, userDto.Password, false, false);

        if (!result.Succeeded) throw new ApplicationException("User not autenthicated!");

        var user = _signInManager.UserManager
            .Users
            .FirstOrDefault(user => user.NormalizedUserName == userDto.Username);

        return _tokenService.GenerateToken(user);
    }
}
