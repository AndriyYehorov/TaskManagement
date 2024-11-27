namespace TaskManagement.Application.DTOs
{
    public record TasksResponse(bool IsSuccess, string? Message = null, IEnumerable<Domain.Entities.Task>? Task = null);
}
