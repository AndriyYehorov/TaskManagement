using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Authentication;

namespace TaskManagement.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtOptions _options;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IOptions<JwtOptions> options, ILogger<UserService> logger)
        {
            _userRepository = userRepository;           
            _options = options.Value;
            _logger = logger;
        }

        public async Task<ServiceResponse<string>> LoginUserAsync(LoginDTO loginDTO)
        {
            var existingUser = await _userRepository.GetUserByUsernameOrEmailAsync(loginDTO.UsernameOrEmail);           

            if (existingUser == null)
            {
                return new ServiceResponse<string>(Result: ServiceResult.NotFound, Message: "User with this Username/Email doesn`t exist");
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDTO.Password, existingUser.PasswordHash))
            {
                return new ServiceResponse<string>(Result: ServiceResult.Error, Message: "Wrong password");
            }
           
            return new ServiceResponse<string>(Result: ServiceResult.Success, Message: "You logged in successfully", Data: GenerateJWT(existingUser));
        }

        public async Task<ServiceResponse<User>> RegisterUserAsync(UserDTO userDTO)
        {
            var existingUserByUsername = await _userRepository.GetUserByUsernameAsync(userDTO.Username);

            var existingUserByEmail = await _userRepository.GetUserByEmailAsync(userDTO.Email);

            if (existingUserByUsername != null || 
                existingUserByEmail != null)
            {
                return new ServiceResponse<User>(Result: ServiceResult.Error, Message: "User with the same username or email already exists");
            }

            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Username = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(newUser);            

            _logger.LogInformation("User was created successfully. UserId: {UserId}.", newUser.Id);

            return new ServiceResponse<User>(Result: ServiceResult.Success, Message: "User was created successfully", Data: newUser);
        }

        private string GenerateJWT(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),                
            };

            var token = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.UtcNow.AddHours(_options.ExpiredHours),                
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
