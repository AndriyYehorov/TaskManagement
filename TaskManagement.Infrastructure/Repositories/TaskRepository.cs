using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagement.Application.DTOs;
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

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(
            TaskFilter? filter,
            string? sortColumn,
            string? sortOrder,
            int page, 
            int pageSize, 
            Guid userId)
        {
            var query = _context.Tasks.Where(t => t.UserId == userId);    
            
            if (filter != null) 
            {
                query = AddFilters(query, filter);
            }            

            if (sortOrder?.ToLower() == "desc")
            {
                query = query.OrderByDescending(GetSortColumn(sortColumn));
            }
            else
            {
                query = query.OrderBy(GetSortColumn(sortColumn));
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        private static IQueryable<TaskItem> AddFilters(IQueryable<TaskItem> query, TaskFilter filter)
        {
            if (filter.Status != null)
            {
                query = query.Where(t => t.Status == filter.Status);
            }

            if (filter.Priority != null)
            {
                query = query.Where(t => t.Priority == filter.Priority);
            }

            if (filter.MinDueDate != null)
            {
                query = query.Where(t => t.DueDate >= filter.MinDueDate);
            }

            if (filter.MaxDueDate != null)
            {
                query = query.Where(t => t.DueDate <= filter.MaxDueDate);
            }

            return query;
        }

        private static Expression<Func<TaskItem, object>> GetSortColumn(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "duedate" => t => t.DueDate,
                "priority" => t => t.Priority,
                _ => t => t.Id
            };
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
