
using trainee_management.Database;
using trainee_management.Models.DTOs;
using trainee_management.Models.Entities;
using trainee_management.Enums;
using trainee_management.Utils;
using trainee_management.Exceptions;
namespace trainee_management.Services;

public class UserService : IUserService
{

       private readonly AppDBContext _context;
       private readonly IConfiguration _configuration;
       public UserService(AppDBContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration=configuration;
        }

    public void CreateUser(UserDTO request)
    {
        string hashedPassword=PasswordUtils.hashPassword(request.Username,request.password);

        User user=new User
        {
            Username=request.Username,
            Email=request.Email,
            PasswordHash=hashedPassword,
            Role=UserRole.ADMIN,
            createdAt=DateTime.UtcNow,
            updatedAt=DateTime.UtcNow
        };

        _context.User.Add(user);
        _context.SaveChanges();
    }

    public LoginResponse LoginUser(UserLoginDTO request)
    {   
        User? user=_context.User.FirstOrDefault(u=>u.Username==request.Username);
        if (user == null)
        {
            throw new NotFoundException($"User with username {request.Username} not found");
        }
        
        bool verified=PasswordUtils.verifyPassword(user.Username,request.password,user.PasswordHash);
        if (!verified)
        {
            throw new InvalidCredentialsException("Invalid Credentials please try different password");
        }
        
        UserResponse userRes=new UserResponse
        {
          Id=user.Id,
          role=user.Role,
          Username=user.Username  
        };

        LoginResponse res=new LoginResponse
        {
            token=JwtUtils.GenerateToken(user,_configuration),
            expiresIn=_configuration["Jwt:ExpiresIn"]!,
            user=userRes
        };
        return res;
    }


}