using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateTaskAsync(TaskDTO taskDTO, string userId);     

        Task<TaskResponse> ReadTaskAsync(string id, string userId);

        Task<TasksResponse> ReadAllTasksAsync(string userId);

        Task<TaskResponse> UpdateTaskAsync(string id, TaskDTO taskDTO, string userId);

        Task<TaskResponse> DeleteTaskAsync(string id, string userId);        
    }
}