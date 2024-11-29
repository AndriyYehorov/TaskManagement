using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateTaskAsync(TaskDTO taskDTO, string userId);     

        Task<TaskResponse> ReadTaskAsync(string id, string userId);

        Task<TasksResponse> ReadAllTasksAsync(
            TaskFilter? filter,
            string? sortColumn, 
            string? sortOrder, 
            int page, 
            int pageSize, 
            string userId);

        Task<TaskResponse> UpdateTaskAsync(string id, TaskDTO taskDTO, string userId);

        Task<TaskResponse> DeleteTaskAsync(string id, string userId);        
    }
}