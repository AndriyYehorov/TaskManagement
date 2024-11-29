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

        public async Task<TaskResponse> DeleteTaskAsync(string id, string userId)
        {
            try
            {
                var taskToDelete = await _taskRepository.GetTaskAsync(new Guid(id));

                if (taskToDelete == null)
                {
                    return new TaskResponse(IsSuccess: false, Message: "Task was not found");
                }

                if (taskToDelete.UserId != new Guid(userId))
                {
                    return new TaskResponse(IsSuccess: false, Message: "You can`t delete other people's tasks");
                }

                await _taskRepository.DeleteTaskAsync(taskToDelete);

                return new TaskResponse(IsSuccess: true, Message: "Task was deleted successfully");
            }
            catch (FormatException)
            {
                return new TaskResponse(IsSuccess: false, Message: "Wrong id format");
            }
        }

        public async Task<TasksResponse> ReadAllTasksAsync(string userId)
        {
            var tasks = await _taskRepository.GetAllTasksAsync(new Guid(userId));

            if (!tasks.Any())
            {
                return new TasksResponse(IsSuccess: false, Message: "No tasks were found");
            }

            return new TasksResponse(IsSuccess: true, Message: $"{tasks.Count()} task(s)", Tasks: tasks);
        }

        public async Task<TaskResponse> ReadTaskAsync(string id, string userId)
        {
            try
            {
                var task = await _taskRepository.GetTaskAsync(new Guid(id));

                if (task == null)
                {
                    return new TaskResponse(IsSuccess: false, Message: "Task was not found");
                }

                if (task.UserId != new Guid(userId))
                {
                    return new TaskResponse(IsSuccess: false, Message: "You can`t read other people's tasks");
                }

                return new TaskResponse(IsSuccess: true, Message: "Here`s your task", Task: task);
            }
            catch(FormatException)
            {
                return new TaskResponse(IsSuccess: false, Message: "Wrong id format");
            }
        }

        public async Task<TaskResponse> UpdateTaskAsync(string id, TaskDTO taskDTO, string userId)
        {
            try
            {
                var taskToUpdate = await _taskRepository.GetTaskAsync(new Guid(id));

                if (taskToUpdate == null)
                {
                    return new TaskResponse(IsSuccess: false, Message: "Task was not found");
                }

                if (taskToUpdate.UserId != new Guid(userId))
                {
                    return new TaskResponse(IsSuccess: false, Message: "You can`t edit other people's tasks");
                }

                taskToUpdate.Title = taskDTO.Title;
                taskToUpdate.Description = taskDTO.Description;
                taskToUpdate.DueDate = taskDTO.DueDate;
                taskToUpdate.Status = taskDTO.Status;
                taskToUpdate.Priority = taskDTO.Priority;
                taskToUpdate.UpdatedAt = DateTime.UtcNow;

                await _taskRepository.UpdateTaskAsync(taskToUpdate);

                return new TaskResponse(IsSuccess: true, Message: "Task updated successfully", Task: taskToUpdate);
            }
            catch (FormatException)
            {
                return new TaskResponse(IsSuccess: false, Message: "Wrong id format");
            }
        }
    }
}
