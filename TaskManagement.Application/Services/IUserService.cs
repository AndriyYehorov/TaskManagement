using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services
{
    public interface IUserService
    {
        Task<UserResponse> RegisterUserAsync(UserDTO userDTO);

        Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
    }
}