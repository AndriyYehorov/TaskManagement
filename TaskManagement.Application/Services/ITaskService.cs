using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Services
{
    public interface ITaskService
    {
        public Task<TaskResponse> CreateTaskAsync(TaskDTO taskDTO);     

        public Task<TaskResponse> ReadTaskAsync(Guid id);

        public Task<TasksResponse> ReadAllTasksAsync();

        public Task<TaskResponse> UpdateTaskAsync(Guid id);

        public Task<TaskResponse> DeleteTaskAsync(Guid id);        
    }
}