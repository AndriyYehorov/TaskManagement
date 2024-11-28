using TaskManagement.Application.DTOs;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<TaskResponse> CreateTaskAsync(TaskDTO taskDTO, string userId)
        {
            var newTask = new TaskItem()
            {
                Id = Guid.NewGuid(),
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                DueDate = taskDTO.DueDate,
                Status = taskDTO.Status,
                Priority = taskDTO.Priority,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                UserId = new Guid(userId),
            };

            await _taskRepository.AddTaskAsync(newTask);

            return new TaskResponse(IsSuccess: true, Message: "Task was created successfully", Task: newTask);
        }

        public async Task<TaskResponse> DeleteTaskAsync(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<TasksResponse> ReadAllTasksAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskResponse> ReadTaskAsync(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<TaskResponse> UpdateTaskAsync(Guid id, TaskDTO taskDTO, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
