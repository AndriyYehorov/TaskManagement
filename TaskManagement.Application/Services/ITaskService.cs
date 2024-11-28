using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services
{
    public interface ITaskService
    {
        Task<TaskResponse> CreateTaskAsync(TaskDTO taskDTO);     

        Task<TaskResponse> ReadTaskAsync(Guid id);

        Task<TasksResponse> ReadAllTasksAsync();

        Task<TaskResponse> UpdateTaskAsync(Guid id, TaskDTO taskDTO);

        Task<TaskResponse> DeleteTaskAsync(Guid id);        
    }
}