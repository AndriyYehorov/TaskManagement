using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<User>> RegisterUserAsync(UserDTO userDTO);

        Task<ServiceResponse<string>> LoginUserAsync(LoginDTO loginDTO);
    }
}