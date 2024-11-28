using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateTaskAsync(TaskDTO taskDTO, string userId);     

        Task<TaskResponse> ReadTaskAsync(Guid id, string userId);

        Task<TasksResponse> ReadAllTasksAsync(string userId);

        Task<TaskResponse> UpdateTaskAsync(Guid id, TaskDTO taskDTO, string userId);

        Task<TaskResponse> DeleteTaskAsync(Guid id, string userId);        
    }
}