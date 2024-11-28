using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.DTOs
{
    public record TasksResponse(bool IsSuccess, string? Message = null, IEnumerable<TaskItem>? Tasks = null);
}
