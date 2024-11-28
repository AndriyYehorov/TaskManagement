using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);

        Task<User?> GetUserByUsernameAsync(string username);

        Task<User?> GetUserByEmailAsync(string email);

        Task<User?> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
    }
}
