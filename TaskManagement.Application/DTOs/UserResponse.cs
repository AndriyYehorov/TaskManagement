namespace TaskManagement.Application.DTOs
{
    public record UserResponse(bool IsSuccess, string? Message = null, UserDTO? UserDTO = null);
}