using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TaskManagement.Application.DTOs;
using TaskManagement.Application.Repositories;
using TaskManagement.Application.Services;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;
        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<ServiceResponse<TaskItem>> CreateTaskAsync(TaskDTO taskDTO, string userId)
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

            _logger.LogInformation("Task was created successfully. TaskId: {TaskId}.", newTask.Id);

            return new ServiceResponse<TaskItem>(IsSuccess: true, Message: "Task was created successfully", Data: newTask);
        }

        public async Task<ServiceResponse<TaskItem>> DeleteTaskAsync(string id, string userId)
        {
            if (!Guid.TryParse(id, out var taskGuid))
            {
                return new ServiceResponse<TaskItem>(IsSuccess: false, Message: "Wrong id format");
            }

            var taskToDelete = await _taskRepository.GetTaskAsync(taskGuid);

            if (taskToDelete == null)
            {
                return new ServiceResponse<TaskItem>(IsSuccess: false, Message: "Task was not found");
            }

            if (taskToDelete.UserId != new Guid(userId))
            {
                return new ServiceResponse<TaskItem>(IsSuccess: false, Message: "You can`t delete other people's tasks");
            }

            await _taskRepository.DeleteTaskAsync(taskToDelete);

            _logger.LogInformation("Task was deleted successfully. TaskId: {TaskId}.", taskToDelete.Id);

            return new ServiceResponse<TaskItem>(IsSuccess: true, Message: "Task was deleted successfully");  
        }

        public async Task<ServiceResponse<IEnumerable<TaskItem>>> ReadAllTasksAsync(
            TaskFilter? filter,
            string? sortColumn,
            string? sortOrder, 
            int page, 
            int pageSize, 
            string userId)
        {
            var tasks = await _taskRepository.GetAllTasksAsync(
                filter,
                sortColumn, 
                sortOrder, 
                page, 
                pageSize, 
                new Guid(userId));

            if (!tasks.Any())
            {
                return new ServiceResponse<IEnumerable<TaskItem>>(IsSuccess: false, Message: "No tasks were found");
            }

            return new ServiceResponse<IEnumerable<TaskItem>>(IsSuccess: true, Message: $"{tasks.Count()} task(s)", Data: tasks);
        }

        public async Task<ServiceResponse<TaskItem>> ReadTaskAsync(string id, string userId)
        {
            if (!Guid.TryParse(id, out var taskGuid))
            {
                return new ServiceResponse<TaskItem>(IsSuccess: false, Message: "Wrong id format");
            }

            var task = await _taskRepository.GetTaskAsync(taskGuid);

            if (task == null)
            {
                return new ServiceResponse<TaskItem>(IsSuccess: false, Message: "Task was not found");
            }

            if (task.UserId != new Guid(userId))
            {
                return new ServiceResponse<TaskItem>(IsSuccess: false, Message: "You can`t read other people's tasks");
            }

            return new ServiceResponse<TaskItem>(IsSuccess: true, Message: "Here`s your task", Data: task);            
        }

        public async Task<ServiceResponse<TaskItem>> UpdateTaskAsync(string id, TaskDTO taskDTO, string userId)
        {
            if (!Guid.TryParse(id, out var taskGuid))
            {
                return new ServiceResponse<TaskItem>(IsSuccess: false, Message: "Wrong id format");
            }

            var taskToUpdate = await _taskRepository.GetTaskAsync(taskGuid);

            if (taskToUpdate == null)
            {
                return new ServiceResponse<TaskItem>(IsSuccess: false, Message: "Task was not found");
            }

            if (taskToUpdate.UserId != new Guid(userId))
            {
                return new ServiceResponse<TaskItem>(IsSuccess: true, Message: "You can`t edit other people's tasks");
            }

            taskToUpdate.Title = taskDTO.Title;
            taskToUpdate.Description = taskDTO.Description;
            taskToUpdate.DueDate = taskDTO.DueDate;
            taskToUpdate.Status = taskDTO.Status;
            taskToUpdate.Priority = taskDTO.Priority;
            taskToUpdate.UpdatedAt = DateTime.UtcNow;

            await _taskRepository.UpdateTaskAsync(taskToUpdate);

            _logger.LogInformation("Task was updated successfully. TaskId: {TaskId}.", taskToUpdate.Id);

            return new ServiceResponse<TaskItem>(IsSuccess: true, Message: "Task updated successfully", Data: taskToUpdate);            
        }
    }
}
