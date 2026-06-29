using trainee_management.Models.DTOs;

namespace trainee_management.Services;

public interface IUserService
{
    public void CreateUser(UserDTO request);

    public LoginResponse LoginUser(UserLoginDTO request);
}