using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.DTOs
{
    public record UserResponse(bool IsSuccess, string? Message = null, User? User = null);
}