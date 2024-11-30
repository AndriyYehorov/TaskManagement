using TaskManagement.Application.DTOs;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Services
{
    public interface ITaskService
    {
        Task<ServiceResponse<TaskItem>> CreateTaskAsync(TaskDTO taskDTO, string userId);     

        Task<ServiceResponse<TaskItem>> ReadTaskAsync(string id, string userId);

        Task<ServiceResponse<IEnumerable<TaskItem>>> ReadAllTasksAsync(
            TaskFilter? filter,
            string? sortColumn, 
            string? sortOrder, 
            int page, 
            int pageSize, 
            string userId);

        Task<ServiceResponse<TaskItem>> UpdateTaskAsync(string id, TaskDTO taskDTO, string userId);

        Task<ServiceResponse<TaskItem>> DeleteTaskAsync(string id, string userId);        
    }
}