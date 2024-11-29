using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetTaskAsync(Guid taskId);

        Task<IEnumerable<TaskItem>> GetAllTasksAsync(Guid userId);

        Task AddTaskAsync(TaskItem task);

        Task DeleteTaskAsync(TaskItem task);

        Task UpdateTaskAsync(TaskItem task);
    }
}
