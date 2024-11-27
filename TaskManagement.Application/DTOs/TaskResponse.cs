namespace TaskManagement.Application.DTOs
{
    public record TaskResponse(bool IsSuccess, string? Message = null, Domain.Entities.Task? Task = null);  
}
