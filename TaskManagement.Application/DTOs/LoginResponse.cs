namespace TaskManagement.Application.DTOs
{
    public record LoginResponse(bool IsSuccess, string? Message = null, string? Token = null);
}