using Microsoft.EntityFrameworkCore;
using TaskManagement.Application.Repositories;
using TaskManagement.Domain.Entities;
using TaskManagement.Infrastructure.Context;

namespace TaskManagement.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationContext _context;

        public TaskRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddTaskAsync(TaskItem task)
        {
            await _context.Tasks.AddAsync(task);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(TaskItem task)
        {
            _context.Tasks.Remove(task);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(Guid userId)
        {
            return await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<TaskItem?> GetTaskAsync(Guid taskId)
        {
            return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            _context.Tasks.Update(task);

            await _context.SaveChangesAsync();
        }
    }
}
