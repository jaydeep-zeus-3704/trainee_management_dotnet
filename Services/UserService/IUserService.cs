using trainee_management.Models.DTOs;

namespace trainee_management.Services;

public interface IUserService
{
    public void createUser(UserDTO request);

    public LoginResponse loginUser(UserLoginDTO request);
}