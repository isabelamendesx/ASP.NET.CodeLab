using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Data.Dtos;
using UsersAPI.Models;
using UsersAPI.Services;

namespace UsersAPI.Controllers;

[ApiController]
[Route("[Controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;

    public UserController(UserService registerService)
    {
        _userService = registerService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(CreateUserDto userDto)
    {
        await _userService.Register(userDto);
        return Ok("User registed!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto userDto)
    {
        var token = await _userService.Login(userDto);
        return Ok(token);
    }
}
