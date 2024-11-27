using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services
{
    public interface IUserService
    {
        public Task<UserResponse> RegisterUserAsync(UserDTO userDTO);

        public Task<LoginResponse> LoginUserAsync(LoginDTO loginDTO);
    }
}