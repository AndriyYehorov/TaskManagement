using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Repositories
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetTaskAsync(Guid id);

        Task<IEnumerable<TaskItem>> GetAllTasksAsync();

        Task AddTaskAsync(TaskItem task);

        Task DeleteTaskAsync(TaskItem task);

        Task UpdateTaskAsync(TaskItem task);
    }
}
