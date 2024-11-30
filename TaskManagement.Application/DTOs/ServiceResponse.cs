namespace TaskManagement.Application.DTOs
{
    public record ServiceResponse<T>(bool IsSuccess, string? Message = null, T? Data = default);    
}
