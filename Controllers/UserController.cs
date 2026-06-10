using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using trainee_management.Enums;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Database;
using trainee_management.Services;
namespace trainee_management.Controllers;


[ApiController]
[Route("api/user")]


public class UserController : ControllerBase
{

    private readonly IUserService _userservice;
    public UserController(IUserService userService)
    {
        _userservice=userService;
    }


    [HttpPost("login")]
    public IActionResult LoginUser(UserLoginDTO request)
    {
        LoginResponse res=_userservice.loginUser(request);
        
        return StatusCode(StatusCodes.Status200OK,res);
    }

    [HttpPost("register")]
    public IActionResult RegisterUser(UserDTO request)
    {   
        _userservice.createUser(request);
        return StatusCode(StatusCodes.Status201Created,new {message="User Created"});
    }
    
}