using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.DTOs
{
    public record TaskResponse(bool IsSuccess, string? Message = null, TaskItem? Task = null);    
}