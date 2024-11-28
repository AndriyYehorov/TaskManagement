using TaskManagement.Application.DTOs;
using TaskManagement.Application.Services;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        public Task<TaskResponse> CreateTaskAsync(TaskDTO taskDTO)
        {
            throw new NotImplementedException();
        }

        public Task<TaskResponse> DeleteTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TasksResponse> ReadAllTasksAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TaskResponse> ReadTaskAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskResponse> UpdateTaskAsync(Guid id, TaskDTO taskDTO)
        {
            throw new NotImplementedException();
        }
    }
}
